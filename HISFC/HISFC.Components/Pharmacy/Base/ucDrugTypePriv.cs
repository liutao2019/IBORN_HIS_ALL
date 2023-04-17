using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ҩƷ����ҩȨ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-01]<br></br>
    /// </summary>
    public partial class ucDrugTypePriv : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugTypePriv()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ҩƷ�����Ƽ���
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա�б�
        /// </summary>
        private System.Collections.ArrayList alPerson = new System.Collections.ArrayList();       

        /// <summary>
        /// ҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����","�����¿�ҩȨ����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ����ǰ��ҩȨ����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);            
           
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.NewPriv();
            }
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelConstantList();
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            System.Collections.ArrayList alTypelist = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alTypelist == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҩƷ�б�ʧ��") + consManager.Err);
                return;
            }

            this.itemTypeHelper = new FS.FrameWork.Public.ObjectHelper(alTypelist);
            FarPoint.Win.Spread.CellType.ComboBoxCellType cmbCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            string[] strItemType = new string[alTypelist.Count];
            for (int i = 0; i < strItemType.Length; i++)
            {
                FS.HISFC.Models.Base.Const tempCons = alTypelist[i] as FS.HISFC.Models.Base.Const;
                strItemType[i] = "<" + tempCons.ID + ">" + tempCons.Name;
            }
            cmbCellType.Items = strItemType;
            this.neuSpread1_Sheet1.Columns[0].CellType = cmbCellType;

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            this.alPerson = personManager.GetEmployeeAll();
            if (this.alPerson == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ա�б�ʧ��") + personManager.Err);
                return;
            }

        }

        /// <summary>
        /// ��������
        /// </summary>
        protected void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;            
        }

        /// <summary>
        /// �����¿�ҩȨ�޷���
        /// </summary>
        protected void NewPriv()
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

            this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.Rows.Count - 1, 0);
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <returns></returns>
        protected int ShowConstantList()
        {
            List<FS.HISFC.Models.Pharmacy.DrugConstant> drugConstantList = phaConsManager.QueryDrugConstant(Function.DrugTypePriv_ConsType);
            if (drugConstantList == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҩƷ�������������") + this.phaConsManager.Err);
                return -1;
            }

            this.Clear();
            this.neuSpread1_Sheet1.Rows.Count = drugConstantList.Count;
            int iRowIndex = 0;
            foreach (FS.HISFC.Models.Pharmacy.DrugConstant info in drugConstantList)
            {
                this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = "<" + info.DrugType + ">" + this.itemTypeHelper.GetName(info.DrugType);        //ҩƷ���
                this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text = info.Item.ID;
                this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = info.Item.Name;
                this.neuSpread1_Sheet1.Cells[iRowIndex, 3].Text = info.Memo;            //��ע

                this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = info;

                iRowIndex++;
            }

            return 1;
        }

        /// <summary>
        /// ɾ��ҩƷ������Ϣ
        /// </summary>
        /// <returns></returns>
        protected int DelConstantList()
        {
            if (this.neuSpread1_Sheet1.Rows.Count < 0)
            {
                return 1;
            }

            //ɾ��������ʾ by Sunjh 2010-8-25 {375E3D5C-F4B3-43bf-9908-CEE1C78BC5F2}
            if (MessageBox.Show("ȷ��ɾ��������¼��?", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return 1;
            }

            if (this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag != null)
            {
                FS.HISFC.Models.Pharmacy.DrugConstant info = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Pharmacy.DrugConstant;
                if (info != null)
                {
                    if (this.phaConsManager.DeleteDrugConstant(info) == -1)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��ҩƷ������Ϣʧ��"));
                        return -1;
                    }

                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ���ɹ�"));
                }
            }

            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);

            return 1;
        }

        /// <summary>
        /// ��Fp�ڻ�ȡ����������Ϣ
        /// </summary>
        /// <param name="iRowIndex"></param>
        /// <returns></returns>
        protected FS.HISFC.Models.Pharmacy.DrugConstant GetDrugConstant(int iRowIndex)
        {
            FS.HISFC.Models.Pharmacy.DrugConstant info = new FS.HISFC.Models.Pharmacy.DrugConstant();
            info.ConsType = Function.DrugTypePriv_ConsType;
            info.DrugType = this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text.Substring(1, 1);
            info.Dept.ID = "0";
            info.Item.ID = this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text;
            info.Item.Name = this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text;
            info.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar( this.neuSpread1_Sheet1.Cells[iRowIndex, 3].Text );
            
            return info;
        }

        protected override int OnSave(object sender, object neuObject)
        {

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 0].Text) == true)
                {
                    MessageBox.Show("��ѡ����Ȩ��ҩƷ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 1].Text) == true)
                {
                    MessageBox.Show("��ѡ����Ȩ��Ա", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.phaConsManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //���ݳ������ɾ��ԭά������Ϣ
            if (this.phaConsManager.DeleteDrugConstant(Function.DrugTypePriv_ConsType) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ�����������Ϣ��������") + this.phaConsManager.Err);
                return -1;
            }
            DateTime sysTime = this.phaConsManager.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Pharmacy.DrugConstant info = new FS.HISFC.Models.Pharmacy.DrugConstant();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                info = this.GetDrugConstant(i);
                if (info == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��FarPoint�ڻ�ȡ���ݷ�������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                info.Oper.ID = this.phaConsManager.Operator.ID;
                info.Oper.OperTime = sysTime;
                if (info != null)
                {
                    if (this.phaConsManager.InsertDrugConstant(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("���볣�������Ϣ��������") + this.phaConsManager.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));

            return base.OnSave(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.ShowConstantList();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.ShowConstantList();
            }

            base.OnLoad(e);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 1)
            {
                FS.FrameWork.Models.NeuObject person = new FS.FrameWork.Models.NeuObject(); 
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alPerson, ref person) == 1)
                {
                    #region {75C20D74-9B76-474a-9069-15FF2EB438C3}
                    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim() == this.neuSpread1_Sheet1.Cells[e.Row, 0].Text.Trim())
                        {
                            if (this.neuSpread1_Sheet1.Cells[i, 1].Text.Trim() == person.ID)
                            {
                                MessageBox.Show("����Ա�Ѵ��ڣ�������ѡ����Ȩ��Ա", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.neuSpread1_Sheet1.ActiveRowIndex = i;
                                this.neuSpread1_Sheet1.ActiveColumnIndex = e.Column;
                                return;
                            }

                        }
                    } 
                    #endregion
                    this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text = person.ID;
                    this.neuSpread1_Sheet1.Cells[e.Row, 2].Text = person.Name;
                }
            }
        }
    }
}
