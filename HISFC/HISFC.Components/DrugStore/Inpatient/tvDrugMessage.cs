using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// <br></br>
    /// [��������: ��ҩ֪ͨ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <˵��>
    ///     1��ҩ�����˵�� 
    ///         ���ѡ���ҩ̨��ʾ��ʽΪ���һ��� ��ô�Ͳ���ʵ��ҩ��ֻ������ҩ��ҩƷ�Ĺ���
    ///         �б�����Ϊ���еİ�ҩ����ȷ�ϵ�ʱ�� ֱ�Ӷ�����ҩƷ������ȷ�Ϸ��͡�
    ///         �����ҩ�������ô��ʾ��ʽֻ��Ϊ������ϸ������ϸ
    ///     2�������ϸ����ʱ
    ///         ֻ������ҩ������ҩƷ  �����ҩ����˵ ���Լ�����ȫ�����ҩƷ
    ///                                 ���ڷ�ҩ����˵ ���Լ�������ҩ�������ҩƷ
    ///         ��֤���ظ�
    /// {41EEF22D-1ADE-446d-9C9F-37E591795607}   �޸��˵����б���ʾ��ʽ
    /// </˵��>
    /// </summary>
    public partial class tvDrugMessage : Common.Controls.baseTreeView
    {
        public tvDrugMessage()
        {
            InitializeComponent();

            //if (!this.DesignMode)
            //    this.Init();
        }

        public tvDrugMessage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            //if (!this.DesignMode)
            //    this.Init();
        }

        public delegate void SelectDataHandler(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, ArrayList alData,bool isShowDetail);

        public event SelectDataHandler SelectDataEvent;

        #region �����

        /// <summary>
        /// �Ƿ��Զ���ӡ��ҩ��
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// ��ҩ����ʽ�Ƿ��ӡ��ǩ
        /// </summary>
        private bool isPrintLabel = false;

        /// <summary>
        /// ��ʾʱ�Ƿ��տ���������ʾ
        /// </summary>
        private bool isDeptFirst = true;

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩ��������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ǰϵͳʱ��
        /// </summary>
        private DateTime sysDate = System.DateTime.MinValue;

        /// <summary>
        /// �Ѿ���ӡ���Ļ�������
        /// </summary>
        private static System.Collections.Hashtable hsPrint = null;

        /// <summary>
        /// ��ǰ��������ҩ̨ʵ��
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugControl drugControl = new DrugControl();

        /// <summary>
        /// �����Ϣ
        /// </summary>
        private System.Collections.Hashtable hsStockData = null;

        /// <summary>
        /// �����Ϣ ֻ��ʾ�������п���������Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject stockMark = null;

        /// <summary>
        /// ��׼����
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveDept = null;

        /// <summary>
        /// �Ƿ��ӡ���ݵ�ͬʱ�����ҩ����
        /// </summary>
        private bool isPrintAndOutput = false;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ��ӡ��ǩ
        /// </summary>
        [Description("��ӡʱ�Ƿ��ӡ��ǩ"), Category("����"), DefaultValue(false)]
        public bool IsPrintLabel
        {
            get
            {
                return this.isPrintLabel;
            }
            set
            {
                this.isPrintLabel = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ���ӡ��ҩ��
        /// </summary>
        [Description("�Ƿ��Զ���ӡ��ҩ��"), Category("����"), DefaultValue(false)]
        public bool IsAutoPrint
        {
            get
            {
                return this.isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        /// <summary>
        /// ��ʾʱ�Ƿ��տ���������ʾ
        /// </summary>
        [Description("��ҩ���б���ʾʱ �Ƿ��տ���������ʾ �ò���Ӱ���ҩ֪ͨ����ʾ"), Category("����"), DefaultValue(true)]
        public bool IsDeptFirst
        {
            get
            {
                return this.isDeptFirst;
            }
            set
            {
                this.isDeptFirst = value;
            }
        }

        /// <summary>
        /// �Ѿ���ӡ���Ļ�������
        /// </summary>
        public static System.Collections.Hashtable HsPrintData
        {
            get
            {
                return tvDrugMessage.hsPrint;
            }
            set
            {
                tvDrugMessage.hsPrint = value;
            }
        }

        /// <summary>
        /// ��ǰ��������ҩ̨ʵ��
        /// </summary>
        public FS.HISFC.Models.Pharmacy.DrugControl OperDrugControl
        {
            set
            {
                this.drugControl = value;
            }
        }

        /// <summary>
        /// �����Ϣ ֻ��ʾ�������п���������Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject StockMarkDept
        {
            get
            {
                return this.stockMark;
            }
            set
            {
                this.stockMark = value;

                this.hsStockData = null;
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
            try
            {
                this.ImageList = this.deptImageList;

                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.GetDeptmentAll();
                if (alDept == null)
                {
                    MessageBox.Show("���ؿ����б�������" + deptManager.Err);
                }

                objHelper.ArrayObject = alDept;

                this.sysDate = deptManager.GetDateTimeFromSysDateTime().Date;

                this.InitControlParam();
            }
            catch (Exception ex)
            {
                MessageBox.Show("����֪ͨ�б��ʼ����������" + ex.Message);
            }
        }

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.IsDeptFirst = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Show_DeptFirst, true, true);

            //�������ϲ���������Ϣ
            //this.isPrintAndOutput = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_AutoPrint_Output, true, false);
        }

        /// <summary>
        /// �������ڵ�չ��״̬
        /// </summary>
        /// <param name="expandLevel">չ���ڵ��� 0 1</param>
        /// <param name="expandAllNode">�Ƿ��ȫ���ڵ�չ�� ���� ֻչ����һ���ڵ�</param>
        public void SetExpand(int expandLevel,bool expandAllNode)
        {
            if (expandAllNode)
            {
                this.ExpandAll();
                return;
            }

            if (this.Nodes.Count <= 0)
                return;

            switch (expandLevel)
            {
                case 0:
                    this.Nodes[0].Expand();
                    break;
                case 1:
                    if (this.Nodes[0].Nodes.Count > 0)
                        this.Nodes[0].Nodes[0].Expand();
                    break;
            }            
        }

        /// <summary>
        /// ������ҩ̨ʵ�� �������͵�����ҩ̨�ĵ���
        /// </summary>
        /// <param name="drugControl">��ҩ̨</param>
        public virtual void ShowList(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            ArrayList al = this.drugStoreManager.QueryDrugMessageList(drugControl);

            if (al == null)
            {
                MessageBox.Show(Language.Msg("������ҩ̨�������͵���̨�ĵ����б�ʧ��") + this.drugStoreManager.Err);
            }

            if (drugControl.ShowLevel == 3)
            {
                this.ShowListForInpatientFirst(al);
            }
            else
            {
                this.ShowList(al, drugControl.ShowLevel);
            }
        }

        /// <summary>
        /// ���ݴ���İ�ҩ֪ͨ���飬��ʾ��tvDrugMessage��
        /// ������������ǰ��տ��ҡ���ҩ�����͡�����ʱ�䣨���������
        /// </summary>
        /// <param name="alDrugMessage">��ҩ֪ͨ����</param>
        /// <param name="showLevel">��ʾ�ȼ�</param>
        protected virtual void ShowList(ArrayList alDrugMessage, int showLevel)
        {
            //if (!this.isListAddType)
            this.Nodes.Clear();

            string privBillCode = "";
            string privDeptCode = "";				//��һ������
            FS.HISFC.Models.Pharmacy.DrugMessage privInfo = new FS.HISFC.Models.Pharmacy.DrugMessage();
            DrugMessageTreeNode nodePatient;
            DrugMessageTreeNode nodeDept = new DrugMessageTreeNode();
            DrugMessageTreeNode nodeBill = new DrugMessageTreeNode();

            foreach (DrugMessage info in alDrugMessage)
            {

                #region ÿ�ν��ڵ�����������

                this.SuspendLayout();

                if (this.isDeptFirst)
                {
                    #region ֻ��ʾ�����ҽڵ�
                    if (info.ApplyDept.ID != privDeptCode)		//����µĿ��ҽڵ�
                    {
                        nodeDept = GetNodeDept(info, null);

                        privDeptCode = info.ApplyDept.ID;
                        privInfo = new FS.HISFC.Models.Pharmacy.DrugMessage();
                        privBillCode = "";
                    }
                    if (showLevel == 0) continue;			//���ֻ��ʾһ�� �����
                    #endregion

                    #region ֻ��ʾ����ҩ���ڵ�
                    if (info.DrugBillClass.ID != privBillCode)	//����µİ�ҩ���ڵ�
                    {
                        nodeBill = this.GetNodeBill(info, nodeDept);

                        privBillCode = info.DrugBillClass.ID;	//������һ�εİ�ҩ����
                    }
                    if (showLevel == 1) continue;			//�������ʾ������Ϣ �����
                    #endregion

                    #region ��ʾ������Ϣ �����Ϣ��ͬ�����
                    if (info.ApplyDept.ID == privInfo.ApplyDept.ID && info.StockDept.ID == privInfo.StockDept.ID
                        && info.DrugBillClass.ID == privInfo.DrugBillClass.ID && info.SendType == privInfo.SendType)
                        continue;
                    privInfo = info;
                    List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                    if (neuObjectList == null)
                    {
                        MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                        return;
                    }
                    foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                    {
                        nodePatient = this.GetNodePatient(obj, nodeBill);

                        this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                    }
                    #endregion
                }
                else
                {
                    #region ���հ�ҩ��������ʾ

                    //��Ӱ�ҩ�������б�������ڵ��Ѿ�������ظ����
                    if (info.DrugBillClass.ID != privBillCode)
                    {
                        nodeBill = this.GetNodeBill(info, null);

                        privDeptCode = "";  //��տ��ұ���������������һ�Ű�ҩ���еĿ����ظ�

                        //{22E638FE-2821-4bdf-A8A9-5BD25D51742E} ��ҩ������ʱ��������
                        privBillCode = info.DrugBillClass.ID;
                    }

                    //�����ʾ�ȼ�Ϊ0����ʾ���һ��ܣ����򲻽�������Ĵ���
                    if (showLevel == 0)
                    {
                        continue;
                    }

                    //��ӿ����б�������ڵ��Ѿ�������ظ����
                    if (info.ApplyDept.ID != privDeptCode)
                    {
                        nodeDept = this.GetNodeDept(info, nodeBill);
                        privDeptCode = info.ApplyDept.ID;   //��������һ����ӵĿ��ҽڵ�

                        //�����ʾ�ȼ�Ϊ1����ʾ������ϸ�����򲻽�������Ĵ���
                        if (showLevel == 1) continue;

                        //��ӻ����б�(������Ϣ��סԺ��ˮ��ID������Name������Memo)
                        List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                        if (neuObjectList == null)
                        {
                            MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                            return;
                        }
                        foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                        {
                            nodePatient = this.GetNodePatient(obj, nodeDept);

                            this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                        }
                    }

                    #endregion
                }

                this.ResumeLayout();

                #endregion

                #region ����ԭ���ۼӷ�ʽ���� ��������

                //if (this.isListAddType)
                //{
                //    #region ÿ���ۼӷ�ʽ

                //    if (this.isDeptFirst)
                //    {
                //        #region ÿ���ۼƷ�ʽ �������������

                //        nodeDept = this.FindNode(info, this.Nodes, "0");
                //        if (nodeDept == null)
                //            nodeDept = GetNodeDept(info, null);

                //        if (showLevel == 0) continue;			//���ֻ��ʾһ�� �����

                //        nodeBill = this.FindNode(info, nodeDept.Nodes, "1");
                //        if (nodeBill == null)
                //            nodeBill = this.GetNodeBill(info, nodeDept);

                //        if (showLevel == 1) continue;			//�������ʾ������Ϣ �����

                //        List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                //        if (neuObjectList == null)
                //        {
                //            MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                //            return;
                //        }
                //        foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                //        {
                //            nodePatient = this.FindNode(obj, nodeBill.Nodes);
                //            if (nodePatient == null)
                //            {
                //                nodePatient = this.GetNodePatient(obj, nodeBill);
                //                this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                //            }
                //        }

                //        #endregion
                //    }
                //    else
                //    {
                //        #region ÿ���ۼƷ�ʽ �������������

                //        nodeBill = this.FindNode(info, this.Nodes, "1");
                //        if (nodeBill == null)
                //            nodeBill = this.GetNodeBill(info, null);

                //        if (showLevel == 0) continue;

                //        nodeDept = this.FindNode(info, nodeBill.Nodes, "1");
                //        if (nodeDept == null)
                //            nodeDept = this.GetNodeDept(info, null);

                //        if (showLevel == 1) continue;

                //        List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                //        if (neuObjectList == null)
                //        {
                //            MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                //            return;
                //        }
                //        foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                //        {
                //            nodePatient = this.FindNode(obj, nodeDept.Nodes);
                //            if (nodePatient == null)
                //            {
                //                nodePatient = this.GetNodePatient(obj, nodeDept);
                //                this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                //            }
                //        }

                //        #endregion
                //    }

                //    #endregion
                //}
                //else
                //{
                //    #region ÿ�ν��ڵ�����������

                //    this.SuspendLayout();

                //    if (this.isDeptFirst)
                //    {
                //        #region ֻ��ʾ�����ҽڵ�
                //        if (info.ApplyDept.ID != privDeptCode)		//����µĿ��ҽڵ�
                //        {
                //            nodeDept = GetNodeDept(info, null);

                //            privDeptCode = info.ApplyDept.ID;
                //            privInfo = new FS.HISFC.Models.Pharmacy.DrugMessage();
                //            privBillCode = "";
                //        }
                //        if (showLevel == 0) continue;			//���ֻ��ʾһ�� �����
                //        #endregion

                //        #region ֻ��ʾ����ҩ���ڵ�
                //        if (info.DrugBillClass.ID != privBillCode)	//����µİ�ҩ���ڵ�
                //        {
                //            nodeBill = this.GetNodeBill(info, nodeDept);

                //            privBillCode = info.DrugBillClass.ID;	//������һ�εİ�ҩ����
                //        }
                //        if (showLevel == 1) continue;			//�������ʾ������Ϣ �����
                //        #endregion

                //        #region ��ʾ������Ϣ �����Ϣ��ͬ�����
                //        if (info.ApplyDept.ID == privInfo.ApplyDept.ID && info.StockDept.ID == privInfo.StockDept.ID
                //            && info.DrugBillClass.ID == privInfo.DrugBillClass.ID && info.SendType == privInfo.SendType)
                //            continue;
                //        privInfo = info;
                //        List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                //        if (neuObjectList == null)
                //        {
                //            MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                //            return;
                //        }
                //        foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                //        {
                //            nodePatient = this.GetNodePatient(obj, nodeBill);

                //            this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                //        }
                //        #endregion
                //    }
                //    else
                //    {
                //        #region ���հ�ҩ��������ʾ

                //        //��Ӱ�ҩ�������б�������ڵ��Ѿ�������ظ����
                //        if (info.DrugBillClass.ID != privBillCode)
                //        {
                //            nodeBill = this.GetNodeBill(info, null);

                //            privDeptCode = "";  //��տ��ұ���������������һ�Ű�ҩ���еĿ����ظ�
                //        }

                //        //�����ʾ�ȼ�Ϊ0����ʾ���һ��ܣ����򲻽�������Ĵ���
                //        if (showLevel == 0) continue;

                //        //��ӿ����б�������ڵ��Ѿ�������ظ����
                //        if (info.ApplyDept.ID != privDeptCode)
                //        {
                //            nodeDept = this.GetNodeDept(info, nodeBill);
                //            privDeptCode = info.ApplyDept.ID;   //��������һ����ӵĿ��ҽڵ�

                //            //�����ʾ�ȼ�Ϊ1����ʾ������ϸ�����򲻽�������Ĵ���
                //            if (showLevel == 1) continue;

                //            //��ӻ����б�(������Ϣ��סԺ��ˮ��ID������Name������Memo)
                //            List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                //            if (neuObjectList == null)
                //            {
                //                MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                //                return;
                //            }
                //            foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                //            {
                //                nodePatient = this.GetNodePatient(obj, nodeDept);

                //                this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                //            }
                //        }

                //        #endregion
                //    }

                //    this.ResumeLayout();

                //    #endregion
                //}

                #endregion
            }
        }

        /// <summary>
        /// ���һ���
        /// </summary>
        /// <param name="inpatientNO">סԺ������ˮ��</param>
        public virtual void FindPatient(string inpatientNO)
        {
            if (this.Nodes.Count > 0)
            {
                foreach (DrugMessageTreeNode node in this.Nodes)
                {
                    if (node.Nodes != null && node.Nodes.Count > 0)
                    {
                        foreach (DrugMessageTreeNode chileNode in node.Nodes)
                        {
                            if (chileNode.Nodes != null && chileNode.Nodes.Count > 0)
                            {
                                foreach (DrugMessageTreeNode patientNode in chileNode.Nodes)
                                {
                                    FS.FrameWork.Models.NeuObject patient = patientNode.Tag as FS.FrameWork.Models.NeuObject;
                                    if (patient == null)
                                    {
                                        continue;
                                    }

                                    if (patient.ID == inpatientNO)
                                    {
                                        this.SelectedNode = patientNode;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual int AutoSave(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            //�������Ұ�ҩ������ϸ����
            ArrayList al = this.itemManager.QueryApplyOutList(drugMessage);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("���ݰ�ҩ֪ͨ��Ϣ��ȡ��ҩ������ϸ��Ϣ�������� ") + this.itemManager.Err);
                return -1;
            }
            if (drugMessage.DrugBillClass.ID == "R")
            {
                if (DrugStore.Function.DrugReturnConfirm(al, drugMessage, this.StockMarkDept, this.approveDept) != 1)
                    return -1;
            }
            else
            {
                if (DrugStore.Function.DrugConfirm(al, drugMessage, this.StockMarkDept, this.approveDept) != 1)
                    return -1;
            }

            return 1;
        }

        #region ���ڵ�����/���ҷ���

        /// <summary>
        /// ��ȡ��ҩ���б���ʾʱ���߽ڵ�
        /// </summary>
        /// <param name="nodeBill">�����ڵ�</param>
        /// <param name="obj">������Ϣ</param>
        /// <returns>�ɹ����ذ�ҩ����ʾʱ�Ļ��߽ڵ�</returns>
        private DrugMessageTreeNode GetNodePatient(FS.FrameWork.Models.NeuObject obj,DrugMessageTreeNode parentNode)
        {
            foreach (DrugMessageTreeNode childNode in parentNode.Nodes)
            {
                if (childNode.Text == "��" + obj.Memo + "��" + obj.Name)
                {
                    return childNode;
                }
            }

            DrugMessageTreeNode nodePatient;
            nodePatient = new DrugMessageTreeNode();
            nodePatient.Text = "��" + obj.Memo + "��" + obj.Name;  //�����š�����
            nodePatient.ImageIndex = 6;
            nodePatient.SelectedImageIndex = 6;
            nodePatient.Tag = obj;
            nodePatient.NodeType = DrugMessageNodeType.Patient;
            if (parentNode == null)
                this.Nodes.Add(nodePatient);
            else
                parentNode.Nodes.Add(nodePatient);
            return nodePatient;
        }

        /// <summary>
        /// ��ȡ��ҩ���б���ʾʱ���ҽڵ�
        /// </summary>
        /// <param name="info">��ҩ֪ͨ��Ϣ</param>
        /// <returns>�ɹ����ذ�ҩ���б���ʾʱ�Ŀ��ҽڵ�</returns>
        private DrugMessageTreeNode GetNodeDept(FS.HISFC.Models.Pharmacy.DrugMessage info,DrugMessageTreeNode parentNode)
        {
            DrugMessageTreeNode nodeDept = new DrugMessageTreeNode();
            if (info.ApplyDept.Name == "")
                info.ApplyDept.Name = objHelper.GetName(info.ApplyDept.ID);
            nodeDept.Text = info.ApplyDept.Name;
            nodeDept.ImageIndex = 0;
            nodeDept.Tag = info;
            nodeDept.NodeType = DrugMessageNodeType.ApplyDept;
            if (parentNode == null)
                this.Nodes.Add(nodeDept);
            else
                parentNode.Nodes.Add(nodeDept);
            return nodeDept;
        }

        /// <summary>
        /// ��ȡ��ҩ���б���ʾʱ�İ�ҩ���ڵ�
        /// </summary>
        /// <param name="info">��ҩ֪ͨ��Ϣ</param>
        /// <param name="nodeDept">�����ڵ�</param>
        /// <returns>�ɹ����ذ�ҩ���б���ʾʱ�İ�ҩ���ڵ�</returns>
        private DrugMessageTreeNode GetNodeBill(FS.HISFC.Models.Pharmacy.DrugMessage info, DrugMessageTreeNode parentNode)
        {
            //{22E638FE-2821-4bdf-A8A9-5BD25D51742E}  ����У�� ����parentNodeΪnull
            if (parentNode != null)
            {
                foreach (DrugMessageTreeNode childNode in parentNode.Nodes)
                {
                    if (childNode.Text == info.DrugBillClass.Name)
                    {
                        return childNode;
                    }
                }
            }

            DrugMessageTreeNode nodeBill = new DrugMessageTreeNode();
            nodeBill.Text = info.DrugBillClass.Name;
            nodeBill.ImageIndex = 4;
            nodeBill.SelectedImageIndex = 5;
            nodeBill.Tag = info;
            nodeBill.NodeType = DrugMessageNodeType.DrugBill;
            if (parentNode == null)
                this.Nodes.Add(nodeBill);
            else
                parentNode.Nodes.Add(nodeBill);		//��ӵ����ҽڵ�
            return nodeBill;
        }

        /// <summary>
        /// �ڽڵ㼯���ڲ����Ƿ��Ѵ��ڸ����ͽڵ� ������� �򷵻ظýڵ�
        /// </summary>
        /// <param name="info">��ҩ֪ͨ��Ϣ</param>
        /// <param name="nodeCollection">���ҵĽڵ㼯��</param>
        /// <param name="findType">������� 0 ���� 1 ����</param>
        /// <returns>�ɹ��򷵻ظ����ͽڵ� �����ڷ���null</returns>
        private DrugMessageTreeNode FindNode(FS.HISFC.Models.Pharmacy.DrugMessage info, TreeNodeCollection nodeCollection,string findType)
        {
            if (nodeCollection == null || nodeCollection.Count == 0)
                return null;
            foreach (DrugMessageTreeNode node in nodeCollection)
            {
                switch (findType)
                {
                    case "0":
                        if ((node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage).ApplyDept.ID == info.ApplyDept.ID)
                            return node;
                        break;
                    case "1":
                        if ((node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage).DrugBillClass.ID == info.DrugBillClass.ID)
                            return node;
                        break;
                }
            }
            return null;
        }

        /// <summary>
        /// �ڽڵ㼯���ڲ����Ƿ��Ѵ��ڸ����ͽڵ� ������� �򷵻ظýڵ�
        /// </summary>
        /// <param name="info">������Ϣ</param>
        /// <param name="nodeCollection">���ҵĽڵ㼯��</param>
        /// <returns>�ɹ��򷵻ظ����ͽڵ� �����ڷ���null</returns>
        private DrugMessageTreeNode FindNode(FS.FrameWork.Models.NeuObject info, TreeNodeCollection nodeCollection)
        {
            if (nodeCollection == null || nodeCollection.Count == 0)
                return null;
            foreach (DrugMessageTreeNode node in nodeCollection)
            {
                if (node.Text == "��" + info.User03 + "��" + info.User02)
                    return node;
            }
            return null;
        }

        #endregion

        /// <summary>
        /// �ж��Ƿ���Ҫ��ӡ ��δ��ӡ�Ļ��߻�ȡ���ݽ��д�ӡ
        /// </summary>
        /// <param name="judgeKey">�жϵ���������</param>
        /// <param name="judgeMessage">��ҩ֪ͨ��Ϣ</param>
        /// <param name="judgePatientNO">����סԺ��</param>
        protected virtual void JudgePrint(string judgeKey, FS.HISFC.Models.Pharmacy.DrugMessage judgeMessage, string judgePatientNO)
        {
            if (!this.isAutoPrint)
                return;

            //if (tvDrugMessage.hsPrint != null && tvDrugMessage.hsPrint.ContainsKey(judgeKey))

            if (tvDrugMessage.hsPrint == null)
            {
                tvDrugMessage.hsPrint = new Hashtable();
            }

            #region ����ӡ���ݻ�ȡ

            judgeMessage.User01 = judgePatientNO;		//���ӡ����סԺ��ˮ��
            //�������߰�ҩ������ϸ����
            ArrayList al = this.itemManager.QueryApplyOutListByPatient(judgeMessage);
            if (al == null)
            {
                MessageBox.Show("��ȡ" + judgePatientNO + "�Ĵ���ӡ����ʱ��������");
                return;
            }

            //���δ˴��ж� ͬһ�ŵ��ݰ�����������ʱ �ж�������
            //if (al.Count > 0)
            //{
            //    FS.HISFC.Models.Pharmacy.ApplyOut info = al[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
            //    if (info != null)
            //    {
            //        //�ǵ������ݲ����ӡ
            //        if (info.Operation.ApplyOper.OperTime < this.sysDate)
            //        {
            //            tvDrugMessage.hsPrint.Add(judgeKey, null);
            //            return;
            //        }
            //    }
            //}

            //ȡ������δ��ӡ����
            ArrayList alPrintData = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut printInfo in al)
            {
                //�Էǵ������ݲ���ӡ
                if (printInfo.Operation.ApplyOper.OperTime < this.sysDate)
                {
                    continue;
                }

                if (printInfo.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid && printInfo.PrintState != "1")
                {
                    alPrintData.Add(printInfo);
                }
            }

            #endregion

            //�޴���ӡ���� ֱ�ӷ���
            if (alPrintData.Count == 0)
            {
                return;
            }

            if (this.isPrintLabel)
            {
                #region ��ǩ��ӡ

                if (Function.PrintLabelForOutpatient(alPrintData) != -1)
                {
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alPrintData)
                    {
                        info.PrintState = "1";			//�ñ�����������Ƿ��ѶԵ��ݽ��й���ӡ
                        if (this.itemManager.UpdateApplyOut(info) == -1)
                        {
                            MessageBox.Show("���´�ӡ��Ƿ�������");
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region ��ҩ����ӡ

                FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = this.drugStoreManager.GetDrugBillClass(judgeMessage.DrugBillClass.ID);
                drugBillClass.Memo = judgeMessage.DrugBillClass.Memo;//��ҩ����

                if (Function.PrintBill(alPrintData, drugBillClass) != -1)
                {
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alPrintData)
                    {
                        info.PrintState = "1";			//�ñ�����������Ƿ��ѶԵ��ݽ��й���ӡ
                        if (this.itemManager.UpdateApplyOutPrintState(info.ID, true) == -1)
                        {
                            MessageBox.Show("���´�ӡ��Ƿ�������");
                        }
                    }
                }

                #endregion
            }

            tvDrugMessage.hsPrint.Add(judgeKey, null);

            //���δ�ӡ�Զ����湦��
            //if (this.isPrintAndOutput)
            //{
            //    this.AutoSave(judgeMessage);
            //}
        }

        /// <summary>
        /// ��ȡѡ�еĽڵ�İ�ҩ������Ϣ
        /// </summary>
        /// <param name="selectNode">��ǰѡ�нڵ�</param>
        /// <param name="drugMessage">��ҩ֪ͨ��Ϣ</param>
        /// <param name="al">��ҩ��ϸ��Ϣ</param>
        /// <param name="treeLevel">���ڵ���</param>
        protected virtual void GetSelectData(DrugMessageTreeNode selectNode,ref FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, ref ArrayList al,ref bool isDetail)
        {
            if (selectNode == null)
            {
                return;
            }

            #region ���ҩƷ�б��ȡ ����ҩ�������ж� ��ȡһ��

            if (stockMark != null && stockMark.ID != "")
            {
                //��ȡ���ҿ��ҩƷ��Ϣ
                if (this.hsStockData == null)
                {
                    this.hsStockData = new Hashtable();

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ؿ��ҿ����Ϣ ���Ժ�..."));
                    Application.DoEvents();

                    ArrayList alStorage = this.itemManager.QueryStockinfoList(stockMark.ID);

                    foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorage)
                    {
                        if (storage.IsArkManager)
                        {
                            continue;
                        }
                        this.hsStockData.Add(storage.Item.ID,null);
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }

            #endregion

            int treeLevel = selectNode.Level;
            drugMessage = selectNode.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;

            ArrayList alTotal = new ArrayList();

            switch (treeLevel)
            {
                case 0:             //��һ��  NodeType ����Ϊ ���ҡ�����
                    if (this.isDeptFirst)
                    {
                        alTotal = this.drugStoreManager.QueryDrugBillList(this.drugControl.ID, drugMessage);
                        isDetail = false;
                    }
                    else
                    {
                        alTotal = this.drugStoreManager.QueryDrugMessageList(drugMessage);
                        isDetail = false;
                    }
                    break;
                case 1:            //�ڶ��� NodeType ����Ϊ ���ҡ����ݡ�����
                    if (selectNode.NodeType == DrugMessageNodeType.Patient)     //�ڵ�Ϊ���� ���⴦�� ȡ�����ӽڵ��ҩ�����д���
                    {
                        FS.FrameWork.Models.NeuObject patientTag = selectNode.Tag as FS.FrameWork.Models.NeuObject;
                        ArrayList alTempTotal = new ArrayList();
                        foreach (DrugMessageTreeNode childNode in selectNode.Nodes)
                        {
                            drugMessage = childNode.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                            if (drugMessage != null)
                            {                               
                                drugMessage.User01 = patientTag.ID;
                                alTempTotal = this.itemManager.QueryApplyOutListByPatient(drugMessage);
                            }

                            alTotal.AddRange(alTempTotal);
                        }
                    }
                    else
                    {
                        alTotal = this.itemManager.QueryApplyOutList(drugMessage);
                    }

                    isDetail = true;

                    break;
                case 2:         //������ NodeType ����Ϊ ���ݡ�����
                    if (selectNode.NodeType == DrugMessageNodeType.Patient)
                    {
                        drugMessage = selectNode.Parent.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                        FS.FrameWork.Models.NeuObject patientObj = selectNode.Tag as FS.FrameWork.Models.NeuObject;
                        drugMessage.User01 = patientObj.ID;
                        alTotal = this.itemManager.QueryApplyOutListByPatient(drugMessage);
                        isDetail = true;
                    }
                    else       //�ڵ�Ϊ����ʱ
                    {
                        drugMessage = selectNode.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                        FS.FrameWork.Models.NeuObject patientObj = selectNode.Parent.Tag as FS.FrameWork.Models.NeuObject;
                        drugMessage.User01 = patientObj.ID;
                        alTotal = this.itemManager.QueryApplyOutListByPatient(drugMessage);
                        isDetail = true;
                    }
                    break;
            }

            #region ҩ�����ҩƷ�Ĵ���

            if (this.hsStockData == null)
            {
                al = alTotal;
            }
            else
            {
                //����ҩ������ҩƷ
                if (alTotal.Count > 0)
                {
                    if (alTotal[0] is FS.HISFC.Models.Pharmacy.ApplyOut)
                    {
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alTotal)
                        {
                            if (this.hsStockData.ContainsKey(info.Item.ID))
                            {
                                al.Add(info);
                            }
                        }
                    }
                    else
                    {
                        al = alTotal;
                    }
                }
            }

            #endregion
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new DrugMessage();
            ArrayList al = new ArrayList();
            bool isDetail = true;
            this.GetSelectData(e.Node as DrugMessageTreeNode,ref drugMessage,ref al,ref isDetail);

            if (al != null)
            {
                if (this.SelectDataEvent != null)
                    this.SelectDataEvent(drugMessage,al, isDetail);
            }

            base.OnAfterSelect(e);
        }

        /// <summary>
        /// ���ݴ���İ�ҩ֪ͨ���飬��ʾ��tvDrugMessage��
        /// ������������ǰ��տ��ҡ���ҩ�����͡�����ʱ�䣨���������
        /// </summary>
        /// <param name="alDrugMessage">��ҩ֪ͨ����</param>
        /// <param name="showLevel">��ʾ�ȼ�</param>
        public virtual void ShowListForDeptFirst(ArrayList alDrugMessage, int showLevel)
        {
            this.Nodes.Clear();

            string privBillCode = "";
            string privDeptCode = "";				//��һ������
            FS.HISFC.Models.Pharmacy.DrugMessage privInfo = new FS.HISFC.Models.Pharmacy.DrugMessage();
            DrugMessageTreeNode nodePatient;
            DrugMessageTreeNode nodeDept = new DrugMessageTreeNode();
            DrugMessageTreeNode nodeBill = new DrugMessageTreeNode();

            foreach (DrugMessage info in alDrugMessage)
            {
                #region ÿ�ν��ڵ�����������

                this.SuspendLayout();

                #region ֻ��ʾ�����ҽڵ�

                if (info.ApplyDept.ID != privDeptCode)		//����µĿ��ҽڵ�
                {
                    nodeDept = GetNodeDept(info, null);

                    privDeptCode = info.ApplyDept.ID;
                    privInfo = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    privBillCode = "";
                }
                if (showLevel == 0)
                {
                    continue;			//���ֻ��ʾһ�� �����
                }

                #endregion

                if (showLevel != 3)         //��ԭ��ʾ��ʽ��������
                {
                    #region ֻ��ʾ����ҩ���ڵ� �˴�����ʾ����

                    if (info.DrugBillClass.ID != privBillCode)	//����µİ�ҩ���ڵ�
                    {
                        nodeBill = this.GetNodeBill(info, nodeDept);

                        privBillCode = info.DrugBillClass.ID;	//������һ�εİ�ҩ����
                    }
                    if (showLevel == 1)
                    {
                        continue;			//�������ʾ������Ϣ �����
                    }

                    #endregion

                    #region ��ʾ������Ϣ �����Ϣ��ͬ�����

                    //�����ظ����
                    if (info.ApplyDept.ID == privInfo.ApplyDept.ID && info.StockDept.ID == privInfo.StockDept.ID
                        && info.DrugBillClass.ID == privInfo.DrugBillClass.ID && info.SendType == privInfo.SendType)
                    {
                        continue;
                    }
                    privInfo = info;
                    List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                    if (neuObjectList == null)
                    {
                        MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                        return;
                    }
                    foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                    {
                        nodePatient = this.GetNodePatient(obj, nodeBill);

                        this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                    }

                    #endregion
                }
                else
                {
                    //��ʾ������ϸ ��������������ʾ ������������ʾ������ϸ
                    DrugMessage tempMessage = info.Clone();
                    tempMessage.DrugBillClass.ID = "A";
                    List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(tempMessage);
                    if (neuObjectList == null)
                    {
                        MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                        return;
                    }

                    foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                    {
                        nodePatient = this.GetNodePatient(obj, nodeDept);

                        this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                    }
                }

                this.ResumeLayout();

                #endregion
            }
        }

        /// <summary>
        /// ���ݴ���İ�ҩ֪ͨ���飬��ʾ��tvDrugMessage��
        /// ������������ǰ��տ��ҡ���ҩ�����͡�����ʱ�䣨���������
        /// 
        /// ��ʾ���Ϊ ����/����/����
        /// </summary>
        /// <param name="alDrugMessage">��ҩ֪ͨ����</param>
        public virtual void ShowListForInpatientFirst(ArrayList alDrugMessage)
        {
            this.Nodes.Clear();

            string privPatientNO = "";
            string privDeptCode = "";				//��һ������
            FS.HISFC.Models.Pharmacy.DrugMessage privInfo = new FS.HISFC.Models.Pharmacy.DrugMessage();
            DrugMessageTreeNode nodePatient;
            DrugMessageTreeNode nodeDept = new DrugMessageTreeNode();
            DrugMessageTreeNode nodeBill = new DrugMessageTreeNode();

            this.SuspendLayout();

            foreach (DrugMessage info in alDrugMessage)
            {
                if (info.ApplyDept.ID != privDeptCode)		//����µĿ��ҽڵ� һ�����ҽڵ������Ӵ���һ��
                {
                    nodeDept = this.GetNodeDept(info, null);

                    privDeptCode = info.ApplyDept.ID;
                    privInfo = new FS.HISFC.Models.Pharmacy.DrugMessage();
                }

                //�����ظ����
                if (info.ApplyDept.ID == privInfo.ApplyDept.ID && info.StockDept.ID == privInfo.StockDept.ID
                    && info.DrugBillClass.ID == privInfo.DrugBillClass.ID && info.SendType == privInfo.SendType)
                {
                    continue;
                }

                //��ȡ������Ϣ
                List<FS.FrameWork.Models.NeuObject> neuObjectList = this.itemManager.QueryApplyOutPatientList(info);
                if (neuObjectList == null)
                {
                    MessageBox.Show("��ӻ��߽ڵ㣺" + this.itemManager.Err);
                    return;
                }
                foreach (FS.FrameWork.Models.NeuObject obj in neuObjectList)
                {
                    nodePatient = this.GetNodePatient(obj, nodeDept);

                    nodeBill = this.GetNodeBill(info, nodePatient);

                    this.JudgePrint(info.ApplyDept.ID + info.DrugBillClass.ID + obj.ID, info, obj.ID);
                }
            }

            this.ResumeLayout();
        }
    }
}
