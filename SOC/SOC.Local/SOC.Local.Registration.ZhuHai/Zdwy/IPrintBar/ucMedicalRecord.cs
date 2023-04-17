using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.Registration.GuangZhou.Zdly.IPrintBar
{
    /// <summary>
    /// 门诊病历号条码打印

    /// </summary>
    public partial class ucMedicalRecord : UserControl
    {
        public ucMedicalRecord()
        {
            InitializeComponent();
        }

        private string cardNo = string.Empty;

        public string CardNo
        {
            set
            {
                this.cardNo = value;
            }
            get
            {
                return this.cardNo;
            }
        }
        /// <summary>
        /// 门诊病历号赋值

        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="printNum"></param>
        /// <param name="registerType">登记类型：门诊 1 体检 3 </param>
        public void SetPrintValue(Neusoft.HISFC.Models.RADT.Patient regInfo, int printNum)
        {
                  //常数管理类
            Neusoft.HISFC.BizLogic.Manager.Constant conMger = new Neusoft.HISFC.BizLogic.Manager.Constant();
            string pactName= regInfo.Pact.Name;
            if (regInfo is Neusoft.HISFC.Models.Registration.Register)
            {
                if (regInfo.Pact.ID == "2")
                {
                    Neusoft.FrameWork.Models.NeuObject neuObj = new Neusoft.FrameWork.Models.NeuObject();
                    neuObj = conMger.GetConstant("SZPERSONTYPE", ((Neusoft.HISFC.Models.Registration.Register)regInfo).SIMainInfo.PersonType.ID);

                    pactName = neuObj.Name;
                }
            }

            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = true;
            this.GetCardBarCode(regInfo.PID.CardNO);
            this.neuPictureBox1.Image = b.Encode(BarcodeLib.TYPE.CODE128, this.CardNo, 140, 50);

            this.lbhospital.Text = Neusoft.FrameWork.Management.Connection.Hospital.Name;
            this.lblCard.Text = "门诊号:" + regInfo.PID.CardNO.ToString();
            this.lblName.Text = regInfo.Name;
            this.lblSex.Text = regInfo.Sex.Name + "  " + regInfo.Birthday.ToString("yyyy-MM-dd");
            
                this.lblBirthday.Text =pactName;

            this.pictureBox1.Image = Common.Function.Create2DBarcode(regInfo.PID.CardNO);

            for (int i = 1; i <= printNum; i++)
            {
                this.PrintRecord();
            }
        }

        /// <summary>
        /// 补打赋值

        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="printNum"></param>
         public void SetPrintValued(Neusoft.HISFC.Models.Registration.Register regInfo, int printNum)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = true;

            this.GetCardBarCode(regInfo.PID.CardNO);
            this.neuPictureBox1.Image = b.Encode(BarcodeLib.TYPE.CODE128, this.CardNo, 140, 50);
            this.lbhospital.Text = Neusoft.FrameWork.Management.Connection.Hospital.Name;
            this.lblCard.Text = regInfo.PID.CardNO.ToString();
            this.lblName.Text = regInfo.Name;
            this.lblSex.Text = regInfo.Sex.Name;
            this.lblBirthday.Text = regInfo.Birthday.ToString("yyyy-MM-dd");
            for (int i = 1; i <= printNum; i++)
            {
                this.PrintRecord();
            }
        }
        /// <summary>
        /// 通过病历号获取病历号的条码


        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private string GetCardBarCode(string CardNO)
        {
            //添加获取条码信息
            this.CardNo = CardNO;
            return CardNO.ToString();
        }
        /// <summary>
        /// 打印病历号

        /// </summary>
        private void PrintRecord()
        {

            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();

            Neusoft.HISFC.BizLogic.Manager.PageSize pm = new Neusoft.HISFC.BizLogic.Manager.PageSize();
            Neusoft.HISFC.Models.Base.PageSize ps = pm.GetPageSize("MZTM");
            if (ps == null)
            {
                ps = new Neusoft.HISFC.Models.Base.PageSize("MZTM", 185, 120);
                ps.Left = 0;
                ps.Top = 0;
                ps.Printer = "MZTM";
            }
            p.SetPageSize(ps);

            p.PrintPage(ps.Left, ps.Top, this);

        }
    }
}

