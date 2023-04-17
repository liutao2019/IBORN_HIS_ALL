using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Management;

namespace Neusoft.SOC.HISFC.Components.Pharmacy.Maintenance
{
    public partial class ucCompoundUsageManager : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucCompoundUsageManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ҩƷ���ʳ������
        /// </summary>
        private string operTypeCode = "USAGE";

        /// <summary>
        /// ���������÷��������
        /// </summary>
        private string compoundUsageTypeCode = "CompoundUsage";

        /// <summary>
        /// ����ҵ�������
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Constant constantManager = new Neusoft.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ƴ���������
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Spell spellManager = new Neusoft.HISFC.BizLogic.Manager.Spell();

        /// <summary>
        /// ϵͳҩƷ�÷�����
        /// </summary>
        private ArrayList alDrugUsageType = new ArrayList();

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected int InitData()
        {
            try
            {
                FarPoint.Win.Spread.InputMap im;
                im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
                im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);

                ///��ʼ��ö����ʾ
                Neusoft.HISFC.Models.Pharmacy.DrugUsageEnumService usageEnumService = new Neusoft.HISFC.Models.Pharmacy.DrugUsageEnumService();

                this.alDrugUsageType = Neusoft.HISFC.Models.Pharmacy.DrugUsageEnumService.List();
                if (this.alDrugUsageType == null)
                {
                    MessageBox.Show(Language.Msg("��ȡϵͳҩƷ���ʷ�������"));
                    return -1;
                }

                this.SetCol();
            }
            catch (Exception e)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="type"></param>
        protected void QueryConsData(string type)
        {
            //����Ժע�÷�����
            ArrayList alCompoundUsage = constantManager.GetAllList(this.compoundUsageTypeCode);
            if (alCompoundUsage == null)
            {
                MessageBox.Show(Language.Msg("��ѯԺע�÷�������������!\r\n") + this.constantManager.Err);
                return;
            }

            Hashtable hsCompoundUsage = new Hashtable();
            foreach (Neusoft.FrameWork.Models.NeuObject con in alCompoundUsage)
            {
                if (!hsCompoundUsage.Contains(con.ID))
                {
                    hsCompoundUsage.Add(con.ID, null);
                }
            }

            ArrayList alCons = this.constantManager.GetAllList(type);
            if (alCons == null)
            {
                MessageBox.Show(Language.Msg("��ѯ�������������") + this.constantManager.Err);
                return;
            }
            if (alCons.Count > 0)
            {
                this.AddConstsToFp(alCons, hsCompoundUsage);
            }

            this.SetCol();
        }

        /// <summary>
        /// ���������ݼ���Fp
        /// </summary>
        /// <param name="list"></param>
        /// <param name="hsInjectUsage"></param>
        private void AddConstsToFp(ArrayList list, Hashtable hsInjectUsage)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            int iCount = 0;

            foreach (Neusoft.HISFC.Models.Base.Const cons in list)
            {
                this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColID].Text = cons.ID;

