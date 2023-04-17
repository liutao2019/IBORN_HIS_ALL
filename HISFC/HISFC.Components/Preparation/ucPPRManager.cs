using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ���ʵ��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///    1 ͨ����������ʵ��,ȡ��������˼·
    ///    2 ���ݴ�ӡ��ͨ���ӿ���ʵ�֣���ͨ���������̵��Զ���������ʵ�ִ�ӡ
    /// 
    ///    2 ��ǰ��д����: ��һ�żƻ�����������ҩƷ������£���δ����Զ���������������
    ///                    ��������ͼ¼�롢����
    ///    6 �ɱ���(�����)����
    ///         --���ȡ���ļ۸�Ϊ0 ����ݹ�ʽ������Ĭ�ϼ���
    ///         --����۸�Ϊ0 �����д���
    ///         --���ֹ��޸ģ���ʾ���ġ���ʽ��Ϣ���е���
    ///         ��ʽ���ִ��������
    ///    8 �����������������������ݵ�ͬ������
    /// </˵��>
    /// </summary>
    public partial class ucPPRManager : FS.FrameWork.WinForms.Controls.ucBaseControl , FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPPRManager ( )
        {
            InitializeComponent ( );
        }

        #region �����

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation ( );

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharamcyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy ( );

        /// <summary>
        /// �Ƽ��ƻ�״̬
        /// </summary>
        private FS.HISFC.Models.Preparation.EnumState listState = FS.HISFC.Models.Preparation.EnumState.Plan;

        /// <summary>
        /// ������ʾ�Ƿ�ʹ�ð�װ��λ
        /// </summary>
        private bool isUsePackUnit = true;

        /// <summary>
        /// ��һ״̬
        /// </summary>
        private FS.HISFC.Models.Preparation.EnumState saveState = FS.HISFC.Models.Preparation.EnumState.Confect;

        /// <summary>
        /// ������д������һ���ǰ�Ƿ������޸�
        /// </summary>
        private bool isCanReSet = false;

        /// <summary>
        /// �Ƿ����������Ա��Ϣ
        /// </summary>
        private bool isUpdateConfectOper = false;

        /// <summary>
        /// �Ƿ���¼�����Ա��Ϣ
        /// </summary>
        private bool isUpdateAssayOper = false;

        /// <summary>
        /// �Ƿ���������Ա��Ϣ
        /// </summary>
        private bool isUpdateInputOper = false;

        /// <summary>
        /// �Ƿ�۳�ԭ��
        /// </summary>
        private bool isExpandMaterial = false;

        /// <summary>
        /// �Ƿ���г�Ʒ���
        /// </summary>
        private bool isInputDrug = false;

        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        private System.Collections.ArrayList alPrescription = null;

        /// <summary>
        /// ����������
        /// </summary>
        private FS.FrameWork.Models.NeuObject stockDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        private string noticeStr = "";
        #endregion

        #region �ӿ�

        /// <summary>
        /// �����������̽ӿ�
        /// </summary>
        HISFC.Components.Preparation.IProcess processInterface = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƽ�״̬
        /// </summary>
        [Category ( "����" ) , Description ( "�����Ƽ�����״̬" )]
        public FS.HISFC.Models.Preparation.EnumState ListState
        {
            get
            {
                return this.listState;
            }
            set
            {
                this.listState = value;
            }
        }

        /// <summary>
        /// �Ƽ���һ����״̬
        /// </summary>
        [Category ( "����" ) , Description ( "�����Ƽ���һ����״̬" )]
        public FS.HISFC.Models.Preparation.EnumState SaveState
        {
            get
            {
                return this.saveState;
            }
            set
            {
                this.saveState = value;

                switch ( value )
                {
                    case FS.HISFC.Models.Preparation.EnumState.Plan:       //�ƻ�
                    case FS.HISFC.Models.Preparation.EnumState.Input:      //���
                    this.isUsePackUnit = true;
                    break;
                    default:
                    this.isUsePackUnit = false;
                    break;
                }

                this.SetFormat ( );

                this.SetExpand ( );
            }
        }

        /// <summary>
        /// ������д������һ���ǰ�Ƿ������޸�
        /// </summary>
        [Category ( "����" ) , Description ( "������д������һ���ǰ�Ƿ������޸�" )]
        public bool IsCanReSet
        {
            get
            {
                return this.isCanReSet;
            }
            set
            {
                this.isCanReSet = value;
            }
        }

        /// <summary>
        /// �Ƿ����������Ա��Ϣ
        /// </summary>
        [Category ( "����" ) , Description ( "�Ƿ����������Ա��Ϣ" )]
        public bool IsUpdateConfectOper
        {
            get
            {
                return this.isUpdateConfectOper;
            }
            set
            {
                this.isUpdateConfectOper = value;
            }
        }

        /// <summary>
        /// �Ƿ���¼�����Ա��Ϣ
        /// </summary>
        [Category ( "����" ) , Description ( "�Ƿ���¼�����Ա��Ϣ" )]
        public bool IsUpdateAssayOper
        {
            get
            {
                return this.isUpdateAssayOper;
            }
            set
            {
                this.isUpdateAssayOper = value;
            }
        }

        /// <summary>
        /// �Ƿ���������Ա��Ϣ
        /// </summary>
        [Category ( "����" ) , Description ( "�Ƿ���������Ա��Ϣ" )]
        public bool IsUpdateInputOper
        {
            get
            {
                return this.isUpdateInputOper;
            }
            set
            {
                this.isUpdateInputOper = value;
            }
        }

        /// <summary>
        /// �Ƿ�۳�ԭ��
        /// </summary>
        [Category ( "����" ) , Description ( "�����Ƿ�۳�ԭ��" )]
        public bool IsExpandMaterial
        {
            get
            {
                return this.isExpandMaterial;
            }
            set
            {
                this.isExpandMaterial = value;
            }
        }

        /// <summary>
        /// �����Ƿ���г�Ʒ���
        /// </summary>
        [Category ( "����" ) , Description ( "�����Ƿ���г�Ʒ���" )]
        public bool IsInputDrug
        {
            get
            {
                return this.isInputDrug;
            }
            set
            {
                this.isInputDrug = value;
            }
        }

        /// <summary>
        /// ������ʾ��Ϣ��ʾ
        /// </summary>
        [Category ( "����" ) , Description ( "������ʾ��Ϣ��ʾ" )]
        public string NoticeStr
        {
            get
            {
                return this.noticeStr;
            }
            set
            {
                if ( value == "" )
                {
                    this.gbNotice.Visible = false;
                }
                else
                {
                    this.gbNotice.Visible = true;
                    this.noticeStr = value;
                    this.lbNotice.Text = value;
                }
            }
        }
        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService ( );

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit ( object sender , object neuObject , object param )
        {
            this.toolBarService.AddToolButton ( "����" , "�����Ƽ��ƻ���ϸ" , FS.FrameWork.WinForms.Classes.EnumImageList.T��� , true , false , null );
            this.toolBarService.AddToolButton ( "�½�" , "�½��Ƽ��ƻ���" , FS.FrameWork.WinForms.Classes.EnumImageList.X�½� , true , false , null );
            this.toolBarService.AddToolButton ( "��������" , "��������¼��" , FS.FrameWork.WinForms.Classes.EnumImageList.H���� , true , false , null );
            this.toolBarService.AddToolButton ( "ɾ��" , "ɾ��δ��ʼ���õļƻ�����ϸ" , FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ�� , true , false , null );
            this.toolBarService.AddToolButton ( "�ɱ�����" , "����ѡ����Ŀ�ĳɱ���" , FS.FrameWork.WinForms.Classes.EnumImageList.S���� , true , false , null );

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked ( object sender , ToolStripItemClickedEventArgs e )
        {
            if ( e.ClickedItem.Text == "����" )
            {
                this.AddNewPreparationDetail ( );
            }
            if ( e.ClickedItem.Text == "�½�" )
            {
                this.NewPreparation ( );
            }
            if ( e.ClickedItem.Text == "��������" )
            {
                this.SetProcess ( );
            }
            if ( e.ClickedItem.Text == "ɾ��" )
            {
                this.DelPreparationPlanDetail ( );
            }
            if ( e.ClickedItem.Text == "�ɱ�����" )
            {
                FS.HISFC.Models.Preparation.Preparation ppr = this.GetDrugFromFp ( this.fsDrug_Sheet1.ActiveRowIndex );

                this.ComputeCostPrice ( ref ppr , ComputeCostPriceType.Manual );

                this.fsDrug_Sheet1.Cells [ this.fsDrug_Sheet1.ActiveRowIndex , ( int ) DrugColumnSet.ColCostPrice ].Text = ppr.CostPrice.ToString ( );
            }
            base.ToolStrip_ItemClicked ( sender , e );
        }

        protected override int OnSave ( object sender , object neuObject )
        {
            switch (this.saveState)
            {
                case FS.HISFC.Models.Preparation.EnumState.Plan:
                    if (this.SavePreparationPlan() == -1)
                    {
                        return -1;
                    }
                    break;
                default:
                    if (this.SavePreparation() == -1)
                    {
                        return -1;
                    }
                    break;
            }

            this.ShowList ( );

            return 1;
        }

        #endregion

        #region �ƻ��б�������

        /// <summary>
        /// ���Ҽƻ��б���
        /// </summary>
        private tvPlanList tvList = null;

        /// <summary>
        /// ���б����
        /// </summary>
        /// <returns></returns>
        protected int ShowList ( )
        {
            if ( this.tvList != null )
            {
                if ( this.isCanReSet )
                {
                    this.tvList.ShowPlanList ( this.listState , this.saveState );
                }
                else
                {
                    this.tvList.ShowPlanList ( this.listState );
                }
            }

            return 1;
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        protected int Init ( )
        {
            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType ( );
            this.fsDrug_Sheet1.Columns [ ( int ) DrugColumnSet.ColCostPrice ].CellType = markNumCellType;
            this.fsDrug_Sheet1.Columns [ ( int ) DrugColumnSet.ColInQty ].CellType = markNumCellType;
            this.fsDrug_Sheet1.Columns [ ( int ) DrugColumnSet.ColPlanNum ].CellType = markNumCellType;
            this.fsDrug_Sheet1.Columns [ ( int ) DrugColumnSet.ColConfectQty ].CellType = markNumCellType;

            FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType markDateCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType ( );
            this.fsDrug_Sheet1.Columns [ ( int ) DrugColumnSet.ColValidDate ].CellType = markDateCellType;

            this.InitPreparationDrug ( );

            return 1;
        }

        /// <summary>
        /// ��ʼ���Ƽ���Ʒ�б�
        /// </summary>
        /// <returns></returns>
        protected int InitPreparationDrug ( )
        {
            List<FS.FrameWork.Models.NeuObject> prescriptionList = this.preparationManager.QueryPrescriptionList ( FS.HISFC.Models.Base.EnumItemType.Drug);
            if ( prescriptionList == null )
            {
                MessageBox.Show ( Language.Msg ( "�����Ƽ������б���Ϣ��������" ) + this.preparationManager.Err );
                return -1;
            }

            this.alPrescription = new System.Collections.ArrayList ( prescriptionList.ToArray ( ) );

            return 1;
        }

        #endregion

        #region ���ݼ��� ������ʾ

        /// <summary>
        /// ���������ƻ�����ʾ�����ƻ���Ϣ
        /// </summary>
        /// <param name="planNO">�����ƻ�����</param>
        internal void ShowPreparation ( string planNO )
        {
            List<FS.HISFC.Models.Preparation.Preparation> preparationList = null;
            if ( this.isCanReSet )        //����һ״̬���ǰ�����޸ġ�
            {
                preparationList = preparationManager.QueryPreparation ( planNO , this.listState , this.saveState );
            }
            else
            {
                preparationList = preparationManager.QueryPreparation ( planNO , this.listState );
            }
            if ( preparationList == null )
            {
                MessageBox.Show ( Language.Msg ( "���������ƻ���ˮ�ż�����״̬��ȡ�Ƽ�������Ϣ��������" ) + preparationManager.Err );
                return;
            }

            this.Clear ( true );

            foreach ( FS.HISFC.Models.Preparation.Preparation info in preparationList )
            {
                FS.HISFC.Models.Preparation.Preparation facualInfo = info.Clone ( );
                this.ComputeCostPrice ( ref facualInfo , ComputeCostPriceType.Auto );

                this.AddDrugToFp ( facualInfo );
            }

            if ( this.fsDrug_Sheet1.ActiveRowIndex >= 0 )
            {
                this.ShowExpandPrescription ( this.fsDrug_Sheet1.ActiveRowIndex );
            }
        }

        /// <summary>
        /// �Ƽ�ԭ��������Ϣ��ʾ
        /// </summary>
        /// <param name="info">�Ƽ���Ʒ�ƻ���Ϣ</param>
        internal protected void ShowExpandPrescription ( int rowIndex )
        {
            FS.HISFC.Models.Preparation.Preparation info = this.GetDrugFromFp ( rowIndex );
            if ( info != null )
            {
                this.ShowExpandPrescription ( info );
            }
        }

        /// <summary>
        /// �Ƽ�ԭ��������Ϣ��ʾ
        /// </summary>
        /// <param name="info">�Ƽ���Ʒ�ƻ���Ϣ</param>
        internal protected void ShowExpandPrescription ( FS.HISFC.Models.Preparation.Preparation info )
        {
            this.ucExpand1.Clear ( );

            this.ucExpand1.PlanNO = info.PlanNO;
            this.ucExpand1.ShowExpand ( info );
        }

        #endregion

        #region Fp�����ݲ��� ��ֵ/��ȡ��Ϣ

        /// <summary>
        /// ����Ƽ���Ʒ�ƻ���Ϣ
        /// </summary>
        /// <param name="info">�Ƽ���Ʒ�ƻ���Ϣ</param>
        protected void AddDrugToFp ( FS.HISFC.Models.Preparation.Preparation info )
        {
            int rowCoount = this.fsDrug_Sheet1.Rows.Count;
            this.fsDrug_Sheet1.Rows.Add ( rowCoount , 1 );

            this.AddDrugToFp ( info , rowCoount , true );
        }

        /// <summary>
        /// ����Ƽ���Ʒ�ƻ���Ϣ
        /// </summary>
        /// <param name="info">�Ƽ���Ʒ�ƻ���Ϣ</param>
        protected void AddDrugToFp ( FS.HISFC.Models.Preparation.Preparation info , int rowIndex , bool refreshDrug )
        {
            try
            {
                if ( refreshDrug )
                {
                    FS.HISFC.Models.Pharmacy.Item item = this.pharamcyIntegrate.GetItem ( info.Drug.ID );
                    info.Drug = item;

                    if ( info.CostPrice == 0 )
                    {
                        info.CostPrice = item.PriceCollection.PurchasePrice;
                    }
                }

                this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColDrugName ].Text = info.Drug.Name;
                this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColSpecs ].Text = info.Drug.Specs;
                this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColPackQty ].Text = info.Drug.PackQty.ToString ( );
                this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColPackUnit ].Text = info.Drug.PackUnit;
                this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColMemo ].Text = info.Memo;

                this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColAssayResult ].Value = info.IsAssayEligible;

                if ( this.isUsePackUnit )
                {
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColPlanNum ].Text = ( info.PlanQty / info.Drug.PackQty ).ToString ( );
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColPlanUnit ].Text = info.Drug.PackUnit;
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColConfectQty ].Text = ( info.ConfectQty / info.Drug.PackQty ).ToString ( );
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColInQty ].Text = ( info.InputQty / info.Drug.PackQty ).ToString ( );
                }
                else
                {
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColPlanNum ].Text = info.PlanQty.ToString ( );
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColPlanUnit ].Text = info.Unit;
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColConfectQty ].Text = info.ConfectQty.ToString ( );
                    this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColInQty ].Text = info.InputQty.ToString ( );
                }

                this.fsDrug_Sheet1.Cells [ rowIndex , ( int ) DrugColumnSet.ColCostPrice ].Text = info.CostPrice.ToString ( );

                this.fsDrug_Sheet1.Cells[rowIndex, (int)DrugColumnSet.ColAssayQty].Text = info.AssayQty.ToString();

                this.fsDrug_Sheet1.Rows [ rowIndex ].Tag = info;
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( ex.Message );
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="inde"></param>
        /// <returns></returns>
        protected FS.HISFC.Models.Preparation.Preparation GetDrugFromFp ( int index )
        {
            FS.HISFC.Models.Preparation.Preparation info = this.fsDrug_Sheet1.Rows [ index ].Tag as FS.HISFC.Models.Preparation.Preparation;
            if ( info == null )
            {
                return null;
            }
            if ( this.isUsePackUnit )         //����С��λ����
            {
                info.PlanQty = FS.FrameWork.Function.NConvert.ToDecimal ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColPlanNum ].Text ) * info.Drug.PackQty;
                info.InputQty = FS.FrameWork.Function.NConvert.ToDecimal ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColInQty ].Text ) * info.Drug.PackQty;
                info.ConfectQty = FS.FrameWork.Function.NConvert.ToDecimal ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColConfectQty ].Text ) * info.Drug.PackQty;
            }
            else
            {
                info.PlanQty = FS.FrameWork.Function.NConvert.ToDecimal ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColPlanNum ].Text );
                info.InputQty = FS.FrameWork.Function.NConvert.ToDecimal ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColInQty ].Text );
                info.ConfectQty = FS.FrameWork.Function.NConvert.ToDecimal ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColConfectQty ].Text );
            }

            //{74EC6D6F-CD5F-446c-BB07-A23BE80F1885}  ��ȡ�����Ƿ�ϸ��Ǹ�ֵ����
            info.IsAssayEligible = FS.FrameWork.Function.NConvert.ToBoolean ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColAssayResult ].Value);
            info.BatchNO = this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColBatchNO ].Text;
            info.ValidDate = FS.FrameWork.Function.NConvert.ToDateTime ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColValidDate ].Text );
            info.AssayQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fsDrug_Sheet1.Cells[index, (int)DrugColumnSet.ColAssayQty].Text);

            //info.Unit = this.fsDrug_Sheet1.Cells[index, (int)DrugColumnSet.ColPlanUnit].Text;
            info.Unit = info.Drug.MinUnit;
            info.CostPrice = NConvert.ToDecimal ( this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColCostPrice ].Text );
            info.Memo = this.fsDrug_Sheet1.Cells [ index , ( int ) DrugColumnSet.ColMemo ].Text;

            return info;
        }

        /// <summary>
        /// ˢ��Fp��ʾ����
        /// </summary>
        /// <param name="info"></param>
        public void RefreshFpData ( FS.HISFC.Models.Preparation.Preparation info )
        {
            this.AddDrugToFp ( info , this.fsDrug_Sheet1.ActiveRowIndex , false );
        }

        #endregion

        #region  �ɼ̳н�������ʵ�� ���ý��桢Fp��Expand��Ϣ��ʾ������У�顢�������̼�¼

        /// <summary>
        /// ��ʽ��
        /// </summary>
        protected virtual void SetFormat ( )
        {
            switch (this.SaveState)
            {
                case FS.HISFC.Models.Preparation.EnumState.Plan:

                    #region Fp��ʽ��

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayResult].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColBatchNO].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColValidDate].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColConfectQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColInQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColCostPrice].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayQty].Visible = false;

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColDrugName].Width = 160;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColSpecs].Width = 90;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackQty].Width = 80;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackUnit].Width = 80;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Width = 90;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanUnit].Width = 60;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColMemo].Width = 180;

                    #endregion

                    break;
                case FS.HISFC.Models.Preparation.EnumState.Confect:
                case FS.HISFC.Models.Preparation.EnumState.Division:

                    #region Fp��ʽ��

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayResult].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColBatchNO].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColValidDate].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColConfectQty].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColInQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Locked = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColCostPrice].Visible = false;

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColDrugName].Width = 160;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColSpecs].Width = 90;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackQty].Width = 75;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackUnit].Width = 75;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Width = 90;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanUnit].Width = 60;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColMemo].Width = 180;

                    #endregion

                    break;
                case FS.HISFC.Models.Preparation.EnumState.Input:

                    #region Fp��ʽ��

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayResult].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColBatchNO].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColValidDate].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColConfectQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColInQty].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Locked = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColCostPrice].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayQty].Visible = false;

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColDrugName].Width = 120;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColSpecs].Width = 60;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackQty].Width = 70;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackUnit].Width = 70;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Width = 80;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanUnit].Width = 60;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColMemo].Width = 120;

                    #endregion

                    break;

                case FS.HISFC.Models.Preparation.EnumState.SemiAssay:
                case FS.HISFC.Models.Preparation.EnumState.PackAssay:

                    #region Fp��ʽ��

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayResult].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColBatchNO].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColValidDate].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColConfectQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColInQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Locked = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColCostPrice].Visible = false;

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColDrugName].Width = 180;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColSpecs].Width = 90;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackQty].Width = 75;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackUnit].Width = 75;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Width = 100;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanUnit].Width = 60;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColMemo].Width = 180;

                    #endregion

                    break;

                case FS.HISFC.Models.Preparation.EnumState.Package:

                    #region Fp��ʽ��

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayResult].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColBatchNO].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColValidDate].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColConfectQty].Visible = false;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColInQty].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Locked = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColCostPrice].Visible = true;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColAssayQty].Visible = false;

                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColDrugName].Width = 160;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColSpecs].Width = 80;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackQty].Width = 70;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPackUnit].Width = 70;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanNum].Width = 80;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColPlanUnit].Width = 60;
                    this.fsDrug_Sheet1.Columns[(int)DrugColumnSet.ColMemo].Width = 120;

                    #endregion

                    break;
            }
        }

        /// <summary>
        /// �Ƿ�ɱ༭������Ϣ
        /// </summary>
        protected virtual void SetExpand ( )
        {
            switch ( this.saveState )
            {
                case FS.HISFC.Models.Preparation.EnumState.Plan:
                case FS.HISFC.Models.Preparation.EnumState.Confect:
                this.ucExpand1.IsCanEdit = true;
                break;
                default:
                this.ucExpand1.IsCanEdit = false;
                break;
            }
        }

        /// <summary>
        /// �����������̼�¼
        /// </summary>
        protected virtual int SetProcess ( )
        {
            if ( this.fsDrug_Sheet1.Rows.Count <= 0 )
            {
                return 1;
            }

            FS.HISFC.Models.Preparation.Preparation info = this.GetDrugFromFp ( this.fsDrug_Sheet1.ActiveRowIndex );

            if ( this.processInterface == null )
            {
                this.processInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject ( this.GetType ( ) , typeof ( FS.HISFC.Components.Preparation.IProcess ) ) as FS.HISFC.Components.Preparation.IProcess;
            }
            if ( this.processInterface != null )
            {
                this.processInterface.SetProcess ( this.saveState , info );
            }
            else
            {
                switch ( this.saveState )
                {
                    case FS.HISFC.Models.Preparation.EnumState.Confect:

                    #region ������Ϣ¼��

                    using ( Process.ucConfectProcess uc = new FS.HISFC.Components.Preparation.Process.ucConfectProcess ( ) )
                    {
                        uc.SetPreparation ( info );

                        FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������������Ϣ¼��";
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl ( uc );

                        if ( uc.Result == DialogResult.OK )
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    #endregion

                    case FS.HISFC.Models.Preparation.EnumState.Division:

                    #region ��װ��Ϣ¼��

                    using ( Process.ucDivisionProcess uc = new FS.HISFC.Components.Preparation.Process.ucDivisionProcess ( ) )
                    {
                        uc.SetPreparation ( info );

                        FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������������Ϣ¼��";
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl ( uc );

                        if ( uc.Result == DialogResult.OK )
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    #endregion

                    case FS.HISFC.Models.Preparation.EnumState.Input:

                    #region �����Ϣ¼��

                    using ( Process.ucInputProcess uc = new FS.HISFC.Components.Preparation.Process.ucInputProcess ( ) )
                    {
                        uc.SetPreparation ( info );

                        FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������������Ϣ¼��";
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl ( uc );

                        if ( uc.Result == DialogResult.OK )
                        {
                            this.RefreshFpData ( info );

                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    #endregion

                    case FS.HISFC.Models.Preparation.EnumState.SemiAssay:

                    #region ���Ʒ������Ϣ¼��

                    using ( Process.ucSemiAssayProcess uc = new FS.HISFC.Components.Preparation.Process.ucSemiAssayProcess ( ) )
                    {
                        uc.SetPreparation ( info );

                        FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������������Ϣ¼��";
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl ( uc );

                        if ( uc.Result == DialogResult.OK )
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    #endregion

                    case FS.HISFC.Models.Preparation.EnumState.Package:

                    #region ���װ��Ϣ¼��

                    using ( Process.ucPackageProcess uc = new FS.HISFC.Components.Preparation.Process.ucPackageProcess ( ) )
                    {
                        uc.SetPreparation ( info );

                        FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������������Ϣ¼��";
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl ( uc );

                        if ( uc.Result == DialogResult.OK )
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    #endregion

                    case FS.HISFC.Models.Preparation.EnumState.PackAssay:

                    #region ��Ʒ������Ϣ¼��

                    using ( Process.frmAssayProcess uc = new FS.HISFC.Components.Preparation.Process.frmAssayProcess ( ) )
                    {
                        uc.SetPreparation ( info );

                        //FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��������������Ϣ¼��";
                        //FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                        uc.ShowDialog ( );

                        //if (uc.Result == DialogResult.OK)
                        //{
                        //    return 1;
                        //}
                        //else
                        //{
                        //    return -1;
                        //}
                    }

                    #endregion

                    break;
                }
            }

            return 1;
        }

        /// <summary>
        /// ������Ч��У��
        /// </summary>
        /// <returns></returns>
        protected virtual bool DataValid()
        {
            //{7AD459B7-0533-46f1-B39E-8A36810377F5} ���ӶԼ���ϸ���ж�
            for (int i = 0; i < this.fsDrug_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Preparation info = this.GetDrugFromFp(i);
                if (this.saveState == FS.HISFC.Models.Preparation.EnumState.SemiAssay || this.saveState == FS.HISFC.Models.Preparation.EnumState.PackAssay)
                {
                    if (info.IsAssayEligible == false)
                    {
                        DialogResult rs = MessageBox.Show(Language.Msg("���Ƽ��ƻ��ڴ��ڼ��鲻�ϸ���Ƽ���ȷ�ϱ�����"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (rs == DialogResult.No)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        #region �Ƽ��ƻ��ĳ���  �½�/�޸�/����

        /// <summary>
        /// ����Ƽ��ƻ���Ϣ
        /// </summary>
        protected void NewPreparation ( )
        {
            this.fsDrug_Sheet1.Rows.Count = 0;

            this.fsDrug_Sheet1.Rows.Add ( 0 , 1 );
        }

        /// <summary>
        /// �����Ƽ��ƻ���Ϣ
        /// </summary>
        protected void AddNewPreparationDetail ( )
        {
            this.fsDrug_Sheet1.Rows.Add ( this.fsDrug_Sheet1.Rows.Count , 1 );
        }

        /// <summary>
        /// ɾ���Ƽ��ƻ���ϸ
        /// </summary>
        protected void DelPreparationPlanDetail ( )
        {
            if ( this.fsDrug_Sheet1.Rows.Count < 0 )
            {
                return;
            }

            DialogResult rs = MessageBox.Show ( FS.FrameWork.Management.Language.Msg ( "�Ƿ�ȷ��ɾ����ǰѡ��ļƻ���Ϣ" ) , "" , MessageBoxButtons.YesNo , MessageBoxIcon.Question );
            if ( rs == DialogResult.No )
            {
                return;
            }

            FS.HISFC.Models.Preparation.Preparation info = this.fsDrug_Sheet1.Rows [ this.fsDrug_Sheet1.ActiveRowIndex ].Tag as FS.HISFC.Models.Preparation.Preparation;
            if ( info != null )
            {
                if ( this.preparationManager.DelPreparation ( info.PlanNO , info.Drug.ID ) == -1 )
                {
                    MessageBox.Show ( FS.FrameWork.Management.Language.Msg ( "ɾ���Ƽ��ƻ���ϸ��Ϣʧ��" ) + this.preparationManager.Err );
                    return;
                }
            }

            this.fsDrug_Sheet1.Rows.Remove ( this.fsDrug_Sheet1.ActiveRowIndex , 1 );

            if (this.fsDrug_Sheet1.Rows.Count == 0)
            {
                this.ucExpand1.Clear();
                this.ShowList();
            }
        }

        /// <summary>
        /// �Ƽ�����Ϣ���Ƽ��ƻ�����
        /// </summary>
        internal protected int SavePreparationPlan ( )
        {
            if ( this.fsDrug_Sheet1.Rows.Count <= 0 )
            {
                return 0;
            }

            #region  ���浱ǰ�Ƽ���Ʒ��������Ϣ

            if (!string.IsNullOrEmpty(this.ucExpand1.PlanNO))
            {
                string msg = "";
                if (this.ucExpand1.SaveExpandInfo(true, ref msg) == -1)
                {
                    MessageBox.Show(msg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            #endregion

            //���ݵ�ǰ������������������ж��Ƿ���Ҫ����ԭ��������
            System.Collections.Generic.Dictionary<string, bool> autoApplyList = new Dictionary<string, bool>();
            for (int i = 0; i < this.fsDrug_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Preparation info = this.GetDrugFromFp(i);

                //{64FAE14C-7D1B-42ea-B19D-2C1B3846D2D0} ������Ϣ�Զ�����ʱ 
                autoApplyList.Add(info.Drug.ID, this.ucExpand1.ValidStock(info, true));
            }


            FS.FrameWork.Management.PublicTrans.BeginTransaction ( );

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy ( );          
            this.preparationManager.SetTrans ( FS.FrameWork.Management.PublicTrans.Trans );

            string planNO = "";

            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime ( );

            for ( int i = 0; i < this.fsDrug_Sheet1.Rows.Count; i++ )
            {
                #region ���ݱ���

                FS.HISFC.Models.Preparation.Preparation info = this.GetDrugFromFp ( i );
                if ( info == null )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack ( );
                    MessageBox.Show ( Language.Msg ( "��Fp�ڻ�ȡ�Ƽ���Ϣ��������" ) );
                    return -1;
                }

                #region �����ƻ��Ŵ���

                if ( planNO == "" )
                {
                    if ( info.PlanNO == "" || info.PlanNO == null )
                    {
                        pharmacyIntegrate.GetCommonListNO((( FS.HISFC.Models.Base.Employee ) this.preparationManager.Operator ).Dept.ID);
                        if ( planNO == null )
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack ( );
                            MessageBox.Show ( Language.Msg ( "��ȡ�����ƻ��ŷ�������" ) + pharmacyIntegrate.Err );
                            return -1;
                        }
                    }
                    else
                    {
                        planNO = info.PlanNO;
                    }
                }

                #endregion

                info.PlanNO = planNO;
                info.PlanEnv.ID = this.preparationManager.Operator.ID;
                info.PlanEnv.OperTime = sysTime;
                info.OperEnv = info.PlanEnv;

                if ( this.preparationManager.PreparationPlan ( info ) == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack ( );
                    MessageBox.Show ( Language.Msg ( "�����Ƽ��ƻ���Ϣ���淢������" ) + this.preparationManager.Err );
                    return -1;
                }

                #endregion

                #region ԭ���Ͽ���ж�

                if (autoApplyList.ContainsKey(info.Drug.ID))
                {
                    if (autoApplyList[info.Drug.ID])
                    {
                        string strErr = "";
                        this.ucExpand1.PlanNO = info.PlanNO;

                        if (this.ucExpand1.SaveExpandForStock(info, true, ref strErr) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg(strErr));
                            return -1;
                        }
                    }
                }

                #endregion
            }

            FS.FrameWork.Management.PublicTrans.Commit ( );
            MessageBox.Show(Language.Msg("����ɹ�"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ˢ���б���ʾ
            return this.ShowList();
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isClearDrug">�Ƿ����������Ϣ</param>        
        internal protected void Clear ( bool isClearExpandDrug )
        {
            this.fsDrug_Sheet1.Rows.Count = 0;
            if ( isClearExpandDrug )
            {
                this.ucExpand1.Clear ( );
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="inTargetDept">���Ŀ�����</param>
        /// <param name="isApply">�Ƿ���Ҫ����</param>
        private int ConfigInputSetting(ref FS.FrameWork.Models.NeuObject inTargetDept, out bool isNeedApply)
        {
            return ucChooseData.ChooseInputTargetData ( this.stockDept , ref inTargetDept , out isNeedApply );
        }

        /// <summary>
        /// �ɱ��ۼ���
        /// </summary>
        /// <returns></returns>
        private int ComputeCostPrice ( ref FS.HISFC.Models.Preparation.Preparation preparation , ComputeCostPriceType computeType )
        {
            if ( preparation.CostPrice == 0 || computeType == ComputeCostPriceType.Manual )
            {
                return ucCostPrice.ComputeCostPrice ( this.preparationManager , ref preparation , computeType );
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// �Ƽ�����Ϣ���������̱���
        /// </summary>
        internal protected int SavePreparation ( )
        {
            if (this.fsDrug_Sheet1.Rows.Count <= 0)
            {
                return 0;
            }

            #region �������̡���� ��Ϣ�жϲ���

            //����У��
            if (!this.DataValid())
            {
                return -1;
            }

            if (this.isExpandMaterial)
            {
                DialogResult materialRs = MessageBox.Show(Language.Msg("��ȷ�ϵ�ǰԭ����������Ϣ�����Ƿ���ȷ?"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (materialRs == DialogResult.No)
                {
                    return -1;
                }
            }

            DialogResult rs = MessageBox.Show(Language.Msg("��ȷ������������������̵�¼��?"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                if (this.SetProcess() == -1)
                {
                    return -1;
                }
            }

            FS.FrameWork.Models.NeuObject inTargetDept = this.stockDept.Clone();
            bool isApply = false;
            if (this.isInputDrug)
            {
                if (this.ConfigInputSetting(ref inTargetDept, out isApply) == -1)
                {
                    return -1;
                }
            }

            #endregion

            #region  ���浱ǰ�Ƽ���Ʒ��������Ϣ

            if (!string.IsNullOrEmpty(this.ucExpand1.PlanNO))
            {
                string msg = "";
                if (this.ucExpand1.SaveExpandInfo(true, ref msg) == -1)
                {
                    MessageBox.Show(msg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            #endregion            

            FS.FrameWork.Management.PublicTrans.BeginTransaction ( );

            this.preparationManager.SetTrans ( FS.FrameWork.Management.PublicTrans.Trans );
            this.pharamcyIntegrate.SetTrans ( FS.FrameWork.Management.PublicTrans.Trans );

            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime ( );

            List<FS.HISFC.Models.Preparation.Preparation> inPreparationList = new List<FS.HISFC.Models.Preparation.Preparation> ( );

            for ( int i = 0; i < this.fsDrug_Sheet1.Rows.Count; i++ )
            {
                FS.HISFC.Models.Preparation.Preparation info = this.GetDrugFromFp ( i );
                if ( info == null )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack ( );
                    MessageBox.Show ( Language.Msg ( "��Fp�ڻ�ȡ�Ƽ���Ϣ��������" ) );
                    return -1;
                }
                info.OperEnv.ID = this.preparationManager.Operator.ID;
                info.OperEnv.OperTime = sysTime;

                #region �Ƽ����̲�����Ա������Ϣ��ֵ

                if ( this.isUpdateConfectOper )
                {
                    info.ConfectEnv = info.OperEnv;
                    if ( this.preparationManager.UpdatePreparationConfect ( info ) == -1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack ( );
                        MessageBox.Show ( Language.Msg ( "�����Ƽ�������Ա��Ϣ��������" ) + this.preparationManager.Err );
                        return -1;
                    }
                }
                if ( this.isUpdateAssayOper )
                {
                    info.AssayEnv = info.OperEnv;
                    if ( this.preparationManager.UpdatePreparationAssay ( info ) == -1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack ( );
                        MessageBox.Show ( Language.Msg ( "�����Ƽ�������Ա��Ϣ��������" ) + this.preparationManager.Err );
                        return -1;
                    }
                }
                if ( this.isUpdateInputOper )
                {
                    info.InputEnv = info.OperEnv;
                    if ( this.preparationManager.UpdatePreparationInput ( info ) == -1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack ( );
                        MessageBox.Show ( Language.Msg ( "�����Ƽ������Ա��Ϣ��������" ) + this.preparationManager.Err );
                        return -1;
                    }
                }

                #endregion

                #region �Ƽ�״̬����

                if ( this.isCanReSet )
                {
                    if ( this.preparationManager.UpdatePreparationState ( info , this.saveState , this.listState , this.saveState ) == -1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack ( );
                        MessageBox.Show ( Language.Msg ( "�����Ƽ�״̬��Ϣ��������" ) + this.preparationManager.Err );
                        return -1;
                    }
                }
                else
                {
                    if ( this.preparationManager.UpdatePreparationState ( info , this.saveState , this.listState ) == -1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack ( );
                        MessageBox.Show ( Language.Msg ( "�����Ƽ�״̬��Ϣ��������" ) + this.preparationManager.Err );
                        return -1;
                    }
                }

                #endregion

                #region ԭ�Ͽ۳�

                if (this.isExpandMaterial)
                {
                    string strErr = "";
                    if (this.ucExpand1.SaveExpandForStock(info, false, ref strErr) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        strErr = "�Ƽ��� " + info.Drug.Name + " ԭ���Ͽ��۳�ʧ��. \n  " + strErr;
                        MessageBox.Show(strErr);
                        return -1;
                    }
                }

                #endregion

                inPreparationList.Add ( info );
            }

            #region ��Ʒ���

            if ( this.isInputDrug )
            {
                if ( pharamcyIntegrate.ProduceInput ( inPreparationList , this.stockDept , inTargetDept , isApply ) == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack ( );
                    MessageBox.Show ( Language.Msg ( "��Ʒ���ʱ��������" ) + pharamcyIntegrate.Err );
                    return -1;
                }
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit ( );
            MessageBox.Show(Language.Msg("����ɹ�"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return 1;
        }

        #region �¼�����

        protected override void OnLoad ( EventArgs e )
        {
            try
            {
                if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
                {
                    this.stockDept = ((FS.HISFC.Models.Base.Employee)this.preparationManager.Operator).Dept;
                    if (this.ucExpand1 != null)
                    {
                        this.ucExpand1.StockDept = this.stockDept;
                    }

                    this.Init();

                    this.tvList = this.tv as tvPlanList;

                    this.ShowList();
                }
            }
            catch
            { }

            base.OnLoad ( e );
        }

        protected override int OnSetValue ( object neuObject , TreeNode e )
        {
            this.Clear ( true );

            if ( e.Tag != null )
            {
                FS.HISFC.Models.Preparation.Preparation info = e.Tag as FS.HISFC.Models.Preparation.Preparation;
                if ( info != null )
                {
                    this.ShowPreparation ( info.PlanNO );
                }
            }
            return base.OnSetValue ( neuObject , e );
        }

        private void btnControl_Click ( object sender , EventArgs e )
        {
            if ( this.splitContainer1.Panel2Collapsed )
            {
                this.splitContainer1.Panel2Collapsed = false;
                this.btnControl.Text = "����ԭ����������Ϣ";
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = true;
                this.btnControl.Text = "��ʾԭ����������Ϣ";
            }
        }

        private void fsDrug_CellDoubleClick ( object sender , FarPoint.Win.Spread.CellClickEventArgs e )
        {
            if ( e.Column == ( int ) DrugColumnSet.ColDrugName )
            {
                FS.FrameWork.Models.NeuObject selectItem = new FS.FrameWork.Models.NeuObject();
                if ( FS.FrameWork.WinForms.Classes.Function.ChooseItem ( this.alPrescription , ref selectItem ) == 1 )
                {
                    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item ( );
                    FS.HISFC.Models.Pharmacy.Item tempItem = itemManager.GetItem ( selectItem.ID );
                    if ( tempItem != null )
                    {
                        FS.HISFC.Models.Preparation.Preparation info = new FS.HISFC.Models.Preparation.Preparation ( );
                        info.Drug = tempItem;

                        this.AddDrugToFp ( info , e.Row , false );

                        this.fsDrug_Sheet1.ActiveRowIndex = this.fsDrug_Sheet1.Rows.Count - 1;
                        this.fsDrug_Sheet1.AddSelection ( this.fsDrug_Sheet1.Rows.Count - 1 , 0 , 1 , -1 );
                        this.fsDrug_SelectionChanged ( null , null );
                    }
                }
            }
        }

        private void fsDrug_EditModeOff ( object sender , EventArgs e )
        {
            if ( this.fsDrug_Sheet1.ActiveRowIndex >= 0 )
            {
                int index = this.fsDrug_Sheet1.ActiveRowIndex;

                this.ShowExpandPrescription ( index );
            }
        }

        private void fsDrug_SelectionChanged ( object sender , FarPoint.Win.Spread.SelectionChangedEventArgs e )
        {
            if ( this.fsDrug_Sheet1.ActiveRowIndex >= 0 )
            {
                int iIndex = this.fsDrug_Sheet1.ActiveRowIndex;

                this.ShowExpandPrescription ( iIndex );
            }
        }

        #endregion

        #region ��ö��

        protected enum DrugColumnSet
        {
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColDrugName ,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs ,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty ,
            /// <summary>
            /// ��װ��λ
            /// </summary>
            ColPackUnit ,
            /// <summary>
            /// �ƻ���
            /// </summary>
            ColPlanNum ,
            /// <summary>
            /// ��λ
            /// </summary>
            ColPlanUnit ,
            /// <summary>
            /// �ͼ���
            /// </summary>
            ColAssayQty,
            /// <summary>
            /// ������
            /// </summary>
            ColAssayResult ,
            /// <summary>
            /// ����
            /// </summary>
            ColBatchNO ,
            /// <summary>
            /// ��Ч��
            /// </summary>
            ColValidDate ,
            /// <summary>
            /// ���Ʒ��
            /// </summary>
            ColConfectQty ,
            /// <summary>
            /// �����
            /// </summary>
            ColInQty ,
            /// <summary>
            /// �ɱ���
            /// </summary>
            ColCostPrice ,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type [ ] InterfaceTypes
        {
            get
            {
                Type [ ] printType = new Type [ 1 ];
                printType [ 0 ] = typeof ( FS.HISFC.Components.Preparation.IProcess );

                return printType;
            }
        }

        #endregion
    }
}
