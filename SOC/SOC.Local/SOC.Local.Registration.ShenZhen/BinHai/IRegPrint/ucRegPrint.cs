using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.FrameWork.Models;
using Neusoft.HISFC.Models.Registration;
using System.Collections;

namespace Neusoft.SOC.Local.Registration.ShenZhen.BinHai.IRegPrint
{
    public partial class ucRegPrint : UserControl
    {
        public ucRegPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���ô�ӡֵ
        /// </summary>
        /// <param name="register">�Һ�ʵ��</param>
        /// <returns></returns>
        public int SetPrintValue(Neusoft.HISFC.Models.Registration.Register register)
        {

            this.lblHosptialName.Text = Neusoft.FrameWork.Management.Connection.Hospital.Name;

            MessageBox.Show("���¼����ţ�" + register.PID.CardNO);
            try
            {
                // �����ã��������ڹҺ��ܷ�����
                // ����ӡʱ����һ���ӡ
                decimal decCardFee = 0;
                if (register.User01 == "CARDFEE")
                {
                    decCardFee = Neusoft.FrameWork.Function.NConvert.ToDecimal(register.User02);
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
                this.lblCostsum.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.PubCost + register.PayCost + register.OwnCost + decCardFee, 2) +
                    "Ԫ";
                //��д
                this.lblUpperCostSum.Text = Neusoft.FrameWork.Public.String.LowerMoneyToUpper(
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
                this.lblRegFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.RegFee, 2) +
                    "Ԫ";
                //����
                this.lblChkFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.ChkFee + register.RegLvlFee.PubDigFee + register.RegLvlFee.OwnDigFee, 2) +
                    "Ԫ";
                //�����ֲ� 
                this.lblOherFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.OthFee, 2) +
                    "Ԫ";

                // ��������
                if (decCardFee > 0)
                {
                    lblCardFeeTitle.Visible = true;
                    lblCardFee.Visible = true;

                    lblCardFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(decCardFee, 2) + "Ԫ";

                }


                SetToPrintMode();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        #region �����ӡ�ú���
        /// <summary>
        /// ����Ϊ��ӡģʽ
        /// </summary>
        private void SetToPrintMode()
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
        /// ��ӡ�õı�ǩ����
        /// </summary>
        private Collection<Label> lblPrint;
        /// <summary>
        /// Ԥ���õı�ǩ����
        /// </summary>
        private Collection<Label> lblPreview;

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
                    c.GetType().FullName == "Neusoft.FrameWork.WinForms.Controls.NeuLabel")
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

        #endregion
    }
}
