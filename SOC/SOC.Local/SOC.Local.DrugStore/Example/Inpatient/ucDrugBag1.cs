using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.DrugStore
{
    /// <summary>
    /// ҩ����ӡ
    /// </summary>
    public partial class ucDrugBag : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        public ucDrugBag()
        {
            InitializeComponent();
        }

        private int MaxRows = 5;

        private static FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// ÿ�����ߵ�����
        /// </summary>
        private int patientTotNum = 1;

        private bool isLZ = false;

        private static int DataInit()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();
            if (alDept != null)
            {
                deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
            }

            return 1;
        }

        #region IDrugPrint ��Ա

        public void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
           
        }

        public void AddAllData(System.Collections.ArrayList al)
        {
            System.Collections.Hashtable hsPatientApply = new System.Collections.Hashtable();
            
            ArrayList sortPatientApply = new ArrayList();

            #region �����߷��� ��ͬһ���ߵ��������뼯����һ��

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)            
            {
                try
                {
                    //��ʱ�ڷ�ҩ����Ҫ����ÿ�����˵�ÿ��ҩ��ҽ��Ϊ��λ������ herong edit at 2011-07-21
                    if(info.Item.Type.Name.Contains("��ʱ")==true)
                    {
                        List<FS.HISFC.Models.Pharmacy.ApplyOut> temp = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                        temp.Add(info);
                        isLZ = true;
                        hsPatientApply.Add(info.ID, temp);
                        sortPatientApply.Add(temp);
                    }
                    else
                    {
                        isLZ = false;
                        string a = info.ExecNO;
                        if (info.PatientNO == "" || info.PatientNO == null)
                        {
                            continue;
                        }
                        if (hsPatientApply.ContainsKey(info.PatientNO))
                        {
                            (hsPatientApply[info.PatientNO] as List<FS.HISFC.Models.Pharmacy.ApplyOut>).Add(info);
                        }
                        else
                        {
                            List<FS.HISFC.Models.Pharmacy.ApplyOut> temp = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                            temp.Add(info);

                            hsPatientApply.Add(info.PatientNO, temp);
                        }
                    }
                }
                catch { }
            }

            #endregion

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            DateTime sysTime = dataManager.GetDateTimeFromSysDateTime();

            this.lbPrintDate.Text = "��ӡʱ�䣺" + sysTime.ToString("HH:mm:ss");

            #region ���ߴ�ӡ �����ߴ�ӡÿ�����ߵ���ҩ��ϸ
            
            if(isLZ == true)
            {
                //��ʱ�ڷ�ҩ�����մ�������
                SortBedNo sortbed = new SortBedNo();
                sortPatientApply.Sort(sortbed);
                foreach ( List<FS.HISFC.Models.Pharmacy.ApplyOut> alPatient in sortPatientApply)
                {
                    if ( alPatient.Count <= 0 )
                    {
                        break;
                    }

                    FS.HISFC.Models.Pharmacy.ApplyOut temp = alPatient[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    //������ҩʱ�������
                    SortOrder sort = new SortOrder();
                    alPatient.Sort(sort);
                  




                    #region Label��Ϣ��ʾ

                    // FS.HISFC.Models.Pharmacy.ApplyOut temp = alPatient[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    string b = temp.ExecNO;
                    if ( deptHelper == null )
                    {
                        DataInit();
                    }
                    if ( deptHelper != null )
                    {
                        this.lbDept.Text = deptHelper.GetName(temp.ApplyDept.ID);
                    }
                    else
                    {
                        this.lbDept.Text = temp.ApplyDept.ID;
                    }
                    if ( temp.User02.Length > 3 )
                    {
                        this.lbBed.Text = temp.User02.Substring(4);
                    }
                    else
                    {
                        this.lbBed.Text = temp.User02;
                    }
                    this.neuLabel4.Text = temp.Item.Type.Name;
                    this.lbName.Text = "     " + temp.User01;
                    this.lbPatientNO.Text = "     " + temp.PatientNO.Substring(4);

                    #endregion

                    string privUsePoint = "-1";

                    this.neuSpread1_Sheet1.Rows.Count = 0;

                    List<List<FS.HISFC.Models.Pharmacy.ApplyOut>> patientList = new List<List<FS.HISFC.Models.Pharmacy.ApplyOut>>();

                    List<FS.HISFC.Models.Pharmacy.ApplyOut> useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                    int iCount = 1;
                    foreach ( FS.HISFC.Models.Pharmacy.ApplyOut apply in alPatient )
                    {
                        #region ��ÿ�����ߵ� ����ҽ����Ϻš���ҩʱ����з���

                        if ( privUsePoint == "-1" )            //��һ�����
                        {
                            useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                            useList.Add(apply);

                            privUsePoint = apply.User03;
                            iCount = 1;
                        }
                        else if ( privUsePoint == apply.User03 )         //ͬһƵ�ε��ҩƷ
                        {
                            if ( iCount > 5 )                 //�����ҩƷ �軻ҳ
                            {
                                patientList.Add(useList);

                                useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                                useList.Add(apply);
                                iCount = 1;
                            }
                            else
                            {
                                useList.Add(apply);
                                iCount++;
                            }
                        }
                        else                              //��ͬƵ�ε��ҩƷ
                        {
                            patientList.Add(useList);

                            useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                            useList.Add(apply);
                            iCount = 1;

                            privUsePoint = apply.User03;
                        }

                        #endregion
                    }

                    if ( useList.Count > 0 )
                    {
                        patientList.Add(useList);
                    }

                    //patientList.Count Ϊ�û��ߵ����е�����
                    int iPageNO = 1;
                    foreach ( List<FS.HISFC.Models.Pharmacy.ApplyOut> singleList in patientList )
                    {
                        this.neuSpread1_Sheet1.Rows.Count = 0;
                        if ( patientList.Count > 0 )
                        {
                            SortApplyOut s = new SortApplyOut();

                            //������
                            //MessageBox.Show(patientList.Count.ToString());
                            singleList.Sort(s);
                        }
                        foreach ( FS.HISFC.Models.Pharmacy.ApplyOut apply in singleList )
                        {
                            string useTime = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(apply.User03), sysTime);

                            int iRowIndex = this.neuSpread1_Sheet1.Rows.Count;
                            this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
                            this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = apply.Item.Name;
                            this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text = ( apply.Operation.ApplyQty * apply.Days ).ToString() + apply.Item.MinUnit + " " + apply.DoseOnce + apply.Item.DoseUnit;
                            this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = useTime;

                            this.lbDate.Text = "         " + FS.FrameWork.Function.NConvert.ToDateTime(apply.User03).ToString("yy-MM-dd");
                            this.lbTime.Text = "         " + useTime;
                            this.lbPage.Text = iPageNO.ToString() + " / " + patientList.Count.ToString();
                            //�ƶ�����ϵͳ��Ҫ��ӡ���룬�����ó����������ΪΨһʶ�� herong edit at 2011-06-27
                            this.neuLabel3.Text = apply.ExecNO;
                            this.neuLabel3.Visible = false;
                            if ( this.neuLabel4.Text.Contains("��ʱ") )
                            {
                                this.neuLabel5.Text ="Ƶ��:" + apply.Frequency.Name;
                                this.neuLabel6.Text ="ÿ����:" + apply.DoseOnce.ToString() + apply.Item.DoseUnit;
                            }
                        }
                        if ( this.neuLabel4.Text.Contains("��ʱ") )
                        {
                            this.PrintLZ();
                        }
                        else
                        {
                            this.Print();
                        }

                        iPageNO++;
                    }

                }
            }
            else
            {



                foreach (List<FS.HISFC.Models.Pharmacy.ApplyOut> alPatient in hsPatientApply.Values)
                {
                    if (alPatient.Count <= 0)
                    {
                        break;
                    }

                    FS.HISFC.Models.Pharmacy.ApplyOut temp = alPatient[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    
                    //������ҩʱ�������
                    SortOrder sort = new SortOrder();
                    alPatient.Sort(sort);
                    


                    

                    #region Label��Ϣ��ʾ

                   // FS.HISFC.Models.Pharmacy.ApplyOut temp = alPatient[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    string b = temp.ExecNO;
                    if (deptHelper == null)
                    {
                        DataInit();
                    }
                    if (deptHelper != null)
                    {
                        this.lbDept.Text = deptHelper.GetName(temp.ApplyDept.ID);
                    }
                    else
                    {
                        this.lbDept.Text = temp.ApplyDept.ID;
                    }
                    if (temp.User02.Length > 3)
                    {
                        this.lbBed.Text = temp.User02.Substring(4);
                    }
                    else
                    {
                        this.lbBed.Text = temp.User02;
                    }
                    this.neuLabel4.Text = temp.Item.Type.Name;
                    this.lbName.Text = "     " + temp.User01;
                    this.lbPatientNO.Text = "     " + temp.PatientNO.Substring(4);

                    #endregion

                    string privUsePoint = "-1";

                    this.neuSpread1_Sheet1.Rows.Count = 0;

                    List<List<FS.HISFC.Models.Pharmacy.ApplyOut>> patientList = new List<List<FS.HISFC.Models.Pharmacy.ApplyOut>>();

                    List<FS.HISFC.Models.Pharmacy.ApplyOut> useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                    int iCount = 1;
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut apply in alPatient)
                    {
                        #region ��ÿ�����ߵ� ����ҽ����Ϻš���ҩʱ����з���

                        if (privUsePoint == "-1")            //��һ�����
                        {
                            useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                            useList.Add(apply);

                            privUsePoint = apply.User03;
                            iCount = 1;
                        }
                        else if (privUsePoint == apply.User03)         //ͬһƵ�ε��ҩƷ
                        {
                            if (iCount > 5)                 //�����ҩƷ �軻ҳ
                            {
                                patientList.Add(useList);

                                useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                                useList.Add(apply);
                                iCount = 1;
                            }
                            else
                            {
                                useList.Add(apply);
                                iCount++;
                            }
                        }
                        else                              //��ͬƵ�ε��ҩƷ
                        {
                            patientList.Add(useList);

                            useList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                            useList.Add(apply);
                            iCount = 1;

                            privUsePoint = apply.User03;
                        }

                        #endregion
                    }

                    if (useList.Count > 0)
                    {
                        patientList.Add(useList);
                    }

                    //patientList.Count Ϊ�û��ߵ����е�����
                    int iPageNO = 1;
                    foreach (List<FS.HISFC.Models.Pharmacy.ApplyOut> singleList in patientList)
                    {
                        this.neuSpread1_Sheet1.Rows.Count = 0;
                        if (patientList.Count > 0)
                        {
                            SortApplyOut s =  new SortApplyOut();

                            //������
                            //MessageBox.Show(patientList.Count.ToString());
                            singleList.Sort(s);
                        }
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut apply in singleList)
                        {
                            string useTime = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(apply.User03), sysTime);

                            int iRowIndex = this.neuSpread1_Sheet1.Rows.Count;
                            this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
                            this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = apply.Item.Name;
                            this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text = (apply.Operation.ApplyQty * apply.Days).ToString() + apply.Item.MinUnit + " " + apply.DoseOnce + apply.Item.DoseUnit;
                            this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = useTime;

                            this.lbDate.Text = "         " + FS.FrameWork.Function.NConvert.ToDateTime(apply.User03).ToString("yy-MM-dd");
                            this.lbTime.Text = "         " + useTime;
                            this.lbPage.Text = iPageNO.ToString() + " / " + patientList.Count.ToString();
                            //�ƶ�����ϵͳ��Ҫ��ӡ���룬�����ó����������ΪΨһʶ�� herong edit at 2011-06-27
                            this.neuLabel3.Text = apply.ExecNO;
                            this.neuLabel3.Visible = false;
                        }
                        if(this.neuLabel4.Text.Contains("��ʱ"))
                        {
                          this.PrintLZ();
                        }
                        else
                        {
                          this.Print();
                        }

                        iPageNO++;
                    }

                    //foreach (FS.HISFC.Models.Pharmacy.ApplyOut apply in alPatient)
                    //{
                    //    if (privOrderID == "")                  //��һ��ҩ
                    //    {
                    //        privOrderID = apply.OrderNO;
                    //        useTime = this.FormatDateTime(FS.NFC.Function.NConvert.ToDateTime(apply.User03), sysTime);

                    //        this.neuSpread1_Sheet1.Cells[iCount, 0].Text = apply.Item.Name + "��" + Function.DrugDosage.GetStaticDosage(apply.Item.ID) + "[" + apply.Item.Specs + "]";
                    //        this.neuSpread1_Sheet1.Cells[iCount, 1].Text = "";
                    //        this.neuSpread1_Sheet1.Cells[iCount, 2].Text = useTime;

                    //        useList.Add(apply);
                    //    }
                    //    else if (privOrderID == apply.OrderNO)        //��һ��ҩ
                    //    {
                    //        useTime = useTime + this.FormatDateTime(FS.NFC.Function.NConvert.ToDateTime(apply.User03), sysTime);
                    //        this.neuSpread1_Sheet1.Cells[iCount, 2].Text = useTime;

                    //        useList.Add(apply);
                    //    }
                    //    else                                    //��ͬ��ҩƷ
                    //    {
                    //        if (iCount == this.MaxRows - 1)
                    //        {
                    //            #region ��ӡÿ��ʱ�����ϸ

                    //            this.PrintDetail(useList, sysTime);

                    //            useList.Clear();

                    //            #endregion

                    //            this.Clear();
                    //        }
                    //        else
                    //        {
                    //            iCount++;

                    //            privOrderID = apply.OrderNO;
                    //            useTime = this.FormatDateTime(FS.NFC.Function.NConvert.ToDateTime(apply.User03), sysTime);

                    //            this.neuSpread1_Sheet1.Cells[iCount, 0].Text = apply.Item.Name + "��" + Function.DrugDosage.GetStaticDosage(apply.Item.ID) + "[" + apply.Item.Specs + "]";
                    //            this.neuSpread1_Sheet1.Cells[iCount, 1].Text = "";
                    //            this.neuSpread1_Sheet1.Cells[iCount, 2].Text = useTime;

                    //            useList.Add(apply);
                    //        }
                    //    }
                    //}

                    //if (useList.Count > 0)
                    //{
                    //    this.PrintDetail(useList, sysTime);
                    //}
                }
            }
            #endregion
        }

        public void AddCombo(System.Collections.ArrayList alCombo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public decimal DrugTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public decimal LabelTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public FS.HISFC.Models.Registration.Register OutpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public void PrintRecipeNo(ArrayList al)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void PrintRecipeNo(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Preview()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
           
            FS.HISFC.BizLogic.Manager.PageSize manager = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize paper = manager.GetPageSize("DrugBag");
            if (paper == null)
            {
                paper = new FS.HISFC.Models.Base.PageSize("DrugBag", 800, 1100);
            }
            print.SetPageSize(paper);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPreview(30, 10, this);
        }

        public void Print()
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = false;
            //this.lblCardBarCode.Text = this.GetCardBarCode(regInfo.PID.CardNO);
            this.neuPictureBox1.Image = b.Encode(BarcodeLib.TYPE.CODE128, this.neuLabel3.Text.Trim());

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.BizLogic.Manager.PageSize manager = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize paper = manager.GetPageSize("DrugBag");
            if (paper == null)
            {
                paper = new FS.HISFC.Models.Base.PageSize("DrugBag", 800, 1100);
            }
            print.SetPageSize(paper);
            System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            pSet.Landscape = true;

            print.PrintDocument.DefaultPageSettings.Landscape = true;

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 50, this);
           // print.PrintPreview(0, 50, this);

            this.neuSpread1_Sheet1.Rows.Count = 0;

        }


        public void PrintLZ()
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = false;
            //this.lblCardBarCode.Text = this.GetCardBarCode(regInfo.PID.CardNO);
            this.neuPictureBox1.Image = b.Encode(BarcodeLib.TYPE.CODE128, this.neuLabel3.Text.Trim());

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.BizLogic.Manager.PageSize manager = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize paper = manager.GetPageSize("DrugBag");
            if (paper == null)
            {
                paper = new FS.HISFC.Models.Base.PageSize("DrugBag", 800, 1100);
            }
            print.SetPageSize(paper);
            System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            pSet.Landscape = true;

            print.PrintDocument.DefaultPageSettings.Landscape = true;
            print.PrintDocument.PrinterSettings.PrinterName = "\\\\172.16.105.44\\Bag";//ԤԼ�Һ�ƾ֤��ӡ������
            //print.PrintDocument.PrinterSettings.PrinterName = "Bag";
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 50, this);
            // print.PrintPreview(0, 50, this);

            this.neuSpread1_Sheet1.Rows.Count = 0;

        }


        #endregion

        private void Clear()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count - 1; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                this.neuSpread1_Sheet1.Cells[i, 2].Text = "";
            }
        }

        private string FormatDateTime(DateTime dt, DateTime sysdate)
        {
            try
            {
                if (sysdate.Date.AddDays(-1) == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0') + dt.ToString("tt");
                }
                else if (sysdate.Date == dt.Date)
                {
                    return dt.Hour.ToString().PadLeft(2, '0') + dt.ToString("tt");
                }
                else if (sysdate.Date.AddDays(1) == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0') + dt.ToString("tt");
                }
                else if (sysdate.Date.AddDays(2) == dt.Date)
                {
                    return "��" + dt.Hour.ToString().PadLeft(2, '0') + dt.ToString("tt");
                }
                else
                {
                    return dt.Hour.ToString().PadLeft(2, '0') + dt.ToString("tt");
                }
            }
            catch
            {
                return dt.Hour.ToString().PadLeft(2, '0') + dt.ToString("tt");
            }
        }


        public class SortBedNo : IComparer
        {
            #region IComparer<ApplyOut> ��Ա
            public SortBedNo()
            {

            }

            public int Compare(object o1, object o2)
            {
                List<FS.HISFC.Models.Pharmacy.ApplyOut> oX = o1 as List<FS.HISFC.Models.Pharmacy.ApplyOut>;          //����
                List<FS.HISFC.Models.Pharmacy.ApplyOut> oY = o2 as List<FS.HISFC.Models.Pharmacy.ApplyOut>;          //����              

                int nComp;

                if ( (oX[0] as FS.HISFC.Models.Pharmacy.ApplyOut).User02 == null )
                {
                    nComp = ( ( oY[0] as FS.HISFC.Models.Pharmacy.ApplyOut ).User02 != null ) ? -1 : 0;
                }
                else if ( ( oY[0] as FS.HISFC.Models.Pharmacy.ApplyOut ).User02 == null )
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(( oX[0] as FS.HISFC.Models.Pharmacy.ApplyOut ).User02.ToString(), ( oY[0] as FS.HISFC.Models.Pharmacy.ApplyOut ).User02.ToString());
                }

                return nComp;
            }

            #endregion
        }


        public class SortOrder : IComparer<FS.HISFC.Models.Pharmacy.ApplyOut>
        {
            #region IComparer<ApplyOut> ��Ա

            public int Compare(FS.HISFC.Models.Pharmacy.ApplyOut o1, FS.HISFC.Models.Pharmacy.ApplyOut o2)
            {
                string oX = o1.User03;          //��ҩʱ��
                string oY = o2.User03;          //��ҩʱ��              

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
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion
        }

        public class SortApplyOut : IComparer<FS.HISFC.Models.Pharmacy.ApplyOut>
        {
            #region IComparer<ApplyOut> ��Ա

            public int Compare(FS.HISFC.Models.Pharmacy.ApplyOut o1, FS.HISFC.Models.Pharmacy.ApplyOut o2)
            {
                string oX = o1.Item.Name;          //��ҩʱ��
                string oY = o2.Item.Name;          //��ҩʱ��              

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
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion
        }
    }
}
