using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.DrugStore.FuYou.Outpatient
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
    /// </summary>
    public partial class ucInjectBill : UserControl
    {
        #region ����

        private int iSet = 8;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ucInjectBill()
        {
            InitializeComponent();
        }      

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="alData"></param>
        private void PrintAllPage(ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
           
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount, drugRecipe);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, drugRecipe);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ӡ����!" + e.Message);
                return;
            }
        }

        private void PrintOnePage(ArrayList alData, int current, int total, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            try
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }
                

                //��ƿ����
                int jpNum = 1;
                //��ֵ����ӡ
                for (int i = 0; i < alData.Count; i++)
                {
                    Neusoft.HISFC.Models.Pharmacy.ApplyOut info = (Neusoft.HISFC.Models.Pharmacy.ApplyOut)alData[i];

                    Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
                    //Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = outpatientFeeMgr.GetFeeItemList(info.RecipeNO, info.SequenceNO);
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(info.OrderNO);
                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    if (info.CombNO.Length <= 2)
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text = info.CombNO;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text = info.CombNO.Substring(info.CombNO.Length - 2, 2);
                    }
                    this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Name;

                    this.neuSpread1_Sheet1.Cells[0, 2].Text = info.DoseOnce.ToString().TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit;//����
                    this.neuSpread1_Sheet1.Cells[0, 3].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID);//�÷�
                    string hypoTest = "";
                    if (order != null)
                    {
                        if (order.HypoTest == 1)
                        {
                            hypoTest = "";
                        }
                        else if (order.HypoTest == 2)
                        {
                            hypoTest = "��ҪƤ��";
                        }
                        else if (order.HypoTest == 3)
                        {
                            hypoTest = "����";
                        }
                        else if (order.HypoTest == 4)
                        {
                            hypoTest = "����";
                        }
                    }
                    this.neuSpread1_Sheet1.Cells[0, 4].Text = hypoTest; //Ƥ��
                    this.neuSpread1_Sheet1.Cells[0, 5].Text = info.Item.Specs;//���
                    this.neuSpread1_Sheet1.Cells[0, 6].Text = Common.Function.GetFrequenceName(info.Frequency);//����
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[0, 7].Text = order.HerbalQty.ToString();// ����
                        this.neuSpread1_Sheet1.Cells[0, 8].Text = order.Memo;//����
                    }
                    this.neuSpread1_Sheet1.Cells[0, 9].Text = "";//��ʼʱ��
                    this.neuSpread1_Sheet1.Cells[0, 10].Text = "";//ִ����
                }

                this.lbInvoiceNo.Text = "��Ʊ�ţ�" + drugRecipe.InvoiceNO;
                this.lbCard.Text = drugRecipe.CardNO;
                this.lbName.Text = drugRecipe.PatientName;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //��ӡʱ��
                Neusoft.FrameWork.Management.DataBaseManger dataBaseManger = new Neusoft.FrameWork.Management.DataBaseManger();
                this.lbAge.Text = dataBaseManger.GetAge(drugRecipe.Age);
                this.lbSex.Text = drugRecipe.Sex.Name;
                this.lbPage.Text = "��" + current.ToString() + "ҳ" + "/" + "��" + total.ToString() + "ҳ";
                
                //����fp���������
                this.neuDoctName.Text = "ҽ����" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);//ҽ������ 
                this.neuChargeOper.Text = "�շ�Ա��" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.FeeOper.ID); ;//�շ�Ա

                PrintPage();
            }
            catch { }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 800, 1100 / 2));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }


        public void PrintDrugBill(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            for (int index = alData.Count - 1; index > -1; index--)
            {
                Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[index] as Neusoft.HISFC.Models.Pharmacy.ApplyOut;
                if (!SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyOut.Usage.ID))
                {
                    alData.RemoveAt(index);
                }
            }
            if (alData.Count > 0)
            {
                PrintAllPage(alData, drugRecipe);
            }
        }

    }
}
