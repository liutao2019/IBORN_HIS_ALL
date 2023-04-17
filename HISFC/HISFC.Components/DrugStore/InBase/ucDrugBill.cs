using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.DrugStore.InBase
{
    /// <summary>
    /// [�ؼ�����:ucDrugBill]<br></br>
    /// [��������: ��ҩ������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-10]<br></br>
    /// <�޸ļ�¼>
    ///     �ĵķ�����������
    ///     ���� д�ĳ���
    /// </�޸ļ�¼>
    ///  />
    /// </summary>
    public partial class ucDrugBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugBill( )
        {
            InitializeComponent( );
        }

        #region ����

        //��ҩ������ʵ����
        private DrugBillClass drugBillClassInfo;
        //ҩ��������
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore( ); 

        #endregion

        #region ����

        #region ��ҩ������

        /// <summary>
        /// �жϴ����ҩ����Ϣ�Ƿ���Ч
        /// </summary>
        /// <param name="drugBillClass">��ҩ����Ϣ</param>
        /// <returns>�ɹ�����True  ʧ�ܷ���False</returns>
        private bool IsDrugBillDataValid(DrugBillClass drugBillClass)
        {
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugBillClass.Memo, 150))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ע�ֶγ���"));
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugBillClass.Name, 30))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ�����Ƴ���"));
                return false;
            }

            foreach (ListViewItem lv in this.lvPutDrugBill1.Items)
            {
                DrugBillClass tempDrugBill = lv.Tag as DrugBillClass;
                if (tempDrugBill == null)
                {
                    continue;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(tempDrugBill.Memo, 150))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ע�ֶγ���"));
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(tempDrugBill.Name, 30))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ�����Ƴ���"));
                    return false;
                }

                if (tempDrugBill.ID == drugBillClass.ID)
                {
                    continue;
                }                

                if (lv.Text == drugBillClass.Name)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(drugBillClass.Name + "��ҩ�������ظ� ������ά��"));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��tabpage2����ȡ���ݣ�������myDrugBillClass�С�
        /// </summary>
        private DrugBillClass GetDrugBillItem( )
        {
            if( this.drugBillClassInfo == null )
            {
                drugBillClassInfo = new DrugBillClass( );
            }

            this.drugBillClassInfo.Name = this.txtName.Text;                       //��ҩ��������
            this.drugBillClassInfo.PrintType.ID = this.cbxPrinttype.Tag;           //��ӡ����
            this.drugBillClassInfo.DrugAttribute.ID = this.cbxPutType.Tag.ToString(); //��ҩ����
            this.drugBillClassInfo.IsValid = this.cbxIsValid.Checked;              //�Ƿ���Ч
            this.drugBillClassInfo.Memo = this.txtMark.Text;               //��ע
            return this.drugBillClassInfo;
        }

        /// <summary>
        /// ��myDrugBillClass��ȡ���ݣ���ʾ��tabpage2�С�
        /// </summary>
        private void SetDrugBillItem( DrugBillClass drugbill )
        {
            this.drugBillClassInfo = drugbill;

            this.txtName.Text = this.drugBillClassInfo.Name;          //��ҩ��������
            this.cbxPrinttype.Tag = this.drugBillClassInfo.PrintType.ID;  //��ӡ����
            this.cbxPutType.Tag = this.drugBillClassInfo.DrugAttribute.ID;   //��ҩ����
            this.cbxIsValid.Checked = this.drugBillClassInfo.IsValid;       //�Ƿ���Ч
            this.txtMark.Text = this.drugBillClassInfo.Memo;          //��ע
            //�����Ұ�ҩ�������޸�
            if( this.drugBillClassInfo.ID == "P" || this.drugBillClassInfo.ID == "R" )
            {
                this.lvPutDrugBill1.AllowEdit = false;
            }
            else
            {
                this.lvPutDrugBill1.AllowEdit = true;
            }
        }

        #endregion

        #region ��ҩ����������

        /// <summary>
        /// ���ð�ҩ�������б�
        /// </summary>
        private void ResetDrugBillAttribute( )
        {
            //���ҽ������
            this.tvAdviceKinde.Nodes[ 0 ].Checked = false;

            //���ҩƷ�÷�
            this.tvUse.Nodes[ 0 ].Checked = false;

            //���ҩƷ����
            this.tvMode.Nodes[ 0 ].Checked = false;

            //���ҩƷ����
            this.tvQuality.Nodes[ 0 ].Checked = false;

            //���ҩƷ����
            this.tvType.Nodes[ 0 ].Checked = false;
        }

        /// <summary>
        /// ���ݰ�ҩ�������ʼ����ҩ��������
        /// </summary>
        /// <param name="drugBillClassCode">��ҩ������</param>
        public void ShowListByDrugBill( string drugBillClassCode )
        {
            try
            {
                ArrayList al;
                //ҽ�����
                this.tvAdviceKinde.Nodes[ 0 ].Checked = false;
                al = this.drugStoreManager.QueryDrugBillList( drugBillClassCode , "TYPE_CODE" );
                foreach( DrugBillList info in al )
                {
                    foreach( TreeNode tn in this.tvAdviceKinde.Nodes[ 0 ].Nodes )
                    {
                        NeuObject obj = ( NeuObject )tn.Tag;
                        if( info.ID == obj.ID )
                        {
                            tn.Checked = true;
                        }
                    }
                }
                //ҩƷ�÷�
                this.tvUse.Nodes[ 0 ].Checked = false;
                al = this.drugStoreManager.QueryDrugBillList( drugBillClassCode , "USAGE_CODE" );
                foreach( DrugBillList info in al )
                {
                    foreach( TreeNode tn in this.tvUse.Nodes[ 0 ].Nodes )
                    {
                        NeuObject obj = ( NeuObject )tn.Tag;
                        if( info.ID == obj.ID )
                        {
                            tn.Checked = true;
                        }
                    }
                }
                //ҩƷ����
                this.tvMode.Nodes[ 0 ].Checked = false;
                al = this.drugStoreManager.QueryDrugBillList( drugBillClassCode , "DOSE_MODEL_CODE" );
                foreach( DrugBillList info in al )
                {
                    foreach( TreeNode tn in this.tvMode.Nodes[ 0 ].Nodes )
                    {
                        NeuObject obj = ( NeuObject )tn.Tag;
                        if( info.ID == obj.ID )
                        {
                            tn.Checked = true;
                        }
                    }
                }
                //ҩƷ����
                this.tvQuality.Nodes[ 0 ].Checked = false;
                al = this.drugStoreManager.QueryDrugBillList( drugBillClassCode , "DRUG_QUALITY" );
                foreach( DrugBillList info in al )
                {
                    foreach( TreeNode tn in this.tvQuality.Nodes[ 0 ].Nodes )
                    {
                        NeuObject obj = ( NeuObject )tn.Tag;
                        if( info.ID == obj.ID )
                        {
                            tn.Checked = true;
                        }
                    }
                }
                //ҩƷ���
                this.tvType.Nodes[ 0 ].Checked = false;
                al = this.drugStoreManager.QueryDrugBillList( drugBillClassCode , "DRUG_TYPE" );
                foreach( DrugBillList info in al )
                {
                    foreach( TreeNode tn in this.tvType.Nodes[ 0 ].Nodes )
                    {
                        NeuObject obj = ( NeuObject )tn.Tag;
                        if( info.ID == obj.ID )
                        {
                            tn.Checked = true;
                        }
                    }
                }
            }
            catch( Exception e )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ʼ����ҩ��������Ϣ����" ) + e.Message );
            }
        }

        #endregion

        #region ���ݲ���

        /// <summary>
        /// ���Ӱ�ҩ��
        /// </summary>
        private void AddDrugBill()
        {
            if (this.neuTabControl1.TabPages.ContainsKey(tabPage2.Name))
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage2);
            }
            //����Ҫ����Ľڵ�
            DrugBillClass info = new DrugBillClass( );
            info.Name = "�½���ҩ��";
            info.IsValid = true;

            //����ϸ��Ϣ����ʾ�����ӵİ�ҩ��
            this.SetDrugBillItem( info );

            //���ð�ҩ��������δѡ��״̬
            this.ResetDrugBillAttribute( );

            this.neuTabControl1.TabPages.Add( this.tabPage2 );
            this.neuTabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// �޸İ�ҩ�����
        /// </summary>
        private void ModifyDrugBill( )
        {
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                if (this.drugBillClassInfo.ID == "" || this.drugBillClassInfo.ID == null)
                {
                    this.lvPutDrugBill1.ClearSelection();

                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ��Ҫ�޸ĵİ�ҩ��"));
                    return;
                }

                //��ʾ��ҩ���༭��Ϣ
                this.neuTabControl1.TabPages.Add( this.tabPage2 );
                this.neuTabControl1.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��Ҫ�޸ĵİ�ҩ��"));
                //�������ð�ҩ������
                this.neuTabControl1.SelectedIndex = 1;
                this.ResetDrugBillAttribute( );
                this.drugBillClassInfo = new DrugBillClass( );

            }
        }

        /// <summary>
        /// ɾ��һ����ҩ����������
        /// </summary>
        private void DeleteDrugBill( )
        {
            //�ж��Ƿ�ѡ��һ����ҩ��
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                //��ȡ��ǰ��ҩ����Ϣ
                this.GetDrugBillItem( );
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ����Ҫɾ���İ�ҩ����" ));

                return;
            }

            if (this.drugBillClassInfo.ID == "P" || this.drugBillClassInfo.ID == "R")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ������ҽ����ҩ��������ɾ��"),"",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if( this.drugBillClassInfo.ID != "" )
            {
    
                //������ʾ����
                System.Windows.Forms.DialogResult result;
                result = MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ҩ��ɾ���󽫲��ɻָ�,��ȷ��Ҫɾ����" + drugBillClassInfo.Name + "����ҩ����" ), FS.FrameWork.Management.Language.Msg( "ɾ����ʾ") , System.Windows.Forms.MessageBoxButtons.OKCancel );
                if( result == DialogResult.Cancel ) return;

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm( FS.FrameWork.Management.Language.Msg( "����ɾ����ҩ��������ϸ��Ϣ�����Ե�..." ));
                Application.DoEvents( );

                //ɾ������
                int parmClass;
                int parmList;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );

                drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //��ɾ����ҩ��������Ϣ
                parmList = drugStoreManager.DeleteDrugBillList( this.drugBillClassInfo.ID );
                //��ɾ����ҩ����Ϣ
                parmClass = drugStoreManager.DeleteDrugBillClass( this.drugBillClassInfo.ID );

                if( parmList == -1 || parmClass == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this.drugStoreManager.Err , FS.FrameWork.Management.Language.Msg( "FS.FrameWork.Management.Language.Msg( ��ʾ"));
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                    return;
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ɾ���ɹ���") );
                    
                }
            }
            //���ð�ҩ��������δδѡ��״̬
            this.ResetDrugBillAttribute( );
            //ɾ���ڵ�
            this.lvPutDrugBill1.DeleteItem( this.lvPutDrugBill1.SelectedIndices[ 0 ] );
            this.lvPutDrugBill1.Focus( );
            this.neuTabControl1.SelectedIndex = 0;
            this.drugBillClassInfo = new DrugBillClass( );
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
        }

        /// <summary>
        /// �����ҩ�����
        /// </summary>
        private void SaveDrugBill()
        {
            //��ȡ��ǰ��ҩ�����µı༭��Ϣ
            this.GetDrugBillItem();
            //��Ч���ж�
            if (!this.IsDrugBillDataValid(this.drugBillClassInfo))
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isNewDrugBill = false;
            //�������
            if (this.drugBillClassInfo.ID == "")
            {
                isNewDrugBill = true;
            }

            int parm = drugStoreManager.SetDrugBillClass(this.drugBillClassInfo);
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.drugStoreManager.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));

            if (isNewDrugBill)
            {
                this.lvPutDrugBill1.AddItem(this.drugBillClassInfo,true);

                this.drugBillClassInfo = new DrugBillClass();
            }
            else
            {
                //�ø��º�Ľڵ���Ϣ�޸�ListView�ж�Ӧ�Ľڵ�
                this.lvPutDrugBill1.ModifyItem(this.drugBillClassInfo, this.lvPutDrugBill1.SelectedIndices[0]);
            }

            this.neuTabControl1.SelectedIndex = 0;            
        }

        /// <summary>
        /// �����ҩ����ϸ��Ϣ
        /// </summary>
        /// <param name="isIncrement">�Ƿ���������</param>
        private void SaveDrugBillList( bool isIncrement )
        {
            if( this.drugBillClassInfo == null )
            {
                return;
            }
            //�жϴ�������Ƿ���Ч
            if( this.drugBillClassInfo.ID == "" )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "���ڰ�ҩ�������б���ѡ��Ҫά���ļ�¼������" ));
                return ;
            }
            //��ҽ����ҩ������ҩ��������������ϸ��Ϣ
            if(this.drugBillClassInfo.ID == "P" )
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(  "��ҽ����ҩ��(�����Ұ�ҩ��)����Ҫ������ϸ��Ϣ��") );
                return ;
            }
            if (this.drugBillClassInfo.ID == "R")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ������Ҫ������ϸ��Ϣ��"));
                return;
            }
            //��ʾȷ���Ƿ񱣴�
            if (!isIncrement)
            {
                DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("ȷ�Ͻ���ȫ�����������? ���������ʱʱ��ϳ�"),"",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (rs == DialogResult.No)
                {
                    return;
                }
            }

            #region ȡ�����б��б�ѡ�е��������Ƿ�©ѡ
            //ҽ������
            ArrayList alOrderType = new ArrayList( );
            foreach( TreeNode tn in this.tvAdviceKinde.Nodes[ 0 ].Nodes )
            {
                if( tn.Checked )
                {
                    alOrderType.Add( ( FS.HISFC.Models.Order.OrderType )tn.Tag ); 
                }
            }
            if( alOrderType.Count == 0 )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��ҽ������" ));
                return ;
            }
            //ҩƷ�÷�
            List<FS.HISFC.Models.Base.Const> alUsage = this.tvUse.SelectedNodes;
            if( alUsage.Count == 0 )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��ҩƷ�÷�" ));
                return ;
            }
            //ҩƷ����
            List<FS.HISFC.Models.Base.Const> alDosageForm = this.tvMode.SelectedNodes;
            if( alDosageForm.Count == 0 )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��ҩƷ����" ));
                return ;
            }
            //ҩƷ����
            List<FS.HISFC.Models.Base.Const> alDrugQuality = this.tvQuality.SelectedNodes;
            if( alDrugQuality.Count == 0 )
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg( "��ѡ��ҩƷ����" ));
                return ;
            }
            //ҩƷ����
            List<FS.HISFC.Models.Base.Const> alDrugType = this.tvType.SelectedNodes;
            if( alDrugType.Count == 0 )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��ҩƷ����" ));
                return ;
            }
            #endregion

            int parm;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );
            //t.BeginTransaction( );

            drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm( FS.FrameWork.Management.Language.Msg( "���ڱ����ҩ��������ϸ��Ϣ�����Ե�..." ));
            Application.DoEvents( );

            //���ݲ����ж��Ƿ���Ҫ��ɾ�������ӡ�
            if( !isIncrement )
            {
                //��ɾ���ɰ�ҩ��������ϸ�е��������ݣ�Ȼ������µ����ݡ�
                parm = drugStoreManager.DeleteDrugBillList( this.drugBillClassInfo.ID );
                if( parm == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this.drugStoreManager.Err );
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                    return ;
                }
            }

            //���������ݣ���ҽ�����ͣ��÷������ͣ�ҩƷ���ʣ�ҩ�����͵�ȫ���зֱ������ϸ��	
            DrugBillList myList = new DrugBillList( );
            myList.DrugBillClass.ID = this.drugBillClassInfo.ID;
            int pro = 0; //����������ʾ������
            int max = alOrderType.Count * alUsage.Count * alDosageForm.Count * alDrugQuality.Count * alDrugType.Count;
            foreach( FS.HISFC.Models.Order.OrderType OrderType in alOrderType )
            {
                foreach( NeuObject Usage in alUsage )
                {
                    foreach( NeuObject DosageForm in alDosageForm )
                    {
                        foreach( NeuObject DrugQuality in alDrugQuality )
                        {
                            foreach( NeuObject DrugType in alDrugType )
                            {
                                //Ϊ��ҩ����ϸʵ����ֵ
                                myList.OrderType = OrderType ;
                                myList.Usage = Usage;
                                myList.DosageForm = DosageForm;
                                myList.DrugQuality = DrugQuality;
                                myList.DrugType = DrugType;

                                //�����ҩ��������ϸ��
                                parm = this.drugStoreManager.InsertDrugBillList( myList );
                                if( parm != 1 )
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    if( this.drugStoreManager.DBErrCode == 1 )
                                    {
                                        MessageBox.Show(FS.FrameWork.Management.Language.Msg( "�����Ѿ����ڣ������ظ�ά���� �������Ƿ���������ҩ�����Ѵ������������Ϣ\n") +
                                            " ҽ������;" + OrderType.ID + OrderType.Name +
                                            " �÷�:" + Usage.ID + Usage.Name +
                                            " ����:" + DosageForm.ID + DosageForm.Name +
                                            " ҩƷ����:" + DrugQuality.ID + DrugQuality.Name +
                                            " ҩƷ����:" + DrugType.ID + DrugType.Name );
                                    }
                                    else
                                    {
                                        MessageBox.Show( this.drugStoreManager.Err );
                                    }
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                                    return ;
                                }
                                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm( pro++ , max );
                                Application.DoEvents( );
                            }
                        }
                    }
                }
            }
            //�ύ���ݿ�
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg( "����ɹ���") );
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
            return;
        }

        #endregion

        #endregion

        #region �¼�
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            try
            {
                //��ʼ����ӡ����
                this.cbxPrinttype.AddItems(BillPrintType.List());
                //��ʼ����ҩ����
                this.cbxPutType.AddItems(DrugAttribute.List());
                //����tabpage2
                this.neuTabControl1.TabPages.Remove(this.tabPage2);

                this.lvPutDrugBill1.CheckBoxes = false;
                this.lvPutDrugBill1.MultiSelect = false;
            }
            catch { }

            base.OnLoad( e );
        }

        /// <summary>
        /// �����ҩ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender , EventArgs e )
        {
            this.SaveDrugBill( );
        }

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
                //���õ�ǰ��ҩ����Ϣ
                this.SetDrugBillItem( this.lvPutDrugBill1.SelectedItems[ 0 ].Tag as DrugBillClass );
                if( this.drugBillClassInfo.ID != null )
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg( "��������Ԥ����Ϣ..." ));
                    Application.DoEvents( );
                    //���ݰ�ҩ�������ʼ��������
                    this.ShowListByDrugBill( this.drugBillClassInfo.ID );
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                }
            }
            else
            {
                //�������ð�ҩ������
                this.ResetDrugBillAttribute( );
                this.drugBillClassInfo = new DrugBillClass( );
            }
        }

        /// <summary>
        /// tabpage�л��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage2);
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
            this.toolBarService.AddToolButton( "����" , "���Ӱ�ҩ��" , FS.FrameWork.WinForms.Classes.EnumImageList.T��� , true , false , null );
            this.toolBarService.AddToolButton( "�༭" , "�༭��ҩ��" , FS.FrameWork.WinForms.Classes.EnumImageList.X�޸� , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ����ҩ��" , FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ�� , true , false , null );
            //this.toolBarService.AddToolButton( "����" , "��������" , 3 , true , false , null );
            this.toolBarService.AddToolButton( "��������" , "��������" , FS.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ� , true , false , null );
            //this.toolBarService.AddToolButton( "�˳�" , "�˳���ǰ����" , 5 , true , false , null );
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
            
            this.SaveDrugBillList( false );
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
                    this.AddDrugBill( );
                    break;
                case "�༭":
                    this.ModifyDrugBill( );
                    break;
                case "ɾ��":
                    this.DeleteDrugBill( );
                    break;
                case "��������":
                    this.SaveDrugBillList( true );
                    break;
            }

        }
        #endregion


    }
}
