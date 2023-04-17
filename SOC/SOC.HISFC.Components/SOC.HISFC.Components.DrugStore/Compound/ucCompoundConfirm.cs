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

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// <br></br>
    /// [��������: ���ù���ȷ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// <˵��>
    ///     1��
    /// </˵��>
    /// </summary>
    public partial class ucCompoundConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompoundConfirm()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ��׼��Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��׼����
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �������ѡ����
        /// </summary>
        private tvCompoundList tvCompound = null;

        /// <summary>
        /// ҽ��������Ϣ
        /// </summary>
        private string groupCode = "U";

        #endregion

        #region ����

        /// <summary>
        /// ��׼����
        /// </summary>
        public FS.FrameWork.Models.NeuObject ApproveDept
        {
            get
            {
                return this.approveDept;
            }
            set
            {
                this.approveDept = value;
            }
        }

        /// <summary>
        /// ��ѡ���ҽ������
        /// </summary>
        private string GroupCode
        {
            get
            {
                this.groupCode = "U";

                return this.groupCode;
            }
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
            this.toolBarService.AddToolButton("ȫѡ", "ѡ��ȫ������", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȫ��ѡ", "ȡ��ȫ������ѡ��", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("ˢ��", "ˢ�»����б���ʾ", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveApply();

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
                case "ȫѡ":
                    this.Check(true);
                    break;
                case "ȫ��ѡ":
                    this.Check(false);
                    break;
                case "ˢ��":
                    this.ShowList();
                    break;
            }

        }

        protected override int OnPrint(object sender, object neuObject)
        {
            //ArrayList alCheck = this.GetCheckData();

            //Function.PrintCompound(alCheck);

            return base.OnPrint(sender, neuObject);
        }

        #endregion

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ػ�������.���Ժ�..."));
            Application.DoEvents();

            if (this.tv != null)
            {
                this.tvCompound = this.tv as tvCompoundList;

                this.tvCompound.Init();
                this.tvCompound.State = "2";
            }

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            this.approveOper = dataManager.Operator;

            this.approveDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            if (this.tvCompound != null)
            {
                this.tvCompound.SelectDataEvent += new tvCompoundList.SelectDataHandler(tvCompound_SelectDataEvent);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected int Clear()
        {
            this.fpApply_Sheet1.Rows.Count = 0;

            return 1;
        }

        /// <summary>
        /// �б���ʾ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected virtual int ShowList()
        {
            this.Clear();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ�����������,���Ժ�..."));
            Application.DoEvents();

            //���ݿ��ҩ��/���λ�ȡ�б�
            SOC.HISFC.BizLogic.Pharmacy.Compound compoundMgr = new SOC.HISFC.BizLogic.Pharmacy.Compound();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alList = compoundMgr.QueryCompoundList(this.ApproveDept.ID, "2", false);
            if (alList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("��ȡ���õ��б�������") + compoundMgr.Err);
                return -1;
            }

            this.tvCompound.ShowList(alList);

            //���ù��˰�ťĬ����ʾȫ��
            this.ReSetToolBarFilterToDefault();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// ���ð�ťĬ��Ϊ��ѯȫ��
        /// </summary>
        private void ReSetToolBarFilterToDefault()
        {
            if (this.toolBarService.GetToolButton("��ͨ����") != null)
            {
                this.toolBarService.GetToolButton("��ͨ����").Text = "��ͨ����";
            }
            if (this.toolBarService.GetToolButton("��������") != null)
            {
                this.toolBarService.GetToolButton("��������").Text = "��ͨ����";
            }
            if (this.toolBarService.GetToolButton("����") != null)
            {
                this.toolBarService.GetToolButton("����").Text = "��ͨ����";
            }
            if (this.toolBarService.GetToolButton("ȫ��") != null)
            {
                this.toolBarService.GetToolButton("ȫ��").Text = "��ͨ����";
            }
        }

        /// <summary>
        /// ����������ˮ�ż���
        /// </summary>
        /// <param name="compoundGroup">������ˮ��</param>
        protected void QueryCompound(string compoundGroup)
        {
            if (compoundGroup == null || compoundGroup == "")
            {
                return;
            }

            SOC.HISFC.BizLogic.Pharmacy.Compound compoundMgr = new SOC.HISFC.BizLogic.Pharmacy.Compound();
            ArrayList alList = compoundMgr.QueryCompoundApplyOut(compoundGroup);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("����������ˮ�Ż�ȡ�������ݷ�������") + compoundMgr.Err);
                return;
            }

            this.AddDataToFp(alList);
        }

        /// <summary>
        /// ��Fp�ڼ�������
        /// </summary>
        /// <param name="alApply">��ҩ������Ϣ</param>
        /// <returns></returns>
        protected int AddDataToFp(ArrayList alApply)
        {
            this.fpApply_Sheet1.Rows.Count = 0;

            int i = 0;

            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alApply)
            {
                this.fpApply_Sheet1.Rows.Add(i, 1);

                if (info.UseTime != System.DateTime.MinValue)
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUseTime].Text = info.UseTime.ToString();
                }

                if (info.User01.Length > 4)
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColBedName].Text = "[" + info.User01.Substring(4) + "]" + info.User02;
                }
                else
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColBedName].Text = "[" + info.User01 + "]" + info.User02;
                }

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = true;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColTradeNameSpecs].Text = info.Item.Name + "[" + info.Item.Specs + "]";
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColRetailPrice].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseOnce].Text = info.DoseOnce.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseUnit].Text = info.Item.DoseUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColQty].Text = (info.Operation.ApplyQty * info.Days).ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUnit].Text = info.Item.MinUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColFrequency].Text = info.Frequency.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text = info.Usage.Name;

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoctor].Text = info.RecipeInfo.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColApplyTime].Text = info.Operation.ApplyOper.OperTime.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text = info.CompoundGroup;
                this.fpApply_Sheet1.Rows[i].Tag = info;
            }

            return 1;
        }

        /// <summary>
        /// ��ȡ���е�ǰѡ�е�����
        /// </summary>
        /// <returns></returns>
        protected ArrayList GetCheckData()
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
            {
                if (NConvert.ToBoolean(this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value))
                {
                    al.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                }
            }

            return al;
        }

        /// <summary>
        /// ѡ��/��ѡ��
        /// </summary>
        /// <param name="isCheck"></param>
        /// <returns></returns>
        public int Check(bool isCheck)
        {
            for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
            {
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = isCheck;
            }

            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public virtual int SaveApply()
        {
            ArrayList alCheck = this.GetCheckData();
            if (alCheck.Count == 0)
            {
                return 0;
            }

            SOC.HISFC.BizLogic.Pharmacy.Compound compoundMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

            DateTime sysTime = compoundMgr.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            compoundMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //��ʱ������ҩ������

            FS.HISFC.Models.Base.OperEnvironment compoundOper = new FS.HISFC.Models.Base.OperEnvironment();
            compoundOper.OperTime = sysTime;
            compoundOper.ID = this.approveOper.ID;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alCheck)
            {
                if (compoundMgr.UpdateCompoundApplyOut(info, compoundOper, true) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("��������ȷ����Ϣ��������") + compoundMgr.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ɹ�"));

            this.ShowList();

            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                if (this.Init() == -1)
                {
                    MessageBox.Show(Language.Msg("��ʼ��ִ�з�������"));
                    return;
                }

                this.ShowList();
            }

            base.OnLoad(e);
        }

        private void tvCompound_SelectDataEvent(ArrayList alData)
        {
            this.AddDataToFp(alData);
        }

        private void cmbOrderGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvCompound.GroupCode = this.GroupCode;

            this.ShowList();
        }

        private void neuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Clear();

                this.QueryCompound(this.txtCompoundGroup.Text);
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ���� ����
            /// </summary>
            ColBedName,
            /// <summary>
            /// ѡ��
            /// </summary>
            ColSelect,
            /// <summary>
            /// ҩƷ���� ���
            /// </summary>
            ColTradeNameSpecs,
            /// <summary>
            /// ���ۼ�
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ����
            /// </summary>
            ColDoseOnce,
            /// <summary>
            /// ������λ
            /// </summary>
            ColDoseUnit,
            /// <summary>
            /// ����
            /// </summary>
            ColQty,
            /// <summary>
            /// ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// Ƶ��
            /// </summary>
            ColFrequency,
            /// <summary>
            /// �÷�
            /// </summary>
            ColUsage,
            /// <summary>
            /// ��ҩʱ��
            /// </summary>
            ColUseTime,
            /// <summary>
            /// ����ҽ��
            /// </summary>
            ColDoctor,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            ColApplyTime,
            /// <summary>
            /// ���κ�
            /// </summary>
            ColCompoundGroup
        }
    }
}
