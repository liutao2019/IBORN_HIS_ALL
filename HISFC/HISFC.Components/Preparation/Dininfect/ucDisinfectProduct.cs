using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation.Disinfect
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
    public partial class ucDisinfectProduct : FS.FrameWork.WinForms.Controls.ucBaseControl,Prescription.IPrescriptionProduct
    {
        public ucDisinfectProduct()
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
        private ArrayList alUnDrugList = null;

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private FS.HISFC.Models.Base.EnumItemType itemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
        #endregion

        #region ������

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper undrugHelper = null;

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            List<FS.HISFC.Models.Fee.Item.Undrug> undrugList = feeIntegrate.QueryAllItemsList();
            if (undrugList == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ�б�������") + feeIntegrate.Err);
                return;
            }

            this.alUnDrugList = new ArrayList(undrugList.ToArray());
            this.undrugHelper = new FS.FrameWork.Public.ObjectHelper(this.alUnDrugList);
        }

        #endregion

        /// <summary>
        /// ѡ���Ʒ
        /// </summary>
        protected FS.HISFC.Models.Fee.Item.Undrug SelectUnDrug()
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alUnDrugList, new string[] { "��Ŀ����", "ҩƷ����", "���" }, new bool[] { false, true, true }, new int[] { 80, 120, 80 }, ref info) == 0)
            {
                return null;
            }
            else
            {
                return info as FS.HISFC.Models.Fee.Item.Undrug;
            }
        }

        /// <summary>
        /// ��ӳ�Ʒ��Ϣ��Fp��
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected int AddDrugToFp(FS.HISFC.Models.Fee.Item.Undrug item)
        {
            try
            {
                int rowCount = this.fsDrug_Sheet1.Rows.Count;
                this.fsDrug_Sheet1.Rows.Add(rowCount, 1);

                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColUndrugID].Text = item.ID;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColTradeName].Text = item.Name;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColPrice].Text = item.Price.ToString();

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
            FS.HISFC.Models.Fee.Item.Undrug item = this.SelectUnDrug();
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
            FS.HISFC.Models.Fee.Item.Undrug item = this.undrugHelper.GetObjectFromID(product.ID) as FS.HISFC.Models.Fee.Item.Undrug;

            return this.AddDrugToFp(item);
        }

        public int DeleteProduct()
        {
            string drugCode = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, 0].Text;

            DialogResult rs = MessageBox.Show(Language.Msg("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��\n ����ɾ��������ɾ���ó�Ʒ�������ƴ�����Ϣ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
                return 1;
            if (this.preparationManager.DelPrescription(drugCode, this.itemType) == -1)
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
            FS.HISFC.Models.Fee.Item.Undrug item = this.fsDrug_Sheet1.Rows[this.fsDrug_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.Item.Undrug;

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
            /// ��Ʒ����
            /// </summary>
            ColUndrugID,
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ����
            /// </summary>
            ColPrice
        }
    }
}
