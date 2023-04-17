using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Logistics.DrugStore
{
    public partial class ucPhaMetDrugDoct : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaMetDrugDoct()
        {
            InitializeComponent();
        }
        #region ����
        /// <summary>
        /// ��ѯ��Χ
        /// </summary>
        private DeptZone deptZone1 = DeptZone.ALL;
        /// <summary>
        /// ҩƷ�����������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        /// <summary>
        /// ҩƷ������Ϣʵ��
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item itemObject = new FS.HISFC.Models.Pharmacy.Item();
        /// <summary>
        /// ���ڴ洢ҩƷ�ֵ�
        /// </summary>
        private ArrayList arrDrugList = null;
        /// <summary>
        /// ���ڴ洢ҩƷ�ֵ�list
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.Item> itemList = new List<FS.HISFC.Models.Pharmacy.Item>();
        #endregion

        #region ����
        /// <summary>
        /// ��ѯ��Χ
        /// </summary>
        [Category("��������"), Description("��ѯ��Χ��MZ:���ZY:סԺ��ALL:ȫԺ")]
        public DeptZone DeptZone1
        {
            get
            {
                return deptZone1;
            }
            set
            {
                deptZone1 = value;
            }
        }
        #endregion

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            string strFeelan = "ȫԺ";

            if (!string.IsNullOrEmpty(cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString()))
            {
                strFeelan = cmbFeelan.Items[cmbFeelan.SelectedIndex].ToString();
            }
            string drugName = "ALL";
            if (this.cmbDrugList.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(this.cmbDrugList.SelectedItem.Name))
                {
                    drugName = this.cmbDrugList.SelectedItem.Name;
                }
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, strFeelan, drugName);
           
        }
        /// <summary>
        /// ��������¼�
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            cmbFeelan.Items.Clear();
            cmbDrugList.Items.Clear();
            if (this.deptZone1 == DeptZone.ALL)
            {
                this.cmbFeelan.Items.Add("����");
                this.cmbFeelan.Items.Add("סԺ");
                this.cmbFeelan.Items.Add("ȫԺ");

                this.cmbFeelan.SelectedIndex = 0;
            }
            if (this.deptZone1 == DeptZone.MZ)
            {
                this.cmbFeelan.Items.Add("����");

                this.cmbFeelan.SelectedIndex = 0;

            }
            if (this.deptZone1 == DeptZone.ZY)
            {
                this.cmbFeelan.Items.Add("סԺ");

                this.cmbFeelan.SelectedIndex = 0;
            }

            arrDrugList = new ArrayList();
            itemList = new List<FS.HISFC.Models.Pharmacy.Item>();
            itemObject = new FS.HISFC.Models.Pharmacy.Item();

            itemList = itemManager.QueryItemList();
            if (itemList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.Item itemObj in itemList)
                {
                    arrDrugList.Add(itemObj);
                }
                this.cmbDrugList.AddItems(arrDrugList);
            }

            this.isAcross = true;
            this.isSort = false;
        }


        /// <summary>
        /// ö��
        /// </summary>
        public enum DeptZone
        {
            //����
            MZ = 0,
            //סԺ
            ZY = 1,
            //ȫԺ
            ALL = 2,
        }
    }
}
