using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace InterfaceInstanceDefault.IRecipePrint
{
    public partial class ucRecipePrintNormal : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IRecipePrint, FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        /// <summary>
        /// ������ӡ
        /// </summary>
        public ucRecipePrintNormal()
        {
            InitializeComponent();
        }

        #region ����

        private string myRecipeNO = "";

        FS.HISFC.Models.RADT.PatientInfo patientinfo;
        FS.HISFC.Models.Registration.Register register;
        /// <summary>
        /// ��ҩ����ҩ����ÿҳ����
        /// </summary>
        private int pPrintNum = 0;
        /// <summary>
        /// ��ҩ����ҩ�Ƿ���Դ�ӡ��һ�Ŵ���
        /// </summary>
        private bool isSameRecipe = true;
        /// <summary>
        /// ��ҩ����ÿҳ����
        /// </summary>
        private int pccPrintNum = 0;
        /// <summary>
        /// �Һ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegister = new FS.HISFC.Models.Registration.Register();
        /// <summary>
        /// ҽ��ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Order.OutPatient.Order orderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManagemnt = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private FS.HISFC.BizLogic.Registration.Register reg = new FS.HISFC.BizLogic.Registration.Register();
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaManagement = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// {B8B67F3B-397F-4e21-9A87-56BD52E0C042}
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���ҵ���{787A81FD-9E3D-4cc3-A932-95A686A89B0A}
        /// </summary>
        private FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseIntegrate = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        #region addby xuewj 2010-3-31 {67B867B1-96BD-454a-9BE0-E4DD6EB3E995} �в�ҩҽ����ӡ����
        /// <summary>
        /// sheetView
        /// </summary>
        private FarPoint.Win.Spread.SheetView view = new FarPoint.Win.Spread.SheetView();

        /// <summary>
        /// ��ҩ����ÿ����ʾ��ҩƷ��
        /// </summary>
        private int pccPerRowCount = 0;

        /// <summary>
        /// ��ҩ����ÿҳ��ʾ������
        /// </summary>
        private int pccPerPageCount = 0;

        /// <summary>
        /// ��ҩ��ӡ�����Ƿ��ʼ����
        /// </summary>
        private bool isInitial = false;

        /// <summary>
        /// ��ö��
        /// </summary>
        enum Columns
        {
            /// <summary>
            /// ����
            /// </summary>
            drugName,
            /// <summary>
            /// ���
            /// </summary>
            specs,
            /// <summary>
            /// ��Ϻ�
            /// </summary>
            comboNO,
            /// <summary>
            /// ��
            /// </summary>
            hearbalQty,
            /// <summary>
            /// ��
            /// </summary>
            comboFlag,
            /// <summary>
            /// ����
            /// </summary>
            doseOnce,
            /// <summary>
            /// �÷�
            /// </summary>
            usage,
            /// <summary>
            /// Ƶ��
            /// </summary>
            frequence,
            /// <summary>
            /// ����
            /// </summary>
            totQty,
            /// <summary>
            /// Ժע
            /// </summary>
            injectCount,
            /// <summary>
            /// ��ע
            /// </summary>
            memo

        }
        #endregion
        #endregion

        #region ����

        /// <summary>
        /// 
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get 
            {
                return this.myRegister;
            }
            set
            {
                this.myRegister = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ȡ���Ʋ���
        /// </summary>
        private void GetArgument()
        {
            pPrintNum = this.controlManagemnt.GetControlParam<int>("200031", false, 99);
            pccPrintNum = this.controlManagemnt.GetControlParam<int>("200033", false, 99);
            isSameRecipe = this.controlManagemnt.GetControlParam<bool>("200032", false, true);

            #region addby xuewj 2010-3-31 {67B867B1-96BD-454a-9BE0-E4DD6EB3E995} �в�ҩҽ����ӡ����
            pccPerRowCount = this.controlManagemnt.GetControlParam<int>("200043", false, 4);
            pccPerPageCount = this.controlManagemnt.GetControlParam<int>("200044", false, 17);
            #endregion
        }

        /// <summary>
        /// ���û��߻�����Ϣ
        /// </summary>
        private void SetPatient()
        {
            this.GetArgument();

            if (this.myRegister == null)
            {
                return;
            }
            DateTime myOperDate = System.DateTime.MinValue;

            myOperDate = this.orderManagement.QueryMaxOperTimeByClinicCode(this.myRegister.ID);

            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory clinicCaseHistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
            clinicCaseHistory = this.orderManagement.QueryCaseHistoryByClinicCode(this.myRegister.ID, myOperDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (clinicCaseHistory != null)
            {
                if (clinicCaseHistory.ID != "" && clinicCaseHistory.ID != null)
                {
                    this.lblDiagnose.Text = clinicCaseHistory.CaseDiag;
                }
                else
                {
                    this.lblDiagnose.Text = "";
                }
            }
            else
            {
                this.lblDiagnose.Text = "";
            }

            #region �����в����������ȥmet_com_diagnose�����������  {787A81FD-9E3D-4cc3-A932-95A686A89B0A}

            if (this.lblDiagnose.Text=="")
            {
                ArrayList alDiagnoses = this.diagnoseIntegrate.QueryDiagnoseNoOps(this.myRegister.ID);
                if (alDiagnoses != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diagnose in alDiagnoses)
                    {
                        if (diagnose.DiagInfo.DiagType.ID == "10")
                        {
                            if (this.lblDiagnose.Text != "")
                            {
                                this.lblDiagnose.Text += "|" + diagnose.DiagInfo.Name;
                            }
                            else
                            {
                                this.lblDiagnose.Text = diagnose.DiagInfo.Name;
                            }                            
                        }
                    }
                }
            }

            #endregion

            #region {B8B67F3B-397F-4e21-9A87-56BD52E0C042}

            this.lblTitle.Text = managerIntegrate.GetHospitalName() + " �� �� ��";

            #endregion

            this.lblPact.Text = this.myRegister.Pact.Name;
            this.lblDept.Text = this.myRegister.DoctorInfo.Templet.Dept.Name;

            this.lblName.Text = this.myRegister.Name;
            this.lblCardNO.Text = this.myRegister.PID.CardNO;
            this.lblSex.Text = this.myRegister.Sex.Name;
            this.lblAge.Text = orderManagement.GetAge(this.myRegister.Birthday);

            this.lblSICard.Text = this.myRegister.SIMainInfo.RegNo;
            this.lblAddress.Text = this.myRegister.AddressHome + "   " + this.myRegister.PhoneHome;

            DateTime sysDate = this.orderManagement.GetDateTimeFromSysDateTime();
            this.lblSeeDate.Text = sysDate.ToString( "yyyy��MM��dd��" );

            this.lblSeeDoctor.Text = this.orderManagement.Operator.Name;
        }

        /// <summary>
        /// ��ѯҽ��
        /// </summary>
        private void QueryOrder()
        {
            ArrayList alOrder = new ArrayList();
            ArrayList alPRecipe = new ArrayList();
            ArrayList alPCZRecipe = new ArrayList();
            ArrayList alPCCRecipe = new ArrayList();
            alOrder = orderManagement.QueryOrder(this.myRecipeNO);
            if (alOrder.Count <= 0 || alOrder == null)
            {
                return;
            }
            #region �������ŷ�������
            System.Collections.Generic.Dictionary<string, ArrayList> alRecipeNoOrder
                = new Dictionary<string, ArrayList>();
            while (alOrder.Count>0)
            {
                string recepeNo = string.Empty;
                FS.HISFC.Models.Order.OutPatient.Order o = alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order;
                if (o != null)
                {
                    recepeNo = o.ReciptNO.ToString();
                }
                if (alRecipeNoOrder.ContainsKey(recepeNo) == false)
                {
                    alRecipeNoOrder.Add(recepeNo, new ArrayList());
                }
                alRecipeNoOrder[recepeNo].Add(o);
                alOrder.RemoveAt(0);
            }
            foreach (ArrayList a in alRecipeNoOrder.Values )
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in a)
                {
                    //if (order.Item.IsPharmacy)
                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if ((order.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID != "S" &&
                            (order.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID != "P" && order.Status != 3)
                        {
                            if (order.Item.SysClass.ID.ToString() == "PCC")
                            {
                                alPCCRecipe.Add(order);
                            }
                            else
                            {
                                if (isSameRecipe)
                                {
                                    alPRecipe.Add(order);
                                }
                                else
                                {
                                    if (order.Item.SysClass.ID.ToString() == "P")
                                    {
                                        alPRecipe.Add(order);
                                    }
                                    else
                                    {
                                        alPCZRecipe.Add(order);
                                    }
                                }
                            }

                        }
                    }
                }
                this.PrintRecipe(alPRecipe, false);
                this.PrintRecipe(alPCZRecipe, false);
                this.PrintRecipe(alPCCRecipe, true);
            }

            #endregion
          
        }

        /// <summary>
        /// ���ɴ���
        /// </summary>
        /// <param name="alOrder"></param>
        private void SetRecipeByApplyOut(ArrayList alApplyOut)
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut ord in alApplyOut)
            {
                int count = this.fpSpread1_Sheet1.Rows.Count;

                this.fpSpread1_Sheet1.Rows.Add(count, 1);

                count = this.fpSpread1_Sheet1.Rows.Count;

                if (ord == alApplyOut[0])
                {
                    if (ord.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Columns[3].Visible = true;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Columns[3].Visible = false;
                    }
                }
                this.fpSpread1_Sheet1.Cells[count - 1, 0].Text = ord.Item.Name;
                this.fpSpread1_Sheet1.Cells[count - 1, 1].Text = ord.Item.Specs;
                this.fpSpread1_Sheet1.Cells[count - 1, 2].Text = "";
                this.fpSpread1_Sheet1.Cells[count - 1, 3].Text = "";
                this.fpSpread1_Sheet1.Cells[count - 1, 5].Text = ord.DoseOnce.ToString();
                this.fpSpread1_Sheet1.Cells[count - 1, 7].Text = ord.Frequency.Name;
                this.fpSpread1_Sheet1.Cells[count - 1, 6].Text = ord.Usage.Name;

                this.fpSpread1_Sheet1.Cells[count - 1, 9].Text = "";
                this.fpSpread1_Sheet1.Cells[count - 1, 10].Text = "";

                this.lblRecipeNO.Text = ord.RecipeNO;
            }
        }

        /// <summary>
        /// ���ɴ���
        /// </summary>
        /// <param name="alOrder"></param>
        private void SetRecipe(ArrayList alOrder)
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            }

            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                int count = this.fpSpread1_Sheet1.Rows.Count;

                this.fpSpread1_Sheet1.Rows.Add(count, 1);
                                
                count = this.fpSpread1_Sheet1.Rows.Count;
                
                if (ord == alOrder[0])
                {
                    if (ord.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Columns[3].Visible = true;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Columns[3].Visible = false;
                    }
                }
                this.fpSpread1_Sheet1.Cells[count - 1, 0].Text = ord.Item.Name;
                this.fpSpread1_Sheet1.Cells[count - 1, 1].Text = ord.Item.Specs;
                this.fpSpread1_Sheet1.Cells[count - 1, 2].Text = ord.Combo.ID;
                this.fpSpread1_Sheet1.Cells[count - 1, 3].Text = ord.HerbalQty.ToString();
                this.fpSpread1_Sheet1.Cells[count - 1, 5].Text = ord.DoseOnce.ToString() + ord.DoseUnit;
                this.fpSpread1_Sheet1.Cells[count - 1, 7].Text = ord.Frequency.Name;
                this.fpSpread1_Sheet1.Cells[count - 1, 6].Text = ord.Usage.Name;
                FS.HISFC.Models.Pharmacy.Item phaItem = phaManagement.GetItem(ord.Item.ID);
                if (ord.MinunitFlag == "1")
                {
                    this.fpSpread1_Sheet1.Cells[count - 1, 8].Text = ord.Qty.ToString() + ord.Unit;
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[count - 1, 8].Text = Convert.ToString(ord.Qty * ord.Item.PackQty) + phaItem.MinUnit;
                }
                
                this.fpSpread1_Sheet1.Cells[count - 1, 9].Text = ord.InjectCount.ToString();
                this.fpSpread1_Sheet1.Cells[count - 1, 10].Text = ord.Memo;

                FS.HISFC.Components.Order.Classes.Function.DrawCombo(this.fpSpread1_Sheet1, 2, 4, 0);

                this.lblRecipeNO.Text = ord.ReciptNO;
            }
        }

        /// <summary>
        /// ���ɴ���
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="isSamePre"></param>
        /// <param name="isSameNext"></param>
        private void SetRecipe(ArrayList alOrder,bool isSamePre,bool isSameNext)
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            }
            this.fpSpread1.ActiveSheet.Rows.Default.Height = 40;

            //FarPoint.Win.Spread.CellType.TextCellType tCell = new FarPoint.Win.Spread.CellType.TextCellType();
            //tCell.Multiline = true;
            //tCell.WordWrap = true;
            //this.fpSpread1_Sheet1.Rows.Default.CellType = tCell;

            decimal totCost = 0;

            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                int count = this.fpSpread1_Sheet1.Rows.Count;

                this.fpSpread1_Sheet1.Rows.Add(count, 1);
                count = this.fpSpread1_Sheet1.Rows.Count;

                if (ord == alOrder[0])
                {
                    if (ord.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Columns[3].Visible = true;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Columns[3].Visible = false;
                    }
                }
                this.fpSpread1_Sheet1.Cells[count - 1, 0].Text = ord.Item.Name;

                //�����и�����Ӧ              
                //this.fpSpread1_Sheet1.Cells[count - 1, 0].CellType = tCell;
                //float fpHeight = this.fpSpread1_Sheet1.GetPreferredRowHeight( count - 1 );
                //this.fpSpread1_Sheet1.Rows[count - 1].Height = fpHeight + 1;

                this.fpSpread1_Sheet1.Cells[count - 1, 1].Text = ord.Item.Specs;
                this.fpSpread1_Sheet1.Cells[count - 1, 2].Text = ord.Combo.ID;
                this.fpSpread1_Sheet1.Cells[count - 1, 3].Text = ord.HerbalQty.ToString();
                this.fpSpread1_Sheet1.Cells[count - 1, 5].Text = ord.DoseOnce.ToString() + ord.DoseUnit;
                this.fpSpread1_Sheet1.Cells[count - 1, 7].Text = ord.Frequency.Name;

                //�����и�����Ӧ              
                //this.fpSpread1_Sheet1.Cells[count - 1, 7].CellType = tCell;
                //this.fpSpread1_Sheet1.Cells[count - 1, 0].CellType = tCell;

                this.fpSpread1_Sheet1.Cells[count - 1, 6].Text = ord.Usage.Name;
                FS.HISFC.Models.Pharmacy.Item phaItem = phaManagement.GetItem(ord.Item.ID);
                if (ord.MinunitFlag == "1")
                {
                    this.fpSpread1_Sheet1.Cells[count - 1, 8].Text = ord.Qty.ToString() + ord.Unit;
                    totCost = Math.Round( totCost + ord.Qty / phaItem.PackQty * phaItem.PriceCollection.RetailPrice, 2 );
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[count - 1, 8].Text = Convert.ToString(ord.Qty * ord.Item.PackQty) + phaItem.MinUnit;
                    totCost = Math.Round( totCost + ord.Qty / phaItem.PackQty * phaItem.PriceCollection.RetailPrice, 2 );
                }

                this.fpSpread1_Sheet1.Cells[count - 1, 9].Text = ord.InjectCount.ToString();
                this.fpSpread1_Sheet1.Cells[count - 1, 10].Text = ord.Memo;

                FS.HISFC.Components.Order.Classes.Function.DrawCombo(this.fpSpread1_Sheet1, 2, 4, 0);

                this.lblRecipeNO.Text = ord.ReciptNO;
            }

            this.lblSummary.Text = totCost.ToString();

            if (isSamePre)
            {
                if (this.fpSpread1_Sheet1.Cells[0, 4].Text == "��")
                {
                    this.fpSpread1_Sheet1.Cells[0, 4].Text = "��";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[0, 4].Text = "��";
                }
            }
            if (isSameNext)
            {
                if (this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 4].Text == "��")
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 4].Text = "��";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 4].Text = "��";
                }
            }
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="alRecipe"></param>
        /// <param name="isPCC"></param>
        private void PrintRecipe(ArrayList alRecipe,bool isPCC)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.IsDataAutoExtend = true;
            p.IsCanCancel = true;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            if (isPCC)
            {
                #region addby xuewj 2010-3-31 {67B867B1-96BD-454a-9BE0-E4DD6EB3E995} �в�ҩҽ����ӡ
                //int count = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(alRecipe.Count) / Convert.ToDouble(pccPrintNum)));
                int count = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(alRecipe.Count) / Convert.ToDouble(pccPerRowCount * pccPerPageCount)));

                if (!this.isInitial)
                {
                    this.Initial(this.fpPcc);
                }
                this.SetPccVisble(true);

                ArrayList alTmp = new ArrayList();
                for (int i = 0; i < count; i++)
                {
                    if (i == count - 1)
                    {
                        alTmp = alRecipe;
                    }
                    else
                    {
                        alTmp = alRecipe.GetRange(0, pccPerPageCount * pccPerRowCount);
                    }
                    //this.SetRecipe(alTmp);
                    this.SetPccRecipe(alTmp);
                    alTmp.Clear();
                    p.PrintPreview(this);
                }
                SetPccVisble(false);
                #endregion
            }
            else
            {
                int count = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(alRecipe.Count) / Convert.ToDouble(pPrintNum)));
                bool isSameNext = false;
                bool isSamePre = false;
                string combIDFirst = "";
                string combIDTmp = "";
                string combIDLast = "";
                for (int i = 0; i < count; i++)
                {
                    ArrayList alTmp = new ArrayList();
                    
                    if (i == count - 1)
                    {
                        combIDFirst = ((FS.HISFC.Models.Order.OutPatient.Order)alRecipe[0]).Combo.ID;
                        if (combIDFirst == combIDLast)
                        {
                            isSamePre = true;
                        }
                        else
                        {
                            isSamePre = false;
                        }
                        isSameNext = false;
                        alTmp = alRecipe;
                    }
                    else
                    {
                        combIDFirst = ((FS.HISFC.Models.Order.OutPatient.Order)alRecipe[0]).Combo.ID;
                        if (combIDFirst == combIDLast)
                        {
                            isSamePre = true;
                        }
                        else
                        {
                            isSamePre = false;
                        }
                        combIDLast = ((FS.HISFC.Models.Order.OutPatient.Order)alRecipe[pPrintNum - 1]).Combo.ID;
                        combIDTmp = ((FS.HISFC.Models.Order.OutPatient.Order)alRecipe[pPrintNum]).Combo.ID;
                        if (combIDLast == combIDTmp)
                        {
                            isSameNext = true;
                        }
                        else
                        {
                            isSameNext = false;
                        }
                        alTmp = alRecipe.GetRange(0, pPrintNum);
                        
                    }
                    this.SetRecipe(alTmp, isSamePre, isSameNext);
                    alTmp.Clear();
                    p.PrintPreview(this);
                }
            }
        }

        #region addby xuewj 2010-3-31 {67B867B1-96BD-454a-9BE0-E4DD6EB3E995} �в�ҩҽ����ӡ����

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="fp">����ʼ����FP</param>
        private void Initial(FS.FrameWork.WinForms.Controls.NeuSpread fp)
        {
            if(fp==null||fp.Sheets.Count==0)
            {
                return;
            }
            view = fp.Sheets[0];
            //�Ƴ�ԭ�е�Farpoint��
            view.RemoveColumns(0, view.ColumnCount);

            //ÿ����ʾ��ҩƷ��=pccPerRowCount
            view.AddColumns(0, pccPerRowCount * 11);

            for (int i = 0; i < pccPerRowCount; i++)
            {
                view.Columns[i * 11+(int)Columns.drugName].Width = 100F;//����
                view.Columns[i * 11 + (int)Columns.specs].Width = 55F;//���
                view.Columns[i * 11 + (int)Columns.comboNO].Visible = false;//��Ϻ�
                view.Columns[i * 11 + (int)Columns.hearbalQty].Width = 18F;//��
                view.Columns[i * 11 + (int)Columns.comboFlag].Width = 18F;//��
                view.Columns[i * 11 + (int)Columns.doseOnce].Width = 41F;//����
                view.Columns[i * 11 + (int)Columns.usage].Width = 68F;//�÷�
                view.Columns[i * 11 + (int)Columns.frequence].Width = 70F;//Ƶ��
                view.Columns[i * 11 + (int)Columns.totQty].Width = 52F;//����
                view.Columns[i * 11 + (int)Columns.injectCount].Width = 37F;//Ժע
                view.Columns[i * 11 + (int)Columns.memo].Width = 38F;//��ע

                view.Columns[i * 11 + (int)Columns.drugName].Label = "����";
                view.Columns[i * 11 + (int)Columns.hearbalQty].Visible = false;
                view.Columns[i * 11 + (int)Columns.doseOnce].Label = "����";
                view.Columns[i * 11 + (int)Columns.usage].Visible = false;
                view.Columns[i * 11 + (int)Columns.specs].Visible = false;
                view.Columns[i * 11 + (int)Columns.comboFlag].Visible = false;
                view.Columns[i * 11 + (int)Columns.usage].Visible = false;
                view.Columns[i * 11 + (int)Columns.frequence].Visible = false;
                view.Columns[i * 11 + (int)Columns.totQty].Visible = false;
                view.Columns[i * 11 + (int)Columns.injectCount].Visible = false;
                view.Columns[i * 11 + (int)Columns.memo].Visible = false;
            }

            view.RowHeader.Visible = false;//����ʾ��ͷ
            view.DefaultStyle.Locked = true;//�������������޸�
            fp.Location = this.fpSpread1.Location;
            fp.Size = this.fpSpread1.Size;
            this.isInitial = true;
        }

        /// <summary>
        /// �ؼ���ʾ
        /// </summary>
        private void SetPccVisble(bool isShow)
        {
            this.fpPcc.Visible = isShow;
            this.fpSpread1.Visible = !isShow;

            this.pnlPcc.Visible = isShow;

            foreach (System.Windows.Forms.Control c in this.pnlBottom.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuLabel)
                {
                    c.Visible = !isShow;
                }
                else
                {
                    c.Visible = isShow;
                }
            }
        }

        /// <summary>
        /// �����в�ҩ����
        /// </summary>
        /// <param name="alTmp"></param>
        private void SetPccRecipe(ArrayList alTmp)
        {
            view.RemoveRows(0, view.RowCount);
            view.AddRows(0, alTmp.Count);

            FS.HISFC.Models.Order.OutPatient.Order orderInfo = null;
            decimal money=0m;
            for (int row = 0, count = 0; count< alTmp.Count; row++, count+=pccPerRowCount)
            {
                for (int perCol = 0; perCol < pccPerRowCount; perCol++)
                {
                    if (row * pccPerRowCount + perCol > alTmp.Count - 1)
                    {
                        break;
                    }
                    orderInfo = new FS.HISFC.Models.Order.OutPatient.Order();
                    orderInfo = alTmp[row * pccPerRowCount + perCol] as FS.HISFC.Models.Order.OutPatient.Order;

                    //����ӡNULL���Ѿ��˷ѵ�
                    if (orderInfo == null || orderInfo.Qty == 0)
                    {
                        continue;
                    }


                    this.view.Cells[row, perCol * 11 + (int)Columns.drugName].Text = orderInfo.Item.Name;
                    this.view.Cells[row, perCol * 11 + (int)Columns.specs].Text = orderInfo.Item.Specs;
                    this.view.Cells[row, perCol * 11 + (int)Columns.comboNO].Text = orderInfo.Combo.ID;
                    this.view.Cells[row, perCol * 11 + (int)Columns.hearbalQty].Text = orderInfo.HerbalQty.ToString();
                    this.view.Cells[row, perCol * 11 + (int)Columns.doseOnce].Text = orderInfo.DoseOnce.ToString() + orderInfo.DoseUnit;
                    this.view.Cells[row, perCol * 11 + (int)Columns.frequence].Text = orderInfo.Frequency.Name;
                    this.view.Cells[row, perCol * 11 + (int)Columns.usage].Text = orderInfo.Usage.Name;
                    FS.HISFC.Models.Pharmacy.Item phaItem = phaManagement.GetItem(orderInfo.Item.ID);
                    if (orderInfo.MinunitFlag == "1")
                    {
                        this.view.Cells[row, perCol * 11 + (int)Columns.totQty].Text = orderInfo.Qty.ToString() + orderInfo.Unit;
                    }
                    else
                    {
                        this.view.Cells[row, perCol * 11 + (int)Columns.totQty].Text = Convert.ToString(orderInfo.Qty * orderInfo.Item.PackQty) + phaItem.MinUnit;
                    }

                    this.view.Cells[row, perCol * 11 + (int)Columns.injectCount].Text = orderInfo.InjectCount.ToString();
                    this.view.Cells[row, perCol * 11 + (int)Columns.memo].Text = orderInfo.Memo;

                    money+=orderInfo.FT.OwnCost;

                }
            }

            if (orderInfo != null)
            {
                this.lblUsage.Text = orderInfo.Usage.Name;
                this.lblFrequency.Text = orderInfo.Frequency.Name;
                this.lblTotQty.Text = orderInfo.HerbalQty.ToString();
                this.lblIsProxy.Text = orderInfo.Memo;
                this.lblSumMoney.Text = money.ToString();
                this.lblRecipeNO.Text = orderInfo.ReciptNO;
            }
        }

        #endregion

        #endregion

        #region IRecipePrint ��Ա

        /// <summary>
        /// ��ӡ������
        /// </summary>
        private string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PrintRecipe()
        { 
            this.QueryOrder();
            return 1;
        }

        public int PrintRecipeView(System.Collections.ArrayList alRecipe)
        {
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        public int SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myRegister = register;
            this.SetPatient();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public string RecipeNO
        {
            get
            {
                return this.myRecipeNO;
            }
            set
            {
                this.myRecipeNO = value;
            }

        }

        #endregion

        #region IDrugPrint ��Ա

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            if (al != null && al.Count > 0)
            {
                this.PatientInfo = reg.GetByClinic(drugRecipe.ClinicNO);//��ȡ���߻�����Ϣ
                this.SetRecipeByApplyOut(al);
                this.SetPatientInfo(this.PatientInfo);
            }
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddAllData(ArrayList al)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddCombo(ArrayList alCombo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        decimal FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.DrugTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        FS.HISFC.Models.RADT.PatientInfo FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.InpatientInfo
        {
            get
            {
                return this.patientinfo;
            }
            set
            {
                this.patientinfo = value;
            }
        }

        decimal FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.LabelTotNum
        {
            set { this.Tag = value; }
        }

        FS.HISFC.Models.Registration.Register FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.OutpatientInfo
        {
            get
            {
               return this.register ;
            }
            set
            {
                this.register = value;
            }
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.Preview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint.Print()
        {
            this.lblTitle.Text = managerIntegrate.GetHospitalName() + "�� �� ��";

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.IsDataAutoExtend = true;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.PrintPreview(this);
        }

        #endregion
    }
}

