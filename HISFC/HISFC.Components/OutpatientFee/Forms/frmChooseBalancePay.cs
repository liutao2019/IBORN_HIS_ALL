using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.OutpatientFee.Froms
{
    public partial class frmChooseBalancePay : Form
    {
        public frmChooseBalancePay()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ����֧����ʽ��Ϣ
        /// </summary>
        private ArrayList payModes = new ArrayList();
        /// <summary>
        /// ����֧����ʽ��Ϣ
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// Ҫ���ʵ�֧����ʽ�б�
        /// </summary>
        private ArrayList quitPayModes = new ArrayList();
        /// <summary>
        /// �޸ĺ�ĳ���֧����ʽ
        /// </summary>
        private ArrayList modfiedPayModes = new ArrayList();
        /// <summary>
        /// ����֧����ʽѡ��
        /// </summary>
        FS.FrameWork.WinForms.Controls.PopUpListBox lbQuitPayMode = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        /// <summary>
        /// ���շ�֧����ʽѡ��
        /// </summary>
        FS.FrameWork.WinForms.Controls.PopUpListBox lbPayMode = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        /// <summary>
        /// ��ʾ���� 0 ������Ϣ 1 ���շ���Ϣ
        /// </summary>
        private string displayInfo;
        /// <summary>
        /// �Էѽ��
        /// </summary>
        private decimal ownCost;
        /// <summary>
        /// û����������Ľ��
        /// </summary>
        private decimal orgOwnCost;
        /// <summary>
        /// �Ƿ�ѡ��ȷ��
        /// </summary>
        public bool IsSelect = false;

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �Ƿ������˿�֧����ʽ-�Ƿ��Ѿ��ս���߸���
        /// by han-zf 2014-10-04
        /// </summary>
        public bool IsLockPayMode;

        #endregion

        #region ����
        /// <summary>
        /// �Էѽ��
        /// </summary>
        public decimal OwnCost
        {
            set
            {
                ownCost = value;
                orgOwnCost = value;
            }
        }
        /// <summary>
        /// ��ʾ���� 0 ������Ϣ 1 ���շ���Ϣ
        /// </summary>
        public string DisplayInfo
        {
            set
            {
                displayInfo = value;
                InitDisplayInfo();
            }
            get
            {
                return displayInfo;
            }
        }
        /// <summary>
        /// Ҫ���ʵ�֧����ʽ
        /// </summary>
        public ArrayList QuitPayModes
        {
            set
            {
                quitPayModes = value;
            }
            get
            {
                return quitPayModes;
            }
        }

        /// <summary>
        /// �޸ĺ�ĳ���֧����ʽ
        /// </summary>
        public ArrayList ModifiedPayModes
        {
            get
            {
                return modfiedPayModes;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        public void Init()
        {
            //����FarPoint��һЩ�ȼ���Ϣ
            this.InitFp();
            //�������֧����ʽ
            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            //payModes = FS.HISFC.Models.Fee.EnumPayTypeService.List();
            payModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            string socialCard = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.SOCIAL_CARD_DISPLAY, "0");

            if (socialCard != "1")
            {
                FS.FrameWork.Models.NeuObject t = null;

                foreach (FS.FrameWork.Models.NeuObject transType in payModes)
                {
                    if (transType.Name == "�籣��")
                    {
                        t = transType;
                        break;
                    }
                }

                if (t != null)
                {
                    payModes.Remove(t);
                }
            }
            //�����Բ�ѯ
            helpPayMode.ArrayObject = payModes;
        }
        /// <summary>
        /// ��ʾ���� ������Ϣ ���շ���Ϣ
        /// </summary>
        public void InitDisplayInfo()
        {
            //ֻ��ʾ������Ϣ
            if (displayInfo == "0")
            {

                this.fpSpead1.Enabled = true;
            }
            else//ֻ��ʾ���շ���Ϣ
            {
                this.fpSpead1.Enabled = false;

            }
        }

        /// <summary>
        /// ��ʼ������֧����ʽ�б�
        /// </summary>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int InitQuitPayModes()
        {
            if (quitPayModes == null)
            {
                return -1;
            }

            lbQuitPayMode.AddItems(payModes);
            Controls.Add(lbQuitPayMode);
            lbQuitPayMode.Hide();
            lbQuitPayMode.BorderStyle = BorderStyle.FixedSingle;
            lbQuitPayMode.BringToFront();
            lbQuitPayMode.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbQuitPayMode_SelectItem);

            BalancePay pTempMode = null;

            this.fpSpead1_Sheet1.RowCount = quitPayModes.Count;

            for (int i = 0; i < quitPayModes.Count; i++)
            {
                pTempMode = quitPayModes[i] as BalancePay;
                this.fpSpead1_Sheet1.Cells[i, 0].Text = helpPayMode.GetName(pTempMode.PayType.ID.ToString());
                this.fpSpead1_Sheet1.Cells[i, 2].Text = helpPayMode.GetName(pTempMode.PayType.ID.ToString());
                //�ֽ�����ֽ����,�������޸ĳ�����֧����ʽ
                //if (pTempMode.PayType.ID.ToString() == "CA")
                //{
                //    this.fpSpead1_Sheet1.Cells[i, 2].Locked = true;
                //}
                this.fpSpead1_Sheet1.Cells[i, 1].Text = pTempMode.FT.RealCost.ToString();

                this.fpSpead1_Sheet1.Rows[i].Tag = pTempMode;

                this.fpSpead1_Sheet1.Cells[i, 0].Locked = true;
                this.fpSpead1_Sheet1.Cells[i, 1].Locked = true;

                //�ս���߸���֮��ԭ֧����ʽΪ��UP[������],MCZH[��ᱣ�Ͽ�(�麣)],MCZS[��ᱣ�Ͽ�(��ɽ)]��GZ[��ᱣ�Ͽ�(����)]ֻ�����ֽ�
                if (this.IsLockPayMode)
                {
                    //{4690DECD-6AB9-42d7-8415-3E788907C46D}
                    //if (pTempMode.PayType.ID == "UP" || pTempMode.PayType.ID == "MCZH" || pTempMode.PayType.ID == "MCZS" || pTempMode.PayType.ID == "GZ")
                    if (pTempMode.PayType.ID == "MCZH" || pTempMode.PayType.ID == "MCZS" || pTempMode.PayType.ID == "GZ")
                    {
                        this.fpSpead1_Sheet1.Cells[i, 2].Text = helpPayMode.GetName("CA");
                        this.fpSpead1_Sheet1.Cells[i, 2].Locked = true;
                    }
                }

                 //CD,DC,DE,PD,PR,PY,PYZZ,RC,YS����֧����ʽ����ѡ�񷵻���ʽ
                if(pTempMode.PayType.ID == "CD" || pTempMode.PayType.ID == "DC" || pTempMode.PayType.ID == "DE" ||
                    pTempMode.PayType.ID == "PD" || pTempMode.PayType.ID == "PR" || pTempMode.PayType.ID == "PY" ||
                    pTempMode.PayType.ID == "PYZZ" || pTempMode.PayType.ID == "RC" || pTempMode.PayType.ID == "YS" ||
                    pTempMode.PayType.ID == "CO" || pTempMode.PayType.ID == "TCO") //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                {
                    this.fpSpead1_Sheet1.Cells[i, 2].Locked = true;
                }

            }
            this.fpSpead1.Focus();

            //by han-zf 2014-10-04
            //for (int i = 0; i < this.fpSpead1_Sheet1.RowCount; i++)
            //{
            //    string a = this.fpSpead1_Sheet1.Cells[i, 2].Text;
            //    this.fpSpead1_Sheet1.Cells[i, 2].Locked = this.IsLockPayMode;
            //}

            //�ҵ���һ�������޸ĵ�֧����ʽ,��ý���
            for (int i = 0; i < quitPayModes.Count; i++)
            {
                if (this.fpSpead1_Sheet1.Cells[i, 2].Locked == false)
                {
                    this.fpSpead1_Sheet1.SetActiveCell(i, 2, false);
                    return 1;
                }
            }

            
            return 1;
        }

        /// <summary>
        /// ����ѡ��֧����ʽListBox��λ��
        /// </summary>
        private void SetLocation()
        {
            if (this.fpSpead1_Sheet1.ActiveColumnIndex == 2)
            {
                Control cell = this.fpSpead1.EditingControl;
                lbQuitPayMode.Location = new Point(this.fpSpead1.Location.X + cell.Location.X + 4,
                    this.groupBox1.Location.Y + this.fpSpead1.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbQuitPayMode.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
            }

        }

        /// <summary>
        /// �����˷ѵĳ���֧����ʽѡ��
        /// </summary>
        /// <returns> -1 ���� 1 ��ȷ</returns>
        private int ProcessQuitPayMode()
        {
            int currRow = this.fpSpead1_Sheet1.ActiveRowIndex;
            if (currRow < 0)
            {
                return 0;
            }
            FS.FrameWork.Models.NeuObject item = null;
            int iReturn = lbQuitPayMode.GetSelectedItem(out item);
            if (iReturn == -1)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }
            fpSpead1_Sheet1.SetValue(currRow, 2, item.Name);

            fpSpead1.StopCellEditing();

            this.lbQuitPayMode.Visible = false;

            return 1;
        }


        private bool isValid()
        {
            for (int i = 0; i < this.fpSpead1_Sheet1.RowCount; i++)
            {
                if (this.fpSpead1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpead1_Sheet1.Rows[i].Tag is BalancePay)
                    {
                        string tmpText = this.fpSpead1_Sheet1.Cells[i, 2].Text;
                        if (tmpText == null || tmpText == "")
                        {
                            MessageBox.Show("����д����֧����ʽ!");
                            this.fpSpead1_Sheet1.SetActiveCell(i, 2, false);
                            return false;
                        }
                        string tmpId = helpPayMode.GetID(tmpText);
                        if (tmpId == null || tmpId == "")
                        {
                            MessageBox.Show("֧����ʽ��д���Ϸ�!��������д!");
                            MessageBox.Show("����д����֧����ʽ!");
                            this.fpSpead1_Sheet1.SetActiveCell(i, 2, false);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// ��ø��ĺ��֧����ʽ
        /// </summary>
        public void GetNewPayMode()
        {
            this.modfiedPayModes = new ArrayList();
            for (int i = 0; i < this.fpSpead1_Sheet1.RowCount; i++)
            {
                if (this.fpSpead1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpead1_Sheet1.Rows[i].Tag is BalancePay)
                    {
                        BalancePay p = this.fpSpead1_Sheet1.Rows[i].Tag as BalancePay;
                        p.PayType.ID = helpPayMode.GetID(this.fpSpead1_Sheet1.Cells[i, 2].Text);
                        p.PayType.Name = helpPayMode.GetName(p.PayType.ID);
                        modfiedPayModes.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// ��ʼ��farpoint,����һЩ�ȼ�
        /// </summary>
        private void InitFp()
        {
            InputMap im;
            im = this.fpSpead1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpead1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpead1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpead1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpead1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (lbQuitPayMode.Visible)
                {
                    lbQuitPayMode.Visible = false;
                    this.fpSpead1.StopCellEditing();
                }
                else
                {
                    IsSelect = false;
                    this.Close();
                }

            }
            if (keyData == Keys.F4)
            {
                if (!this.isValid())
                {
                    return false;
                }
                IsSelect = true;
                this.GetNewPayMode();
                this.Close();
            }
            if (this.fpSpead1.ContainsFocus)
            {
                if (keyData == Keys.Up)
                {
                    if (lbQuitPayMode.Visible)
                    {
                        lbQuitPayMode.PriorRow();
                    }
                    else
                    {
                        int currRow = this.fpSpead1_Sheet1.ActiveRowIndex;
                        if (currRow > 0)
                        {
                            this.fpSpead1_Sheet1.ActiveRowIndex = currRow - 1;
                            this.fpSpead1_Sheet1.SetActiveCell(currRow - 1, 2);
                        }
                    }
                }
                if (keyData == Keys.Down)
                {
                    if (lbQuitPayMode.Visible)
                    {
                        lbQuitPayMode.NextRow();
                    }
                    else
                    {
                        int currRow = this.fpSpead1_Sheet1.ActiveRowIndex;

                        this.fpSpead1_Sheet1.ActiveRowIndex = currRow + 1;
                        this.fpSpead1_Sheet1.SetActiveCell(currRow + 1, 2);
                    }
                }
                if (keyData == Keys.Enter)
                {
                    int currRow = this.fpSpead1_Sheet1.ActiveRowIndex;
                    int currCol = this.fpSpead1_Sheet1.ActiveColumnIndex;
                    if (currCol == 2)
                    {
                        ProcessQuitPayMode();
                        this.fpSpead1_Sheet1.SetActiveCell(currRow, 2, false);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }


        private int lbQuitPayMode_SelectItem(Keys key)
        {
            ProcessQuitPayMode();
            fpSpead1.Focus();
            return 0;
        }

        private void fpSpead1_EditModeOn(object sender, System.EventArgs e)
        {
            fpSpead1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            SetLocation();
            if (fpSpead1_Sheet1.ActiveColumnIndex != 2)
            {
                lbQuitPayMode.Visible = false;
            }

        }

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void fpSpead1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 2)
            {
                string text = fpSpead1_Sheet1.ActiveCell.Text;
                lbQuitPayMode.Filter(text);
                if (!lbQuitPayMode.Visible)
                {
                    lbQuitPayMode.Visible = true;
                }
            }

        }

        private void frmChoosePayMode_Load(object sender, System.EventArgs e)
        {
            try
            {

            }
            catch { }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (!this.isValid())
            {
                return;
            }
            IsSelect = true;
            this.GetNewPayMode();
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            IsSelect = false;
            this.Close();
        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            if (!this.isValid())
            {
                return;
            }
            IsSelect = true;
            this.GetNewPayMode();
            this.Close();
        }

    }
}