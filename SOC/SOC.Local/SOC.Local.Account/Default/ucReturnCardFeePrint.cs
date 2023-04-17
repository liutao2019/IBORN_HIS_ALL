using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SOC.Local.Account.Default
{

    public partial class ucReturnCardFeePrint : UserControl, FS.HISFC.BizProcess.Interface.Account.IPrintCardFee
    {
        /// <summary>
        /// �����˻���ȡ�����վ�
        /// </summary>
        public ucReturnCardFeePrint()
        {
            InitializeComponent();
        }

        #region ����

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region IPrintPrePayRecipe ��Ա

        /// <summary>
        /// ��ӡ��Ʊ
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("ZLKSF", 600, 400);

            print.SetPageSize(pSize);

            print.PrintPage(0, 0, this);
        }

        /// <summary>
        /// ΪUC��ֵ
        /// </summary>
        /// <param name="prepay"></param>
        public void SetValue(FS.HISFC.Models.Account.AccountCardFee cardFee)
        {
            lblRecipeNO.Text = cardFee.InvoiceNo.TrimStart('0');
            lblDate.Text = cardFee.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
            lblName.Text = cardFee.Patient.Name;
            lblCardNO.Text = cardFee.CardNo;
            lblCardInfo.Text = "��" + cardFee.MarkType.Name + "��" + cardFee.MarkNO;

            lblMoney.Text = cardFee.Tot_cost.ToString() + " Ԫ";
            this.lblCaption.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(cardFee.Tot_cost);

            this.lblOper.Text = cardFee.Oper.ID.Substring(2);
            this.lblHospalName.Text = this.managerIntegrate.GetHospitalName();

            // �����ʶ
            this.lblPrintState.Text = cardFee.User01;
        }

        #endregion
    }
}
