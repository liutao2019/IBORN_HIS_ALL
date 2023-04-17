using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation.Prescription
{
    /// <summary>
    /// <br></br>
    /// [��������: ��Ʒ����ά����ԭ����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-05]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class ucMaterial : FS.FrameWork.WinForms.Controls.ucBaseControl,IPrescriptionMaterial
    {
        public ucMaterial()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ��ǰ��ά���õ����ƴ���
        /// </summary>
        private System.Collections.Hashtable hsPrescription = new Hashtable();

        /// <summary>
        /// ��ǰ�����ĳ�Ʒ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject operProduct = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private FS.HISFC.Models.Base.EnumItemType itemType = FS.HISFC.Models.Base.EnumItemType.Drug;
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {      
            FarPoint.Win.Spread.InputMap im;
            im = this.fsMaterial.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCell = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            this.fsMaterial_Sheet1.Columns[(int)MaterialColumnSet.ColQty].CellType = markNumCell;

            this.fsMaterial.PhaListColumnIndex = 1;
            this.fsMaterial.PhaListEnabled = true;
            this.fsMaterial.Init();
        }

        #endregion        
     
        /// <summary>
        /// ��Ӵ�����ϸ��Ϣ
        /// </summary>
        /// <param name="item"></param>
        public int AddItemDetail(FS.HISFC.Models.Pharmacy.Item item)
        {
            int i = this.fsMaterial_Sheet1.ActiveRowIndex;

            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialID].Text = item.ID;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialName].Text = item.Name;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text = item.Specs;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text = item.PriceCollection.RetailPrice.ToString();
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text = item.MinUnit;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPackQty].Text = item.PackQty.ToString();

            this.fsMaterial_Sheet1.Rows[i].Tag = item;

            return 1;
        }

        private void fsMaterial_SelectItem(object sender, EventArgs e)
        {
            if (this.AddItemDetail(sender as FS.HISFC.Models.Pharmacy.Item) == 1)
            {
                this.fsMaterial_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColQty;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fsMaterial.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fsMaterial_Sheet1.ActiveColumnIndex == (int)MaterialColumnSet.ColQty)
                    {
                        this.fsMaterial_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColMemo;
                        return base.ProcessDialogKey(keyData);
                    }
                    if (this.fsMaterial_Sheet1.ActiveColumnIndex == (int)MaterialColumnSet.ColMemo)
                    {
                        this.fsMaterial_Sheet1.Rows.Add(this.fsMaterial_Sheet1.Rows.Count, 1);
                        this.fsMaterial_Sheet1.ActiveRowIndex = this.fsMaterial_Sheet1.Rows.Count;
                        this.fsMaterial_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColMaterialName;
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }        

        #region IPrescriptionMaterial ��Ա

        /// <summary>
        /// ���ԭ�ϡ�������ϸ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int AddMaterial()
        {
            int rowCount = this.fsMaterial_Sheet1.Rows.Count;

            this.fsMaterial_Sheet1.Rows.Add(rowCount, 1);

            return 1;
        }

        public int ShowMaterial(FS.FrameWork.Models.NeuObject product)
        {
            this.fsMaterial_Sheet1.Rows.Count = 0;

            List<FS.HISFC.Models.Preparation.PrescriptionBase> al = this.preparationManager.QueryPrescription(product.ID, this.itemType, FS.HISFC.Models.Preparation.EnumMaterialType.Material);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰѡ���Ʒ�����ƴ�����Ϣ����\n" + product.ID));
                return -1;
            }
            foreach (FS.HISFC.Models.Preparation.PrescriptionBase info in al)
            {
                int i = this.fsMaterial_Sheet1.Rows.Count;

                this.fsMaterial_Sheet1.Rows.Add(i, 1);

                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialID].Text = info.Material.ID;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialName].Text = info.Material.Name;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text = info.Specs;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text = info.Price.ToString();
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColQty].Text = info.NormativeQty.ToString();
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMemo].Text = info.Memo;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text = info.NormativeUnit;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPackQty].Text = info.MaterialPackQty.ToString();

                this.fsMaterial_Sheet1.Rows[i].Tag = info.Material;
            }

            this.operProduct = product.Clone();

            return 1;
        }

        public int DeleteMaterial()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 1;
            }

            if (this.fsMaterial_Sheet1.Rows.Count <= 0)
            {
                return 1;
            }
            int iIndex = this.fsMaterial_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject material = this.fsMaterial_Sheet1.Rows[iIndex].Tag as FS.FrameWork.Models.NeuObject;
            if (material == null)
            {
                return 1;
            }
            if (this.preparationManager.DelPrescription(this.operProduct.ID, this.itemType, material.ID) == -1)
            {
                MessageBox.Show("�Ե�ǰѡ�񴦷���¼����ɾ������ʧ��\n" + this.preparationManager.Err);
                return -1;
            }

            this.fsMaterial_Sheet1.Rows.Remove(iIndex, 1);

            return 1;
        }

        public int Clear()
        {
            this.fsMaterial_Sheet1.Rows.Count = 0;

            return 1;
        }

        public List<FS.HISFC.Models.Preparation.PrescriptionBase> GetMaterial()
        {
            FS.HISFC.Models.Preparation.Prescription info = null;

            List<FS.HISFC.Models.Preparation.PrescriptionBase> prescriptionList = new List<FS.HISFC.Models.Preparation.PrescriptionBase>();
            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Pharmacy.Item drug = this.itemManager.GetItem(this.operProduct.ID);
            if (drug == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return null;
            }

            for (int i = 0; i < this.fsMaterial_Sheet1.Rows.Count; i++)
            {
                if (this.fsMaterial_Sheet1.Cells[i, 0].Text == "")
                {
                    continue;
                }

                info = new FS.HISFC.Models.Preparation.Prescription();

                info.Material = this.fsMaterial_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject;
                if (info.Material == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ת������");
                    return null;
                }

                info.Drug = drug;

                info.Specs = this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text;
                info.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text);
                info.NormativeUnit = this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text;

                info.MaterialType = FS.HISFC.Models.Preparation.EnumMaterialType.Material;
                info.NormativeQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColQty].Text);
                info.Memo = this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMemo].Text;
                info.OperEnv.ID = this.preparationManager.Operator.ID;
                info.OperEnv.OperTime = sysTime;
                info.MaterialPackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPackQty].Text);

                prescriptionList.Add(info);
            }

            return prescriptionList;

        }

        public FS.FrameWork.WinForms.Controls.ucBaseControl ProductControl
        {
            get
            {
                return this;
            }
        }

        public FS.HISFC.Models.Base.EnumItemType ItemType
        {
            set
            {
                this.itemType = value;
            }
        }

        #endregion

        #region ö��

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
            /// ��װ����
            /// </summary>
            ColPackQty,
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

        #endregion
    }
}
