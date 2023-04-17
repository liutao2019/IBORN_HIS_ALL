using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ�����ά���ӿ�ʵ����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class ucWrapper : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.Components.Preparation.IPrescription
    {
        public ucWrapper()
        {
            InitializeComponent();

        }

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();

        /// <summary>
        /// �Ƽ���Ʒ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item drug = null;

        #region IPrescription ��Ա

        public FS.HISFC.Models.Pharmacy.Item Drug
        {
            set
            {
                this.drug = value;
            }
        }

        public string DisplayTitle
        {
            get
            {
                return "��������";
            }
        }

        public FS.FrameWork.WinForms.Controls.ucBaseControl Control
        {
            get 
            {
                return this;
            }
        }

        public int Init()
        {
            FarPoint.Win.Spread.InputMap im;
            im = this.fsWrapper.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            this.fsWrapper.DrugType = "M";
            this.fsWrapper.PhaListEnabled = true;
            this.fsWrapper.PhaListColumnIndex = 1;

            this.fsWrapper.Init();

            return 1;
        }

        public int Save(FS.HISFC.Models.Pharmacy.Item item, ref string information)
        {
            if (FS.FrameWork.Management.PublicTrans.Trans != null)
            {
                this.preparationManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );
            }

            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime();

            try
            {
                FS.HISFC.Models.Preparation.Prescription info = null;

                #region ������������

                for (int i = 0; i < this.fsWrapper_Sheet1.Rows.Count; i++)
                {
                    if (this.fsWrapper_Sheet1.Cells[i, 0].Text == "")
                    {
                        continue;
                    }

                    info = new FS.HISFC.Models.Preparation.Prescription();

                    info.Drug = item;

                    info.Material = this.fsWrapper_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject;
                    if (info.Material == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ת������");
                        return -1;
                    }

                    info.Specs = this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text;
                    info.Price = FS.FrameWork.Function.NConvert.ToDecimal( this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text );
                    info.NormativeUnit = this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text;

                    info.MaterialType = FS.HISFC.Models.Preparation.EnumMaterialType.Wrapper;
                    info.NormativeQty = FS.FrameWork.Function.NConvert.ToDecimal( this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColQty].Text );
                    info.Memo = this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMemo].Text;
                    info.OperEnv.ID = this.preparationManager.Operator.ID;
                    info.OperEnv.OperTime = sysTime;

                    if (this.preparationManager.SetPrescription(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        if (this.preparationManager.DBErrCode == 1)
                        {
                            MessageBox.Show(info.Material.Name + "�����ظ����");
                        }
                        else
                        {
                            MessageBox.Show(Language.Msg("����" + info.Drug.Name + "���ƴ�����Ϣʧ��" + this.preparationManager.Err));
                        }

                        return -1;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        public int Delete()
        {
            if (this.fsWrapper.ContainsFocus)
            {
                #region ����ԭ��ɾ��

                if (this.fsWrapper_Sheet1.Rows.Count <= 0)
                    return 1;

                DialogResult rs = MessageBox.Show(Language.Msg("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                    return 1;


                int iIndex = this.fsWrapper_Sheet1.ActiveRowIndex;
                FS.FrameWork.Models.NeuObject material = this.fsWrapper_Sheet1.Rows[iIndex].Tag as FS.FrameWork.Models.NeuObject;
                if (material == null)
                {
                    return 1;
                }
                if (this.preparationManager.DelPrescription(this.drug.ID,FS.HISFC.Models.Base.EnumItemType.Drug, material.ID) == -1)
                {
                    MessageBox.Show("�Ե�ǰѡ�񴦷���¼����ɾ������ʧ��\n" + this.preparationManager.Err);
                    return -1;
                }
                this.fsWrapper_Sheet1.Rows.Remove(iIndex, 1);

                #endregion
            }

            return 1;
        }

        public int Query()
        {
            if (this.drug == null)
            {
                return -1;
            }
            this.fsWrapper_Sheet1.Rows.Count = 0;

            List<FS.HISFC.Models.Preparation.Prescription> al = this.preparationManager.QueryDrugPrescription(this.drug.ID, FS.HISFC.Models.Preparation.EnumMaterialType.Wrapper);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰѡ���Ʒ����������������Ϣ����\n" + this.drug.ID));
                return -1;
            }
            foreach (FS.HISFC.Models.Preparation.Prescription info in al)
            {
                int i = this.fsWrapper_Sheet1.Rows.Count;

                this.fsWrapper_Sheet1.Rows.Add(i, 1);
                this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialID].Text = info.Material.ID;
                this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialName].Text = info.Material.Name;
                this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text = info.Specs;
                this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text = info.Price.ToString();
                this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColQty].Text = info.NormativeQty.ToString();
                this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMemo].Text = info.Memo;
                this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text = info.NormativeUnit;

                this.fsWrapper_Sheet1.Rows[i].Tag = info.Material;
            }

            return 1;
        }

        public int AddNewItem()
        {
            int rowCount = this.fsWrapper_Sheet1.Rows.Count;

            this.fsWrapper_Sheet1.Rows.Add(rowCount, 1);

            return 1;
        }

        #endregion

        /// <summary>
        /// ��Ӵ�����ϸ��Ϣ
        /// </summary>
        /// <param name="item"></param>
        public int AddItemDetail(Item item)
        {
            int i = this.fsWrapper_Sheet1.ActiveRowIndex;

            this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialID].Text = item.ID;
            this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialName].Text = item.Name;
            this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text = item.Specs;
            this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text = item.PriceCollection.RetailPrice.ToString();
            this.fsWrapper_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text = item.MinUnit;

            this.fsWrapper_Sheet1.Rows[i].Tag = item;

            return 1;
        }

        private void ucWrapper_Load(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCell = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            this.fsWrapper_Sheet1.Columns[(int)MaterialColumnSet.ColQty].CellType = markNumCell;
        }

        private void fsWrapper_SelectItem(object sender, EventArgs e)
        {
            if (this.AddItemDetail(sender as FS.HISFC.Models.Pharmacy.Item) == 1)
            {
                this.fsWrapper_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColQty;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fsWrapper.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fsWrapper_Sheet1.ActiveColumnIndex == (int)MaterialColumnSet.ColQty)
                    {
                        this.fsWrapper_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColMemo;
                        return base.ProcessDialogKey(keyData);
                    }
                    if (this.fsWrapper_Sheet1.ActiveColumnIndex == (int)MaterialColumnSet.ColMemo)
                    {
                        this.fsWrapper_Sheet1.Rows.Add(this.fsWrapper_Sheet1.Rows.Count, 1);
                        this.fsWrapper_Sheet1.ActiveRowIndex = this.fsWrapper_Sheet1.Rows.Count;
                        this.fsWrapper_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColMaterialName;
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }        

        protected enum MaterialColumnSet
        {
            /// <summary>
            /// ԭ�ϱ���
            /// </summary>
            ColMaterialID,
            /// <summary>
            /// ����
            /// </summary>
            ColMaterialName,
            /// <summary>
            /// ���
            /// </summary>
            ColMaterialSpecs,
            /// <summary>
            /// �۸�
            /// </summary>
            ColPrice,
            /// <summary>
            /// ������
            /// </summary>
            ColQty,
            /// <summary>
            /// ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo
        }
    }
}
