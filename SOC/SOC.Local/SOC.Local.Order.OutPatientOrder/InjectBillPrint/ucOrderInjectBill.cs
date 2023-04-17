using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.InjectBillPrint
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
    /// </summary>
    public partial class ucOrderInjectBill : UserControl,Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
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
        /// ����
        /// </summary>
        private decimal zhenChaFee = 0m;

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
        public ucOrderInjectBill()
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
        private void PrintAllPage(ArrayList alData, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct)
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
                        this.PrintOnePage(alPrint, i, icount, regObj,reciptDept,reciptDoct);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct);
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
        private void PrintOnePage(ArrayList alData, int current, int total, Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct)
        {
            this.neuLabel2.Text = strHosName  + "Ժע��";
            this.lblZhenChaFee.Visible = false;
            if (zhenChaFee > 0)
            {
                this.lblZhenChaFee.Visible = true;
                this.lblZhenChaFee.Text = string.Format("�û�����֧������໤��({0}Ԫ)", zhenChaFee);
            }
            try
            {
                spanRowIndex = 0;
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

                FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Gray, 1, false, false, false, true);
                FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);

                //��ֵ����ӡ
                for (int i = 0; i < alData.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order order = (Neusoft.HISFC.Models.Order.OutPatient.Order)alData[i];

                    Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
                    //Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = outpatientFeeMgr.GetFeeItemList(info.RecipeNO, info.SequenceNO);
                    //Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order(); //Common.Function.GetOrder(info.OrderNO);
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
                    }

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = showGroupNO.ToString();


                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name;

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;//����
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//�÷�
                    }
                    catch { }
                    string hypoTest = "";

                    //���� �ο� Ҫ������ ���Բ�����ʾƤ����Ϣ
                    if (order != null)
                    {
                        hypoTest = this.outOrderMgr.TransHypotest(order.HypoTest);
                        //if (order.HypoTest == 1)
                        //{
                        //    hypoTest = "��Ƥ��";
                        //}
                        //else if (order.HypoTest == 2)
                        //{
                        //    hypoTest = "��ҪƤ��";
                        //}
                        //else if (order.HypoTest == 3)
                        //{
                        //    hypoTest = "����";
                        //}
                        //else if (order.HypoTest == 4)
                        //{
                        //    hypoTest = "����";
                        //}
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = hypoTest; //Ƥ��
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = order.Item.Specs;//���
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = order.Frequency.ID;//����
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = order.HerbalQty.ToString();// ����

                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = order.Memo;//����
                    }
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = order.Qty.ToString() + order.DoseUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = " ";//��ʼʱ��
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 11].Text = " ";//ִ����

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }

                //this.lbInvoiceNo.Text = "��Ʊ�ţ�" + drugRecipe.InvoiceNO;
                this.lbCard.Text = regObj.Card.ID;
                this.lbName.Text = regObj.Name;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //��ӡʱ��
                Neusoft.FrameWork.Management.DataBaseManger dataBaseManger = new Neusoft.FrameWork.Management.DataBaseManger();

                this.lbAge.Text = dataBaseManger.GetAge(regObj.Birthday);
                this.lbSex.Text = regObj.Sex.Name;
                this.lbPage.Text = "��" + current.ToString() + "ҳ" + "/" + "��" + total.ToString() + "ҳ";

                ////����fp���������
                this.neuDoctName.Text = "ҽ����" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.Name);//ҽ������ 
                //this.neuChargeOper.Text = "�շ�Ա��" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.FeeOper.ID); ;//�շ�Ա

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
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 800, 1110 / 3));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPreview(5, 5, this);
            }
        }

        //public void PrintInjectBill(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        //{
        //    Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        //    Neusoft.HISFC.BizLogic.Manager.Constant constantMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();
        //    usageHelper.ArrayObject = constantMgr.GetAllList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);
        //    Neusoft.HISFC.BizLogic.Fee.Outpatient feeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        //    ArrayList alIM= new ArrayList();    //IM��ע��������һ��ע�䵥�������÷�������һ��ע�䵥
        //    ArrayList alOther = new ArrayList(); //����ע�����ҩƷ
        //    ArrayList alSpecial = new ArrayList();//�Ѿ�ά������Ҫ�ر�򵥵�ҩƷ


        //    ArrayList alFeeItemList = feeMgr.QueryFeeItemListsByInvoiceNO(drugRecipe.InvoiceNO);
        //    if (alFeeItemList!=null&&alFeeItemList.Count>0)
        //    {
        //        foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item in alFeeItemList)
        //        {
        //            if (item.Item.ID == "F00000040341")   //�˴�д���ˣ������Ŀ�Ӷ࣬���Ը�Ϊά��������lable��ʾ����Ҫ�޸�
        //            {
        //                zhenChaFee += item.FT.OwnCost + item.FT.PubCost + item.FT.PayCost;
        //            }
        //        }
        //    }

        //      ArrayList alSpecialInjectBill = constMgr.GetAllList("SpecialInjectBill");

        //    for (int index = alData.Count - 1; index > -1; index--)
        //    {
        //        Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[index] as Neusoft.HISFC.Models.Pharmacy.ApplyOut;
        //        if (!SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyOut.Usage.ID))
        //        {
        //            alData.RemoveAt(index);
        //        }
        //        else
        //        {
        //            Neusoft.HISFC.Models.Order.OutPatient.Order order = Neusoft.SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
        //            if (order != null)
        //            {
        //                applyOut.CombNO = order.SubCombNO.ToString();
        //            }
        //            else
        //            {
        //                applyOut.CombNO = "";
        //                return; //�ֹ�������ӡ,�Ҳ���������Ϣ����Ϊ���ֹ���
        //            }

        //            #region ��ע�������÷��ֵ���ӡ
        //            Neusoft.FrameWork.Models.NeuObject neuObject = usageHelper.GetObjectFromID(applyOut.Usage.ID);
        //            if (neuObject == null)
        //            {
        //                continue;
        //            }
        //            Neusoft.HISFC.Models.Base.Const usage = (Neusoft.HISFC.Models.Base.Const)neuObject;
        //            if (usage == null)
        //            {
        //                continue;
        //            }

        //            //[2011-6-20]zhaozf �����������õ�ҩƷҪ������
        //            bool isSpecial = false;
        //            if (alSpecialInjectBill!=null&&alSpecialInjectBill.Count>0)
        //            {
        //                foreach (Neusoft.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
        //                {
        //                    if (cnst.ID==applyOut.Item.ID)
        //                    {
        //                        alSpecial.Add(applyOut);
        //                        isSpecial = true;
        //                        break;
        //                    }
        //                }
        //            }
        //            if (!isSpecial)
        //            {

        //                if (usage.UserCode == "IM")//����ע��
        //                {
        //                    alIM.Add(applyOut);
        //                }
        //                else
        //                {
        //                    alOther.Add(applyOut);
        //                }
        //            }
        //            #endregion
        //        }
        //    }

        //    if (alSpecial.Count>0)
        //    {
        //        alSpecial.Sort(new CompareApplyOutByCombNO());
        //        PrintAllPage(alSpecial, drugRecipe);
        //    }

        //    if (alIM.Count > 0)
        //    {
        //        //����������
        //        alIM.Sort(new CompareApplyOutByCombNO());

        //        PrintAllPage(alIM, drugRecipe);
        //    }
        //    if (alOther.Count>0)
        //    {
        //         //����������
        //        alOther.Sort(new CompareApplyOutByCombNO());

        //        PrintAllPage(alOther, drugRecipe);
        //    }
        //}

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
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IList)
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
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();
            Neusoft.HISFC.BizLogic.Manager.Constant constantMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();
            usageHelper.ArrayObject = constantMgr.GetAllList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);
            //Neusoft.HISFC.BizLogic.Fee.Outpatient feeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

            ArrayList alIM = new ArrayList();    //IM��ע��������һ��ע�䵥�������÷�������һ��ע�䵥
            ArrayList alOther = new ArrayList(); //����ע�����ҩƷ
            ArrayList alSpecial = new ArrayList();//�Ѿ�ά������Ҫ�ر�򵥵�ҩƷ


            //ArrayList alFeeItemList = feeMgr.QueryFeeItemListsByInvoiceNO(drugRecipe.InvoiceNO);

            ArrayList alSpecialInjectBill = constMgr.GetAllList("SpecialInjectBill");

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (order.Item.ID == "F00000040341")   //�˴�д���ˣ������Ŀ�Ӷ࣬���Ը�Ϊά��������lable��ʾ����Ҫ�޸�
                {
                    //zhenChaFee += order.Item.FT.OwnCost + order.Item.FT.PubCost + order.Item.FT.PayCost;
                }

                if (SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
                {
                    #region ��ע�������÷��ֵ���ӡ
                    Neusoft.FrameWork.Models.NeuObject neuObject = usageHelper.GetObjectFromID(order.Usage.ID);
                    if (neuObject == null)
                    {
                        continue;
                    }
                    Neusoft.HISFC.Models.Base.Const usage = (Neusoft.HISFC.Models.Base.Const)neuObject;
                    if (usage == null)
                    {
                        continue;
                    }
                    //[2011-6-20]zhaozf �����������õ�ҩƷҪ������
                    bool isSpecial = false;
                    if (alSpecialInjectBill != null && alSpecialInjectBill.Count > 0)
                    {
                        foreach (Neusoft.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
                        {
                            if (cnst.ID == order.Item.ID)
                            {
                                alSpecial.Add(order);
                                isSpecial = true;
                                break;
                            }
                        }
                    }
                    if (!isSpecial)
                    {
                        if (usage.UserCode == "IM")//����ע��
                        {
                            alIM.Add(order);
                        }
                        else
                        {
                            alOther.Add(order);
                        }
                    }
                    #endregion
                }
            }
            if (alSpecial.Count > 0)
            {
                alSpecial.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(alSpecial, regObj, reciptDept, reciptDoct);
            }
            if (alIM.Count > 0)
            {
                //����������
                alIM.Sort(new CompareApplyOutByCombNO());

                PrintAllPage(alIM, regObj, reciptDept, reciptDoct);
            }
            if (alOther.Count > 0)
            {
                //����������
                alOther.Sort(new CompareApplyOutByCombNO());

                PrintAllPage(alOther, regObj, reciptDept, reciptDoct);
            }
            return 1;
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