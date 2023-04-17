using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using PManager = FS.HISFC.BizLogic.Pharmacy.Preparation;
using PObject = FS.HISFC.Models.Preparation;

namespace FS.HISFC.Components.Preparation
{       
    /// <summary>
    /// <br></br>
    /// [��������: �Ƽ�ԭ������]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///    1���Ƽ����Ͽۿ�ʵ��
    ///    2���Բ�����ԭ�����Զ��γ�����ƻ�
    /// </˵��>
    /// </summary>
    public partial class ucExpand : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucExpand()
        {
            InitializeComponent();

            this.Init();
        }

        /// <summary>
        /// ������Ϣ��������¼�
        /// </summary>
        public event System.EventHandler ExpandDataFinishEvent;

        #region �����

        /// <summary>
        /// �Ƽ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();

        /// <summary>
        /// ҩƷ���ҵ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �Ƽ�ԭ�Ϲ���ӿ�
        /// </summary>
        private HISFC.Components.Preparation.IPreparation MaterialInterface = null;

        /// <summary>
        /// ����������
        /// </summary>
        private FS.FrameWork.Models.NeuObject stockDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ԭ�Ͽ����� �����Զ��γ�ԭ���������
        /// </summary>
        private FS.FrameWork.Models.NeuObject materialStockDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����ƻ�����
        /// </summary>
        private string planNO = "";

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���Ե����༭
        /// </summary>
        public bool IsCanEdit
        {
            set
            {
                this.btnSave.Enabled = value;

                this.fsMaterial_Sheet1.Columns[(int)ExpandColumnSet.ColFactualExpand].Locked = !value;
                this.fsMaterial_Sheet1.Columns[(int)ExpandColumnSet.ColMemo].Locked = !value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public FS.FrameWork.Models.NeuObject StockDept
        {
            get
            {
                return this.stockDept;
            }
            set
            {
                this.stockDept = value;

                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                System.Collections.ArrayList alMaterialStockDept = managerIntegrate.GetPrivInOutDeptList(this.stockDept.ID, "0310");
                if (alMaterialStockDept == null)
                {
                    MessageBox.Show(managerIntegrate.Err);
                    return;
                }
                if (alMaterialStockDept.Count > 0)
                {
                    //{385C8E03-2028-4bb4-81C6-D1197FEA2E74} ��ȡ��Ӧ�ϼ����� ����ԭ��������
                    this.materialStockDept = (alMaterialStockDept[0] as FS.HISFC.Models.Base.PrivInOutDept).Dept;
                }
            }
        }

        /// <summary>
        /// �����ƻ�����
        /// </summary>
        public string PlanNO
        {
            set
            {
                this.planNO = value;
            }
            get
            {
                return this.planNO;
            }
        }

        /// <summary>
        /// �Ƿ������������Ϣ��ʾ
        /// </summary>
        public bool IsOnlyShowExpand
        {
            get
            {
                return !this.gbControl.Visible;
            }
            set
            {
                this.gbControl.Visible = !value;

                this.fsMaterial_Sheet1.Columns[(int)ExpandColumnSet.ColPlanExpand].Visible = !value;
                this.fsMaterial_Sheet1.Columns[(int)ExpandColumnSet.ColStore].Visible = !value;
                this.fsMaterial_Sheet1.Columns[(int)ExpandColumnSet.ColNormativeQty].Visible = !value;

                this.IsCanEdit = !value;
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        protected int Init()
        {
            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            this.fsMaterial_Sheet1.Columns[(int)ExpandColumnSet.ColFactualExpand].CellType = markNumCellType;

            return 1;
        }

        /// <summary>
        /// ����Ƽ���Ʒ������Ϣ
        /// </summary>
        /// <param name="info">�Ƽ�������Ϣ</param>
        protected void AddExpandToFp(FS.HISFC.Models.Preparation.Expand info)
        {
            int rowCoount = this.fsMaterial_Sheet1.Rows.Count;

            this.fsMaterial_Sheet1.Rows.Add(rowCoount, 1);
            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColMaterialName].Text = info.Prescription.Material.Name;
            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColSpecs].Text = info.Prescription.Specs;
            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColPrice].Text = info.Prescription.Price.ToString();
            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColNormativeQty].Text = info.Prescription.NormativeQty.ToString() + "[" + info.Prescription.NormativeUnit + "]";
            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColPlanExpand].Text = info.PlanExpand.ToString() + "[" + info.Prescription.NormativeUnit + "]";

            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColStore].Text = info.StoreQty.ToString() + "[" + info.Prescription.NormativeUnit + "]";
            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColFactualExpand].Text = info.FacutalExpand.ToString() + "[" + info.Prescription.NormativeUnit + "]";
            this.fsMaterial_Sheet1.Cells[rowCoount, (int)ExpandColumnSet.ColMemo].Text = info.Memo;

            this.fsMaterial_Sheet1.Rows[rowCoount].Tag = info;
        }

        /// <summary>
        /// �Ƽ���Ʒ������Ϣ��ʾ
        /// </summary>
        /// <param name="expandList">�Ƽ���Ʒ������Ϣ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int ShowExpand(List<FS.HISFC.Models.Preparation.Expand> expandList)
        {
            this.Clear();

            foreach (FS.HISFC.Models.Preparation.Expand info in expandList)
            {
                if (info.Prescription.MaterialType == FS.HISFC.Models.Preparation.EnumMaterialType.Material)
                {
                    info.Prescription.Material = this.pharmacyIntegrate.GetItem(info.Prescription.Material.ID);
                    if (info.Prescription.Material == null)
                    {
                        MessageBox.Show("������Ŀ����" + info.Prescription.Material.ID + "��ȡ��Ŀ��Ϣʵ��ʧ��");
                        return -1;
                    }
                    decimal storeQty = 0;
                    if (this.pharmacyIntegrate.GetStorageNum(this.stockDept.ID, info.Prescription.Material.ID, out storeQty) == -1)
                    {
                        MessageBox.Show("����ԭ�Ͽ�淢������" + this.pharmacyIntegrate.Err);
                        return -1;
                    }
                    info.StoreQty = storeQty;
                }
                else
                {
                    if (this.MaterialInterface != null)
                    {
                        info.StoreQty = this.MaterialInterface.GetStore(this.stockDept.ID, info.Prescription.Material.ID);
                    }
                }

                if (info.FacutalExpand == -1)       //��ʼֵ ��һ�β���
                {
                    info.FacutalExpand = info.PlanExpand;
                }

                this.AddExpandToFp(info);
            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        internal void Clear()
        {
            this.fsMaterial_Sheet1.Rows.Count = 0;

            this.tabPage1.Text = "����ԭ�ϡ�������Ϣ �� �޸�������Ϣ����ע�Ᵽ��";
        }

        /// <summary>
        /// �Ƽ���Ʒ������Ϣ��ȡ
        /// </summary>
        /// <param name="preparation">�Ƽ���Ʒ������Ϣ</param>
        /// <returns>�ɹ������Ƽ���Ʒ������Ϣ ʧ�ܷ���null</returns>
        internal List<FS.HISFC.Models.Preparation.Expand> QueryExpandList(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            List<FS.HISFC.Models.Preparation.Expand> expandList = this.preparationManager.QueryExpand(preparation,this.stockDept);
            if (expandList == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ�Ƽ�������Ϣ��������") + this.preparationManager.Err);
                return null;
            }
            else if (expandList.Count == 0)
            {
                expandList = this.ComputePrescription(preparation);
            }
            else
            {
                foreach (FS.HISFC.Models.Preparation.Expand expand in expandList)
                {
                    if (expand.PlanQty == preparation.PlanQty)
                    {
                        continue;
                    }
                    else
                    {
                        expand.PlanQty = preparation.PlanQty;
                        expand.PlanExpand = preparation.PlanQty * expand.Prescription.NormativeQty;
                        expand.FacutalExpand = -1;
                    }
                }
            }

            return expandList;
        }

        /// <summary>
        /// �Ƽ���Ʒ������Ϣ��ʾ
        /// </summary>
        /// <param name="preparation">�Ƽ���Ʒ������Ϣ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        internal int ShowExpand(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            this.tabPage1.Text = preparation.Drug.Name + "[ " + preparation.Drug.Specs + " ] ����  ����ԭ�ϡ�������Ϣ �� �޸�������Ϣ����ע�Ᵽ��";

            List<FS.HISFC.Models.Preparation.Expand> expandList = this.QueryExpandList(preparation);

            if (expandList != null)
            {
                this.ShowExpand(expandList);
            }

            return 1;
        }

        /// <summary>
        /// �Ƽ�ԭ��������Ϣ����
        /// </summary>
        /// <param name="info">�Ƽ���Ʒ�ƻ���Ϣ</param>
        internal List<FS.HISFC.Models.Preparation.Expand> ComputePrescription(FS.HISFC.Models.Preparation.Preparation info)
        {
            List<FS.HISFC.Models.Preparation.Prescription> prescriptionList = this.preparationManager.QueryDrugPrescription(info.Drug.ID);
            if (prescriptionList == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�Ƽ����ƴ�����Ϣ��������") + this.preparationManager.Err);
                return null;
            }

            List<FS.HISFC.Models.Preparation.Expand> expandList = new List<FS.HISFC.Models.Preparation.Expand>();

            foreach (FS.HISFC.Models.Preparation.Prescription prescription in prescriptionList)
            {
                FS.HISFC.Models.Preparation.Expand expand = new FS.HISFC.Models.Preparation.Expand();

                expand.Prescription = prescription;
                expand.PlanNO = info.PlanNO;
                expand.PlanQty = info.PlanQty;
                expand.PlanExpand = info.PlanQty * prescription.NormativeQty;

                expand.FacutalExpand = -1;

                //{8840008D-2FEA-4471-B404-B05E25832120}  ��ȡ���
                decimal storeQty = 0;
                if (this.pharmacyIntegrate.GetStorageNum(this.stockDept.ID, expand.Prescription.Material.ID, out storeQty) == -1)
                {
                    MessageBox.Show("����ԭ�Ͽ�淢������" + this.pharmacyIntegrate.Err);
                    return null;
                }
                expand.StoreQty = storeQty;
                //{8840008D-2FEA-4471-B404-B05E25832120}  ��ȡ���

                expandList.Add(expand);
            }

            return expandList;
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <param name="isNotice">�Ƿ������Ϣ��ʾ</param>
        /// <returns>�������������Զ��γ�������Ϣ True ��治����Զ��γ����� False</returns>
        internal bool ValidStock(bool isNotice)
        {
            for (int i = 0; i < this.fsMaterial_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Expand expand = (this.fsMaterial_Sheet1.Rows[i].Tag as FS.HISFC.Models.Preparation.Expand).Clone();

                expand.FacutalExpand = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)ExpandColumnSet.ColFactualExpand].Text);

                if (expand.StoreQty < expand.FacutalExpand)
                {
                    if (isNotice)
                    {
                        DialogResult rs = MessageBox.Show(Language.Msg("��治�� �޷���ԭ���Ͽ��۳�,�Կ�治���ԭ�����Ƿ��Զ��γ����룿"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rs == DialogResult.No)      //�������Զ��γ����� ������Ч
                        {
                            return false;
                        }
                        else                           //�����Զ��γ����� isAutoApply����ΪTrue
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <param name="preparation">�Ƽ�����Ϣ</param>
        /// <param name="isNotice">�Ƿ������Ϣ��ʾ</param>
        /// <returns>�������������Զ��γ�������Ϣ True ��治����Զ��γ����� False</returns>
        internal bool ValidStock(FS.HISFC.Models.Preparation.Preparation preparation, bool isNotice)
        {
            List<FS.HISFC.Models.Preparation.Expand> expandList = this.QueryExpandList(preparation);
            if (expandList == null)
            {
                return false;
            }
            foreach (FS.HISFC.Models.Preparation.Expand expand in expandList)
            {
                if (expand.StoreQty < expand.FacutalExpand)
                {
                    if (isNotice)
                    {
                        DialogResult rs = MessageBox.Show(preparation.Drug.Name + Language.Msg("  ԭ�Ͽ�治��,�Ƿ��Զ��γ�������룿"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rs == DialogResult.No)      //�������Զ��γ����� ������Ч
                        {
                            return false;
                        }
                        else                           //�����Զ��γ����� isAutoApply����ΪTrue
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="info">�Ƽ�����Ϣ</param>
        /// <param name="isExecApplyData">�Ƿ���������ԭ����������Ϣ</param>
        /// <param name="Err">������ʾ</param>
        /// <returns></returns>
        internal int SaveExpandForStock(FS.HISFC.Models.Preparation.Preparation preparation, bool isExecApplyData, ref string Err)
        {
            Err = "";
            bool isLocalTrans = false;
            if (FS.FrameWork.Management.PublicTrans.Trans == null)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                isLocalTrans = true;
            }

            this.pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.preparationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);          

            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime();

            List<FS.HISFC.Models.Preparation.Expand> expandList = this.preparationManager.QueryExpand(preparation,this.stockDept);
            if (expandList == null)
            {
                if (isLocalTrans)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                }
                Err = FS.FrameWork.Management.Language.Msg("��ȡ�Ƽ�������Ϣ��������") + this.preparationManager.Err;
                return -1;
            }

            foreach (FS.HISFC.Models.Preparation.Expand info in expandList)
            {
                info.Prescription.OperEnv.OperTime = sysTime;
                info.Prescription.OperEnv.ID = this.preparationManager.Operator.ID;
                info.PlanNO = preparation.PlanNO;

                if (isExecApplyData)
                {
                    #region ������Ϣ����

                    if (info.StoreQty < info.FacutalExpand)
                    {
                        if (info.Prescription.MaterialType == FS.HISFC.Models.Preparation.EnumMaterialType.Material)     //ҩƷ
                        {
                            //{64FAE14C-7D1B-42ea-B19D-2C1B3846D2D0} ������Ϣ�Զ�����ʱ ���»�ȡ��Ŀ��Ϣ
                            FS.HISFC.Models.Pharmacy.Item tempItem = this.pharmacyIntegrate.GetItem(info.Prescription.Material.ID);
                            if (tempItem == null)
                            {
                                if (isLocalTrans)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                }
                                Err = Language.Msg("����ҩƷԭ�ϱ����ȡԭ������Ϣʧ�ܣ� ") + this.pharmacyIntegrate.Err;
                                return -1;
                            }
                            if (this.pharmacyIntegrate.ProduceApply(tempItem, info, this.stockDept, this.materialStockDept) == -1)
                            {
                                if (isLocalTrans)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                }
                                Err = Language.Msg("���ݿ�漰ԭ����������������Ϣ��������" + this.pharmacyIntegrate.Err);
                                return -1;
                            }
                        }
                        else
                        {
                            if (this.MaterialInterface != null)
                            {
                                if (this.MaterialInterface.Apply(info.Prescription.Material, info, stockDept,FS.FrameWork.Management.PublicTrans.Trans) == -1)
                                {
                                    if (isLocalTrans)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                    }
                                    Err = Language.Msg("ͨ���ӿڽ���ԭ������ʱ��������") + this.preparationManager.Err;
                                    return -1;
                                }
                            }
                        }
                    }

                    #endregion

                    info.ExecOutput = false;
                }
                else
                {
                    #region ���۳�

                    //����ԭ�Ͽۿ�
                    if (info.Prescription.MaterialType == FS.HISFC.Models.Preparation.EnumMaterialType.Material)     //ҩƷ
                    {
                        FS.HISFC.Models.Pharmacy.Item tempItem = this.pharmacyIntegrate.GetItem(info.Prescription.Material.ID);
                        if (tempItem == null)
                        {
                            if (isLocalTrans)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            Err = Language.Msg("����ҩƷԭ�ϱ����ȡԭ������Ϣʧ�ܣ� ") + this.pharmacyIntegrate.Err;
                            return -1;
                        }
                        if (this.pharmacyIntegrate.ProduceOutput(tempItem, info, stockDept) == -1)
                        {
                            if (isLocalTrans)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            Err = this.pharmacyIntegrate.Err;
                            return -1;
                        }
                    }
                    else
                    {
                        if (this.MaterialInterface != null)
                        {
                            if (this.MaterialInterface.Output(info.Prescription.Material, info, stockDept, FS.FrameWork.Management.PublicTrans.Trans) == -1)
                            {
                                if (isLocalTrans)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                }
                                Err = Language.Msg("ͨ���ӿڽ���ԭ�Ͽ��۳�ʧ�ܣ� ");
                                return -1;
                            }
                        }
                    }
                    #endregion

                    info.ExecOutput = true;
                }

                if (this.preparationManager.SetExpand(info) == -1)
                {
                    if (isLocalTrans)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    Err = Language.Msg("����������Ϣʱ��������") + this.preparationManager.Err;
                    return -1;
                }
            }

            if (isLocalTrans)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            return 1;
        }
        
        /// <summary>
        /// ����������Ϣ����
        /// </summary>
        /// <param name="isLocalTrans">�Ƿ񱾵�(�����ڲ�)��������</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <returns></returns>
        internal int SaveExpandInfo(bool isLocalTrans,ref string msg)
        {
            msg = "";
            if (this.fsMaterial_Sheet1.Rows.Count <= 0)
            {
                return 0;
            }

            if (isLocalTrans)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            }
            this.preparationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            
            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.fsMaterial_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Expand expand = (this.fsMaterial_Sheet1.Rows[i].Tag as FS.HISFC.Models.Preparation.Expand).Clone();

                if (expand.PlanNO == null || expand.PlanNO == "")
                {
                    if (isLocalTrans)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    msg = Language.Msg("���ȱ���ƻ���Ϣ�ٽ���������Ϣ����");
                    return -1;
                }

                expand.FacutalExpand = FS.FrameWork.Function.NConvert.ToDecimal(this.fsMaterial_Sheet1.Cells[i, (int)ExpandColumnSet.ColFactualExpand].Text);
                expand.Prescription.OperEnv.OperTime = sysTime;
                expand.Prescription.OperEnv.ID = this.preparationManager.Operator.ID;

                if (this.preparationManager.SetExpand(expand) == -1)
                {
                    if (isLocalTrans)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    msg = Language.Msg("����������Ϣʱ��������") + this.preparationManager.Err;
                    return -1;
                }
            }

            if (isLocalTrans)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                msg = Language.Msg("������Ϣ���óɹ�");
            }

            if (this.ExpandDataFinishEvent != null)
            {
                this.ExpandDataFinishEvent(this, System.EventArgs.Empty);
            }

            return 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            this.SaveExpandInfo(true,ref msg);

            MessageBox.Show(msg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region ��̬��������

        /// <summary>
        /// �Ƽ�������Ϣ��ȡ
        /// </summary>
        /// <param name="preparationManager">�Ƽ�����ҵ���</param>
        /// <param name="preparation">�Ƽ���Ʒ��Ϣ</param>
        /// <returns>�ɹ������Ƽ�������Ϣ ʧ�ܷ���null</returns>
        internal static List<PObject.Expand> QueryExpandList(PManager preparationManager, PObject.Preparation preparation)
        {
            List<FS.HISFC.Models.Preparation.Expand> expandList = preparationManager.QueryExpand(preparation,null);
            if (expandList == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ�Ƽ�������Ϣ��������") + preparationManager.Err);
                return null;
            }

            return expandList;
        }

        #endregion

        private enum ExpandColumnSet
        {
            /// <summary>
            /// ԭ������
            /// </summary>
            ColMaterialName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ����
            /// </summary>
            ColPrice,
            /// <summary>
            /// ��׼������
            /// </summary>
            ColNormativeQty,
            /// <summary>
            /// ����������
            /// </summary>
            ColPlanExpand,
            /// <summary>
            /// �����
            /// </summary>
            ColStore,
            /// <summary>
            /// ʵ��������
            /// </summary>
            ColFactualExpand,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo
        }
    }
}
