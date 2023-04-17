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
    /// [�ؼ�����:ucSpecialDrugTerminal]<br></br>
    /// [��������: ���������ն�ά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-29]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucSpecialDrugTerminal : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucSpecialDrugTerminal( )
        {
            InitializeComponent( );
        }

        #region ����

        /// <summary>
        /// �洢ҩƷ�б�
        /// </summary>
        List<Neusoft.HISFC.Object.Pharmacy.Item> drugItemList;
        /// <summary>
        /// �洢�����б�
        /// </summary>
        ArrayList deptList;
        /// <summary>
        /// �洢�շ�����б�
        /// </summary>
        ArrayList feeItemList;
        /// <summary>
        /// �Һż���
        /// </summary>
        ArrayList regLevelList;
        /// <summary>
        /// ��Ա������
        /// </summary>
        private Neusoft.NFC.Public.ObjectHelper personHelper = new Neusoft.NFC.Public.ObjectHelper( );
        /// <summary>
        /// ����ԱȨ�޿���
        /// </summary>
        private Neusoft.NFC.Object.NeuObject privDept = new Neusoft.NFC.Object.NeuObject( );

        /// <summary>
        /// ҵ��������
        /// </summary>
        private Neusoft.HISFC.Management.Pharmacy.DrugStore drugStore = new Neusoft.HISFC.Management.Pharmacy.DrugStore( );
        /// <summary>
        /// ������
        /// </summary>
        Neusoft.HISFC.Integrate.Manager manager = new Neusoft.HISFC.Integrate.Manager( );
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
                this.toolBarService.SetToolButtonEnabled( "����" , value );
                this.toolBarService.SetToolButtonEnabled( "ɾ��" , value );
                this.toolBarService.SetToolButtonEnabled( "����" , value );
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void InitConstant( )
        {
            //��ȡҩƷ����Ϣ�б�
            Neusoft.HISFC.Management.Pharmacy.Item item = new Neusoft.HISFC.Management.Pharmacy.Item( );

            drugItemList = item.QueryItemAvailableList( );
            if( drugItemList == null )
            {
                MessageBox.Show( item.Err );
                return;
            }

            //��ȡ������Ϣ�б�
            deptList = manager.GetDepartment( Neusoft.HISFC.Object.Base.EnumDepartmentType.C );
            if( deptList == null )
            {
                MessageBox.Show( manager.Err );
                return;
            }

            //��ȡ�������
            manager.GetConstantList( Neusoft.HISFC.Object.Base.EnumConstant.PACKUNIT );
            if( feeItemList == null )
            {
                MessageBox.Show( manager.Err );
                return;
            }

            //��ȡ��Ա�б�
            ArrayList al = manager.QueryEmployeeAll( );
            if( al == null )
            {
                MessageBox.Show( "��ȡ��Ա��Ϣ����" + manager.Err );
                return;
            }
            this.personHelper.ArrayObject = al;

            //��ȡ�Һż���(��ʱ����)
            //Neusoft.HISFC.Management.Registration.RegLevel regLevelManager = new Neusoft.HISFC.Management.Registration.RegLevel( );
            //this.regLevelList = regLevelManager.Query( );
            //if( this.regLevelList == null )
            //{
            //    MessageBox.Show( "��ȡ�Һż������" + regLevelManager.Err );
            //    return -1;
            //}

            return;
        }

        /// <summary>
        /// ����������ʾ
        /// </summary>
        private void ShowData( )
        {
            int index = this.neuSpread1.ActiveSheetIndex + 1;
            try
            {
                ArrayList al = this.drugStore.QueryDrugSPETerminalByDeptCode( this.privDept.ID , index.ToString( ) );
                if( al == null )
                {
                    MessageBox.Show( Language.Msg( "��ȡ������ҩ̨��Ϣ����!" ) + this.drugStore.Err );
                    return;
                }

                this.neuSpread1.Sheets[ index ].Rows.Count = al.Count;
                Neusoft.HISFC.Object.Pharmacy.DrugSPETerminal info;

                for( int i = 0 ; i < al.Count ; i++ )
                {
                    info = al[ i ] as Neusoft.HISFC.Object.Pharmacy.DrugSPETerminal;
                    if( info == null )
                    {
                        continue;
                    }
                    //��ҩ̨����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 0 ].Text = info.Terminal.Name;
                    //������Ŀ����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 1 ].Text = info.Item.Name;
                    //��ע
                    this.neuSpread1.Sheets[ index ].Cells[ i , 2 ].Text = info.Memo;
                    //����Ա
                    if( this.personHelper != null )
                    {
                        this.neuSpread1.Sheets[ index ].Cells[ i , 3 ].Text = this.personHelper.GetName( info.Oper.ID );
                    }
                    //����ʱ��
                    this.neuSpread1.Sheets[ index ].Cells[ i , 4 ].Text = info.Oper.OperTime.ToString( );
                    //��ҩ̨����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 5 ].Text = info.Terminal.ID;
                    //������Ŀ����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 6 ].Text = info.Item.ID;
                    // 0 ���ݿ���� 1 ���������
                    this.neuSpread1.Sheets[ index ].Cells[ i , 7 ].Text = "0";
                    //ʵ��
                    this.neuSpread1.Sheets[ index ].Rows[ i ].Tag = info;
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
                return;
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ���ڳ�ʼ��
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
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm( Language.Msg( "���ڼ��������ն���Ϣ...." ) );
            Application.DoEvents( );

            //��ʼ�������ն�����
            this.ucDrugTerminalList1.InitDeptTerminal( this.privDept.ID );

            //���̼߳�������
            System.Threading.ThreadStart start = new System.Threading.ThreadStart( this.InitConstant );
            System.Threading.Thread thread = new System.Threading.Thread( start );
            thread.Start( );

            //���õ�ǰѡ�еĵ�һ��sheet
            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            //��ʼ������
            //this.ShowData( );

            Neusoft.NFC.Interface.Classes.Function.HideWaitForm( );

            base.OnLoad( e );
        }

        /// <summary>
        /// ˫��sheettabʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_SheetTabClick( object sender , FarPoint.Win.Spread.SheetTabClickEventArgs e )
        {
            
        }

        #endregion

        #region ��������Ϣ

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
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "���Ӽ�¼" , 0 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ����¼" , 1 , true , false , null );

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
                    break;
                case "ɾ��":
                    break;
            }

        }

        #endregion

        private void neuSpread1_ActiveSheetChanged( object sender , EventArgs e )
        {
            this.ShowData( );
        }


    }
}
