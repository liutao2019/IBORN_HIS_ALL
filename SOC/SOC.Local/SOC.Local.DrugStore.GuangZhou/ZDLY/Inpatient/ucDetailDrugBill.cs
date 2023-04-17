using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Inpatient
{
    /// <summary>
    /// [��������: סԺҩ����ϸ����ӡ���ػ�ʵ��]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-12]<br></br>
    /// ˵����
    /// 1����Ϊһ�����ӱ�����������Ҫ����
    /// 2������Ŀ����޸Ĳ���Ļ������Կ��Ǽ̳з�ʽ
    /// </summary>
    public partial class ucDetailDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// ��ϸ��ӡ��ҩ��
        /// </summary>
        public ucDetailDrugBill()
        {
            InitializeComponent();
            //pageSize = this.GetPaperSize();
        }

        #region ����

        /// <summary>
        /// ÿҳ������������ǰ���Letterֽ�ŵ����ģ��и߸ı�Ӱ���ҳ
        /// </summary>
        int pageRowNum = 200;

        /// <summary>
        /// ���ҳ��
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// ��ӡ����Ч����,��ѡ��ҳ�뷶Χʱ��Ч
        /// </summary>
        int validRowNum = 0;

        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// ����������
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region ��ҩ����ͨ�÷���

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            this.nlbTitle.Text = "סԺҩ����ϸ��ҩ��";
            this.nlbRowCount.Text = "��¼����";
            this.nlbBillNO.Text = "���ݺţ�";
            this.nlbFirstPrintTime.Text = "�״δ�ӡ��";
            this.nlbStockDept.Text = "��ҩ���ң�";
            this.nlbPrintTime.Text = "��ӡʱ�䣺";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ������
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");
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
            this.ShowDetailData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(ArrayList alDataOrg, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            ArrayList alData = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut objOrg in alDataOrg)
            {
                alData.Add(objOrg.Clone());
                if (string.IsNullOrEmpty(drugBillClass.ApplyDept.ID))
                {
                    drugBillClass.ApplyDept.ID = objOrg.ApplyDept.ID;
                }
            }

            //��Ԫ������
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder noneBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);

            string applyDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            this.nlbTitle.Text =  drugBillClass.Name + "(��ϸ)";
            this.lblApplyDept.Text = "��ҩ���ң� " + applyDeptName;

            this.nlbRowCount.Text = "��¼����" + alData.Count.ToString();
            this.nlbBillNO.Text = "���ݺţ�" + drugBillClass.DrugBillNO;// (drugBillClass.DrugBillNO.Length > 8 ? drugBillClass.DrugBillNO.Substring(8) : drugBillClass.DrugBillNO);
            this.nlbStockDept.Text = "��ҩ���ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);

            //��ҩ������ʾƵ�κ�ÿ���� add by zhy
            if (drugBillClass.Name == "��ҩ��")
            {
                this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = " ";
                this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = " ";
            }
            else
            {
                this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Ƶ��";
                this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ÿ��";
            }

            #region ��ͬһҽ������ҩʱ�������ʾ

            CompareApplyOutByOrderNO com1 = new CompareApplyOutByOrderNO();
            alData.Sort(com1);

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();

            string orderId = "";//����ð�ҽ����ˮ������ 
            FS.HISFC.Models.Pharmacy.ApplyOut objLast = null;
            System.Collections.Hashtable hsFrequenceCount = new Hashtable();

            //�ϲ��������ҩ����
            for (int i = alData.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = (alData[i] as FS.HISFC.Models.Pharmacy.ApplyOut);
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
                    objLast.Operation.ApplyQty += obj.Operation.ApplyQty;//�������
                    alData.RemoveAt(i);

                }
                else
                {
                    orderId = obj.OrderNO;
                    objLast = obj;
                    if (needAdd)
                    {
                        obj.Frequency.Name = "����";
                    }
                    objLast.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(objLast.UseTime), dt);
                }
                if (obj.OrderType.ID != "CZ")
                {
                    objLast.User03 = "";
                }
            }

            #endregion

            #region ����������

            CompareApplyOutByPatient com2 = new CompareApplyOutByPatient();
            alData.Sort(com2);
            #endregion

            this.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

            //��������
            string privPatientName = "";
            string samePatient = "";
            //����
            int iRow = 0;
            //���ߴ��� �������Ժ�ҩƷͬ�У���������Ƿ�Ҫ����һ����ʾҩƷ
            //bool isNeedAddRow = true;

            #region ��������
            string patientInfo = "";
            this.nlbRowCount.Text = "��¼����" + alData.Count.ToString();

            ArrayList alKeys = new ArrayList();
            Hashtable hsPatient = new Hashtable();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (hsPatient.ContainsKey(info.PatientNO))
                {
                    ArrayList al = hsPatient[info.PatientNO] as ArrayList;
                    al.Add(info);
                }
                else
                {
                    alKeys.Add(info.PatientNO);
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsPatient.Add(info.PatientNO, al);
                }
            }

            foreach (string patientNO in alKeys)
            {          

                decimal totCost = 0m;

                ArrayList alPatientData = hsPatient[patientNO] as ArrayList;
                string age = "";
                string bedNO = "";
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                FS.HISFC.Models.Pharmacy.ApplyOut applyout = new FS.HISFC.Models.Pharmacy.ApplyOut();
                try
                {
                    p = inpatientManager.GetPatientInfoByPatientNO(patientNO);
                    age = inpatientManager.GetAge(p.Birthday);

                    applyout = alPatientData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    bedNO = applyout.BedNO;
                    if (bedNO.Length > 4)
                    {
                        bedNO = bedNO.Substring(4);
                    }
                    if (drugBillClass.ID == "R")
                    {
                        if (p.PVisit.InState.ID.ToString() == "2")
                        {
                            bedNO = "*" + bedNO;
                        }
                    }
                }
                catch { }

                privPatientName = applyout.PatientName;
                samePatient = applyout.PatientNO;
                patientInfo = string.Format("{0}    {1}סԺ�ţ�{2}   ���䣺{3}    �Ա�{4}", bedNO, SOC.Public.String.PadRight(privPatientName, 8, ' '), p.PID.PatientNO, age, p.Sex.Name);

                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noneBorder;
                this.neuSpread1_Sheet1.Cells[iRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[iRow, 0].Font = new System.Drawing.Font("����", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.Cells[iRow, 0].Text = patientInfo;
                iRow++;


                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alPatientData)
                {

                    //ҩƷ��Ϣ
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    this.neuSpread1.SetCellValue(0, iRow, "��λ��", info.PlaceNO);
                    this.neuSpread1.SetCellValue(0, iRow, "����", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));

                    FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                    if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                    {

                        this.neuSpread1.SetCellValue(0, iRow, "����", item.NameCollection.RegularName);
                    }
                    else
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "����", item.Name);
                    }
                    this.neuSpread1.SetCellValue(0, iRow, "���", info.Item.Specs);
                    if (string.IsNullOrEmpty(info.Usage.Name))
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "�÷�", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
                    }
                    else
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "�÷�", info.Usage.Name);
                    }
                    //Ƶ��
                    try
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "Ƶ��", info.Frequency.ID.ToLower());
                        if (info.Frequency.Name == "����")
                        {
                            this.neuSpread1.SetCellValue(0, iRow, "Ƶ��", "��" + info.Frequency.ID.ToLower());
                        }
                    }
                    catch { }

                    string doseOnce = Common.Function.GetOnceDose(info);
                    this.neuSpread1.SetCellValue(0, iRow, "ÿ��", doseOnce);
                    //this.neuSpread1.SetCellValue(0, iRow, "ÿ������", info.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.DoseUnit);


                    this.neuSpread1.SetCellValue(0, iRow, "����", info.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit);
                    this.neuSpread1.SetCellValue(0, iRow, "���ۼ�", FS.FrameWork.Public.String.FormatNumberReturnString(info.Item.PriceCollection.RetailPrice / info.Item.PackQty, 4));


                    //��ҩ����
                    if (drugBillClass.ID == "R")
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "ʹ��ʱ��", "");
                    }
                    else
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "ʹ��ʱ��", info.User03);
                        this.neuSpread1.SetCellValue(0, iRow, "��ע", "");//info.Memo);
                    }
                    //                this.neuSpread1.SetCellValue(0, iRow, "���ۼ�", FS.FrameWork.Public.String.FormatNumberReturnString(price,2));
                    this.neuSpread1.SetCellValue(0, iRow, "���۽��", FS.FrameWork.Public.String.FormatNumberReturnString(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2));


                    //��ӡʱ�䣬�״δ�ӡҲ���ڱ�����ӡ�ģ�info.State�������0����drugBillClass.ApplyState�ڵ��øÿؼ�ǰ������
                    if (drugBillClass.ApplyState != "0")
                    {
                        this.lblReprint.Visible = true;
                        this.nlbFirstPrintTime.Text = "�״δ�ӡ��" + info.Operation.ExamOper.OperTime.ToString();
                        this.nlbPrintTime.Text = "��ӡʱ�䣺" + DateTime.Now;
                    }
                    else
                    {
                        this.lblReprint.Visible = false;
                        this.nlbPrintTime.Text = "��ӡʱ�䣺" + info.Operation.ExamOper.OperTime.ToString();
                        this.nlbFirstPrintTime.Text = "";
                    }


                    for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
                    {
                        this.neuSpread1_Sheet1.Cells.Get(iRow, i).Border = topBorder;
                    }
                    totCost += FS.FrameWork.Public.String.FormatNumber(info.Operation.ApplyQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2);
                    iRow++;
                }

                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                //this.neuSpread1_Sheet1.Cells[iRow, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;
                this.neuSpread1_Sheet1.Rows[iRow].Border = topBorder;
                this.neuSpread1_Sheet1.Rows[iRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Rows[iRow].Font = new System.Drawing.Font("����", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1.SetCellValue(0, iRow, "���۽��", totCost.ToString());
                this.neuSpread1_Sheet1.Cells[iRow, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1.SetCellValue(0, iRow, "���ۼ�", "�ܽ�");
                iRow++;
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1_Sheet1.Rows[iRow].Height = 30;
                iRow++;
            }

            #endregion

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            #region ���õײ�����
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    int index = this.neuSpread1_Sheet1.Rows.Count;
                    totPageNO = (int)Math.Ceiling((double)index / pageRowNum);
                    for (int page = totPageNO; page > 0; page--)
                    {
                        if (page == totPageNO)
                        {

                            this.neuSpread1_Sheet1.AddRows(index, 1);
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;

                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "     ��ҩ��                          �˶ԣ�                          ��ҩ��                      ";
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("����", 10f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                            this.neuSpread1_Sheet1.Cells[index, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
                            //���ҳ�룬����ѡ��ҳ��ʱ��
                            this.neuSpread1_Sheet1.Rows[index].Tag = page;
                            continue;
                        }
                        this.neuSpread1_Sheet1.AddRows(page * pageRowNum, 1);

                        this.neuSpread1_Sheet1.Cells[page * pageRowNum, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
                        this.neuSpread1_Sheet1.Cells[page * pageRowNum, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        this.neuSpread1_Sheet1.Cells[page * pageRowNum, 0].Text = "ҳ��" + page.ToString() + "/" + totPageNO.ToString();

                        //���ҳ�룬����ѡ��ҳ��ʱ��
                        this.neuSpread1_Sheet1.Rows[index].Tag = page;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            this.ResumeLayout(true);
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
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillD", dept);
            //����Ӧֽ��
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 800;

                    int curHeight = 0;

                    int addHeight = this.validRowNum * (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    int additionAddHeight = 120;

                    paperSize.Width = width;
                    paperSize.Height = (addHeight + curHeight + additionAddHeight);

                    this.Height = paperSize.Height;
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

        #region ��ϸ�������ⷽ��

        /// <summary>
        /// ��ȡƵ�δ�����ÿ�����
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            //return 1000;

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
            else if (id == "pcd")//���ͺ�
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
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");
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
            if (alData == null || alData.Count == 0)
            {
                return;
            }
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
         //   this.PrintPage();
            this.Print();
        }


        #endregion

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        private class CompareApplyOutByPatient : IComparer
        {
            /// <summary>
            /// ���򷽷�
            /// </summary>
            //public int Compare(object x, object y)
            //{
            //    FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
            //    FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

            //    string oX = "";          //��������
            //    string oY = "";          //��������


            //    oX = o1.BedNO + o1.PatientName + this.GetFrequencySortNO(o1.Frequency) + o1.UseTime.ToString();
            //    oY = o2.BedNO + o2.PatientName + this.GetFrequencySortNO(o2.Frequency) + o2.UseTime.ToString(); 

            //    return string.Compare(oX, oY);
            //}
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //��������
                string oY = "";          //��������


                oX = o1.BedNO.Substring(4).PadLeft(4,'0') + o1.PatientName + this.GetFrequencySortNO(o1.Frequency) + this.GetOrderNo(o1) + o1.UseTime.ToString();
                oY = o2.BedNO.Substring(4).PadLeft(4,'0') + o2.PatientName + this.GetFrequencySortNO(o2.Frequency) + this.GetOrderNo(o2) + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }

            private string GetOrderNo(FS.HISFC.Models.Pharmacy.ApplyOut app)
            {
                string id = app.Item.ID.ToString();
                return id;
            }
            private string GetFrequencySortNO(FS.HISFC.Models.Order.Frequency f)
            {
                string id = f.ID.ToLower();
                string sortNO = "";
                if (id == "qd")
                {
                    sortNO = "1";
                }
                else if (id == "bid")
                {
                    sortNO = "2";
                }
                else if (id == "tid")
                {
                    sortNO = "3";
                }
                else
                {
                    sortNO = "4";
                }
                if (f.Name == "����")
                {
                    sortNO = "9999" + sortNO;
                }
                else
                {
                    sortNO = "0000" + sortNO;
                }
                return sortNO;
            }

        }

        /// <summary>
        /// ������
        /// </summary>
        private class CompareApplyOutByOrderNO : IComparer
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


                oX = o1.OrderNO + o1.UseTime.ToString();
                oY = o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }
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
        public void OldPrint()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            SOC.Windows.Forms.PrintPageSelectDialog printPageSelectDialog = new SOC.Windows.Forms.PrintPageSelectDialog();
            printPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
            printPageSelectDialog.MaxPageNO = this.totPageNO;
            printPageSelectDialog.ShowDialog();

            //��ʼҳ��Ϊ0��˵���û�ȡ����ӡ
            if (printPageSelectDialog.FromPageNO == 0)
            {
                return;
            }

            //��ӡȫ��
            if (printPageSelectDialog.FromPageNO == 1 && printPageSelectDialog.ToPageNO == this.totPageNO)
            {
                this.PrintPage();
                return;
            }

            //ѡ����ҳ
            int curPageNO = 1;
            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                if (this.neuSpread1_Sheet1.Rows[rowIndex].Tag != null)
                {
                    curPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows[rowIndex].Tag) + 1;
                }
                if (curPageNO >= printPageSelectDialog.FromPageNO && curPageNO <= printPageSelectDialog.ToPageNO)
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = false;
                    this.validRowNum--;
                }
            }

            this.PrintPage();

            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
            }

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
        }


        public void Print()
        {

            if (this.neuSpread1_Sheet1.RowCount==0||this.nlbRowCount.Text == "��¼����0")
            {
                return;
            }
            //���û��ϴ�ӡ�����ӡ
            FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();

            //��ȡά����ֽ��
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("Letter");
                //ָ����ӡ������default˵��ʹ��Ĭ�ϴ�ӡ���Ĵ���
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                //û��ά��ʱĬ��һ��ֽ��
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("Letter", 850, 1100);
                }
            }

            //��ӡ�߾ദ��
            print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, pageSize.Top, 0);

            //ֽ�Ŵ���
            print.PaperName = pageSize.Name;
            print.PaperHeight = pageSize.Height;
            print.PaperWidth = pageSize.Width;

            //��ӡ������
            print.PrinterName = pageSize.Printer;

            //ҳ����ʾ
            this.lbPageNO.Tag = "ҳ�룺{0}/{1}";
            print.PageNOControl = this.lbPageNO;

            //ҳü�ؼ�����ʾÿҳ����ӡ
            print.HeaderControls.Add(this.neuPanel1);
            //print.HeaderControl = this.neuPanel1;
            //ҳ�ſؼ�����ʾÿҳ����ӡ
            //print.FooterControls.Add(this.plBottom);

            //����ʾҳ��ѡ��
            print.IsShowPageNOChooseDialog = false;

            //����Աʹ��Ԥ������
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPageView(this);
            }
            else
            {
                print.PrintPage(this);
            }

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
        public SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.��ϸ;
            }
        }

        
        #endregion
    }
}