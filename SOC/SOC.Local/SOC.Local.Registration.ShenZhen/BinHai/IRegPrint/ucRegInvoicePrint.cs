using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Account;

namespace FS.SOC.Local.Registration.ShenZhen.BinHai.IRegPrint
{
    /// <summary>
    /// �Һŷ�Ʊ��ӡ
    /// </summary>
    public partial class ucRegInvoicePrint : UserControl, FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        public ucRegInvoicePrint()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��ӡֽ��������
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        /// <summary>
        /// ��ӡ�õı�ǩ����
        /// </summary>
        public Collection<Label> lblPrint;

        /// <summary>
        /// Ԥ���õı�ǩ����
        /// </summary>
        public Collection<Label> lblPreview;

        private bool isPreview = false;

        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        #endregion

        #region IRegPrint ��Ա

        public int Clear()
        {
            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            try
            {

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

                FS.HISFC.Models.Base.PageSize ps = null;
                ps = psManager.GetPageSize("MZGH");

                if (ps == null)
                {
                    //û��ά�����Ͳ�ȡĬ������
                    ps = new FS.HISFC.Models.Base.PageSize("MZGH", 452, 276);
                    ps.Top = 0;
                    ps.Left = 0;
                }

                print.SetPageSize(ps);

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(ps.Left, ps.Top, this);
                }
                else
                {
                    print.PrintPage(ps.Left, ps.Top, this);
                }

                //��ӡ�ĵ������Ӳҳ�߾��X���꣬���Ӳ�߾�ʹ�ӡ���йأ����Ӳ�߾�>0���������ô�ӡ�ؼ��ı߾�ֵΪ����
                //print.PrintDocument.PrinterSettings.DefaultPageSettings.HardMarginX;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <returns></returns>
        public int PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this);
            return 0;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                this.InitReceipt();

                //��ʾ[ȥ��]
                //MessageBox.Show("���¼����ţ�" + register.PID.CardNO);

                //ҽԺ����
                this.lblHospitalName.Text = manageIntegrate.GetHospitalName();

                //�Һŷ�Ʊ��
                this.lblInvoiceNo.Text = register.InvoiceNO;
                //�Һ�ӡˢ��
                List<AccountCardFee> lstCardFee = new List<AccountCardFee>();
                int iRes = (new FS.HISFC.BizLogic.Fee.Account()).QueryAccountCardFeeByInvoiceNO(register.InvoiceNO, out lstCardFee);
                if (lstCardFee != null && lstCardFee.Count > 0)
                {
                    this.lblYsNo.Text = (lstCardFee[0] as FS.HISFC.Models.Account.AccountCardFee).Print_InvoiceNo;
                    this.lblYsNo.Visible = true;
                }
                //�����
                this.lblCardNo.Text = register.PID.CardNO;

                //�Һſ���
                this.lblDeptName.Text = register.DoctorInfo.Templet.Dept.Name;

                //�Һż���
                this.lblRegLevel.Text = register.DoctorInfo.Templet.RegLevel.Name;

                //����
                this.lblPatientName.Text = register.Name;

                //�Һ�Ա��
                this.lblRegOper.Text = register.InputOper.ID;

                //�Һ�����
                this.lblRegDate.Text = register.DoctorInfo.SeeDate.ToString();

                //��ͬ��λ���
                this.lblPactName.Text = register.Pact.Name;

                #region ���ø�ֵ
                string bookFee = string.Empty;
                if (register.LstCardFee != null && register.LstCardFee.Count > 0)
                {
                    #region �Һż�¼�ͷ��÷���

                    decimal regFee = 0m;
                    decimal chkFee = 0m;
                    decimal cardFee = 0m;
                    decimal caseFee = 0m;

                    foreach (FS.HISFC.Models.Account.AccountCardFee accFee in register.LstCardFee)
                    {
                        if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.RegFee)
                        {
                            //�Һŷ�
                            regFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.CaseFee)
                        {
                            //��������
                            caseFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.CardFee)
                        {
                            //��������
                            cardFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.ChkFee)
                        {
                            //����
                            chkFee += accFee.Tot_cost;
                        }
                        else if (accFee.FeeType == FS.HISFC.Models.Account.AccCardFeeType.DiaFee)
                        {
                            //����
                            chkFee += accFee.Tot_cost;
                        }
                    }

