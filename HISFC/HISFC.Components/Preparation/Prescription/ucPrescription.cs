using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ����ô���ά��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public partial class ucPrescription : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPrescription()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();
     
        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();      

        /// <summary>
        /// ������Ʒ�б���Ϣ
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> prescriptionList;

        /// <summary>
        /// ��ǰ��ά���õ����ƴ���
        /// </summary>
        private System.Collections.Hashtable hsPrescription = new Hashtable();

        /// <summary>
        /// ��ǰ��ʾ�����ƴ����ĳ�Ʒ����
        /// </summary>
        private string nowDrugPrescription = "";

        /// <summary>
        /// ҩƷ�б�����
        /// </summary>
        private ArrayList alDrugList = null;
        #endregion

        #region ������

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugHelper = null;

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ػ������� ���Ժ�...");
            Application.DoEvents();

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            List<FS.HISFC.Models.Pharmacy.Item> phaList = pharmacyIntegrate.QueryItemList(true);
            if (phaList == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ�б�������") + pharmacyIntegrate.Err);
                return;
            }
            foreach (FS.HISFC.Models.Pharmacy.Item info in phaList)
            {
                info.Memo = info.Specs;
            }

            this.alDrugList = new ArrayList(phaList.ToArray());
            this.drugHelper = new FS.FrameWork.Public.ObjectHelper(this.alDrugList);

            FarPoint.Win.Spread.InputMap im;
            im = this.fsMaterial.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCell = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            this.fsMaterial_Sheet1.Columns[(int)MaterialColumnSet.ColQty].CellType = markNumCell;
            
            this.fsMaterial.PhaListColumnIndex = 1;
            this.fsMaterial.PhaListEnabled = true;
            this.fsMaterial.Init();

            this.GetInterface();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("���ӳ�Ʒ", "�������Ƽ���Ʒ", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("������ϸ", "�����Ƽ���Ʒԭ����ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ���Ƽ���Ʒ���Ʒԭ����ϸ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "���ӳ�Ʒ")
            {
                this.AddNewDrug();
            }
            if (e.ClickedItem.Text == "������ϸ")
            {
                this.AddMaterial();
            }
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelPrescription();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SavePrescription();

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowPrescription();

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        /// <summary>
        /// ѡ���Ʒ
        /// </summary>
        protected FS.HISFC.Models.Pharmacy.Item SelectDrug()
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alDrugList, new string[] { "ҩƷ����", "ҩƷ����", "���" }, new bool[] { false, true, true }, new int[] { 80, 120, 80 }, ref info) == 0)
            {
                return null;
            }
            else
            {
                return info as FS.HISFC.Models.Pharmacy.Item;
            }
        }

        /// <summary>
        /// ��ӳ�Ʒ��Ϣ��Fp��
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected int AddDrugToFp(FS.HISFC.Models.Pharmacy.Item item)
        {
            try
            {
                int rowCount = this.fsDrug_Sheet1.Rows.Count;
                this.fsDrug_Sheet1.Rows.Add(rowCount, 1);

                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColDrugID].Text = item.ID;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColTradeName].Text = item.Name;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColSpecs].Text = item.Specs;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColPackQty].Text = item.PackQty.ToString();
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColPackUnit].Text = item.PackUnit;
                this.fsDrug_Sheet1.Cells[rowCount, (int)DrugColumnSet.ColMinUnit].Text = item.MinUnit;

                this.fsDrug_Sheet1.Rows[rowCount].Tag = item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����Ƽ��³�Ʒ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int AddNewDrug()
        {
            FS.HISFC.Models.Pharmacy.Item item = this.SelectDrug();
            if (item == null)
            {
                return -1;
            }

            if (this.hsPrescription.ContainsKey(item.ID))
            {
                MessageBox.Show(Language.Msg("��ҩƷ��ά�������ƴ���"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            this.hsPrescription.Add(item.ID,null);

            this.AddDrugToFp(item);

            this.fsDrug_Sheet1.ActiveRowIndex = this.fsDrug_Sheet1.Rows.Count - 1;
            this.fsDrug_Sheet1.AddSelection(this.fsDrug_Sheet1.Rows.Count - 1, 0, 1, -1);
            this.fsDrug_SelectionChanged(null, null);

            return 1;
        }

        /// <summary>
        /// ���ԭ�ϡ�������ϸ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int AddMaterial()
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                int rowCount = this.fsMaterial_Sheet1.Rows.Count;

                this.fsMaterial_Sheet1.Rows.Add(rowCount, 1);
            }
            else
            {
                if (this.wrapperInterface != null)
                {
                    this.wrapperInterface.AddNewItem();
                }
            }

            return 1;
        }

        /// <summary>
        /// ��Ӵ�����ϸ��Ϣ
        /// </summary>
        /// <param name="item"></param>
        public int AddItemDetail(Item item)
        {
            for (int rowIndex = 0; rowIndex < this.fsMaterial_Sheet1.Rows.Count; rowIndex++)
            {
                if (this.fsMaterial_Sheet1.Cells[rowIndex, (int)MaterialColumnSet.ColMaterialID].Text == item.ID)
                {
                    MessageBox.Show(item.Name + " ԭ�����Ѵ��� �����ظ����", "������ϸ�ظ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            int i = this.fsMaterial_Sheet1.ActiveRowIndex;

            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialID].Text = item.ID;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialName].Text = item.Name;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text = item.Specs;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text = item.PriceCollection.RetailPrice.ToString();
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text = item.MinUnit;
            this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPackQty].Text = item.PackQty.ToString();

            this.fsMaterial_Sheet1.Rows[i].Tag = item;

            return 1;
        }

        /// <summary>
        /// ����һ����ϸ
        /// </summary>
        protected void Add()
        {
            this.fsMaterial_Sheet1.Rows.Add(this.fsMaterial_Sheet1.Rows.Count, 1);
            this.fsMaterial_Sheet1.ActiveColumnIndex = 0;
        }

        /// <summary>
        /// ��ȡ��Ʒ���ƴ�����Ϣ
        /// </summary>
        /// <returns></returns>
        public int ShowPrescriptionList()
        {
            this.fsMaterial_Sheet1.Rows.Count = 0;
            this.fsDrug_Sheet1.Rows.Count = 0;

            this.prescriptionList = this.preparationManager.QueryPrescriptionList(FS.HISFC.Models.Base.EnumItemType.Drug);
            if (this.prescriptionList == null)
            {
                MessageBox.Show(Language.Msg("δ��ȷ��ȡ��Ʒ���ƴ�����Ϣ \n" + this.preparationManager.Err));
                return -1;
            }

            foreach (FS.FrameWork.Models.NeuObject info in this.prescriptionList)
            {
                if (this.AddDrugToFp(this.drugHelper.GetObjectFromID(info.ID) as FS.HISFC.Models.Pharmacy.Item) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }
    
        /// <summary>
        /// ���ƴ�����Ϣ ����ʾ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ShowPrescription()
        {
            if (this.fsDrug_Sheet1.Rows.Count <= 0)
                return -1;

            string drugCode = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, (int)DrugColumnSet.ColDrugID].Text;
            this.fsMaterial_Sheet1.Rows.Count = 0;

            this.lbInformation.Text = string.Format("{0}  ��Ʒ�������ݣ���׼�������Գ�Ʒ��1��С��λΪ��׼����", this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, (int)DrugColumnSet.ColTradeName].Text);

            List<FS.HISFC.Models.Preparation.Prescription> al = this.preparationManager.QueryDrugPrescription(drugCode,FS.HISFC.Models.Preparation.EnumMaterialType.Material);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰѡ���Ʒ�����ƴ�����Ϣ����\n" + drugCode));
                return -1;
            }
            foreach (FS.HISFC.Models.Preparation.Prescription info in al)
            {
                int i = this.fsMaterial_Sheet1.Rows.Count;

                this.fsMaterial_Sheet1.Rows.Add(i, 1);
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialID].Text = info.Material.ID;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialName].Text = info.Material.Name;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text = info.Specs;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text = info.Price.ToString();
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColQty].Text = info.NormativeQty.ToString();
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMemo].Text = info.Memo;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text = info.NormativeUnit;
                this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPackQty].Text = info.MaterialPackQty.ToString();

                this.fsMaterial_Sheet1.Rows[i].Tag = info.Material;
            }

            if (this.wrapperInterface != null)
            {
                if (this.nowDrugPrescription == "" || this.nowDrugPrescription == null)
                {
                    this.wrapperInterface.Drug = this.drugHelper.GetObjectFromID(drugCode) as FS.HISFC.Models.Pharmacy.Item;

                }
                this.wrapperInterface.Query();
            }

            return 1;
        }

        /// <summary>
        /// �������ƴ�����Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SavePrescription()
        {
            if (this.fsDrug_Sheet1.Rows.Count <= 0)
            {
                return 1;
            }
            if (this.fsMaterial_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show(Language.Msg("��ά���Ƽ����ƴ�����Ϣ"));
                return 1;
            }

            for (int i = 0; i < this.fsMaterial_Sheet1.Rows.Count; i++)
            {
                if (this.fsMaterial_Sheet1.Cells[i, 0].Text == "")
                {
                    continue;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColQty].Text) == 0)
                {
                    MessageBox.Show(Language.Msg("��׼�������������"));
                    return 1;
                }
            }

            string drugCode = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, (int)DrugColumnSet.ColDrugID].Text;
            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.preparationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {                
                if (this.preparationManager.DelPrescription(drugCode,FS.HISFC.Models.Base.EnumItemType.Drug) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("ɾ�����ƴ�����Ϣ����" + this.preparationManager.Err));
                    return -1;
                }

                FS.HISFC.Models.Pharmacy.Item drug = this.drugHelper.GetObjectFromID(drugCode) as FS.HISFC.Models.Pharmacy.Item;

                FS.HISFC.Models.Preparation.Prescription info = null;

                #region ��������ԭ��

                for (int i = 0; i < this.fsMaterial_Sheet1.Rows.Count; i++)
                {
                    if (this.fsMaterial_Sheet1.Cells[i, 0].Text == "")
                    {
                        continue;
                    }

                    info = new FS.HISFC.Models.Preparation.Prescription();

                    info.Material = this.fsMaterial_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject;
                    if (info.Material == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ת������");
                        return -1;
                    }

                    info.Drug = drug;

                    info.Specs = this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMaterialSpecs].Text;
                    info.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPrice].Text);
                    info.NormativeUnit = this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColUnit].Text;

                    info.MaterialType = FS.HISFC.Models.Preparation.EnumMaterialType.Material;
                    info.NormativeQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColQty].Text);
                    info.Memo = this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColMemo].Text;
                    info.OperEnv.ID = this.preparationManager.Operator.ID;
                    info.OperEnv.OperTime = sysTime;
                    info.MaterialPackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)MaterialColumnSet.ColPackQty].Text);

                    if (this.preparationManager.SetPrescription(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        if (this.preparationManager.DBErrCode == 1)
                        {
                            MessageBox.Show(info.Material.Name + "�����ظ����");                            
                        }
                        else
                        {
                            MessageBox.Show(Language.Msg("����" + info.Drug.Name + "���ƴ�����Ϣʧ��" + this.preparationManager.Err));
                        }

                        return -1;
                    }
                }

                #endregion

                #region ���ýӿڱ���

                if (this.wrapperInterface != null)
                {
                    string information = "";
                    if (wrapperInterface.Save(drug, ref information) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ɹ�"));

            return 1;
        }

        /// <summary>
        /// ɾ�����ƴ�����Ϣ
        /// </summary>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ���-1</returns>
        public int DelPrescription()
        {
            if (this.fsDrug_Sheet1.Rows.Count <= 0)
                return 1;

            string drugCode = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, 0].Text;
            if (this.fsDrug.ContainsFocus)
            {
                #region �Ƽ���Ʒɾ��

                DialogResult rs = MessageBox.Show(Language.Msg("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��\n ����ɾ��������ɾ���ó�Ʒ�������ƴ�����Ϣ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                    return 1;
                if (this.preparationManager.DelPrescription(drugCode,FS.HISFC.Models.Base.EnumItemType.Drug) == -1)
                {
                    MessageBox.Show("�Ե�ǰѡ���Ʒִ��ɾ������ʧ��\n" + this.preparationManager.Err);
                    return -1;
                }

                if (this.hsPrescription.ContainsKey(drugCode))
                {
                    this.hsPrescription.Remove(drugCode);
                }

                this.ShowPrescriptionList();

                this.ShowPrescription();

                #endregion
            }
            else if (this.fsMaterial.ContainsFocus)
            {
                #region ����ԭ��ɾ��

                DialogResult rs = MessageBox.Show(Language.Msg("ɾ����ǰѡ��ĳ�Ʒ���ƴ�����Ϣ��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                    return 1;

                if (this.fsMaterial_Sheet1.Rows.Count <= 0)
                    return 1;
                int iIndex = this.fsMaterial_Sheet1.ActiveRowIndex;
                FS.FrameWork.Models.NeuObject material = this.fsMaterial_Sheet1.Rows[iIndex].Tag as FS.FrameWork.Models.NeuObject;
                if (material == null)
                {
                    return 1;
                }
                if (this.preparationManager.DelPrescription(drugCode,FS.HISFC.Models.Base.EnumItemType.Drug, material.ID) == -1)
                {
                    MessageBox.Show("�Ե�ǰѡ�񴦷���¼����ɾ������ʧ��\n" + this.preparationManager.Err);
                    return -1;
                }
                this.fsMaterial_Sheet1.Rows.Remove(iIndex, 1);

                #endregion
            }
            else if (this.wrapperInterface != null)
            {
                if (this.neuTabControl1.SelectedTab != this.tabPage1)
                {
                    this.wrapperInterface.Delete();
                }
            }

            return 1;
        }

        #region ���ýӿ����

        HISFC.Components.Preparation.IPrescription wrapperInterface = null;

        /// <summary>
        /// ��ȡ�ӿ�ʵ��
        /// </summary>
        /// <returns></returns>
        protected int GetInterface()
        {
            try
            {
                wrapperInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(HISFC.Components.Preparation.IPrescription)) as HISFC.Components.Preparation.IPrescription;
                if (wrapperInterface == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ�ӿڷ�������"));
                    return -1;
                }

                System.Windows.Forms.TabPage interfaceTab = new TabPage(wrapperInterface.DisplayTitle);

                this.neuTabControl1.TabPages.Add(interfaceTab);

                wrapperInterface.Control.Dock = DockStyle.Fill;
                interfaceTab.Controls.Add(wrapperInterface.Control);

                this.wrapperInterface.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        #endregion

        private void fsDrug_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.nowDrugPrescription == this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, 0].Text)
            {
                return;
            }
            else
            {
                this.nowDrugPrescription = this.fsDrug_Sheet1.Cells[this.fsDrug_Sheet1.ActiveRowIndex, 0].Text;
            }

            if (this.wrapperInterface != null)
            {
                this.wrapperInterface.Drug = this.drugHelper.GetObjectFromID(this.nowDrugPrescription) as FS.HISFC.Models.Pharmacy.Item;
            }

            this.ShowPrescription();
        }

        private void fsMaterial_SelectItem(object sender, EventArgs e)
        {
            if (this.AddItemDetail(sender as FS.HISFC.Models.Pharmacy.Item) == 1)
            {
                this.fsMaterial_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColQty;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fsMaterial.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fsMaterial_Sheet1.ActiveColumnIndex == (int)MaterialColumnSet.ColQty)
                    {
                        this.fsMaterial_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColMemo;
                        return base.ProcessDialogKey(keyData);
                    }
                    if (this.fsMaterial_Sheet1.ActiveColumnIndex == (int)MaterialColumnSet.ColMemo)
                    {
                        this.fsMaterial_Sheet1.Rows.Add(this.fsMaterial_Sheet1.Rows.Count, 1);
                        this.fsMaterial_Sheet1.ActiveRowIndex = this.fsMaterial_Sheet1.Rows.Count;
                        this.fsMaterial_Sheet1.ActiveColumnIndex = (int)MaterialColumnSet.ColMaterialName;
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }        

        private void ucPrescription_Load(object sender, EventArgs e)
        {
            this.Init();

            this.ShowPrescriptionList();

            this.ShowPrescription();            
        }

        #region ö��

        /// <summary>
        /// �Ƽ���Ʒ������
        /// </summary>
        protected enum DrugColumnSet
        {
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColDrugID,
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// ��װ��λ
            /// </summary>
            ColPackUnit,
            /// <summary>
            /// ��С��λ
            /// </summary>
            ColMinUnit
        }

        protected enum MaterialColumnSet
        {
            /// <summary>
            /// ԭ�ϱ���
            /// </summary>
            ColMaterialID,
            /// <summary>
            /// ����
            /// </summary>
            ColMaterialName,
            /// <summary>
            /// ���
            /// </summary>
            ColMaterialSpecs,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// �۸�
            /// </summary>
            ColPrice,
            /// <summary>
            /// ������
            /// </summary>
            ColQty,
            /// <summary>
            /// ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] interfaceTypes = new Type[] { typeof(HISFC.Components.Preparation.IPrescription) };

                return interfaceTypes;
            }
        }

        #endregion
    }
}
