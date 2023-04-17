using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucBalancePay : UserControl
    {
        public ucBalancePay()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ʹ�÷�Χ Inpatient סԺ Outpatient ����
        /// </summary>
        private FS.HISFC.Models.Base.ServiceTypes serviceType = FS.HISFC.Models.Base.ServiceTypes.I;

        /// <summary>
        /// ֧����ʽ�б�
        /// </summary>
        private ArrayList payModes = new ArrayList();

        /// <summary>
        /// �����б�
        /// </summary>
        private ArrayList banks = new ArrayList();

        /// <summary>
        /// ֧����ʽѡ���б�
        /// </summary>
        FS.FrameWork.WinForms.Controls.PopUpListBox lbPayMode = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        FS.FrameWork.WinForms.Controls.PopUpListBox lbBank = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �����Էѽ��
        /// </summary>
        private decimal totOwnCost = 0;

        /// <summary>
        /// ʵ�ʻ��ѽ��
        /// </summary>
        private decimal realCost = 0;

        /// <summary>
        /// ��ǰ���ݿ�����
        /// </summary>
        protected System.Data.IDbTransaction trans = null;

        /// <summary>
        /// �Ƿ���ʾ��ť
        /// </summary>
        protected bool isShowButton = false;

        /// <summary>
        /// �Ƿ���ȷѡ����֧����ʽ,ǰ����IsShowButton����ΪTrue����رմ��ں͵��ȡ����ť,��ֵΪfalse
        /// </summary>
        protected bool isCurrentChoose = false;

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ���ݿ�����
        /// </summary>
        public System.Data.IDbTransaction Trans 
        {
            get 
            {
                return this.trans;
            }
            set 
            {
                this.trans = value;
            }
        }
        
        /// <summary>
        /// �����Էѽ��
        /// </summary>
        public decimal TotOwnCost 
        {
            get 
            {
                return this.totOwnCost;
            }
            set 
            {
                this.totOwnCost = value;
                this.fpPayMode_Sheet1.CellChanged -= new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
                this.fpPayMode_Sheet1.SetValue(0, (int)PayModeCols.Cost, this.totOwnCost);
                this.fpPayMode_Sheet1.CellChanged += new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
            }
        }

        /// <summary>
        /// ʵ�ʻ��ѽ��
        /// </summary>
        public decimal RealCost 
        {
            get 
            {
                return this.realCost;
            }
            set 
            {
                this.realCost = value;
            }
        }

        /// <summary>
        /// �Ƿ���ȷѡ����֧����ʽ,ǰ����IsShowButton����ΪTrue����رմ��ں͵��ȡ����ť,��ֵΪfalse
        /// </summary>
        public bool IsCurrentChoose 
        {
            get 
            {
                return this.isCurrentChoose;
            }
        }


        /// <summary>
        /// �Ƿ���ʾ��ť
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ʹ��ȷ����ȡ����ť")]
        public bool IsShowButton 
        {
            get 
            {
                return this.isShowButton;
            }
            set 
            {
                this.isShowButton = value;
                if (this.isShowButton)
                {
                    this.plButton.Height = 40;
                }
                else 
                {
                    this.plButton.Height = 0;
                }
            }
        }

        /// <summary>
        /// ʹ�÷�Χ Inpatient סԺ Outpatient ����
        /// </summary>
        [Category("�ؼ�����"), Description("ʹ�÷�Χ Inpatient סԺ Outpatient ����")]
        public FS.HISFC.Models.Base.ServiceTypes ServiceType 
        {
            get 
            {
                return this.serviceType;
            }
            set 
            {
                this.serviceType = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��farpoint,����һЩ�ȼ�
        /// </summary>
        private void InitFp()
        {
            InputMap im;
            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// ��ʼ��֧����ʽ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual  int InitPayMode()
        {
            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            //payModes = FS.HISFC.Models.Fee.EnumPayTypeService.List();
            payModes = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);

            ArrayList payModesClone = (ArrayList)payModes.Clone();

            //�ֽ�֧����ʽʵ��
            FS.FrameWork.Models.NeuObject objCA = null;

            foreach (FS.FrameWork.Models.NeuObject obj in payModesClone) 
            {
                if (obj.ID == "CA") 
                {
                    objCA = obj;
                    break;
                }
            }
          
            payModesClone.Remove(objCA);

            lbPayMode.AddItems(payModesClone);
            Controls.Add(lbPayMode);
            lbPayMode.Hide();
            lbPayMode.BorderStyle = BorderStyle.FixedSingle;
            lbPayMode.BringToFront();
            lbPayMode.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbPayMode_SelectItem);

            this.fpPayMode_Sheet1.Rows.Count = payModes.Count;

            this.fpPayMode_Sheet1.Rows.Add(0, 1);
            this.fpPayMode_Sheet1.SetValue(0, (int)PayModeCols.PayMode, objCA.Name);
            this.fpPayMode_Sheet1.Rows[0].Tag = objCA.ID;
            this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.PayMode].Locked = true;
            
            for (int i = 0; i < payModesClone.Count; i++) 
            {
                this.fpPayMode_Sheet1.SetValue(i + 1, (int)PayModeCols.PayMode, ((FS.FrameWork.Models.NeuObject)payModesClone[i]).Name);
                this.fpPayMode_Sheet1.Rows[i + 1].Tag = ((FS.FrameWork.Models.NeuObject)payModesClone[i]).ID;
                this.fpPayMode_Sheet1.Cells[i + 1, (int)PayModeCols.PayMode].Locked = true;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ��������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitBanks()
        {
            if (trans != null)
            {
                this.managerIntegrate.SetTrans(this.trans);
            }
            banks = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.BANK);
            if (banks == null || banks.Count <= 0)
            {
                MessageBox.Show(Language.Msg("��ȡ�����б�ʧ��!") + this.managerIntegrate.Err);

                return -1;
            }

            this.lbBank.AddItems(banks);
            Controls.Add(this.lbBank);
            lbBank.Hide();
            lbBank.BorderStyle = BorderStyle.FixedSingle;
            lbBank.BringToFront();
            lbBank.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbBank_SelectItem);
            
            return 1;
        }

        int lbBank_SelectItem(Keys key)
        {
            this.ProcessPayBank();
            this.fpPayMode.Focus();
            this.fpPayMode_Sheet1.SetActiveCell(fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Account, true);

            return 1;
        }

        /// <summary>
        /// ���õ����б�λ��
        /// </summary>
        private void SetLocation()
        {
            if (this.fpPayMode_Sheet1.ActiveColumnIndex == (int)PayModeCols.PayMode)
            {
                Control cell = this.fpPayMode.EditingControl;
                lbPayMode.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4, this.fpPayMode.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);   
                lbPayMode.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
                
            }
            if (this.fpPayMode_Sheet1.ActiveColumnIndex == (int)PayModeCols.Bank)
            {
                Control cell = this.fpPayMode.EditingControl;
                lbBank.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4,
                     + this.fpPayMode.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbBank.Size = new Size(cell.Width + 200 + SystemInformation.Border3DSize.Width * 2, 150);
            }
        }

        /// <summary>
        /// ֧����ʽ�Ļس�
        /// </summary>
        /// <returns></returns>
        private int ProcessPayMode()
        {
            int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
            if (currRow < 0)
            {
                return 0;
            }
            FS.FrameWork.Models.NeuObject item = null;
            int returnValue = lbPayMode.GetSelectedItem(out item);
            if (returnValue == -1)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }

            this.fpPayMode_Sheet1.SetValue(currRow, (int)PayModeCols.PayMode, item.Name);
            this.fpPayMode_Sheet1.Rows[currRow].Tag = item.ID;

            this.fpPayMode.StopCellEditing();

            decimal nowCost = 0;
            decimal currCost = 0;
            bool isOnlyCash = true;
            
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (i == 0)
                    {
                        continue;
                    }
                    
                    nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                }
            }

            currCost = NConvert.ToDecimal(this.totOwnCost) - nowCost;
            this.fpPayMode_Sheet1.SetValue(0, (int)PayModeCols.Cost, currCost);

            nowCost = 0;
            
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (this.fpPayMode_Sheet1.Rows[i].Tag.ToString() != "CA")
                    {
                        isOnlyCash = false;
                    }
                    if (i == currRow)
                    {
                        continue;
                    }

                    nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                }
            }
            if (isOnlyCash)
            {
                currCost = this.totOwnCost - nowCost;
            }
            else
            {
                currCost = this.realCost - nowCost;
            }

            this.fpPayMode_Sheet1.SetValue(currRow, (int)PayModeCols.Cost, currCost);

            this.lbPayMode.Visible = false;
            
            return 1;
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int ProcessPayBank()
        {
            if (lbBank.Visible == false)
            {
                return -1;
            }
            int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
            if (currRow < 0)
            {
                return 0;
            }
            FS.FrameWork.Models.NeuObject item = null;
            int returnValue = lbBank.GetSelectedItem(out item);
            if (returnValue == -1)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }

            this.fpPayMode.StopCellEditing();
            this.fpPayMode_Sheet1.SetValue(currRow, (int)PayModeCols.Bank, item.Name);
            this.fpPayMode_Sheet1.Cells[currRow, (int)PayModeCols.Bank].Tag = item.ID;
      
            this.lbBank.Visible = false;
            
            return 1;
        }

        /// <summary>
        /// �������Ƿ�Ϸ�
        /// </summary>
        /// <returns>True�Ϸ� False���Ϸ�</returns>
        private bool IsComputCostValid()
        {
            decimal tmpCost = 0;
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                tmpCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                if (tmpCost > NConvert.ToDecimal(this.totOwnCost))
                {
                    MessageBox.Show(Language.Msg("������ܴ��ڿɲ���Էѽ��!"));
                    this.fpPayMode.Focus();
                    this.fpPayMode_Sheet1.SetActiveCell(i, (int)PayModeCols.Cost, false);
                    
                    return false;
                }
            }

            return true;
        }

        int lbPayMode_SelectItem(Keys key)
        {
            this.ProcessPayMode();
            this.fpPayMode.Focus();
            this.fpPayMode_Sheet1.SetActiveCell(fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost, true);
            
            return 1;
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public virtual int Init()
        {
            //��ʼ��Farpoint
            this.InitFp();

            //��ʼ��֧����ʽ
            if (this.InitPayMode() == -1)
            {
                return -1;
            }

            //��ʼ��������Ϣ
            if (this.InitBanks() == -1)
            {
                return -1;
            }

            return 1;
        }
        
        /// <summary>
        /// ���õ�ǰ���ݿ�����
        /// </summary>
        /// <param name="trans">��ǰ���ݿ�����</param>
        public void SetTrans(System.Data.IDbTransaction trans) 
        {
            this.trans = trans;
        }

        /// <summary>
        /// ���֧����ʽ�б�
        /// </summary>
        /// <returns>�ɹ� ֧����ʽ�б� ʧ�� null</returns>
        public ArrayList QueryBalancePayList() 
        {
            ArrayList balancePayList = new ArrayList();
            FS.HISFC.Models.Fee.BalancePayBase balancePay = null;

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text == string.Empty)
                {
                    continue;
                }
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Text == string.Empty)
                {
                    continue;
                }
                if (NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) == 0)
                {
                    continue;
                }

                if (this.serviceType == FS.HISFC.Models.Base.ServiceTypes.I)
                {
                    balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                }
                else if (this.serviceType == FS.HISFC.Models.Base.ServiceTypes.C) 
                {
                    balancePay = new FS.HISFC.Models.Fee.Outpatient.BalancePay();
                }

                balancePay.PayType.Name = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                balancePay.PayType.ID = this.fpPayMode_Sheet1.Rows[i].Tag.ToString();

                if (balancePay.PayType.ID == null || balancePay.PayType.ID.ToString() == string.Empty)
                {
                    return null;
                }
                
                balancePay.FT.TotCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                balancePay.FT.RealCost = balancePay.FT.TotCost;
                balancePay.Bank.Name = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Bank].Text;
                if (balancePay.Bank.Name != null && balancePay.Bank.Name != string.Empty)
                {
                    balancePay.Bank.ID = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Bank].Tag.ToString();
                }
                balancePay.Bank.Account = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Account].Text;

                if (balancePay.PayType.Name == "֧Ʊ" || balancePay.PayType.Name == "��Ʊ")
                {
                    balancePay.Bank.InvoiceNO = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PosNo].Text;
                }
                else
                {
                    balancePay.POSNO = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PosNo].Text;
                }

                balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePay.RetrunOrSupplyFlag = "1";
                
                balancePayList.Add(balancePay);
            }

            return balancePayList;
        }

        #endregion

        #region �¼�

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (lbPayMode.Visible)
                {
                    lbPayMode.Visible = false;
                    this.fpPayMode.StopCellEditing();
                }
                else if (lbBank.Visible)
                {
                    lbBank.Visible = false;
                    this.fpPayMode.StopCellEditing();
                }
            }

            if (this.fpPayMode.ContainsFocus)
            {
                if (keyData == Keys.Up)
                {
                    if (lbPayMode.Visible)
                    {
                        lbPayMode.PriorRow();
                    }
                    else if (lbBank.Visible)
                    {
                        lbBank.PriorRow();
                    }
                    else
                    {
                        int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
                        if (currRow > 0)
                        {
                            this.fpPayMode_Sheet1.ActiveRowIndex = currRow - 1;
                            if (this.fpPayMode_Sheet1.Cells[currRow - 1, (int)PayModeCols.PayMode].Locked == true)
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow - 1, (int)PayModeCols.Cost);
                            }
                            else
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow - 1, (int)PayModeCols.PayMode);
                            }
                        }
                    }
                }
               
                if (keyData == Keys.Down)
                {
                    if (lbPayMode.Visible)
                    {
                        lbPayMode.NextRow();
                    }
                    else if (lbBank.Visible)
                    {
                        lbBank.NextRow();
                    }
                    else
                    {
                        int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
                        this.fpPayMode_Sheet1.ActiveRowIndex = currRow + 1;
                        if (this.fpPayMode_Sheet1.Cells[currRow + 1, (int)PayModeCols.PayMode].Locked == true)
                        {
                            this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Cost);
                        }
                        else
                        {
                            this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.PayMode);
                        }
                    }

                }
                if (keyData == Keys.Enter)
                {
                    int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
                    int currCol = this.fpPayMode_Sheet1.ActiveColumnIndex;
                    this.fpPayMode.StopCellEditing();
                    if (currCol == (int)PayModeCols.PayMode)
                    {
                        ProcessPayMode();
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Cost, false);

                    }
                    if (currCol == (int)PayModeCols.Cost)
                    {
                        decimal cost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[currRow, (int)PayModeCols.Cost].Value);
                        if (cost < 0)
                        {
                            MessageBox.Show("����С����");
                            this.fpPayMode.Focus();
                            this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Cost, false);
                            return false;
                        }
                        else
                        {
                            decimal tempOwnCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value);

                            if (!IsComputCostValid())
                            {
                                return false;
                            }
                            if (currRow == 0)//�ֽ�
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.PayMode, false);
                            }
                            else
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Bank, false);
                            }
                        }
                    }
                    if (currCol == (int)PayModeCols.Bank)
                    {
                        this.ProcessPayBank();
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Account, false);
                    }
                    if (currCol == (int)PayModeCols.Account)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Company, false);
                    }
                    if (currCol == (int)PayModeCols.Company)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.PosNo, false);
                    }
                    if (currCol == (int)PayModeCols.PosNo)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.PayMode, false);
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        private void fpPayMode_EditModeOn(object sender, EventArgs e)
        {
            this.SetLocation();
            if (fpPayMode_Sheet1.ActiveColumnIndex != (int)PayModeCols.PayMode)
            {
                lbPayMode.Visible = false;
            }
            if (fpPayMode_Sheet1.ActiveColumnIndex != (int)PayModeCols.Bank)
            {
                lbBank.Visible = false;
            }
        }

        private void fpPayMode_EditChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)PayModeCols.PayMode)
            {
                string text = fpPayMode_Sheet1.ActiveCell.Text;
                lbPayMode.Filter(text);
                if (!lbPayMode.Visible)
                {
                    lbPayMode.Visible = true;
                }
            }
            if (e.Column == (int)PayModeCols.Bank)
            {
                string text = fpPayMode_Sheet1.ActiveCell.Text;
                lbBank.Filter(text);
                if (!lbBank.Visible)
                {
                    lbBank.Visible = true;
                }
            }
        }

        private void fpPayMode_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            string tempString = this.fpPayMode_Sheet1.Cells[e.Row, (int)PayModeCols.PayMode].Text;
            if (tempString == string.Empty)
            {
                for (int i = 0; i < this.fpPayMode_Sheet1.Columns.Count; i++)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, i].Text = string.Empty;
                }
            }
            bool isOnlyCash = true;
            decimal nowCost = 0;
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (this.fpPayMode_Sheet1.Rows[i].Tag != null && this.fpPayMode_Sheet1.Rows[i].Tag.ToString() != "CA"
                        && NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) > 0)
                    {
                        isOnlyCash = false;
                        nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                    }
                }
            }

            if (this.realCost == 0)
            {
                this.realCost = this.totOwnCost;
            }

            if (isOnlyCash)
            {
                this.totOwnCost = FS.FrameWork.Public.String.FormatNumber(totOwnCost, 2);
                this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Text = totOwnCost.ToString();
            }
            else
            {
                if (realCost - nowCost < 0)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, (int)PayModeCols.Cost].Value = 0;
                    this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.Cost, false);
                    nowCost = 0;
                }
                this.totOwnCost = FS.FrameWork.Public.String.FormatNumber(realCost, 2);
                this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value = realCost - nowCost;
            }
        }

        #endregion

        #region ö��

        /// <summary>
        /// ʹ�÷�Χ
        /// </summary>
        public enum UsingAreas
        {
            /// <summary>
            /// סԺ
            /// </summary>
            Inpatient = 0,

            /// <summary>
            /// ����
            /// </summary>
            Outpatient
        }

        /// <summary>
        /// ֧����ʽ��ö��
        /// </summary>
        private enum PayModeCols
        {
            /// <summary>
            /// ֧����ʽ
            /// </summary>
            PayMode = 0,
            /// <summary>
            /// ���
            /// </summary>
            Cost = 1,
            /// <summary>
            /// ��������
            /// </summary>
            Bank = 2,
            /// <summary>
            /// �ʺ�
            /// </summary>
            Account = 3,
            /// <summary>
            /// ���ݵ�λ
            /// </summary>
            Company = 4,
            /// <summary>
            /// ֧Ʊ����Ʊ�����׺�
            /// </summary>
            PosNo = 5
        }

        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.isCurrentChoose = true;

            if (this.Parent is Form) 
            {
                this.ParentForm.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.isCurrentChoose = false;

            if (this.Parent is Form)
            {
                this.ParentForm.Close();
            }
        }

        
    }
}
