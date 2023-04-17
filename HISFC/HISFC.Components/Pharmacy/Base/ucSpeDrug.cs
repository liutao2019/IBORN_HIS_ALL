using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ����ҩƷά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��
    ///		
    ///  />
    /// </summary>
    public partial class ucSpeDrug : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSpeDrug()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ҩƷ����������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private ArrayList alDpet = new ArrayList();

        /// <summary>
        /// ��Ա��Ϣ
        /// </summary>
        private ArrayList alPerson = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ�SheetView
        /// </summary>
        protected FarPoint.Win.Spread.SheetView ActiveSv
        {
            get
            {
                return this.neuSpread1.ActiveSheet;
            }
        }

        #endregion

        #region ������ 

        protected override int OnSave(object sender, object neuObject)
        {
            return this.SaveDrugSpecial();
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ɾ��", "ɾ����ϸ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
         
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelDrugSpecial();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            //ҩƷ�б����
            this.ucDrugList1.ShowInfoList("Pharmacy.Item.SpeDrug",new string[] { "SPELL_CODE", "WB_CODE", "TRADE_NAME", "CUSTOM_CODE" });

            this.ucDrugList1.SetFormat(new string[] { "����", "��Ʒ����", "���", "ƴ����", "�����", "�Զ�����" }, new int[] { 100, 120, 100, 60, 60, 60 }, new bool[] { false, true, true, false, false, false, false,false,false,false });
                                     

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            this.alDpet = deptManager.GetDeptmentAll();
            if (this.alDpet == null)
            {
                MessageBox.Show(Language.Msg("�����б����ʧ��") + deptManager.Err);
                return -1;
            }

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            this.alPerson = personManager.GetEmployeeAll();
            if (this.alPerson == null)
            {
                MessageBox.Show(Language.Msg("��Ա�б����ʧ��") + personManager.Err);
                return -1;
            }

            this.SetItemListWidth(3);

            #region ����Fp�س�/���м�

            FarPoint.Win.Spread.InputMap im;

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Space, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #endregion

            return 1;
        }

        #endregion

        #region ����

        /// <summary>
        /// �����б����ݿ�� ��ʾָ����
        /// </summary>
        /// <param name="showColumnCount">��ʾָ���и��� ������������</param>
        public void SetItemListWidth(int showColumnCount)
        {
            int iWidth = this.splitContainer1.SplitterDistance;

            this.ucDrugList1.GetColumnWidth(showColumnCount, ref iWidth);

            this.splitContainer1.SplitterDistance = iWidth + 5;
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Clear(bool isClearAll)
        {
            if (isClearAll)
            {
                this.fpDeptSheet.Rows.Count = 0;
                this.fpDocSheet.Rows.Count = 0;
            }
            else
            {
                this.ActiveSv.Rows.Count = 0;
            }
        }

        /// <summary>
        /// ������ʾ����ҩƷ�б�
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ShowDrugSpecialList()
        {
            FS.HISFC.Models.Pharmacy.EnumDrugSpecialType speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;
            if (this.ActiveSv == this.fpDeptSheet)
            {
                speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;
            }
            else
            {
                speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Doc;
            }

            List<FS.HISFC.Models.Pharmacy.DrugSpecial> drugSpeList = this.consManager.QueryDrugSpecialList(speType);

            if (drugSpeList == null)
            {
                MessageBox.Show(Language.Msg("��������ҩƷ�б�������") + this.consManager.Err);
                return -1;
            }

            this.Clear(false);
            foreach (FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe in drugSpeList)
            {
                this.AddItem(drugSpe);
            }

            return 1;
        }

        /// <summary>
        /// ����ҩƷ��Ϣ��ʾ
        /// </summary>
        /// <param name="drugSpe">����ҩƷ��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int AddItem(FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe)
        {
            this.ActiveSv.Rows.Add(0, 1);

            this.ActiveSv.Cells[0, 0].Text = drugSpe.Item.Name;
            this.ActiveSv.Cells[0, 1].Text = drugSpe.Item.Specs;
            this.ActiveSv.Cells[0, 2].Text = drugSpe.SpeItem.Name;
            this.ActiveSv.Cells[0, 3].Text = drugSpe.Memo;
            this.ActiveSv.Cells[0, 1].Tag = null;

            this.ActiveSv.Rows[0].Tag = drugSpe;

            return 1;
        }

        /// <summary>
        /// ��Ŀ��Ϣ����
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddItem(FS.HISFC.Models.Pharmacy.Item item)
        {
            int iIndex = this.ActiveSv.Rows.Count;
            this.ActiveSv.Rows.Add(iIndex, 1);

            FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = new FS.HISFC.Models.Pharmacy.DrugSpecial();
            if (this.ActiveSv == this.fpDeptSheet)
            {
                drugSpe.SpeType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;
            }
            else
            {
                drugSpe.SpeType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Doc;
            }

            drugSpe.Item = item;

            this.ActiveSv.Cells[iIndex, 0].Text = item.Name;
            this.ActiveSv.Cells[iIndex, 1].Text = item.Specs;
            this.ActiveSv.Cells[iIndex, 0].Tag = "New";
            this.ActiveSv.Rows[iIndex].Tag = drugSpe;

            return 1;
        }

        /// <summary>
        /// ������Ϣɾ��
        /// </summary>
        /// <returns></returns>
        public int DelDrugSpecial()
        {
            if (this.ActiveSv.Rows.Count <= 0)
            {
                return 1;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ������������Ϣ��?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = this.ActiveSv.Rows[this.ActiveSv.ActiveRowIndex].Tag as FS.HISFC.Models.Pharmacy.DrugSpecial;

            if (this.consManager.DelDrugSpecial(drugSpe) == -1)
            {
                MessageBox.Show(Language.Msg(string.Format("ɾ��{0} - {1} ������Ϣʧ��",drugSpe.Item.Name,drugSpe.SpeItem.Name)) + this.consManager.Err);
                return -1;
            }

            MessageBox.Show(Language.Msg("ɾ���ɹ�"));

            this.ActiveSv.Rows.Remove(this.ActiveSv.ActiveRowIndex, 1);

            return 1;
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <returns></returns>
        protected bool IsValid()
        {
            if (this.ActiveSv.Rows.Count <= 0)
            {
                return false;
            }

            for (int i = 0; i < this.ActiveSv.Rows.Count; i++)
            {
                if (this.ActiveSv.Cells[i, 2].Text == "")
                {
                    MessageBox.Show(Language.Msg("������" + this.ActiveSv.Cells[i,0].Text + " ������Ŀ��Ϣ"));
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.ActiveSv.Cells[i, 3].Text, 20))
                { 
                    MessageBox.Show(Language.Msg(string.Format("{0} - {1} ������Ϣ�� ��ע��Ϣ���� ����20���ַ�",this.ActiveSv.Cells[i,0].Text,this.ActiveSv.Cells[0,2].Text)));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ��������ҩƷ��Ϣ
        /// </summary>
        /// <returns></returns>
        public int SaveDrugSpecial()
        {
            if (!this.IsValid())
            {
                return -1;
            }

            FS.HISFC.Models.Pharmacy.EnumDrugSpecialType speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;

            if (this.ActiveSv == this.fpDeptSheet)
            {
                speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Dept;
            }
            else
            {
                speType = FS.HISFC.Models.Pharmacy.EnumDrugSpecialType.Doc;
            }

            DateTime sysTime = this.consManager.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ��ɾ��ԭ������Ϣ

            if (this.consManager.DelDrugSpecial(speType) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ԭ����ҩƷ��Ϣɾ��ʧ��") + this.consManager.Err);
                return -1;
            }

            #endregion

            #region ��������Ϣ����

            for (int i = 0; i < this.ActiveSv.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = this.ActiveSv.Rows[i].Tag as FS.HISFC.Models.Pharmacy.DrugSpecial;

                drugSpe.Memo = this.ActiveSv.Cells[i, 3].Text;          //��ע��Ϣ
                drugSpe.Oper.OperTime = sysTime;
                drugSpe.Oper.ID = this.consManager.Operator.ID;

                if (this.consManager.InsertDrugSpecial(drugSpe) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (this.consManager.DBErrCode == 1)
                    {
                        MessageBox.Show(Language.Msg(string.Format("{0} - {1} ������Ϣ�ظ� ��ɾ��һ��", drugSpe.Item.Name, drugSpe.SpeItem.Name)));
                    }
                    else
                    {
                        MessageBox.Show(Language.Msg(string.Format("����{0} - {1} ������Ϣʧ��", drugSpe.Item.Name, drugSpe.SpeItem.Name)) + this.consManager.Err);
                    }
                    return -1;
                }
                this.ActiveSv.Cells[i, 0].Tag = null;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            
            MessageBox.Show(Language.Msg("����ɹ�"));

            return 1;
        }

        /// <summary>
        /// ������Ŀѡ���
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public void PopSpeItem(int iIndex)
        {
            ArrayList alData = new ArrayList();

            if (this.ActiveSv == this.fpDeptSheet)
            {
                alData = this.alDpet;
            }
            else
            {
                alData = this.alPerson;
            }

            string[] label = { "��Ŀ", "��Ŀ����" };
            float[] width = { 80F, 100F };
            bool[] visible = { true, true };
            FS.FrameWork.Models.NeuObject speObj = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alData, ref speObj) == 1)
            {
                FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = this.ActiveSv.Rows[iIndex].Tag as FS.HISFC.Models.Pharmacy.DrugSpecial;

                drugSpe.SpeItem = speObj;

                this.ActiveSv.Cells[iIndex, 2].Text = speObj.Name;

                this.SetFocus(false);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isFpFocus">�Ƿ�����Fp����</param>
        public void SetFocus(bool isFpFocus)
        {
            if (isFpFocus)
            {
                this.ActiveSv.ActiveRowIndex = this.ActiveSv.Rows.Count - 1;
                this.ActiveSv.ActiveColumnIndex = 2;

                this.neuSpread1.StartCellEditing(null, false);
            }
            else
            {
                this.ucDrugList1.SetFocusSelect();
            }

        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.ShowDrugSpecialList();

                this.ucDrugList1.SetFocusSelect();
            }

            base.OnLoad(e);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.ActiveSv.ActiveColumnIndex == 2)
            {
                this.PopSpeItem(e.Row);
            }
        }

        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string drugCode = sv.Cells[activeRow, 0].Text;

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.Models.Pharmacy.Item item = itemManager.GetItem(drugCode);

            if (item == null)
            {
                MessageBox.Show(Language.Msg("��ȡҩƷ��Ϣʧ��") + itemManager.Err);
            }

            this.AddItem(item);

            this.SetFocus(true);
        }

        private void neuSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (!isSucc)
            {
                this.neuSpread1.ActiveSheetChanging -= new FarPoint.Win.Spread.ActiveSheetChangingEventHandler(neuSpread1_ActiveSheetChanging);
                this.neuSpread1.ActiveSheetIndex = this.acctiveSheetIndex;
                this.neuSpread1.ActiveSheetChanging += new FarPoint.Win.Spread.ActiveSheetChangingEventHandler(neuSpread1_ActiveSheetChanging);
            }

            if (isSucc)
            {
                this.ShowDrugSpecialList();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space || keyData == Keys.Enter)
                {
                    if (this.ActiveSv.ActiveColumnIndex == 2)
                    {
                        this.PopSpeItem(this.ActiveSv.ActiveRowIndex);

                        return true;
                    }
                }
            }
            if (keyData == Keys.F8)            
            {
                ucDeptDrugListPriv uc = new ucDeptDrugListPriv();
                uc.Init();

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc); 
            }
            return base.ProcessDialogKey(keyData);
        }

        bool isSucc = true;
        int acctiveSheetIndex = 0;
        
        private void neuSpread1_ActiveSheetChanging(object sender, FarPoint.Win.Spread.ActiveSheetChangingEventArgs e)
        {
            acctiveSheetIndex = this.neuSpread1.ActiveSheetIndex;

            int j = 0;
            for (int i = 0; i < this.ActiveSv.Rows.Count; i++)
            {
                string Flag = string.Empty;
                try
                {
                    Flag = this.ActiveSv.Cells[i, 0].Tag.ToString() ;
                }
                catch (Exception)
                {

                    Flag = null;
                }

                if (Flag != null)
                {
                    j++;
                }
               
            }

            if (j > 0)
            {
                DialogResult dr = MessageBox.Show("��Ϣ�䶯���Ƿ񱣴�", "��ʾ", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    int returnvalue = this.SaveDrugSpecial();

                    if (returnvalue < 0)
                    {
                        isSucc = false;
                    }

                }
                else
                {
                    isSucc = true;
                }
            }
            else
            {
                isSucc = true;
            }
        }
    }
}
