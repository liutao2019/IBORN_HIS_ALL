using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PManager = FS.HISFC.BizLogic.Pharmacy.Preparation;
using PObject = FS.HISFC.Models.Preparation;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �ɱ��ۼ��㹫ʽ]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-04]<br></br>
    /// </summary>
    public partial class ucCostPriceFormula : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCostPriceFormula ( )
        {
            InitializeComponent ( );
        }

        #region ����
        /// <summary>
        /// �Ƽ�����ҩƷ�б���
        /// </summary>
        private tvDrugList tvList = null;
  
        /// <summary>
        /// ҩƷ��Ŀ������
        /// </summary>
        HISFC.BizLogic.Pharmacy.Item itemMr = new FS.HISFC.BizLogic.Pharmacy.Item ( );
        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation ( );
        /// <summary>
        /// ҩƷ��ϣ������Ϊkey��
        /// </summary>
        private Dictionary<string , string> drugNameDict = new Dictionary<string , string> ( );
        /// <summary>
        /// ҩƷ��ϣ������Ϊkey��
        /// </summary>
        private Dictionary<string , string> drugCodeDict = new Dictionary<string , string> ( );
        #endregion
        

        #region ����
        /// <summary>
        /// ���б����
        /// </summary>
        /// <returns></returns>
        protected void ShowList ( )
        {
            this.tvList.ShowDrugList ( );

        }
       
        /// <summary>
        /// ���ƴ�����Ϣ ����ʾ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ShowPrescription ( string drugCode )
        {

            this.neuSpread1_Sheet1.RowCount = 0;
            drugCodeDict.Clear ( );
            drugNameDict.Clear ( );
            
            List<FS.HISFC.Models.Preparation.PrescriptionBase> al = this.preparationManager.QueryPrescription ( drugCode,FS.HISFC.Models.Base.EnumItemType.Drug, FS.HISFC.Models.Preparation.EnumMaterialType.Material );
            if ( al == null )
            {
                MessageBox.Show ( Language.Msg ( "��ȡ��ǰѡ���Ʒ�����ƴ�����Ϣ����\n" + drugCode ) );
                return -1;
            }
            foreach (FS.HISFC.Models.Preparation.PrescriptionBase info in al)
            {
                int i = this.neuSpread1_Sheet1.Rows.Count;

                this.neuSpread1_Sheet1.Rows.Add ( i , 1 );
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColMaterialID ].Text = info.Material.ID;
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColMaterialName ].Text = info.Material.Name;
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColMaterialSpecs ].Text = info.Specs;
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColPrice ].Text = info.Price.ToString ( );
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColQty ].Text = info.NormativeQty.ToString ( );
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColMemo ].Text = info.Memo;
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColUnit ].Text = info.NormativeUnit;
                this.neuSpread1_Sheet1.Cells [ i , ( int ) MaterialColumnSet.ColPackQty ].Text = info.MaterialPackQty.ToString ( );

                this.neuSpread1_Sheet1.Rows [ i ].Tag = info.Material;
                drugCodeDict.Add ( info.Material.ID , info.Material.Name + "(" + info.Specs + ")" );
                drugNameDict.Add ( info.Material.Name + "(" + info.Specs + ")" , info.Material.ID );
            }

            string costPriceFormula = preparationManager.GetCostPriceFormula ( drugCode );

            this.neuTextBox1.Text = this.CodeToName ( costPriceFormula );
            return 1;
        }
        /// <summary>
        /// ����ת���ɱ���
        /// </summary>
        /// <param name="costPriceFormula"></param>
        /// <returns></returns>
        private string NameToCode ( string costPriceFormula )
        {
            int i , j;

            string oldValue , newValue;


            for ( i = costPriceFormula.Length - 1; i >= 0; i-- )
            {

                if ( costPriceFormula [ i ] == '[' )
                {
                    j = costPriceFormula.IndexOf ( ']' , i );
                    oldValue = costPriceFormula.Substring ( i + 1 , j - i - 1 );
                    try
                    {
                        newValue = drugNameDict [ oldValue ];
                    }
                    catch ( Exception e )
                    {
                        MessageBox.Show ( e.Message );
                        return null;
                    }
                    costPriceFormula = costPriceFormula.Replace ( oldValue , newValue );
                }

            }
            return costPriceFormula;
        }
        /// <summary>
        /// ����ת��������
        /// </summary>
        /// <param name="costPriceFormula"></param>
        /// <returns></returns>
        private string CodeToName ( string costPriceFormula )
        {
            int i , j;

            string oldValue , newValue;


            for ( i = costPriceFormula.Length - 1; i >= 0; i-- )
            {


                if ( costPriceFormula [ i ] == '[' )
                {
                    j = costPriceFormula.IndexOf ( ']' , i );
                    oldValue = costPriceFormula.Substring ( i + 1 , j - i - 1 );
                    newValue = drugCodeDict [ oldValue ];

                    costPriceFormula = costPriceFormula.Replace ( oldValue , newValue );
                }

            }
            return costPriceFormula;
        }
        /// <summary>
        /// ���ɱ����㹫ʽ�Ƿ������һ��ֵ
        /// </summary>
        /// <param name="costPriceFormula"></param>
        /// <returns></returns>
        private string Check ( string costPriceFormula )
        {
            int i , j;

            string oldValue;
            string newValue = "1";
            


            for ( i = costPriceFormula.Length - 1; i >= 0; i-- )
            {


                if ( costPriceFormula [ i ] == '[' )
                {
                    j = costPriceFormula.IndexOf ( ']' , i );
                    oldValue = costPriceFormula.Substring ( i , j - i + 1 );


                    costPriceFormula = costPriceFormula.Replace ( oldValue , newValue );
                }
            }
            return costPriceFormula;
        }
        #endregion

        #region �¼�
        /// <summary>
        /// load�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnLoad ( EventArgs e )
        {
            this.tvList = this.tv as tvDrugList;
            this.ShowList ( );
            base.OnLoad ( e );
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int  OnSave(object sender, object neuObject)
        {
            FS.FrameWork.Models.NeuObject info = this.tvList.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
                FS.HISFC.Models.Preparation.CostPrice costPrice = new FS.HISFC.Models.Preparation.CostPrice ( );
                HISFC.Models.Pharmacy.Item item = itemMr.GetItem ( info.ID );
                //�ж�item�Ƿ�Ϊ��
                if ( item.ID == null || item == null )
                {
                }
                else
                {
                    costPrice.ID = item.ID;
                    costPrice.Name = item.Name;
                    costPrice.Specs = item.Specs;
                    costPrice.PactUnit = item.PackUnit;
                    costPrice.PactQty = ( int ) item.PackQty;
                    costPrice.MinUnit = item.MinUnit;
                    string costPriceFormula = this.neuTextBox1.Text;
                    costPrice.CostPriceFormula = this.NameToCode ( costPriceFormula );
                    //�жϱ��ʽ�ܷ����һ��ֵ������������ֵ֤���Ϸ���
                    object i = FS.FrameWork.Public.String.ExpressionVal ( ( this.Check ( costPriceFormula ) ) );




                    costPrice.SalePriceFormula = "";
                    costPrice.PriceSource = "";
                    costPrice.PriceRate = item.PriceCollection.PriceRate;
                    costPrice.Memo = item.Memo;
                    costPrice.Extend = "";
                    costPrice.Oper.Name = ( ( FS.HISFC.Models.Base.Employee ) FS.FrameWork.Management.Connection.Operator ).ID;
                    costPrice.Oper.OperTime = DateTime.Now;
                
                if ( preparationManager.IsHaveCostPriceFormula ( costPrice.ID ) )
                {
                    if ( i == null )
                    {
                        MessageBox.Show ( "�޸�ʧ��" );
                    }
                    else
                    {
                        if ( costPrice.CostPriceFormula == null )
                        {
                            MessageBox.Show ( "�޸�ʧ��" );
                        }
                        else
                        {
                            preparationManager.UpdateCostPrice ( costPrice );
                            MessageBox.Show ( "�޸ĳɹ�" );
                        }
                    }

                }
                else
                {
                    if ( preparationManager.InsertCostPrice ( costPrice ) == 1 )
                    {
                        if ( i == null )
                        {
                            MessageBox.Show ( "����ʧ��" );
                        }
                        else
                        {
                            if ( costPrice.CostPriceFormula == null )
                            {
                                MessageBox.Show ( "����ʧ��" );
                            }
                            else
                            {
                                MessageBox.Show ( "����ɹ�" );
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show ( preparationManager.Err );
                    }

                }
            }
            return base.OnSave ( sender , neuObject );
        }

       

       /// <summary>
       /// ���������ͣ������������
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void neuTextBox1_MouseUp ( object sender , MouseEventArgs e )
        {
            string costPriceFormula = this.neuTextBox1.Text;

            int start = this.neuTextBox1.SelectionStart;
            int i , j;

            for ( i = costPriceFormula.Length - 1; i >= 0; i-- )
            {


                if ( costPriceFormula [ i ] == '[' )
                {
                    j = costPriceFormula.IndexOf ( ']' , i );
                    if ( start > i && start <= j )
                    {
                        this.neuTextBox1.SelectionStart += ( j - start+1 );
                    }
                }

            }
        }
        /// <summary>
        /// ��������������ﲻ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_KeyPress ( object sender , KeyPressEventArgs e )
        {
            string costPriceFormula = this.neuTextBox1.Text;

            int start = this.neuTextBox1.SelectionStart;
            int i , j;

            for ( i = costPriceFormula.Length - 1; i >= 0; i-- )
            {


                if ( costPriceFormula [ i ] == '[' )
                {
                    j = costPriceFormula.IndexOf ( ']' , i );
                    if ( start > i && start <= j )
                    {
                        e.Handled = true;
                        this.neuTextBox1.SelectionStart += ( j - start + 1 );
                        return;
                    }
                }

            }
        }

        protected override int OnSetValue ( object neuObject , TreeNode e )
        {
            if ( e.Tag != null )
            {
                FS.FrameWork.Models.NeuObject info = e.Tag as FS.FrameWork.Models.NeuObject;
                this.ShowPrescription ( info.ID );
            }
            return base.OnSetValue ( neuObject , e );
        }
      
        #endregion

        protected enum MaterialColumnSet
        {
            /// <summary>
            /// ԭ�ϱ���
            /// </summary>
            ColMaterialID ,
            /// <summary>
            /// ����
            /// </summary>
            ColMaterialName ,
            /// <summary>
            /// ���
            /// </summary>
            ColMaterialSpecs ,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty ,
            /// <summary>
            /// �۸�
            /// </summary>
            ColPrice ,
            /// <summary>
            /// ������
            /// </summary>
            ColQty ,
            /// <summary>
            /// ��λ
            /// </summary>
            ColUnit ,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo
        }

        private void neuSpread1_CellDoubleClick ( object sender , FarPoint.Win.Spread.CellClickEventArgs e )
        {
            int activeRow = this.neuSpread1_Sheet1.ActiveRowIndex;
            string drugInfo="["+this.neuSpread1_Sheet1.Cells[activeRow,(int)MaterialColumnSet.ColMaterialName].Text+"("+this.neuSpread1_Sheet1.Cells[activeRow,(int)MaterialColumnSet.ColMaterialSpecs].Text+")]";
            this.neuTextBox1.Text = this.neuTextBox1.Text.Insert ( this.neuTextBox1.SelectionStart , drugInfo );
        }





    }

    
}
