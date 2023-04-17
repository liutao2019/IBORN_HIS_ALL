using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.BizProcess.Interface.Account;

namespace FS.HISFC.Components.Account.Controls
{
    public partial class ucRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucRePrint()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ����ҵ���
        /// </summary>
        private HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// �����߼���
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountFeeManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// ����Ͽ�ʵ��
        /// </summary>
        private HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// �����ʻ�ҵ���
        /// </summary>
        private HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// �ۺ�ҵ���
        /// </summary>
        private HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ����

        /// <summary>
        ///����ʼ��ComBox
        /// </summary>
        private void InitCmb()
        {
            ArrayList al = new ArrayList();
            NeuObject tempObj = null;

            //added by yerl ���ڿ���ʱ��ӡ����ֽ����û�д�ӡ�ʻ�������,����û�еط��ܽ��в��򿪿���,�ڴ����
            tempObj = new NeuObject();
            tempObj.ID = ((int)FS.HISFC.Models.Account.OperTypes.NewAccount).ToString();
            tempObj.Name = "�½��ʻ�";
            al.Add(tempObj);

            tempObj = new NeuObject();
            tempObj.ID = ((int)FS.HISFC.Models.Account.OperTypes.StopAccount).ToString();
            tempObj.Name = "ͣ�ʻ�";
            al.Add(tempObj);

            tempObj = new NeuObject();
            tempObj.ID = ((int)FS.HISFC.Models.Account.OperTypes.AginAccount).ToString();
            tempObj.Name = "�����ʻ�";
            al.Add(tempObj);

            tempObj = new NeuObject();
            tempObj.ID = ((int)FS.HISFC.Models.Account.OperTypes.CancelAccount).ToString();
            tempObj.Name = "ע���ʻ�";
            al.Add(tempObj);

            tempObj = new NeuObject();
            tempObj.ID = ((int)FS.HISFC.Models.Account.OperTypes.EditPassWord).ToString();
            tempObj.Name = "�޸�����";
            al.Add(tempObj);

            tempObj = new NeuObject();
            tempObj.ID = ((int)FS.HISFC.Models.Account.OperTypes.BalanceVacancy).ToString();
            tempObj.Name = "�������";
            al.Add(tempObj);

            // ����ȡ�ִ�ӡ
            // {48314E1F-72EC-4044-A41A-833C84687A40}
            tempObj = new NeuObject();
            tempObj.ID = ((int)FS.HISFC.Models.Account.OperTypes.AccountTaken).ToString();
            tempObj.Name = "ȡ��";
            al.Add(tempObj);

            this.cmbOper.AddItems(al);
        }

        /// <summary>
        /// ���ݾ��￨�Ų��һ�����Ϣ
        /// </summary>
        private void GetPatientByMarkNO()
        {
            string markNO = this.txtMarkNO.Text.Trim();
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = feeIntegrate.ValidMarkNO(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(feeIntegrate.Err);
                this.txtMarkNO.Focus();
                this.txtMarkNO.SelectAll();
                return;
            }
            this.txtMarkNO.Tag = this.accountCard.Patient.PID.CardNO;
            this.txtMarkNO.Text = this.accountCard.MarkNO;
        }

