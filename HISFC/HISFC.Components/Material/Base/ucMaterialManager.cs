using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using System.Collections;
namespace FS.HISFC.Components.Material.Base
{
    /// <summary>		
    /// ucMaterialManager��ժҪ˵����<br></br>
    /// [��������: ������Ϣά��]<br></br>
    /// [�� �� ��: �]<br></br>
    /// [����ʱ��: 2007-03-28<br></br>
    /// </summary>
    public partial class ucMaterialManager : UserControl
    {
        public ucMaterialManager()
        {
            InitializeComponent();
            this.Init();
        }

        #region ������

        /// <summary>
        /// �����ֵ����
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem magageMetItem = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ���ʿ�����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// ȡ�����б�
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ƴ����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Controler controler = new FS.HISFC.BizLogic.Manager.Controler();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.BizLogic.Material.Baseset baseInfo = new FS.HISFC.BizLogic.Material.Baseset();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.BizLogic.Material.ComCompany comCompany = new FS.HISFC.BizLogic.Material.ComCompany();

        #endregion

        #region �����

        /// <summary>
        /// �����ֵ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Material.MaterialItem item;

        /// <summary>
        /// ����¼�뱣��������ֵ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Material.MaterialItem originalItem;

        /// <summary>
        /// ��������
        /// </summary>
        private string inputType = "N";

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private string checkCtrl = "0";

        /// <summary>
        /// ��ǰ���ʿ�Ŀ����
        /// </summary>
        private string matKind = "";

        /// <summary>
        /// �س���ת˳��
        /// </summary>
        private System.Collections.Hashtable hsJudgeOrder = new Hashtable();

        public delegate void SaveInput(FS.HISFC.Models.Material.MaterialItem item);

        public event SaveInput MyInput;

        public string storageCode;//liuxq add

        #endregion

        #region ����

        /// <summary>
        /// �������� Update/Insert/Check
        /// </summary>
        public string InputType
        {
            get
            {
                return this.inputType;
            }
            set
            {
                this.inputType = value;
                if (value.ToString().ToUpper() == "U")
                {
                    this.continueCheckBox.Enabled = false;
                }
                else if (value.ToUpper().ToUpper() == "I")
                {
                    this.continueCheckBox.Enabled = true;
                }
            }
        }

        /// <summary>
        /// ���ʿ�Ŀ�������
        /// </summary>
        public string MatKind
        {
            get
            {
                return this.matKind;
            }
            set
            {
                this.matKind = value;
            }
        }

        /// <summary>
        /// ������Ʒ�Ƿ���Ҫ�������
        /// </summary>
        public bool IsCheck
        {
            get
            {
                if (checkCtrl == "1")
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// �Ƿ���ֻ��״̬ �������޸�
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return this.btnSave.Visible;
            }
            set
            {
                this.btnSave.Visible = !value;
            }
        }

