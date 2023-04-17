using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.HISFC.Object.Pharmacy;

namespace Neusoft.UFC.DrugStore.Inpatient
{
    public partial class ucDrugBill : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucDrugBill( )
        {
            InitializeComponent( );
        }

        #region �¼�

        /// <summary>
        /// ѡ���ҩ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvPutDrugBill1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                //�����еķǵ�ǰ��ҩ��Ϊδѡ��״̬
                foreach( ListViewItem lvi in this.lvPutDrugBill1.CheckedItems )
                {
                    lvi.Checked = false;
                }
                this.lvPutDrugBill1.SelectedItems[ 0 ].Checked = true;
            }
            else
            {

            }
        }

        private void lvPutDrugBill1_DoubleClick( object sender , EventArgs e )
        {

        }

        #endregion

        #region ��������Ϣ

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
            //��ʼ����ӡ����
            this.cbxPrinttype.AddItems( BillPrintType.List( ) );
            //��ʼ����ҩ����
            this.cbxPutType.AddItems( DrugAttribute.List( ) );
            //����tabpage2
            //this.neuTabControl1.TabPages.Remove( this.tabPage2 );
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
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "���Ӱ�ҩ��" , 0 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ����ҩ��" , 1 , true , false , null );
            this.toolBarService.AddToolButton( "����" , "��������" , 2 , true , false , null );
            this.toolBarService.AddToolButton( "��������" , "��������" , 3 , true , false , null );
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
                    //this.AddDrugControl( );
                    break;
                case "ɾ��":
                    //this.DeleteDrugControl( );
                    break;
                case "����":
                    //this.SaveDrugControl( );
                    break;
                case "��������":
                    //this.SaveDrugControl( );
                    break;
            }

        }
        #endregion

    }
}
