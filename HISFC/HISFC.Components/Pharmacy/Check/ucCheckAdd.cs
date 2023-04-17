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

namespace FS.HISFC.Components.Pharmacy.Check
{
    /// <summary>
    /// [��������: ҩƷ�̵㵥���ӿؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸ļ�¼>
    ///    1.���β�����ȡ���޸Ĵӿ��ҿ�泣���л�ȡ�Ƿ�������� by Sunjh 2010-8-24 {41170BF0-5EFE-4f24-8D63-6CF2AE9FBAAA}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucCheckAdd : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCheckAdd()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// ҩƷ����ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �̵㸽��Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷ��������ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// ѡ������
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.Check> chooseData = new List<FS.HISFC.Models.Pharmacy.Check>();

        /// <summary>
        /// ��ť������
        /// </summary>
        private DialogResult result = DialogResult.Cancel;

        private bool isShowAddCheckBox = false;

        private bool isShowButton = false;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ�̵㸽�ӵ�CheckBox
        /// </summary>
        [Description("�Ƿ���ʾ�̵㸽��Check��"), Category("����"), DefaultValue(false)]
        public bool IsShowAddCheckBox
        {
            get
            {
                return this.isShowAddCheckBox;
            }
            set
            {
                this.isShowAddCheckBox = value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsAdd].Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�²����ܰ�ť
        /// </summary>
        [Description("�Ƿ���ʾ�²����ܰ�ť"), Category("����"), DefaultValue(false)]
        public bool IsShowButton
        {
            get
            {
                return this.isShowButton;
            }
            set
            {
                this.isShowButton = value;
                this.gbButton.Visible = !value;
                this.splitContainer1.Panel1Collapsed = !value;
            }            
        }

        /// <summary>
        /// ��ǰѡ������
        /// </summary>
        public List<FS.HISFC.Models.Pharmacy.Check> ChooseData
        {
            get
            {
                return this.chooseData;
            }
        }

        /// <summary>
        /// ��ť������
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.result;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ɾ����ϸ", "ɾ����ǰѡ���ҩƷ", 8, true, false, null);
            toolBarService.AddToolButton("����ɾ��", "ɾ�����ŵ���", 8, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ����ϸ")
            {
                this.DeleteDetail();
            }
            if (e.ClickedItem.Text == "����ɾ��")
            {
                this.DelAll(this.privDept.ID);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save(this.privDept.ID);

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
            return 1;
        }

        #endregion

        #region ��ʼ��DataTable��Fp����

        /// <summary>
        /// ��ʼ��DataSet
        /// </summary>
        private void InitDataTable()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            this.dt.Columns.AddRange(new DataColumn[] {
                                                                    new DataColumn("�����̵�",    dtBol),
                                                                    new DataColumn("ҩƷ����",	  dtStr),
                                                                    new DataColumn("�Զ�����",    dtStr),
                                                                    new DataColumn("ҩƷ����",	  dtStr),
                                                                    new DataColumn("���",		  dtStr),
                                                                    new DataColumn("��λ��",	  dtStr),
                                                                    new DataColumn("����",		  dtStr),
                                                                    new DataColumn("ƴ����",      dtStr),
                                                                    new DataColumn("�����",      dtStr),
                                                                    new DataColumn("ͨ����ƴ����",dtStr),
                                                                    new DataColumn("ͨ���������",dtStr),
            });

            this.dt.DefaultView.AllowDelete = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowNew = true;

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;

            this.SetFormat();
        }

        /// <summary>
        /// Fp��ʽ��
        /// </summary>
        private void SetFormat()
        {
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradeName].Width = 200F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpecs].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColBatchNO].Width = 60F;

            if (this.isShowAddCheckBox)
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsAdd].Visible = true;
            else
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsAdd].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColDrugNO].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRegularSpell].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRegularWB].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlaceCode].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsAdd].Locked = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlaceCode].BackColor = System.Drawing.Color.SeaShell;
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void InitData()
        {
            //��ʾ����Աѡ��������Ҳ��ж��Ƿ��в���Ȩ��
            //int privParm = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0305", ref this.privDept);
            //if (privParm == 0)
            //{
            //    return;
            //}

            this.privDept = ((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept;

            //�޸�Ϊ�ɿ��Ʋ�����ȡ�Ƿ������̵�
            FS.FrameWork.Management.ControlParam ctrlMgr = new ControlParam();

            //string ctrlStr = ctrlMgr.QueryControlerInfo("510001");            
            //if (ctrlStr == "1")
            //    this.ucDrugList1.ShowDeptStorage(this.privDept.ID, true, 0);
            //else
            //    this.ucDrugList1.ShowDeptStorage(this.privDept.ID, false, 0);

            //���β�����ȡ���޸Ĵӿ��ҿ�泣���л�ȡ�Ƿ�������� by Sunjh 2010-8-24 {41170BF0-5EFE-4f24-8D63-6CF2AE9FBAAA}
            bool isBatch = this.consManager.IsManageBatch(this.privDept.ID);
            if (isBatch)
            {
                this.ucDrugList1.ShowDeptStorage(this.privDept.ID, true, 0);
            }
            else
            {
                this.ucDrugList1.ShowDeptStorage(this.privDept.ID, false, 0);
            }

            this.splitContainer1.SplitterDistance = 140;
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ݲ���
        /// </summary>
        /// <param name="item">�̵㸽��ʵ��</param>
        public int AddDataToTable(FS.HISFC.Models.Pharmacy.Check check)
        {
            try
            {
                this.dt.Rows.Add(new object[]{
                                                          false,							                // �����̵�
                                                          check.Item.ID,							        // ҩƷ����
                                                          check.Item.NameCollection.UserCode,               // �Զ�����
                                                          check.Item.Name,						            // ҩƷ����
                                                          check.Item.Specs,						            // ���
                                                          check.PlaceNO,						            // ��λ��
                                                          check.BatchNO,						            // ����
                                                          check.Item.NameCollection.SpellCode,	            // ƴ����
                                                          check.Item.NameCollection.WBCode,   	            // �����
                                                          check.Item.NameCollection.RegularSpell.SpellCode,	// ͨ����ƴ����
                                                          check.Item.NameCollection.RegularSpell.WBCode	    // ͨ���������														
                                                      }
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        protected void Clear()
        {
            try
            {
                this.dt.Rows.Clear();

                this.neuSpread1_Sheet1.Rows.Count = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isFpFocus"></param>
        protected void SetFocus(bool isFpFocus)
        {
            if (isFpFocus)
            {
                this.neuSpread1.Select();
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColPlaceCode;
            }
            else
            {
                this.ucDrugList1.Select();
                this.ucDrugList1.SetFocusSelect();
            }
        }

        /// <summary>
        /// ���ؿ����̵�ҩƷ������Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        public void ShowCheckAdd(string deptNO)
        {
            this.Clear();

            //ȡ�̵㸽����Ϣ
            ArrayList alDetail = this.itemManager.QueryCheckAddByDept(deptNO);
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg("���ؿ����̵���Ϣ����" + this.itemManager.Err));
                return;
            }

            foreach (FS.HISFC.Models.Pharmacy.Check check in alDetail)
            {
                check.Item = this.itemManager.GetItem(check.Item.ID);
                if (check.Item == null)
                {
                    MessageBox.Show(Language.Msg("�����̵㸽����Ϣʱ ��ȡҩƷ��Ϣ����" + this.itemManager.Err));
                    return;
                }

                if (this.AddDataToTable(check) == -1)
                {
                    MessageBox.Show(Language.Msg("�����ݱ�����̵㸽����Ϣʱ����"));
                    return;
                }
            }

            this.SetFocus(false);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void DeleteDetail()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            if (this.dt.Rows.Count == 1)
            {
                MessageBox.Show(Language.Msg("�õ�ֻʣһ��ҩ ��ѡ������ɾ��"));
                return;
            }
            
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
        }

        /// <summary>
        /// ɾ�������̵㸽����Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        public void DelAll(string deptNO)
        {
            if (this.dt.Rows.Count <= 0)
                return;

            DialogResult result;
            result = MessageBox.Show("ȷ��ɾ����ǰ���м�¼��? �˲������ɻָ�", "", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return;
            }
           
            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.itemManager.DeleteCheckAdd(deptNO) == -1)            
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("ɾ���ɹ�"));

            this.Clear();
        }

        /// <summary>
        /// �̵��Ƿ�������
        /// </summary>
        /// <returns>�Ƿ�������</returns>
        public bool IsValid()
        {
            if (this.dt.Rows.Count <= 0)
                return false;

            foreach (DataRow row in this.dt.Rows)
            {
                if (row["��λ��"].ToString().Trim() == "")
                {
                    MessageBox.Show(Language.Msg("��ά��" + row["ҩƷ����"].ToString() + "   ��λ��"));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ���̵㸽����Ϣ����
        /// </summary>
        /// <param name="deptNO"></param>
        public void Save(string deptNO)
        {
            if (!this.IsValid())
                return;

            this.txtFilter.Text = "";
            this.dt.DefaultView.RowFilter = "1=1";
            for (int i = 0; i < this.dt.DefaultView.Count; i++)
            {
                this.dt.DefaultView[i].EndEdit();
            }

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б���.���Ժ�...");
            Application.DoEvents();

            if (this.itemManager.DeleteCheckAdd(deptNO) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("����ǰɾ��ԭ������Ϣ��������" + this.itemManager.Err));
                return;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.Check info = new FS.HISFC.Models.Pharmacy.Check();

                info.Item.ID = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDrugNO].Text;      //ҩƷ����
                info.StockDept = this.privDept;                                                     //�ⷿ����
                info.PlaceNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColPlaceCode].Text;   //��λ��
                info.BatchNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColBatchNO].Text;     //����
                info.Operation.Oper.ID = this.itemManager.Operator.ID;
                info.Operation.Oper.OperTime = sysTime;                                             //������Ϣ

                if (this.itemManager.InsertCheckAdd(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (this.itemManager.DBErrCode == 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(Language.Msg("�����Ѵ���,�����ظ�ά����\n" + "ҩƷ���ƣ�" + this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColTradeName].Text + "   ��λ�ţ�" + info.PlaceNO));
                        return;
                    }
                    else
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(Language.Msg(this.itemManager.Err));
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show(Language.Msg("����ɹ�"));
        }

        /// <summary>
        /// ����ѡ�е�ҩƷ����ArrayList�����̵㵥
        /// </summary>
        /// <returns>�ɹ�ѡ�񷵻�1 ���򷵻أ�1</returns>
        public int SaveChoose()
        {
            for (int i = 0; i < this.dt.DefaultView.Count; i++)
            {
                this.dt.DefaultView[i].EndEdit();
            }

            string filter = "�����̵� = true";

            this.dt.DefaultView.RowFilter = filter;

            if (MessageBox.Show(Language.Msg("ȷ��Ҫ������ѡ�еġ�" + this.neuSpread1_Sheet1.Rows.Count.ToString() + "����ҩƷ��"), "ȷ�ϼ����̵�", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                filter = "1=1";
                this.dt.DefaultView.RowFilter = filter;
                return -1;
            }

            FS.HISFC.Models.Pharmacy.Check info;

            this.chooseData = new List<FS.HISFC.Models.Pharmacy.Check>();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                info = new FS.HISFC.Models.Pharmacy.Check();

                info.PlaceNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColPlaceCode].Text;		//��λ��
                info.Item.ID = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDrugNO].Text;			//ҩƷ����
                info.BatchNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColBatchNO].Text;			//����
                info.IsAdd = true;												                        //�Ƿ񸽼� 1 ����ҩƷ
                
                this.chooseData.Add(info);
            }

            return 1;
        }

        #endregion

        private void ucCheckAdd_Load(object sender, EventArgs e)
        {
            this.InitDataTable();

            this.SetFormat();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitData();

                this.ShowCheckAdd(this.privDept.ID);

                this.SetFocus(false);
            }
        }       

        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (activeRow < 0)
                return;

            FS.HISFC.Models.Pharmacy.Item item = this.itemManager.GetItem(sv.Cells[activeRow, 0].Text);

            FS.HISFC.Models.Pharmacy.Check check = new FS.HISFC.Models.Pharmacy.Check();

            check.Item = item;
            check.PlaceNO = sv.Cells[activeRow, 4].Text;            //��λ��
            check.BatchNO = sv.Cells[activeRow, 3].Text;            //����

            if (this.AddDataToTable(check) == 1)
                this.SetFocus(true);
            else
                this.SetFocus(false);
        }  

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
                this.result = DialogResult.Cancel;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SaveChoose() == -1)
                return;

            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
                this.result = DialogResult.OK;
            }
        }       

        private void txtFilter1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtFilter.Text != "")
                    this.dt.DefaultView.RowFilter = Function.GetFilterStr(this.dt.DefaultView, "%" + this.txtFilter.Text + "%");
                else
                    this.dt.DefaultView.RowFilter = "1=1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;

                return;
            }

            if (e.KeyData == Keys.Up)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;

                return;
            }
        }

        private void fpSpread1_Click(object sender, EventArgs e)
        {
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;
            int j = this.neuSpread1_Sheet1.ActiveColumnIndex;
            if (j == (int)ColumnSet.ColPlaceCode)
            {
                this.neuSpread1_Sheet1.SetActiveCell(i, (int)ColumnSet.ColPlaceCode, false);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsAdd].Value = true;
            }
            //foreach (DataRow dr in this.dt.Rows)
            //{
            //    dr["�����̵�"] = true;
            //}
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsAdd].Value = false;
            }

            //foreach (DataRow dr in this.dt.Rows)
            //{
            //    dr["�����̵�"] = false;
            //}
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter && this.neuSpread1.ContainsFocus)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex == this.neuSpread1_Sheet1.Rows.Count - 1)
                {
                    this.SetFocus(false);
                }
                else
                {
                    if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColPlaceCode)
                        this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.ActiveRowIndex + 1;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private enum ColumnSet
        {
            /// <summary>
            /// �Ƿ�����̵�
            /// </summary>
            ColIsAdd,
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColDrugNO,
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColUserCode,
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ��λ��
            /// </summary>
            ColPlaceCode,
            /// <summary>
            /// ����
            /// </summary>
            ColBatchNO,
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpellCode,
            /// <summary>
            /// �����
            /// </summary>
            ColWBCode,
            /// <summary>
            /// ͨ����ƴ����
            /// </summary>
            ColRegularSpell,
            /// <summary>
            /// ͨ���������
            /// </summary>
            ColRegularWB
        }           
    }
}
