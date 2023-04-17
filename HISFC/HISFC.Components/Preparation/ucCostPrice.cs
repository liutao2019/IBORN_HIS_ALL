using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PManager = FS.HISFC.BizLogic.Pharmacy.Preparation;
using PObject = FS.HISFC.Models.Preparation;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �ɱ��ۼ���]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <˵��>
    ///  1 ��ʽ�����洢ԭ�ϱ��� 
    ///  2 ��ʽ���㷽ʽ�ľ���ϸ��ʵ��
    ///        ���㷽ʽ�����ֹ������������ʽ����
    /// </˵��>
    /// </summary>
    public partial class ucCostPrice : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCostPrice ( )
        {
            InitializeComponent ( );
        }
        /// <summary>
        /// ҩƷ����Ϊkey���۸�Ϊvalue
        /// </summary>
        private Dictionary<string , string> drugNameDict = new Dictionary<string , string> ( );
        /// <summary>
        /// ҩƷ����Ϊkey������Ϊvalue
        /// </summary>
        private Dictionary<string , string> drugCodeDict = new Dictionary<string , string> ( );
        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation ( );
        /// <summary>
        /// �ɱ����޸����
        /// </summary>
        public event System.EventHandler CostPriceChanged;

        #region ��̬��������

        /// <summary>
        /// �ɱ��ۼ���
        /// </summary>
        /// <param name="preparation">�Ƽ���Ʒ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal static int ComputeCostPrice ( PManager pManager , ref PObject.Preparation preparation , ComputeCostPriceType computeType )
        {

            if ( computeType == ComputeCostPriceType.Manual )
            {
                using ( ucCostPrice uc = new ucCostPrice ( ) )
                {
                    uc.SetPreparation ( preparation,pManager,ref preparation );
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�ɱ�������";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl ( uc );
                    if ( uc.Result == DialogResult.Cancel )
                    {
                        return 1;
                    }
                    else
                    {
                        preparation.CostPrice = uc.CostPrice;
                        return 1;
                    }


                }
            }
            else
            {

                List<FS.HISFC.Models.Preparation.Expand> expandList = ucExpand.QueryExpandList ( pManager , preparation );
                if ( expandList == null )
                {
                    return -1;
                }

                decimal costPrice = 0;
                
                foreach ( FS.HISFC.Models.Preparation.Expand info in expandList )
                {
                    costPrice = info.FacutalExpand / info.Prescription.MaterialPackQty * info.Prescription.Price;
                    
                }
                if ( costPrice == 0 )
                {
                    preparation.CostPrice = preparation.Drug.PriceCollection.PurchasePrice;
                }
                else
                {
                    preparation.CostPrice = costPrice;
                }
            }

            return 1;
        }

        #endregion

        /// <summary>
        /// �������
        /// </summary>
        private DialogResult result = DialogResult.Cancel;

        protected string strPreparation = "�Ƽ���Ʒ��{0}  ���{1}  ���ţ�{2}  �ƻ�����{3}  ��λ��{4}";

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        internal DialogResult Result
        {
            get
            {
                return result;
            }
        }

        /// <summary>
        /// ���μ���ɱ�ֵ
        /// </summary>
        internal decimal CostPrice
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDecimal ( this.ntxtCostPrice.Text );
            }
        }

        #endregion
        private Dictionary<string , string> ComputeCostPrice ( PManager pManager , ref PObject.Preparation preparation )
        {
            List<FS.HISFC.Models.Preparation.Expand> expandList = ucExpand.QueryExpandList ( pManager , preparation );
            if ( expandList == null )
            {
                return null;
            }

            decimal costPrice = 0;

            foreach ( FS.HISFC.Models.Preparation.Expand info in expandList )
            {
                costPrice = info.FacutalExpand / info.Prescription.MaterialPackQty * info.Prescription.Price;
                this.drugNameDict.Add ( "[" + info.Prescription.Material.ID + "]" , costPrice.ToString() );

            }
            if ( costPrice == 0 )
            {
                preparation.CostPrice = preparation.Drug.PriceCollection.PurchasePrice;
            }
            else
            {
                preparation.CostPrice = costPrice;
            }
            return drugNameDict;
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
        /// ����ת���ɼ۸�
        /// </summary>
        /// <param name="costPriceFormula"></param>
        /// <returns></returns>
        private string CodeToPrice ( string costPriceFormula,Dictionary<string,string> drugNameDict )
        {
            int i , j;

            string oldValue , newValue;
            
         
            for ( i = costPriceFormula.Length - 1; i >= 0; i-- )
            {


                if ( costPriceFormula [ i ] == '[' )
                {
                    j = costPriceFormula.IndexOf ( ']' , i );
                    oldValue = costPriceFormula.Substring ( i  , j - i + 1 );
                    newValue = drugNameDict[ oldValue ];

                    costPriceFormula = costPriceFormula.Replace ( oldValue , newValue );
                }

            }
            return costPriceFormula;
        }
        /// <summary>
        /// �����Ƽ���Ʒ��Ϣ �� ��ʾ�ɱ����㹫ʽ����ǰ�ɱ���
        /// </summary>
        /// <param name="preparation">�Ƽ���Ʒ��Ϣ</param>
        protected int SetPreparation ( FS.HISFC.Models.Preparation.Preparation preparation , PManager pManager , ref PObject.Preparation preparation1 )
        {
            
            string drugCode = preparation.Drug.ID;
            List<FS.HISFC.Models.Preparation.Prescription> al = this.preparationManager.QueryDrugPrescription( drugCode);
            if ( al == null )
            {

                return -1;
            }
            foreach ( FS.HISFC.Models.Preparation.Prescription info in al )
            {

                drugCodeDict.Add ( info.Material.ID , info.Material.Name );
                //drugNameDict.Add ( info.Material.Name, info.Material.ID );
            }

            string costPriceFormula = this.preparationManager.GetCostPriceFormula ( drugCode );
            this.ntxtCostPrice.Text = FS.FrameWork.Public.String.ExpressionVal ( this.CodeToPrice ( costPriceFormula,this.ComputeCostPrice(pManager, ref preparation1 ) )).ToString();
            costPriceFormula = this.CodeToName ( costPriceFormula );
            this.costPriceTxt.Text = costPriceFormula;
            this.lbPreparation.Text = string.Format ( this.strPreparation , preparation.Drug.Name , preparation.Drug.Specs , preparation.BatchNO , preparation.PlanQty , preparation.Unit );

            return this.ucExpand1.ShowExpand ( preparation );

        }

        /// <summary>
        /// �ر�
        /// </summary>
        protected void Close ( )
        {
            if ( this.ParentForm != null )
            {
                this.ParentForm.Close ( );
            }
        }

        private void btnOK_Click ( object sender , System.EventArgs e )
        {
            this.result = DialogResult.OK;

            if ( this.CostPriceChanged != null )
            {
                this.CostPriceChanged ( null , System.EventArgs.Empty );
            }

            this.Close ( );
        }

        private void btnCancel_Click ( object sender , System.EventArgs e )
        {
            this.result = DialogResult.Cancel;

            this.Close ( );
        }

    }

    internal enum ComputeCostPriceType
    {
        /// <summary>
        /// �Զ����� �޵���������Ϣ ���ݹ�ʽ����۸�
        /// </summary>
        Auto ,
        /// <summary>
        /// �ֶ����� ����������Ϣ ���ֹ�����
        /// </summary>
        Manual
    }
}
