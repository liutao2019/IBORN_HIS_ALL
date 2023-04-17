using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient
{
    /// <summary>
    /// [��������: ����ҩ����ӡ���ػ�ʵ��:ҩƷ�嵥]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-12]<br></br>
    /// ˵����
    /// 1���������Ĳ�����,��ֵ����
    /// 2�����������Զ�����ҳ��,��ҳ����
    /// 3�����ݱ��ⳤ���Զ�����
    /// </summary>>
    public partial class ucDrugList : UserControl
    {
        public ucDrugList()
        {
            InitializeComponent();
        }


        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// ��ո�ֵ������
        /// �Խ������е�label��Text��ֵ""
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Text = "";
                }
            }

            this.lbRePringFlag.Text = "����";
            this.fpSpread1_Sheet1.RowCount = 0;

            return 1;
        }

        /// <summary>
        /// ����ҩƷ��Ϣ
        /// </summary>
        /// <param name="applyOut"></param>
        /// <param name="isPrintRegularName"></param>
        /// <param name="isPrintMemo"></param>
        /// <param name="qtyShowType"></param>
        /// <returns></returns>
        private int SetDrugInfo
            (
            ArrayList alApplyOut,
            bool isPrintRegularName,
            bool isPrintMemo,
            string qtyShowType)
        {
            int curRowIndex = 0;
            decimal totCost = 0;
            this.fpSpread1_Sheet1.RowCount = alApplyOut.Count;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {

                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                if (item == null)
                {
                    MessageBox.Show("��ӡ��ǩʱ��ȡҩƷ������Ϣʧ��");
                    return -1;
                }

                this.fpSpread1.SetCellValue(0, curRowIndex, "����", item.UserCode);

                string printDrugName = applyOut.Item.Name;
                if (isPrintRegularName)
                {
                    printDrugName = item.NameCollection.RegularName + "(" + applyOut.Item.Name + ")";
                }
                this.fpSpread1.SetCellValue(0, curRowIndex, "����", printDrugName);


                this.fpSpread1.SetCellValue(0, curRowIndex, "���", applyOut.Item.Specs);
                this.fpSpread1.SetCellValue(0, curRowIndex, "Ƶ��", SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(applyOut.Frequency.ID));
                this.fpSpread1.SetCellValue(0, curRowIndex, "ÿ������", SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut));

                FS.HISFC.Models.Order.OutPatient.Order order = SOC.Local.DrugStore.ShenZhen.Common.Function.GetOrder(applyOut.OrderNO);
                this.fpSpread1.SetCellValue(0, curRowIndex, "����", order.HerbalQty.ToString("F4").TrimEnd('0').TrimEnd('.'));


                //������ʾ����
                string applyPackQty = "";
                if (qtyShowType == "��װ��λ")
                {
                    int applyQtyInt = 0;//���ȡ���̣�������װ��λ����������������
                    decimal applyRe = 0;//���ȡ������������С��λ������������С��
                    applyQtyInt = (int)(applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
                    applyRe = applyOut.Operation.ApplyQty - applyQtyInt * applyOut.Item.PackQty;
                    if (applyQtyInt > 0)
                    {
                        applyPackQty += applyQtyInt.ToString() + applyOut.Item.PackUnit;
                    }
                    if (applyRe > 0)
                    {
                        applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    }
                }
                else
                {
                    applyPackQty = (applyOut.Operation.ApplyQty).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                }

                this.fpSpread1.SetCellValue(0, curRowIndex, "����", applyPackQty);
                if (isPrintMemo)
                {
                    this.fpSpread1.SetCellValue(0, curRowIndex, "��ע", order.Memo);
                }
                this.fpSpread1.SetCellValue(0, curRowIndex, "��λ��", SOC.Local.DrugStore.ShenZhen.Common.Function.GetStockPlaceNO(applyOut));
                totCost += applyOut.Operation.ApplyQty / applyOut.Item.PackQty * applyOut.Item.PriceCollection.RetailPrice;
                curRowIndex++;

            }
            //��Ϻű�ǻ���
            //��Ϻ���
            int comboNOIndex = this.fpSpread1.GetColumnIndex(0, "��Ϻ�");
            int comboNOFlagIndex = this.fpSpread1.GetColumnIndex(0, "��");
            if (comboNOIndex > -1 && comboNOFlagIndex > -1)
            {
                SOC.HISFC.Components.Common.Function.DrawCombo(this.fpSpread1_Sheet1, comboNOIndex, comboNOFlagIndex);
            }
            this.lbCost.Text = "��ҩ�ۣ�" + totCost.ToString("F2");
            return 1;
        }

        /// <summary>
        /// ���û�����Ϣ
        /// ͬһ�Ŵ���ֻ����һ��
        /// </summary>
        /// <param name="drugRecipe"></param>
        /// <returns></returns>
        private int SetPatientInfo(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            this.lbPatientInfo.Text = "������" + drugRecipe.PatientName
                + "  " + drugRecipe.Sex.Name
                + "  " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetAge(drugRecipe.Age)
                + "      " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetPactUnitName(drugRecipe.PayKind.ID)
                + "";
            return 1;
        }

        /// <summary>
        /// ����������Ϣ����ϵ�
        /// ͬһ�Ŵ���ֻ����һ��
        /// </summary>
        /// <param name="hospitalName"></param>
        /// <param name="diagnose"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        private int SetOtherInfo(string hospitalName, string diagnose, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string windowName)
        {
            this.lbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name  +"��ҩ�嵥";
            this.lbRecipeNO.Text = "���ţ�" + drugRecipe.RecipeNO;
            this.lbInvoiceNO.Text = "��Ʊ��" + drugRecipe.InvoiceNO;
            this.lbRePringFlag.Visible = (drugRecipe.RecipeState == "0");

            this.lbRecipeDeptName.Text = "���ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.lbRecipeDoctName.Text = "" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.lbDiagnose.Text = "��ϣ�" + diagnose;
            this.lbWindowName.Text = windowName;
            this.lbPrintTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            return 1;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            ////���û��ϴ�ӡ�����ӡ
            //FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();

            ////��ȡά����ֽ��
            //if (pageSize == null)
            //{
            //    //��ϵͳֽ��������ά����OutPatientDrugListΪ��ţ�OutPatientDrugListΪ���ƣ�ʹ��Ĭ�ϴ�ӡ��ʱ��ӡ����ά��
            //    //��ά�����ϱ߾���Ϊ�±߾࣬��������ҳβ��ӡ�Ŀհף���֤������ȫ��ӡ
            //    pageSize = pageSizeMgr.GetPageSize("OutPatientDrugList");
            //    //ָ����ӡ����default˵��ʹ��Ĭ�ϴ�ӡ���Ĵ���
            //    if (pageSize != null && pageSize.Printer.ToLower() == "default")
            //    {
            //        pageSize.Printer = "";
            //    }
            //    //û��ά��ʱĬ��һ��ֽ��
            //    if (pageSize == null)
            //    {
            //        pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugList", 850, 1100);
            //    }
            //}

            ////��ӡ�߾ദ����ά�����ϱ߾���Ϊ�±߾࣬��������ҳβ��ӡ�Ŀհף���֤������ȫ��ӡ
            //print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, 0, pageSize.Top);

            ////ֽ�Ŵ���
            //print.PaperName = pageSize.Name;
            //print.PaperHeight = pageSize.Height;
            //print.PaperWidth = pageSize.Width;

            ////��ӡ������
            //print.PrinterName = pageSize.Printer;

            ////ҳ����ʾ
            //this.lbPageNO.Tag = "ҳ�룺{0}/{1}";
            //print.PageNOControl = this.lbPageNO;

            ////ҳü�ؼ�����ʾÿҳ����ӡ
            //print.HeaderControls.Add(this.paneltitle);
            ////ҳ�ſؼ�����ʾÿҳ����ӡ
            //print.FooterControls.Add(this.panelBottom);


            ////����ʾҳ��ѡ��
            //print.IsShowPageNOChooseDialog = false;

            //this.SetUI();

            ////����Աʹ��Ԥ������
            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    print.PrintPageView(this);
            //}
            //else
            //{
            //    print.PrintPage(this);
            //}

                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugList", 860, 550));
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsDataAutoExtend = false;
                try
                {
                    //�ռ÷�Ժ4�Ŵ����Զ���ӡ������ͣ����ӡ����ͷ��ֽ��̫����̫�񶼿���������ͣ
                    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                catch { }
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(0, 0, this);
                }
                else
                {
                    print.PrintPage(0, 0, this);
                }
                this.Clear();
            }



        /// <summary>
        /// ��ӡʱ����UserInterface
        /// </summary>
        private void SetUI()
        {
            if (pageSize != null)
            {
                this.lbTitle.Location = new Point((pageSize.Width - this.lbTitle.PreferredWidth) / 2, this.lbTitle.Location.Y);
            }
            this.fpSpread1.SetActiveSkin(0, FS.SOC.Windows.Forms.FpSpread.EnumSkinType.��һ����);

        }

        public int PrintDrugList
           (
           System.Collections.ArrayList alApplyOut,
           FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe,
           FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal,
           bool isPrintRegularName,
           bool isPrintMemo,
           string qtyShowType,
           string hospitalName,
           string diagnose
           )
        {
            if (alApplyOut == null || alApplyOut.Count == 0 || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }

            this.Clear();
            this.SetPatientInfo(drugRecipe);
            this.SetOtherInfo(hospitalName, diagnose, drugRecipe, drugTerminal.Name);
            this.SetDrugInfo(alApplyOut, isPrintRegularName, isPrintMemo, qtyShowType);
            this.Print();

            return 1;
        }

    }
}
