using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.DrugStore.FuYou.Outpatient
{
    public partial class ucDrugLabel : UserControl

    {
        public ucDrugLabel()
        {
            InitializeComponent();
        }

        System.Collections.ArrayList alApplyOut;

        Neusoft.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new Neusoft.HISFC.BizLogic.Manager.PageSize();
        Neusoft.HISFC.Models.Base.PageSize pageSize;

        /// <summary>
        /// ��¼������
        /// </summary>
        System.Collections.Hashtable hsCombo = new System.Collections.Hashtable();        
       
        /// <summary>
        /// �����ʾ��Ϣ
        /// </summary>
        private void Clear()
        {
           
            this.lbPatientName.Text = "";
            this.nlbPrintTime.Text = "";
            this.nlbPageNO.Text = "";
            this.lbDrugInfo.Text = "";
            this.lbUsage.Text = "";
            this.nlbFrequence.Text = "";
            this.nlbOnceDose.Text = "";
            this.nlbMemo.Text = "";
            this.nlbRecipeNO.Text = "";

            this.nlbSendTerminal.Text = "";
            this.nlbComboNO.Text = "";
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 400, 200);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            //try
            //{
            //    //�ռ÷�Ժ4�Ŵ����Զ���ӡ������ͣ����ӡ����ͷ��ֽ��̫����̫�񶼿���������ͣ
            //    Neusoft.FrameWork.WinForms.Classes.Print.ResumePrintJob();
            //}
            //catch { }
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }

        /// <summary>
        /// ��ȡƵ������
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        private string GetFrequenceName(Neusoft.HISFC.Models.Order.Frequency frequency)
        {
            return Common.Function.GetFrequenceName(frequency);
        }

        /// <summary>
        /// ��ȡÿ����������С��λ������ʽ
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetOnceDose(Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {

            return Common.Function.GetOnceDose(applyOut);
        }

        /// <summary>
        /// ��ӡ��ǩ
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string qtyShowType, DateTime printTime)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }
            if (alApplyOut != null)
            {
                alApplyOut.Clear();
            }
            this.alApplyOut = alData;

            hsCombo.Clear();
            int comboNO = 1;
            int pageNO = 1;
            foreach (Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                this.Clear();
                this.lbPatientName.Text = drugRecipe.PatientName;
                this.nlbPrintTime.Text = printTime.ToString();
                this.nlbPageNO.Text = pageNO.ToString() + "/" + alData.Count.ToString();
                string itemName = applyOut.Item.Name;
                if (isPrintRegularName)
                {
                    Neusoft.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                    if (item == null)
                    {
                        MessageBox.Show("��ӡ��ǩʱ��ȡҩƷ������Ϣʧ��");
                        return -1;
                    }

                    itemName = item.NameCollection.RegularName;
                }
                this.lbDrugInfo.Text = itemName + "(" + applyOut.Item.Specs + ")";

                this.lbUsage.Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

                this.nlbFrequence.Text = this.GetFrequenceName(applyOut.Frequency);
                this.nlbOnceDose.Text = "ÿ��"+this.GetOnceDose(applyOut);

                this.nlbMemo.Visible = isPrintMemo;
                this.nlbMemo1.Visible = isPrintMemo;
                this.nlbMemo.Text = applyOut.Memo;
                this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");

                this.nlbSendTerminal.Text = drugTerminal.Name;
                this.nlbHospitalInfo.Text = hospitalName;
                this.nlbRecipeNO.Text = drugRecipe.RecipeNO + "��";

                int x = this.GetHospitalNameLocationX();
                this.nlbHospitalInfo.Location = new Point(x, this.nlbHospitalInfo.Location.Y);
                if (!string.IsNullOrEmpty(applyOut.CombNO))
                {
                    if (!hsCombo.Contains(applyOut.CombNO))
                    {
                        hsCombo.Add(applyOut.CombNO, comboNO.ToString());
                        comboNO++;
                    }
                    this.nlbComboNO.Text = hsCombo[applyOut.CombNO].ToString() + "��";
                }
                else
                {
                    this.nlbComboNO.Text = "";
                }
                //������ʾ����
                string applyPackQty = "";
                //string price = applyOut.Item.PriceCollection.RetailPrice.ToString() + "Ԫ" + "/" + applyOut.Item.PackUnit;
                if (qtyShowType == "��װ��λ")
                {
                    int applyQtyInt = 0;//���ȡ���̣�������װ��λ����������������
                    decimal applyRe = 0;//���ȡ������������С��λ������������С��
                    applyQtyInt = (int)(applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty);
                    applyRe = applyOut.Operation.ApplyQty * applyOut.Days - applyQtyInt * applyOut.Item.PackQty;
                    if (applyQtyInt > 0)
                    {
                        applyPackQty += applyQtyInt.ToString() + applyOut.Item.PackUnit;
                    }
                    if (applyRe > 0)
                    {
                        applyPackQty += applyRe.ToString().TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    }
                }
                else
                {
                    applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString().TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    //price = (applyOut.Item.PriceCollection.RetailPrice / applyOut.Item.PackQty).ToString("F4") + "Ԫ" + "/" + applyOut.Item.MinUnit;
                }
                this.nlbDrugQty.Text = "" + applyPackQty;
                this.BackColor = System.Drawing.Color.White;
                this.Print();
                pageNO++;
            }
            return 1;
        }
        private string fileName = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        private int GetHospitalNameLocationX()
        {
            return Neusoft.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "HospitalNameLocationX", this.nlbHospitalInfo.Location.X.ToString()));
        }
    }
}