        /// <summary>
        /// �ؼ��ڲ�������Ʒʵ��
        /// </summary>
        public FS.HISFC.Models.Material.MaterialItem Item
        {
            get
            {
                this.GetItem();
                return this.item;
            }
            set
            {
                if (value == null)
                {
                    this.item = new FS.HISFC.Models.Material.MaterialItem();
                }
                else
                {
                    this.item = value;
                }

                this.originalItem = this.item.Clone();

                this.SetItem();
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �ӿؼ���ȡ����,����itemʵ��
        /// </summary>
        private void GetItem()
        {
            FS.FrameWork.Management.DataBaseManger data = new FS.FrameWork.Management.DataBaseManger();

            string operCode = ((FS.HISFC.Models.Base.Employee)data.Operator).ID;

            if (this.item == null)
            {
                this.item = new FS.HISFC.Models.Material.MaterialItem();
            }

            this.item.ID = this.txtItemId.Text;

            //if (this.inputType == "I")
            //{
            //    FS.HISFC.BizLogic.Material.MetItem item = new FS.HISFC.BizLogic.Material.MetItem();

            //    try
            //    {
            //        this.item.ID = item.GetMaxItemID(this.cmbKind.Tag.ToString());
            //        this.txtItemId.Text = this.item.ID;
            //    }
            //    catch { }
            //}
            this.item.Name = this.txtName.Text;
            this.item.SpellCode = this.txtSpellCode.Text;
            this.item.WBCode = this.txtWbCode.Text;
            this.item.UserCode = this.txtUserCode.Text;
            this.item.GbCode = this.txtGbCode.Text;
            this.item.MaterialKind.ID = this.cmbKind.Tag.ToString();
            this.item.Specs = this.txtSpec.Text;
            this.item.MinUnit = this.cmbUnit.Text.ToString();
            this.item.UnitPrice = NConvert.ToDecimal(txtPrice.Text);
            this.item.ApproveInfo = this.txtApprove.Text;
            if (this.cmbMet.Tag != null)
            {
                this.item.Compare.ID = this.cmbMet.Tag.ToString();
                this.item.Compare.Name = this.cmbMet.Text;
            }
            if (this.cmbUnDrug.Tag != null)
            {
                if (this.cmbUnDrug.Tag.ToString() == "None")
                {
                    this.item.UndrugInfo.ID = "";
                    this.item.UndrugInfo.Name = "";
                }
                else
                {
                    this.item.UndrugInfo.ID = this.cmbUnDrug.Tag.ToString();
                    this.item.UndrugInfo.Name = this.cmbUnDrug.Text;
                }
            }
            this.item.ValidState = !this.ckStop.Checked;
            this.item.SpecialFlag = FS.FrameWork.Function.NConvert.ToInt32(this.ckSpecial.Checked).ToString();

            this.item.Factory.ID = this.cmbFactory.Tag.ToString();
            this.item.Company.ID = this.cmbCompany.Tag.ToString();
            this.item.MinFee.ID = this.cmbFeeKind.Tag.ToString();
            this.item.StatInfo.ID = this.cmbStatCode.Tag.ToString();
            this.item.InSource = this.txtSource.Text;
            this.item.Usage = this.txtUse.Text;
            this.item.PackUnit = this.cmbPackUnit.Text.ToString();
            this.item.PackQty = NConvert.ToDecimal(txtPackNum.Text);
            this.item.PackPrice = this.item.UnitPrice * this.item.PackQty;
            this.item.StorageInfo.ID = this.txtStorage.Text;
            this.item.Oper.ID = operCode;
            this.item.OperTime = Convert.ToDateTime(DateTime.Now.ToString("f"));
            this.item.FinanceState = this.ckFinance.Checked;
            this.item.Mader = this.txtMader.Text;
            this.item.ZCH = this.txtZCH.Text;
            this.item.SpeType = this.cmbSpeType.Text;
            this.item.ZCDate = this.dtZC.Value;
            this.item.OverDate = this.dtOver.Value;

        }

        /// <summary>
        /// ��itemʵ����ȡ����,����ؼ�
        /// </summary>
        private void SetItem()
        {
            FS.FrameWork.Management.DataBaseManger data = new FS.FrameWork.Management.DataBaseManger();

            string operName = ((FS.HISFC.Models.Base.Employee)data.Operator).Name;

            this.txtItemId.Text = this.item.ID;
            this.txtName.Text = this.item.Name;
            this.txtSpellCode.Text = this.item.SpellCode;
            this.txtWbCode.Text = this.item.WBCode;
            this.txtUserCode.Text = this.item.UserCode;
            this.txtGbCode.Text = this.item.GbCode;
            this.cmbKind.Tag = this.item.MaterialKind.ID;
            
            if (this.inputType == "I")
            {
                //{1349F10A-8E5D-4fba-8EDE-D6B09A6F88A7}���ڵ㲻������������ ���������ظ�
                //this.cmbKind.Tag = this.item.MaterialKind.ID;
                if (this.cmbKind.Tag.ToString() == "0")
                {
                    this.cmbKind.Tag = "";
                }
                //----------------
                this.ckStop.Checked = false;
                this.ckFinance.Checked = false;
            }
            else
            {
                this.ckStop.Checked = !this.item.ValidState;
                this.ckFinance.Checked = this.item.FinanceState;

            }
            this.txtSpec.Text = this.item.Specs;
            this.cmbUnit.Text = this.item.MinUnit;
            this.txtPrice.Text = this.item.UnitPrice.ToString();
            this.txtApprove.Text = this.item.ApproveInfo;

            this.cmbMet.Tag = this.item.Compare.ID;
            if (this.cmbUnDrug.Tag.ToString() != "None")
            {
                this.cmbUnDrug.Tag = this.item.UndrugInfo.ID;
            }
            else
            {
                this.item.UndrugInfo.ID = "";
            }

            this.ckSpecial.Checked = FS.FrameWork.Function.NConvert.ToBoolean(this.item.SpecialFlag);
            this.cmbFactory.Tag = this.item.Factory.ID;
            this.cmbCompany.Tag = this.item.Company.ID;
            this.cmbFeeKind.Tag = this.item.MinFee.ID;
            this.cmbStatCode.Tag = this.item.StatInfo.ID;
            this.txtSource.Text = this.item.InSource;
            this.txtUse.Text = this.item.Usage;
            this.cmbPackUnit.Text = this.item.PackUnit;
            this.txtPackNum.Text = this.item.PackQty.ToString();

            this.txtStorage.Text = this.item.StorageInfo.ID;//this.storageCode;		

            this.txtMader.Text = this.item.Mader;
            this.txtZCH.Text = this.item.ZCH;
            this.cmbSpeType.Text = this.item.SpeType;
            if (this.item.OverDate.ToString() == "0001-1-1 0:00:00")
            {
                this.item.OverDate = System.DateTime.Now;
            }
            this.dtOver.Value = this.item.OverDate;

            if (this.item.ZCDate.ToString() == "0001-1-1 0:00:00")
            {
                this.item.ZCDate = System.DateTime.Now;
            }
            this.dtZC.Value = this.item.ZCDate;

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>�ɹ�: ����1,ʧ��: ���� -1</returns>
        public int Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            magageMetItem.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int parm = 1;

            FS.HISFC.Models.Material.MaterialItem matItem = new FS.HISFC.Models.Material.MaterialItem();

            matItem = this.Item;

            #region ����������Ч��״̬
            if (this.InputType == "U")
            {
                this.storeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                string errMsg = "";

                if (this.SetMatVaild(matItem, out errMsg) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errMsg);
                    return -1;
                }
            }
            #endregion

            switch (this.InputType)
            {
                case "U":
                    parm = magageMetItem.UpdateMetItem(matItem);
                    break;
                case "I":
                    matItem.ID = magageMetItem.GetMaxItemID(this.cmbKind.Tag.ToString());
                    parm = magageMetItem.InsertMetItem(matItem);
                    break;
                case "N":
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
            }

            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.magageMetItem.Err);
                return -1;
            }
            else
            {

                FS.FrameWork.Management.PublicTrans.Commit();

                if (this.inputType == "I")
                {
                    this.MyInput(matItem);
                }

                MessageBox.Show("����ɹ���", "��ʾ");

                return 1;
            }
        }

