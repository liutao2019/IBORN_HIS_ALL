using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Pharmacy;
using System.Collections;

namespace FS.HISFC.Components.Preparation
{
    public partial class ucItemList : UserControl
    {
        public ucItemList()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucPhaItem_Load);
            this.neuSpread1.KeyDown += new KeyEventHandler(neuSpread1_KeyDown);
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);
            this.pbHide.Click += new EventHandler(pbHide_Click);
        }

        public event System.EventHandler SelectItem;

        #region ����
        /// <summary>
        /// �����ļ��洢·��
        /// </summary>
        protected string FilePath = Application.StartupPath + "//Profile//PhaItem.xml";
        /// <summary>
        /// ��������DataSet
        /// </summary>
        protected DataSet dsPhaData;
        /// <summary>
        /// ��������DataView
        /// </summary>
        protected DataView dvPhaData;
        /// <summary>
        /// �������ҩƷ���
        /// </summary>
        protected string drugType = "ALL";
        /// <summary>
        /// ������ ID Ϊ�� ������������ֵ���ڻ�ȡ
        /// </summary>
        protected FS.FrameWork.Models.NeuObject drugDept = new FS.FrameWork.Models.NeuObject(); 
        /// <summary>
        /// �����ַ���
        /// </summary>
        protected string strFilter = "(spell_code like '{0}') or (wb_code like '{0}') or (custom_code like '{0}') or (trade_name like '{0}')";
        #endregion

        #region ����

        /// <summary>
        /// �����ַ���  ��ʽ (spell_code like '{0}') or (wb_code like '{0}')
        /// </summary>
        public string FilterString
        {
            get
            {
                return this.strFilter;
            }
            set
            {
                this.strFilter = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ݼ�����ʼ�� 
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int Init()
        {
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;

            #region ��ȡ����

            FS.FrameWork.Management.DataBaseManger dataBaseMgr = new FS.FrameWork.Management.DataBaseManger();
            if (dsPhaData == null)
            {
                dsPhaData = new DataSet();
            }

            #region ��ȡ��ѯSql����
            string[] strIndex;
            string[] strParam = null;
            if (drugDept == null || drugDept.ID == "")
            {
                if (drugType == "ALL")
                {
                    strIndex = new string[] { "Pharmacy.Item.Info" };
                    strParam = new string[] { };
                }
                else
                {
                    strIndex = new string[] { "Pharmacy.Item.Info", "Preparation.Item.GetList.QueryByType" };
                    strParam = new string[] { drugType };
                }
            }
            else
            {
                strIndex = new string[] { "Pharmacy.Item.Info", "Pharmacy.Item.GetAvailableList.Inpatient.ByDrugType" };
                strParam = new string[] { drugDept.ID, drugType };
            }
            #endregion

            if (dataBaseMgr.ExecQuery(strIndex, ref dsPhaData, strParam) == -1)
            {
                MessageBox.Show("��ȡҩƷ��Ϣʱ�������� \n" + dataBaseMgr.Err);
                return -1;
            }

            if (dsPhaData.Tables.Count > 0)
            {
                this.dvPhaData = new DataView(this.dsPhaData.Tables[0]);
                this.neuSpread1_Sheet1.DataSource = dvPhaData;

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, this.FilePath);
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// ���ݼ�����ʼ��
        /// </summary>
        /// <param name="drugDept">�����ⷿ</param>
        /// <param name="drugType">����ҩƷ��� ALL�����������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int Init(FS.FrameWork.Models.NeuObject drugDept, string drugType)
        {
            if (drugDept == null || drugDept.ID == "")
                this.drugDept = new FS.FrameWork.Models.NeuObject();
            else
                this.drugDept = drugDept;
            this.drugType = drugType;
            return this.Init();
        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        private void SetFormat()
        {

        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="str">�����ַ���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int Filter(string str)
        {
            try
            {
                if (this.dvPhaData == null)
                    return -1;
                str = FS.FrameWork.Public.String.TakeOffSpecialChar(str);
                if (!this.ckExactSearch.Checked)
                {
                    str = "%" + str + "%";
                }
                this.dvPhaData.RowFilter = string.Format(this.strFilter, str);
                this.neuSpread1_Sheet1.DataSource = this.dvPhaData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ִ�й��˲��������쳣 \n" + ex.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ȡ��ǰҩƷʵ��
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int GetItem()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return 1;

            if (this.SelectItem != null)
            {
                Item item = this.GetItem(this.neuSpread1_Sheet1.ActiveRowIndex);
                if (item == null)
                    return -1;
                this.SelectItem(item, System.EventArgs.Empty);

                this.Hide();
            }
            return 1;
        }

        /// <summary>
        /// ��ȡָ���е�Itemʵ��
        /// </summary>
        /// <param name="rowIndex">ָ��������</param>
        /// <returns>�ɹ�����Itemʵ�� ʧ�ܷ���null</returns>
        protected Item GetItem(int rowIndex)
        {
            try
            {
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                return itemManager.GetItem(this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��FarPoint��ȡʵ����Ϣ���и�ֵʱ�����쳣\n" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// ������Ӧ
        /// </summary>
        /// <param name="key"></param>
        public void Key(System.Windows.Forms.Keys key)
        {
            switch (key)
            {
                case Keys.Enter:
                    this.GetItem();
                    break;
                case Keys.Up:
                    this.PrivRow();
                    break;
                case Keys.Down:
                    this.NextRow();
                    break;
                case Keys.Escape:
                    this.Hide();
                    break;
            }
        }

        /// <summary>
        /// ѡ����һ��
        /// </summary>
        public void PrivRow()
        {
            this.neuSpread1_Sheet1.ActiveRowIndex--;
        }

        /// <summary>
        /// ѡ����һ��
        /// </summary>
        public void NextRow()
        {
            this.neuSpread1_Sheet1.ActiveRowIndex++;
        }

        #endregion

        private void ucPhaItem_Load(object sender, EventArgs e)
        {
            string strErr = "";
            ArrayList al = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("Pha", "ItemSize", out strErr);
            if (al != null && al.Count > 1)
            {
                this.Height = FS.FrameWork.Function.NConvert.ToInt32(al[0]);
                this.Width = FS.FrameWork.Function.NConvert.ToInt32(al[1]);
            }
        }

        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.GetItem();
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader || e.ColumnHeader)
                return;

            this.GetItem();
        }

        private void pbHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, this.FilePath);
        }

    }
}
