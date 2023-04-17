using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using FS.FrameWork.Management;
using System.Collections;
//using FS.HISFC.BizLogic.Pharmacy;
//using FS.HISFC.Models.Pharmacy;
//using FS.HISFC.Models.HealthRecord.EnumServer;
//using FS.HISFC.BizLogic.Registration;

namespace FS.SOC.Local.DrugStore.FOSI.Outpatient
{
    public partial class ucFoSiDrugList : UserControl
    {
        public ucFoSiDrugList()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //Init();
            }
        }     

        #region ����

        /// <summary>
        /// ��ҩ��
        /// </summary>
        decimal drugListTotalPrice = 0;

        /// <summary>
        /// ��ӡ�߶�
        /// </summary>
        int height = 0;

        /// <summary>
        /// �������
        /// </summary>
        private int rowMaxCount = 0;

        /// <summary>
        /// ҩ�������
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        FS.HISFC.BizLogic.Registration.Register regist = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        private bool isPrint = true;

        string sendWindows = "";

        private bool isMoreOnePage = false;
        #endregion

        private void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string diagnose, string hospitalName)
        {
            if (al.Count > 0)
            {
                //��Ч��ҩƷҳ��
                int pageNO = 1;
                //��Ч��ҩƷҳ��
                int pageInValidNO = 1;
                //����
                int iRow = 0;
                int num = 0;

                #region ��������ڷ�ҳ��ӡ����Ч�ĺ���Ч��ҩƷ��Ϣ�б�
                System.Collections.Hashtable hsApplyOut = new Hashtable();
                //��Ч��ҩƷ����
                Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applyOutPageList = new Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
                List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                //��Ч��ҩƷ����
                Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applyOutInValidPageList = new Dictionary<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>>();
                List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutInValidList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in al)
                {
                    //    if (applyOut.PrintState.ToString() == "1")
                    //    {
                    //        this.lbReprint.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        this.lbReprint.Visible = false;
                    //    }
                    if (drugRecipe.RecipeState.ToString() == "0")
                    {
                        this.lbReprint.Visible = false;
                    }
                    else
                    {
                        this.lbReprint.Visible = true;
                    }

                    //����ͬ��ϴ�ӡ
                    if (!hsApplyOut.Contains(applyOut.ID))
                    {
                        hsApplyOut.Add(applyOut.ID, null);

                        ArrayList alTmp = new ArrayList();
                        if (!string.IsNullOrEmpty(applyOut.CombNO))
                        {
                            //������²�ѯ�������������
                            //alTmp = this.itemManager.QueryApplyOutListForClinic(applyOut.CombNO);
                        }
                        if (alTmp == null)
                        {
                            alTmp = new ArrayList();
                        }

                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            applyOutList.Add(applyOut);

                        }
                        else
                        {
                            applyOutInValidList.Add(applyOut);
                        }
                    }
                    num++;

                    if (applyOutList.Count == 20 || (num == al.Count && applyOutList.Count > 0))
                    {
                        applyOutPageList.Add(pageNO, applyOutList);
                        applyOutList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        pageNO++;
                    }

                    if (applyOutInValidList.Count == 20 || (num == al.Count && applyOutInValidList.Count > 0))
                    {
                        applyOutInValidPageList.Add(pageInValidNO, applyOutInValidList);
                        applyOutInValidList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        pageInValidNO++;
                    }
                }

                #endregion

                FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)al[0];

                #region �������ݲ���ӡ
                foreach (KeyValuePair<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applist in applyOutInValidPageList)
                {
                    this.Clear();
                    this.label1.Text = "     �˵�";
                    this.label1.Font = new System.Drawing.Font("����", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.SystemColors.Control;
                    this.neuSpread1_Sheet1.Columns.Get(1).BackColor = System.Drawing.SystemColors.Control;
                    this.neuSpread1_Sheet1.Columns.Get(2).BackColor = System.Drawing.SystemColors.Control;

                    SetLableValue(drugRecipe, diagnose, hospitalName);
                    this.isMoreOnePage = (applyOutPageList.Count > 1);
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applayOut in applist.Value)
                    {
                        SetDrugDeatail(iRow, applayOut, true);
                        this.DrawLing(iRow);
                        iRow++;
                    }
                    //������ҩ�ۺͷ�ҩʱ��
                    //this.AddLastRow();
                    this.AddLastRow(iRow);

                    this.DrawLing(iRow);

                    drugListTotalPrice = 0;

                    iRow = 1;

                    this.Print();
                }
                iRow = 0;
                foreach (KeyValuePair<int, List<FS.HISFC.Models.Pharmacy.ApplyOut>> applist in applyOutPageList)
                {
                    // ����Lable��ֵ
                    this.Clear();
                    this.label1.Text = "��ɽ�е�������ҽԺҩ����";
                    this.label1.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
                    this.neuSpread1_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.White;
                    this.neuSpread1_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.White;

                    SetLableValue(drugRecipe, diagnose, hospitalName);
                    this.isMoreOnePage = (applyOutPageList.Count > 1);
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applayOut in applist.Value)
                    {
                        SetDrugDeatail(iRow, applayOut, true);
                        this.DrawLing(iRow);
                        iRow++;
                    }
                    //������ҩ�ۺͷ�ҩʱ��
                    //this.AddLastRow();
                    this.AddLastRow(iRow);

                    this.DrawLing(iRow);

                    drugListTotalPrice = 0;

                    iRow = 1;

                    this.Print();
                }
                #endregion

                this.isPrint = true;
            }
        }

        private void DrawLing(int iRow)
        { 
             FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, false);
             FarPoint.Win.LineBorder lineBorder12 = new FarPoint.Win.LineBorder(Color.Black, 1, false, false, false, true);

                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow, 0).Border = lineBorder11;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow, 1).Border = lineBorder11;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow, 2).Border = lineBorder11;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow + 3, 0).Border = lineBorder12;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow + 3, 1).Border = lineBorder12;
                 this.neuSpread1_Sheet1.Cells.Get(4 * iRow + 3, 2).Border = lineBorder12;
 
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            if (isPrint == true)
            {
                //this.neuPanel1.Dock = DockStyle.None;

                int heght = 138;

                int count = this.neuSpread1_Sheet1.Rows.Count;

                int addHeght = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows[0].Height);

                int addHeght1 = addHeght * count;

                heght += addHeght1;
 
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 400, 550));
                print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 460, heght + 160));
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.IsDataAutoExtend = false;
                try
                {
                    //�ռ÷�Ժ4�Ŵ����Զ���ӡ������ͣ����ӡ����ͷ��ֽ��̫����̫�񶼿���������ͣ
                    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                catch { }
                this.Size = new Size(460, heght);
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(0, 0, this);
                }
                else
                {
                    print.PrintPage(0, 0, this);
                }

                //this.neuPanel1.Dock = DockStyle.Fill;
                this.Clear();
            }

            this.isPrint = true;
        }

        /// <summary>
        /// ���������뷽��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// ����ؼ�����
        /// </summary>
        private void Clear()
        {
            lbName.Text = "����:";
            lbCardNo.Text = "";
            lbSex.Text = "�Ա�:";
            lbDiagnose.Text = "���:";
            lbAge.Text = "����:";
            lbDeptName.Text = "��������:";
            lbInvoice.Text = "��Ʊ��:";
            lbRecipe.Text = "������:";
            lblDoc.Text = "ҽʦ:";
            lblDoc.Text = "ҽʦ:";
            lblPhone.Text = "";
            lblPrintDate.Text = "";
            this.sendWindows = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.Clear();

        }

        /// <summary>
        /// ����Lable��ֵ
        /// </summary>
        /// <param name="info"></param>
        private void SetLableValue(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string diagnose, string hospitalName)
        {

            //����
            lbName.Text = "����:"+drugRecipe.PatientName.ToString();

            //����
            this.lbCardNo.Text = drugRecipe.CardNO.TrimStart('0').ToString();

            lbInvoice.Text = "";
            
            //�Ա�
            lbSex.Text =  "�Ա�"+drugRecipe.Sex.Name.ToString();

            //����
            lbAge.Text = "���䣺" + itemManager.GetAge(drugRecipe.Age);

            //ҽʦ
            this.lblDoc.Text = "ҽʦ��" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            //������
            this.lbRecipe.Text = "�����ţ�" + drugRecipe.RecipeNO;
            //��������
            lbDeptName.Text = "�������ƣ�" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            //���
            lbDiagnose.Text = "��ϣ�" + diagnose.ToString();

            //�绰
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            register = regist.GetByClinic(drugRecipe.ClinicNO);
            lblPhone.Text ="�绰��"+ register.PhoneHome;

             //��ӡʱ��
            lblPrintDate.Text = itemManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
 
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
        /// ��ȡƵ������
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetFrequency(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        { 
            FS.HISFC.Models.Order.Frequency frequency = applyOut.Frequency as FS.HISFC.Models.Order.Frequency;
            return Common.Function.GetFrequenceName(frequency);
        }

        /// <summary>
        /// ����Ϣ��ӵ��б���
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="info"></param>
        private void SetDrugDeatail(int iRow, FS.HISFC.Models.Pharmacy.ApplyOut info, bool isValid)
        {
            this.neuSpread1_Sheet1.Rows.Add(4 * iRow,4);

            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (item != null)
                {
                    if(item.NameCollection.RegularName != null && item.NameCollection.RegularName != "")
                    {
                        this.neuSpread1_Sheet1.SetText(4 * iRow, 0,iRow+1+" "+ item.NameCollection.RegularName.ToString());
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.SetText(4 * iRow, 0, iRow+1+"");
                    }
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 0, "");
                }

                if (info.Item.Specs == null || info.Item.Specs == "")
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 1, "");
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 1, info.Item.Specs.ToString());
                }

                int outMinQty;

                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
                if (outPackQty == 0)
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 2, ((int)(info.Operation.ApplyQty * info.Days)).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit);
                }
                else if (outMinQty == 0)
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 2, outPackQty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.PackUnit);
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow, 2, ((int)(info.Operation.ApplyQty * info.Days)).ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.MinUnit);
                }

                this.neuSpread1_Sheet1.SetText(4 * iRow + 1, 0, info.Item.NameCollection.Name.ToString());

                if (string.IsNullOrEmpty(info.DoseOnce.ToString()))
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 0, "�÷��� " + "��ҽ��");
                }
                this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 0, "�÷��� " + "ÿ��" + this.GetOnceDose(info).ToString() + "("+info.DoseOnce+info.Item.DoseUnit+")");

                if ((info.Frequency.ID) == null || info.Frequency.ID == "")
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 1, "");
                }
                else
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 1, this.GetFrequency(info));
                }

                this.neuSpread1_Sheet1.SetText(4 * iRow + 2, 2,SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
                try
                {
                    this.neuSpread1_Sheet1.SetText(4 * iRow + 3, 0, "ע�����" + Common.Function.GetOrder(info.OrderNO).Memo);
                }
                catch { }

                //��ҩ��
                this.drugListTotalPrice += (info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * (info.Operation.ApplyQty * info.Days);

        }

        /// <summary>
        /// �������һ��
        /// </summary>
        private void AddLastRow(int iRow)
        {
            try
            {
                this.neuSpread1_Sheet1.Rows.Add(4 * iRow,4); 
                this.neuSpread1_Sheet1.SetText(4 * iRow + 1, 0, "��ҩ��ǩ��:");
                this.neuSpread1_Sheet1.SetText(4 * iRow + 1, 1, "�˶���ǩ��:");
                this.neuSpread1_Sheet1.Rows[4 * iRow + 1].Font = new Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.neuSpread1_Sheet1.SetText(4 * iRow + 3, 0, string.Format("��ҩ�ۣ���{0} ��ӡʱ�䣺{1}    {2}",
                           new object[] { this.drugListTotalPrice.ToString("0.00"), itemManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"), this.sendWindows }));
            }
            catch { }
            //FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, true);
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Border = lineBorder11;
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 1).Border = lineBorder11;
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 2).Border = lineBorder11;
            //this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            ////this.neuSpread1_Sheet1.Cells.Get(this.rowMaxCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }

        /// <summary>
        /// ��ӡ��ҩ�嵥
        /// </summary>
        /// <param name="alData">��������ʵ��</param>
        /// <param name="diagnose">���</param>
        /// <param name="drugRecipe">����������Ϣ</param>
        /// <param name="drugTerminal">�ն���Ϣ</param>
        /// <returns></returns>
        public int PrintDrugBill(ArrayList alData, string diagnose, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, string hospitalName)
        {
            if (alData == null || drugRecipe == null)
            {
                return -1;
            }
            this.Init();
            //this.npbBarCode.Image = this.CreateBarCode(drugRecipe.RecipeNO);
            this.AddAllData(alData, drugRecipe, diagnose, hospitalName);

            return 0;
        }

    }
    
}
