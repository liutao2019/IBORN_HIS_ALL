using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.NFC.Object;
using Neusoft.HISFC.Object.Registration;
using System.Collections;

namespace ReceiptPrint
{
    public partial class ucRegBillPrint : UserControl, Neusoft.HISFC.Integrate.Registration.IRegPrint
    {
        public ucRegBillPrint()
        {
            InitializeComponent();
        }

        private Neusoft.NFC.Management.Transaction trans = new Neusoft.NFC.Management.Transaction();
        /// <summary>
        /// ���ô�ӡֵ
        /// </summary>
        /// <param name="register">�Һ�ʵ��</param>
        /// <returns></returns>
        public int SetPrintValue(Neusoft.HISFC.Object.Registration.Register register)
        {
            try
            {
                this.InitReceipt();
                //�����
                this.lblCardNo.Text = register.PID.CardNO;
                //�Һſ���
                this.lblDeptName.Text = register.DoctorInfo.Templet.Dept.Name;
                //�ű�
                this.lblRegLevel.Text = register.DoctorInfo.Templet.RegLevel.Name;
                //�Һŷ�Ʊ��
                this.lblInvoiceno.Text = register.InvoiceNO;
                //����
                this.lblPatientName.Text = register.Name;
                //�Һ�Ա��
                this.lblRegOper.Text = register.InputOper.ID;
                //С��                
                this.lblCostsum.Text = Neusoft.NFC.Public.String.FormatNumberReturnString(
                    register.PubCost + register.PayCost + register.OwnCost, 2) +
                    "Ԫ";
                //��д
                this.lblUpperCostSum.Text = Neusoft.NFC.Public.String.LowerMoneyToUpper(
                   register.PubCost + register.PayCost + register.OwnCost
                    );
                //�Һ�����
                this.lblRegDate.Text = register.DoctorInfo.SeeDate.ToShortDateString();
                string medicalTypeName = string.Empty;

                //this.lblPayCostTitle.Visible = false;
                //this.lblOwnCostTitle.Visible = false;
                //this.lblIndividualBalanceTitle.Visible = false;
                //register.Pact.ID = "2";
                if (register.Pact.ID == "2")
                {
                    //this.lblPayCostTitle.Visible = true;
                    //this.lblOwnCostTitle.Visible = true;
                    //this.lblIndividualBalanceTitle.Visible = true;

                    //this.lblPayCost.Text = Neusoft.NFC.Public.String.FormatNumberReturnString(
                    //register.SIMainInfo.PayCost, 2) +
                    //"Ԫ";

                    //this.lblOwnCost.Text = Neusoft.NFC.Public.String.FormatNumberReturnString(
                    //register.SIMainInfo.OwnCost, 2) +
                    //"Ԫ";

                    //this.lblIndividualBalance.Text = Neusoft.NFC.Public.String.FormatNumberReturnString(
                    //register.SIMainInfo.IndividualBalance, 2) +
                    //"Ԫ";                  
                    switch (register.SIMainInfo.MedicalType.ID)
                    {
                        case "11":
                            {
                                //
                                medicalTypeName = "(" + "��ͨ����)";
                                break;
                            }
                        case "12":
                            {
                                //
                                medicalTypeName = "(" + "��������)";
                                break;
                            }
                        case "15":
                            {
                                //
                                medicalTypeName = "(" + "�������Բ�)";
                                break;
                            }
                        case "16":
                            {
                                //
                                medicalTypeName = "(" + "�����)";
                                break;
                            }
                        default:
                            {
                                //
                                medicalTypeName = "(" + "��ͨ����)";
                                break;
                            }
                    } 
                }
                //ҽ�����
                this.lblPactName.Text = register.Pact.Name + medicalTypeName;
               
                //�Һŷ�
                this.lblRegFee.Text = Neusoft.NFC.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.RegFee, 2) +
                    "Ԫ";
                //����
                this.lblChkFee.Text = Neusoft.NFC.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.ChkFee + register.RegLvlFee.PubDigFee + register.RegLvlFee.OwnDigFee, 2) +
                    "Ԫ";
                //�����ֲ� 
                //this.lblCaseBookCost.Text = Neusoft.NFC.Public.String.FormatNumberReturnString(
                //    register.RegLvlFee.OthFee, 2) +
                //    "Ԫ";
                //���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��
                if (IsPreview)
                {
                    SetToPreviewMode();
                }
                else
                {
                    SetToPrintMode();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }
        private bool isPreview = false ;

        private bool IsPreview
        {
            get { return isPreview; }
            set { isPreview = value; }
        }

        public int Print()
        {
            try
            {
                Neusoft.NFC.Interface.Classes.Print print = null;
                try
                {
                    print = new Neusoft.NFC.Interface.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);

                    return -1;
                }

                Neusoft.HISFC.Object.Base.PageSize ps = new Neusoft.HISFC.Object.Base.PageSize("MZGH", 0, 0);               
                ////ֽ�ſ��
                //ps.Width = this.Width;
                ////ֽ�Ÿ߶�
                //ps.Height = this.Height;
                ps.Printer = "MZGH";
                //�ϱ߾�
                ps.Top = 0;
                //��߾�
                ps.Left = 0;               
                print.SetPageSize(ps);               
                print.PrintPage(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 1;
            }

            return 1;
        }
        public int Clear()
        {
            return 0;
        }

        public void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public System.Data.IDbTransaction Trans
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
        public int PrintView()
        {
            Neusoft.NFC.Interface.Classes.Print print = new Neusoft.NFC.Interface.Classes.Print();
            print.PrintPreview(0, 0, this);
            return 0;
        }
        #region �����ӡ�ú���
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

        /// <summary>
        /// ��ӡ�õı�ǩ����
        /// </summary>
        public Collection<Label> lblPrint;
        /// <summary>
        /// Ԥ���õı�ǩ����
        /// </summary>
        public Collection<Label> lblPreview;

        /// <summary>
        /// ��ʼ���վ�
        /// </summary>
        /// <remarks>
        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
        /// </remarks>
        private void InitReceipt(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "Neusoft.NFC.Interface.Controls.NeuLabel")
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
        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
        /// </remarks>
        private void InitReceipt()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            foreach (Control c in this.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "Neusoft.NFC.Interface.Controls.NeuLabel")
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
        #endregion
    }
}
