using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class frmShelf : Form
    {
        #region ������
        private ShelfSpec specTemp = new ShelfSpec();
        private ShelfSpecManage shelfManage = new ShelfSpecManage();
        private BoxSpecManage boxspecManage = new BoxSpecManage();
        private string Error;
        #endregion

        #region ˽�з���
        /// <summary>
        /// ��ҳ���ϻ�ȡҪ��ӵļ��ӹ����Ϣ
        /// </summary>
        /// <returns> ���ӹ��ʵ��</returns>
        private ShelfSpec ShelfSpecInfo()
        {
            string sequence ="";
            shelfManage.GetNextSequence(ref sequence);
            specTemp.ShelfSpecID = Convert.ToInt32(sequence);
            specTemp.ShelfSpecName = txtName.Text.ToString();
            specTemp.Row = Convert.ToInt32(nudRow.Value);
            specTemp.Col = 1;
            specTemp.Height = Convert.ToInt32(nudHeight.Value);
            specTemp.BoxSpec.BoxSpecID = Convert.ToInt32(cmbBoxSpec.SelectedValue);
            specTemp.Comment = txtComment.Text;
            return specTemp;
        }

        /// <summary>
        /// ������ӹ��
        /// </summary>
        /// <returns>-1������ʧ�ܣ��������ɹ�</returns>
        private int SaveShelfSpec()
        {
            try
            {
                ShelfSpecInfo();
                return shelfManage.InsertShelfSpec(specTemp);
            }
            catch (Exception e)
            {
                Error = e.Message;
                return -1;
            }

        }

        private bool DataValidte()
        {
            if (nudRow.Value <= 0)
            {
                return false;
            }
            if (nudHeight.Value <= 0)
                return false;
            if (cmbBoxSpec.SelectedValue == null || cmbBoxSpec.Text.Trim() == "")
                return false;
            return true;
 
        }
        #endregion

        #region ���з������¼�
        public frmShelf()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ȡ����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!DataValidte())
            {
                MessageBox.Show("�������ݲ��ϸ����飡", "����ܹ�����");
                return;
            }
            int result = SaveShelfSpec();
            if (result != -1)
            {
                MessageBox.Show("����ɹ�", "����ܹ�����");
            }
            else
            {
                MessageBox.Show("����ʧ��", "����ܹ�����");
            }
        }
        #endregion

        /// <summary>
        /// �󶨱걾�й��
        /// </summary>
        private void ComboxBinding()
        {
            Dictionary<int, string> dicSpec = boxspecManage.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = dicSpec;
            this.cmbBoxSpec.DisplayMember = "Value";
            this.cmbBoxSpec.ValueMember = "Key";
            this.cmbBoxSpec.DataSource = bsTemp;
        }

        private void frmShelf_Load(object sender, EventArgs e)
        {
            ComboxBinding();
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //��ѯ���������б�
            ArrayList iceBoxTypeList = new ArrayList();
            iceBoxTypeList = con.GetList("ICEBOXTYPE");            
            this.txtIceBoxType.AddItems(iceBoxTypeList);
        }

        private void cmbBoxSpec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtIceBoxType_TextChanged(object sender, EventArgs e)
        {
            if (txtIceBoxType.Tag == null || txtIceBoxType.Text == "")
                return;
            else
            {
                if (txtIceBoxType.Tag.ToString() == "1")
                {
                    txtName.Text = "��ʽ�䶳��ܹ��";
                    txtName.Enabled = true;
                    nudRow.Enabled = true;
                    nudHeight.Enabled = true;
                    btnSave.Enabled = true;
                    btnAdd.Enabled = true;
                }
                if (txtIceBoxType.Tag.ToString() == "2")
                {
                    txtName.Enabled = false;
                    nudRow.Enabled = false;
                    nudHeight.Enabled = false;
                    btnSave.Enabled = false;
                    btnAdd.Enabled = false;
                }
                if (txtIceBoxType.Tag.ToString() == "3")
                {
                    txtName.Text = "Һ�����䶳�ܹ��";
                    txtName.Enabled = true;
                    nudRow.Enabled = false;
                    nudRow.Value = 1;
                    nudHeight.Enabled = true;
                    btnSave.Enabled = true;
                    btnAdd.Enabled = true;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtIceBoxType.Text = "";
            txtIceBoxType.Tag = null; 
        }

    }
}