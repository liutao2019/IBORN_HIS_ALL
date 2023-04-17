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

namespace InterfaceInstanceDefault.IRegPrint
{
    public partial class IRegPrintDefault : UserControl, FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        public IRegPrintDefault()
        {
            InitializeComponent();
        }

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ���ô�ӡֵ
        /// </summary>
        /// <param name="register">�Һ�ʵ��</param>
        /// <returns></returns>
        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {

            this.lblHosptialName.Text = manageIntegrate.GetHospitalName();

            MessageBox.Show("���¼����ţ�"+register.PID.CardNO);
            try
            {
                // �����ã��������ڹҺ��ܷ�����
                // ����ӡʱ����һ���ӡ
                decimal decCardFee = 0;
                if (register.User01 == "CARDFEE")
                {
                    decCardFee = FS.FrameWork.Function.NConvert.ToDecimal(register.User02);
                }


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
                this.lblCostsum.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                    register.PubCost + register.PayCost + register.OwnCost + decCardFee, 2) +
                    "Ԫ";
                //��д
                this.lblUpperCostSum.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(
                   register.PubCost + register.PayCost + register.OwnCost + decCardFee
                    );
                //�Һ�����
                this.lblRegDate.Text = register.DoctorInfo.SeeDate.ToString();
                string medicalTypeName = string.Empty;

                //this.lblPayCostTitle.Visible = false;
                //this.lblOwnCostTitle.Visible = false;
                //this.lblIndividualBalanceTitle.Visible = false;
                //register.Pact.ID = "2";
                
                //ҽ�����
                this.lblPactName.Text = register.Pact.Name + medicalTypeName;

                //�Һŷ� 
                this.lblRegFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.RegFee, 2) +
                    "Ԫ";
                //����
                this.lblChkFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.ChkFee + register.RegLvlFee.PubDigFee + register.RegLvlFee.OwnDigFee, 2) +
                    "Ԫ";
                //�����ֲ� 
                this.lblOherFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.OthFee, 2) +
                    "Ԫ";

                // ��������
                if (decCardFee > 0)
                {
                    lblCardFeeTitle.Visible = true;
                    lblCardFee.Visible = true;

                    lblCardFee.Text = FS.FrameWork.Public.String.FormatNumberReturnString(decCardFee, 2) + "Ԫ";

                }


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
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();

                    FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
                    FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("MZGH");
                    if (pSize == null)
                    {
                        pSize = new FS.HISFC.Models.Base.PageSize("MZGH", 0, 0);
                        pSize.Printer = "MZGH";
                    }
                    print.SetPageSize(pSize);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);

                    return -1;
                }

                //FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZGH", 0, 0);
                //////ֽ�ſ��
                ////ps.Width = this.Width;
                //////ֽ�Ÿ߶�
                ////ps.Height = this.Height;
                //ps.Printer = "MZGH";
                ////�ϱ߾�
                //ps.Top = 0;
                ////��߾�
                //ps.Left = 0;
                //print.SetPageSize(ps);
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
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
        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
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
        #endregion
    }
}
