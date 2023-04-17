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
    /// [��������: ��Ʒ����ά������Ʒ��Ϣ��ʾ]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-05]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class ucProduct : FS.FrameWork.WinForms.Controls.ucBaseControl,IPrescriptionProduct
    {
        public ucProduct()
        {
            InitializeComponent();
        }


        #region ����

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();     

        /// <summary>
        /// ��ǰ��ά���õ����ƴ���
        /// </summary>
        private System.Collections.Hashtable hsPrescription = new Hashtable();

        /// <summary>
        /// ҩƷ�б�����
        /// </summary>
        private ArrayList alDrugList = null;

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private FS.HISFC.Models.Base.EnumItemType itemType = FS.HISFC.Models.Base.EnumItemType.Drug;
        #endregion

        #region ������

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugHelper = null;

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
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
        }

        #endregion

        /// <summary>
        /// ѡ���Ʒ
        /// </summary>
        protected FS.HISFC.Models.Pharmacy.Item SelectDrug()
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alDrugList, new string[] { "ҩƷ����", "ҩƷ����", "���" }, new bool[] { false, true, true }, new int[] { 80, 120, 80 }, ref info) == 0)
            {
                return null;
            }
            else
            {
                return info as FS.HISFC.Models.Pharmacy.Item;
            }
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

        /// <summary>
        /// ����Ƽ��³�Ʒ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int AddNewDrug()
        {
            FS.HISFC.Models.Pharmacy.Item item = this.SelectDrug();
            if (item == null)
            {
                return -1;
            }

            if (this.hsPrescription.ContainsKey(item.ID))
            {
                MessageBox.Show(Language.Msg("��ҩƷ��ά�������ƴ���"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            this.hsPrescription.Add(item.ID, null);

            this.AddDrugToFp(item);

            this.fsDrug_Sheet1.ActiveRowIndex = this.fsDrug_Sheet1.Rows.Count - 1;
            this.fsDrug_Sheet1.AddSelection(this.fsDrug_Sheet1.Rows.Count - 1, 0, 1, -1);
            this.fsDrug_SelectionChanged(null, null);

            return 1;
        }

        #region IPrescriptionProduct ��Ա

        public event EventHandler ShowPrescriptionEvent;

        public int AddProduct()
        {
            return this.AddNewDrug();
        }

        public int ShowProduct(FS.FrameWork.Models.NeuObject product)
        {
            FS.HISFC.Models.Pharmacy.Item item = this.drugHelper.GetObjectFromID(product.ID) as FS.HISFC.Models.Pharmacy.Item;

            return this.AddDrugToFp(item);
        }

        public int DeleteProduct()
        {
            string drugCode = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, 0].Text;

            DialogResult rs = MessageBox.Show(Language.Msg("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��\n ����ɾ��������ɾ���ó�Ʒ�������ƴ�����Ϣ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
                return 1;
            if (this.preparationManager.DelPrescription(drugCode,this.itemType) == -1)
            {
                MessageBox.Show("�Ե�ǰѡ���Ʒִ��ɾ������ʧ��\n" + this.preparationManager.Err);
                return -1;
            }

            if (this.hsPrescription.ContainsKey(drugCode))
            {
                this.hsPrescription.Remove(drugCode);
            }

            return 1;
        }

        public int Clear()
        {
            this.fsDrug_Sheet1.Rows.Count = 0;

            return 1;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void fsDrug_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            FS.HISFC.Models.Pharmacy.Item item = this.fsDrug_Sheet1.Rows[this.fsDrug_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Pharmacy.Item;

            if (this.ShowPrescriptionEvent != null)
            {
                this.ShowPrescriptionEvent(item, System.EventArgs.Empty);
            }
        }


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
    }
}
