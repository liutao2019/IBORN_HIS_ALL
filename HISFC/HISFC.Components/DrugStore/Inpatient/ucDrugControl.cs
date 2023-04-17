using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Management.Pharmacy;
using Neusoft.HISFC.Object.Pharmacy;
using Neusoft.HISFC.Management.Manager;


namespace Neusoft.UFC.DrugStore.Inpatient
{
    public partial class ucDrugControl : Neusoft.NFC.Interface.Controls.ucBaseControl
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
        private Neusoft.HISFC.Management.Pharmacy.DrugStore drugStore = new Neusoft.HISFC.Management.Pharmacy.DrugStore( );
        //���嵱ǰѡ�����
        private Neusoft.NFC.Object.NeuObject currentDept = new Neusoft.NFC.Object.NeuObject( );
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
            this.lvDrugControlList.Columns.Add( "��ҩ̨����" , 120 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "���ó���" , 100 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��������" , 80 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��ʾ�ȼ�" , 120 , HorizontalAlignment.Left );
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
                this.CbxUser.AddItems( DrugAttribute.List( ) );
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

            this.txtName.Text = this.currentDrugControlInfo.Name;
            this.CbxUser.Tag = this.currentDrugControlInfo.DrugAttribute.ID;
            this.CbxSendType.SelectedIndex = this.currentDrugControlInfo.SendType;
            this.CbxShowGrade.SelectedIndex = this.currentDrugControlInfo.ShowLevel;
            this.RtxMark.Text = this.currentDrugControlInfo.Memo;
        }

        /// <summary>
        /// ��ð�ҩ̨�༭��Ϣ(from tabpage2)
        /// </summary>
        /// <returns>��ǰ�༭��ҩ̨</returns>
        public DrugControl GetNewDrugControlInfo( )
        {
            this.currentDrugControlInfo.Name = this.txtName.Text;                   //��ҩ̨����
            this.currentDrugControlInfo.DrugAttribute.ID = this.CbxUser.Tag;        //��ҩִ̨�г���
            this.currentDrugControlInfo.SendType = this.CbxSendType.SelectedIndex;  //ҽ���������ͣ�1���з��ͣ�2��ʱ���ͣ�
            this.currentDrugControlInfo.Memo = this.RtxMark.Text;                   //��ע
            this.currentDrugControlInfo.Dept = this.currentDept;                    //��ҩ����
            this.currentDrugControlInfo.ShowLevel = this.CbxShowGrade.SelectedIndex;//��ʾ�ȼ���0��ʾ���һ��ܣ�1��ʾ������ϸ��2��ʾ������ϸ

            return currentDrugControlInfo;
        }
        /// <summary>
        /// ȡ�ð�ҩ̨��ʾ��������
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
                default:
                    showTypeName = "��ʾ������ϸ";
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

            lvi.SubItems.Add( drugControl.DrugAttribute.Name );
            lvi.SubItems.Add( drugControl.SendType == 0 ? "ȫ��" : ( drugControl.SendType == 1 ? "����" : "��ʱ" ) );
            lvi.SubItems.Add( this.GetShowTypeName(drugControl.SendType) );
            lvi.SubItems.Add( this.currentDept.Name );
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
        /// ���Ӱ�ҩ��
        /// </summary>
        private void AddDrugControl( )
        {
           //�����ҩ��ѡ��״̬
            this.ResetDrugBill( );
           //�����ҩ̨ѡ״̬
            //�����еķǵ�ǰ��ҩ̨Ϊδѡ��״̬
            foreach( ListViewItem L in this.lvDrugControlList.CheckedItems )
            {
                L.Checked = false;
            }
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
            this.neuTabControl1.TabPages.Remove( this.tabPage2 );
            this.neuTabControl1.TabPages.Add( this.tabPage2 );
            this.neuTabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// ɾ��������ҩ��
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
                MessageBox.Show( "��ѡ����Ҫɾ���İ�ҩ̨��" );
                return;
            }

            if( this.currentDrugControlInfo.ID != "" )
            {
                DialogResult result = MessageBox.Show( "��ȷ��Ҫɾ����" + this.currentDrugControlInfo.Name + "����ҩ̨��" , "ɾ����ʾ" , System.Windows.Forms.MessageBoxButtons.OKCancel );
                if( result == DialogResult.Cancel ) return;

                int parm;
                Neusoft.NFC.Management.Transaction trans = new Neusoft.NFC.Management.Transaction( Neusoft.NFC.Management.Connection.Instance );
                this.drugStore.SetTrans( trans.Trans );

                //ɾ���ɰ�ҩ̨��ҩ��ϸ�е���������
                parm = this.drugStore.DeleteDrugControl( this.currentDrugControlInfo.ID );

                if( parm == -1 )
                {
                    trans.RollBack( );
                    MessageBox.Show( this.drugStore.Err ,"��ʾ");
                    return;
                }
                else
                {
                    trans.Commit( );
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
        /// �����ҩ����Ϣ
        /// </summary>
        private void SaveDrugControl( )
        {
            //�ж��Ƿ�ѡ���ҩ��
            if( this.lvPutDrugBill1.CheckedItems.Count == 0 )
            {
                MessageBox.Show( "��Ϊ��ҩ̨��Ӱ�ҩ����" );
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
                MessageBox.Show( "��ѡ��һ����ҩ̨��" );
                return;
            }
            //����������ӵİ�ҩ̨����ȡ�˰�ҩ̨����ˮ��
            if( currentDrugControlInfo.ID == "" )
            {
                currentDrugControlInfo.ID = this.drugStore.GetDrugControlNO( );
                if( currentDrugControlInfo.ID == "-1" )
                {
                    MessageBox.Show( "ȡ��ҩ̨��ˮ��ʱʧ��:" + this.drugStore.Err );
                    return;
                }
            }

            int parm;
            Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction( Neusoft.NFC.Management.Connection.Instance );
            t.BeginTransaction( );
            drugStore.SetTrans( t.Trans );

            try
            {
                //��ɾ���ɰ�ҩ̨��ҩ��ϸ�е��������ݣ�Ȼ������µ����ݡ�
                parm = drugStore.DeleteDrugControl( currentDrugControlInfo.ID );
                if( parm == -1 )
                {
                    t.RollBack( );
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
                            t.RollBack( );
                            MessageBox.Show( this.drugStore.Err );
                            return;
                        }
                    }
                }
                //�ύ���ݿ�
                t.Commit( );
                this.neuTabControl1.TabPages.Remove( this.tabPage2 );
                this.neuTabControl1.SelectedIndex = 0;
                MessageBox.Show( "��ҩ̨���óɹ���" );
            }
            catch( Exception ex )
            {
                t.RollBack( );
                MessageBox.Show( "�����ҩ̨����ʱ����" + ex.Message );
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
            this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 3 ].Text = this.GetShowTypeName( currentDrugControlInfo.SendType );
            this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 4 ].Text = this.currentDept.Name;
            this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 5 ].Text = currentDrugControlInfo.Memo;
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
                    //��ǰѡ��Ŀ���
                    this.currentDept = e.Tag as Neusoft.NFC.Object.NeuObject;

                    //��ʾ��ǰ���ҵİ�ҩ̨��Ϣ
                    this.ShowDrugControlByDept( );
                }
                else
                {
                    this.currentDept = new Neusoft.NFC.Object.NeuObject( );
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message ,"��ʾ");
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
                //��ʾ��ҩ���༭��Ϣ
                this.neuTabControl1.TabPages.Remove( this.tabPage2 );
                this.neuTabControl1.TabPages.Add( this.tabPage2 );
                this.SetDrugControlInfo( this.lvDrugControlList.SelectedItems[ 0 ].Tag as DrugControl );
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
                //�����еķǵ�ǰ��ҩ̨Ϊδѡ��״̬
                foreach( ListViewItem lvi in this.lvDrugControlList.CheckedItems )
                {
                   lvi.Checked = false;
                }
                this.lvDrugControlList.SelectedItems[ 0 ].Checked = true;
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
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
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
        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit( object sender , object neuObject , object param )
        {
            //this.ToolButtonClicked += new EventHandler( ToolButton_clicked );
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "���Ӱ�ҩ̨" , 0 , true , false ,null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ����ҩ̨" , 1 , true , false , null );
            this.toolBarService.AddToolButton( "����" , "��������" , 2 , true , false , null );
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
                case "����":
                    this.AddDrugControl( );
                    break;
                case "ɾ��":
                    this.DeleteDrugControl( );
                    break;

                case "����":
                    this.SaveDrugControl( );
                    break;
            }
           
        }
        #endregion


    }
}
