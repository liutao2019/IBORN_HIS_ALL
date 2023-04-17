using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.NFC.Management;
using System.Collections;
using Neusoft.NFC.Function;

namespace Neusoft.UFC.Material.Apply
{
    /// <summary>
    /// [��������:��������������]<br></br>
    /// [�� �� ��: �]<br></br>
    /// [����ʱ��: 2007-4]<br></br>
    /// </summary>
    public partial class ucApply :UFC.Material.ucIMAInOutBase
    {
        public ucApply()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucMatApply_Load);
            this.ucMaterialItemList1.ChooseDataEvent += new Material.Base.ucMaterialItemList.ChooseDataHandler(ucMaterialItemList1_ChooseDataEvent);

        }

        #region �����

        /// <summary>
        ///  ��������� 1��� 0����
        /// </summary>
        private string iotype;

        public IMatManager IManager = null;

        private System.Collections.Hashtable hsIManager = new Hashtable();

        //private Material.Base.ucMaterialItemList ucMaterialItemList1;

        Neusoft.HISFC.Management.Material.Store myStore = new Neusoft.HISFC.Management.Material.Store();

        /// <summary>
        /// ���������
        /// </summary>
        public string IOType
        {
            get
            {
                return this.iotype;
            }
            set
            {
                this.iotype = value;
            }
        }


        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private Neusoft.NFC.Object.NeuObject privDept = new Neusoft.NFC.Object.NeuObject();

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TextBox txtInvoiceNo;

        /// <summary>
        /// �Ƿ���Ҫ����������
        /// </summary>
        private bool isCheck = false;

        /// <summary>
        /// �Ƿ�ⷿ�쵼���
        /// </summary>
        private bool isHeaderCheck = false;

        #endregion

        #region ����

        /// <summary>
        /// FpSheet
        /// </summary>
        [Browsable(false)]
        public FarPoint.Win.Spread.SheetView FpSheetView
        {
            get
            {
                return this.neuSpread1.Sheets[0];
            }
        }


        /// <summary>
        /// Fp
        /// </summary>
        [Browsable(false)]
        public Neusoft.NFC.Interface.Controls.NeuSpread Fp
        {
            get
            {
                return this.neuSpread1;
            }
        }


        /// <summary>
        /// ���������Ƿ���Ҫ���
        /// </summary>
        public bool IsCheck
        {
            get
            {
                return this.isCheck;
            }
            set
            {
                this.isCheck = value;
            }
        }

        /// <summary>
        /// �Ƿ�ⷿ�쵼���
        /// </summary>
        public bool IsHeaderCheck
        {
            get
            {
                return this.isHeaderCheck;
            }
            set
            {
                this.isHeaderCheck = value;
            }
        }

        #endregion

        /// <summary>
        /// ���ô�ѡ������
        /// </summary>
        /// <param name="dataType">������� 0 ��Ʒ�б� 1 Ŀ�굥λ���ҿ���б� 2 �����ҿ���б� 3 �Զ����б�</param>
        /// <param name="sqlIndex">Sql���� ���Ϊ3ʱ�ò�����������</param>
        /// <param name="filterField">�����ֶ� ���Ϊ3ʱ�ò�����������</param>
        /// <param name="formatStr">Sql���� ���Ϊ3ʱ�ò�����������</param>
        /// <returns></returns>
        public int SetSelectData(string dataType, bool isBatch, string[] sqlIndex, string[] filterField, params string[] formatStr)
        {
            this.ucMaterialItemList1.ShowFpRowHeader = false;

            switch (dataType)
            {
                case "0":
                    this.ucMaterialItemList1.ShowMaterialList();
                    break;
                case "1":
                    if (this.TargetDept.ID == "")
                    {
                        MessageBox.Show("��ѡ�񹩻���λ");
                        return -1;
                    }
                    //this.ucMaterialItemList1.ShowDeptStorage(this.TargetDept.ID, isBatch);
                    this.ucMaterialItemList1.ShowApplyAllList(this.TargetDept.ID, isBatch);
                    break;
                case "2":
                    this.ucMaterialItemList1.ShowDeptStorage(this.DeptInfo.ID, isBatch);
                    break;
                case "3":
                    this.ucMaterialItemList1.ShowInfoList(sqlIndex, filterField, formatStr);
                    break;
            }

            return 1;
        }


        /// <summary>
        /// ���ô�ѡ��������ʾ
        /// </summary>
        /// <param name="label"></param>
        /// <param name="width"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public void SetSelectFormat(string[] label, int[] width, bool[] visible)
        {
            this.ucMaterialItemList1.SetFormat(label, width, visible);
        }


        /// <summary>
        /// ��ȡ��ʾ���ݵĵ�һ�е�ָ���п��
        /// </summary>
        /// <param name="columnNum">������������</param>
        /// <param name="width">���صĿ��</param>
        protected void GetColumnWidth(int iColumn, ref int iWidth)
        {
            this.ucMaterialItemList1.GetColumnWidth(iColumn, ref iWidth);
        }


        /// <summary>
        /// �����б����ݿ�� ��ʾָ����
        /// </summary>
        /// <param name="showColumnCount">��ʾָ���и��� ������������</param>
        public void SetItemListWidth(int showColumnCount)
        {
            int iWidth = this.panelItemSelect.Width;

            this.ucMaterialItemList1.GetColumnWidth(showColumnCount, ref iWidth);

            this.panelItemSelect.Width = iWidth + 5;
        }


        /// <summary>
        /// ���� 
        /// </summary>
        /// <param name="filterData"></param>
        protected override void Filter(string filterData)
        {
            if (this.IManager != null)
            {
                this.IManager.Filter(filterData);
            }
        }


        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            Neusoft.NFC.Management.DataBaseManger dataManager = new DataBaseManger();
            Neusoft.NFC.Object.NeuObject class2Priv = new Neusoft.NFC.Object.NeuObject();

            if (this.IOType == "1")
            {
                class2Priv.ID = "0510";
                class2Priv.Name = "�������";
            }
            else
            {
                class2Priv.ID = "0520";
                class2Priv.Name = "��������";
            }

            this.Class2Priv = class2Priv;
            this.OperInfo = dataManager.Operator;
            this.OperInfo.Memo = "apply";
            Neusoft.HISFC.Management.Manager.Department managerIntegrate = new Neusoft.HISFC.Management.Manager.Department();
            Neusoft.HISFC.Object.Base.Department dept = managerIntegrate.GetDeptmentById(this.DeptInfo.ID);

            if (dept != null)
            {
                this.DeptInfo.Memo = dept.DeptType.ID.ToString();               
            }
            if (this.FilePath == "")
            {
                this.FilePath = @"\Setting\MatApplySetting.xml";
            }

            if (this.SetPrivType(true) == -1)
            {
                return;
            }

            this.GetInterface();
        }


        protected override void FilterPriv(ref List<Neusoft.NFC.Object.NeuObject> privList)
        {

        }


        /// <summary>
        /// ��ʼ��Fp��Ϣ
        /// </summary>
        public void InitFp()
        {
            FarPoint.Win.Spread.InputMap im;

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }


        /// <summary>
        /// ���ó�ʼ����
        /// </summary>
        public void SetFocus()
        {
            if (this.IsShowItemSelectpanel)
            {
                this.ucMaterialItemList1.SetFocusSelect();
            }
            else
            {
                this.IManager.SetFocusSelect();
            }
        }


        /// <summary>
        /// ����Fp
        /// </summary>
        public void SetFpFocus()
        {
            this.neuSpread1.Select();
        }


        protected override void Clear()
        {
            base.Clear();

            this.ucMaterialItemList1.Clear();

            this.FpSheetView.Reset();

            this.FpSheetView.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpSheetView.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);

            this.Fp.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.Fp.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;

            this.InitFp();
        }


        /// <summary>
        /// ���ýӿ�ʵ��
        /// </summary>
        private void GetInterface()
        {
            this.Clear();

            //ͨ�����䷽ʽ��ȡFactory�ļ� ���� ���Խ� Factory��������ص������ļ�ȫ��Ų��UFC ʵ���Զ���            

            MatFactory factory = new MatFactory();
            this.IManager = factory.GetApplyInstance(this.PrivType, this);

            if (this.IManager == null)
            {
                System.Windows.Forms.MessageBox.Show("������������ȡ��Ӧ�ӿ�ʵ��ʧ��");
                return;
            }

            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;

            this.neuSpread1_Sheet1.DataSource = null;
            //Ϊ��ʵ�ֹ��� ��ֵDefaultView
            DataTable dtTemp = this.IManager.InitDataTable();
            if (dtTemp != null)
                this.neuSpread1_Sheet1.DataSource = dtTemp.DefaultView;
            else
                this.neuSpread1_Sheet1.DataSource = dtTemp;

            this.IManager.SetFormat();				//��ʽ��

            this.IManager.SetFocusSelect();			//��������

            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.panelItemSelect.SuspendLayout();
            this.SuspendLayout();

            this.AddItemInputUC(this.IManager.InputModualUC);

            this.neuPanel1.ResumeLayout();
            this.neuPanel3.ResumeLayout();
            this.neuPanel4.ResumeLayout();
            this.panelItemSelect.ResumeLayout();
            this.ResumeLayout();
        }


        #region ������

        /*
        protected override int OnSave()
        {
            if (this.IManager != null)
            {
                if (this.IsCheck == false)
                {
                    this.IManager.Save();
                }
                else
                {
                    this.IManager.SaveCheck(this.isHeaderCheck);
                }

            }

            return base.OnSave();
        }

        protected override int OnCancel()
        {
            if (this.IManager != null)
            {
                this.IManager.Cancel();

            }

            return base.OnSave();
        }

        protected override int OnShowApplyList()
        {
            if (this.IManager != null)
            {
                this.IManager.ShowApplyList(this.isHeaderCheck);
            }

            this.lbInfo.Text = (this.IManager as InApplyPriv).showInfo;

            return base.OnShowApplyList();
        }

        protected override int OnShowInList()
        {
            if (this.IManager != null)
            {
                this.IManager.ShowInList();
            }

            return base.OnShowInList();
        }

        protected override int OnShowOutList()
        {
            if (this.IManager != null)
            {
                this.IManager.ShowOutList();
            }

            return base.OnShowOutList();
        }

        protected override int OnDel()
        {
            if (this.IManager != null)
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    this.IManager.Delete(this.neuSpread1_Sheet1, this.neuSpread1_Sheet1.ActiveRowIndex);
                }
            }

            return base.OnDel();
        }

        protected override int OnShow()
        {
            this.panelItemSelect.Visible = !this.panelItemSelect.Visible;
            return base.OnShow();
        }

        */

        protected override int OnSave(object sender, object neuObject)
        {
            this.neuSpread1.StopCellEditing();

            this.IManager.Save();

            this.neuSpread1.StartCellEditing(null, false);

            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.IManager.Print();
            return 1;
        }

        internal override void OnApplyList()
        {
            this.IManager.ShowApplyList();
        }

        internal override void OnInList()
        {
            this.IManager.ShowInList();
        }

        internal override void OnStockList()
        {
            this.IManager.ShowStockList();
        }

        internal override void OnOutList()
        {
            this.IManager.ShowOutList();
        }

        internal override void OnDelete()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                this.IManager.Delete(this.neuSpread1_Sheet1, this.neuSpread1_Sheet1.ActiveRowIndex);
            }
        }

        internal override void OnImport()
        {
            if (this.IManager != null)
            {
                this.IManager.ImportData();
            }
        }

        #endregion

        private void ucMatApply_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                Neusoft.NFC.Object.NeuObject testPrivDept = new Neusoft.NFC.Object.NeuObject();

                int parma = Neusoft.UFC.Common.Classes.Function.ChoosePivDept("0510", ref testPrivDept);

                if (parma == -1)            //��Ȩ��
                {
                    MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                    return;
                }
                else if (parma == 0)       //�û�ѡ��ȡ��
                {
                    return;
                }

                this.DeptInfo = testPrivDept;

                base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

                //��Ҫ�ڴ˴����� ��������                

                Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڽ������ݳ�ʼ��...���Ժ�");
                Application.DoEvents();

                this.Init();

                this.InitFp();

                if (this.IManager != null)
                {
                    this.IManager.SetFocusSelect();
                }

                Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            }

            this.ucMaterialItemList1.SetKind();
            return;
        }


        protected override void OnEndPrivChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڸ������������ؽ��� ���Ժ�..");
            Application.DoEvents();

            this.GetInterface();

            Neusoft.NFC.Interface.Classes.Function.HideWaitForm();

            base.OnEndPrivChanged(changeData, param);
        }


        protected override void OnEndTargetChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            if (this.IManager != null)
            {
                this.IManager.SetFocusSelect();
            }

            base.OnEndTargetChanged(changeData, param);
        }


        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (sv != null && activeRow >= 0)
            {
                if (this.IManager != null)
                {
                    if (this.IManager.AddItem(sv, activeRow) == -1)
                    {
                        this.ucMaterialItemList1.SetFocusSelect();
                    }
                }
            }
        }


        private void txtInvoiceNo_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (this.txtInvoiceNo.Text.Trim() == "")
            {
                return;
            }

            Neusoft.HISFC.Object.Base.Employee p = this.myStore.Operator as Neusoft.HISFC.Object.Base.Employee;

            ArrayList alAll = new ArrayList();

            ArrayList al0 = this.myStore.QueryInputDetailByInvoice(p.Dept.ID, this.txtInvoiceNo.Text.Trim(), "0");

            ArrayList al1 = this.myStore.QueryInputDetailByInvoice(p.Dept.ID, this.txtInvoiceNo.Text.Trim(), "1");

            ArrayList al2 = this.myStore.QueryInputDetailByInvoice(p.Dept.ID, this.txtInvoiceNo.Text.Trim(), "2");

            if (al0 != null && al0.Count > 0)
            {
                alAll.AddRange(al0);
            }

            if (al1 != null && al1.Count > 0)
            {
                alAll.AddRange(al1);
            }

            if (al2 != null && al2.Count > 0)
            {
                alAll.AddRange(al2);
            }

            Hashtable hsInvoice = new Hashtable();

            ArrayList alTemp = new ArrayList();

            for (int i = 0; i < alAll.Count; i++)
            {
                Neusoft.HISFC.Object.Material.Input input = alAll[i] as Neusoft.HISFC.Object.Material.Input;

                if (!hsInvoice.ContainsKey(input.StoreBase.Company.Name))
                {
                    Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();

                    obj.ID = this.txtInvoiceNo.Text.Trim();
                    obj.Name = input.StoreBase.Company.Name;

                    alTemp.Add(obj);
                }
            }

            string companyName = "";

            if (alTemp.Count > 1)
            {
                //����ѡ�񴰿�
                Neusoft.NFC.Object.NeuObject info = new Neusoft.NFC.Object.NeuObject();

                if (Neusoft.NFC.Interface.Classes.Function.ChooseItem(alTemp, ref info) == 0)
                {
                    return;
                }

                companyName = info.Name;
            }

            if (alAll != null)
            {
                DataSet dsTemp = this.neuSpread1_Sheet1.DataSource as DataSet;

                if (dsTemp == null)
                {
                    DataView dvTemp = this.neuSpread1_Sheet1.DataSource as DataView;

                    for (int i = 0; i < alAll.Count; i++)
                    {
                        Neusoft.HISFC.Object.Material.Input input = alAll[i] as Neusoft.HISFC.Object.Material.Input;

                        if (alTemp.Count > 1)
                        {
                            if (input.StoreBase.Company.Name == companyName)
                            {
                                this.IManager.AddItem(null, input);
                            }
                        }
                        else
                        {
                            this.IManager.AddItem(null, input);
                        }
                    }
                }
            }
        }


        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                /*
                UFC.Material.Report.ucApplyHistory uc = new Material.Report.ucApplyHistory();
                //				if(this.Class2Priv.ID == "0510")//�������
                //				{
                Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();
                obj.ID = this.neuSpread1_Sheet1.Cells[e.Row, 9].Text;
                obj.Name = this.neuSpread1_Sheet1.Cells[e.Row, 0].Text;
                uc.Init("", this.DeptInfo, obj, true);
                //				}

                Neusoft.NFC.Interface.Classes.Function.PopShowControl(uc);
                */
            }
            catch { }

        }

        internal void SetToolButton(bool p, bool p_2, bool p_3, bool p_4, bool p_5)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
