using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.HISFC.Components.DrugStore.InBase
{
    /// <summary>
    /// [�ؼ�����:lvPutDrugBill]<br></br>
    /// [��������: ��ҩ���ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-9]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class lvPutDrugBill : FS.FrameWork.WinForms.Controls.NeuListView
    {
        public lvPutDrugBill( )
        {
            InitializeComponent( );
            //��ʼ��
            try
            {
                this.InitListView();
            }
            catch
            {
            }
        }
        /// <summary>
        /// �������Ĺ�����
        /// </summary>
        /// <param name="container"></param>
        public lvPutDrugBill( IContainer container )
        {
            InitializeComponent( );
            container.Add( this );
            //��ʼ��
            try
            {
                this.InitListView( );
            }
            catch
            {
            }
        }

        #region ����
        /// <summary>
        /// �Ƿ���Ա༭��ҩ����Ϣ
        /// </summary>
        private bool isAllowEdit = true;

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ���Ա༭��ҩ����Ϣ
        /// </summary>
        [Description( "�Ƿ���Ա༭��ҩ����Ϣ" ) , Category( "����" ) , DefaultValue( true )]
        public bool AllowEdit
        {
            get
            {
                return this.isAllowEdit;
            }
            set
            {
                this.isAllowEdit = value;
            }

        }
        /// <summary>
        /// ��ǰѡ�еİ�ҩ����Ϣ
        /// </summary>
        [Description( "��ǰѡ�еİ�ҩ����Ϣ" ) , Category( "����" )]
        public virtual List<DrugBillClass> SelectedDrugBill
        {
            get
            {
                List<DrugBillClass> selectedBill = new List<DrugBillClass>( );
                if( this.CheckBoxes && this.Items.Count > 0 )
                {
                    foreach( ListViewItem item in this.Items )
                    {
                        if( item.Checked )
                        {
                            selectedBill.Add( item.Tag as DrugBillClass );
                        }
                    }
                }
                return selectedBill;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��ʼ����ҩ����Ϣ
        /// </summary>
        protected virtual void InitListView( )
        {
            if( this.DesignMode )
            {
                return;
            }
            this.SuspendLayout( );
            this.Columns.Clear( );
            this.Items.Clear( );

            this.Columns.Add( "��ҩ������" , 160 , HorizontalAlignment.Left );
            this.Columns.Add( "��ӡ����" , 70 , HorizontalAlignment.Center );
            this.Columns.Add( "�Ƿ���Ч" , 70 , HorizontalAlignment.Center );
            this.Columns.Add( "��ע" , 200 , HorizontalAlignment.Left );

            try
            {
                FS.HISFC.BizLogic.Pharmacy.DrugStore drugStore = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
                ArrayList drugBillClassList = new ArrayList();
                drugBillClassList = drugStore.QueryDrugBillClassList();

                bool isHaveRBill = false;
                bool isHavePBill = false;

                foreach (DrugBillClass billClass in drugBillClassList)
                {
                    if (billClass.ID == "R")
                    {
                        isHaveRBill = true;
                    }
                    if (billClass.ID == "P")
                    {
                        isHavePBill = true;
                    }

                    this.AddItem(billClass, false);
                }

                this.SaveDefaultBill(!isHavePBill, !isHaveRBill);
            }
            catch
            {

            }
            this.ResumeLayout( );
        }

        /// <summary>
        /// Ĭ�ϰ�ҩ������
        /// </summary>
        protected virtual void SaveDefaultBill(bool isP,bool isR)
        {
            if (isR)            //������ҩ��ҩ��
            {
                FS.HISFC.Models.Pharmacy.DrugBillClass pDrugBill = new DrugBillClass();

                pDrugBill.ID = "R";
                pDrugBill.Name = "��ҩ��";                       //��ҩ��������
                pDrugBill.PrintType.ID = BillPrintType.enuBillPrintType.D ;           //��ӡ����
                pDrugBill.DrugAttribute.ID = DrugAttribute.enuDrugAttribute.T; //��ҩ����
                pDrugBill.IsValid = true;              //�Ƿ���Ч
                pDrugBill.Memo = "��ҩ������ά����ϸ";               //��ע

                FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

                if (drugStoreManager.InsertDrugBillClass(pDrugBill) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���������ҩ��ʧ��"));
                    return;
                }
            }

            if (isP)            //���÷�ҽ����ҩ��
            {
                FS.HISFC.Models.Pharmacy.DrugBillClass pDrugBill = new DrugBillClass();

                pDrugBill.ID = "P";
                pDrugBill.Name = "��ҽ����ҩ��";                       //��ҩ��������
                pDrugBill.PrintType.ID = BillPrintType.enuBillPrintType.D;           //��ӡ����
                pDrugBill.DrugAttribute.ID = DrugAttribute.enuDrugAttribute.T; //��ҩ����
                pDrugBill.IsValid = true;              //�Ƿ���Ч
                pDrugBill.Memo = "��ҽ����ҩ������ά����ϸ";               //��ע

                FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

                if (drugStoreManager.InsertDrugBillClass(pDrugBill) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���������ҩ��ʧ��"));
                    return;
                }
            }
 
        }

        /// <summary>
        /// ���ӵ�����ҩ����Ϣ
        /// </summary>
        /// <param name="billClass">��ҩ������ʵ��</param>
        /// <param name="isAutoSelect">�Ƿ��Զ�ѡ��</param>
        /// <returns></returns>
        public virtual int AddItem( DrugBillClass billClass,bool isAutoSelect)
        {
            if( billClass != null )
            {
                //���ò���Ľڵ���Ϣ
                ListViewItem lvi = new ListViewItem( );

                //������Ч����Ч����Ŀͼ��
                if( billClass.IsValid )
                {
                    lvi.ImageIndex = 0;
                }
                else
                {
                    lvi.ImageIndex = 1;
                }
                //����������ӵ���Ŀ����ʾ�����ͼ��
                if( billClass.ID == "" )
                {
                    lvi.ImageIndex = 2;
                }

                //Tag���Ա����ҩ������ʵ��
                lvi.Tag = billClass;

                lvi.Text = billClass.Name;

                //���listView���ӽڵ�
                lvi.SubItems.Add( billClass.PrintType.Name );
                lvi.SubItems.Add( billClass.IsValid ? "��Ч" : "��Ч" );
                lvi.SubItems.Add( billClass.Memo );

                this.Items.Add( lvi );

                if (isAutoSelect)
                {
                    lvi.Selected = true;
                }

                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// �޸ĵ�����ҩ����Ϣ
        /// </summary>
        /// <param name="billClass">��ҩ������ʵ��</param>
        /// <returns></returns>
        public virtual int ModifyItem( DrugBillClass billClass , int index )
        {
            if( billClass != null && index >= 0 )
            {
                //������Ч����Ч����Ŀͼ��
                if( billClass.IsValid )
                {
                    this.Items[ index ].ImageIndex = 0;
                }
                else
                {
                    this.Items[ index ].ImageIndex = 1;
                }
                //����������ӵ���Ŀ����ʾ�����ͼ��
                if( billClass.ID == "" )
                {
                    this.Items[ index ].ImageIndex = 2;
                }

                //Tag���Ա����ҩ������ʵ��
                this.Items[ index ].Tag = billClass;
                //���listView���ӽڵ�
                this.Items[ index ].SubItems[ 0 ].Text = billClass.Name;
                this.Items[ index ].SubItems[ 1 ].Text = billClass.PrintType.Name;
                this.Items[ index ].SubItems[ 2 ].Text = billClass.IsValid ? "��Ч" : "��Ч";
                this.Items[ index ].SubItems[ 3 ].Text = billClass.Memo;
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// ɾ��������ҩ����Ϣ
        /// </summary>
        /// <param name="billClass">��ҩ������ʵ��</param>
        /// <returns></returns>
        public virtual int DeleteItem( DrugBillClass billClass )
        {
            if( billClass != null )
            {
                foreach( ListViewItem item in this.Items )
                {
                    if( item.Tag == billClass )
                    {
                        this.Items.Remove( item );
                    }
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// ɾ��������ҩ����Ϣ
        /// </summary>
        /// <param name="index">Ҫɾ�����б�����</param>
        /// <returns></returns>
        public virtual int DeleteItem( int index )
        {
            if( index >= 0 )
            {
                this.Items.RemoveAt( index );
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// ���ѡ��
        /// </summary>
        /// <returns></returns>
        public virtual void ClearSelection()
        {
            foreach (ListViewItem lv in this.Items)
            {
                lv.Selected = false;
            }
        }
        #endregion
    }
}
