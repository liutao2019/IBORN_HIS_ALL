using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISFC.Components.Package.Fee.Controls
{
    public delegate int DelegateInvoicesSet(FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice);

    public partial class ucInvoiceTree : UserControl
    {

        #region 属性

        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetInvoiceTree();
            }
        }

        /// <summary>
        /// 当前会员卡信息
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCardInfo = null;

        /// <summary>
        /// 会员卡信息
        /// </summary>
        public FS.HISFC.Models.Account.AccountCard AccountCardInfo
        {
            get { return this.accountCardInfo; }
            set { this.accountCardInfo = value; }
        }
        
        #endregion

        #region

        /// <summary>
        /// 选择一张发票
        /// </summary>
        public event DelegateInvoicesSet SelectInvoice;

        #endregion

        #region 管理类

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 套餐管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
        
        /// <summary>
        /// 套餐管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice invoiceMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice();

        #endregion

        public ucInvoiceTree()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.dtpBegin.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.addEvents();
        }

        /// <summary>
        /// 事件添加
        /// </summary>
        private void addEvents()
        {
            this.txtCardNO.KeyDown += new KeyEventHandler(txtCardNO_KeyDown);
            this.trvInvoice.AfterSelect += new TreeViewEventHandler(trvInvoice_AfterSelect);
        }

        void trvInvoice_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e == null || e.Node == null)
                return;

            TreeNode selectedNode = e.Node as TreeNode;
            if (e.Node != null && selectedNode.Tag is FS.HISFC.Models.MedicalPackage.Fee.Invoice)
            {
                this.SelectInvoice(selectedNode.Tag as FS.HISFC.Models.MedicalPackage.Fee.Invoice);
            }
        }

        /// <summary>
        /// 事件删除
        /// </summary>
        private void delEvents()
        {
            this.txtCardNO.KeyDown -= new KeyEventHandler(txtCardNO_KeyDown);
        }

        /// <summary>
        /// {892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
        /// 通过读卡号来查询患者的发票信息
        /// </summary>
        /// <param name="MCardNO"></param>
        public void QueryByMCardNO(string MCardNO)
        {
            if (!string.IsNullOrEmpty(MCardNO))
            {
                this.txtCardNO.Text = MCardNO;
                this.txtCardNO_KeyDown(this.txtCardNO, new KeyEventArgs(Keys.Enter));
            }
        }

        /// <summary>
        /// 卡号回车检索患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string QueryStr = string.Empty;
                QueryStr = this.txtCardNO.Text; 

                //TextBox queryControl = sender as TextBox;
                //string QueryStr = queryControl.Text.Trim();
                if (QueryStr == string.Empty)
                {
                    MessageBox.Show("请输入就诊卡号！");
                    this.txtCardNO.Focus();
                    return;
                }

                #region 这个方法好繁琐，而且只能查找会员卡号Account_CARD，如果没有院内卡号的查不到信息，屏蔽了
                //bool isMarkNO = false;

                //if (string.IsNullOrEmpty(QueryStr))
                //{
                //    return;
                //}

                //if (QueryStr[0] == ';')
                //{
                //    isMarkNO = true;
                //    QueryStr = QueryStr.Substring(1, QueryStr.Length - 1);
                //}

                //if (string.IsNullOrEmpty(QueryStr))
                //{
                //    return;
                //}

                ////病历号默认进行补全
                //if (!isMarkNO)
                //{
                //    QueryStr = QueryStr.PadLeft(10, '0');
                //}

                //this.Clear();

                ////病历号查询
                //if (!isMarkNO)
                //{
                //    System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(QueryStr, "Card_No", "1");
                //    if (cardList == null || cardList.Count < 1)
                //    {
                //        MessageBox.Show("病历号不存在！");
                //        return;
                //    }
                //    this.AccountCardInfo = cardList[cardList.Count - 1];
                //}
                //else //卡号查询
                //{
                //    this.AccountCardInfo = accountMgr.GetAccountCard(QueryStr, "Account_CARD");
                //    if (this.AccountCardInfo == null || (this.AccountCardInfo.MarkStatus != FS.HISFC.Models.Account.MarkOperateTypes.Begin))
                //    {
                //        MessageBox.Show("卡号不存在或者已经作废！");
                //        return;
                //    }
                //}
                #endregion
                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                int resultValue = accountMgr.GetCardByRule(QueryStr, ref accountCard);
                if (resultValue <= 0)
                {
                    MessageBox.Show(accountMgr.Err);
                    this.Clear();
                    return;
                }
                this.AccountCardInfo = accountCard;
                this.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.AccountCardInfo.Patient.PID.CardNO);

                if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
                {
                    MessageBox.Show("未查询到在患者！");
                    return;
                }
            }
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        public void Clear()
        {
            this.PatientInfo = null;
        }

        /// <summary>
        /// 设置发票树信息显示
        /// </summary>
        private void SetInvoiceTree()
        {
            this.trvInvoice.Nodes.Clear();

            if (this.PatientInfo == null)
            {
                return;
            }

            //查询指定时间有效的套餐记录
            ArrayList invoiceList = this.invoiceMgr.QueryInvoiceByCardNoDate(this.PatientInfo.PID.CardNO, this.dtpBegin.Value.Date, this.dtpEnd.Value.Date.AddDays(1), "0");

            if (invoiceList == null)
            {
                MessageBox.Show(packageMgr.Err);
                return;
            }

            if (invoiceList.Count == 0)
            {
                MessageBox.Show("该段时间内未找到套餐购买记录！");
                return;
            }

            TreeNode root = new TreeNode();
            root.Tag = this.PatientInfo;
            root.Text = this.PatientInfo.Name+"【"+this.PatientInfo.PID.CardNO+"】";

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice in invoiceList)
            {
                TreeNode invoiceNode = new TreeNode();
                invoiceNode.Name = invoice.ID;
                invoiceNode.Text = invoice.ID;
                invoiceNode.Tag = invoice;
                root.Nodes.Add(invoiceNode);
            }

            this.trvInvoice.Nodes.Add(root);
            this.trvInvoice.ExpandAll();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)// {D55B4DFA-DA91-42b0-8163-27036100E89E}
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {

                    this.txtCardNO.Text = frmQuery.PatientInfo.PID.CardNO;
                    this.txtCardNO.Focus();
                    TextBox queryControl = new TextBox();
                    queryControl.Text = this.txtCardNO.Text;
                    txtCardNO_KeyDown(queryControl, new KeyEventArgs(Keys.Enter));
                }
            }
        }
    }
}
