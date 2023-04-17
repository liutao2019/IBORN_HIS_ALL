using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.InjectBillPrint
{
    /// <summary>
    /// ע�䵥��ӡ�ӿ�
    /// </summary>
    public partial class ucOrderInjectBill : UserControl,
        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint,
        FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint
    {
        #region ����
        /// <summary>
        /// ÿҳ��ʾ������
        /// </summary>
        private int iSet = 10;

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
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

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
        private void PrintAllPage(ArrayList alData, FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct)
        {
            try
            {
                //��ȡҽԺ����
                bool blPreview = false;
                strHosName = this.constMgr.GetHospitalName();
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, blPreview);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, blPreview);
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
        /// ��ӡ����Ժע�䵥
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        private void PrintAllPage(ArrayList alData, FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {
            try
            {
                //��ȡҽԺ����
                strHosName = this.constMgr.GetHospitalName();
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alData.GetRange(iSet * (i - 1), iSet);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, isPreview);
                    }
                    else
                    {
                        int num = alData.Count % iSet;
                        if (alData.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = alData.GetRange(iSet * (i - 1), num);
                        this.PrintOnePage(alPrint, i, icount, regObj, reciptDept, reciptDoct, isPreview);
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
        private void PrintOnePage(ArrayList alData, int current, int total, FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, bool isPreview)
        {
            showGroupNO = 0;//�����ʼ�����������´�ӡ��Ż����
            this.neuLabel2.Text = strHosName + "Ժע��";
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
                    FS.HISFC.Models.Order.OutPatient.Order order = (FS.HISFC.Models.Order.OutPatient.Order)alData[i];

                    FS.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
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

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = order.Item.Name + "\n[" + order.Item.Specs + "]";
                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Height *= 2;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;//����
                    try
                    {
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = (SOC.HISFC.BizProcess.Cache.Common.GetUsage(order.Usage.ID)).Name;//�÷�
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = " ";
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = " ";
                    }
                    catch { }

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }

                this.lbCard.Text = regObj.PID.CardNO;
                this.lbName.Text = regObj.Name;

                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //��ӡʱ��
                FS.FrameWork.Management.DataBaseManger dataBaseManger = new FS.FrameWork.Management.DataBaseManger();

                this.lbAge.Text = dataBaseManger.GetAge(regObj.Birthday);
                this.lbSex.Text = regObj.Sex.Name;
                this.lbPage.Text = "��" + current.ToString() + "ҳ" + "/" + "��" + total.ToString() + "ҳ";

                ////����fp���������
                this.neuDoctName.Text = "ҽ����" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(reciptDoct.Name);//ҽ������ 
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPage(5, 5, this);
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
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
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
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            //[2012-08-20]ע�䵥���÷ֿ���ӡ
            FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            usageHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            //FS.HISFC.BizLogic.Fee.Outpatient feeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

            //ArrayList alIM = new ArrayList();    //IM��ע��������һ��ע�䵥�������÷�������һ��ע�䵥
            //ArrayList alOther = new ArrayList(); //����ע�����ҩƷ
            ArrayList al = new ArrayList();
            ArrayList alSpecial = new ArrayList();//�Ѿ�ά������Ҫ�ر�򵥵�ҩƷ


            //ArrayList alFeeItemList = feeMgr.QueryFeeItemListsByInvoiceNO(drugRecipe.InvoiceNO);

            ArrayList alSpecialInjectBill = constMgr.GetAllList("SpecialInjectBill");

            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
                {
                    #region ��ע�������÷��ֵ���ӡ
                    FS.FrameWork.Models.NeuObject neuObject = usageHelper.GetObjectFromID(order.Usage.ID);
                    if (neuObject == null)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
                    if (usage == null)
                    {
                        continue;
                    }
                    //[2011-6-20]zhaozf �����������õ�ҩƷҪ������
                    bool isSpecial = false;
                    if (alSpecialInjectBill != null && alSpecialInjectBill.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
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
                        //if (usage.UserCode == "IM")//����ע��
                        //{
                        //    alIM.Add(order);
                        //}
                        //else
                        //{
                        //    alOther.Add(order);
                        //}
                        al.Add(order);
                    }
                    #endregion
                }
            }
            if (alSpecial.Count > 0)
            {
                alSpecial.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(alSpecial, regObj, reciptDept, reciptDoct, isPreview);
            }
            //if (alIM.Count > 0)
            //{
            //    //����������
            //    alIM.Sort(new CompareApplyOutByCombNO());

            //    PrintAllPage(alIM, regObj, reciptDept, reciptDoct);
            //}
            //if (alOther.Count > 0)
            //{
            //    //����������
            //    alOther.Sort(new CompareApplyOutByCombNO());

            //    PrintAllPage(alOther, regObj, reciptDept, reciptDoct);
            //}
            if (al.Count > 0)
            {
                al.Sort(new CompareApplyOutByCombNO());
                PrintAllPage(al, regObj, reciptDept, reciptDoct, isPreview);
            }
            else
            {
                return -1;
            }
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion

        #region IInjectPrint ��Ա

        void FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint.Init(ArrayList alPrintData)
        {
            if (alPrintData == null || alPrintData.Count == 0)
            {
                return;
            }
            FS.HISFC.Models.Nurse.Inject inject = alPrintData[0] as FS.HISFC.Models.Nurse.Inject;
            FS.HISFC.Models.Registration.Register register = inject.Patient;

            this.neuRePrintTag.Visible = true;//��ʾ����
            //ҽ��ҵ���
            FS.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();
            ArrayList al = OrderManagement.QueryOrderByRecipeNO(register.ID, inject.Item.RecipeNO);
            FS.HISFC.Models.Order.Order order = al[0] as FS.HISFC.Models.Order.Order;
            FS.FrameWork.Models.NeuObject dept = order.ReciptDept;
            FS.FrameWork.Models.NeuObject doc = order.ReciptDoctor;
            this.PrintAllPage(al, register, dept, doc);
        }

        #endregion

        #region IOutPatientOrderPrint Members


        public void SetPage(string pageStr)
        {
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
            FS.HISFC.Models.Order.OutPatient.Order o1 = (x as FS.HISFC.Models.Order.OutPatient.Order).Clone();
            FS.HISFC.Models.Order.OutPatient.Order o2 = (y as FS.HISFC.Models.Order.OutPatient.Order).Clone();

            Int32 oX = o1.SortID;
            Int32 oY = o2.SortID;

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
