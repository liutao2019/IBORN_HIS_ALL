using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Neusoft.FrameWork.Function;
using DiseasePay.Models;
using DiseasePay.BizLogic;

namespace FoShanDiseasePay
{
    /// <summary>
    /// ҩƷ/ҽ�úĲĵĲɹ�������Ϣ
    /// </summary>
    public partial class ucMaterialPay : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucMaterialPay()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[8].Visible = false;
            this.neuSpread1_Sheet1.Columns[9].Visible = false;
            this.neuSpread1_Sheet1.Columns[10].Visible = false;
            this.neuSpread1_Sheet1.Columns[11].Visible = false;
        }

        #region ˽���ֶ�
        private bool isDrug = false;

        [Category("����"), Description("�Ƿ�ҩƷ")]
        public bool IsDrug
        {
            get { return isDrug; }
            set { isDrug = value; }
        }

        private DiseasePay.BizLogic.BalanceManager payMgr = new DiseasePay.BizLogic.BalanceManager();

        /// <summary>
        /// ���ù�����
        /// </summary>
        //private Neusoft.HISFC.BizProcess.Material.Pay.payMgr payMgr = new Neusoft.HISFC.BizProcess.Material.Pay.payMgr();

        /// <summary>
        /// �ۺϹ�����
        /// </summary>
        //private Neusoft.HISFC.Interface.Material.InterfaceProxy.ManagerProxy managerProxy = new Neusoft.HISFC.Interface.Material.InterfaceProxy.ManagerProxy();

        /// <summary>
        /// ��ӡ�ؼ�����
        /// </summary>
        //private Neusoft.HISFC.Interface.Material.InterfaceProxy.BillPrintProxy printProxy = new Neusoft.HISFC.Interface.Material.InterfaceProxy.BillPrintProxy();

        /// <summary>
        /// ������λ
        /// </summary>
        private ArrayList alCompany = new ArrayList();

        /// <summary>
        /// �����ϸ��Ϣ
        /// </summary>
        //private ArrayList alPayDetail = new ArrayList();

        /// <summary>
        /// ��ѯ����־
        /// </summary>
        private string payFlag;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject privDept;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject privOper;

        /// <summary>
        /// ������
        /// </summary>
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        /// <summary>
        /// �����ϸ��Ϣ
        /// </summary>
        private Hashtable htPayDetail = new Hashtable();
        /// <summary>
        /// ��ӡ����ˮ����
        /// </summary>
        private string strPrintPayListNo = null;

        /// <summary>
        /// ����б�
        /// </summary>
        private List<DiseasePay.Models.BalanceHead> PayHeadList = new List<DiseasePay.Models.BalanceHead>();

        /// <summary>
        /// �������б�
        /// </summary>
        private List<Neusoft.FrameWork.Models.NeuObject> companyList = new List<Neusoft.FrameWork.Models.NeuObject>();

        /// <summary>
        /// ������˾����
        /// </summary>
        /// {38AE0936-69C9-4543-BB4E-78998C7CCE94}
        private Hashtable htCompanyName = new Hashtable();
        #endregion

        #region �̳з���

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }

            base.OnLoad(e);
        }

        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        protected override int OnSave(object sender, object NeuObject)
        {
            if (this.rbPay.Checked)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�ѽ����¼���ܽ��и���"));
                return 0;
            }
            if (this.SavePay() == 1)
            {
                this.Query();
            }
            return 1;
        }

        public override int Export(object sender, object NeuObject)
        {
            if (this.neuSpread2.Export() == 1)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�����ɹ�"));
            }
            return 1;
        }
        #endregion

        #region ˽�з���
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            string type = isDrug ? "1" : "0";
            companyList = payMgr.QueryCompany(type);
            if (companyList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ȡ��������Ϣ����" + payMgr.Err));
                return;
            }
            //{38AE0936-69C9-4543-BB4E-78998C7CCE94}
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "AAAA";
            obj.Name = Neusoft.FrameWork.Management.Language.Msg("ȫ��");
            companyList.Insert(0, obj);
            this.cmbCompany.AddItems(new ArrayList(companyList.ToArray()));
            //{38AE0936-69C9-4543-BB4E-78998C7CCE94}
            foreach (Neusoft.FrameWork.Models.NeuObject company in companyList)
            {
                if (!this.htCompanyName.Contains(company.ID))
                {
                    this.htCompanyName.Add(company.ID, company.Name);
                }
            }

            DateTime sysTime = payMgr.GetDateTimeFromSysDateTime().AddDays(1);
            this.dtpStartDate.Text = sysTime.AddDays(-30).ToString();
            this.dtpEndDate.Text = sysTime.ToString();

            this.payFlag = "'0','1'";

            //FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            //comboBoxCellType.Items = Enum.GetNames(typeof(EnumPayType));
            //this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].CellType = comboBoxCellType;

            #region ע�͵� ��Ҫ��
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            //cmbQueryCondition.Items.AddRange(Enum.GetNames(typeof(EnumSearchContion)));

            //cmbQueryCondition.Tag = EnumSearchContion.��Ʊ��.ToString();
            //cmbQueryCondition.SelectedItem.ID = EnumSearchContion.��Ʊ��.ToString();
            #endregion

            //{488136F1-77CC-4c26-973D-AF9C79467030}
            PayHeadList = this.payMgr.QueryPayList("");
            if (PayHeadList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ȡ�����Ϣʧ��" + this.payMgr.Err));
                return;
            }
            this.SetFpCellType();
        }

        /// <summary>
        /// ��������ѯ�����Ϣ
        /// </summary>
        private void Query()
        {
            //{488136F1-77CC-4c26-973D-AF9C79467030}
            PayHeadList = this.payMgr.QueryPayList("");
            if (PayHeadList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ȡ�����Ϣʧ��" + this.payMgr.Err));
                return;
            }

            if (this.cmbCompany.SelectedItem == null)
            {
                //MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ѡ�񹩻���˾"));
                return;
            }
            List<BalanceHead> payList = new List<BalanceHead>();
            Neusoft.FrameWork.Models.NeuObject currentCompany = this.cmbCompany.SelectedItem as Neusoft.FrameWork.Models.NeuObject;
            payList = this.payMgr.QueryPayList(this.privDept.ID, currentCompany.ID, this.payFlag, this.dtpStartDate.Value.Date, this.dtpEndDate.Value);
            if (payList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ȡ�����Ϣʧ��" + this.payMgr.Err));
                return;
            }

            //List<Input> inputList = inputMgr.QueryInputTotalForpay(this.privDept.ID, currentCompany.ID, this.dtpStartDate.Value.Date, this.dtpEndDate.Value);

            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;

            BalanceHead info = null;
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            //this.alPayDetail = new ArrayList();
            this.htPayDetail = new Hashtable();

            for (int i = 0; i < payList.Count; i++)
            {
                info = payList[i] as BalanceHead;
                if (info == null || info.PayHeadNo == "")
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�����") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("�н�������Ϣ����"));
                    continue;
                }
                List<BalanceHead> listTemp = this.payMgr.QueryPayDetail(info.PayHeadNo, info.InvoiceNo);
                if (listTemp == null)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ȡ��") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("�н����ϸ��Ϣ����") + this.payMgr.Err);
                    continue;
                }
                if (listTemp.Count > 0)
                {
                    //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
                    //this.alPayDetail.Add(listTemp);
                    htPayDetail.Add(info.PayHeadNo, listTemp);
                }
                this.AddPayHeadData(info);
            }
            this.SetStyle();
        }

        /// <summary>
        /// 
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        private void QueryInfo()
        {
            #region ע�͵� ��Ҫ��
            #region ��ò�ѯ����

            //string sqlWhere = string.Empty;
            //string codeCondition = string.Empty;
            //switch (this.cmbQueryCondition.Text)
            //{
            //    case "��Ʊ��":
            //        sqlWhere = "\nwhere t.invoice_no = '" + this.txtSearchCondition.Text + "'";
            //        break;
            //    case "��ⵥ��":
            //        sqlWhere = "\nwhere t.in_list_code = '" + this.txtSearchCondition.Text.PadLeft(10, '0').ToString() + "'";
            //        break;
            //    default:
            //        break;
            //}

            #endregion
            //List<PayHead> payList = new List<PayHead>();
            //payList = this.payMgr.QueryPayList(sqlWhere);
            //if (payList == null)
            //{
            //    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ȡ�����Ϣʧ��" + this.payMgr.Err));
            //    return;
            //}
            #endregion

            //{488136F1-77CC-4c26-973D-AF9C79467030}
            string codeCondition = string.Empty;
            List<BalanceHead> tempPayList = new List<BalanceHead>();
            //switch (this.cmbQueryCondition.Text)
            //{
            //case "��Ʊ��":
            //    codeCondition = this.txtSearchCondition.Text;
            //    foreach (PayHead tempPay in this.PayHeadList)
            //    {
            //        if (tempPay.InvoiceNo == codeCondition)
            //        {
            //            tempPayList.Add(tempPay);
            //        }
            //    }

            //    break;

            //case "��ⵥ��":
            //    codeCondition = this.txtSearchCondition.Text.PadLeft(10, '0').ToString();
            //    foreach (PayHead tempPay in this.PayHeadList)
            //    {
            //        if (tempPay.InListCode == codeCondition)
            //        {
            //            tempPayList.Add(tempPay);
            //        }
            //    }

            //    break;

            //    default:

            //        break;

            //}
            if (tempPayList.Count == 0)
            {
                return;
            }
            foreach (Neusoft.FrameWork.Models.NeuObject company in companyList)
            {
                if (company.ID == tempPayList[0].Company.ID)
                {
                    this.cmbCompany.Text = company.Name;
                    break;
                }
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;

            BalanceHead info = null;
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            //this.alPayDetail = new ArrayList();
            this.htPayDetail = new Hashtable();

            for (int i = 0; i < tempPayList.Count; i++)
            {
                info = tempPayList[i] as BalanceHead;
                if (info == null || info.PayHeadNo == "")
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�����") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("�н�������Ϣ����"));
                    continue;
                }
                List<BalanceHead> listTemp = this.payMgr.QueryPayDetail(info.PayHeadNo, info.InvoiceNo);
                if (listTemp == null)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��ȡ��") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("�н����ϸ��Ϣ����") + this.payMgr.Err);
                    continue;
                }
                if (listTemp.Count > 0)
                {
                    //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
                    //this.alPayDetail.Add(listTemp);
                    htPayDetail.Add(info.PayHeadNo, listTemp);
                }
                this.AddPayHeadData(info);
                if (info.PayState == "2")
                {
                    rbPay.Checked = true;
                }
                else
                {
                    rbUnPay.Checked = true;
                }
            }

        }

        /// <summary>
        /// ���������ϢFarPoint�ڼ�������
        /// </summary>
        private void AddPayHeadData(DiseasePay.Models.BalanceHead pay)
        {
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColChoose].Value = true;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColChoose].Locked = false;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceNo].Text = pay.InvoiceNo;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceDate].Value = pay.InvoiceTime;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceCost].Value = pay.PurchaseCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDiscountCost].Value = pay.DiscountCost;
            //Ӧ�����ͨ��FarPoint��ʽ�Զ�����
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPaidUpCost].Value = pay.PayCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCost].Value = pay.UnpayCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDeliveryCost].Value = pay.DeliveryCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayType].Value = pay.PayType;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDept].Value = this.privDept.Name;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInListCode].Value = pay.InListCode;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayType].BackColor = Color.SeaShell;
            //{38AE0936-69C9-4543-BB4E-78998C7CCE94}
            if (this.htCompanyName.Contains(pay.Company.ID))
            {
                Neusoft.FrameWork.Models.NeuObject currentCompany = this.cmbCompany.SelectedItem as Neusoft.FrameWork.Models.NeuObject;
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColCompanyName].Value = htCompanyName[pay.Company.ID].ToString();
                //{E5979899-1CA2-4ed1-A86C-B7E41031E044}
                //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].Value = currentCompany.OpenAccounts;
                //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].Value = currentCompany.OpenBank;
            }
            this.neuSpread1_Sheet1.Rows[rowCount].Tag = pay;
        }

        /// <summary>
        /// ���õ�Ԫ���ʽ
        /// </summary>
        private void SetFpCellType()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;// Function.MoneyDecimalPlaces;

            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPaidUpCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColInvoiceCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDue].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].CellType = numberCellType;

            this.neuSpread2_Sheet1.Columns[(int)ColPayDetailSet.ColDeliveryCost].CellType = numberCellType;
            this.neuSpread2_Sheet1.Columns[(int)ColPayDetailSet.ColPayCost].CellType = numberCellType;

            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = Neusoft.FrameWork.Management.Language.Msg("����");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = Neusoft.FrameWork.Management.Language.Msg("��Ʊ��");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = Neusoft.FrameWork.Management.Language.Msg("��Ʊ����");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = Neusoft.FrameWork.Management.Language.Msg("��Ʊ���");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = Neusoft.FrameWork.Management.Language.Msg("�Żݽ��");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = Neusoft.FrameWork.Management.Language.Msg("Ӧ�����");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = Neusoft.FrameWork.Management.Language.Msg("�Ѹ����");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = Neusoft.FrameWork.Management.Language.Msg("���θ���");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = Neusoft.FrameWork.Management.Language.Msg("�˷�");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = Neusoft.FrameWork.Management.Language.Msg("��������");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = Neusoft.FrameWork.Management.Language.Msg("��������");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = Neusoft.FrameWork.Management.Language.Msg("�����ʺ�");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = Neusoft.FrameWork.Management.Language.Msg("������");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = Neusoft.FrameWork.Management.Language.Msg("��ⵥ�ݺ�");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = Neusoft.FrameWork.Management.Language.Msg("����ƾ֤");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = Neusoft.FrameWork.Management.Language.Msg("δ����ƾ֤");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = Neusoft.FrameWork.Management.Language.Msg("δ����ƾ֤����");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = Neusoft.FrameWork.Management.Language.Msg("������");

            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = Neusoft.FrameWork.Management.Language.Msg("��Ʊ��");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = Neusoft.FrameWork.Management.Language.Msg("���θ���");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = Neusoft.FrameWork.Management.Language.Msg("�˷�");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = Neusoft.FrameWork.Management.Language.Msg("��������");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = Neusoft.FrameWork.Management.Language.Msg("��������");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = Neusoft.FrameWork.Management.Language.Msg("�����ʺ�");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = Neusoft.FrameWork.Management.Language.Msg("������");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = Neusoft.FrameWork.Management.Language.Msg("����ƾ֤");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = Neusoft.FrameWork.Management.Language.Msg("δ����ƾ֤");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = Neusoft.FrameWork.Management.Language.Msg("δ����ƾ֤����");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = Neusoft.FrameWork.Management.Language.Msg("��������");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = Neusoft.FrameWork.Management.Language.Msg("��ˮ����");
        }

        /// <summary>
        /// ������ϸFarPoint�ڼ�������
        /// </summary>
        /// <param name="al">�����̽��ʵ������</param>
        private void AddPayDetailData(List<BalanceHead> list)
        {
            foreach (BalanceHead pay in list)
            {
                int rowCount = this.neuSpread2_Sheet1.Rows.Count;
                this.neuSpread2_Sheet1.Rows.Add(rowCount, 1);

                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColInvoiceNo].Value = pay.InvoiceNo;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCost].Value = pay.PayCost;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColDeliveryCost].Value = pay.DeliveryCost;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayType].Text = pay.PayType;
                //this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenBank].Value = pay.Company.OpenBank;
                //this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenAccounts].Value = pay.Company.OpenAccounts;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayOper].Value = "";// this.managerProxy.GetEmployeeInfoByID(pay.PayOper.ID).Name;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCredence].Text = pay.PayCredence;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnpayCredence].Text = pay.UnpayCredence;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnCredenceDate].Text = pay.UnpayCredenceTime.ToString();
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayDate].Text = pay.OperDate.ToString();
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayListNum].Text = pay.ListNum;

                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColInvoiceNo].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCost].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColDeliveryCost].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayType].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenBank].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenAccounts].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayOper].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCredence].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnpayCredence].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnCredenceDate].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayDate].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayListNum].Locked = true;
                this.neuSpread2_Sheet1.Rows[rowCount].Tag = pay;
            }
        }

        /// <summary>
        /// ��ʾ�����̽����Ϣ
        /// </summary>
        private void ShowPayDetail()
        {
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            #region ���벻������
            //if (this.alPayDetail != null && this.alPayDetail.Count > 0)
            //{
            //    this.neuSpread2_Sheet1.Rows.Count = 0;

            //    if (this.alPayDetail.Count <= this.neuSpread1_Sheet1.ActiveRowIndex)
            //    {
            //        return;
            //    }

            //    this.AddPayDetailData(this.alPayDetail[this.neuSpread1_Sheet1.ActiveRowIndex] as List<PayHead>);
            //}
            #endregion
            if (this.htPayDetail != null && this.htPayDetail.Count > 0)
            {
                this.neuSpread2_Sheet1.Rows.Count = 0;
                if (this.htPayDetail.ContainsKey((this.neuSpread1_Sheet1.ActiveRow.Tag as BalanceHead).PayHeadNo))
                {
                    this.AddPayDetailData(this.htPayDetail[(this.neuSpread1_Sheet1.ActiveRow.Tag as BalanceHead).PayHeadNo] as List<BalanceHead>);
                }
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// {DCAEF089-F156-4a81-A476-0D29DBA7AEFD}
        /// [���ܣ��ı���޸�״̬]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetStyle()
        {
            for (int columnCount = 0; columnCount < this.neuSpread1_Sheet1.ColumnCount; columnCount++)
            {
                this.neuSpread1_Sheet1.Columns[columnCount].Locked = true;
                this.neuSpread1_Sheet1.Columns[columnCount].BackColor = Color.White;
                this.neuSpread1_Sheet1.Columns[columnCount].Width = this.neuSpread1_Sheet1.Columns[columnCount].GetPreferredWidth();
            }
            if (rbUnPay.Checked && !rbPay.Checked)
            {
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].BackColor = Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].BackColor = Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].BackColor = Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnpayCredence].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnpayCredence].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCredence].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCredence].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnCredenceDate].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnCredenceDate].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenBank].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenBank].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenAccounts].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenAccounts].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].BackColor = Color.SeaShell;
            }
            //for (int rowCount = 0; rowCount < this.neuSpread1_Sheet1.RowCount; rowCount++)
            //{
            //    if (rbPay.Checked == true)
            //    {
            //        //���ÿɱ༭��
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].Locked = true;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].Locked = true;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].Locked = true;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].Locked = true;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Locked = true;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].Locked = true;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Locked = true;

            //        //������ɫ
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("�Żݽ��")].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("���θ���")].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("�˷�")].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].BackColor = Color.White;
            //    }
            //    else if (rbUnPay.Checked == true)
            //    {

            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].Locked = false;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].Locked = false;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].Locked = false;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].Locked = false;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Locked = false;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].Locked = false;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Locked = false;
            //        if (rbPay.Checked == true)

            //            //������ɫ
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].BackColor = Color.SeaShell;
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("���θ���")].BackColor = Color.SeaShell;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].BackColor = System.Drawing.Color.SeaShell;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].BackColor = System.Drawing.Color.SeaShell;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].BackColor = System.Drawing.Color.SeaShell;
            //    }
            //}
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="lableName"></param>
        /// <returns></returns>
        protected int GetColIndex(string lableName)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Columns[i].Label == lableName)
                {
                    return i;
                }
            }
            //����-1��ʱ��fp��Ĭ�������У����������쳣
            return -2;
        }

        #region ���з���
        /// <summary>
        /// �������
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SavePay()
        {
            if (!this.SaveValid())
            {
                return -1;
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.payMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            BalanceHead pay = null;
            int saveCount = 0;

            string tempPayListNum = this.payMgr.GetPayListNum(this.cmbCompany.Tag.ToString());

            this.strPrintPayListNo = tempPayListNum;



            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                //if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value == null || !((bool)this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value))
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value == null || !Neusoft.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value))
                    continue;

                pay = this.neuSpread1_Sheet1.Rows[i].Tag as BalanceHead;
                if (pay == null)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("������������� ��������ת������"));
                    return -1;
                }
                //�ѽ�� ���ٴδ���
                if (pay.PayState == "2")
                {
                    //MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��"+(i+1)+"����¼�Ѿ����㣬�޷����и������"));
                    continue;
                }

                if (pay.DiscountCost <= 0)
                {
                    //�Żݽ��
                    pay.DiscountCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDiscountCost].Value);
                    pay.UnpayCost = pay.UnpayCost - pay.DiscountCost;		//δ�����
                }
                //�˷�
                pay.DeliveryCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDeliveryCost].Value);
                pay.Oper.ID = this.privOper.ID;
                pay.OperDate = this.payMgr.GetDateTimeFromSysDateTime();

                if (this.payMgr.UpdateInsertPayHead(pay) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("���¹����̽����Ϣ����" + this.payMgr.Err));
                    return -1;
                }

                //��������
                pay.PayType = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayType].Text;
                if (pay.PayType == "")
                {
                    pay.PayType = "�ֽ�";
                }
                //��������
                //pay.Company.OpenBank = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenBank].Text;
                //�����ʺ�
                //pay.Company.OpenAccounts = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenAccounts].Text;
                pay.PayOper.ID = this.privOper.ID;
                //���θ���
                pay.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCost].Value);

                if (pay.PayCost == 0)
                {
                    continue;
                }
                //����ƾ֤
                pay.PayCredence = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCredence].Text;
                //δ����ƾ֤
                pay.UnpayCredence = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColUnpayCredence].Text;
                //δ����ƾ֤����
                pay.UnpayCredenceTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColUnCredenceDate].Text);
                pay.UnpayCost = pay.UnpayCost - pay.PayCost;

                //����ͷ��Ľ����Ϣ
                int returnVal = this.payMgr.UpdateHeadPayInfo(pay);
                if (returnVal < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.payMgr.Err);
                    return -1;
                }
                else if (returnVal == 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʱδ�������ͷ���¼");
                    return -1;
                }
                //��ȡ�������
                int sequenceNo = pay.SequenceNo;
                returnVal = this.payMgr.GetInvoicePaySequence(pay.PayHeadNo, pay.InvoiceNo, ref sequenceNo);
                if (returnVal != 1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.payMgr.Err);
                    return -1;
                }
                pay.SequenceNo = sequenceNo;
                //�������������ϸ��Ϣ
                returnVal = this.payMgr.InsertPayDetail(pay, tempPayListNum);
                if (returnVal != 1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.payMgr.Err);
                    return -1;
                }
                saveCount++;
            }
            if (saveCount <= 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("û��ѡ�л�û�пɱ���ļ�¼"));
                return 0;
            }


            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("����ɹ�"));
            //this.Print();
            return 1;
        }

        #endregion

        #region ��������
        /// <summary>
        /// ������Ч���ж�
        /// </summary>
        /// <returns>�����Ƿ�������</returns>
        protected bool SaveValid()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return false;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                decimal payCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCost].Value);
                decimal due = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDue].Value);
                decimal paidUp = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPaidUpCost].Value);
                if (payCost > due - paidUp)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��Ʊ��" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " ���θ���ܴ���δ������"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayType].Text == "֧Ʊ")
                {
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenBank].Text == "")
                    {
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��Ʊ��" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " ��������Ϊ֧Ʊʱ����д��������"));
                        return false;
                    }
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenAccounts].Text == "")
                    {
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("��Ʊ��" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " ��������Ϊ֧Ʊʱ����д�����ʺ�"));
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #endregion

        #region ����������

        /// <summary>
        /// ȫѡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton(Neusoft.FrameWork.Management.Language.Msg("ȫѡ"), Neusoft.FrameWork.Management.Language.Msg("ȫѡ��ȫ��ѡ"), (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            return this.toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == Neusoft.FrameWork.Management.Language.Msg("ȫѡ"))
            {

                for (int j = 0; j < this.neuSpread1_Sheet1.RowCount; j++)
                {

                    this.neuSpread1_Sheet1.Cells[j, (int)ColPayHeadSet.ColChoose].Value = !(bool)this.neuSpread1_Sheet1.Cells[j, (int)ColPayHeadSet.ColChoose].Value;
                }

            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region ��ö��

        /// <summary>
        /// ��������Ϣ������
        /// </summary>
        enum ColPayHeadSet
        {
            /// <summary>
            /// �Ƿ񸶿� 0
            /// </summary>
            ColChoose,
            /// <summary>
            /// ��Ʊ�� 1
            /// </summary>
            ColInvoiceNo,
            /// <summary>
            /// ��Ʊ����	2
            /// </summary>
            ColInvoiceDate,
            /// <summary>
            /// ��Ʊ���	3
            /// </summary>
            ColInvoiceCost,
            /// <summary>
            /// �Żݽ��	4
            /// </summary>
            ColDiscountCost,
            /// <summary>
            /// Ӧ�����	5
            /// </summary>
            ColDue,
            /// <summary>
            /// �Ѹ����	6
            /// </summary>
            ColPaidUpCost,
            /// <summary>
            /// ���θ���	7
            /// </summary>
            ColPayCost,
            /// <summary>
            /// �˷�		8
            /// </summary>
            ColDeliveryCost,
            /// <summary>
            /// ��������	9
            /// </summary>
            ColPayType,
            /// <summary>
            /// ��������	10
            /// </summary>
            ColOpenBank,
            /// <summary>
            /// �����ʺ�	11
            /// </summary>
            ColOpenAccounts,
            /// <summary>
            /// ������	12
            /// </summary>
            ColDept,
            /// <summary>
            /// ��ⵥ�ݺ�	13
            /// </summary>
            ColInListCode,
            /// <summary>
            /// ����ƾ֤
            /// </summary>
            ColPayCredence,
            /// <summary>
            /// δ����ƾ֤
            /// </summary>
            ColUnpayCredence,
            /// <summary>
            /// δ����ƾ֤����
            /// </summary>
            ColUnCredenceDate,

            /// <summary>
            /// ����������
            /// </summary>
            /// {38AE0936-69C9-4543-BB4E-78998C7CCE94}
            ColCompanyName
        }
        /// <summary>
        /// ��渶����ϸ��Ϣ��������
        /// </summary>
        enum ColPayDetailSet
        {
            /// <summary>
            /// ��Ʊ��
            /// </summary>
            ColInvoiceNo,
            /// <summary>
            /// ������
            /// </summary>
            ColPayCost,
            /// <summary>
            /// �˷�
            /// </summary>
            ColDeliveryCost,
            /// <summary>
            /// ��������
            /// </summary>
            ColPayType,
            /// <summary>
            /// ��������
            /// </summary>
            ColOpenBank,
            /// <summary>
            /// �����ʺ�
            /// </summary>
            ColOpenAccounts,
            /// <summary>
            /// ������
            /// </summary>
            ColPayOper,
            /// <summary>
            /// ����ƾ֤
            /// </summary>
            ColPayCredence,
            /// <summary>
            /// δ����ƾ֤
            /// </summary>
            ColUnpayCredence,
            /// <summary>
            /// δ����ƾ֤����
            /// </summary>
            ColUnCredenceDate,
            /// <summary>
            /// ��������
            /// </summary>
            ColPayDate,
            /// <summary>
            /// �����ˮ����
            /// </summary>
            ColPayListNum
        }

        /// <summary>
        /// ��ѯ����ö��
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        enum EnumSearchContion
        {
            ��Ʊ��,
            ��ⵥ��
        }
        #endregion

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            Neusoft.FrameWork.Models.NeuObject testPrivDept = new Neusoft.FrameWork.Models.NeuObject();

            string privIndex = "5514";// Neusoft.FrameWork.Function.NConvert.ToInt32(Neusoft.HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.�����̽��);

            if (isDrug)
            {
                privIndex = "0310";
            }

            int parma = Neusoft.HISFC.Components.Common.Classes.Function.ChoosePivDept(privIndex, ref testPrivDept);

            if (parma == -1)            //��Ȩ��
            {
                //MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("���޴˴��ڲ���Ȩ��"));
                return -1;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return -1;
            }

            this.privDept = testPrivDept;
            this.privOper = Neusoft.FrameWork.Management.Connection.Operator;
            base.OnStatusBarInfo(null, Neusoft.FrameWork.Management.Language.Msg("��������:") + testPrivDept.Name);

            return 1;
        }

        #endregion

        #region �¼�

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.ShowPayDetail();
        }

        private void rbUnPay_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbUnPay.Checked)
            {
                this.payFlag = "'0','1'";
                this.Query();
            }
        }

        private void rbPay_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbPay.Checked)
            {
                this.payFlag = "'2'";
                this.Query();
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        /// <summary>
        /// 
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryInfo();
            }
        }


        private void txtSearchCondition_MouseEnter(object sender, EventArgs e)
        {
            //this.txtSearchCondition.SelectAll();
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void txtSearchCondition_MouseHover(object sender, EventArgs e)
        {
            //this.txtSearchCondition.SelectAll();
        }


        /// <summary>
        /// {DCAEF089-F156-4a81-A476-0D29DBA7AEFD}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            decimal d1 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, GetColIndex(Neusoft.FrameWork.Management.Language.Msg("Ӧ�����"))].Value);
            decimal d2 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, GetColIndex(Neusoft.FrameWork.Management.Language.Msg("�Ѹ����"))].Value);
            decimal d3 = d1 - d2;
            this.neuSpread1_Sheet1.Cells[e.Row, GetColIndex(Neusoft.FrameWork.Management.Language.Msg("���θ���"))].Text = d3.ToString();
        }

        #endregion
    }

}
