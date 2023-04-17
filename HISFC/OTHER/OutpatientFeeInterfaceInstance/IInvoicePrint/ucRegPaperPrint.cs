using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ReceiptPrint
{
    public partial class ucRegPaperPrint : UserControl, Neusoft.HISFC.Integrate.IRecipePrint
    {
        public ucRegPaperPrint()
        {
            InitializeComponent();
        }



        #region IRecipePrint ��Ա

        public void PrintRecipe()
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
                    return;
                }
                string paperName = string.Empty;
                //if (this.InvoiceType == "MZ02")
                //{
                paperName = "MZBLB";

                //}
                //else if (this.InvoiceType == "MZ01")
                //{
                //    paperName = "MZYB";
                //}

                Neusoft.HISFC.Object.Base.PageSize ps = new Neusoft.HISFC.Object.Base.PageSize(paperName, 0, 0);
                ////ֽ�ſ��
                //ps.Width = this.Width;
                ////ֽ�Ÿ߶�
                //ps.Height = this.Height;
                ps.Printer = "MZBLB";
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
            }

        }

        public string RecipeNO
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public void SetPatientInfo(Neusoft.HISFC.Object.Registration.Register register)
        {
            this.lblAdress.Text = register.AddressHome;
            this.lblAge.Text = register.Age;
            this.lblGms.Text = register.User03.ToString();
            this.lblName.Text = register.Name;
            this.lblPaytype.Text = register.Pact.Name;
            this.lblSex.Text = register.Sex.Name;
        }

        #endregion
    }
}
