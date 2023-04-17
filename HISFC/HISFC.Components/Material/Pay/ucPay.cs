using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Material.Pay
{
    /// <summary>
    /// [��������: �����̽��]
    /// [�� �� ��: wangw]
    /// [����ʱ��: 2008-03]
    /// </summary>
    public partial class ucPay : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucPay()
        {
            InitializeComponent();
        }

        #region �ֶ�

        /// <summary>
        /// ���а�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper bankHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���ʹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store matManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// �����ϸ��Ϣ
        /// </summary>
        private ArrayList alPayDetail = new ArrayList();

        /// <summary>
        /// ������˾
        /// </summary>
        private FS.HISFC.Models.Material.MaterialCompany company = new FS.HISFC.Models.Material.MaterialCompany();

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept;

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject privOper;

        /// <summary>
        /// ��ѯ��ʼʱ��
        /// </summary>
        private DateTime dtBegin = System.DateTime.MinValue;

        /// <summary>
        /// ��ѯ��ֹʱ��
        /// </summary>
        private DateTime dtEnd = System.DateTime.MaxValue;

        /// <summary>
        /// ��ѯ����־
        /// </summary>
        private string payFlag;

        /// <summary>
        /// ������λ
        /// </summary>
        private ArrayList alCompany = new ArrayList();

        #endregion

        #region ����

        protected FS.HISFC.Models.Material.MaterialCompany Company
        {
            set
            {
                if(value == null)
                {
                    this.company = new FS.HISFC.Models.Material.MaterialCompany();
                }
                else
                {
                    this.company = value;
                }
                this.lbCompany.Text = "��浥λ" + this.company.Name;
            }
        }

        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ����

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ʱ  ��", "���ò�ѯʱ��", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            toolBarService.AddToolButton("�ѽ���ѯ", "�ѽ�浥�ݲ�ѯ", FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null);
            toolBarService.AddToolButton("δ����ѯ", "δ��浥�ݲ�ѯ", FS.FrameWork.WinForms.Classes.EnumImageList.P�̵�����, true, false, null);
            toolBarService.AddToolButton("������λ", "ѡ���ѯ�Ĺ�����λ", FS.FrameWork.WinForms.Classes.EnumImageList.J����, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ʱ  ��")
            {
                if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.dtBegin, ref dtEnd) == 0)
                {
                    return;
                }
            }
            if (e.ClickedItem.Text == "δ����ѯ")
            {
                this.Query("'0','1'", dtBegin, dtEnd);

                this.payFlag = "'0','1'";
            }
            if (e.ClickedItem.Text == "�ѽ���ѯ")
            {
                this.Query("'2'", dtBegin, dtEnd);

                this.payFlag = "2";
            }
            if (e.ClickedItem.Text == "������λ")
            {
                this.ShowCompany();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object NeuObject)
        {
            if (this.rbPay.Checked)
            {
                MessageBox.Show(Language.Msg("�ѽ�浥�ݲ����ٴα���"), "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return -1;
            }

            this.SavePay();

            this.Query(this.payFlag, this.dtBegin, this.dtEnd);

            return 1;
        }

        public override int Export(object sender, object NeuObject)
        {
            if (this.neuSpread2.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query(this.payFlag, dtBegin, dtEnd);

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void Init()
        {
            ArrayList al = new ArrayList();

            #region ������Ϣ

            FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();
            al = constantManager.GetList("BANK");
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�����б�ʧ��" + constantManager.Err));
                return;
            }
            bankHelper.ArrayObject = al;

            #endregion

            #region ��Ա

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            al = personManager.GetEmployeeAll();
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ������Ա�б�" + personManager.Err));
                return;
            }
            this.personHelper.ArrayObject = al;

            #endregion

            #region ��Ӧ��

            FS.HISFC.BizLogic.Material.ComCompany companyManager = new FS.HISFC.BizLogic.Material.ComCompany();

            this.alCompany = companyManager.QueryCompany("1", "A");

            if (this.alCompany == null)
            {
                MessageBox.Show(constantManager.Err);
                return;
            }

            #endregion

            #region ʱ��

            FS.FrameWork.Management.DataBaseManger databaseManager = new FS.FrameWork.Management.DataBaseManger();
            DateTime sysTime = databaseManager.GetDateTimeFromSysDateTime().Date.AddDays(1);
            this.dtBegin = sysTime.AddDays(-30);
            this.dtEnd = sysTime;

            this.privOper = databaseManager.Operator;

            this.payFlag = "'0','1'";

            #endregion

        }

        protected void ShowCompany()
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alCompany, ref info) == 0)
            {
                return;
            }
            else
            {
                this.Company = (FS.HISFC.Models.Material.MaterialCompany)info;

                this.Query(this.payFlag, this.dtBegin, this.dtEnd);
            }
        }

        /// <summary>
        /// ���ݽ���ǲ�ѯ��������Ϣ
        /// </summary>
        /// <param name="payFlag">����� 0δ����  1�Ѹ��� 2��ɸ���</param>
        /// <param name="dtBegin">��ѯ��ʼʱ��</param>
        /// <param name="dtEnd">��ѯ����ʱ��</param>
        public void Query(string payFlag, DateTime dtBegin, DateTime dtEnd)
        {
            if (this.company == null)
            {
                MessageBox.Show(Language.Msg("��ѡ�񹩻���˾"));
                return;
            }
            ArrayList al = new ArrayList();
            al = this.matManager.QueryPayList(this.privDept.ID, this.company.ID, payFlag, dtBegin, dtEnd);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��������Ϣʧ��" + this.matManager.Err));
                return;
            }

            this.alPayDetail = new ArrayList();
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;
            FS.HISFC.Models.Material.Pay info;

            for (int i = 0; i < al.Count; i++)
            {
                info = al[i] as FS.HISFC.Models.Material.Pay;
                if (info == null)
                {
                    MessageBox.Show(Language.Msg("�����" + (i + 1).ToString() + "�н�������Ϣ����"));
                    continue;
                }
                ArrayList alTemp = this.matManager.QueryPayDetail(info.ID, info.InvoiceNo);
                if (alTemp == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ��" + (i + 1).ToString() + "�н����ϸ��Ϣ����" + this.matManager.Err));
                    continue;
                }
                if (alTemp.Count > 0)
                {
                    this.alPayDetail.Add(alTemp);
                }

                this.AddPayHeadData(info);
            }
        }

        /// <summary>
        /// ���������ϢFarPoint�ڼ�������
        /// </summary>
        /// <param name="pay">�����̽��ʵ��</param>
        protected void AddPayHeadData(FS.HISFC.Models.Material.Pay pay)
        {
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);

            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColChoose].Value = true;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceNo].Text = pay.InvoiceNo;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceDate].Value = pay.InvoiceTime;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceCost].Value = pay.PurchaseCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDiscountCost].Value = pay.DiscountCost;
            //Ӧ�����ͨ��FarPoint��ʽ�Զ�����
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPaidUpCost].Value = pay.PayCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCost].Value = pay.UnpayCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDeliveryCost].Value = pay.DeliveryCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayType].Value = pay.PayType;
            if (pay.Company.OpenBank == null || pay.Company.OpenBank == "")
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].Value = this.company.OpenBank;
            else
            {
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].Value = pay.Company.OpenBank;
            }
            if (pay.Company.OpenAccounts == null || pay.Company.OpenAccounts == "")
            {
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].Value = this.company.OpenAccounts;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].Value = pay.Company.OpenAccounts;
            }
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDept].Value = this.privDept.Name;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInListCode].Value = pay.InListCode;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Value = pay.UnpayCredence;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Locked = false;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].BackColor = System.Drawing.Color.SeaShell;

            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Value = pay.UnpayCredenceTime;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Locked = false;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].BackColor = System.Drawing.Color.SeaShell;

            this.neuSpread1_Sheet1.Rows[rowCount].Tag = pay;
        }

        /// <summary>
        /// ������ϸFarPoint�ڼ�������
        /// </summary>
        /// <param name="al">�����̽��ʵ������</param>
        protected void AddPayDetailData(ArrayList al)
        {
            foreach (FS.HISFC.Models.Material.Pay pay in al)
            {
                int rowCount = this.neuSpread2_Sheet1.Rows.Count;
                this.neuSpread2_Sheet1.Rows.Add(rowCount, 1);

                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColInvoiceNo].Value = pay.InvoiceNo;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCost].Value = pay.PayCost;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColDeliveryCost].Value = pay.DeliveryCost;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayType].Text = pay.PayType;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenBank].Value = pay.Company.OpenBank;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenAccounts].Value = pay.Company.OpenAccounts;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayOper].Value = this.personHelper.GetName(pay.PayOper.ID);
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayDate].Text = pay.Oper.OperTime.ToString();


                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColInvoiceNo].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCost].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColDeliveryCost].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayType].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenBank].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenAccounts].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayOper].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayDate].Locked = true;
                this.neuSpread2_Sheet1.Rows[rowCount].Tag = pay;
            }
        }

        /// <summary>
        /// ��ʾ�����̽����Ϣ
        /// </summary>
        public void ShowPayDetail()
        {
            if (this.alPayDetail != null && this.alPayDetail.Count > 0)
            {
                this.neuSpread2_Sheet1.Rows.Count = 0;

                if (this.alPayDetail.Count <= this.neuSpread1_Sheet1.ActiveRowIndex)
                {
                    return;
                }

                this.AddPayDetailData(this.alPayDetail[this.neuSpread1_Sheet1.ActiveRowIndex] as ArrayList);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;
            this.lbCompany.Text = "��浥λ��";
        }

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
                decimal payCost = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCost].Value);
                decimal due = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDue].Value);
                decimal paidUp = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPaidUpCost].Value);
                if (payCost > due - paidUp)
                {
                    MessageBox.Show(Language.Msg("��Ʊ��" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " ���θ���ܴ���δ������"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayType].Text == "֧Ʊ")
                {
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenBank].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��Ʊ��" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " ��������Ϊ֧Ʊʱ����д��������"));
                        return false;
                    }
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenAccounts].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��Ʊ��" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " ��������Ϊ֧Ʊʱ����д�����ʺ�"));
                        return false;
                    }
                }
            }
            return true;
        }

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

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.matManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Material.Pay pay;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value == null || !((bool)this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value))
                    continue;

                pay = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Material.Pay;
                if (pay == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("������������� ��������ת������"));
                    return -1;
                }
                //�ѽ�� ���ٴδ���
                if (pay.PayState == "2")
                {
                    continue;
                }

                if (pay.DiscountCost <= 0)
                {
                    //�Żݽ��
                    pay.DiscountCost = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDiscountCost].Value);
                    pay.UnpayCost = pay.UnpayCost - pay.DiscountCost;		//δ�����
                }
                //�˷�
                pay.DeliveryCost = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDeliveryCost].Value);
                pay.Oper.ID = this.privOper.ID;
                pay.Oper.OperTime = this.matManager.GetDateTimeFromSysDateTime();

                if (this.matManager.UpdateInsertPayHead(pay) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���¹����̽����Ϣ����" + this.matManager.Err));
                    return -1;
                }

                //��������
                pay.PayType = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayType].Text;
                if (pay.PayType == "")
                    pay.PayType = "�ֽ�";
                //��������
                pay.Company.OpenBank = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenBank].Text;
                //�����ʺ�
                pay.Company.OpenAccounts = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenAccounts].Text;
                pay.PayOper.ID = this.privOper.ID;
                //���θ���
                pay.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCost].Value);
                //δ����ƾ֤
                pay.UnpayCredence = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColUnpayCredence].Text;
                //δ����ƾ֤����
                pay.UnpayCredenceTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColUnCredenceDate].Text);

                if (pay.PayCost == 0)
                    continue;

                pay.UnpayCost = pay.UnpayCost - pay.PayCost;

                if (this.matManager.Pay(pay.ID, pay) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���湩���̽����Ϣ����" + this.matManager.Err));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ�"));
            return 1;
        }        

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
                //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0514", ref testPrivDept);

                //if (parma == -1)            //��Ȩ��
                //{
                //    MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                //    return;
                //}
                //else if (parma == 0)       //�û�ѡ��ȡ��
                //{
                //    return;
                //}

                //this.privDept = testPrivDept;

                //base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

                this.Init();
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// δ�����˵���ӡ {54092BCA-BDA1-45e8-A7C6-777282653264}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("���Ȳ�ѯ�ٴ�ӡ��");

                return -1;
            }

            string strTime = dtBegin.ToString() + "--" + dtEnd.ToString();
            List<FS.HISFC.Models.Material.Pay> payList = new List<FS.HISFC.Models.Material.Pay>();
            foreach (FarPoint.Win.Spread.Row r in this.neuSpread1_Sheet1.Rows)
            {
                FS.HISFC.Models.Material.Pay pay = r.Tag as FS.HISFC.Models.Material.Pay;
                payList.Add(pay);
            }

            FS.HISFC.Components.Material.Pay.ucUnpayListPrint ucPrint = new FS.HISFC.Components.Material.Pay.ucUnpayListPrint();
            ucPrint.SetPrintValue(strTime, this.company, this.privDept.Name, payList);
            ucPrint.Print();

            return base.Print(sender, neuObject);
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
            /// δ����ƾ֤
            /// </summary>
            ColUnpayCredence,
            /// <summary>
            /// δ����ƾ֤����
            /// </summary>
            ColUnCredenceDate
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
            /// ��������
            /// </summary>
            ColPayDate
        }

        #endregion

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.ShowPayDetail();
        }

        private void rbUnPay_CheckedChanged(object sender, EventArgs e)
        {
            this.payFlag = "'0','1'";

            this.Query(null, null);
        }

        private void rbPay_CheckedChanged(object sender, EventArgs e)
        {
            this.payFlag = "2";

            this.Query(null, null);
        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0514", ref testPrivDept);

            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                return -1;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return -1;
            }

            this.privDept = testPrivDept;

            base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

            return 1;
        }

        #endregion
    }
}
