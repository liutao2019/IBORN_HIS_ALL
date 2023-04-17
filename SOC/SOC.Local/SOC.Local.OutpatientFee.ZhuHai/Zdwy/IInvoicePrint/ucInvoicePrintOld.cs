using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.IInvoicePrint
{
    public partial class ucInvoicePrintOld : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucInvoicePrintOld()
        {
            InitializeComponent();
        }

        #region ����

        private string description = "��ɽ��ѧ��������ҽԺ";

        private string invoiceType = "MZ01";

        private bool isPreView = false;

        private FS.HISFC.Models.Registration.Register register;

        private string setPayModeType;

        private string splitInvoicePayMode;

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        FS.FrameWork.Public.ObjectHelper payModesHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region ����

        /// <summary>
        /// ��÷������������
        /// </summary>
        /// <param name="i">���</param>
        /// <returns></returns>
        private Control GetFeeNameLable(int i)
        {
            Control c = this.Controls[string.Concat("lblPreFeeName", i.ToString())];
            if (c != null)
            {
                c.Visible = true;
            }

            return c;
        }

        /// <summary>
        /// ��÷��ý�������
        /// </summary>
        /// <param name="i">���</param>
        /// <returns></returns>
        private Control GetFeeCostLable(int i)
        {
            Control c = this.Controls[string.Concat("lblPriCost", i.ToString())];
            if (c != null)
            {
                c.Visible = true;
            }

            return c;
        }

        /// <summary>
        /// ��ȡ��Ʊ��ӡ��д��������(ֻ��ӡ��ʮ��)
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        private string[] GetUpperCashbyNumber(decimal Cash)
        {
            string[] sNumber = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
            string[] sReturn = new string[9];
            string strCash = null;
            //���λ��
            int iLen = 0;
            strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
            if (strCash.Length > 9)
            {
                strCash = strCash.Substring(strCash.Length - 9);
            }

            //���λ��
            iLen = 9 - strCash.Length;
            for (int j = 0; j < iLen; j++)
            {
                int k = 0;
                k = 8 - j;
                sReturn[k] = "��";
            }
            for (int i = 0; i < strCash.Length; i++)
            {
                string Temp = null;

                Temp = strCash.Substring(strCash.Length - 1 - i, 1);

                if (Temp == ".")
                {
                    continue;
                }
                sReturn[i] = sNumber[int.Parse(Temp)];
            }
            return sReturn;
        }

        #endregion

        #region IInvoicePrint ��Ա

        public string Description
        {
            get { return this.description; }
        }

        public string InvoiceType
        {
            get { return this.invoiceType; }
        }

        /// <summary>
        /// �����Ƿ��ӡ���� true��Ʊ���� false��Ʊ�״�
        /// </summary>
        public bool IsPreView
        {
            set { this.isPreView = value; }
        }

        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZFP", 787, 400);
                print.SetPageSize(ps);
                if (isPreView)
                {
                    //��Ʊ����
                    print.PrintDocument.PrinterSettings.PrinterName = this.controlIntegrate.GetControlParam<string>("MZFPFB", false, "MZFPFB");
                }
                else
                {
                    //��Ʊ�״�
                    print.PrintDocument.PrinterSettings.PrinterName = this.controlIntegrate.GetControlParam<string>("MZFP", false, "MZFP");
                }

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                print.PrintPage(0, 0, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        public int PrintOtherInfomation()
        {
            return 1;
        }

        public FS.HISFC.Models.Registration.Register Register
        {
            set { this.register = value; }
        }

        /// <summary>
        /// ����֧����ʽģʽ 1ʹ��SplitInvoicePayMode ����ʹ��SetPrintValue�еķ�Ʊ����
        /// </summary>
        public string SetPayModeType
        {
            set { this.setPayModeType = value; }
        }

        /// <summary>
        /// ��Ʊ֧����ʽ
        /// </summary>
        public string SplitInvoicePayMode
        {
            set { this.splitInvoicePayMode = value; }
        }

        public void SetPreView(bool isPreView)
        {
            this.isPreView = isPreView;
        }

        public int SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, ArrayList Invoices, ArrayList invoiceDetails, ArrayList feeDetails)
        {
            return 1;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, bool isPreview)
        {
            return 1;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList alPayModes, bool isPreview)
        {
            this.isPreView = isPreview;

            if (feeDetails.Count <= 0)
            {
                return -1;
            }
            //{2B6B02FF-9244-49af-B6A9-D5759033A3D7}
            //this.neuLabel1.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
            this.neuLabel1.Text = curDepartment.HospitalName;
            //���ÿؼ���ʾ
            foreach (Control c in this.Controls)
            {
                if (c.Name.Length > 6 && "lblPre".Equals(c.Name.Substring(0, 6)))
                {
                    c.Visible = isPreview;
                }

                if (isPreview == false)
                {
                    if (c.Name.Length > 3 && "lbl".Equals(c.Name.Substring(0, 3)))
                    {
                        System.Windows.Forms.Label lblControl = c as System.Windows.Forms.Label;
                        lblControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    }
                }
            }

            //���û�����Ϣ
            this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();
            this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();
            this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();
            string strOperTemp="";
            if(!string.IsNullOrEmpty(invoice.BalanceOper.Name))
            {
                strOperTemp="("+invoice.BalanceOper.Name+")";
            }
            this.lblPriOper.Text = invoice.BalanceOper.ID+strOperTemp;
            this.lblInvoice.Text = invoice.Invoice.ID;
            this.lblFeeDate.Text = invoice.PrintTime.ToString("HH:mm:ss");
            //if (isPreview == true)
            //{
            //    this.lblPriSwYear.Text = regInfo.DoctorInfo.SeeDate.Year.ToString();
            //    this.lblPriSwMonth.Text = regInfo.DoctorInfo.SeeDate.Month.ToString();
            //    this.lblPriSwDay.Text = regInfo.DoctorInfo.SeeDate.Day.ToString();
            //    this.lblFeeDate.Text = "";
            //}
            this.lblPriSwBalanceType.Text = invoice.PrintedInvoiceNO;
            this.lblPriSwBalanceType2.Text = invoice.PrintedInvoiceNO;
            this.lblPriName.Text = regInfo.Name;
            if (regInfo.Sex.Name.ToString() != "" && regInfo.Sex.Name.ToString() != null && regInfo.Sex.Name.ToString() == "��" && isPreview == false)
            {
                this.lblPriSexM.Text = "��";
                this.lblPriSexM.Visible = true;
            }
            else if (regInfo.Sex.Name.ToString() != "" && regInfo.Sex.Name.ToString() != null && regInfo.Sex.Name.ToString() == "Ů" && isPreview == false)
            {
                this.lblPriSexW.Text = "��";
                this.lblPriSexW.Visible = true;
            }
            else if (isPreview == true)
            {
                this.lblPriSexM.Text = "��";
                this.lblPriSexW.Text = "Ů";
                this.lblPriSexM.Visible = true;
            }
          
            this.lblPriPayKind.Text = regInfo.Pact.Name;
            if (regInfo.SSN != "")
            {
                this.lbPactName.Text += "," + "ҽ��֤�ţ�" + regInfo.SSN;
            }
            this.neuLabel5.Visible = true;
            this.neuLabel5.Text = "�����ۺ�";
            ////���ô�������
            this.lblPreFeeName1.Text = "��ҩ��";
            this.lblPreFeeName1.Visible = true;
            this.lblPreFeeName2.Text = "�г�ҩ";
            this.lblPreFeeName2.Visible = true;
            this.lblPreFeeName3.Text = "�в�ҩ";
            this.lblPreFeeName3.Visible = true;
            this.lblPreFeeName4.Text = "����";
            this.lblPreFeeName4.Visible = true;
            this.lblPreFeeName5.Text = "����";
            this.lblPreFeeName5.Visible = true;
            this.lblPreFeeName6.Text = "�����";
            this.lblPreFeeName6.Visible = true;
            this.lblPreFeeName7.Text = "���Ʒ�";
            this.lblPreFeeName7.Visible = true;
            this.lblPreFeeName8.Text = "������";
            this.lblPreFeeName8.Visible = true;
            this.lblPreFeeName9.Text = "�����";
            this.lblPreFeeName9.Visible = true;
            this.lblPreFeeName10.Text = "������";
            this.lblPreFeeName10.Visible = true;
            this.lblPreFeeName11.Text = "���Ϸ�";
            this.lblPreFeeName11.Visible = true;
            this.lblPreFeeName12.Text = "��λ��";
            this.lblPreFeeName12.Visible = true;
            //֧����ʽ
            if (payModesHelper.ArrayObject == null || payModesHelper.ArrayObject.Count == 0)
            {
                payModesHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            }
            string payKind = "";
            if ("1".Equals(this.setPayModeType))
            {
                payKind = this.splitInvoicePayMode;
            }
            else
            {
                for (int i = 0; i < alPayModes.Count; i++)
                {
                    FS.HISFC.Models.Fee.Outpatient.BalancePay payMode = alPayModes[i] as FS.HISFC.Models.Fee.Outpatient.BalancePay;
                    payKind += " " + payModesHelper.GetObjectFromID(payMode.PayType.ID).Name
                        + " " + FS.FrameWork.Public.String.FormatNumber(payMode.FT.TotCost, 2) + "��  ";
                }
            }
            lbPactName.Font = new Font("����", 8);
            lbPactName.Text = payKind;

            string strTemp = "";
            bool boolTemp = false;
            if(regInfo.SIMainInfo.AddTotCost!=0)
            {
                strTemp = "���κ�׼��" + regInfo.SIMainInfo.AddTotCost.ToString();
                boolTemp = true;
            }
            if (regInfo.SIMainInfo.YearPubCost != 0)
            {
                strTemp = strTemp + "�ۼƺ�׼��" + regInfo.SIMainInfo.YearPubCost.ToString();
                boolTemp = true;
            }
            this.lbAddTotCost.Visible = boolTemp;
            this.lbAddTotCost.Text = strTemp;
           

            //���ô�����Ϣ
            for (int i = 0; i < invoiceDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.BalanceList detail = invoiceDetails[i] as FS.HISFC.Models.Fee.Outpatient.BalanceList;
                if (detail.InvoiceSquence < 1 || detail.InvoiceSquence > 24)
                {
                    continue;
                }

                ////���ô�������
                //System.Windows.Forms.Label lblFeeName = this.GetFeeNameLable(detail.InvoiceSquence) as System.Windows.Forms.Label;
                //if (lblFeeName == null)
                //{
                //    MessageBox.Show("û���ҵ����ô���Ϊ" + detail.FeeCodeStat.Name + "�Ĵ�ӡ���!");
                //    return -1;
                //}
                //lblFeeName.Text = detail.FeeCodeStat.Name;
                //lblFeeName.Visible = true;

                //���ô�����
                System.Windows.Forms.Label lblFeeCost = this.GetFeeCostLable(detail.InvoiceSquence) as System.Windows.Forms.Label;
                if (lblFeeCost == null)
                {
                    MessageBox.Show("û���ҵ����ô���Ϊ" + detail.FeeCodeStat.Name + "�Ĵ�ӡ���!");
                    return -1;
                }
                lblFeeCost.Text = detail.BalanceBase.FT.TotCost.ToString();

            }

            //������Ϣ
            this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2);
            this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PayCost + invoice.FT.OwnCost, 2);

            //��ʾ��ҩҩ����Ϣ(ȥ���ظ�)
            if (string.IsNullOrEmpty(invoice.DrugWindowsNO) == false)
            {
                string[] drugWindows = invoice.DrugWindowsNO.Split('|');
                ArrayList alDrugWindow = new ArrayList();
                string disPlayWindow = "";
                for (int i = 0; i < drugWindows.Length; i++)
                {
                    if (alDrugWindow.Contains(drugWindows[i]) == false)
                    {
                        alDrugWindow.Add(drugWindows[i]);
                        disPlayWindow += drugWindows[i] + ",";
                    }
                }
                this.lblDrugWindow.Visible = true;
                this.lblDrugWindow.Text = disPlayWindow.TrimEnd(',');
            }
            else
            {
                this.lblDrugWindow.Visible = false;
            }

            //�ܽ���Сд
            this.lblPriLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);

            string[] strMoney = this.GetUpperCashbyNumber(FS.FrameWork.Public.String.FormatNumber(invoice.FT.TotCost, 2));
            this.lblPriF.Text = strMoney[0];
            this.lblPriJ.Text = strMoney[1];
            this.lblPriY.Text = strMoney[3];
            this.lblPriS.Text = strMoney[4];
            this.lblPriB.Text = strMoney[5];
            this.lblPriQ.Text = strMoney[6];
            this.lblPriW.Text = strMoney[7];
            this.lblPriSW.Text = strMoney[8];

            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public IDbTransaction Trans
        {
            set { }
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint);
                return type;
            }
        }

        #endregion

     
    }
}
