using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neusoft.FrameWork.Function;
using Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.TreatmentBillPrint
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ xiaohf 2012��6��28��19:52:42
    /// </summary>
    public partial class ucTreatmentBill : UserControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        #region ����
        /// <summary>
        /// ÿҳ��ʾ������
        /// </summary>
        private int iSet = 8;

        /// <summary>
        /// ��ǰ��ʾ���
        /// </summary>
        private int showGroupNO = 0;

        /// <summary>
        /// ��ǰ��¼����Ϻ�
        /// </summary>
        private string rememberComboNO = "";

        /// <summary>
        /// �ϲ���һ�е�Ԫ�����ʼ�к�
        /// </summary>
        private int spanRowIndex = 1;

        /// <summary>
        /// ҽԺ����
        /// </summary>
        string strHosName = string.Empty;

        /// <summary>
        ///����ά��ҵ���
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Constant constMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public ucTreatmentBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ӡ����Ժע�䵥
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        private void PrintAllPage(ArrayList alData, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {
            try
            {
                //��ȡҽԺ����
                strHosName = this.constMgr.GetHospitalName();
                ArrayList alPrint = new ArrayList();
                int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));
                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct,isPreview);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct,isPreview);
                    }
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
        private void PrintOnePage(ArrayList alData, int current, int total, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct,bool isPreview)
        {
            GetHospLogo();
            try
            {
                spanRowIndex = 0;

                decimal totCost = 0;
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }

                //���ö���
                for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    }

                    this.neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }

                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
                FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);

                //��ֵ����ӡ
                for (int i = 0; i < alData.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)alData[i];

                    Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = bottomBorder;


                    //�޸����
                    if (order.Combo.ID != rememberComboNO)
                    {
                        rememberComboNO = order.Combo.ID;
                        spanRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                        showGroupNO++;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 3].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 6].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 7].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 10].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 11].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    }

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = showGroupNO.ToString();


                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name;

                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = order.Qty.ToString();// ����

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = order.Item.Price.ToString("F2");// ����

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = (order.Item.Price*order.Qty).ToString("F2");// ����


                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = (SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(order.Frequency.ID) * order.HerbalQty).ToString();// ����
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//�÷�
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Memo;
                    }
                    catch 
                    {
                    
                    }

                    totCost += order.Item.Price * order.Qty;
                    //totCost += order.FT.OwnCost+order.FT.PayCost+order.FT.PubCost;

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }

                //foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                //{
                //    if (!itemlist.Item.IsMaterial)
                //    {
                //        itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                //        phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//ҩƷ���
                //        ownPhaMoney += itemlist.FT.OwnCost;
                //    }
                //    else
                //    {
                //        injectMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//ע���
                //    }


                //}


                #region ������Ϣ
                this.lbCard.Text = regObj.PID.CardNO;
                this.lblClinicCode.Text = regObj.ID;
                this.lbName.Text = regObj.Name;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //��ӡʱ��
                Neusoft.FrameWork.Management.DataBaseManger dataBaseManger = new Neusoft.FrameWork.Management.DataBaseManger();

                this.lbAge.Text = dataBaseManger.GetAge(regObj.Birthday);
                this.lbSex.Text = regObj.Sex.Name;

                this.lblDoctName.Text = regObj.SeeDoct.ID + "/" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID);

                this.lblMoney.Text = totCost.ToString("F2")+"Ԫ";

                this.lbPage.Text = "��" + current.ToString() + "ҳ" + "/" + "��" + total.ToString() + "ҳ";
                #endregion

                if (!isPreview)
                {
                    PrintPage();
                }
            
            }
            catch { }
        }



        private void PrintOnePage(IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, int current, int total, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {
            GetHospLogo();
            try
            {
                spanRowIndex = 0;

                decimal totCost = 0;
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }

                //���ö���
                for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
                {
                    if (i == 1)
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    }

                    this.neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }

                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
                FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);

                //��ֵ����ӡ
                for (int i = 0; i < orderList.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)orderList[i];

                    Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = bottomBorder;


                    //�޸����
                    if (order.Combo.ID != rememberComboNO)
                    {
                        rememberComboNO = order.Combo.ID;
                        spanRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
                        showGroupNO++;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 3].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 6].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 7].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 10].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                        this.neuSpread1_Sheet1.Cells[spanRowIndex, 11].RowSpan = this.neuSpread1_Sheet1.RowCount - spanRowIndex;
                    }

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = showGroupNO.ToString();


                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name;

                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = order.Qty.ToString();// ����

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = order.Item.Price.ToString("F2");// ����

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = (order.Item.Price * order.Qty).ToString("F2");// ����


                        //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = (SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(order.Frequency.ID) * order.HerbalQty).ToString();// ����
                        if (order.Usage != null && !string.IsNullOrEmpty(order.Usage.ID))
                        {
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//�÷�
                        }
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Memo;
                    }
                    catch
                    {

                    }

                    totCost += order.Item.Price * order.Qty;
                    //totCost += order.FT.OwnCost+order.FT.PayCost+order.FT.PubCost;

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }

                //foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                //{
                //    if (!itemlist.Item.IsMaterial)
                //    {
                //        itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                //        phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//ҩƷ���
                //        ownPhaMoney += itemlist.FT.OwnCost;
                //    }
                //    else
                //    {
                //        injectMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//ע���
                //    }


                //}


                #region ������Ϣ
                this.lbCard.Text = regObj.PID.CardNO;
                this.lblClinicCode.Text = regObj.ID;
                this.lbName.Text = regObj.Name;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //��ӡʱ��
                Neusoft.FrameWork.Management.DataBaseManger dataBaseManger = new Neusoft.FrameWork.Management.DataBaseManger();

                this.lbAge.Text = dataBaseManger.GetAge(regObj.Birthday);
                this.lbSex.Text = regObj.Sex.Name;

                this.lblDoctName.Text = regObj.SeeDoct.ID + "/" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID);

                this.lblMoney.Text = totCost.ToString("F2") + "Ԫ";

                //this.lbPage.Text = "��" + current.ToString() + "ҳ" + "/" + "��" + total.ToString() + "ҳ";
                #endregion

                if (!isPreview)
                {
                    PrintPage();
                }

            }
            catch { }
        }


        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 575, 800));
            //print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 790));
            //print.PrintDocument.DefaultPageSettings.Landscape = true;
            print.IsLandScape = true;
            print.SetPageSize(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common.Function.getPrintPage(true));



            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            //����Ա�����ǲ���͵���Ԥ����ӡ
            if (Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
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
            Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();
            Neusoft.HISFC.BizLogic.Manager.Constant constantMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();
            usageHelper.ArrayObject = constantMgr.GetAllList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);

            ArrayList alSpecial = new ArrayList();
            ArrayList alSpecialInjectBill = constMgr.GetAllList("TreatmentItem"); //�Ѿ�ά������Ҫ�ر�򵥵�ҩƷ

            Hashtable hsPrint = new Hashtable();
            //����ִ�п��ҷ���
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                //�����÷���ӡ
                if (SOC.HISFC.BizProcess.Cache.Common.IsCure(order.Usage.ID))
                {
                    if (!hsPrint.Contains(order.ExeDept.ID))
                    {
                        ArrayList al = new ArrayList();
                        al.Add(order);
                        hsPrint.Add(order.ExeDept.ID, al);
                    }
                    else
                    {
                        ArrayList al = hsPrint[order.ExeDept.ID] as ArrayList;
                        al.Add(order);
                        hsPrint[order.ExeDept.ID] = al;
                    }
                }
                else if (alSpecialInjectBill != null && alSpecialInjectBill.Count > 0)
                {
                    foreach (Neusoft.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
                    {
                        if (cnst.ID == order.Item.ID)
                        {
                            if (!hsPrint.Contains(order.ExeDept.ID))
                            {
                                ArrayList al = new ArrayList();
                                al.Add(order);
                                hsPrint.Add(order.ExeDept.ID, al);
                            }
                            else
                            {
                                ArrayList al = hsPrint[order.ExeDept.ID] as ArrayList;
                                al.Add(order);
                                hsPrint[order.ExeDept.ID] = al;
                            }

                            break;
                        }
                    }
                }

            
            }
            if (hsPrint.Count > 0)
            {
                foreach (string key in hsPrint.Keys)
                {
                    ArrayList al = hsPrint[key] as ArrayList;
                    al.Sort(new CompareApplyOutByCombNO());
                    PrintAllPage(al, regObj, reciptDept, reciptDoct, isPreview);
                }
            }          
            return 1;
        }
        #endregion

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // ��Ԫ���ػ�
            Pen pp = new Pen(Color.Red);
            e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1);
        }



        #region IOutPatientOrderPrint ��Ա



        public void PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.PrintOnePage(orderList, 1, 1, regObj, reciptDept, reciptDoct, isPreview);
        }

        #endregion

        #region IOutPatientOrderPrint ��Ա


        public void SetPage(string pageStr)
        {
            throw new NotImplementedException();
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