        /// <summary>
        /// ������Ʒ����Ϊ��Ч״̬������log_mat_stock��log_mat_stockinfo����Ч��־�ֶ�
        /// </summary>
        /// <param name="matItem">������Ŀʵ��</param>
        /// <returns>1:�ɹ���-1:ʧ��</returns>
        private int SetMatVaild(FS.HISFC.Models.Material.MaterialItem matItem, out string errMsg)
        {
            errMsg = "";
            FS.HISFC.Models.Material.MaterialItem oldMatItem = this.magageMetItem.GetMetItemByMetID(matItem.ID);
            if (oldMatItem == null)
            {
                errMsg = "��ȡԭ������Ϣʧ��" + this.magageMetItem.Err;
                return -1;
            }
            if ((oldMatItem.ValidState && matItem.ValidState) || (!oldMatItem.ValidState && !matItem.ValidState))
            {
                return 1;
            }
            if (this.storeManager.SetMatVaild(matItem.ID, matItem.ValidState) == -1)
            {
                errMsg = "���Ŀ��������Ч״̬ʧ��" + this.storeManager.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        public void Init()
        {
            try
            {
                this.cmbPackUnit.AddItems(constant.GetList("WZPACKUNIT"));
                this.cmbUnit.AddItems(constant.GetList("WZUNIT"));
                this.cmbFeeKind.AddItems(constant.GetList(FS.HISFC.Models.Base.EnumConstant.MINFEE));
                this.cmbFactory.AddItems(comCompany.QueryCompany("0", "A"));
                this.cmbCompany.AddItems(comCompany.QueryCompany("1", "A"));
                this.cmbKind.AddItems(baseInfo.QueryKind());
                this.btnSave.Visible = true;

                FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                List<FS.HISFC.Models.Fee.Item.Undrug> feeItemCollection = feeIntegrate.QueryAllItemsList();
                FS.HISFC.Models.Fee.Item.Undrug info = new FS.HISFC.Models.Fee.Item.Undrug();
                info.ID = "None";
                info.Name = "ȡ������";
                feeItemCollection.Insert(0, info);

                this.cmbUnDrug.AddItems(new ArrayList(feeItemCollection.ToArray()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʼ��������Ŀά������ʧ�ܣ�" + ex.Message);
                return;
            }

        }

        /// <summary>
        /// ���ƿؼ���ֻ��
        /// </summary>
        /// <param name="flag"></param>
        public void ReadOnlySp(bool flag)
        {
        }

        /// <summary>
        /// ���������Ч��
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (this.inputType == "I" && this.ckStop.Checked)
            {
                MessageBox.Show("����������Ч״̬��������Ŀ!��ȡ��ѡ��ͣ�á���ѡ��");
                return false;
            }
            if (this.txtName.TextLength == 0)
            {
                MessageBox.Show("���Ʋ���Ϊ��!");
                this.txtName.Focus();
                return false;
            }
            if (this.txtSpellCode.TextLength == 0)
            {
                MessageBox.Show("ƴ������Ϊ��!");
                this.txtSpellCode.Focus();
                return false;
            }
            if (this.cmbKind.Text == "" || this.cmbKind.Text == null)
            {
                MessageBox.Show("��Ŀ�����Ϊ��!");
                this.cmbKind.Focus();
                return false;
            }
            if (this.txtPrice.Text == "" || this.txtPrice.Text == null)
            {
                MessageBox.Show("���۲��ܿ�!");
                this.txtPrice.Focus();
                return false;
            }
            if (this.txtSpec.TextLength == 0)
            {
                MessageBox.Show("�����Ϊ��!");
                this.txtSpec.Focus();
                return false;
            }
            //			if (this.txtPackNum.TextLength == 0||this.txtPackNum.Text.Trim() == "0") 
            //			{
            //				MessageBox.Show("��װ��������Ϊ�ջ���0!");
            //				this.txtPackNum.Focus();
            //				return false;
            //			}
            //			if (this.cmbPackUnit.Text == "" || this.cmbPackUnit.Text == null) 
            //			{
            //				MessageBox.Show("��װ��λ����Ϊ��!");
            //				this.cmbPackUnit.Focus();
            //				return false;
            //			}
            if (this.cmbUnit.Text == "" || this.cmbUnit.Text == null)
            {
                MessageBox.Show("��С��λ����Ϊ��!");
                this.cmbUnit.Focus();
                return false;
            }
            if (this.txtUserCode.Text.Length == 0)
            {
                MessageBox.Show("�Զ�����벻��Ϊ��!");
                this.txtUserCode.Focus();
                return false;
            }
            if (this.ckFinance.Checked)
            {
                if (this.cmbFeeKind.Tag.ToString() == "")
                {
                    MessageBox.Show("�����շ���Ŀ������д�������!");
                    this.cmbFeeKind.Focus();
                    return false;
                }
            }
            //			if (this.txtPackPrice.Text.Length == 0||this.txtPackPrice.Text.Trim() == "0") 
            //			{
            //				MessageBox.Show("���װ�۸���Ϊ�ջ���0!");
            //				this.txtPackPrice.Focus();
            //				return false;
            //			}		

            System.Decimal decPrice;

            decPrice = FS.FrameWork.Function.NConvert.ToDecimal(txtPrice.Text.Trim()) * FS.FrameWork.Function.NConvert.ToDecimal(txtPackNum.Text.Trim());

            //			if (decPrice != FS.FrameWork.Function.NConvert.ToDecimal(txtPackPrice.Text.Trim()))
            //			{
            //				MessageBox.Show("���� �� ��װ���� �� ��װ�۸����!");
            //				return false;
            //			}
            return true;
        }

        /// <summary>
        /// ��տؼ�
        /// </summary>
        public void Reset()
        {
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                            continue;
                        if (crl.GetType() != typeof(System.Windows.Forms.Label) && crl.GetType() != typeof(System.Windows.Forms.CheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                        }
                    }
                }
            }

            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() != typeof(System.Windows.Forms.Label) && crl.GetType() != typeof(System.Windows.Forms.CheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                        }
                    }
                }
            }

            this.item = null;
            //this.cmbKind.Tag = this.item.MaterialKind.ID;

            this.ckFinance.Checked = false;
            this.ckStop.Checked = false;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    if (keyData == Keys.Enter)
        //    {
        //        SendKeys.Send("{TAB}");
        //    }

        //    return base.ProcessDialogKey(keyData);
        //}

        private void ucMaterialManager_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //���������Ч��
            if (!this.IsValid()) return;

            //����
            if (this.Save() == -1) return;


            switch (this.InputType)
            {
                case "U":
                    this.InputType = "N";
                    this.FindForm().Close();
                    break;
                case "I":

                    this.Reset();

                    if (this.continueCheckBox.Checked)
                    {
                        //						this.MyInput(this.Item);
                        this.InputType = "I";
                        this.Item = this.originalItem;
                        //{311DF45B-025A-4fac-A8FD-1B74AFFE4933}
                        this.txtName.Focus();
                    }
                    else
                    {
                        this.InputType = "N";
                        this.FindForm().Close();
                    }
                    break;
            }
        }

        /// <summary>
        /// ȡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.FindForm().Close();
        }

        /// <summary>
        /// �س��Զ�����ƴ���롢�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();

                spCode = (FS.HISFC.Models.Base.Spell)mySpell.Get(this.txtName.Text.Trim());
                this.txtSpellCode.Text = spCode.SpellCode;
                this.txtWbCode.Text = spCode.WBCode;
            }

        }

        private void txtPrice_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtPackNum.Text = "1";
            }
        }

        private void cmbUnit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtPackNum.Text = "1";
                this.cmbPackUnit.Tag = this.cmbUnit.Tag;
                this.cmbPackUnit.Text = this.cmbUnit.Text;
            }
        }

        private void neuLabel11_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