                    //�Һŷ�
                    this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regFee, 2) + "Ԫ";

                    //����
                    this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(chkFee, 2) + "Ԫ";

                    //��������
                    if (caseFee > 0)
                    {
                        this.lblCaseBook.Text = "��������";
                        bookFee = FS.FrameWork.Public.String.FormatNumberReturnString(caseFee, 2);
                        this.lblCaseBookCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(caseFee, 2) + "Ԫ";
                    }

                    // ��������
                    if (cardFee > 0)
                    {
                        this.lblCard.Text = "��������";
                        this.lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(cardFee, 2) + "Ԫ";
                    }

                    //�ϼ�
                    this.lblTotCost.Text = "�ϼƣ�" + (regFee + chkFee + caseFee + cardFee).ToString("F2") + " Ԫ";

                    #endregion

                }
                else
                {
                    #region ���û��Ǳ�����fin_opr_register(��)

                    //�Һŷ�
                    this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.RegFee, 2) +
                        "Ԫ";
                    //����
                    this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.ChkFee + register.RegLvlFee.PubDigFee + register.RegLvlFee.OwnDigFee, 2) +
                        "Ԫ";


                    //�����ֲ�
                    if (register.RegLvlFee.OthFee > 0)
                    {
                        this.lblCaseBook.Text = "��������";
                        this.lblCaseBookCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                        register.RegLvlFee.OthFee, 2) +
                        "Ԫ";
                    }

                    // ��������
                    decimal decCardFee = 0;
                    if (register.User01 == "CARDFEE")
                    {
                        decCardFee = FS.FrameWork.Function.NConvert.ToDecimal(register.User02);
                    }

                    if (decCardFee > 0)
                    {
                        this.lblCard.Text = "��������";
                        this.lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(decCardFee, 2) + "Ԫ";
                    }

                    #endregion
                }


                #endregion

                #region ҽ�������ʾ
                if (register.Pact.PayKind.ID == "02")
                {
                    string lblSimOld = FS.FrameWork.Public.String.FormatNumberReturnString(register.SIMainInfo.IndividualBalance, 2) + "Ԫ";//ҽ������ǰ���                    
                    string lblSimNow = FS.FrameWork.Public.String.FormatNumberReturnString(register.SIMainInfo.IndividualBalance - register.PayCost, 2) + "Ԫ";//ҽ���������
                    if (register.SIMainInfo.IndividualBalance == 0) lblSimNow = "0.00Ԫ";
                    this.lblYbBalance.Text = "ҽ������ǰ��" + lblSimOld;
                    this.lblYbBalanced.Text = "ҽ��������" + lblSimNow;
                    string lblSimRegFee = FS.FrameWork.Public.String.FormatNumberReturnString(register.OwnCost, 2);//�����Ը����+�������  + NConvert.ToDecimal(bookFee)
                    string lblSimSeeFee = FS.FrameWork.Public.String.FormatNumberReturnString(register.PayCost + register.PubCost, 2);//ҽ�����˽��
                    this.lblSiBalance.Text = "ҽ�����˽�" + lblSimSeeFee;
                    this.lblSiBalanced.Text = "�����ֽ�ɷѣ�" + lblSimRegFee;
                    this.nlsiinfo.Text = "���������������籣ͳ�����֧��";
                    if (register.SIMainInfo.PersonType.ID == "1")
                    {
                        this.lblSimType.Text = "ҽ�Ʊ���";
                    }
                    else if (register.SIMainInfo.PersonType.ID == "2")
                    {
                        this.lblSimType.Text = "����ҽ��";
                    }
                    else if (register.SIMainInfo.PersonType.ID == "3")
                    {
                        this.lblSimType.Text = "����ҽ��";
                    }
                    else if (register.SIMainInfo.PersonType.ID == "4")
                    {
                        this.lblSimType.Text = "����ͳ��ҽ��";
                    }
                    else if (register.SIMainInfo.PersonType.ID == "5")
                    {
                        this.lblSimType.Text = "����ҽ��";
                    }
                    else if (register.SIMainInfo.PersonType.ID == "6")
                    {
                        this.lblSimType.Text = "����ҽ�Ʊ���";
                    }
                    else
                    {
                        this.lblSimType.Text = "�ٶ�ҽ��";
                    }

                    if (register.SIMainInfo.MedicalType.ID == "1")
                    {
                        this.lblSimMedType.Text = "��ͨ����";
                    }
                    else if (register.SIMainInfo.MedicalType.ID == "2")
                    {
                        this.lblSimMedType.Text = "�ز�����";
                    }
                    else if (register.SIMainInfo.MedicalType.ID == "3")
                    {
                        this.lblSimMedType.Text = "�ؼ�����";
                    }
                    else if (register.SIMainInfo.MedicalType.ID == "4")
                    {
                        this.lblSimMedType.Text = "��������";
                    }
                    else if (register.SIMainInfo.MedicalType.ID == "5")
                    {
                        this.lblSimMedType.Text = "�������";
                    }
                    else if (register.SIMainInfo.MedicalType.ID == "6")
                    {
                        this.lblSimMedType.Text = "Ԥ������";
                    }
                    else
                    {
                        this.lblSimMedType.Text = "�ٶ�ͨ��";
                    }
                }
                else
                {
                    this.lblYbBalance.Visible = false;
                    this.lblYbBalanced.Visible = false;
                    this.lblSiBalance.Visible = false;
                    this.lblSiBalanced.Visible = false;
                    this.lblSimType.Visible = false;
                    this.lblSimMedType.Visible = false;
                }
                #endregion

                FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee doct = personMgr.GetPersonByID(register.DoctorInfo.Templet.Doct.ID);
                if (doct != null)
                {
                    this.lblDoctor.Text = doct.Name + " " + doct.ID;
                }
                else
                {
                    this.lblDoctor.Text = "";
                }
                //ԤԼȫ�Ʋ���
                //if (register.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                //{
                //    this.lbSeeInfo.Text = "[" + register.DoctorInfo.SeeNO + "��" + "]"
                //        + register.DoctorInfo.Templet.Begin.ToString("HH��mm��") + "-" + register.DoctorInfo.Templet.End.ToString("HH��mm��");


                //}
                //else
                //{                    
                    //this.lbSeeInfo.Visible = false;                    
                    this.lbSeeInfo.TextAlign = ContentAlignment.MiddleCenter;
                    this.lbSeeInfo.Text = register.DoctorInfo.SeeNO.ToString();
                    this.lbSeeInfo.Font = new System.Drawing.Font("����", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));                       
                //}

                //���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��
                if (IsPreview)
                {
                    SetToPreviewMode();
                }
                else
                {
                    SetToPrintMode();
                }

                if (true)//register.RegType != FS.HISFC.Models.Base.EnumRegType.Pre)
                {                    
                    this.lbSeeInfo.BorderStyle = BorderStyle.FixedSingle;
                    Label lbTmp = new Label();
                    lbTmp.Location = new Point(this.lbSeeInfo.Location.X - 1, this.lbSeeInfo.Location.Y - 1);
                    lbTmp.Size = new Size(this.lbSeeInfo.Width + 2, this.lbSeeInfo.Height + 2);
                    lbTmp.BorderStyle = BorderStyle.FixedSingle;
                    this.Controls.Add(lbTmp);
                    lbTmp.SendToBack();
                    this.lbSeeInfo.BringToFront();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public IDbTransaction Trans
        {
            get
            {
                return this.trans.Trans;
            }
            set
            {
                this.trans.Trans = value;
            }
        }

        #endregion

        #region ��ӡ����

        /// <summary>
        /// ��ʼ���վ�
        /// </summary>
        /// <remarks>
        /// �Ѵ�ӡ���Ԥ�������tag��ǩ��ֵ���ֿ�
        /// </remarks>
        private void InitReceipt(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            if (!string.IsNullOrEmpty(l.Text) || l.Text == "ӡ")
                            {
                                l.Text = "";
                            }
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }


        /// <summary>
        /// ��ʼ���վ�
        /// </summary>
        /// <remarks>
        /// �Ѵ�ӡ���Ԥ�������tag��ǩ��ֵ���ֿ�
        /// </remarks>
        private void InitReceipt()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            foreach (Control c in this.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            #region ����ӡ�ֵĴ�ӡ��ֵ���
                            if (!string.IsNullOrEmpty(l.Text) && l.Text == "ӡ")
                            {
                                l.Text = "";
                            }
                            #endregion
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }

        /// <summary>
        /// ���ñ�ǩ���ϵĿɼ���
        /// </summary>
        /// <param name="v">�Ƿ�ɼ�</param>
        /// <param name="l">��ǩ����</param>
        private void SetLableVisible(bool v, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Visible = v;
            }
        }


        /// <summary>
        /// ���ô�ӡ���ϵ�ֵ
        /// </summary>
        /// <param name="t">ֵ����</param>
        /// <param name="l">��ǩ����</param>
        private void SetLableText(string[] t, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Text = "";
            }
            if (t != null)
            {
                if (t.Length <= l.Count)
                {
                    int i = 0;
                    foreach (string s in t)
                    {
                        l[i].Text = s;
                        i++;
                    }
                }
                else
                {
                    if (t.Length > l.Count)
                    {
                        int i = 0;
                        foreach (Label lbl in l)
                        {
                            lbl.Text = t[i];
                            i++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ����Ϊ��ӡģʽ
        /// </summary>
        public void SetToPrintMode()
        {
            //��Ԥ������Ϊ���ɼ�
            SetLableVisible(false, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        /// <summary>
        /// ����ΪԤ��ģʽ
        /// </summary>
        public void SetToPreviewMode()
        {
            //��Ԥ������Ϊ�ɼ�
            SetLableVisible(true, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        #endregion
    }
}
