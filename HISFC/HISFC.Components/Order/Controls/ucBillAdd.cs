using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// ִ�е�ά�����
    /// </summary>
    public partial class ucBillAdd : UserControl
    {
        private ArrayList arr = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper helper;
        private cResult r = new cResult();
        public ArrayList alExecBill = new ArrayList();
        public FS.FrameWork.Models.NeuObject objExecBill = new FS.FrameWork.Models.NeuObject();

        private FS.FrameWork.Models.NeuObject aExecBill = new FS.FrameWork.Models.NeuObject();
        private ArrayList alExecBillNames = new ArrayList();
        public ArrayList AlExecBillNames
        {
            get
            {
                return this.alExecBillNames;
            }
            set
            {
                alExecBillNames = value;
            }
        }

        public ucBillAdd()
        {
            InitializeComponent();
        }

        public ucBillAdd(cResult r)
            : this()
        {
            this.r = r;
        }

        public ucBillAdd(FS.FrameWork.Models.NeuObject aExecBill, ArrayList alExecBillNames)
            : this()
        {
            this.aExecBill = aExecBill;
            this.alExecBillNames = alExecBillNames;
        }

        /// <summary>
        /// �ж�����������Ƿ�null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsNull(FS.FrameWork.WinForms.Controls.NeuTextBox obj)
        {
            if (obj.Text.Trim() != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ı�����Ϊ�գ�"));
                return false;
            }
        }

        private void ucBillAdd_Load(object sender, EventArgs e)
        {
            helper = new FS.FrameWork.Public.ObjectHelper(this.alExecBill);
            arr = r.al;
            txtExecBillName.Text = r.Result1;
            string ID = r.Result2;
            if (ID.Trim() != "")
            {
                FS.FrameWork.Models.NeuObject obj = helper.GetObjectFromID(ID);
                txtExecBillName.Tag = r.Result2;
                this.txtMemo.Text = obj.User01;//ִ�е���ע
                if (obj.User02 != "")
                    this.cmbStyle.SelectedIndex = System.Convert.ToInt16(obj.User02);//ִ�е�����
            }
            else
            {
                txtExecBillName.Tag = null;
                this.cmbStyle.SelectedIndex = 1;
                this.txtMemo.Text = "";
            }
        }

        private void EventResultChanged(ArrayList al)
        {

        }

        private bool IsRepeat()
        {
            bool bRet = true;
            for (int i = 0; i < arr.Count; i++)
            {
                if (txtExecBillName.Text.Trim() == arr[i].ToString().Trim())
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ظ���"));
                    bRet = false;
                }
            }
            return bRet;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtExecBillName.Text.Trim() == "" || this.txtExecBillName.Text.Length == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ִ�е����Ʋ���Ϊ��"));
                return;
            }
            if (this.cmbStyle.Text.Length == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ִ�е������Ϊ��"));
                return;
            }

            FS.HISFC.BizLogic.Order.ExecBill oExecBill = new FS.HISFC.BizLogic.Order.ExecBill();
            if (txtExecBillName.Tag == null)
            {
                if (IsNull(txtExecBillName) && IsRepeat())
                {
                    this.objExecBill.Name = this.txtExecBillName.Text.Trim();
                    #region addby xuewj 2010-9-2 {46983F5B-E184-4b8b-B819-AA1C34993F1B} ��ҩ��ִ�е�����Ŀά��

                    if (this.chkItemBill.Checked)
                    {
                        this.objExecBill.Memo = "1";//��Ŀִ�е�
                    }
                    else
                    {
                        this.objExecBill.Memo = "0";//����Ŀִ�е�
                    }

                    #endregion
                    this.objExecBill.User01 = this.txtMemo.Text;//��ע
                    this.objExecBill.User02 = this.cmbStyle.SelectedIndex.ToString();//����
                    r.Result1 = txtExecBillName.Text;
                    #region addby xuewj 2010-9-2 {46983F5B-E184-4b8b-B819-AA1C34993F1B} ��ҩ��ִ�е�����Ŀά��

                    FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                    if (oExecBill.SetExecBill(this.objExecBill, empl.Nurse.ID) < 0)
                    {
                        MessageBox.Show("�����ִ�е�����:" + oExecBill.Err);
                        this.objExecBill = null;
                    }

                    #endregion
                    this.FindForm().Close();
                }
            }
            else
            {
                string strId = txtExecBillName.Tag.ToString();
                if (txtMemo.Text != "")
                    txtMemo.Text = txtMemo.Text.Trim();
                if (oExecBill.UpdateExecBillName(strId, txtExecBillName.Text.Trim(), txtMemo.Text, this.cmbStyle.SelectedIndex.ToString()) != -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���³ɹ���"));
                    r.Result1 = txtExecBillName.Text;
                    this.FindForm().Close();
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ��"));
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

    }
}