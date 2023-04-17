using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.Preparation.Stencil
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ���Ʒģ��ά��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <˵��>
    ///    1���б����ʱֻ��ȡ��ά�������ô�����ҩƷ
    /// </˵��>
    /// </summary>
    public partial class ucStencil : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucStencil()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();

        /// <summary>
        /// ������Ʒ�б���Ϣ
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> prescriptionList;

        /// <summary>
        /// ҩƷ�б�����
        /// </summary>
        private ArrayList alDrugList = null;

        /// <summary>
        /// ��ǰ���ô�����Ʒ����
        /// </summary>
        private string nowDrugPrescription = "";

        /// <summary>
        /// ģ������
        /// </summary>
        private FS.HISFC.Models.Preparation.EnumStencialType stencilType = FS.HISFC.Models.Preparation.EnumStencialType.ProductAssayStencial;

        #endregion

        #region ����

        /// <summary>
        /// ģ������
        /// </summary>
        public FS.HISFC.Models.Preparation.EnumStencialType StencilType
        {
            get
            {
                return this.stencilType;
            }
            set
            {
                this.stencilType = value;
            }
        }

        #endregion

        #region ������

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugHelper = null;

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "�����Ƽ���Ʒģ����ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ���Ƽ���Ʒģ����ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.AddStencilData();
            }
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelStencilData();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.SaveStencil();
        }

        protected override int OnQuery(object sender, object neuObject)
        {

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ػ������� ���Ժ�...");
            Application.DoEvents();

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            List<FS.HISFC.Models.Pharmacy.Item> phaList = pharmacyIntegrate.QueryItemList(true);
            if (phaList == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ�б�������") + pharmacyIntegrate.Err);
                return;
            }
            foreach (FS.HISFC.Models.Pharmacy.Item info in phaList)
            {
                info.Memo = info.Specs;
            }

            this.alDrugList = new ArrayList(phaList.ToArray());
            this.drugHelper = new FS.FrameWork.Public.ObjectHelper(this.alDrugList);

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCell = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            this.fsStencil_Sheet1.Columns[4].CellType = markNumCell;
            this.fsStencil_Sheet1.Columns[5].CellType = markNumCell;

            this.stencilItemTypeList = FS.HISFC.Models.Preparation.EnumStencilItemTypeService.List();
            string[] strItemType = new string[this.stencilItemTypeList.Count];
            int i = 0;
            foreach (FS.FrameWork.Models.NeuObject tempItemType in this.stencilItemTypeList)
            {
                strItemType[i] = tempItemType.Name;
                i++;
            }

            FarPoint.Win.Spread.CellType.ComboBoxCellType itemTypeCombo = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            itemTypeCombo.Items = strItemType;
            this.fsStencil_Sheet1.Columns[2].CellType = itemTypeCombo;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            FarPoint.Win.Spread.InputMap im;
            im = this.fsStencil.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        private string GetDescriptionForStencilType(FS.HISFC.Models.Preparation.EnumStencialType type)
        {
            switch (type)
            {
                case FS.HISFC.Models.Preparation.EnumStencialType.ProductAssayStencial:
                    return "��Ʒ����ģ��";
                case FS.HISFC.Models.Preparation.EnumStencialType.ProductStencial:
                    return "������������";
                case FS.HISFC.Models.Preparation.EnumStencialType.SemiAssayStencial:
                    return "���Ʒ����ģ��";
            }

            return "";
        }

        #endregion

        #region �Ƽ���Ʒ��Ϣ����

        /// <summary>
        /// ��ȡ��Ʒ���ƴ�����Ϣ
        /// </summary>
        /// <returns></returns>
        public int ShowPrescriptionList()
        {
            this.fsDrug_Sheet1.Rows.Count = 0;

            this.prescriptionList = this.preparationManager.QueryPrescriptionList(FS.HISFC.Models.Base.EnumItemType.Drug);
            if (this.prescriptionList == null)
            {
                MessageBox.Show(Language.Msg("δ��ȷ��ȡ��Ʒ���ƴ�����Ϣ \n" + this.preparationManager.Err));
                return -1;
            }

            foreach (FS.FrameWork.Models.NeuObject info in this.prescriptionList)
            {
                if (this.AddDrugToFp(this.drugHelper.GetObjectFromID(info.ID) as FS.HISFC.Models.Pharmacy.Item) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ��ӳ�Ʒ��Ϣ��Fp��
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected int AddDrugToFp(FS.HISFC.Models.Pharmacy.Item item)
        {
            try
            {
                int rowCount = this.fsDrug_Sheet1.Rows.Count;
                this.fsDrug_Sheet1.Rows.Add(rowCount, 1);

                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColDrugID].Text = item.ID;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColTradeName].Text = item.Name;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColSpecs].Text = item.Specs;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColPackQty].Text = item.PackQty.ToString();
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColPackUnit].Text = item.PackUnit;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColMinUnit].Text = item.MinUnit;

                this.fsDrug_Sheet1.Rows[rowCount].Tag = item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        #endregion

        #region ģ����Ϣ����

        private ArrayList stencilItemTypeList = new ArrayList();

        /// <summary>
        /// ����������Ϣ��ȡö��
        /// </summary>
        /// <param name="str">������Ϣ</param>
        /// <returns>ö��</returns>
        private FS.HISFC.Models.Preparation.EnumStencilItemType GetEnumFromDescription(string str)
        {
            string itemTypeID = "";
            foreach (FS.FrameWork.Models.NeuObject tempItemType in this.stencilItemTypeList)
            {
                if (tempItemType.Name == str)
                {
                    itemTypeID = tempItemType.ID;
                    break;
                }
            }

            return (FS.HISFC.Models.Preparation.EnumStencilItemType)Enum.Parse(typeof(FS.HISFC.Models.Preparation.EnumStencilItemType), itemTypeID);
        }

        /// <summary>
        /// ����ö�ٻ�ȡ������Ϣ
        /// </summary>
        /// <param name="enumItemType">ģ�����ö��</param>
        /// <returns>������Ϣ</returns>
        private string GetDescriptionFromEnum(FS.HISFC.Models.Preparation.EnumStencilItemType enumItemType)
        {
            foreach (FS.FrameWork.Models.NeuObject tempItemType in this.stencilItemTypeList)
            {
                if (tempItemType.ID == enumItemType.ToString())
                {
                    return tempItemType.Name;
                }
            }

            return "";
        }

        /// <summary>
        /// ����ģ����Ϣ
        /// </summary>
        protected void AddStencilData()
        {
            this.fsStencil_Sheet1.Rows.Add(this.fsStencil_Sheet1.Rows.Count, 1);
        }

        /// <summary>
        /// ɾ��ģ����ϸ��Ϣ
        /// </summary>
        protected int DelStencilData()
        {
            int rowIndex = this.fsStencil_Sheet1.ActiveRowIndex;
            if (rowIndex >= 0)
            {
                DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�ȷ��ɾ������ϸ��Ϣ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.No)
                {
                    return -1;
                }
            }

            FS.HISFC.Models.Preparation.Stencil tempStencil = this.GetStencilData(rowIndex);
            if (tempStencil != null)
            {
                if (this.preparationManager.DelStencil(tempStencil.ID) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��ʧ��") + this.preparationManager.Err);
                    return -1;
                }

                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ���ɹ�"));
            }

            this.fsStencil_Sheet1.Rows.Remove(rowIndex, 1);

            return 1;
        }

        /// <summary>
        /// ģ����Ϣ����
        /// </summary>
        protected void ShowStencil()
        {           
            List<FS.HISFC.Models.Preparation.Stencil> stencilList = this.preparationManager.QueryStencil(this.nowDrugPrescription, this.stencilType);
            if (stencilList == null)
            {
                MessageBox.Show(Language.Msg(this.preparationManager.Err));
                return;
            }

            this.SetStencilData(stencilList);
        }

        /// <summary>
        /// ģ����ϸ��Ϣ���
        /// </summary>
        protected void SetStencilData(List<FS.HISFC.Models.Preparation.Stencil> stencilList)
        {
            this.fsStencil_Sheet1.Rows.Count = 0;
            
            foreach (FS.HISFC.Models.Preparation.Stencil info in stencilList)
            {
                int i = this.fsStencil_Sheet1.Rows.Count;

                this.fsStencil_Sheet1.Rows.Add(i, 1);
                this.fsStencil_Sheet1.Cells[i, 0].Text = info.Type.ID;                  //���
                this.fsStencil_Sheet1.Cells[i, 1].Text = info.Type.Name;
                this.fsStencil_Sheet1.Cells[i, 2].Text = this.GetDescriptionFromEnum(info.ItemType);      //��Ŀ���
                this.fsStencil_Sheet1.Cells[i, 3].Text = info.Item.Name;                
                this.fsStencil_Sheet1.Cells[i, 4].Text = info.StandardMax.ToString();   //��׼ֵ
                this.fsStencil_Sheet1.Cells[i, 5].Text = info.StandardMin.ToString();
                this.fsStencil_Sheet1.Cells[i, 6].Text = info.StandardDes;
                this.fsStencil_Sheet1.Cells[i, 7].Text = info.Memo;
                this.fsStencil_Sheet1.Cells[i, 8].Text = info.ID;

                this.fsStencil_Sheet1.Rows[i].Tag = info;
            }
        }

        /// <summary>
        /// ��ȡģ����ϸ��Ϣ
        /// </summary>
        /// <param name="rowIndex">������</param>
        /// <returns>�ɹ�����ģ����ϸ��Ϣ ʧ�ܷ���null</returns>
        protected FS.HISFC.Models.Preparation.Stencil GetStencilData(int rowIndex)
        {
            FS.HISFC.Models.Preparation.Stencil info = new FS.HISFC.Models.Preparation.Stencil();

            info.Drug = this.fsDrug_Sheet1.Rows[this.fsDrug_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Pharmacy.Item;
            if (info.Drug == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��ǰѡ���Ƽ���Ʒ��Ϣʱ��������"));
                return null;
            }

            info.Type.ID = this.fsStencil_Sheet1.Cells[rowIndex, 0].Text;       //���
            info.Type.Name = this.fsStencil_Sheet1.Cells[rowIndex, 1].Text;

            info.ItemType = this.GetEnumFromDescription(this.fsStencil_Sheet1.Cells[rowIndex, 2].Text);

            info.Item.Name = this.fsStencil_Sheet1.Cells[rowIndex, 3].Text;
            info.StandardMax = FS.FrameWork.Function.NConvert.ToDecimal(this.fsStencil_Sheet1.Cells[rowIndex, 4].Text);
            info.StandardMin = FS.FrameWork.Function.NConvert.ToDecimal(this.fsStencil_Sheet1.Cells[rowIndex, 5].Text);
            info.StandardDes = this.fsStencil_Sheet1.Cells[rowIndex, 6].Text;
            info.Memo = this.fsStencil_Sheet1.Cells[rowIndex, 7].Text;
            info.ID = this.fsStencil_Sheet1.Cells[rowIndex, 8].Text;

            return info;
        }

        /// <summary>
        /// ģ����Ϣ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        protected int SaveStencil()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.preparationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.fsStencil_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Stencil info = this.GetStencilData(i);

                info.OperEnv.ID = this.preparationManager.Operator.ID;
                info.OperEnv.OperTime = sysTime;

                if (this.preparationManager.SetStencil(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ģ����Ϣ����ʧ��") + this.preparationManager.Err);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));

            return 1;
        }

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            this.ShowPrescriptionList();
            
            base.OnLoad(e);
        }

        private void fsDrug_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.nowDrugPrescription == this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, 0].Text)
            {
                return;
            }
            else
            {
                this.nowDrugPrescription = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, 0].Text;
            }

            FS.HISFC.Models.Preparation.EnumStencilItemTypeService s = new FS.HISFC.Models.Preparation.EnumStencilItemTypeService();

            this.lbInformation.Text = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, (int)DrugColumnSet.ColTradeName].Text + "  " + this.GetDescriptionForStencilType(this.StencilType);

            this.ShowStencil();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fsStencil.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fsStencil_Sheet1.ActiveColumnIndex == this.fsStencil_Sheet1.Columns.Count - 1)
                    {
                        this.fsStencil_Sheet1.Rows.Add(this.fsStencil_Sheet1.Rows.Count, 1);
                        this.fsStencil_Sheet1.ActiveRowIndex = this.fsStencil_Sheet1.Rows.Count;
                        this.fsStencil_Sheet1.ActiveColumnIndex = 0;                        
                    }
                    else
                    {
                        this.fsStencil_Sheet1.ActiveColumnIndex++;
                        return base.ProcessDialogKey(keyData);
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }        

        #endregion

        #region ö��

        /// <summary>
        /// �Ƽ���Ʒ������
        /// </summary>
        protected enum DrugColumnSet
        {
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColDrugID,
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// ��װ��λ
            /// </summary>
            ColPackUnit,
            /// <summary>
            /// ��С��λ
            /// </summary>
            ColMinUnit
        }

        #endregion       
    }
}
