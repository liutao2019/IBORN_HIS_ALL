using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Linq;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient
{
    /// <summary>
    /// [��������: סԺҩ����Ժ��ҩ���������ػ�]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-12]<br></br>
    /// ˵����
    /// </summary>    
    public partial class ucAnestheticByPatientDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        public ucAnestheticByPatientDrugBill()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        #region ��ҩ����ͨ�÷���

        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            for (int index = 0; index < this.neuPanel1.Controls.Count; index++)
            {
                this.neuPanel1.Controls[index].Text = "";
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuLabel4.Text = "���";
            this.neuLabel5.Text = "������";
        }

        /// <summary>
        /// ��ʼ��Fp
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreAnesDrugBill.xml");
        }

        /// <summary>
        /// ��ʵ��û�����壬�ͻ��ܵ�ͳһ����
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        private void ShowBillData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            string orderId = "";//����ð�ҽ����ˮ������ 
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Pharmacy.ApplyOut objLast = null;
            System.Collections.Hashtable hsFrequenceCount = new Hashtable();
            alData.Sort(new CompareApplyOutByOrderNO());

            //�ϲ��������ҩ����
            for (int i = alData.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = (alData[i] as FS.HISFC.Models.Pharmacy.ApplyOut);
                obj.User01 = FS.FrameWork.Function.NConvert.ToInt32(!FS.SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(obj.StockDept.ID, obj.Item.ID)).ToString();
                bool needAdd = false;
                if (hsFrequenceCount.Contains(obj.OrderNO))
                {
                    int count = (int)hsFrequenceCount[obj.OrderNO];
                    count = count + 1;
                    if ((count > this.GetFrequencyCount(obj.Frequency.ID)) && obj.OrderType.ID == "CZ" && drugBillClass.ID != "R")
                    {
                        needAdd = true;
                    }
                    if (count == this.GetFrequencyCount(obj.Frequency.ID) + 1)
                    {
                        hsFrequenceCount[obj.OrderNO] = 1;
                    }
                    else
                    {
                        hsFrequenceCount[obj.OrderNO] = count;
                    }
                }
                else
                {
                    int count = 1;
                    hsFrequenceCount[obj.OrderNO] = count;
                }

                if (orderId == "")
                {
                    orderId = obj.OrderNO;
                    objLast = obj;
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.UseTime), dt);
                }
                else if (orderId == obj.OrderNO && !needAdd)//��һ��ҩ
                {
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(obj.UseTime), dt) + " " + objLast.User03;
                    objLast.Operation.ApplyQty += obj.Operation.ApplyQty * obj.Days;//�������
                    alData.RemoveAt(i);

                }
                else
                {
                    orderId = obj.OrderNO;
                    objLast = obj;
                    if (needAdd)
                    {
                        obj.Frequency.Name = "���";
                    }
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.UseTime), dt);
                }
                if (obj.OrderType.ID != "CZ")
                {
                    objLast.User03 = "";
                }
            }

            int index = 1;
            //��7�� ��ΪҽԺҪ���鷽 ��ҩƷ���� ���� BY FZC 2014-10-03
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            //{
            //    this.SetDetailInfo(drugBillClass,info,index,alData.Count);
            //    this.PrintPage();
            //    this.Clear();
            //    index++;
            //}

            //����������ת��ΪILIST���ڹ���
            IList<FS.HISFC.Models.Pharmacy.ApplyOut> applyList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut a in alData)
            {
                applyList.Add(a);
            }
            if (applyList.Count < 1)
            {
                return;
            }
            //��ҩƷ�ŷ���
            var listByDrug = applyList.GroupBy(o => o.Item.ID);
            int pageNum = 0;
            foreach (var drugGroup in listByDrug)
            {
                if (drugGroup.Count() > 10)//��������ܴ�10��ҩ
                {
                    int yu = 0;
                    int num = System.Math.DivRem(drugGroup.Count(), 10, out yu);
                    if (yu == 0)
                    {
                        pageNum += num;
                    }
                    else
                    {
                        pageNum += (num + 1);
                    }
                }
                else
                {
                    pageNum++;
                }
            }
            foreach (var drugGroup in listByDrug)
            {
                if (drugGroup.Count() <= 10)
                {
                    this.SetDetailData(drugBillClass, drugGroup.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>(), index, pageNum);
                    this.PrintPage();
                    this.Clear();
                    index++;
                }
                else
                {
                    int yu = 0;
                    int num = System.Math.DivRem(drugGroup.Count(), 10, out yu);
                    IList<FS.HISFC.Models.Pharmacy.ApplyOut> applyListSplit = null;
                    
                    for (int i = 0; i < num; i++)
                    {
                        applyListSplit = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        for (int j = 0; j < 10; j++)
                        {
                            applyListSplit.Add(drugGroup.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>()[j + i * 10]);
                        }
                        this.SetDetailData(drugBillClass, applyListSplit, index, pageNum);
                        this.PrintPage();
                        this.Clear();
                        index++;
                    }

                    if (yu != 0)
                    {
                        applyListSplit = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        for (int k = 0; k < yu; k++)
                        {
                            applyListSplit.Add(drugGroup.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>()[k + num * 10]);
                        }

                        this.SetDetailData(drugBillClass, applyListSplit.ToList<FS.HISFC.Models.Pharmacy.ApplyOut>(), index, pageNum);
                        this.PrintPage();
                        this.Clear();
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetSendType(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string sendType = string.Empty;
            switch (applyOut.SendType)
            {
                case 1:
                    sendType = "����";
                    break;
                case 2:
                    sendType = "��ʱ";
                    break;
                case 4:
                    sendType = "����";
                    break;
            }
            return sendType;
        }

        /// <summary>
        /// �°���ܰ�ҩƷ���ܵĶ�����ϸ��ӡ����
        /// </summary>
        /// <param name="drugBillClass"></param>
        /// <param name="applyList"></param>
        /// <param name="curPageNO"></param>
        /// <param name="TotPageNO"></param>
        private void SetDetailData(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, IList<FS.HISFC.Models.Pharmacy.ApplyOut> applyList, int curPageNO, int TotPageNO)
        {

            string sendType = this.GetSendType(applyList[0] as FS.HISFC.Models.Pharmacy.ApplyOut);

            #region �ж��Ƿ��Ժ��ҩ������
            if (drugBillClass.ID.Contains("O"))
            {
                this.lbTitle.Text = "(��Ժ��ҩ)" + applyList[0].Item.Name;//+ "(" + sendType + ")";
                // {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
            }
            else
            {
                this.lbTitle.Text = applyList[0].Item.Name;//+ "(" + sendType + ")";
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            {                
                if (!this.lbTitle.Text.Contains("����"))
                {
                    this.lbTitle.Text = "(����)" + this.lbTitle.Text;
                }
                this.nlbReprint.Visible = false;
            }
            else
            {
                this.nlbReprint.Visible = false;
            }
            #endregion

            #region ���ô�ӡ��ʽ
            this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            this.nlbSpecs.Text = applyList[0].Item.Specs.PadRight(30, ' ');

            this.nlbPageNO.Text = "��" + curPageNO + "ҳ��" + "��" + TotPageNO + "ҳ";

            this.nlbDrugDate.Text = "��ҩʱ�䣺" + applyList[0].Operation.ExamOper.OperTime.ToString();

            this.nlbPrintDate.Text = "��ӡʱ��" + inPatientMgr.GetDateTimeFromSysDateTime();

            //�ȼ������� �жϷ�ҩ��λ
            decimal totalNum = 0;
            foreach (var apply in applyList)
            {
                totalNum += apply.Operation.ApplyQty * apply.Days;
            }

            int outMinQty = 0;
            int outPackQty = System.Math.DivRem((int)totalNum, (int)applyList[0].Item.PackQty, out outMinQty);

            string total = string.Empty;
            if (outMinQty == 0)
            {
                this.nlbUnit.Text = outPackQty + applyList[0].Item.PackUnit;
            }
            else
            {
                this.nlbUnit.Text = totalNum + applyList[0].Item.MinUnit;
            }

            #endregion

            for (int i = 0; i < applyList.Count; i++)
            {
                this.neuSpread1_Sheet1.Rows.Count++;
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.QueryPatientInfoByInpatientNONew(applyList[i].PatientNO);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", applyList[i].Operation.ExamOper.OperTime.ToShortDateString());

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "���", i + 1);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "�Ա�", patientInfo.Sex.Name);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", inPatientMgr.GetAge(patientInfo.Birthday));

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(patientInfo.PVisit.PatientLocation.Dept.ID));

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "���֤��", patientInfo.IDCard);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "סԺ��", patientInfo.PID.PatientNO);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "���", Common.Function.GetInpatientDiagnose(patientInfo.PID.ID).Replace("-1", string.Empty));

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����ҽ��", applyList[i].RecipeInfo.ID);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", applyList[i].Operation.ApplyQty);
                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", patientInfo.Name);

                this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "�����", string.Empty);

            }
            this.neuPanel12.Height = this.neuSpread1_Sheet1.Rows.Count * 15;
        }

        /// <summary>
        /// �������ǰ�ķ��� ֻ�ܴ�һ��ҩƷ
        /// </summary>
        /// <param name="drugBillClass"></param>
        /// <param name="info"></param>
        /// <param name="curPageNO"></param>
        /// <param name="TotPageNO"></param>
        private void SetDetailInfo(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.HISFC.Models.Pharmacy.ApplyOut info, int curPageNO, int TotPageNO)
        {

            if (drugBillClass.ID.Contains("O"))
            {
                this.lbTitle.Text = "(��Ժ��ҩ)" + info.Item.Name;
            }
            else
            {
                this.lbTitle.Text = info.Item.Name;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
            {
                this.nlbReprint.Visible = false;
                if (!this.lbTitle.Text.Contains("����"))
                {
                    this.lbTitle.Text = this.nlbReprint.Text + this.lbTitle.Text;
                }
            }
            else
            {
                this.nlbReprint.Visible = false;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.inPatientMgr.QueryPatientInfoByInpatientNONew(info.PatientNO);

            this.lbTitle.Location = new Point((this.Width - this.lbTitle.Width) / 2, this.lbTitle.Location.Y);

            this.nlbSpecs.Text = info.Item.Specs.PadRight(30, ' ');

            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            //����
            decimal applyQty = info.Operation.ApplyQty;
            string unit = info.Item.MinUnit;
            decimal price = 0m;

            int outMinQty;
            int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
            if (outPackQty == 0)
            {
                applyQty = info.Operation.ApplyQty;
                unit = info.Item.MinUnit;
                price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 4);
            }
            else if (outMinQty == 0)
            {
                applyQty = outPackQty;
                unit = info.Item.PackUnit;
                price = info.Item.PriceCollection.RetailPrice;
            }
            else
            {
                applyQty = info.Operation.ApplyQty;
                unit = info.Item.MinUnit;
                price = Math.Round(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 4);
            }

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", info.Operation.ExamOper.OperTime.ToShortDateString());

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "���", "1");

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "�Ա�", patientInfo.Sex.Name);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", inPatientMgr.GetAge(patientInfo.Birthday));

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(patientInfo.PVisit.PatientLocation.Dept.ID));

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "���֤��", patientInfo.IDCard);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "סԺ��", patientInfo.PID.PatientNO);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "���", Common.Function.GetInpatientDiagnose(patientInfo.PID.ID).Replace("-1", string.Empty));

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����ҽ��", info.RecipeInfo.ID);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", applyQty);
            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "����", patientInfo.Name);

            this.neuSpread1.SetCellValue(0, this.neuSpread1_Sheet1.Rows.Count - 1, "�����", string.Empty);

            this.nlbPageNO.Text = "��" + curPageNO + "ҳ��" + "��" + TotPageNO + "ҳ";

            this.nlbDrugDate.Text = "��ҩʱ�䣺" + info.Operation.ExamOper.OperTime.ToString();

            this.nlbPrintDate.Text = "��ӡʱ��" + inPatientMgr.GetDateTimeFromSysDateTime();

            this.nlbUnit.Text = unit;
        }


        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            this.Dock = DockStyle.None;

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
            print.SetPageSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(10, 10, this);
            }
            else
            {
                print.PrintPage(10, 10, this);
            }

            this.Dock = DockStyle.Fill;

        }

        /// <summary>
        /// ��ȡֽ��
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillN", "ALL");
            //����Ӧֽ��
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 870;

                    //int curHeight = 0;

                    //int addHeight = (this.neuSpread1.ActiveSheet.RowCount - 1) *
                    //    (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    //int additionAddHeight = 180;

                    paperSize.Width = width;
                    paperSize.Height = 550;//(addHeight + curHeight + additionAddHeight);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("����ֽ�ų���>>" + ex.Message);
                }
            }
            if (!string.IsNullOrEmpty(paperSize.Printer) && paperSize.Printer.ToLower() == "default")
            {
                paperSize.Printer = "";
            }
            return paperSize;
        }
        #endregion

        #region ���÷���

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            this.Clear();
            this.SetFormat();
            this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreNorDrugBill.xml");
        }

        /// <summary>
        /// ����ҩʱ��/��ǰʱ�� �����ʾ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        private string FormatDateTime(DateTime dt, DateTime sysdate)
        {
            try
            {
                if (sysdate.Date.AddDays(-1) == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date.AddDays(1) == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else if (sysdate.Date.AddDays(2) == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0');
                }
                else
                {
                    if (dt.Month == sysdate.Month)
                    {
                        return dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');

                    }
                }
            }
            catch
            {
                return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
            }
        }


        /// <summary>
        /// ��ȡƵ�δ����ÿ�����
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            return 1000;

            //��ׯ������
            if (string.IsNullOrEmpty(frequencyID))
            {
                return 1000;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//ÿ��һ��
            {
                return 1;
            }
            else if (id == "bid")//ÿ������
            {
                return 2;
            }
            else if (id == "tid")//ÿ������
            {
                return 3;
            }
            else if (id == "hs")//˯ǰ
            {
                return 1;
            }
            else if (id == "qn")//ÿ��һ��
            {
                return 1;
            }
            else if (id == "qid")//ÿ���Ĵ�
            {
                return 4;
            }
            else if (id == "pcd")//��ͺ�
            {
                return 1;
            }
            else if (id == "pcl")//��ͺ�
            {
                return 1;
            }
            else if (id == "pcm")//��ͺ�
            {
                return 1;
            }
            else if (id == "prn")//��Ҫʱ����
            {
                return 1;
            }
            else if (id == "��ҽ��")
            {
                return 1;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                return 100;
            }
        }

        /// <summary>
        /// �ṩû�з�Χѡ��Ĵ�ӡ
        /// һ���ڰ�ҩ����ʱ����
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        public void PrintData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        #endregion

        #region IInpatientBill ��Ա������ʱ��

        /// <summary>
        /// �ṩ��ҩ��������ʾ�ķ���
        /// һ���ڰ�ҩ������ʱ����
        /// </summary>
        /// <param name="alData">��������applyout</param>
        /// <param name="drugBillClass">��ҩ������</param>
        /// <param name="stockDept">������</param>
        public void ShowData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// �ṩ����ѡ���ӡ��Χ�Ĵ�ӡ����
        /// </summary>
        public void Print()
        {
            this.PrintPage();
        }

        /// <summary>
        /// ����Dock���ԣ�����ʱ��
        /// </summary>
        public DockStyle WinDockStyle
        {
            get
            {
                return this.Dock;
            }
            set
            {
                this.Dock = value;
            }
        }

        /// <summary>
        /// �������ͣ�����ʱ��
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.��Ժ��ҩ����;
            }
        }

        #endregion

    }

    /// <summary>
    /// ������
    /// </summary>
    public class CompareApplyOutByOrderNO : IComparer
    {
        /// <summary>
        /// ���򷽷�
        /// </summary>
        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
            FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

            string oX = "";          //��������
            string oY = "";          //��������


            oX = o1.BedNO + o1.PatientName + o1.OrderNO + o1.UseTime.ToString();
            oY = o2.BedNO + o2.PatientName + o2.OrderNO + o2.UseTime.ToString();

            return string.Compare(oX, oY);
        }
    }
}
