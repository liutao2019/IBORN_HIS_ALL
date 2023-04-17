using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;

namespace Neusoft.WinForms.Report.Material
{
    /// <summary>
    /// [��������: ������ⵥ��ӡ]
    /// [�� �� ��: ��ά]
    /// [����ʱ��: 2008-3-19]
    /// </summary>
    public partial class ucMatInputBill : UserControl, Neusoft.HISFC.BizProcess.Interface.Material.IBillPrint
    {
        #region ���췽��

        public ucMatInputBill()
        {
            InitializeComponent();
        }

        #endregion

        #region �ֶ�

        /// <summary>
        /// �������
        /// </summary>
        private int maxRowNO;

        /// <summary>
        /// ���ҹ�����
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Department detpManager = new Neusoft.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ���ʹ�˾���ҹ�����
        /// </summary>
        private Neusoft.HISFC.BizLogic.Material.ComCompany comManager = new Neusoft.HISFC.BizLogic.Material.ComCompany();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Person psManager = new Neusoft.HISFC.BizLogic.Manager.Person();

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        public int MaxRowNo
        {
            get
            {
                return this.maxRowNO;
            }
            set
            {
                this.maxRowNO = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="al">��ӡ��Ϣ</param>
        /// <param name="inow">���ڴ�ӡҳ��</param>
        /// <param name="icount">��ҳ��</param>
        /// <param name="operCode">����Ա����</param>
        /// <param name="kind">��ӡ����</param>
        private void Print(List<Neusoft.HISFC.Models.FeeStuff.Input> al, int inow, int icount, string operCode, string kind)
        {
            this.PrintInput(al, inow, icount, operCode);
        }

        /// <summary>
        /// ��ⵥ�ݴ�ӡ
        /// </summary>
        /// <param name="al"></param>
        /// <param name="inow"></param>
        /// <param name="icount"></param>
        /// <param name="operCode"></param>
        private void PrintInput(List<Neusoft.HISFC.Models.FeeStuff.Input> al, int inow, int icount, string operCode)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("�޴�ӡ����!");
                return;
            }
            #region LABLE��ֵ

            Neusoft.HISFC.Models.Material.Input input = (Neusoft.HISFC.Models.Material.Input)al[0];

            this.lbDept.Text += this.detpManager.GetDeptmentById(input.StoreBase.StockDept.ID);

            this.lbTitle.Text = this.detpManager.GetDeptmentById(input.StoreBase.StockDept.ID) + "������ⵥ";

            this.lbTime.Text = this.psManager.GetSysDateTime("yyyy.MM.dd");

            this.lbPageNum.Text = "��" + inow.ToString() + "ҳ/��" + icount.ToString() + "ҳ";

            #endregion


            decimal sum4 = 0;
            decimal sum5 = 0;
            decimal sum6 = 0;
            decimal sum7 = 0;
            decimal sum8 = 0;
            this.sheetView1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.sheetView1.AddRows(i, 1);
                Neusoft.HISFC.Models.Material.Input info = al[i] as Neusoft.HISFC.Models.Material.Input;

                this.sheetView1.Cells[i, 0].Text = info.StoreBase.Item.Name;
                this.sheetView1.Cells[i, 1].Text = info.StoreBase.Item.Specs;
                this.sheetView1.Cells[i, 2].Text = info.StoreBase.Quantity.ToString();
                this.sheetView1.Cells[i, 3].Text = info.StoreBase.Item.PriceUnit;
                this.sheetView1.Cells[i, 4].Text = info.StoreBase.PriceCollection.RetailPrice.ToString();
                this.sheetView1.Cells[i, 5].Text = info.StoreBase.PriceCollection.PurchasePrice.ToString();
                this.sheetView1.Cells[i, 6].Text = (info.StoreBase.PriceCollection.RetailPrice * info.StoreBase.Quantity).ToString();
                this.sheetView1.Cells[i, 7].Text = (info.StoreBase.PriceCollection.PurchasePrice * info.StoreBase.Quantity).ToString();
                this.sheetView1.Cells[i, 8].Text = (info.StoreBase.PriceCollection.RetailPrice * info.StoreBase.Quantity - info.StoreBase.PriceCollection.PurchasePrice * info.StoreBase.Quantity).ToString();
                this.sheetView1.Cells[i, 9].Text = info.StoreBase.ValidTime.ToString();
                this.sheetView1.Cells[i, 10].Text = info.StoreBase.BatchNO.ToString();
                if (this.comManager.QueryCompanyByCompanyID(info.StoreBase.Producer.ID, "A", "0") != null)
                {
                    this.sheetView1.Cells[i, 11].Text = this.comManager.QueryCompanyByCompanyID(info.StoreBase.Producer.ID, "A", "0").Name.ToString();
                }
                else
                {
                    this.sheetView1.Cells[i, 11].Text = "δ¼��";
                }
                sum4 += info.StoreBase.PriceCollection.RetailPrice;
                sum5 += info.StoreBase.PriceCollection.PurchasePrice;
                sum6 += (info.StoreBase.PriceCollection.RetailPrice * info.StoreBase.Quantity);
                sum7 += (info.StoreBase.PriceCollection.PurchasePrice * info.StoreBase.Quantity);
                sum8 += (info.StoreBase.PriceCollection.RetailPrice * info.StoreBase.Quantity - info.StoreBase.PriceCollection.PurchasePrice * info.StoreBase.Quantity);
            }
            this.sheetView1.RowCount = al.Count + 1;
            this.sheetView1.Cells[al.Count, 0].Text = "�ϼ�";
            this.sheetView1.Cells[al.Count, 4].Text = sum4.ToString();
            this.sheetView1.Cells[al.Count, 5].Text = sum5.ToString();
            this.sheetView1.Cells[al.Count, 6].Text = sum6.ToString();
            this.sheetView1.Cells[al.Count, 7].Text = sum7.ToString();
            this.sheetView1.Cells[al.Count, 8].Text = sum8.ToString();

            this.fpSpread1.Height = (int)this.sheetView1.RowHeader.Rows[0].Height +
                (int)(this.sheetView1.Rows[0].Height * (al.Count + 1)) + 10;

            Neusoft.FrameWork.WinForms.Classes.Print print = null;
            try
            {
                print = new Neusoft.FrameWork.WinForms.Classes.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);
            }
            print.PrintPage(12, 2, this.neuPanel1);
            this.sheetView1.RowCount = 0;
        }

        #endregion

        #region IBillPrint��Ա

        public int Print()
        {
            return 1;
        }

        public int Prieview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.Check> listCheck)
        {
            throw new Exception("The method or operation is not implemented");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.InputPlan> listInputPlan)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.Output> listOutput)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(List<Neusoft.HISFC.Models.FeeStuff.Input> listInput)
        {
            this.maxRowNO = 12;
            try
            {
                List<Neusoft.HISFC.Models.Material.Input> alPrint = new List<Neusoft.HISFC.Models.Material.Input>();
                int icount = NConvert.ToInt32(System.Math.Ceiling(Convert.ToDouble(listInput.Count) / this.MaxRowNo));
                for (int i = 0; i < icount; i++)
                {
                    if (i != icount - 1)
                    {
                        alPrint = listInput.GetRange(i, this.MaxRowNo);
                    }
                    else
                    {
                        int num = listInput.Count % this.MaxRowNo;
                        if (num == 0)
                        {
                            num = this.MaxRowNo;
                        }
                        alPrint = listInput.GetRange(i, num);
                    }
                    this.Print(alPrint, i + 1, icount, "", "");
                }
                return 1;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("��ӡ����!" + e.Message);
                return -1;
            }
        }

        #endregion
    }
}
