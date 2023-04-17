using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// <br></br>
    /// [��������: ��ҩ��ϸ��ʾ�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// </summary>
    public partial class ucDrugDetail : ucDrugBase, FS.HISFC.BizProcess.Interface.Pharmacy.IInpatientDrug
    {
        public ucDrugDetail()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                try
                {
                    this.Init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʼ��ʧ��! " + ex.Message);
                }
            }
        }

        #region �����

        /// <summary>
        /// ����ҽ�����ͱ������ҽ����������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper orderTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���һ���ʱҩƷ�����б�
        /// </summary>
        private ArrayList deptDrugInfo = new ArrayList();

        /// <summary>
        /// ���һ���ʱҩƷ��������
        /// </summary>
        private ArrayList deptDrugNum = new ArrayList();

        /// <summary>
        /// ������ʾ������
        /// </summary>		
        private ArrayList alApplyOutInfo = new ArrayList();

        /// <summary>
        /// �洢���δ�����
        /// </summary>
        private ArrayList alBillCode = new ArrayList();

        /// <summary>
        /// �Ƿ���յ��ݺ�(������)��ʾ
        /// </summary>
        private bool isBillCodeClear = true;

        /// <summary>
        /// ��ҩ֪ͨ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugBillClass myDrugBillClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();

        /// <summary>
        /// ȫѡ��ȫ��ѡʱ�Ƿ�ֻ����ѡ�����
        /// </summary>
        private bool isCheckSelect = false;

        /// <summary>
        /// ���ڰ�ҩ��ϸ��ʾʱ �Ƿ���ʾѡ����
        /// </summary>
        private bool isShowCheckColumn = true;

        /// <summary>
        /// ��ҩ�����ͼ���
        /// </summary>
        private System.Collections.Hashtable hsDrugBillClass = new Hashtable();

        /// <summary>
        /// �Ƿ������˰�ҩ����ʾ�ؼ�
        /// </summary>
        private bool isAddDrugBillControl = false;

        /// <summary>
        /// ���߰�����
        /// </summary>
        private System.Collections.Hashtable hsPerson = new Hashtable();

        /// <summary>
        /// �Ƿ�ʹ��ѡ����һ��ܷ�ҩ {C5E8F7F5-2759-4c5e-B519-86FD151AFCD7}
        /// </summary>
        private bool isUseCollect = true;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾҪ��ӡ�İ�ҩ��
        /// </summary>
        [Description("�Ƿ�Ԥ����ʾҪ��ӡ�İ�ҩ��"), Category("����"), DefaultValue(true)]
        public bool IsShowBillPreview
        {
            get
            {
                return this.tbDrugDetail.TabPages.Contains(this.tpDrugBill);
            }
            set
            {
                if (value)
                {
                    if (!this.tbDrugDetail.TabPages.Contains(this.tpDrugBill))
                    {
                        this.tbDrugDetail.TabPages.Insert(0, this.tpDrugBill);

                        this.tbDrugDetail.SelectTab(this.tpDrugBill);
                    }
                }
                if (!value)
                {
                    if (this.tbDrugDetail.TabPages.Contains(this.tpDrugBill))
                    {
                        this.tbDrugDetail.TabPages.Remove(this.tpDrugBill);
                    }
                }
            }
        }

        /// <summary>
        /// �Ƿ���ʾҪȡ/��ҩ����
        /// </summary>
        [Description("�Ƿ���ʾ�������ڻ��߷�ҩ������Ϣ"), Category("����"), DefaultValue(true)]
        public bool IsShowPatientTot
        {
            get
            {
                return this.tbDrugDetail.TabPages.Contains(this.tpPatientMerge);
            }
            set
            {
                if (value)
                {
                    if (!this.tbDrugDetail.TabPages.Contains(this.tpPatientMerge))
                    {
                        this.tbDrugDetail.TabPages.Add(tpPatientMerge);
                    }
                }
                else
                {
                    if (this.tbDrugDetail.TabPages.Contains(this.tpPatientMerge))
                    {
                        this.tbDrugDetail.TabPages.Remove(tpPatientMerge);
                    }
                }
            }
        }

        /// <summary>
        /// �Ƿ���ʾ���һ���
        /// </summary>
        [Description("�Ƿ���ʾ�����ݵĿ��һ��ܷ�ҩ��Ϣ"), Category("����"), DefaultValue(true)]
        public bool IsShowDeptTot
        {
            get
            {
                return this.tbDrugDetail.TabPages.Contains(this.tpDeptMerge);
            }
            set
            {
                if (value)
                {
                    if (!this.tbDrugDetail.TabPages.Contains(this.tpDeptMerge))
                    {
                        this.tbDrugDetail.TabPages.Add(tpDeptMerge);
                    }
                }
                else
                {
                    if (this.tbDrugDetail.TabPages.Contains(this.tpDeptMerge))
                    {
                        this.tbDrugDetail.TabPages.Remove(tpDeptMerge);
                    }
                }
            }
        }

        /// <summary>
        /// �Ƿ����ͨ�������ҩ���Զ������ҩ��¼
        /// </summary>
        [Description("�Ƿ���������ҩ�����Զ�ѡ����ҩ��¼"), Category("����"), DefaultValue(true)]
        public bool IsAutoCheck
        {
            get
            {
                return this.neuSpread3_DeptDetailSheet.Columns.Get(4).Visible;
            }
            set
            {
                this.neuSpread3_DeptDetailSheet.Columns[4].Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����panel  �����Ź���
        /// </summary>
        [Description("�Ƿ�������ʾ��ҩ���Ź��˿�"), Category("����"), DefaultValue(false)]
        public bool IsFilterBillCode
        {
            get
            {
                return this.panelTop.Visible;
            }
            set
            {
                this.panelTop.Visible = value;
            }
        }

        /// <summary>
        /// ���ڰ�ҩ��ϸ��ʾʱ �Ƿ���ʾѡ����
        /// </summary>
        [Description("���ڰ�ҩ��ϸ��ʾʱ �Ƿ���ʾѡ����"), Category("����"), DefaultValue(false)]
        public bool IsShowCheckColumn
        {
            get
            {
                return this.isShowCheckColumn;
            }
            set
            {
                this.neuSpread1_DetailSheet.Columns[2].Visible = value;

                this.isShowCheckColumn = value;
            }
        }

        /// <summary>
        /// ȫѡ��ȫ��ѡʱ�Ƿ�ֻ����ѡ�����
        /// </summary>
        [Description("ȫѡ��ȫ��ѡʱ�Ƿ�ֻ����ѡ�����"), Category("����"), DefaultValue(false)]
        public bool CheckSelect
        {
            get
            {
                return this.isCheckSelect;
            }
            set
            {
                this.isCheckSelect = value;
            }
        }

        /// <summary>
        /// �Ƿ�������н��ж�ѡ ���ڶ�ѡ״̬ʱ���ܶ��н���ѡ��/��ѡ�в���
        /// </summary>
        [Description("�Ƿ�������н��ж�ѡ ���ڶ�ѡ״̬ʱ���ܶ��н���ѡ��/��ѡ�в���"), Category("����"), DefaultValue(false)]
        public bool AllowMultiSelect
        {
            get
            {
                if (this.neuSpread1_DetailSheet.OperationMode == FarPoint.Win.Spread.OperationMode.RowMode)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    this.neuSpread1_DetailSheet.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
                else
                    this.neuSpread1_DetailSheet.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            }
        }

        /// <summary>
        ///  �Ƿ�ʹ��ѡ����һ��ܷ�ҩ {C5E8F7F5-2759-4c5e-B519-86FD151AFCD7}
        /// </summary>
        [Description("�Ƿ�ʹ��ѡ����һ��ܷ�ҩ"), Category("����"), DefaultValue(true)]
        public bool IsUseCollect
        {
            get
            {
                return this.isUseCollect;
            }
            set
            {
                this.isUseCollect = value;
            }
        }

        #endregion

        #region ��ҩ���ؼ�

        /// <summary>
        /// ��Ӱ�ҩ����ӡ��ʾ�ؼ�
        /// </summary>
        /// <param name="ucBill">�ؼ�����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public virtual int AddDrugBill(object ucBill, bool isAddToContainer)
        {
            if (isAddToContainer)
            {
                Function.ucDrugBill = ucBill as FS.FrameWork.WinForms.Controls.ucBaseControl;
                if (Function.ucDrugBill == null)
                {
                    MessageBox.Show("��ҩ���ؼ����ô��� ��̳л���ucBaseControl");
                    return -1;
                }

                Function.ucDrugBill.Dock = DockStyle.Fill;
                this.tpDrugBill.Controls.Clear();
                this.tpDrugBill.Controls.Add(Function.ucDrugBill);
            }

            Function.IDrugPrint = ucBill as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;
            if (Function.IDrugPrint == null)
            {
                MessageBox.Show("��ҩ���ؼ�ʵ�ִ��� ��ʵ�ֻ���IDrugPrint�ӿ�");
                return -1;
            }

            this.isAddDrugBillControl = isAddToContainer;

            return 1;
        }

        public virtual int AddDrugBill(object ucBill)
        {
            return AddDrugBill(ucBill, true);
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            this.InitControlParam();

            //ȡҽ�����ͣ����ڽ�����ת��������
            FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();
            this.orderTypeHelper.ArrayObject = orderManager.GetList();

            //�ϲ��ظ�ֵ����
            this.neuSpread1_DetailSheet.SetColumnMerge(0, FarPoint.Win.Spread.Model.MergePolicy.Always);
            this.neuSpread1_DetailSheet.SetColumnMerge(1, FarPoint.Win.Spread.Model.MergePolicy.Always);

            this.neuSpread2_PatientDetailSheet.SetColumnMerge(0, FarPoint.Win.Spread.Model.MergePolicy.Always);
            this.neuSpread2_PatientDetailSheet.SetColumnMerge(1, FarPoint.Win.Spread.Model.MergePolicy.Always);


            this.neuSpread1_DetailSheet.SetColumnAllowAutoSort(-1, false);

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            numCellType.DecimalPlaces = 0;
            this.neuSpread3_DeptDetailSheet.Columns[4].CellType = numCellType;

            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
            ArrayList alDrugBillClass = drugStoreManager.QueryDrugBillClassList();
            if (alDrugBillClass == null)
            {
                MessageBox.Show(Language.Msg(""));
                return;
            }

            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass info in alDrugBillClass)
            {
                this.hsDrugBillClass.Add(info.ID, info.PrintType);
            }            
        }

        /// <summary>
        /// ��ʼ�����Ʋ���
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            //�������ϲ���������Ϣ
            //this.IsShowBillPreview = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Priview_Bill, true, true);

            this.IsShowPatientTot = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Show_PatientTot, true, false);
            this.IsShowDeptTot = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Show_DeptTot, true, true);

        }

        /// <summary>
        /// ���ڰ�ҩ��׼ʱ ��ʾ������������ 
        /// </summary>
        /// <param name="arrayApplyOut">������������</param>
        /// <param name="drugBillClass">��ҩ֪ͨ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public void ShowData(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            this.myDrugBillClass = drugBillClass;

            this.ShowData(arrayApplyOut);

            //��ʾ��ҩ��
            if (this.IsShowBillPreview && Function.IDrugPrint != null)
            {
                Function.IDrugPrint.AddAllData(this.GetCheckData(), this.myDrugBillClass);
            }
        }

        /// <summary>
        /// ���ݳ����������� ������ҩ��������ʾ
        /// </summary>
        /// <param name="applyOut">������������</param>
        private void SetBillCodeData(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            if (this.isBillCodeClear && this.IsFilterBillCode)
            {
                bool isNew = true;
                for (int i = 0; i < alBillCode.Count; i++)
                {
                    if (alBillCode[i] as string == applyOut.BillNO)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    alBillCode.Add(applyOut.BillNO);
                    this.cmbBillCode.Items.Add(applyOut.BillNO);
                }
            }
        }

        /// <summary>
        /// ��FpDetail�������� ��ʾ��ҩ��ϸ��Ϣ
        /// </summary>
        /// <param name="applyOut">����������Ϣ</param>
        /// <param name="i"></param>
        protected void AddDataToFpDetail(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, int iIndex)
        {
            if (applyOut.User01.Length > 4)
                applyOut.User01 = applyOut.User01.Substring(4);
            this.neuSpread1_DetailSheet.Cells[iIndex, 0].Value = applyOut.User01;  //����
            this.neuSpread1_DetailSheet.Cells[iIndex, 1].Value = applyOut.User02;  //����
            this.neuSpread1_DetailSheet.Cells[iIndex, 2].Value = true;
            this.neuSpread1_DetailSheet.Cells[iIndex, 3].Value = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";
            this.neuSpread1_DetailSheet.Cells[iIndex, 4].Value = applyOut.Item.PriceCollection.RetailPrice;
            this.neuSpread1_DetailSheet.Cells[iIndex, 5].Value = applyOut.DoseOnce;
            this.neuSpread1_DetailSheet.Cells[iIndex, 6].Value = applyOut.Item.DoseUnit;
            this.neuSpread1_DetailSheet.Cells[iIndex, 7].Value = applyOut.Operation.ApplyQty * applyOut.Days;
            this.neuSpread1_DetailSheet.Cells[iIndex, 8].Value = applyOut.Item.MinUnit;
            this.neuSpread1_DetailSheet.Cells[iIndex, 9].Value = applyOut.Frequency.ID;
            this.neuSpread1_DetailSheet.Cells[iIndex, 10].Value = applyOut.Usage.Name;
            this.neuSpread1_DetailSheet.Cells[iIndex, 11].Value = applyOut.Operation.ApplyOper.OperTime.ToString();

            if (this.hsPerson.ContainsKey( applyOut.Operation.ExamOper.ID ) == false)       //��������Ա����/������Ϣ ���»�ȡ
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee person = managerIntegrate.GetEmployeeInfo( applyOut.Operation.ExamOper.ID );
                if (person != null)
                {
                    applyOut.Operation.ExamOper.Name = person.Name;
                    this.hsPerson.Add( applyOut.Operation.ExamOper.ID, person.Name );
                }
            }
            else
            {
                applyOut.Operation.ExamOper.Name = this.hsPerson[applyOut.Operation.ExamOper.ID].ToString();
            }

            this.neuSpread1_DetailSheet.Cells[iIndex, 12].Value = applyOut.Operation.ExamOper.Name;

            if (applyOut.Operation.ExamOper.OperTime != System.DateTime.MinValue)
                this.neuSpread1_DetailSheet.Cells[iIndex, 13].Value = applyOut.Operation.ExamOper.OperTime.ToString();

            this.neuSpread1_DetailSheet.Cells[iIndex, 14].Value = this.orderTypeHelper.GetName(applyOut.OrderType.ID);
            this.neuSpread1_DetailSheet.Rows[iIndex].Tag = applyOut;
            //�����ҩ�������ϻ��߲���ҩ�����ú�ɫ������ʾ����
            if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid || applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                this.neuSpread1_DetailSheet.Rows[iIndex].ForeColor = System.Drawing.Color.Red;
            }
        }

        #region ��ҩ������ʾ

        /// <summary>
        /// �ϲ����ܼ��㻼��ҩƷ������ ���
        /// </summary>
        public virtual void MergePatientData()
        {
            this.neuSpread2_PatientDetailSheet.Rows.Count = 0;
            string bed_Name = "";		//���ߴ���+ ���� Ψһ
            string drugCode = "";		//����ҩƷ 
            decimal drugNum = 0;		//����ҩƷ���� 
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut;
                int iRow = 0;
                FS.HISFC.Models.Pharmacy.ApplyOut privApplyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

                for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
                {
                    if (this.neuSpread1_DetailSheet.Cells[i, 2].Value != null && this.neuSpread1_DetailSheet.Cells[i, 2].Value.ToString() == "True")
                    {
                        applyOut = this.neuSpread1_DetailSheet.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (applyOut.User01 + applyOut.User02 != bed_Name) //���� �����ڴ���+����
                        {
                            #region ��ͬ����

                            if (bed_Name != "")
                            {
                                this.neuSpread2_PatientDetailSheet.Rows.Add(iRow, 1);
                                this.AddDataToFpPatientMerge(iRow, privApplyOut, drugNum);
                                iRow = iRow + 1;
                                drugNum = 0;
                            }
                            drugNum = drugNum + applyOut.Operation.ApplyQty * applyOut.Days;
                            drugCode = applyOut.Item.ID;
                            bed_Name = applyOut.User01 + applyOut.User02;
                            privApplyOut = applyOut;
                            if (i == this.neuSpread1_DetailSheet.Rows.Count - 1)
                            {
                                this.neuSpread2_PatientDetailSheet.Rows.Add(iRow, 1);
                                this.AddDataToFpPatientMerge(iRow, applyOut, drugNum);
                                iRow = iRow + 1;
                            }

                            #endregion
                        }
                        else										//��ͬ���� 
                        {
                            if (applyOut.Item.ID == drugCode)			//��ͬҩƷ
                            {
                                #region ��ͬ������ͬҩƷ

                                drugNum = drugNum + applyOut.Operation.ApplyQty * applyOut.Days;	//����ҩƷ����
                                if (i == this.neuSpread1_DetailSheet.Rows.Count - 1)
                                {
                                    this.neuSpread2_PatientDetailSheet.Rows.Add(iRow, 1);
                                    this.AddDataToFpPatientMerge(iRow, applyOut, drugNum);
                                    iRow = iRow + 1;
                                }

                                #endregion
                            }
                            else									//��ͬҩƷ
                            {
                                #region ��ͬ���߲�ͬҩƷ

                                this.neuSpread2_PatientDetailSheet.Rows.Add(iRow, 1);
                                this.AddDataToFpPatientMerge(iRow, privApplyOut, drugNum);
                                iRow = iRow + 1;
                                drugNum = applyOut.Operation.ApplyQty * applyOut.Days;	//����ҩƷ����
                                privApplyOut = applyOut;
                                drugCode = applyOut.Item.ID;

                                #endregion
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// ��FpPatientMerge�ڼ������� ��ʾ���߰�ҩ������Ϣ
        /// </summary>
        /// <param name="iIndex">�����������</param>
        /// <param name="applyOut">��ҩ������Ϣ</param>
        /// <param name="drugTot">��������</param>
        protected void AddDataToFpPatientMerge(int iIndex, FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal drugTot)
        {
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 0].Value = applyOut.User01;  //����
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 1].Value = applyOut.User02;  //����
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 2].Value = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 3].Value = applyOut.Item.PriceCollection.RetailPrice;
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 4].Value = applyOut.DoseOnce;
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 5].Value = applyOut.Item.DoseUnit;
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 6].Value = drugTot;
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 7].Value = applyOut.Item.MinUnit;
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 8].Value = applyOut.Frequency.Name;
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 9].Value = applyOut.Usage.Name;
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 10].Value = applyOut.Operation.ApplyOper.OperTime.ToString();
            this.neuSpread2_PatientDetailSheet.Cells[iIndex, 11].Value = this.orderTypeHelper.GetName(applyOut.OrderType.ID);
            this.neuSpread2_PatientDetailSheet.Rows[iIndex].Tag = applyOut;
            //�����ҩ�������ϻ��߲���ҩ�����ú�ɫ������ʾ����
            if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid || applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
            {
                this.neuSpread2_PatientDetailSheet.Rows[iIndex].ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// �ϲ����ܼ������ҩƷ������ ���
        /// </summary>
        public virtual void MergeDeptData()
        {
            this.neuSpread3_DeptDetailSheet.Rows.Count = 0;
            this.deptDrugInfo = new ArrayList();
            this.deptDrugNum = new ArrayList();
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info;
                FS.HISFC.Models.Pharmacy.ApplyOut privInfo = new FS.HISFC.Models.Pharmacy.ApplyOut();

                for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
                {
                    //{C5E8F7F5-2759-4c5e-B519-86FD151AFCD7}
                    if (this.neuSpread1_DetailSheet.Cells[i, 2].Value != null && this.neuSpread1_DetailSheet.Cells[i, 2].Value.ToString() == "True" || this.isUseCollect)
                    {
                        info = this.neuSpread1_DetailSheet.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (info == null) continue;
                        bool isFind = false;
                        for (int j = 0; j < this.deptDrugInfo.Count; j++)
                        {
                            FS.HISFC.Models.Pharmacy.ApplyOut temp = this.deptDrugInfo[j] as FS.HISFC.Models.Pharmacy.ApplyOut;
                            if (temp.Item.ID == info.Item.ID)
                            {
                                this.deptDrugNum[j] = (decimal)this.deptDrugNum[j] + info.Operation.ApplyQty * info.Days;
                                isFind = true;
                                break;
                            }
                        }
                        if (!isFind)
                        {
                            this.deptDrugInfo.Add(info);
                            this.deptDrugNum.Add(info.Operation.ApplyQty * info.Days);
                        }

                    }
                }
                for (int i = 0; i < this.deptDrugInfo.Count; i++)
                {
                    this.neuSpread3_DeptDetailSheet.Rows.Add(i, 1);
                    this.AddTotDataToFpDeptMerge(i, this.deptDrugInfo[i] as FS.HISFC.Models.Pharmacy.ApplyOut, (decimal)this.deptDrugNum[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// ��FpPatientMerge�ڼ������� ��ʾ���Ұ�ҩ������Ϣ
        /// </summary>
        /// <param name="iIndex">�����������</param>
        /// <param name="applyOut">��ҩ������Ϣ</param>
        /// <param name="drugTot">��������</param>
        protected void AddTotDataToFpDeptMerge(int iIndex, FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal drugNum)
        {
            //{C5E8F7F5-2759-4c5e-B519-86FD151AFCD7}
            if (this.isUseCollect)
            {
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 0].Value = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 1].Value = applyOut.Item.PriceCollection.RetailPrice;
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 2].Value = drugNum;
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 3].Value = applyOut.Item.MinUnit;
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 5].Value = true;
                this.neuSpread3_DeptDetailSheet.Rows[iIndex].Tag = applyOut;
            }
            else
            {
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 0].Value = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 1].Value = applyOut.Item.PriceCollection.RetailPrice;
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 2].Value = drugNum;
                this.neuSpread3_DeptDetailSheet.Cells[iIndex, 3].Value = applyOut.Item.MinUnit;
                this.neuSpread3_DeptDetailSheet.Rows[iIndex].Tag = applyOut;
            }
        }

        #endregion

        /// <summary>
        /// ����������İ�ҩ���Զ�ѡ�����ҩ����
        /// </summary>
        /// <returns>����ɹ����� Ture ʧ�ܷ��� False</returns>
        public bool AutoSetCheck()
        {

            #region �ж��û������Ƿ���ȷ

            this.neuSpread1_DetailSheet.SortRows(7, false, false);

            for (int i = 0; i < this.neuSpread3_DeptDetailSheet.Rows.Count; i++)
            {
                //�������İ�ҩ�������������������ᵯ��������ʾ��
                //�жϵİ�ҩ����ʵ�ʹ�ѡ��֮��Ĳ��������ʾ��ͬ
                //if ((decimal)this.deptDrugNum[i] < NConvert.ToDecimal(this.neuSpread3_DeptDetailSheet.Cells[i, 4].Text))
                //{
                //    MessageBox.Show("��" + (i + 1).ToString() + "�� ҩƷ ��ҩ��ӦС�ڵ���������");
                //    return false;
                //}
                this.deptDrugNum[i] = NConvert.ToDecimal(this.neuSpread3_DeptDetailSheet.Cells[i, 4].Text);
            }

            #endregion

            this.CheckNone();

            for (int i = 0; i < this.deptDrugInfo.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info = this.deptDrugInfo[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                int privI = 0;
                for (int j = 0; j < this.neuSpread1_DetailSheet.Rows.Count; j++)
                {
                    #region ������ҩ��ϸ
                    FS.HISFC.Models.Pharmacy.ApplyOut temp = this.neuSpread1_DetailSheet.Rows[j].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if ((decimal)this.deptDrugNum[i] == 0)
                        break;
                    if (temp.Item.ID == info.Item.ID)
                    {
                        if ((decimal)this.deptDrugNum[i] >= temp.Operation.ApplyQty * temp.Days)
                        {
                            this.deptDrugNum[i] = (decimal)this.deptDrugNum[i] - temp.Operation.ApplyQty * temp.Days;
                            this.neuSpread1_DetailSheet.Cells[j, 2].Value = true;
                            privI = j;

                            if (j == this.neuSpread1_DetailSheet.Rows.Count - 1) break;

                            FS.HISFC.Models.Pharmacy.ApplyOut tempObj = this.neuSpread1_DetailSheet.Rows[j + 1].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                            if ((decimal)this.deptDrugNum[i] == 0) break;
                            if ((decimal)this.deptDrugNum[i] < tempObj.Operation.ApplyQty * tempObj.Days)
                            {
                                bool isFind = false;
                                for (int k = j + 1; k < this.neuSpread1_DetailSheet.Rows.Count; k++)
                                {
                                    FS.HISFC.Models.Pharmacy.ApplyOut obj = this.neuSpread1_DetailSheet.Rows[k].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                                    if (info.Item.ID != obj.Item.ID)
                                        continue;
                                    if ((decimal)this.deptDrugNum[i] == obj.Operation.ApplyQty * obj.Days)
                                    {
                                        this.neuSpread1_DetailSheet.Cells[k, 2].Value = true;
                                        this.deptDrugNum[i] = (decimal)this.deptDrugNum[i] - obj.Operation.ApplyQty * obj.Days;
                                        isFind = true;
                                        break;
                                    }
                                }
                                if (!isFind)
                                {
                                    this.neuSpread1_DetailSheet.Cells[j, 2].Value = false;
                                    this.deptDrugNum[i] = (decimal)this.deptDrugNum[i] + temp.Operation.ApplyQty * temp.Days;
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            for (int i = 0; i < this.deptDrugNum.Count; i++)
            {
                if ((decimal)this.deptDrugNum[i] > 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��ȡ��ǰ�û�ѡ�е�����
        /// </summary>
        internal ArrayList GetCheckData()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_DetailSheet.Cells[i, 2].Value))
                    al.Add(this.neuSpread1_DetailSheet.Rows[i].Tag);
            }

            return al;
        }

        #region IInpatientDrug ��Ա

        /// <summary>
        /// ����ǰ
        /// </summary>
        public event EventHandler BeginSaveEvent;

        /// <summary>
        /// �����
        /// </summary>
        public event EventHandler EndSaveEvent;

        /// <summary>
        /// ��ձ���е�����
        /// </summary>
        public void Clear()
        {
            //��պ�׼����
            this.neuSpread1_DetailSheet.Rows.Count = 0;
            try
            {
                //��հ�ҩ����ʾ
                Function.IDrugPrint.AddAllData(new ArrayList());

                if (this.tbDrugDetail.TabPages.Contains(this.tpPatientMerge))
                {
                    if (this.tbDrugDetail.SelectedTab == this.tpPatientMerge)
                        this.tbDrugDetail.SelectedIndex = 0;
                }
                if (this.tbDrugDetail.TabPages.Contains(this.tpDeptMerge))
                {
                    if (this.tbDrugDetail.SelectedTab == this.tpDeptMerge)
                        this.tbDrugDetail.SelectedIndex = 0;
                }
            }
            catch { }
        }

        /// <summary>
        /// ѡ��ȫ������
        /// </summary>
        public void CheckAll()
        {
            for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
            {
                if (this.isCheckSelect && !this.neuSpread1_DetailSheet.IsSelected(i, 0))
                    continue;
                this.neuSpread1_DetailSheet.Cells[i, 2].Value = true;
            }
        }

        /// <summary>
        /// ��ѡ���κ�����
        /// </summary>
        public void CheckNone()
        {
            for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
            {
                if (this.isCheckSelect && !this.neuSpread1_DetailSheet.IsSelected(i, 0))
                    continue;
                this.neuSpread1_DetailSheet.Cells[i, 2].Value = false;
            }
        }

        /// <summary>
        /// ��ʾ������������
        /// </summary>
        /// <param name="arrayApplyOut">������������</param>
        /// <returns>�ɹ�����1 �������󷵻�-1</returns>
        public void ShowData(ArrayList arrayApplyOut)
        {
            this.Clear();

            if (this.isBillCodeClear)
            {
                this.alBillCode = new ArrayList();
                this.cmbBillCode.Items.Clear();
                this.cmbBillCode.Text = "";
            }
            if (arrayApplyOut.Count == 0)
            {
                return;
            }

            arrayApplyOut.Sort(new CompareApplyOut());

            FS.HISFC.Models.Pharmacy.ApplyOut applyOut;
            this.neuSpread1_DetailSheet.Rows.Add(0, arrayApplyOut.Count);
            //���汾����ʾ����
            this.alApplyOutInfo = arrayApplyOut;

            for (int i = 0; i < arrayApplyOut.Count; i++)
            {
                applyOut = arrayApplyOut[i] as FS.HISFC.Models.Pharmacy.ApplyOut;

                this.AddDataToFpDetail(applyOut, i);

                this.SetBillCodeData(applyOut);
            }
        }

        /// <summary>
        /// ���ݴ���İ�ҩ֪ͨ������ҩ����
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨ</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public virtual int Save(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            if (drugMessage == null || drugMessage.ApplyDept.ID == "" || this.neuSpread1_DetailSheet.Rows.Count <= 0)
                return -1;

            if (this.BeginSaveEvent != null)
                this.BeginSaveEvent(drugMessage, null);

            #region ��������ҩƷ�Զ����д���ҩ����

            if (this.IsAutoCheck && this.tbDrugDetail.SelectedTab == this.tpDeptMerge)
            {
                if (!this.AutoSetCheck())
                {
                    MessageBox.Show(Language.Msg("������������İ�ҩ����δ����ȷ��������Щ�����¼ \n �����ʣ�����������ֹ�����"));
                    return -1;
                }
            }

            #endregion

            #region ���û�ѡ������ݽ��а�ҩ����

            ArrayList al = this.GetCheckData();

            if (al.Count == 0)
            {
                MessageBox.Show(Language.Msg("��ѡ�����׼��ҩ������"));
                return -1;
            }

            if (drugMessage.DrugBillClass.ID == "R")
            {
                if (DrugStore.Function.DrugReturnConfirm(al, drugMessage, this.ArkDept, this.ApproveDept) == -1)
                    return -1;
            }
            else
            {
                if (DrugStore.Function.DrugConfirm(al, drugMessage, this.ArkDept, this.ApproveDept) == -1)
                    return -1;
            }

            #endregion

            //�����ҩ����
            this.myDrugBillClass.DrugBillNO = drugMessage.DrugBillClass.Memo;

            if (this.hsDrugBillClass.ContainsKey(drugMessage.DrugBillClass.ID))
            {
                this.myDrugBillClass.PrintType = this.hsDrugBillClass[drugMessage.DrugBillClass.ID] as FS.HISFC.Models.Pharmacy.BillPrintType;
                this.myDrugBillClass.Name = drugMessage.DrugBillClass.Name;
            }

            Function.Print(al, this.myDrugBillClass, this.IsAutoPrint, this.IsPrintLabel, this.IsNeedPreview);

            if (this.EndSaveEvent != null)
                this.EndSaveEvent(drugMessage, null);

            return 1;
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        public void Preview()
        {
            this.Print();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            if (this.isAddDrugBillControl)      //��������˰�ҩ��Ԥ��
            {
                if (this.tbDrugDetail.SelectedTab != this.tpDrugBill)
                {
                    FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

                    p.PrintPage(30, 30, this);
                    return;
                }
            }

            if (this.hsDrugBillClass.ContainsKey(this.myDrugBillClass.ID))
            {
                this.myDrugBillClass.PrintType = this.hsDrugBillClass[this.myDrugBillClass.ID] as FS.HISFC.Models.Pharmacy.BillPrintType;
            }

            //����� ����ҩ��ӡ����
            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClassTemp = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            if (this.myDrugBillClass.PrintType.ID.ToString() == "T" && this.tbDrugDetail.SelectedTab == this.tpDetail)
            {
                drugBillClassTemp = this.myDrugBillClass.Clone();
                drugBillClassTemp.PrintType.ID = "D";
            }
            else
            {
                drugBillClassTemp = this.myDrugBillClass.Clone();
            }

            if (this.isAddDrugBillControl && Function.IDrugPrint != null)
            {
                Function.IDrugPrint.Print();
            }
            else
            {
                if (this.IsPrintLabel)
                    Function.PrintLabelForOutpatient(this.GetCheckData());
                else
                    Function.PrintBill(this.GetCheckData(), drugBillClassTemp);
            }
        }

        #endregion

        private void cmbBillCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuSpread1_DetailSheet.Rows.Count < this.alApplyOutInfo.Count)
            {
                this.isBillCodeClear = false;
                this.ShowData(this.alApplyOutInfo);
                this.isBillCodeClear = true;
            }

            if (this.cmbBillCode.Text == "")
                return;

            for (int i = this.neuSpread1_DetailSheet.Rows.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.neuSpread1_DetailSheet.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (applyOut.BillNO != this.cmbBillCode.Text)
                {
                    this.neuSpread1_DetailSheet.Rows[i].Remove();
                }
            }
        }

        private void tbDrugDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tbDrugDetail.SelectedTab == this.tpPatientMerge)
            {
                this.MergePatientData();
            }
            if (this.tbDrugDetail.SelectedTab == this.tpDeptMerge)
            {
                this.IsAutoCheck = false;

                this.MergeDeptData();
            }
        }

        //{C5E8F7F5-2759-4c5e-B519-86FD151AFCD7}
        private void neuSpread3_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Row < 0)
            {
                return;
            }
            if (e.Column == 5)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.neuSpread3_DeptDetailSheet.Rows[e.Row].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyTemp = this.neuSpread1_DetailSheet.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (applyTemp.Item.ID == applyOut.Item.ID)
                    {
                        if (neuSpread3_DeptDetailSheet.Cells[e.Row, 5].Text.ToUpper() == "TRUE")
                        {
                            neuSpread1_DetailSheet.Cells[i, 2].Text = "True";
                        }
                        else
                        {
                            neuSpread1_DetailSheet.Cells[i, 2].Text = "False";
                        }
                    }

                }
            }
        }

        #region Fp�����˵�

        System.Windows.Forms.ContextMenu fpContextMenu = new ContextMenu();

        private void neuSpread1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.fpContextMenu.MenuItems != null)
                this.fpContextMenu.MenuItems.Clear();
        }

        private void neuSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.fpContextMenu.MenuItems != null)
                    this.fpContextMenu.MenuItems.Clear();

                MenuItem menuSelect = new MenuItem("ѡ  ��");
                menuSelect.Click += new EventHandler(menuSelect_Click);

                MenuItem menuCancelSelect = new MenuItem("ȡ��ѡ��");
                menuCancelSelect.Click += new EventHandler(menuCancelSelect_Click);

                MenuItem menuReverseSelect = new MenuItem("����ѡ��");
                menuReverseSelect.Click += new EventHandler(menuReverseSelect_Click);

                this.fpContextMenu.MenuItems.Add(menuReverseSelect);
                if (this.AllowMultiSelect)
                {
                    this.fpContextMenu.MenuItems.Add(menuCancelSelect);
                    this.fpContextMenu.MenuItems.Add(menuSelect);
                }

                this.fpContextMenu.Show(this.neuSpread1, new Point(e.X, e.Y));
            }
        }

        void menuCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
            {
                if (this.neuSpread1_DetailSheet.IsSelected(i, 0))
                    this.neuSpread1_DetailSheet.Cells[i, 2].Value = false;
            }
        }

        void menuSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
            {
                if (this.neuSpread1_DetailSheet.IsSelected(i, 0))
                    this.neuSpread1_DetailSheet.Cells[i, 2].Value = true;
            }
        }

        void menuReverseSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_DetailSheet.Rows.Count; i++)
            {
                this.neuSpread1_DetailSheet.Cells[i, 2].Value = !(bool)this.neuSpread1_DetailSheet.Cells[i, 2].Value;
            }
        }

        /// <summary>
        /// �ؼ���ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            #region �����ȡ��ҩ����ӡdll

            try
            {
                //object obj = FS.FrameWork.WinForms.Classes.Function.CreateControl( "TestInterface" , "TestInterface.ucDrugBillPrint" );
                //this.AddDrugBill( obj );
            }
            catch (System.TypeLoadException ex)
            {
                MessageBox.Show("��ҩ����ָ�������ռ���Ч\n" + ex.Message);
                return;
            }

            #endregion

            base.OnLoad(e);
        }
        #endregion

        #region  ����

        protected class CompareApplyOut : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = null;
                string oY = null;
                if (o1.User01.Length > 4)
                {
                    oX = o1.User01.Substring(4);
                }
                else
                {
                    oX = o1.User01;
                }
                if (o2.User01.Length > 4)
                {
                    oY = o2.User01.Substring(4);
                }
                else
                {
                    oY = o2.User01;
                }
                oX = oX.PadLeft(5, '0');
                oY = oY.PadLeft(5, '0');

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
        }

        #endregion
        
    }
}
