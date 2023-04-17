using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.InBase
{
    /// <summary>
    /// [�ؼ�����:ucDrugSplitProperty]<br></br>
    /// [��������: סԺҩƷ�������ά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-15]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDrugSplitProperty : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugSplitProperty()
        {
            InitializeComponent();
            //���б�����ѡ����
            this.ucDrugList1.ChooseDataEvent += new Common.Controls.ucDrugList.ChooseDataHandler(ucDrugList1_ChooseDataEvent);
        }

        #region ����

        /// <summary>
        /// �����б�
        /// </summary>
        private ArrayList alDept = null;

        /// <summary>
        /// �����б�
        /// </summary>
        private ArrayList alDosageMode = null;

        /// <summary>
        /// �������
        /// </summary>
        private ArrayList drugSplitPropertyList = new ArrayList();// { "0���ɲ��" , "1�ɲ�ֲ�ȡ��" , "2�ɲ����ȡ��" , "3���ɲ�ֵ���ȡ��","4�ɲ�ְ�����ȡ��","5�ɲ�ְ�����ȡ��" };

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item drugManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ǰ��ͼ
        /// </summary>
        private FarPoint.Win.Spread.SheetView currentView = null;

        /// <summary>
        /// ��ǰ��
        /// </summary>
        private int currentRow = 0;

        #endregion

        #region ����

        private bool noSplitAtAll = true;
        /// <summary>
        /// 0���ɲ��
        /// </summary>
        [Description("סԺҩƷ���ɲ��"), Category("����"), DefaultValue(true),Browsable(false)]
        public bool NoSplitAtAll
        {
            get
            {
                return noSplitAtAll;
            }
            set
            {
                noSplitAtAll = value;
            }
        }

        private bool splitAndNoInteger = true;
        /// <summary>
        /// 1�ɲ�ֲ�ȡ��
        /// </summary>
        [Description("סԺҩƷ�ɲ�ֲ�ȡ��"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool SplitAndNoInteger
        {
            get
            {
                return splitAndNoInteger;
            }
            set
            {
                splitAndNoInteger = value;
            }
        }

        private bool splitAndUpperToInteger = true;
        /// <summary>
        /// 2�ɲ����ȡ��
        /// </summary>
        [Description("סԺҩƷ�ɲ����ȡ��"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool SplitAndUpperToInteger
        {
            get
            {
                return splitAndUpperToInteger;
            }
            set
            {
                splitAndUpperToInteger = value;
            }
        }

        private bool nosplitAndDayToInteger = true;
        /// <summary>
        /// 3���ɲ�ֵ���ȡ��
        /// </summary>
        [Description("סԺҩƷ���ɲ�ֵ���ȡ��"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool NosplitAndDayToInteger
        {
            get
            {
                return nosplitAndDayToInteger;
            }
            set
            {
                nosplitAndDayToInteger = value;
            }
        }

        private bool splitAndDeptToInteger = true;
        /// <summary>
        /// 4�ɲ�ְ�����ȡ��
        /// </summary>
        [Description("סԺҩƷ�ɲ�ְ�����ȡ��"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool SplitAndDeptToInteger
        {
            get
            {
                return splitAndDeptToInteger;
            }
            set
            {
                splitAndDeptToInteger = value;
            }
        }

        private bool splitAndNurceCellToInteger = true;
        /// <summary>
        /// 5�ɲ�ְ�����ȡ��
        /// </summary>
        [Description("סԺҩƷ�ɲ�ְ�����ȡ��"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool SplitAndNurceCellToInteger
        {
            get
            {
                return splitAndNurceCellToInteger;
            }
            set
            {
                splitAndNurceCellToInteger = value;
            }
        }

        private bool noSplitAndPackageUnit = true;
        /// <summary>
        /// 6�ɲ�ְ�����ȡ��
        /// </summary>
        [Description("סԺҩƷ�����װȡ���߿��"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool NoSplitAndPackageUnit
        {
            get
            {
                return noSplitAndPackageUnit;
            }
            set
            {
                noSplitAndPackageUnit = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitData()
        {
            // ���ؿ���
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Department temp = new FS.HISFC.Models.Base.Department();

            temp.ID = "AAAA";
            temp.Name = "ȫԺ";
            alDept = manager.GetDepartment();
            if (alDept == null)
            {
                MessageBox.Show(Language.Msg("���ȫԺ�����б����") + manager.Err);
                return;
            }
            alDept.Insert(0, temp);

            objHelper.ArrayObject = alDept;
            // ����ҩƷ����
            alDosageMode = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
            if (alDosageMode == null)
            {
                MessageBox.Show(Language.Msg("��ȡҩƷ���ͳ���!") + manager.Err);
                return;
            }

            this.InitControlParam();
        }

        /// <summary>
        /// ��ʼ��������Ϣ
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            //�Ƿ�����ò��ɲ������
            this.NoSplitAtAll = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Can_Set_NoSplitAtAll, true, true);
            //�Ƿ�����ÿɲ�ֲ�ȡ������
            this.SplitAndNoInteger = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Can_Set_SplitAndNoInteger, true, false);
            //�Ƿ�����ÿɲ����ȡ������
            this.SplitAndUpperToInteger = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Can_Set_SplitAndUpperToInteger, true, true);
            //�Ƿ�����ò��ɲ�ֵ���ȡ������
            this.NosplitAndDayToInteger = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Can_Set_NosplitAndDayToInteger, true, true);
            //�Ƿ������סԺҩƷ�ɲ�ְ�����ȡ��
            this.SplitAndDeptToInteger = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Can_Set_SplitAndDeptToInteger, true, false);
            //�Ƿ������סԺҩ���ɲ�ְ�����ȡ��
            this.SplitAndNurceCellToInteger = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Can_Set_SplitAndNurceCellToInteger, true, false);

            //�Ƿ������סԺҩ���ɲ�ְ���װ��λȡ��
            this.NoSplitAndPackageUnit = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Can_Set_NoSplitAndPackUnit, true, false);
        }

        /// <summary>
        /// ����Fp��ʾ��ʽ
        /// </summary>
        private void SetFpValueType()
        {
            if (this.noSplitAtAll)
            {
                this.drugSplitPropertyList.Add("0���ɲ��");
            }
            if (this.splitAndNoInteger)
            {
                this.drugSplitPropertyList.Add("1�ɲ�ֲ�ȡ��");
            }
            if (this.splitAndUpperToInteger)
            {
                this.drugSplitPropertyList.Add("2�ɲ����ȡ��");
            }
            if (this.nosplitAndDayToInteger)
            {
                this.drugSplitPropertyList.Add("3���ɲ�ֵ���ȡ��");
            }
            if (this.splitAndDeptToInteger)
            {
                this.drugSplitPropertyList.Add("4�ɲ�ְ�����ȡ��");
            }
            if (this.splitAndNurceCellToInteger)
            {
                this.drugSplitPropertyList.Add("5�ɲ�ְ�����ȡ��");
            }
            //{9D24EE50-57C5-4cae-8B70-8D8C3E7B4BDC}
            if (this.noSplitAndPackageUnit)
            {
                this.drugSplitPropertyList.Add("6�����װȡ���߿��");
            }
            //���������б�
            string[] str = new string[this.drugSplitPropertyList.Count];
            for (int i = 0; i < this.drugSplitPropertyList.Count; i++)
            {
                str[i] = this.drugSplitPropertyList[i].ToString();
            }

            FarPoint.Win.Spread.CellType.ComboBoxCellType cel = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            cel.Items = str;
            this.neuSpread1_Sheet1.Columns[5].CellType = cel;
            this.neuSpread1_Sheet2.Columns[1].CellType = cel;
        }

        /// <summary>
        /// ��ʼ��ԭʼ����
        /// </summary>
        private void LoadDrugSplitProperty()
        {
            //ҩƷ��Ϣʵ��
            FS.HISFC.Models.Pharmacy.Item drugInfo;
            //��ʱ�������ϳɲ������
            string tempProperty = "";
            //��ȡȫ����ҩ����
            ArrayList alProperty = drugManager.QueryDrugProperty();
            if (alProperty == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ҩ���Գ���!") + drugManager.Err);
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ���ҩƷ��ҩ������Ϣ..."));
            Application.DoEvents();
            foreach (FS.FrameWork.Models.NeuObject info in alProperty)
            {
                switch (info.User01)
                {
                    case "0":
                        tempProperty = "0���ɲ��";
                        break;
                    case "1":
                        tempProperty = "1�ɲ�ֲ�ȡ��";
                        break;
                    case "2":
                        tempProperty = "2�ɲ����ȡ��";
                        break;
                    case "3":
                        tempProperty = "3���ɲ�ֵ���ȡ��";
                        break;
                    case "4":
                        tempProperty = "4�ɲ�ְ�����ȡ��";
                        break;
                    case "5":                      
                        tempProperty = "5�ɲ�ְ�����ȡ��";
                        break;
                    //{9D24EE50-57C5-4cae-8B70-8D8C3E7B4BDC}
                    case "6":
                        tempProperty = "6�����װȡ���߿��";
                        break;

                }
                //ҩƷ
                if (info.Memo == "0")
                {
                    //ȡҩƷ�ֵ���Ϣ
                    drugInfo = this.drugManager.GetItem(info.ID);
                    if (drugInfo == null) continue;
                    //�������
                    drugInfo.User01 = tempProperty;
                    //���ű���
                    drugInfo.Product.Company.ID = info.User02;

                    //drugInfo.Product.Company.Name = this.objHelper.GetObjectFromID(info.User02).Name;
                    drugInfo.Product.Company.Name = this.objHelper.GetName(info.User02);

                    //��������
                    this.AddRow(drugInfo, 0);
                }
                else//����
                {
                    drugInfo = new FS.HISFC.Models.Pharmacy.Item();
                    //���ͱ���
                    drugInfo.ID = info.ID;
                    //��������
                    drugInfo.Name = info.Name;
                    //�������
                    drugInfo.User01 = tempProperty;
                    //���ű���
                    drugInfo.Product.Company.ID = info.User02;
                    //��������
                    drugInfo.Product.Company.Name = this.objHelper.GetObjectFromID(info.User02).Name;
                    this.AddRow(drugInfo, 1);
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// �ظ��ж�
        /// </summary>
        /// <param name="index"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private int RepeatCheck(int index, string ID)
        {
            //ҩƷ��Ϣ
            if (index == 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string id = this.neuSpread1_Sheet1.Cells[i, 0].Tag.ToString();
                    if (id == ID)
                    {
                        MessageBox.Show(Language.Msg("��ǰѡ��ҩƷ�Ѿ����ڣ�������ѡ��!"));
                        return -1;
                    }
                }
            }
            else
            {
            }
            return 0;
        }

        /// <summary>
        /// ���һ��
        /// </summary>
        /// <param name="item">ҩƷ��Ϣʵ��</param>
        /// <param name="index">0ҩƷ1����</param>
        private void AddRow(FS.HISFC.Models.Pharmacy.Item item, int index)
        {
            //ҩƷ��ϸ
            if (index == 0)
            {
                //if( this.RepeatCheck( index , item.ID ) < 0 )
                //{
                //    return;
                //}

                int rowCount = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);
                //ҩƷ����
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 0).Tag = item.ID;
                //ҩƷ����
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 0).Value = item.Name;
                //���
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 1).Value = item.Specs;
                //��װ����
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 2).Value = item.PackQty;
                //��װ��λ
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 3).Value = item.PackUnit;
                //��С��λ
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 4).Value = item.MinUnit;
                //�������
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 5).Value = item.User01;
                //��������
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 6).Value = item.Product.Company.Name;
                //���ű���
                this.neuSpread1_Sheet1.Cells.Get(rowCount, 6).Tag = item.Product.Company.ID;
            }
            else //������Ϣ
            {
                int rowCount = this.neuSpread1_Sheet2.Rows.Count;
                this.neuSpread1_Sheet2.Rows.Add(rowCount, 1);
                //���ͱ���
                this.neuSpread1_Sheet2.Cells.Get(rowCount, 0).Tag = item.ID;
                //��������
                this.neuSpread1_Sheet2.Cells.Get(rowCount, 0).Value = item.Name;
                //�������
                this.neuSpread1_Sheet2.Cells.Get(rowCount, 1).Value = item.User01;
                //���ű���
                this.neuSpread1_Sheet2.Cells.Get(rowCount, 2).Tag = item.Product.Company.ID;
                //��������
                this.neuSpread1_Sheet2.Cells.Get(rowCount, 2).Value = item.Product.Company.Name;
            }
        }

        /// <summary>
        /// ����һ����
        /// </summary>
        private void AddRow()
        {
            if (this.neuSpread1.ActiveSheetIndex == 0)
            {
                if (currentRow >= 0 && currentView != null)
                {
                    this.ucDrugList1_ChooseDataEvent(currentView, currentRow);
                }
                else
                {
                    MessageBox.Show(Language.Msg("��ѡ��ҩƷ"));
                }

            }
            else
            {
                //�������
                FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                //�������
                this.AddRow(item, 1);
            }
        }

        /// <summary>
        /// ɾ��һ��
        /// </summary>
        private void DeleteRow()
        {
            if (this.neuSpread1.ActiveSheet.Rows.Count <= 0)
            {
                return;
            }
            DialogResult result = MessageBox.Show(Language.Msg("ȷ��ɾ��������ҩ����������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No)
            {
                return;
            }
            //�������ݿ⴦������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            this.drugManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string itemCode = "";
            string deptCode = "";
            //ҩƷ��ϸ
            if (this.neuSpread1.ActiveSheetIndex == 0)
            {
                itemCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Tag.ToString();
                deptCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 6].Tag.ToString();
            }
            else //������Ϣ
            {
                itemCode = this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.ActiveRowIndex, 0].Tag.ToString();
                deptCode = this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.ActiveRowIndex, 2].Tag.ToString();
            }

            if (this.drugManager.DeleteDrugProperty(this.neuSpread1.ActiveSheet.ActiveRowIndex.ToString(), itemCode, deptCode) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ɾ����ҩ��������ʧ�ܣ�") + this.drugManager.Err);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.neuSpread1.ActiveSheet.Rows.Remove(this.neuSpread1.ActiveSheet.ActiveRowIndex, 1);
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns>��д��ȷ����1 ���򷵻أ�1</returns>
        private int isValid()
        {
            if (this.neuSpread1.ActiveSheetIndex == 0)
            {
                #region ҩƷ��ϸ
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��ѡ���  ") + (i + 1).ToString() + Language.Msg("  �ж�Ӧ��ҩƷ"));
                        return -1;
                    }
                    if (this.neuSpread1_Sheet1.Cells[i, 5].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��ѡ���  ") + (i + 1).ToString() + Language.Msg("  �ж�Ӧ����ҩ����"));
                        return -1;
                    }
                    if (this.neuSpread1_Sheet1.Cells[i, 6].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��ѡ���") + (i + 1).ToString() + Language.Msg("�п���"));
                        return -1;
                    }
                }
                #endregion
            }
            else
            {
                #region ҩƷ����
                for (int i = 0; i < this.neuSpread1_Sheet2.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet2.Cells[i, 0].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��ѡ���  ") + (i + 1).ToString() + Language.Msg("  �ж�Ӧ��ҩƷ����"));
                        return -1;
                    }
                    if (this.neuSpread1_Sheet1.Cells[i, 1].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��ѡ���  ") + (i + 1).ToString() + Language.Msg("  �ж�Ӧ����ҩ����"));
                        return -1;
                    }
                    if (this.neuSpread1_Sheet2.Cells[i, 2].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��ѡ���  ") + (i + 1).ToString() + Language.Msg("  �ж�Ӧ�Ŀ���"));
                        return -1;
                    }
                }
                #endregion
            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        private int SaveData()
        {
            if (this.isValid() == -1)
            {
                return -1;
            }

            //�������ݿ⴦������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            this.drugManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //ȫ��ɾ��
            if (this.drugManager.DeleteDrugProperty(this.neuSpread1.ActiveSheetIndex.ToString(), "A", "A") == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ɾ��ԭ����ҩ��������ʧ�ܣ�") + this.drugManager.Err);
                return -1;
            }
            FS.FrameWork.Models.NeuObject info;
            if (this.neuSpread1.ActiveSheetIndex == 0)
            {
                #region ҩƷ
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    //ҩƷ����
                    info.ID = this.neuSpread1_Sheet1.Cells[i, 0].Tag.ToString();
                    //ҩƷ����
                    info.Name = this.neuSpread1_Sheet1.Cells[i, 0].Value.ToString();
                    //����
                    info.Memo = "0";
                    //�������								
                    info.User01 = this.neuSpread1_Sheet1.Cells[i, 5].Value.ToString().Substring(0, 1);
                    //���ű��� "AAAA" ȫԺ
                    info.User02 = this.neuSpread1_Sheet1.Cells[i, 6].Tag.ToString();
                    //�����
                    if (this.drugManager.InsertDrugProperty(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        if (this.drugManager.DBErrCode == 1)
                        {
                            MessageBox.Show(Language.Msg("�����Ѵ��ڣ������ظ�ά����\n" + "ҩƷ���ƣ�") + info.Name + Language.Msg("\n �������ƣ�") + this.neuSpread1_Sheet1.Cells[i, 6].Text);
                        }
                        else
                        {
                            MessageBox.Show(this.drugManager.Err);
                        }
                        return -1;
                    }
                }
                #endregion
            }
            else
            {
                #region ����
                for (int i = 0; i < this.neuSpread1_Sheet2.Rows.Count; i++)
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    //���ͱ���
                    info.ID = this.neuSpread1_Sheet2.Cells[i, 0].Tag.ToString();
                    //��������
                    info.Name = this.neuSpread1_Sheet2.Cells[i, 0].Value.ToString();
                    //���� 0 ҩƷ 1 ����
                    info.Memo = "1";
                    //�������						
                    info.User01 = this.neuSpread1_Sheet2.Cells[i, 1].Value.ToString().Substring(0, 1);
                    //���ű��� "AAAA" ȫԺ
                    info.User02 = this.neuSpread1_Sheet2.Cells[i, 2].Tag.ToString();

                    if (this.drugManager.InsertDrugProperty(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        if (this.drugManager.DBErrCode == 1)
                        {
                            MessageBox.Show(Language.Msg("�����Ѵ��ڣ������ظ�ά����\n" + "ҩƷ���ƣ�") + info.Name + Language.Msg("\n �������ƣ�") + this.neuSpread1_Sheet2.Cells[i, 2].Text);
                        }
                        else
                        {
                            MessageBox.Show(this.drugManager.Err);
                        }
                        return -1;
                    }
                }
                #endregion
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ���"));
            return 1;
        }

        /// <summary>
        /// ����ѡ�񴰿�
        /// </summary>
        private void PopSelectWindow()
        {
            if (this.neuSpread1.ActiveSheet.Rows.Count <= 0)
            {
                return;
            }
            //��ǰ��¼���С���
            int iRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            int iColumn = this.neuSpread1.ActiveSheet.ActiveColumnIndex;
            if (iRow < 0) return;

            #region ����
            if ((this.neuSpread1.ActiveSheetIndex == 0 && iColumn == 6) || (this.neuSpread1.ActiveSheetIndex == 1 && iColumn == 2))
            {

                FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alDept, ref info) == 0)
                {
                    return;
                }
                else
                {
                    //ҩƷ��ϸ
                    if (this.neuSpread1.ActiveSheetIndex == 0)
                    {
                        //���ű���
                        this.neuSpread1_Sheet1.Cells[iRow, 6].Tag = info.ID;
                        //��������
                        this.neuSpread1_Sheet1.Cells[iRow, 6].Value = info.Name;
                    }
                    else//ҩƷ����							
                    {
                        //���ű���
                        this.neuSpread1_Sheet2.Cells[iRow, 2].Value = info.Name;
                        //��������
                        this.neuSpread1_Sheet2.Cells[iRow, 2].Tag = info.ID;
                    }
                }
            }
            #endregion

            #region ����
            if (this.neuSpread1.ActiveSheetIndex == 1 && iColumn == 0)
            {
                FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alDosageMode, ref info) == 0)
                {
                    return;
                }
                else
                {
                    //���ͱ���
                    this.neuSpread1_Sheet2.Cells[iRow, 0].Value = info.Name;
                    //��������
                    this.neuSpread1_Sheet2.Cells[iRow, 0].Tag = info.ID;
                }
            }
            #endregion ����
        }

        #endregion

        #region �¼�
        //��ʼ��
        protected override void OnLoad(EventArgs e)
        {
            //��ʾҩƷ��Ϣ
            this.ucDrugList1.ShowPharmacyList();
            //��ʼ�����ҡ������б�
            this.InitData();
            //����Fp��ʾ��ʽ
            this.SetFpValueType();
            //��ʼ����ά������
            this.LoadDrugSplitProperty();

            this.ucDrugList1.ShowAdvanceFilter = false;
            base.OnLoad(e);
        }

        /// <summary>
        /// �б�ѡ�񴥷����¼�
        /// </summary>
        /// <param name="sv">��ͼ</param>
        /// <param name="activeRow">ѡ�����</param>
        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (this.neuSpread1.ActiveSheetIndex != 0)
            {
                this.neuSpread1.ActiveSheetIndex = 0;
            }
            if (sv != null && activeRow >= 0)
            {
                this.currentView = sv;
                this.currentRow = activeRow;
                string drugID;
                drugID = sv.Cells[activeRow, 0].Value.ToString();
                //ȡҩƷ�ֵ���Ϣ
                FS.HISFC.Models.Pharmacy.Item drugItem = this.drugManager.GetItem(drugID);
                if (drugItem != null)
                {
                    //�������
                    this.AddRow(drugItem, 0);
                }
                else
                {
                    MessageBox.Show(Language.Msg("����ҩƷ������Ϣʧ��"));
                }
            }
        }

        /// <summary>
        /// ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.PopSelectWindow();
        }
        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //���ӹ�����
            this.toolBarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "ɾ��", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolBarService;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.AddRow();
                    break;
                case "ɾ��":
                    this.DeleteRow();
                    break;
            }

        }

        #endregion


    }
}