        /// <summary>
        /// ���ݾ��￨�Ų��ҽ��׼�¼
        /// </summary>
        /// <param name="cardNO"></param>
        private void QueryOperRecord()
        {
            if (this.txtMarkNO.Tag==null)
            {
                MessageBox.Show("��������￨�ţ�");
                this.txtMarkNO.Focus();
                return;
            }
            if (this.cmbOper.Tag == null || this.cmbOper.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("��Ҫ�����Ʊ�����ͣ�");
                this.cmbOper.Focus();
                return;
            }
            string cardNO = this.txtMarkNO.Tag.ToString();
            int rowIndex = 0;
            int count = this.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, count);
            }
            string operType = this.cmbOper.Tag.ToString();
            if (operType.Equals("1"))
            {
                List<FS.HISFC.Models.Account.AccountCardFee> listCardFee= accountFeeManager.QueryCardFeebyMCardNo(txtMarkNO.Text);
                if(listCardFee==null||listCardFee.Count<1)
                {
                    MessageBox.Show("��������ʧ�ܣ�");
                    return;
                }
                foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in listCardFee)
                {
                    neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                    rowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = "���ʻ�";
                    this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = cardFee.Tot_cost.ToString();
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = "�Һ��շѴ�";
                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = cardFee.Oper.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = cardFee.Oper.OperTime.ToString();
                    this.neuSpread1_Sheet1.Rows[rowIndex].Tag = cardFee;
                }
            }
            else
            {
                List<HISFC.Models.Account.AccountRecord> list = accountManager.GetAccountRecordList(cardNO, operType);
                if (list == null||list.Count<1)
                {
                    MessageBox.Show("��������ʧ�ܣ�");
                    return;
                }
                foreach (HISFC.Models.Account.AccountRecord record in list)
                {
                    neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
                    rowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = record.OperType.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = (-record.BaseVacancy).ToString();
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = record.FeeDept.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = record.Oper.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = record.OperTime.ToString();
                    record.Patient = accountCard.Patient;
                    this.neuSpread1_Sheet1.Rows[rowIndex].Tag = record;
                }
            }
        }

        /// <summary>
        /// ��ӡ�ʻ�����Ʊ��
        /// </summary>
        /// <param name="tempaccountRecord"></param>
        private void PrintAccountOperRecipe(HISFC.Models.Account.AccountRecord tempaccountRecord)
        {
            IPrintOperRecipe Iprint = FS.FrameWork.WinForms.Classes.
            UtilInterface.CreateObject(this.GetType(), typeof(IPrintOperRecipe)) as IPrintOperRecipe;
            if (Iprint == null)
            {
                MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                return;
            }
            Iprint.SetValue(tempaccountRecord);
            Iprint.Print();
        }

        /// <summary>
        /// ��ӡ�������Ʊ��
        /// </summary>
        /// <param name="tempaccount"></param>
        private void PrintCancelVacancyRecipe(HISFC.Models.Account.AccountRecord tempaccountRecord)
        {
            IPrintCancelVacancy Iprint = FS.FrameWork.WinForms.Classes.
             UtilInterface.CreateObject(this.GetType(), typeof(IPrintCancelVacancy)) as IPrintCancelVacancy;
            if (Iprint == null)
            {
                MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                return;
            }
            tempaccountRecord.Memo = "1";
            Iprint.SetValue(tempaccountRecord);
            Iprint.Print();
        }
        #endregion

        #region �¼�
        private void ucRePrint_Load(object sender, EventArgs e)
        {
            InitCmb();
        }

        private void txtMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                GetPatientByMarkNO();
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            QueryOperRecord();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0) return -1;
            int rowIndex = this.neuSpread1_Sheet1.ActiveRowIndex;
            HISFC.Models.Account.AccountRecord record = this.neuSpread1_Sheet1.Rows[rowIndex].Tag as HISFC.Models.Account.AccountRecord;
            if (record != null)
            {
                // ����ȡ�ִ�ӡ
                // {48314E1F-72EC-4044-A41A-833C84687A40}
                if (record.OperType.ID.ToString() == ((int)HISFC.Models.Account.OperTypes.BalanceVacancy).ToString() ||
                    record.OperType.ID.ToString() == ((int)HISFC.Models.Account.OperTypes.AccountTaken).ToString())
                {
                    PrintCancelVacancyRecipe(record);
                }
                else
                {
                    this.PrintAccountOperRecipe(record);
                }
            }
            if (this.neuSpread1_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Account.AccountCardFee)
            {

                IPrintCardFee iPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<IPrintCardFee>(this.GetType());
                if (iPrint == null)
                {
                    MessageBox.Show("û��ά����ӡ");
                    return -1;
                }
                iPrint.SetValue(this.neuSpread1_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Account.AccountCardFee);
                iPrint.Print();
            }

            return base.OnPrint(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("ˢ��", "ˢ��", FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);

            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {

                case "ˢ��":
                    {
                        string McardNo = "";
                        string error = "";

                        if (Function.OperMCard(ref McardNo, ref error) < 0)
                        {
                            MessageBox.Show("����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���\n" + error);
                            return;
                        }
                        else
                        {
                            this.txtMarkNO.Text = McardNo;
                            this.txtMarkNO.Focus();
                            GetPatientByMarkNO();
                        }

                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get 
            {
                Type [] vType = new Type[2];
                vType[0] = typeof(IPrintOperRecipe);
                vType[1] = typeof(IPrintCancelVacancy);
                return vType;
            }
        }

        #endregion
    }
}
