using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Account.FoSi
{

    public partial class ucLabelPrint : UserControl, FS.HISFC.BizProcess.Interface.Account.IPrintLable
    {
        /// <summary>
        /// 门诊账户收取按金收据
        /// </summary>
        public ucLabelPrint()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
        FS.HISFC.BizLogic.Fee.Account accountManage = new FS.HISFC.BizLogic.Fee.Account();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        #endregion

        #region IPrintPrePayRecipe 成员

        /// <summary>
        /// 打印标签
        /// </summary>
        public void PrintLable(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            string cardNo = "";
            bool bTemp = accountManage.GetCardNoByMarkNo(accountCard.MarkNO,accountCard.MarkType,ref cardNo);
            if (!bTemp || cardNo == null)
            {
                MessageBox.Show("打印标签失败，请补打！");
                return;
            }
            this.npbCardNo1.Image = this.CreateBarCode(cardNo);
            this.npbCardNo2.Image = this.npbCardNo1.Image;
            //this.npbCardNo3.Image = this.npbCardNo1.Image;

            string pSex = "";
            if (accountCard.Patient.Sex.ID.ToString() == "M")
            {
                pSex = "男";
            }
            else if (accountCard.Patient.Sex.ID.ToString() == "F")
            {
                pSex = "女";
            }
            else
            {
                pSex = "";
            }
            this.lblInfo1.Text = accountCard.Patient.Name.ToString() + "   " + pSex;
            this.lblInfo2.Text = this.lblInfo1.Text;
            //this.lblInfo3.Text = this.lblInfo1.Text;


            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize pSize = null;
            pSize = pageSizeMgr.GetPageSize("MZBKBQ");

            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("MZBKBQ", this.Width, this.Height);
            }
            else
            {
                if (!string.IsNullOrEmpty(pSize.Printer))
                {
                    print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = pSize.Printer;
                }
            }

            print.SetPageSize(pSize);

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }


        #endregion

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbCardNo1.Size.Width, this.npbCardNo1.Height);
        }
    }
}
