using System;
using System.Collections.Generic;
using System.Linq;   //������ӵ�
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDLY.InjectBillPrint
{
    /// <summary>
    /// �д���Ժע�䵥��ӡ
    /// </summary>
    public partial class ucOrderInjectBill : UserControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public ucOrderInjectBill()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ÿҳ��ʾ������
        /// </summary>
        private int iSet = 6;

        /// <summary>
        /// �ϲ���һ�е�Ԫ�����ʼ�к�
        /// </summary>
        private int spanRowIndex = 1;

        /// <summary>
        /// ����
        /// </summary>
        private decimal zhenChaFee = 0m;

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        private Hashtable comboHash = new Hashtable();

        /// <summary>
        ///����ά��ҵ���
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Constant constMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// ����ҵ����
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
        #endregion


        /// <summary>
        /// ��ӡ����Ժע�䵥
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        private void PrintAllPage(ArrayList alData, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {
            GetHospLogo();
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));
                for (int i = 1; i <= icount; i++)
                {
                    //if (i != icount)
                    //{
                    //alPrint = alData.GetRange(iSet * (i - 1), iSet);
                    alPrint = alData;
                    this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, isPreview);
                    //}
                    //else
                    //{
                    //    int num = alData.Count % iSet;
                    //    if (alData.Count % iSet == 0)
                    //    {
                    //        num = iSet;
                    //    }
                    //    //alPrint = alData.GetRange(iSet * (i - 1), num);
                    //    this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct);
                    //}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ӡ����!" + e.Message);
                return;
            }
        }

        /// <summary>
        /// ��ӡһ��Ժע�䵥
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="current"></param>
        /// <param name="total"></param>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        private void PrintOnePage(ArrayList alData, int current, int total, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {


            this.comboHash = new Hashtable();

            try
            {
                spanRowIndex = 0;

                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }


                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                decimal tmpCharge = 0.0M;
                this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Border = bottomBorder;

                ArrayList alFee = new ArrayList();
                if (alData.Count > 0)
                {
                    alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(regObj.ID, ((Neusoft.HISFC.Models.Order.OutPatient.Order)alData[0]).ReciptSequence, "ALL");
                }

                //��ֵ����ӡ
                for (int i = 0; i < alData.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)alData[i];

                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);


                    if (!comboHash.Contains(order.Combo.ID))
                    {
                        if (alFee != null && alFee.Count >= 1)
                        {
                            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                            {
                                if (itemlist.Item.IsMaterial && itemlist.Order.Combo.ID == order.Combo.ID)
                                {
                                    tmpCharge += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//ע���
                                }

                            }
                        }
                        comboHash.Add(order.Combo.ID, order.Combo.ID);
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = order.SubCombNO.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name + "[" + order.Item.Specs + "]" + outOrderMgr.TransHypotest(order.HypoTest);//ҩƷ��+���
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//�÷�

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = order.Frequency.Name;//��ҩʱ��
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;//����
                    }
                    catch
                    {
                    }

                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = order.InjectCount.ToString();// ����
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = " ";//�����÷�
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = " ";//����
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Combo.ID.ToString();//����
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = order.Memo;//��ע
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));


                    //������ʾ
                    if (neuSpread1_Sheet1.RowCount - 1 >= (current - 1) * iSet &&
                        neuSpread1_Sheet1.RowCount - 1 < current * iSet)
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = false;
                    }
                }

                this.lbCard.Text = regObj.PID.CardNO;//��ˮ��

                this.npbBarCode.Image = SOC.Public.Function.CreateBarCode(regObj.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
                this.lbName.Text = regObj.Name;//����
                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //��ӡʱ��

                this.lbAge.Text = constMgr.GetAge(regObj.Birthday);//����
                this.lbSex.Text = regObj.Sex.Name;//�Ա�
                this.lblPage.Text = current.ToString() + "/" + total.ToString() + "ҳ";

                this.lbInvoiceNo.Text = SOC.HISFC.BizProcess.Cache.Common.GetDept(reciptDept.ID).Name;

                ////����fp���������
                this.neuDoctName.Text = "ҽ��ǩ����" + reciptDoct.ID + "/" +
                    SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.ID);//ҽ������ 
                this.neuCharge.Text = "ע��ѣ�" + tmpCharge.ToString();//���� temp

                //����Ϻ�
                Neusoft.HISFC.Components.Common.Classes.Function.DrawCombo(neuSpread1_Sheet1, 9, 2);
                if (!isPreview)
                {
                    PrintPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡע�䵥����\r\n" + ex.Message);
            }
        }




        private void PrintOnePage(IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, int current, int total, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {


            this.comboHash = new Hashtable();

            try
            {
                spanRowIndex = 0;

                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }


                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                decimal tmpCharge = 0.0M;
                this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Border = bottomBorder;

                //��ֵ����ӡ
                for (int i = 0; i < orderList.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)orderList[i];

                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);


                    if (!comboHash.Contains(order.Combo.ID))
                    {
                        ArrayList alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(order.Combo.ID, regObj.ID, "ALL");
                        if (alFee != null && alFee.Count >= 1)
                        {
                            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                            {
                                if (itemlist.Item.IsMaterial)
                                {
                                    tmpCharge += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//ע���
                                }

                            }
                        }
                        comboHash.Add(order.Combo.ID, order.Combo.ID);
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = order.SubCombNO.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name + "[" + order.Item.Specs + "]" + outOrderMgr.TransHypotest(order.HypoTest);//ҩƷ��+���
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//�÷�

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = order.Frequency.Name;//��ҩʱ��
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;//����
                    }
                    catch
                    {
                    }

                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = order.InjectCount.ToString();// ����
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = " ";//�����÷�
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = " ";//����
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Combo.ID.ToString();//����
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = order.Memo;//��ע
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));


                    //������ʾ
                    if (neuSpread1_Sheet1.RowCount - 1 >= (current - 1) * iSet &&
                        neuSpread1_Sheet1.RowCount - 1 < current * iSet)
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Visible = false;
                    }
                }

                this.lbCard.Text = regObj.PID.CardNO;//��ˮ��
                this.npbBarCode.Image = SOC.Public.Function.CreateBarCode(regObj.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
                this.lbName.Text = regObj.Name;//����

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //��ӡʱ��

                this.lbAge.Text = constMgr.GetAge(regObj.Birthday);//����
                this.lbSex.Text = regObj.Sex.Name;//�Ա�
                this.lblPage.Text = current.ToString() + "/" + total.ToString() + "ҳ";

                this.lbInvoiceNo.Text = SOC.HISFC.BizProcess.Cache.Common.GetDept(reciptDept.ID).Name;

                ////����fp���������
                this.neuDoctName.Text = "ҽ��ǩ����" + reciptDoct.ID + "/" +
                    SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.ID);//ҽ������ 
                this.neuCharge.Text = "ע��ѣ�" + tmpCharge.ToString();//���� temp

                //����Ϻ�
                Neusoft.HISFC.Components.Common.Classes.Function.DrawCombo(neuSpread1_Sheet1, 9, 2);
                if (!isPreview)
                {
                    PrintPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡע�䵥����\r\n" + ex.Message);
            }
        }


        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();

            print.IsLandScape = true;

            print.SetPageSize(Neusoft.SOC.Local.Order.ZhuHai.ZDLY.Common.Function.GetPrintPage(true));

            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsDataAutoExtend = false;
            //����Ա�����ǲ���͵���Ԥ����ӡ
            if (Neusoft.SOC.Local.Order.ZhuHai.ZDLY.Common.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }


        #region IOutPatientOrderPrint ��Ա
        /// <summary>
        /// ʵ�ִ�ӡ�ӿ�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IList"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return -1;
        }

        /// <summary>
        /// ҽ��վ��ӡע�䵥
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            List<Neusoft.HISFC.Models.Order.OutPatient.Order> dayorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            string type = "Q";

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> injectDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInnerInjectUsage(order.Usage.ID))
                {
                    type = Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetUsageSystemType(order.Usage.ID) == "IVD" ? "IVD" : "Q";

                    if (injectDictionary.ContainsKey(type))
                    {
                        injectDictionary[type].Add(order);
                    }
                    else
                    {
                        dayorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                        dayorderList.Add(order);
                        injectDictionary.Add(type, dayorderList);
                    }
                }
            }
            foreach (string usage in injectDictionary.Keys)
            {
                ArrayList printList = new ArrayList(injectDictionary[usage]);

                printList.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(printList, regObj, reciptDept, reciptDoct, isPreview);
            }

            /*
            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> PrintDic = null;
            foreach (string days in injectDictionary.Keys)
            {
                PrintDic = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
                for (int t = 0; t < injectDictionary[days].Count; t++)
                {
                    if (PrintDic.ContainsKey(injectDictionary[days][t].Usage.ID))
                    {
                        PrintDic[injectDictionary[days][t].Usage.ID].Add(injectDictionary[days][t]);

                    }
                    else
                    {
                        List<Neusoft.HISFC.Models.Order.OutPatient.Order> usageorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                        usageorderList.Add(injectDictionary[days][t]);
                        PrintDic.Add(injectDictionary[days][t].Usage.ID, usageorderList);
                    }
                    
                }

                foreach (string usage in PrintDic.Keys)
                {
                    ArrayList printList = new ArrayList(PrintDic[usage]);

                    printList.Sort(new CompareApplyOutByCombNO());

                    PrintAllPage(printList, regObj, reciptDept, reciptDoct);
                }

            }
            */
            return 1;
        }
        #endregion

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // ��Ԫ���ػ�
            Pen pp = new Pen(Color.Red);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1);
        }

        private void GetHospLogo()
        {
            try
            {
                System.IO.MemoryStream image = new System.IO.MemoryStream(((Neusoft.HISFC.Models.Base.Hospital)this.outOrderMgr.Hospital).HosLogoImage);
                this.picLogo.Image = Image.FromStream(image);

            }
            catch
            {

            }
        }

        private void ucOrderInjectBill_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.Columns[6].Visible = false;
            this.neuSpread1_Sheet1.Columns[7].Visible = false;
            this.picLogo.Visible = false;
        }

        #region IOutPatientOrderPrint ��Ա

        public void PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            PrintOnePage(orderList, 1, 1, regObj, reciptDept, reciptDoct, isPreview);
        }

        public void SetPage(string pageStr)
        {
            this.lblPage.Visible = true;
            this.lblPage.Text = pageStr;
            return;
        }
        #endregion
    }

    #region ������
    /// <summary>
    /// ������������
    /// </summary>
    public class CompareApplyOutByCombNO : IComparer
    {
        /// <summary>
        /// ���򷽷�
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            Neusoft.HISFC.Models.Order.OutPatient.Order o1 = (x as Neusoft.HISFC.Models.Order.OutPatient.Order).Clone();
            Neusoft.HISFC.Models.Order.OutPatient.Order o2 = (y as Neusoft.HISFC.Models.Order.OutPatient.Order).Clone();

            Int32 oX = o1.SortID;          //��������
            Int32 oY = o2.SortID;          //��������

            int nComp;

            if (oX == null)
            {
                nComp = (oY != null) ? -1 : 0;
            }
            else if (oY == null)
            {
                nComp = 1;
            }
            else
            {
                nComp = oX - oY;
            }

            return nComp;
        }

    }
    #endregion
}