                foreach (Neusoft.FrameWork.Models.NeuObject info in this.alDrugUsageType)
                {
                    if (cons.UserCode == info.ID)
                    {
                        this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColSysType].Text = info.Name;
                        this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColSysType].Tag = info.ID;
                        break;
                    }
                }

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColName].Text = cons.Name;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMemo].Text = cons.Memo;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColSpellCode].Text = cons.SpellCode;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColWBCode].Text = cons.WBCode;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColValid].Value = cons.IsValid;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColSort].Text = cons.SortID.ToString();
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOperCode].Text = cons.OperEnvironment.ID;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOperTime].Text = cons.OperEnvironment.OperTime.ToString();
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColFlag].Text = "Old";

                if (hsInjectUsage.Contains(cons.ID))
                {
                    neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.IsCompound].Value = true;
                }

                this.neuSpread1_Sheet1.Rows[iCount].Tag = cons;

                iCount++;
            }
        }

        /// <summary>
        /// �����ݱ��ڻ�ȡ����
        /// </summary>
        /// <param name="row"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.Base.Const GetConstFromFp(int iIndex)
        {
            Neusoft.HISFC.Models.Base.Const cons = new Neusoft.HISFC.Models.Base.Const();

            cons.ID = this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColID].Text;
            cons.Name = this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColName].Text;
            cons.SpellCode = this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColSpellCode].Text;
            cons.Memo = this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColMemo].Text;
            cons.WBCode = this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColWBCode].Text;
            cons.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColValid].Value);
            cons.SortID = Neusoft.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColSort].Text);
            try
            {
                foreach (Neusoft.FrameWork.Models.NeuObject info in this.alDrugUsageType)
                {
                    if (this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColSysType].Text == info.Name)
                    {
                        cons.UserCode = info.ID;
                        break;
                    }
                }
            }
            catch
            {
                cons.UserCode = "O1";
            }
            return cons;
        }

        /// <summary>
        /// Fp��ʽ��
        /// </summary>
        private void SetCol()
        {
            FarPoint.Win.Spread.CellType.ComboBoxCellType cel = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            ArrayList alUsage = Neusoft.HISFC.Models.Pharmacy.DrugUsageEnumService.List();
            //�����ַ������飬����ϵͳҩƷ����
            string[] item = new string[alUsage.Count];
            int index = 0;
            //ȡҩƷ�����б��������ַ���������
            foreach (Neusoft.FrameWork.Models.NeuObject info in alUsage)
            {
                item[index] = info.Name;
                index++;
            }

            cel.Items = item;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSysType].CellType = cel;

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColFlag].Text == "Old")
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColID].Locked = true;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColID].Locked = false;
                }
            }
        }

        public void Closeing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                #region ������Ч���ж�

                string tempID = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColID].Text.ToString();
                if (tempID.ToString() == "")
                {
                    MessageBox.Show(Language.Msg("��Ų���Ϊ�գ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (tempID.IndexOf("'") >= 0)
                {
                    MessageBox.Show(Language.Msg("��Ų��ܳ��������ַ�,���� ' "), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(tempID, 20))
                {
                    MessageBox.Show(Language.Msg("��Ų��ܳ���20���ַ�!"));
                    return false;
                }

                #endregion

                #region ������Ч���ж�

                string tempName = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColName].Text.ToString();
                if (tempName.ToString() == "")
                {
                    MessageBox.Show(Language.Msg("���Ʋ���Ϊ�գ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (tempName.IndexOf("'") >= 0)
                {
                    MessageBox.Show(Language.Msg("���Ʋ��ܳ��������ַ�,���� ' "), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(tempName, 50))
                {
                    MessageBox.Show(Language.Msg("���Ʋ��ܳ���20���ַ�!"));
                    return false;
                }

                #endregion

                #region ϵͳ�����Ч���ж�

                string tmepSysType = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColSysType].Text;
                if (tmepSysType.ToString() != "")
                {

                }
                else
                {
                    //MessageBox.Show("��ѡ��ϵͳ����");
                    //return false;
                }

                #endregion
            }

            return true;
        }

        /// <summary>
        /// ���ݱ���
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            int countNum = this.neuSpread1_Sheet1.RowCount;

            #region ��Ч���ж�

            if (!this.Valid())
            {
                return -1;
            }

            #endregion

            this.neuSpread1.StopCellEditing();

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            //Neusoft.FrameWork.Management.Transaction t = new Neusoft.FrameWork.Management.Transaction(Neusoft.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.constantManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            try
            {
                //�������������÷�����
                //��ȫ��ɾ����Ȼ�󵥸�������Ҫ���õĳ���
                if (this.constantManager.DelConstant(this.compoundUsageTypeCode) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("ɾ��Ժע�÷�����ʧ�ܣ�\r\n" + this.constantManager.Err));
                    return -1;
                }

                DateTime sysTime = this.constantManager.GetDateTimeFromSysDateTime();

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    Neusoft.HISFC.Models.Base.Const cons = this.GetConstFromFp(i);
                    if (cons == null)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���淢��������Fp�ڻ�ȡ����ʧ��"));
                        return -1;
                    }

                    cons.OperEnvironment.ID = this.constantManager.Operator.ID;
                    cons.OperEnvironment.OperTime = sysTime;

                    if (this.constantManager.SetConstant(this.operTypeCode, cons) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���淢������" + this.constantManager.Err));
                        return -1;
                    }

                    //����Ժע�÷�����
                    if (Neusoft.FrameWork.Function.NConvert.ToBoolean(neuSpread1_Sheet1.Cells[i, (int)ColumnSet.IsCompound].Value))
                    {
                        if (this.constantManager.InsertItem(this.compoundUsageTypeCode, cons) == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���淢������" + this.constantManager.Err));
                            return -1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                if (e.Message == "����ظ�,������¼��")
                {
                    MessageBox.Show(Language.Msg("����ظ�,������¼��"));
                }
                else
                {
                    MessageBox.Show(Language.Msg("���ݱ���ʧ�ܣ�" + e.Message), "ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return -1;
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ɹ�!"));

            this.QueryConsData(this.operTypeCode);

            this.SetCol();

            this.isDirty = false;

            return 0;
        }

        private void fpSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == (int)ColumnSet.ColName)
            {
                Neusoft.HISFC.Models.Base.Spell spCode = new Neusoft.HISFC.Models.Base.Spell();

                spCode = (Neusoft.HISFC.Models.Base.Spell)this.spellManager.Get(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColName].Text.ToString());

                if (spCode == null)
                {
                    return;
                }
                if (spCode.SpellCode == null || spCode.SpellCode == "")
                {
                    return;
                }

                this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColSpellCode].Value = spCode.SpellCode;
                this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColWBCode].Value = spCode.WBCode;
            }
        }

        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.isDirty = true;
        }

        private void fpSpread1_ComboSelChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)ColumnSet.ColSysType)
            {
                //ȡ��ѡ���ϵͳ����

            }
        }

        #region IMaintenanceControlable ��Ա

        private bool isDirty = false;

        /// <summary>
        /// ���� 
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            this.isDirty = true;

            int index = this.neuSpread1_Sheet1.RowCount;

            this.neuSpread1_Sheet1.Rows.Add(index, 1);

            this.neuSpread1_Sheet1.Cells[index - 1, (int)ColumnSet.ColID].Locked = false;
            this.neuSpread1_Sheet1.Cells[index - 1, (int)ColumnSet.ColFlag].Text = "New";
            this.neuSpread1_Sheet1.Cells[index - 1, (int)ColumnSet.ColSort].Value = index++;
            this.neuSpread1_Sheet1.Cells[index - 1, (int)ColumnSet.ColOperCode].Text = this.constantManager.Operator.ID.ToString();
            this.neuSpread1_Sheet1.Cells[index - 1, (int)ColumnSet.ColValid].Value = true;

            this.neuSpread1_Sheet1.SetActiveCell(neuSpread1_Sheet1.Rows.Count - 1, 0);

            return 0;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public int Del()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return 0;
            }

            this.isDirty = true;

            if (MessageBox.Show(Language.Msg("ɾ����������"), "ɾ��", System.Windows.Forms.MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int index = this.neuSpread1_Sheet1.ActiveRowIndex;

                string code = this.neuSpread1_Sheet1.Cells[index, (int)ColumnSet.ColID].Text.Trim();
                string name = this.neuSpread1_Sheet1.Cells[index, (int)ColumnSet.ColName].Text.Trim();
                string ifNew = this.neuSpread1_Sheet1.Cells[index, (int)ColumnSet.ColFlag].Text.Trim();

                int flag = this.constantManager.CanDeleteCons(this.operTypeCode, code);

                switch (flag)
                {
                    case 1:
                        if (ifNew == "Old")
                        {
                            MessageBox.Show(Language.Msg(name + "������ɾ���޸�!"));
                            return 0;
                        }
                        break;
                    case 0:
                        if (flag == 0)
                        {
                            MessageBox.Show(Language.Msg(name + "�����ݿ����Ѿ����ڲ���ɾ��"));
                            return 0;
                        }
                        break;
                    case 2:
                        if (flag == 2)
                        {
                            MessageBox.Show(Language.Msg("������Ŀ������ɾ��!"));
                            return 0;
                        }
                        break;
                }

                if (ifNew != "New")
                {
                    if (this.constantManager.DelConstant(this.operTypeCode, code) == -1)
                    {
                        MessageBox.Show(Language.Msg("ɾ����Ŀʧ��!") + this.constantManager.Err);
                        return -1;
                    }
                }

                this.neuSpread1_Sheet1.Rows[index].Remove();
            }

            return 0;
        }

        public int Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Delete()
        {
            return this.Del();
        }

        public int Export()
        {
            if (this.neuSpread1.Export() != -1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }

            return 1;
        }

        int Neusoft.FrameWork.WinForms.Forms.IMaintenanceControlable.Import()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Init()
        {
            if (this.InitData() == -1)
            {
                return -1;
            }

            this.QueryConsData(this.operTypeCode);

            return 0;
        }

        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }

        public int Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int NextRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PreRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            // TODO:  ��� ConstantManager.Print ʵ��
            return 1;
        }

        public int PrintConfig()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Query()
        {
            this.QueryConsData(this.operTypeCode);

            return 1;
        }

        public Neusoft.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
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

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ����
            /// </summary>
            ColID,
            /// <summary>
            /// ����
            /// </summary>
            ColName,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpellCode,
            /// <summary>
            /// �����
            /// </summary>
            ColWBCode,

            /// <summary>
            /// �Ƿ����������÷�
            /// </summary>
            IsCompound,

            /// <summary>
            /// ϵͳ���
            /// </summary>
            ColSysType,
            /// <summary>
            /// ��Ч��
            /// </summary>
            ColValid,
            /// <summary>
            /// ���к�
            /// </summary>
            ColSort,
            /// <summary>
            /// ����Ա
            /// </summary>
            ColOperCode,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            ColOperTime,
            /// <summary>
            /// ��־
            /// </summary>
            ColFlag
        }
    }
}
