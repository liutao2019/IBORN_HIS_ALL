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

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// <br></br>
    /// [��������: ���ñ�ǩ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// <˵��>
    ///     1��
    /// </˵��>
    /// </summary>
    public partial class ucCompoundRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompoundRePrint()
        {
            InitializeComponent();
        }

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

        /// <summary>
        /// ҽ�����ͼ���
        /// </summary>
        private System.Collections.Hashtable hsOrderType = new Hashtable();

        /// <summary>
        /// ���ڴ洢���л�������
        /// </summary>
        Hashtable hsAllRadiotherapyLZ = new Hashtable();

        /// <summary>
        /// ���ڴ洢���л����÷�
        /// </summary>
        Hashtable hsRadiotherapyUsage = new Hashtable();

        /// <summary>
        /// �������Ĺ�����
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// �������ĵ��ݴ�ӡ�ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundPrint iCompoundPrintObj;

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
        /// ���μ������ʱ��
        /// </summary>
        private DateTime MaxDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMaxValue.Text);
            }
        }

        /// <summary>
        /// ���μ�����Сʱ��
        /// </summary>
        private DateTime MinDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMinValue.Text);
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
            this.toolBarService.AddToolButton("�����ǩ", "�������ñ�ǩ", FS.FrameWork.WinForms.Classes.EnumImageList.YԤ��, true, false, null);
            this.toolBarService.AddToolButton("����ִ��", "����ִ����ϸ��", FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("ˢ��", "ˢ�»����б���ʾ", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            this.toolBarService.AddToolButton("��ͨ����", "������ͨ��ʱҽ��", FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��, true, false, null);
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
                case "�����ǩ":
                    this.Print(true, false);
                    break;
                case "����ִ��":
                    this.Print(false, true);
                    break;
                case "��ͨ����":
                    this.Filter("short");
                    break;
                case "��������":
                    this.Filter("shortSpecial");
                    break;
                case "����":
                    this.Filter("long");
                    break;
                case "ȫ��":
                    this.Filter("all");
                    break;
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return base.OnPrint(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryCompound(this.txtCompoundGroup.Text);

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        private void Print(bool isPrintLabel, bool isPrintDetail)
        {
            ArrayList alCheck = this.GetCheckData();
            if (iCompoundPrintObj == null)
            {
                return;
            }
            if (isPrintLabel)
            {
                iCompoundPrintObj.PrintCompoundLabel(alCheck);
            }
            if (isPrintDetail)
            {
                iCompoundPrintObj.PrintCompoundList(alCheck);
            }
        }

        /// <summary>
        /// ��ȡ�������������÷�
        /// </summary>
        /// <param name="alData"></param>
        private void SplitSecialLZ(ArrayList alData)
        {
            if (alData == null || alData.Count == 0)
            {
                return;
            }
            hsAllRadiotherapyLZ = this.GetRadiotherapyLZ(alData);
        }

        /// <summary>
        /// ��ȡ����������Ŀ
        /// </summary>
        /// <param name="allApplyData"></param>
        /// <returns></returns>
        private Hashtable GetRadiotherapyLZ(ArrayList allApplyData)
        {
            Hashtable hsCombo = new Hashtable();

            if (allApplyData == null || allApplyData.Count == 0)
            {
                return hsCombo;
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in allApplyData)
            {
                if (string.IsNullOrEmpty(applyInfo.CombNO))
                {
                    continue;
                }

                if (applyInfo.OrderType.ID != "LZ")
                {
                    continue;
                }

                if (hsCombo.Contains(applyInfo.CombNO))
                {
                    continue;
                }

                FS.HISFC.Models.Pharmacy.Item itemTmp = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyInfo.Item.ID);

                if (FS.FrameWork.Function.NConvert.ToBoolean(itemTmp.SpecialFlag))
                {
                    hsCombo.Add(applyInfo.CombNO, applyInfo.CombNO);

                    continue;
                }

                if (hsRadiotherapyUsage != null && hsRadiotherapyUsage.Count > 0)
                {
                    if (hsRadiotherapyUsage.Contains(applyInfo.Usage.ID))
                    {
                        ArrayList allDataTmp = new ArrayList();

                        allDataTmp.Add(applyInfo);

                        hsCombo.Add(applyInfo.CombNO, allDataTmp);

                        continue;

                    }
                }
            }
            return hsCombo;
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
                        if (this.hsAllRadiotherapyLZ.Contains((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).CombNO))
                        {
                            this.fpApply_Sheet1.Rows[i].Visible = false;
                        }
                        else
                        {
                            this.fpApply_Sheet1.Rows[i].Visible = true;
                        }
                    }
                    else
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = false;
                    }
                }
                this.toolBarService.GetToolButton("��ͨ����").Text = "��������";
            }
            else if (tag == "shortSpecial")
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    if ((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).OrderType.ID.ToString() == "LZ")
                    {
                        if (this.hsAllRadiotherapyLZ.Contains((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).CombNO))
                        {
                            this.fpApply_Sheet1.Rows[i].Visible = true;
                        }
                        else
                        {
                            this.fpApply_Sheet1.Rows[i].Visible = false;
                        }
                    }
                    else
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = false;
                    }
                }
                this.toolBarService.GetToolButton("��������").Text = "����";
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
                this.toolBarService.GetToolButton("����").Text = "ȫ��";
            }
            else
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    this.fpApply_Sheet1.Rows[i].Visible = true;
                }
                this.toolBarService.GetToolButton("ȫ��").Text = "��ͨ����";
            }
            return 1;
        }

        /// <summary>
        /// ��ʼ�����нӿ�
        /// </summary>
        private void InitInterface()
        {
            iCompoundPrintObj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundPrint)) as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundPrint;
        }

        /// <summary>
        /// ��ʼ��ҽ��������Ϣ
        /// </summary>
        /// <returns></returns>
        private int InitOrderGroup()
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

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
            this.cmbOrderGroup.SelectedIndex = 0;
            string orderGroup = consManager.GetOrderGroup(consManager.GetDateTimeFromSysDateTime());
            if (orderGroup != "")
            {
                this.cmbOrderGroup.Text = orderGroup;
            }

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
            FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alList = itemManager.QueryCompoundListByUseTime(this.ApproveDept.ID, groupCode, "2", "0", true, this.MinDate, this.MaxDate);
            if (alList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("��ȡ���õ��б�������") + itemManager.Err);
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
        /// ��������
        /// </summary>
        protected void Clear()
        {
            this.fpApply_Sheet1.Rows.Count = 0;
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
        /// ����������ˮ�ż���
        /// </summary>
        /// <param name="compoundGroup">������ˮ��</param>
        protected void QueryCompound(string compoundGroup)
        {
            if (compoundGroup == null || compoundGroup == "")
            {
                return;
            }

            FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();
            ArrayList alList = itemManager.QueryCompoundApplyOut(compoundGroup);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("����������ˮ�Ż�ȡ�������ݷ�������") + itemManager.Err);
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
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCombNo].Text = info.CombNO + info.UseTime;

                if (this.hsOrderType.ContainsKey(info.OrderType.ID))
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = this.hsOrderType[info.OrderType.ID].ToString();
                }
                else
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = info.OrderType.ID;
                }

                this.fpApply_Sheet1.Rows[i].Tag = info;
            }
            FS.SOC.HISFC.Components.Common.Function.DrawCombo(this.fpApply_Sheet1, (int)ColumnSet.ColCombNo, (int)ColumnSet.ColMoCombo); 
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

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                if (this.Init() == -1)
                {
                    MessageBox.Show(Language.Msg("��ʼ��ִ�з�������"));
                    return;
                }
            }

            base.OnLoad(e);
        }

        private void txtCompoundGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Clear();

                this.QueryCompound(this.txtCompoundGroup.Text);
            }
        }

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
                this.tvCompound = this.tv as FS.SOC.HISFC.Components.DrugStore.Compound.tvCompoundList;

                this.tvCompound.Init();

                this.tvCompound.State = "2";
            }

            DateTime date = this.itemManager.GetDateTimeFromSysDateTime();
            string str1 = date.Date.AddDays(0).ToShortDateString() + " " + "00:00:00";
            string str2 = date.Date.AddDays(1).ToShortDateString() + " " + "23:59:59";
            this.dtMinValue.Value = FS.FrameWork.Function.NConvert.ToDateTime(str1);
            this.dtMaxValue.Value = FS.FrameWork.Function.NConvert.ToDateTime(str2);

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            this.approveDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            //�洢���л����÷�
            ArrayList alRadiotherapyUsage = consMgr.GetAllList("RadiotherapyUsage");

            if (alRadiotherapyUsage != null && alRadiotherapyUsage.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject usageObj in alRadiotherapyUsage)
                {
                    hsRadiotherapyUsage.Add(usageObj.ID, usageObj);
                }
            }

            //ȡҽ�����ͣ����ڽ�����ת��������
            FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();
            ArrayList alOrderType = orderManager.GetList();
            foreach (FS.FrameWork.Models.NeuObject infoOrderType in alOrderType)
            {
                this.hsOrderType.Add(infoOrderType.ID, infoOrderType.Name);
            }

            this.InitOrderGroup();

            this.InitInterface();

            this.tvCompound.SelectDataEvent += new tvCompoundList.SelectDataHandler(tvCompound_SelectDataEvent);
            this.tvCompound.GetSelectApplyDataEvent +=new tvCompoundList.GetSelectApplyDataHandler(tvCompound_GetSelectApplyDataEvent); 

            //this.ShowList();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        private void tvCompound_SelectDataEvent(ArrayList alData)
        {
            this.AddDataToFp(alData);
            this.SplitSecialLZ(alData);
        }

        private void tvCompound_GetSelectApplyDataEvent(FS.HISFC.Models.Pharmacy.ApplyOut info,string patientNO, string groupCode, string state, bool isExec, ref ArrayList al)
        {
            al = this.itemManager.QueryCompoundApplyOutByUseTime(info.StockDept.ID, info.ApplyDept.ID, groupCode, patientNO, state, true,this.MinDate,this.MaxDate);
        }

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
            }
        }

        private void cmbOrderGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvCompound.GroupCode = this.GroupCode;

            this.ShowList();
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
            ColCompoundGroup,
            /// <summary>
            /// ��Ϻ�
            /// </summary>
            ColCombNo,
            /// <summary>
            /// ҽ������
            /// </summary>
            ColOrderType
        }

        
    }
}
