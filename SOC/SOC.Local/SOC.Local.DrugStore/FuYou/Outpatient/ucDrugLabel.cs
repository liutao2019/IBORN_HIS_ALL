using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.FuYou.Outpatient
{
    public partial class ucDrugLabel : UserControl

    {
        public ucDrugLabel()
        {
            InitializeComponent();
        }

        System.Collections.ArrayList alApplyOut;

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

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
            this.lblSpec.Text = "";
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 260, 200);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            //try
            //{
            //    //�ռ÷�Ժ4�Ŵ����Զ���ӡ������ͣ����ӡ����ͷ��ֽ��̫����̫�񶼿���������ͣ
            //    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob();
            //}
            //catch { }
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
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
        private string GetFrequenceName(FS.HISFC.Models.Order.Frequency frequency)
        {
            return Common.Function.GetFrequenceName(frequency);
        }

        /// <summary>
        /// ��ȡÿ����������С��λ������ʽ
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetOnceDose(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
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
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string qtyShowType, DateTime printTime)
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
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                this.Clear();
                this.lbPatientName.Text = drugRecipe.PatientName;
                this.nlbPrintTime.Text = printTime.ToString();
                this.nlbPageNO.Text = pageNO.ToString() + "/" + alData.Count.ToString();
                string itemName = applyOut.Item.Name;
                if (isPrintRegularName)
                {
                    FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                    if (item == null)
                    {
                        MessageBox.Show("��ӡ��ǩʱ��ȡҩƷ������Ϣʧ��");
                        return -1;
                    }

                    itemName = item.NameCollection.RegularName;
                }
                this.lbDrugInfo.Text = itemName;
                this.lblSpec.Text = "���" + applyOut.Item.Specs;

                this.lbUsage.Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

                this.nlbFrequence.Text = this.GetFrequenceName(applyOut.Frequency);

                FS.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(applyOut.OrderNO);

                if (order!=null)
                {
                    string onceDose = "";
                    if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                    {
                        onceDose = "ÿ��" + order.DoseOnceDisplay + order.DoseUnit;
                    }
                    else
                    {
                        onceDose = "ÿ��" + this.GetOnceDose(applyOut);
                    }
                    this.nlbOnceDose.Text = onceDose;

                    this.nlbMemo.Visible = isPrintMemo;
                    this.nlbMemo1.Visible = isPrintMemo;
                    try
                    {
                        this.nlbMemo.Text = order.Memo;
                    }
                    catch { }
                }
               
                this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");
                try
                {
                    this.nlbSendTerminal.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugTerminal.Dept.ID) + drugTerminal.Name;
                }
                catch (Exception)
                {
                    this.nlbSendTerminal.Text =  drugTerminal.Name;
                }
                
                this.nlbHospitalInfo.Text = string.IsNullOrEmpty(hospitalName) ? "˳�¸��ױ���Ժ" : hospitalName;
                this.nlbRecipeNO.Text = drugRecipe.RecipeNO + "��";

                //int x = this.GetHospitalNameLocationX();
                //this.nlbHospitalInfo.Location = new Point(x, this.nlbHospitalInfo.Location.Y);
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
                this.nlbDrugQty.Text = "��" + applyPackQty;
                this.BackColor = System.Drawing.Color.White;
                this.Print();
                pageNO++;
            }
            return 1;
        }
        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        private int GetHospitalNameLocationX()
        {
            return FS.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "HospitalNameLocationX", this.nlbHospitalInfo.Location.X.ToString()));
        }
    }
}
