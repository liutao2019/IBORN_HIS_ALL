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

namespace FS.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// <br></br>
    /// [��������: ���ù���������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// <˵��>
    ///     1����ֲ�����Һ����
    ///     2��ʵ�ִ�ӡ��ǩ��ȷ�Ϸ�ҩ����
    ///     3����Һ�����㷨�ӿ�ʵ��
    ///     4. �������ϱ�ǩ��ӡ���������κţ�����ҩƷ��������ʱ���ڹ��ˡ���Һ��ǩ��Ƭ��ʾ����ǩ����ȹ���
    /// </˵��>
    /// <�޸�˵��>
    ///   �޸�ʵ���ˣ���������ú����볬{432F8D1A-80F9-45e1-9FE6-7332C49487BA}
    /// </�޸�˵��>
    /// </summary>
    public partial class ucCompoundManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup
    {
        public ucCompoundManager()
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
        /// ҩƷҵ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩƷ��������ҵ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// ҽ����������ҵ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// �������ѡ����
        /// </summary>
        private tvCompoundList tvCompound = null;

        /// <summary>
        /// ҽ��������Ϣ
        /// </summary>
        private string groupCode = "U";
        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private string drugCode = "ALL";
        /// <summary>
        /// �Ƿ�������ȷ��
        /// </summary>
        private bool isNeedConfirm = true;

        /// <summary>
        /// ҽ�����ͼ���
        /// </summary>
        private System.Collections.Hashtable hsOrderType = new Hashtable();
        /// <summary>
        /// Ƶ������
        /// </summary>
        ArrayList alFrequency = null;
        /// <summary>
        /// ҩ����������
        /// </summary>
        ArrayList alPharmcyFunction = null;
        /// <summary>
        /// �洢����
        /// </summary>
        Hashtable hsTable = null;

        /// <summary>
        /// �Ƿ���ʾ��Ƭ��ʽ
        /// </summary>
        private bool isShowCardStyle = false;

        /// <summary>
        /// �Ƿ���ʾ�б���ʽ
        /// </summary>
        private bool isShowListStyle = false;

        /// <summary>
        /// �����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ���ڴ洢��ҩƷ����ʱ�������в�������
        /// </summary>
        ArrayList alThisParList = new ArrayList();

        /// <summary>
        /// Ƿ����ʾ����
        /// </summary>
        private FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.Y;

        /// <summary>
        /// ��ǰ���ڹ�����
        /// </summary>
        private WinFun currentWinFun = WinFun.Print;

        /// <summary>
        /// �Ƿ������������ True ���� False ������
        /// </summary>
        private bool isManagerUnvalidData = false;
        /// <summary>
        /// �Ƿ����ϱ�ǩ��ӡ
        /// </summary>
        private bool cancelLablePrint = false;

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
                if (this.cmbOrderGroup.Text == "" || this.cmbOrderGroup.Text == null || this.cmbOrderGroup.Text == "ȫ��")
                {
                    this.groupCode = "U";
                }
                else
                {
                    this.groupCode = this.cmbOrderGroup.Text;
                }

                return this.groupCode;
            }
        }

        /// <summary>
        /// ��ѡ���ҩƷ
        /// </summary>
        private string DrugCode
        {
            get
            {
                if (this.cmbParmacy.Text == "" || this.cmbParmacy.Tag == null)
                {
                    this.drugCode = "ALL";
                }
                else
                {
                    this.drugCode = this.cmbParmacy.Tag.ToString();
                }

                return this.drugCode;
            }
        }

        /// <summary>
        /// ���μ������ʱ��
        /// </summary>
        private DateTime MaxDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMaxDate.Text);
            }
        }

        /// <summary>
        /// ���μ�����Сʱ��
        /// </summary>
        private DateTime MinDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMinDate.Text);
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��Ƭ��ʽ
        /// </summary>
        [Description("�Ƿ���ʾ��Ƭ��ʽ"), Category("����"), DefaultValue(false)]
        public bool IsShowCardStyle
        {
            get
            {
                return isShowCardStyle;
            }
            set
            {
                isShowCardStyle = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�б���ʽ
        /// </summary>
        [Description("�Ƿ���ʾ�б���ʽ"), Category("����"), DefaultValue(false)]
        public bool IsShowListStyle
        {
            get
            {
                return isShowListStyle;
            }
            set
            {
                isShowListStyle = value;
            }
        }

        /// <summary>
        /// �Ƿ��ж�Ƿ�ѣ�Ƿ���Ƿ���ʾ
        /// </summary>
        [Description("����Ƿ����ʾ���� Y ֻ��ʾǷ�� �������շ� M ��ʾǷ�� ���������շ� N ���ж�Ƿ��"), Category("����")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            set
            {
                messType = value;
            }
            get
            {
                return messType;
            }
        }

        /// <summary>
        /// ��ǰ���ڹ����� Print ��ǰ������Ϊ���ݴ�ӡ  Save ��ǰ������Ϊ���档
        /// </summary>
        [Description("��ǰ���ڹ����� Print ��ǰ������Ϊ���ݴ�ӡ  Save ��ǰ������Ϊ����"), Category("����")]
        public WinFun CurrentWinFun
        {
            get
            {
                return this.currentWinFun;
            }
            set
            {
                this.currentWinFun = value;
            }
        }

        /// <summary>
        /// �Ƿ������������ True ���� False ������
        /// </summary>
        [Description("�Ƿ������������ True ���� False ������ �����Խ���CurrentWinFun����ΪSaveʱ��Ч"), Category("����")]
        public bool IsManagerUnValidData
        {
            get
            {
                return this.isManagerUnvalidData;
            }
            set
            {
                this.isManagerUnvalidData = value;
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

            this.toolBarService.AddToolButton("��������", "���ϵ�ǰѡ�������ҩƷ������Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Mģ��ɾ��, true, false, null);

            this.toolBarService.AddToolButton("���ϱ�ǩ��ӡ", "��ӡ�����ϵı�ǩ�����±�־", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);

            this.toolBarService.AddToolButton("���ϱ�ǩ����", "��ӡ�Ѵ�ӡ�������ϱ�ǩ", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);

            this.toolBarService.AddToolButton("��������", "�����µ�����", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);

            this.toolBarService.AddToolButton("��ʱ", "���˳���ҽ��", FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��, true, false, null);


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
            //��Һ���ļ��ݿ�Ƭ������ʽby Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (this.IsShowCardStyle)
            {
                this.SynCheckedStates();
            }

            this.SaveApply();
            this.pnlCardCollections.Controls.Clear();

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
                case "��������":
                    this.CancelApplyout();
                    break;
                case "���ϱ�ǩ��ӡ":
                    this.PrintCancelDate();
                    break;
                case "��������":
                    this.ModifyCompandGroup();
                    break;
                case "���ϱ�ǩ����":
                    this.CancelLabelRePrint();
                    break;
                case "��ʱ":
                    this.Filter("short");
                    break;
                case "����":
                    this.Filter("long");
                    break;
                case "ȫ��":
                    this.Filter("all");
                    break;
            }

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int Filter(string tag)
        {
            ArrayList list;
            if (tag == "short")
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    if ((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).OrderType.ID.ToString() == "LZ")
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = true;
                    }
                    else
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = false;
                    }
                }
                if (this.IsShowCardStyle)
                {
                    list = new ArrayList();
                    for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                    {
                        if (this.fpApply_Sheet1.Rows[i].Visible)
                        {
                            list.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                        }
                    }
                    this.SetCardData(list);
                }
                this.toolBarService.GetToolButton("��ʱ").Text = "����";
            }
            else if (tag == "long")
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    if ((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).OrderType.ID.ToString() == "CZ")
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = true;
                    }
                    else
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = false;
                    }
                }
                if (this.IsShowCardStyle)
                {
                    list = new ArrayList();
                    for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                    {
                        if (this.fpApply_Sheet1.Rows[i].Visible)
                        {
                            list.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                        }
                    }
                    this.SetCardData(list);
                }
                this.toolBarService.GetToolButton("����").Text = "ȫ��";
            }
            else
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    this.fpApply_Sheet1.Rows[i].Visible = true;
                }
                if (this.IsShowCardStyle)
                {
                    list = new ArrayList();
                    for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                    {
                        if (this.fpApply_Sheet1.Rows[i].Visible)
                        {
                            list.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                        }
                    }
                    this.SetCardData(list);
                }
                this.toolBarService.GetToolButton("ȫ��").Text = "��ʱ";
            }
            return 1;
        }
        /// <summary>
        /// ���ϱ�ǩ����
        /// </summary>
        /// <returns></returns>
        private int CancelLabelRePrint()
        {
            if (!this.rbCancel.Checked || this.fpApply_Sheet1.Rows.Count <= 0)
            {
                return -1;
            }
            //��ȡѡ������
            ArrayList alCheck = this.GetCheckData();
            this.neuTabControl1.SelectedTab = this.tpCardStyle;
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        if (ckk.Checked)
                        {
                            ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                            ucc.IsUnvalidTitle = true;          //���ϱ�����ʾ
                            if (ucc.Tag is ArrayList)
                            {
                                alCheck.AddRange(ucc.Tag as ArrayList);
                            }
                            ucc.Print();
                        }
                    }
                }
            }
            this.ShowList();
            return 1;
        }
        /// <summary>
        /// �����µ����κ�
        /// </summary>
        /// <returns></returns>
        private int ModifyCompandGroup()
        {
            ArrayList alOriginalData = this.GetCheckData();
            if (alOriginalData.Count == 0)
            {
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //��Ϊ�����ֹ����Ĺ����κţ����Ա����ʱ�����¸���һ��������Ϣ
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alOriginalData)
            {
                if (itemManager.UpdateCompoundGroup(info.ID, info.CompoundGroup) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("�������γ���") + itemManager.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�������κųɹ�!");
            this.tvCompound.RefreshData();
            return 1;
        }
        /// <summary>
        /// ��ӡ���ϱ�ǩ����ȷ�ϱ�ʶ
        /// </summary>
        /// <returns></returns>
        private int PrintCancelDate()
        {
            ArrayList al = new ArrayList();
            //ֻ������Ч����
            for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut appTemp = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                appTemp.Item = this.itemManager.GetItem(appTemp.Item.ID);
                appTemp.CompoundGroup = this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text;
                if (appTemp.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid && appTemp.ExtFlag != "1"
                    && (bool)this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value)
                {
                    al.Add(appTemp);
                }
            }
            if (al.Count == 0)
            {
                MessageBox.Show("���������ݣ�");
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //���´�����Ч���� ͨ��������չ���Ϊ��1�� ��ʶ����Ҫ���д��� ���ٴμ���ʱ������������ʾ
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut unvalidData in al)
            {
                unvalidData.ExtFlag = "1";
                string str = "";  //��Ϊ���ĸ���ȡ�����ڵ�ʱ���õ���User03 ��������User03��������ã����Խ���һ��
                str = unvalidData.User03;
                unvalidData.User03 = unvalidData.UseTime.ToString();
                if (itemManager.UpdateApplyOut(unvalidData) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���������ݽ���ȷ�ϱ�ʶʱ��������") + itemManager.Err);
                    return -1;
                }
                unvalidData.User03 = str;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.cancelLablePrint = true;//���ϱ�ǩ��ӡ��ʱ�򲻴�ӡ������ϸ����ӡģʽ���ñ�ǩҳ��ӡ����
            this.Print(al);
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                if (this.IsShowCardStyle)
                {
                    for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                            if (ckk.Checked)
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ArrayList alCheck = this.GetCheckData();
            if (this.currentWinFun == WinFun.Print)         //������ӡ״̬�£��Ž��д�ӡ��Ǹ���
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //string drugBillNO = this.itemManager.GetNewDrugBillNO();
                string drugBillNO = string.Empty;

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alCheck)
                {
                    if (this.itemManager.UpdateApplyOutPrintState(info.ID, true) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���±�ǩ��ӡ���ʧ��" + this.itemManager.Err);
                        return -1;
                    }

                    if (string.IsNullOrEmpty(info.DrugNO) == true)
                    {
                        info.DrugNO = drugBillNO;
                        if (this.itemManager.ExamApplyOut(info) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���°�ҩ����ʧ��" + this.itemManager.Err);
                            return -1;
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            this.Print(alCheck);

            return base.OnPrint(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.tvCompound.RefreshData();

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region ����
        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ػ�������.���Ժ�..."));
            Application.DoEvents();

            this.tvCompound = this.tv as tvCompoundList;

            this.tvCompound.Init();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            this.approveOper = dataManager.Operator;

            this.approveDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            //this.InitOrderGroup();
            this.cmbOrderGroup.SelectedIndex = 0;
            DateTime date = dataManager.GetDateTimeFromSysDateTime();
            string str1 = date.Date.AddDays(1).ToShortDateString() + " " + "00:00:00";
            string str2 = date.Date.AddDays(1).ToShortDateString() + " " + "23:59:59";
            //this.dtMaxDate.Value = dataManager.GetDateTimeFromSysDateTime().Date.AddDays(1);
            this.dtMaxDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(str2);
            this.dtMinDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(str1);
            this.tvCompound.SelectDataEvent += new tvCompoundList.SelectDataHandler(tvCompound_SelectDataEvent);

            //ȡҽ�����ͣ����ڽ�����ת��������
            FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();
            ArrayList alOrderType = orderManager.GetList();
            foreach (FS.FrameWork.Models.NeuObject infoOrderType in alOrderType)
            {
                this.hsOrderType.Add(infoOrderType.ID, infoOrderType.Name);
            }
            //ҩƷ�б�
            List<FS.HISFC.Models.Pharmacy.Item> drugList = new List<FS.HISFC.Models.Pharmacy.Item>();
            drugList = itemManager.QueryItemList(false);
            if (drugList != null)
            {
                ArrayList list = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Item itemObj in drugList)
                {
                    list.Add(itemObj);
                }
                this.cmbParmacy.AddItems(list);
            }

            //��Һ���ļ��ݿ�Ƭ������ʽby Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (!this.IsShowCardStyle)
            {
                this.tpCardStyle.Hide();

                this.neuTabControl1.TabPages.Remove(this.tpCardStyle);
            }

            //��ʱ���� �����б�������ʾ��ʽ
            if (this.isShowListStyle == false)
            {
                this.neuTabControl1.TabPages.Remove(this.tpListStyle);
            }

            this.fpApply_Sheet1.Columns[(int)ColumnSet.ColCompoundGroup].Locked = false;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// ��ʼ��ҽ��������Ϣ
        /// </summary>
        /// <returns></returns>
        private int InitOrderGroup()
        {
            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

            List<FS.HISFC.Models.Pharmacy.OrderGroup> orderGroupList = consManager.QueryOrderGroup();
            if (orderGroupList == null)
            {
                MessageBox.Show(Language.Msg("��ȡҽ��������Ϣ��������"));
                return -1;
            }

            string[] strOrderGroup = new string[orderGroupList.Count + 1];
            strOrderGroup[0] = "ȫ��";
            int i = 1;
            foreach (FS.HISFC.Models.Pharmacy.OrderGroup info in orderGroupList)
            {
                strOrderGroup[i] = info.ID;
                i++;
            }

            this.cmbOrderGroup.Items.AddRange(strOrderGroup);

            string orderGroup = consManager.GetOrderGroup(consManager.GetDateTimeFromSysDateTime());
            if (orderGroup != "")
            {
                this.cmbOrderGroup.Text = orderGroup;
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected int Clear()
        {
            this.fpApply_Sheet1.Rows.Count = 0;

            this.pnlCardCollections.Controls.Clear();

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
            //�������ҩƷ�����򲻷ֲ������������в�������������
            if (this.DrugCode != "ALL")
            {
                this.tvCompound.ShowListByParmacy(this.ApproveDept.ID, ref this.alThisParList);
            }
            else
            {
                //���ݿ��ҩ��/���λ�ȡ�б�
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

                List<FS.HISFC.Models.Pharmacy.ApplyOut> alList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                if (this.currentWinFun == WinFun.Print)     //��ӡ����
                {
                    alList = itemManager.QueryCompoundList(this.ApproveDept.ID, this.GroupCode, "0", "ALL", true);
                }
                else
                {
                    //��ʱ�ɰ�����Ч���� �Դ�ӡ������
                    alList = itemManager.QueryCompoundList(this.ApproveDept.ID, this.GroupCode, "0", "ALL", false);
                }
                if (alList == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(Language.Msg("��ȡ���õ��б�������") + itemManager.Err);
                    return -1;
                }

                this.tvCompound.ShowList(alList);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// ��Fp�ڼ�������
        /// </summary>
        /// <param name="alApply">��ҩ������Ϣ</param>
        /// <returns></returns>
        protected int AddDataToFp(ArrayList alData)
        {
            ArrayList alApply = new ArrayList();
            //�����������
            ComboSort sort = new ComboSort();
            alData.Sort(sort);

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut tempApplyOut in alData)
            {
                if (this.rbCancel.Checked && this.currentWinFun == WinFun.Save)
                {
                    if (tempApplyOut.PrintState == "1" && tempApplyOut.ExtFlag == "1" && tempApplyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        alApply.Add(tempApplyOut);
                    }
                    continue;
                }
                if (tempApplyOut.ExtFlag == "1" && tempApplyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)            //�˱�ʶ��ʽ�����ϵ��ݵ������ȷ�ϲ��������� ��������ʾ
                {
                    continue;
                }

                if (this.isManagerUnvalidData == false)      //�����������ݲ����в���
                {
                    if (tempApplyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)  //��Ч����
                    {
                        continue;
                    }
                }
                else                                         //�ɶ��������ݽ��в���
                {
                    if (this.currentWinFun == WinFun.Print) //��ʹ��һ���ж��������� ��ǰ���ڹ���Ϊ��ӡʱ �����������ݽ��в���
                    {
                        continue;
                    }
                }

                if (this.rbPrinting.Checked == true)        //����δ��ӡ����
                {
                    if (tempApplyOut.PrintState == "0" || string.IsNullOrEmpty(tempApplyOut.PrintState) == true)
                    {
                        alApply.Add(tempApplyOut);
                    }
                }
                else if (this.rbPrinted.Checked == true)    //�����Ѵ�ӡ����
                {
                    if (tempApplyOut.PrintState == "1")
                    {
                        alApply.Add(tempApplyOut);
                    }
                }
                else                                       //����ȫ������
                {
                    alApply.Add(tempApplyOut);
                }
            }
            //������Һ���� ��Ƭ��ʽ��ʾ
            this.SetCardData(alApply);

            this.fpApply_Sheet1.Rows.Count = 0;

            int i = 0;

            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alApply)
            {
                if (info.UseTime > this.MaxDate || info.UseTime < this.MinDate)
                {
                    continue;
                }
                this.fpApply_Sheet1.Rows.Add(i, 1);

                if (info.PrintState == "1")     //�Ѵ�ӡ��Ŀ
                {
                    this.fpApply_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.fpApply_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Black;
                }

                //������������ ����ɫ��ʾ��ɫ
                if (info.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    this.fpApply_Sheet1.Rows[i].BackColor = System.Drawing.Color.Gray;
                }
                else
                {
                    this.fpApply_Sheet1.Rows[i].BackColor = System.Drawing.Color.White;
                }

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

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = false;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColTradeNameSpecs].Text = info.Item.Name + "[" + info.Item.Specs + "]";
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColRetailPrice].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseOnce].Text = info.DoseOnce.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseUnit].Text = info.Item.DoseUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColQty].Text = (info.Operation.ApplyQty * info.Days).ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUnit].Text = info.Item.MinUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColFrequency].Text = info.Frequency.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text = info.Usage.Name;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCombNo].Text = info.CombNO + info.UseTime.ToString();
                if (info.OrderType == null || info.OrderType.ID == "")
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = "ֱ���շ�";
                }
                else
                {
                    if (this.hsOrderType.ContainsKey(info.OrderType.ID))
                    {
                        this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = this.hsOrderType[info.OrderType.ID].ToString();
                    }
                    else
                    {
                        this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = info.OrderType.ID;
                    }
                }

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoctor].Text = info.RecipeInfo.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColApplyTime].Text = info.Operation.ApplyOper.OperTime.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text = info.CompoundGroup;
                this.fpApply_Sheet1.Rows[i].Tag = info;
            }
            //�����
            FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.fpApply_Sheet1, (int)ColumnSet.ColCombNo, (int)ColumnSet.ColMoCombo);
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
                    FS.HISFC.Models.Pharmacy.ApplyOut appTemp = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    appTemp.Item = this.itemManager.GetItem(appTemp.Item.ID);
                    appTemp.CompoundGroup = this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text;
                    al.Add(appTemp);
                    //al.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);                    
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
                if (this.fpApply_Sheet1.Rows[i].Visible)  //ֻѡ��ɼ�����
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = isCheck;
                }
            }

            //��Һ���ļ��ݿ�Ƭ������ʽ by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (this.IsShowCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        ckk.Checked = isCheck;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public virtual int SaveApply()
        {
            ArrayList alOriginalData = this.GetCheckData();
            if (alOriginalData.Count == 0)
            {
                return 0;
            }

            ArrayList alUnValidData = new ArrayList();      //��Ч����

            ArrayList alCheck = new ArrayList();            //��Ч����
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alOriginalData)
            {
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    alCheck.Add(info);
                }
                else
                {
                    alUnValidData.Add(info);
                }
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڱ���,���Ժ�..."));
            Application.DoEvents();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //itemManager.SetTrans(t.Trans);

            //��ʱ������ҩ������

            if (Function.DrugConfirm(alCheck, null, null, this.approveDept.Clone(), FS.FrameWork.Management.PublicTrans.Trans) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }

            //if (Function.DrugApprove(alCheck, this.approveOper.ID, this.approveDept.ID, FS.FrameWork.Management.PublicTrans.Trans) == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    return -1;
            //}

            //�粻������ȷ�����ڴ�ֱ�ӽ���ȷ��
            if (!this.isNeedConfirm)
            {
                FS.HISFC.Models.Base.OperEnvironment compoundOper = new FS.HISFC.Models.Base.OperEnvironment();
                compoundOper.OperTime = sysTime;
                compoundOper.ID = this.approveOper.ID;

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alCheck)
                {
                    if (itemManager.UpdateCompoundApplyOut(info, compoundOper, true) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(Language.Msg("��������ȷ����Ϣ��������") + itemManager.Err);
                        return -1;
                    }
                }
            }

            #region ���ļƷ� �����ε���
            //if (Function.CompoundFeeByUsage(alCheck, this.approveDept, FS.FrameWork.Management.PublicTrans.Trans) == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    return -1;
            //} 
            #endregion

            //���´�����Ч���� ͨ��������չ���Ϊ��1�� ��ʶ����Ҫ���д��� ���ٴμ���ʱ������������ʾ
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut unvalidData in alUnValidData)
            {
                unvalidData.ExtFlag = "1";
                string str = "";  //��Ϊ���ĸ���ȡ�����ڵ�ʱ���õ���User03 ��������User03��������ã����Խ���һ��
                str = unvalidData.User03;
                unvalidData.User03 = unvalidData.UseTime.ToString();
                if (itemManager.UpdateApplyOut(unvalidData) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���������ݽ���ȷ�ϱ�ʶʱ��������") + itemManager.Err);
                    return -1;
                }
                unvalidData.User03 = str;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show(Language.Msg("����ɹ�"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Function.PrintCompound(alCheck,true,true);

            if (alUnValidData.Count > 0)
            {
                this.cancelLablePrint = true;
                this.Print(alUnValidData);  //�����ʱ��ֻ��ӡ��������
            }
            this.ShowList();

            return 1;
        }

        /// <summary>
        /// ����ҽ����Ϻ����Ϸ�ҩ�����ִ�е� by Sunjh 2010-12-8 {68126BF1-DCE3-4383-9F25-046E2E74C717}
        /// </summary>
        /// <returns></returns>
        public void CancelApplyout()
        {
            //1 ��ȡҽ����Ϻ�
            ArrayList alCancel = this.GetCheckData();
            //2 ���Ϸ�ҩ�����ִ�е�
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut appCancel in alCancel)
            {
                //���Ϸ�ҩ����
                if (this.pharmacyIntegrate.CancelApplyOut(appCancel.ExecNO) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ҩƷ����ʱ����!");
                    return;
                }
                //����ִ�е�
                if (this.orderIntegrate.DcExecImmediateUnNormal(appCancel.ExecNO, true, FS.FrameWork.Management.Connection.Operator) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ҽ��ִ�е�ʱ����!");
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����������Ϣ�ɹ�!");
            //3 ˢ�½����б�
            this.ShowList();
        }

        /// <summary>
        /// ��ӡִ��
        /// </summary>
        /// <param name="alPrintData"></param>
        public void Print(ArrayList alPrintData)
        {
            ArrayList alData = new ArrayList();

            alData = alPrintData;
            CompandGroupSort sort = new CompandGroupSort();
            alData.Sort(sort);  //�Ȱ������κ������ڴ���

            this.neuTabControl1.SelectedTab = this.tpCardStyle;//�б�Ϳ�Ƭ������һ����ӡ����

            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        if (ckk.Checked)
                        {
                            //count1++;
                            if (this.currentWinFun == WinFun.Save)      //��ǰΪ����״̬ �����������ݽ��д�ӡ
                            {
                                if (ckk.Text.IndexOf("[������]") == -1)   //��ǰΪ��Ч��ǩ�������д�ӡ
                                {
                                    continue;
                                }
                                else   //�����ϱ�ǩ���д�ӡ
                                {
                                    ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                    ucc.IsUnvalidTitle = true;          //���ϱ�����ʾ
                                    if (ucc.Tag is ArrayList)
                                    {
                                        alData.AddRange(ucc.Tag as ArrayList);
                                    }
                                    ucc.Print();
                                }
                            }
                            else
                            {
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (rbPrinted.Checked)
                                {
                                    ucc.IsPrintedTitle = true;
                                }
                                ucc.Print();

                            }
                        }
                    }
                }
            }
            else
            {
                Function.PrintCompound(alData, true, true);
            }

            if (alData.Count > 0 && !this.cancelLablePrint)
            {
                this.PrintDrugBills(alData);
            }

            this.ShowList();

        }



        private int SetCardData(ArrayList alData)
        {
            this.pnlCardCollections.Controls.Clear();
            ArrayList alDataClone = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut apply in alData)
            {
                alDataClone.Add(apply.Clone());
            }
            CompandGroupSort sort = new CompandGroupSort();
            alDataClone.Sort(sort);  //�Ȱ������κ������ڴ���
            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();
            string privUseTime = "";
            string privCombo = "-1";

            #region ��ǩ���� ����������ˮ��

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alDataClone)
            {
                if (info.UseTime > this.MaxDate || info.UseTime < this.MinDate)
                {
                    continue;
                }
                //��������ͬ����ʹ��ʱ����ͬ
                if ((privCombo == "-1" || (privCombo == info.CombNO && info.CombNO != ""))
                    && (privUseTime == "" || (privUseTime == info.UseTime.ToString() && info.UseTime.ToString() != "")))
                {
                    alCombo.Add(info.Clone());
                    privCombo = info.CombNO;
                    privUseTime = info.UseTime.ToString();
                    continue;
                }
                else			//��ͬ������
                {
                    alGroupApplyOut.Add(alCombo);

                    privCombo = info.CombNO;
                    privUseTime = info.UseTime.ToString();
                    alCombo = new ArrayList();
                    alCombo.Add(info.Clone());
                }
            }

            if (alCombo.Count > 0)
            {
                alGroupApplyOut.Add(alCombo);
            }

            #endregion

            bool val = false;
            int i = 0;
            int j = 0;
            foreach (ArrayList alTemp in alGroupApplyOut)
            {
                //{D8B142A6-A344-45a4-930E-30561148E056}feng.ch
                if (this.DrugCode != "ALL") //����ҩƷ����
                {
                    #region ��ҩƷ����

                    //��������а�������ҩƷvalΪtrue
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut f in alTemp)
                    {
                        if (f.Item.ID == this.DrugCode)
                        {
                            val = true;
                            break;
                        }
                    }
                    //�������Һ���в�������ҩƷ�򲻼�������
                    if (!val)
                    {
                        val = false;
                        continue;
                    }

                    #endregion
                }

                if (i >= 3)
                {
                    i = 0;
                    j += 1;
                }

                //����4����ʱ���ҳ��ʾ
                ucCompoundLabel ucclc = new ucCompoundLabel();
                if (alTemp.Count > 4)
                {
                    #region ��ҳ����

                    int count = 1;
                    int iCount = 1;
                    int pCount = 0;
                    ArrayList listSmall = new ArrayList();
                    ArrayList listBig = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alTemp)
                    {
                        if (count <= 4 * iCount)
                        {
                            count++;
                            listSmall.Add(info);
                            continue;    //����4��ʱ����������������4����¼
                        }
                        else
                        {
                            listBig.Add(listSmall);
                            listSmall = new ArrayList();
                            listSmall.Add(info);
                            iCount++;
                            count++;
                        }
                    }
                    if (listSmall.Count > 0)
                    {
                        listBig.Add(listSmall);
                    }

                    #endregion

                    foreach (ArrayList list in listBig)
                    {
                        if (i >= 3)
                        {
                            i = 0;
                            j += 1;
                        }
                        bool isHaveUnValid = false;

                        #region �����list��ߴ����������� ����Ŀ�����Ա�ʶ

                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in list)
                        {
                            if (info.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                            {
                                isHaveUnValid = true;
                                break;
                            }
                        }

                        #endregion

                        #region ��ǩҳ��ֵ����ӡ

                        pCount++;
                        ucclc = new ucCompoundLabel();
                        this.pnlCardCollections.Controls.Add(ucclc);
                        ucclc.Left = i * 5 + i * 235;
                        ucclc.Top = j * 20 + j * 307 + 20;
                        ucclc.Clear();
                        ucclc.LabelTotNum = Convert.ToDecimal(alGroupApplyOut.Count);
                        ucclc.ICount = i + j * 3 + 1;
                        ucclc.IPage = pCount + "/" + iCount;
                        ucclc.Tag = list;           //���汾��ǩ������
                        ucclc.AddComboNonePrint(list);

                        CheckBox ckTemp = new CheckBox();
                        ckTemp.Left = ucclc.Left;
                        ckTemp.Top = ucclc.Top - 20;
                        ckTemp.Tag = Convert.ToString(i + j * 3);
                        ckTemp.Width = 235;

                        if (isHaveUnValid == true)      //������������
                        {
                            ckTemp.Text = "����˴�ѡ��ǰ��ǩ[������]                      ";
                            //ckTemp.ForeColor = System.Drawing.Color.Red;                            
                        }
                        else
                        {
                            ckTemp.Text = "����˴�ѡ��ǰ��ǩ                              ";
                            //ckTemp.ForeColor = System.Drawing.Color.Black;
                        }

                        FS.HISFC.Models.Pharmacy.ApplyOut tempApplyState = list[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (tempApplyState.PrintState == "0" || string.IsNullOrEmpty(tempApplyState.PrintState) == true)
                        {
                            ckTemp.ForeColor = System.Drawing.Color.Black;
                            // ckTemp.Text = "����˴�ѡ��ǰ��ǩ                              ";//ckTemp.Tag.ToString();
                        }
                        else if (tempApplyState.PrintState == "1")
                        {
                            ckTemp.ForeColor = System.Drawing.Color.Red;
                            //ckTemp.Text = "����˴�ѡ��ǰ��ǩ[�Ѵ�ӡ]                      ";//ckTemp.Tag.ToString();
                        }

                        ckTemp.Visible = true;
                        this.pnlCardCollections.Controls.Add(ckTemp);

                        #endregion

                        i++;
                    }
                }
                else
                {
                    bool isHaveUnValid = false;

                    #region �����list��ߴ����������� ����Ŀ�����Ա�ʶ

                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alTemp)
                    {
                        if (info.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            isHaveUnValid = true;
                            break;
                        }
                    }

                    #endregion

                    #region ��ǩҳ��ֵ����ӡ

                    //ucCompoundLabelC ucclc = new ucCompoundLabelC();     
                    ucclc = new ucCompoundLabel();

                    this.pnlCardCollections.Controls.Add(ucclc);
                    ucclc.Left = i * 5 + i * 235;
                    ucclc.Top = j * 20 + j * 307 + 20;
                    ucclc.Clear();
                    ucclc.LabelTotNum = Convert.ToDecimal(alGroupApplyOut.Count);
                    ucclc.ICount = i + j * 3 + 1;
                    ucclc.Tag = alTemp;           //���汾��ǩ������
                    ucclc.AddComboNonePrint(alTemp);


                    CheckBox ckTemp = new CheckBox();
                    ckTemp.Left = ucclc.Left;
                    ckTemp.Top = ucclc.Top - 20;
                    ckTemp.Tag = Convert.ToString(i + j * 3);
                    ckTemp.Width = 235;

                    if (isHaveUnValid == true)      //������������
                    {
                        //ckTemp.ForeColor = System.Drawing.Color.Red;
                        ckTemp.Text = "����˴�ѡ��ǰ��ǩ[������]                      ";
                    }
                    else
                    {
                        //ckTemp.ForeColor = System.Drawing.Color.Black;
                        ckTemp.Text = "����˴�ѡ��ǰ��ǩ                              ";
                    }


                    FS.HISFC.Models.Pharmacy.ApplyOut tempApplyState = alTemp[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (tempApplyState.PrintState == "0" || string.IsNullOrEmpty(tempApplyState.PrintState) == true)
                    {
                        ckTemp.ForeColor = System.Drawing.Color.Black;
                        //ckTemp.Text = "����˴�ѡ��ǰ��ǩ                              ";//ckTemp.Tag.ToString();
                    }
                    else if (tempApplyState.PrintState == "1")
                    {
                        //ckTemp.Text = "����˴�ѡ��ǰ��ǩ[�Ѵ�ӡ]                      ";//ckTemp.Tag.ToString();
                        ckTemp.ForeColor = System.Drawing.Color.Red;
                    }

                    ckTemp.Visible = true;
                    this.pnlCardCollections.Controls.Add(ckTemp);

                    #endregion

                    i++;
                }

            }

            return 1;
        }



        /// <summary>
        /// ͨ��ѡ��Ƭͬ��ѡ����ϸ�б���Ӧ���κ�����
        /// </summary>
        public void SynCheckedStates()
        {
            //��Һ���ļ��ݿ�Ƭ������ʽ by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        if (ckk.Checked)
                        {
                            //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                            ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                            if (ucc.GroupNO != "")
                            {
                                for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                {
                                    if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                    {
                                        this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��ӡ��ϸ����ܰ�ҩ��
        /// </summary>
        /// <param name="alApplyout"></param>
        public void PrintDrugBills(ArrayList alApplyout)
        {
            Function.PrintCompound(alApplyout, true, false);
        }
        #endregion

        #region �¼�

        /// <summary>
        /// ҳ�����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                if (this.Init() == -1)
                {
                    MessageBox.Show(Language.Msg("��ʼ��ִ�з�������"));
                    return;
                }

                base.OnStatusBarInfo(null, "   �����ɫ�����Ѵ�ӡ,������ɫ����������");

                if (this.currentWinFun == WinFun.Print)     //������ӡ
                {
                    this.rbPrinting.Checked = true;
                    this.rbCancel.Visible = false;

                }
                else if (this.currentWinFun == WinFun.Save) //ȷ�ϲ���
                {
                    this.rbPrinted.Checked = true;
                    this.rbCancel.Visible = true;
                    this.rbAll.Visible = false;
                    this.rbPrinting.Visible = false;
                }
            }
        }
        /// <summary>
        /// TABҳ�л�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                //this.chkPhamrcy.Visible = false;
            }
            else
            {
                //��Һ���ļ��ݿ�Ƭ������ʽby Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
                if (this.IsShowCardStyle)
                {
                    for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                            if (ckk.Checked)
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                this.chkPhamrcy.Visible = true;
            }
        }
        /// <summary>
        /// ͬ���ѡ��һ����ѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpApply_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut apply = this.fpApply_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            if (apply == null)
            {
                return;
            }
            string combo = apply.CombNO;
            DateTime dateUse = apply.UseTime;
            if (e.Column == 1)
            {
                for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (info == null)
                    {
                        return;
                    }
                    if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpApply_Sheet1.Cells[this.fpApply_Sheet1.ActiveRowIndex, 1].Value))
                    {
                        if (info.UseTime == dateUse && info.CombNO == combo)
                        {
                            this.fpApply_Sheet1.Cells[i, 1].Value = true;
                        }
                    }
                    else
                    {
                        if (info.UseTime == dateUse && info.CombNO == combo)
                        {
                            this.fpApply_Sheet1.Cells[i, 1].Value = false;
                        }
                    }
                }
                for (int z = 0; z < this.fpApply_Sheet1.Rows.Count; z++)
                {
                    for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;

                            ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                            if (ucc.GroupNO != "")
                            {

                                if (this.fpApply_Sheet1.Cells[z, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                {
                                    ckk.Checked = (bool)this.fpApply_Sheet1.Cells[z, 1].Value;
                                }

                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// �������κ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpApply_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut apply = this.fpApply_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            if (apply == null)
            {
                return;
            }
            string combo = apply.CombNO;
            DateTime dateUse = apply.UseTime;
            string compoundGroup = this.fpApply_Sheet1.Cells[this.fpApply_Sheet1.ActiveRowIndex, (int)ColumnSet.ColCompoundGroup].Text;
            if (e.Column == (int)ColumnSet.ColCompoundGroup)
            {
                for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (info == null)
                    {
                        return;
                    }
                    if (info.UseTime == dateUse && info.CombNO == combo)
                    {
                        this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text = compoundGroup;
                    }
                }
            }
        }
        /// <summary>
        /// �����ѡ���¼�
        /// </summary>
        /// <param name="alData"></param>
        private void tvCompound_SelectDataEvent(ArrayList alData)
        {
            if (this.tvCompound.SelectedNode.Tag == "allDept")
            {
                return;
            }
            if (this.tvCompound.SelectedNode.Parent == null)
            {
                this.lbInfo.Text = string.Format("��ǰ����:{0} �����ܼ�:{1} ", this.tvCompound.SelectedNode.Text, this.tvCompound.SelectedNode.Nodes.Count);
            }
            else
            {
                this.lbInfo.Text = string.Format("��ǰ����:{0}", this.tvCompound.SelectedNode.Text);
            }

            this.AddDataToFp(alData);
        }
        /// <summary>
        /// ���κ�ѡ��ʱ�򴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrderGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvCompound.GroupCode = this.GroupCode;

            this.ShowList();
        }
        /// <summary>
        /// ҩƷ�б�ѡ��򴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPhamrcy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPhamrcy.Checked)
            {
                this.cmbParmacy.Visible = true;
                this.cmbOrderGroup.SelectedIndex = 0;
                this.cmbOrderGroup.Enabled = false;
            }
            else
            {
                this.cmbParmacy.Visible = false;
                this.cmbOrderGroup.Enabled = true;
                this.cmbParmacy.Text = "";
                this.ShowList();
            }
        }
        /// <summary>
        /// ҩƷ�б�ѡ�񴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbParmacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool val = false;
            ArrayList applyList = new ArrayList();
            this.ShowList();
            this.lbInfo.Text = "���в����а�����ҩƷ��Ϣ...";
            if (this.alThisParList != null && this.alThisParList.Count > 0)
            {
                ArrayList alGroupApplyOut = new ArrayList();
                ArrayList alCombo = new ArrayList();
                string privCombo = "-1";
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in this.alThisParList)
                {
                    if (privCombo == "-1" || (privCombo == info.CompoundGroup && info.CompoundGroup != ""))
                    {
                        alCombo.Add(info.Clone());
                        privCombo = info.CompoundGroup;
                        continue;
                    }
                    else
                    {
                        alGroupApplyOut.Add(alCombo);
                        privCombo = info.CompoundGroup;
                        alCombo = new ArrayList();
                        alCombo.Add(info.Clone());
                    }
                }
                if (alCombo.Count > 0)
                {
                    alGroupApplyOut.Add(alCombo);
                }
                foreach (ArrayList list in alGroupApplyOut)
                {
                    val = false;
                    //��������а�������ҩƷvalΪtrue
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut f in list)
                    {
                        if (f.Item.ID == this.DrugCode)
                        {
                            val = true;
                            break;
                        }
                    }
                    //�������Һ���в�������ҩƷ�򲻴�ӡ
                    if (!val)
                    {
                        continue;
                    }
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut f in list)
                    {
                        applyList.Add(f);
                    }
                }
                this.AddDataToFp(applyList);
            }
        }
        /// <summary>
        /// ���ô�ӡ��ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                ArrayList alCheck = this.GetCheckData();

                this.PrintDrugBills(alCheck);
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region ö��
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
            /// ���κ�
            /// </summary>
            ColCompoundGroup,
            /// <summary>
            /// �����
            /// </summary>
            ColMoCombo,
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
            /// ҽ������
            /// </summary>
            ColOrderType,
            /// <summary>
            /// ����ҽ��
            /// </summary>
            ColDoctor,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            ColApplyTime,
            /// <summary>
            /// ��Ϻ�
            /// </summary>
            ColCombNo

        }

        /// <summary>
        /// ��ǰ���ڹ�������
        /// </summary>
        public enum WinFun
        {
            Print,
            Save
        }
        #endregion

        #region ����
        /// <summary>
        /// �����������
        /// </summary>
        class ComboSort : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
                string str1 = o1.CombNO + o1.UseTime.ToString();
                string str2 = o2.CombNO + o2.UseTime.ToString();
                return string.Compare(str1, str2);
            }
        }
        /// <summary>
        /// �������κ�����
        /// </summary>
        class CompandGroupSort : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
                string str1 = o1.CompoundGroup + o1.Item.ID;
                string str2 = o2.CompoundGroup + o2.Item.ID;

                return string.Compare(str1, str2);
            }
        }

        #endregion

        #region ICompoundGroup ��Ա
        /* ���˼·�����Ƚ�ҽ���ֽ�ʱ�򴫵ݹ�����ҽ����Ϣ������д���ֻ���������͵���Һ���ң��ڳ�����ά����*/
        /*��ҽ����Ϣ�洢��һ�������飬Ȼ���������㷨�Ƚ�һЩ�������Σ�A��B��C��D��E��4��5 ���������*/
        /*֮��ѭ��һ�½�ͬ�������Һ�嶼��ֵ��ͬһ���Σ������������ζ��Ѿ���ֵ����ʣ���ҽ����Ϣ�洢��һ�� */
        /*���飬Ȼ���շ����ε��㷨�������Σ���ÿ��Һ��ҩ����������Ǹ�ҩƷȡ�����γ�һ�����飬������ */
        /*����Ƶ������Ȼ��ȡ��ÿ������Һ���������ۼӸ����� ��֮��ѭ��һ�½�ͬ�������Һ�嶼��ֵͬһ����*/
        public string GetCompoundGroup(FS.HISFC.Models.Order.ExecOrder order)
        {
            string compoundGroup = "";
            return compoundGroup;
        }

        public int GetCompoundGroup(ArrayList List, ref string error)
        {
            //�������
            ComboSort comboSort = new ComboSort();

            //������ҽ����Ϣ����
            ArrayList execOrderList = new ArrayList();
            //��ȡ��Һ����--begin
            ArrayList compoundDeptList = this.managerIntegrate.GetConstantList("CompoundDept");
            if (compoundDeptList == null)
            {
                error = "��ȡ��Һ���ҳ���";
                return -1;
            }
            if (compoundDeptList.Count == 0)
            {
                error = "��Һ���Ļ�û��ά����Һ���ң���֪ͨ��Һ�����ڳ���ά����ά����Һ���ң�";
                return -1;
            }
            //--end

            //��ȡ�÷�---begin---
            ArrayList usageList = this.managerIntegrate.GetConstantList("USAGE");
            if (usageList == null)
            {
                error = "��ȡ�÷�����";
                return -1;
            }
            //----end-----

            //ֻ����ȡҩ����Ϊ��Һ���ҵ�ҽ����Ϣ�γ�����--begin
            foreach (FS.FrameWork.Models.NeuObject obj in compoundDeptList)
            {
                foreach (FS.HISFC.Models.Order.ExecOrder order in List)
                {
                    if (obj.ID == order.Order.StockDept.ID)
                    {
                        execOrderList.Add(order);
                    }
                }
            }
            if (execOrderList.Count == 0)
            {
                return 0;
            }
            //--end

            //��ȡƵ��--begin
            FS.HISFC.BizProcess.Integrate.Manager manger = new FS.HISFC.BizProcess.Integrate.Manager();
            alFrequency = new ArrayList();
            alFrequency = manger.QuereyFrequencyList();
            if (alFrequency == null)
            {
                error = "��ȡƵ����Ϣ����";
                return -1;
            }
            //--end


            //���������
            execOrderList.Sort(comboSort);


            //---------------------begin----------------------------------------
            #region ���������㷨

            //----begin-����---
            string combo1 = "";
            string combo2 = "";
            string compoundGroupNo1 = "";
            string compoundGroupNo2 = "";
            string sqNo = "";   //ȡ��������������ˮ��
            //----end---------

            foreach (FS.HISFC.Models.Order.ExecOrder order in execOrderList)
            {
                #region ǰ�ڸ�ֵ
                //ҩ����������Ÿ�ֵ��User01�ϣ����ں���ȡҩ����������ҩƷ
                for (int i = 0; i < this.alPharmcyFunction.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.PhaFunction phFun = this.alPharmcyFunction[i] as FS.HISFC.Models.Pharmacy.PhaFunction;
                    if (order.Order.Item.ID == phFun.ID)
                    {
                        order.Order.Item.User01 = phFun.SortID.ToString();
                    }
                }
                //Ƶ������Ÿ�ֵ ����Ƶ������
                foreach (FS.HISFC.Models.Order.Frequency fr in this.alFrequency)
                {
                    if (order.Order.Frequency.ID == fr.ID)
                    {
                        order.Order.Frequency.SortID = fr.SortID;
                    }
                }
                #endregion



                combo2 = order.Order.Combo.ID + order.DateUse.ToString();
                //ͬ���ֱ�Ӹ�ֵ������ѭ��              
                if (combo1 == combo2)
                {
                    order.Order.Item.User02 = compoundGroupNo1;
                    continue;
                }

                //�ж�ͬ��Һ���е�û�е�λΪml�ģ��ŵ�E����-----begin----
                string str = "";
                int drugCount = 0; //һ���е�ҩƷ���������Ϊ1��һ��ҩƷ��ŵ�B����
                foreach (FS.HISFC.Models.Order.ExecOrder o in execOrderList)
                {
                    if (order.Order.Combo.ID + order.DateUse.ToString() == o.Order.Combo.ID + o.DateUse.ToString())
                    {
                        str += o.Order.DoseUnit.ToString() + "|";
                        drugCount++;
                    }
                }
                //���ͬ���в�������λΪml��
                if (!str.Contains("ml|"))
                {
                    sqNo = itemManager.GetNewCompoundGroup();//��ȡ�µ�������ˮ��
                    order.Order.Item.User02 = "E_" + sqNo;
                    compoundGroupNo2 = order.Order.Item.User02;
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //--------end-----


                //------��ʱҽ��,����A -- begin--
                if (order.Order.OrderType.ID.ToString() == "LZ")
                {
                    sqNo = itemManager.GetNewCompoundGroup();
                    order.Order.Item.User02 = "A_" + sqNo;
                    compoundGroupNo2 = order.Order.Item.User02;
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //----end----------


                //------------�����ҩƷ�����Σ�B-----begin---
                for (int i = 0; i < this.alPharmcyFunction.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.PhaFunction phFun = this.alPharmcyFunction[i] as FS.HISFC.Models.Pharmacy.PhaFunction;
                    if (order.Order.Item.ID == phFun.ID)
                    {
                        //���ҩƷ���߾�һ��ҩƷ
                        if (phFun.Memo == "1" || drugCount == 1)
                        {
                            sqNo = itemManager.GetNewCompoundGroup();
                            order.Order.Item.User02 = "B_" + sqNo;
                            compoundGroupNo2 = order.Order.Item.User02;
                        }
                        break;
                    }
                }
                //����Ѿ���ֵ�����������һ��ҽ��
                if (order.Order.Item.User02 != "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //--------end----


                //----�����÷�,����Ӫ��:C,���֣�D-----begin---- 
                for (int i = 0; i < usageList.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject useObj = usageList[i] as FS.FrameWork.Models.NeuObject;
                    foreach (FS.HISFC.Models.Order.ExecOrder o in execOrderList)
                    {
                        if ((order.Order.Combo.ID + order.DateUse.ToString() == o.Order.Combo.ID + o.DateUse.ToString()) &&
                            o.Order.Usage.ID == useObj.ID)
                        {
                            switch (useObj.Memo)
                            {
                                case "����Ӫ��":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "C_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                                case "Ī���Ϲ�":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "D_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                                case "��������":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "D_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                                case "����ע��":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "D_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                            }
                            if (order.Order.Item.User02 != "")
                            {
                                break;
                            }
                        }
                    }
                    if (order.Order.Item.User02 != "")
                    {
                        break;
                    }
                }
                //����Ѿ���ֵ�����������һ��ҽ��
                if (order.Order.Item.User02 != "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //-------end-----------


                //ִ��ʱ�����12:00��16:00֮�������Ϊ��4,����16:00������Ϊ��5  ------begin------

                //ִ������
                string dateStr = order.DateUse.Year.ToString() + "-" + order.DateUse.Month.ToString()
                    + "-" + order.DateUse.Day.ToString();
                foreach (FS.HISFC.Models.Order.Frequency fr in this.alFrequency)
                {
                    if (order.Order.Frequency.ID == fr.ID)
                    {
                        if (DateTime.Compare(order.DateUse, FS.FrameWork.Function.NConvert.ToDateTime(dateStr + " 12:00")) >= 0
                            && DateTime.Compare(order.DateUse, FS.FrameWork.Function.NConvert.ToDateTime(dateStr + " 16:00")) <= 0)
                        {
                            sqNo = itemManager.GetNewCompoundGroup();
                            order.Order.Item.User02 = "4_" + sqNo;
                            compoundGroupNo2 = order.Order.Item.User02;
                            break;
                        }
                        if (DateTime.Compare(order.DateUse, FS.FrameWork.Function.NConvert.ToDateTime(dateStr + " 16:00")) > 0)
                        {
                            sqNo = itemManager.GetNewCompoundGroup();
                            order.Order.Item.User02 = "5_" + sqNo;
                            compoundGroupNo2 = order.Order.Item.User02;
                            break;
                        }
                    }
                }
                //����Ѿ���ֵ�����������һ��ҽ��
                if (order.Order.Item.User02 != "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //------end-------

                //���û�����θ�ֵΪ����ֵһ����Ϻţ����ҽ�compoundGroupNo1���
                if (order.Order.Item.User02 == "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = "";
                    continue;
                }

            }
            #endregion
            //---------------------end------------------------------------------


            //�������������Ѿ���ֵ��ϣ����ڽ�ʣ������Ϊ�յ�������һ�����������1,2,3���ε�
            //��ֵ���������Һ�����ۼ��㷨


            //-------------------------begin-------------------------------------   

            #region 1,2,3�����㷨

            //----ȡ��ʣ���ҽ����Ϣ���������---begin----
            ArrayList otherList = new ArrayList();//�洢ʣ��ҽ����Ϣ
            foreach (FS.HISFC.Models.Order.ExecOrder order in execOrderList)
            {
                if (order.Order.Item.User02 == "")
                {
                    otherList.Add(order);
                }
            }
            if (otherList.Count == 0)
            {
                return 0;
            }
            //------end-------

            otherList.Sort(comboSort);//�������

            //-----begin----
            #region ȡÿ���е�ҩ����������Ǹ�д��һ��������
            ArrayList alCouts = new ArrayList();
            string bill1 = "";
            string bill2 = "";
            int sortId = 0;
            ArrayList alList = new ArrayList();
            int count = 0;
            foreach (FS.HISFC.Models.Order.ExecOrder o in otherList)
            {
                bill2 = o.Order.Combo.ID + o.DateUse.ToString();
                if (bill2 != bill1)
                {
                    sortId = FS.FrameWork.Function.NConvert.ToInt32(o.Order.Item.User01);
                    if (sortId == 0 || sortId.ToString() == "")
                    {
                        sortId = 99;
                    }
                    alList = new ArrayList();
                    alList.Add(o);
                    count++;
                    alCouts.Add(alList);
                    bill1 = bill2;
                }
                else
                {
                    if (sortId > FS.FrameWork.Function.NConvert.ToInt32(o.Order.Item.User01))
                    {
                        sortId = FS.FrameWork.Function.NConvert.ToInt32(o.Order.Item.User01);
                        alList = new ArrayList();
                        alList.Add(o);
                        alCouts[count - 1] = alList;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            #endregion
            //-------end-----

            //------begin------
            #region �������Һ�����ۼӷ�����
            string combo = "";
            decimal doseOnce = 0;
            string compGroup = "";
            bool val1 = true;
            bool val2 = true;
            bool val3 = true;
            for (int i = 0; i < alCouts.Count; i++)
            {
                ArrayList al = alCouts[i] as ArrayList;
                decimal maxDoseOnce = 0;
                FS.HISFC.Models.Order.ExecOrder execOrder = al[0] as FS.HISFC.Models.Order.ExecOrder;
                if (execOrder.Order.DoseUnit == "ml") //�����λΪ������ֵ���Һ����������Ϊ0
                {
                    maxDoseOnce = execOrder.Order.DoseOnce;
                }
                else
                {
                    maxDoseOnce = 0;
                }
                combo = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();
                //ȡÿ��Һ�е����Һ����---begin---
                foreach (FS.HISFC.Models.Order.ExecOrder order in otherList)
                {
                    if (order.Order.Combo.ID + order.DateUse.ToString() == combo)
                    {
                        if (order.Order.DoseOnce > maxDoseOnce && order.Order.DoseUnit == "ml")
                        {
                            maxDoseOnce = order.Order.DoseOnce;
                        }
                    }
                }
                //----end-------
                //��һ�����ۼƣ�������200���´�ѭ���������ⲿ��
                if (val1)
                {
                    if ((maxDoseOnce + doseOnce) >= 200)
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "1_" + sqNo;
                        doseOnce = 0;
                        val1 = false;
                        continue;
                    }
                    else
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "1_" + sqNo;
                        doseOnce += maxDoseOnce;
                        val1 = true;
                        continue;
                    }
                }
                //�ڶ������ۼƣ�������250���´�ѭ���������ⲿ��
                if (val2)
                {
                    if ((maxDoseOnce + doseOnce) >= 250)
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "2_" + sqNo;
                        doseOnce = 0;
                        val2 = false;
                        continue;
                    }
                    else
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "2_" + sqNo;
                        doseOnce += maxDoseOnce;
                        val2 = true;
                        continue;
                    }
                }
                //���������ۼƣ�������750���´�ѭ���������ⲿ��
                if (val3)
                {
                    if ((maxDoseOnce + doseOnce) >= 750)
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "3_" + sqNo;
                        doseOnce = 0;
                        val3 = false;
                        continue;
                    }
                    else
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "3_" + sqNo;
                        doseOnce += maxDoseOnce;
                        val3 = true;
                        continue;
                    }
                }
                //�������ҩ�򶼷Ž���������
                if (!val1 && !val2 && !val3)
                {
                    sqNo = itemManager.GetNewCompoundGroup();
                    execOrder.Order.Item.User02 = "4_" + sqNo;
                    continue;
                }
            }
            #endregion
            //------end--------

            //-----ͬ���ҩƷ���κŸ�ֵ---begin-----
            for (int i = 0; i < alCouts.Count; i++)
            {
                ArrayList al = alCouts[i] as ArrayList;
                FS.HISFC.Models.Order.ExecOrder execOrder = al[0] as FS.HISFC.Models.Order.ExecOrder;
                foreach (FS.HISFC.Models.Order.ExecOrder o in otherList)
                {
                    if (o.Order.Combo.ID + o.DateUse.ToString() == execOrder.Order.Combo.ID + execOrder.DateUse.ToString())
                    {
                        o.Order.Item.User02 = execOrder.Order.Item.User02;
                    }
                }
            }
            //------end--------

            #endregion

            //--------------------------end---------------------------------------

            return 1;
        }

        #endregion






        #region ICompoundGroup ��Ա

        public int GetCompoundGroup(ArrayList List)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
