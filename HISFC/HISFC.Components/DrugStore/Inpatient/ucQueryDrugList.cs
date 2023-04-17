using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// [�ؼ�����: סԺ��ҩ����ѯ,������ֲ]
    /// [�� �� ��: ����]
    /// [����ʱ��: 2010-8-10]
    /// </summary>
    public partial class ucQueryDrugList : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucQueryDrugList()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���ؼ���ʾ�Ľڵ�����
        /// </summary>
        private NodeType treeNodeType = NodeType.NurseCell;

        /// <summary>
        /// ��ҩ��ѯ��Χ
        /// </summary>
        private QueryRange queryDrugRange = QueryRange.AllDrug;        

        /// <summary>
        /// �Ƿ�����ҩƷ����
        /// </summary>
        private bool isFiterByDrug = false;

        /// <summary>
        /// �Ƿ��ʽ���п�
        /// </summary>
        private bool isFormatColumn = true;

        /// <summary>
        /// �������
        /// </summary>
        private string reportTitle = "סԺ��ҩ��";

        /// <summary>
        /// �����б�
        /// </summary>
        private string argDept = "";

        /// <summary>
        /// ��ѯ�����
        /// </summary>
        System.Data.DataTable dsDrugList = new DataTable();

        /// <summary>
        /// �Ƿ��ѯʱ���ڵ��Զ�չ��
        /// </summary>
        private bool isTreeExpandAll = true;

        /// <summary>
        /// �Ƿ���ʾ��Ч������
        /// </summary>
        private bool isShowInvalidData = false;

        /// <summary>
        /// �Ƿ�Բ�ѯ��Ϣ��ɫ��ʾ
        /// </summary>
        private bool isHintColor = false;

        /// <summary>
        /// ������Ϣ����ҵ����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ҩ������ҵ����
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.DrugStore drugstoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        FS.HISFC.BizLogic.Pharmacy.Item drugItem = new FS.HISFC.BizLogic.Pharmacy.Item();
        /// <summary>
        /// ҩƷ����ҵ����
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Constant conManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint printInterface = null;
        FS.HISFC.Models.Pharmacy.DrugBillClass billClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();
        #endregion

        #region ����

        /// <summary>
        /// ���ؼ���ʾ�Ľڵ�����
        /// </summary>
        [Description("���ؼ���ʾ�Ľڵ�����"),Category("������"),DefaultValue(ucQueryDrugList.NodeType.NurseCell)]
        public NodeType TreeNodeType
        {
            get 
            { 
                return treeNodeType; 
            }
            set 
            { 
                treeNodeType = value;
            }
        }

        /// <summary>
        /// ��ҩ��ѯ��Χ
        /// </summary>
        [Description("��ҩ��ѯ��Χ"), Category("����"), DefaultValue(ucQueryDrugList.QueryRange.NoneDrug)]
        public QueryRange QueryDrugRange
        {
            get 
            {
                return queryDrugRange; 
            }
            set 
            { 
                queryDrugRange = value; 
            }
        }

        /// <summary>
        /// �Ƿ�����ҩƷ����
        /// </summary>
        [Description("�Ƿ�����ҩƷ����"), Category("����"), DefaultValue(false)]
        public bool IsFiterByDrug
        {
            get 
            { 
                return isFiterByDrug; 
            }
            set 
            { 
                isFiterByDrug = value; 
            }
        }

        /// <summary>
        /// �Ƿ��ʽ���п�
        /// </summary>
        [Description("�Ƿ��ʽ���п�"), Category("�б�����"), DefaultValue(true)]
        public bool IsFormatColumn
        {
            get 
            { 
                return isFormatColumn; 
            }
            set 
            { 
                isFormatColumn = value; 
            }
        }

        /// <summary>
        /// �Ƿ��ѯʱ���ڵ��Զ�չ��
        /// </summary>
        [Description("�Ƿ��ʼ����ʱ�ڵ��Զ�չ��"), Category("������"), DefaultValue(true)]
        public bool IsTreeExpandAll
        {
            get 
            { 
                return isTreeExpandAll; 
            }
            set 
            { 
                isTreeExpandAll = value; 
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        [Description("�������"), Category("�б�����"), DefaultValue("������ԺסԺ��ҩ��")]
        public string ReportTitle
        {
            get 
            { 
                return reportTitle; 
            }
            set 
            { 
                reportTitle = value;
                this.lbTitle.Text = reportTitle;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��Ч������
        /// </summary>
        [Description("�Ƿ���ʾ��Ч������"), Category("����"), DefaultValue(false)]
        public bool IsShowInvalidData
        {
            get 
            {
                return isShowInvalidData;
            }
            set 
            { 
                isShowInvalidData = value; 
            }
        }

        /// <summary>
        /// �Ƿ�Բ�ѯ��Ϣ��ɫ��ʾ
        /// </summary>
        [Description("�Ƿ�Բ�ѯ��Ϣ��ɫ��ʾ"), Category("�б�����"), DefaultValue(false)]
        public bool IsHintColor
        {
            get 
            { 
                return isHintColor;
            }
            set 
            { 
                isHintColor = value; 
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void IniControls()
        {
            if (this.IsFiterByDrug)
            {
                this.neuPanel5.Visible = true;
            }
            else
            {
                this.neuPanel5.Visible = false;
            }

            if (this.QueryDrugRange == ucQueryDrugList.QueryRange.NoneDrug)
            {
                this.neuPanel6.Visible = false;
            }
            else
            {
                this.neuPanel6.Visible = true;
            }

            this.cbbPharmacy.Items.Clear();
            ArrayList al = new ArrayList();
            //al = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PHARMACYTYPE);            
            this.cbbPharmacy.AddItems(al);
            this.cbbPharmacy.Items.Insert(0, "");
            this.neuLabel8.Visible = false;
            this.cbbPharmacy.Visible = false;
            this.IniTreeView();
            this.cmbState.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private void IniTreeView()
        {
            if (this.treeNodeType == NodeType.NurseCell)
            {
                //��ȡ��ǰ��½�������ڵĲ���
                FS.FrameWork.Models.NeuObject nurseObj = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Nurse;
                this.tvSelectType.Nodes.Add("������" + nurseObj.Name + "��");
                this.tvSelectType.Nodes[0].Tag = nurseObj.ID;
                ////��ѯ�������°����Ŀ����б�
                //ArrayList alDept = managerIntegrate.QueryNurseStationByDept(nurseObj);
                //for (int i = 0; i < alDept.Count; i++)
                //{
                //    FS.FrameWork.Models.NeuObject tempObj = alDept[i] as FS.FrameWork.Models.NeuObject;
                //    if (argDept == "")
                //    {
                //        argDept = tempObj.ID;
                //    }
                //    else
                //    {
                //        argDept = argDept + "','" + tempObj.ID;
                //    }
                //}
                ArrayList alBills = new ArrayList();
                if (this.QueryDrugRange == ucQueryDrugList.QueryRange.NoneDrug)
                {
                    alBills = this.drugstoreManager.QueryBillListByDept(nurseObj.ID, this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.ToString(), "0", "0");
                }
                else if (this.QueryDrugRange == ucQueryDrugList.QueryRange.Druged)
                {
                    alBills = this.drugstoreManager.QueryBillListByDept(nurseObj.ID, this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.ToString(), "1", "1");
                }
                else
                {
                    alBills = this.drugstoreManager.QueryBillListByDept(nurseObj.ID, this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.ToString(), "0", "2");
                }
                for (int j = 0; j < alBills.Count; j++)
                {
                    FS.FrameWork.Models.NeuObject billObj = alBills[j] as FS.FrameWork.Models.NeuObject;
                    TreeNode tempTN = new TreeNode();
                    tempTN.Text = billObj.Name;
                    tempTN.Tag = billObj.ID;
                    tempTN.ToolTipText = "��ҩ��";
                    this.tvSelectType.Nodes[0].Nodes.Add(tempTN);
                }
            }
            else if (this.treeNodeType == NodeType.NurseDept)
            {
                //��ȡ��ǰ��½�������ڵĲ���
                FS.FrameWork.Models.NeuObject nurseObj = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Nurse;
                //FS.FrameWork.Models
                this.tvSelectType.Nodes.Add("������" + nurseObj.Name + "��");
                //��ѯ�������°����Ŀ����б�
                ArrayList alDept = managerIntegrate.QueryDepartment(nurseObj.ID);
                for (int i = 0; i < alDept.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject deptObj = alDept[i] as FS.FrameWork.Models.NeuObject;
                    TreeNode tempDeptNode = new TreeNode();
                    tempDeptNode.Text = deptObj.Name;
                    tempDeptNode.Tag = deptObj.ID;
                    tempDeptNode.ToolTipText = "����";

                    this.tvSelectType.Nodes[0].Nodes.Add(tempDeptNode);
                    if (argDept == "")
                    {
                        argDept = deptObj.ID;
                    }
                    else
                    {
                        argDept = argDept + "','" + deptObj.ID;
                    }

                    ArrayList alBills = new ArrayList();
                    if (this.QueryDrugRange == ucQueryDrugList.QueryRange.NoneDrug)
                    {
                        alBills = this.drugstoreManager.QueryBillListByDept(deptObj.ID, this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.ToString(), "0", "0");
                    }
                    else if (this.QueryDrugRange == ucQueryDrugList.QueryRange.Druged)
                    {
                        alBills = this.drugstoreManager.QueryBillListByDept(deptObj.ID, this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.ToString(), "1", "1");
                    }
                    else
                    {
                        alBills = this.drugstoreManager.QueryBillListByDept(deptObj.ID, this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.ToString(), "0", "2");
                    }
                    for (int j = 0; j < alBills.Count; j++)
                    {
                        FS.FrameWork.Models.NeuObject billObj = alBills[j] as FS.FrameWork.Models.NeuObject;
                        TreeNode tempTN = new TreeNode();
                        tempTN.Text = billObj.Name;
                        tempTN.Tag = billObj.ID;
                        tempTN.ToolTipText = "��ҩ��";
                        this.tvSelectType.Nodes[0].Nodes[i].Nodes.Add(tempTN);
                    }
                    //ArrayList alBills = this.drugstoreManager.QueryBillListByDept(deptObj.ID,);
                }                
            }
            else if (this.treeNodeType == NodeType.Dept)
            {
                //��ȡ����סԺ����
                ArrayList alInDept = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                for (int i = 0; i < alInDept.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject deptObj = alInDept[i] as FS.FrameWork.Models.NeuObject;
                    this.tvSelectType.Nodes.Add(deptObj.Name);
                } 
            }
            else if (this.treeNodeType == NodeType.Pharmacy)
            {
                //��ȡ��ǰ��½�������ڵĲ���
                FS.FrameWork.Models.NeuObject nurseObj = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept;                
                this.tvSelectType.Nodes.Add("���ң�" + nurseObj.Name + "��");
                ArrayList alPharmacy = this.conManager.QueryReciveDrugDept(nurseObj.ID);
                for (int i = 0; i < alPharmacy.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject deptObj = alPharmacy[i] as FS.FrameWork.Models.NeuObject;
                    this.tvSelectType.Nodes[0].Nodes.Add(deptObj.Name);
                }
            }
            else
            {
                //��ȡ��ǰ��½�������ڵĲ���
                FS.FrameWork.Models.NeuObject nurseObj = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept;
                this.tvSelectType.Nodes.Add("���ң�" + nurseObj.Name + "��");
            }

            //�ж������Ƿ��Զ�չ�����ڵ�
            if (this.IsTreeExpandAll)
            {
                this.tvSelectType.ExpandAll();
            }
        }

        /// <summary>
        /// ��ѯ��ҩ����Ϣ
        /// </summary>
        private void QueryDrugList()
        {
            this.ClearData();
            if (this.tvSelectType.SelectedNode != null)
            {
                if (this.tvSelectType.SelectedNode.ToolTipText == "��ҩ��")
                {
                    //{B5A791DF-585A-4763-96CE-8A9F11037D8B}feng.ch
                    if (this.rbTotal.Checked)
                    {
                        if (this.cmbState.Text == "�Ѱ�")
                        {
                            this.dsDrugList = this.drugstoreManager.QueryDrugTotalByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                              this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(),"2");
                        }
                        else
                        {
                            if (this.cmbState.Text == "δ��")
                            {
                                this.dsDrugList = this.drugstoreManager.QueryDrugTotalByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                                  this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(),"0,1");
                            }
                            //if (this.QueryDrugRange == ucQueryDrugList.QueryRange.NoneDrug)
                            //{
                            //    this.dsDrugList = this.drugstoreManager.QueryDrugTotalByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                            //        this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(), "0", "0,1");
                            //}
                            //else if (this.QueryDrugRange == ucQueryDrugList.QueryRange.Druged)
                            //{
                            //    //this.dsDrugList = new DataTable();
                            //    this.dsDrugList = this.drugstoreManager.QueryDrugTotalByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                            //        this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(), "1", "2");
                            //}
                            else
                            {
                                this.dsDrugList = this.drugstoreManager.QueryDrugTotalByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                                    this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(),"0,1,2");
                            }
                        }
                        this.lbTitle.Text = ReportTitle + "�����ܣ�";
                    }
                    else
                    {
                        //{B5A791DF-585A-4763-96CE-8A9F11037D8B}feng.ch
                        if (this.cmbState.Text == "δ��")
                        {
                            this.dsDrugList = this.drugstoreManager.QueryDrugDetailByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                            this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(),"0,1");
                        }
                        else
                        {
                            if (this.cmbState.Text == "�Ѱ�")
                            {
                                this.dsDrugList = this.drugstoreManager.QueryDrugDetailByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                                this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(),"2");
                            }
                            //if (this.QueryDrugRange == ucQueryDrugList.QueryRange.NoneDrug)
                            //{
                            //    this.dsDrugList = this.drugstoreManager.QueryDrugDetailByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                            //        this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(), "0", "0,1");
                            //}
                            //else if (this.QueryDrugRange == ucQueryDrugList.QueryRange.Druged)
                            //{
                            //    this.dsDrugList = this.drugstoreManager.QueryDrugDetailByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                            //        this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(), "1", "2");
                            //}
                            else
                            {
                                this.dsDrugList = this.drugstoreManager.QueryDrugDetailByDept(this.tvSelectType.SelectedNode.Tag.ToString(),
                                    this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(),"0,1,2");
                            }
                        }
                        this.lbTitle.Text = ReportTitle + "����ϸ��";
                    }
                    this.lbDept.Text = this.tvSelectType.SelectedNode.Parent.Text;
                    this.lbBillType.Text = this.tvSelectType.SelectedNode.Text;

                    DataView dvDrugList = new DataView(dsDrugList);
                    this.fpDrugList.DataSource = dvDrugList;
                    if (!this.rbTotal.Checked)
                    {
                        this.Filter();
                    }
                    this.FormatColumnSet(false);
                    if (this.rbTotal.Checked)
                    {
                        this.FormatColumnVisible(true);
                    }
                    else
                    {
                        this.FormatColumnVisible(false);
                    }
                }
            }
            else
            {
                MessageBox.Show("��ѡ���ҩ���ڵ���в�ѯ!", "��ʾ", MessageBoxButtons.OK);

                return;
            }
        }

        /// <summary>
        /// ҩƷ���ƹ���
        /// </summary>
        private void Filter()
        {
            string strFilter = " (ƴ���� like '%" + this.txtFilter.Text.Trim() + "%' or ����� like '%" + this.txtFilter.Text.Trim() + "%') ";//{56EC4715-22E9-48b7-AD66-63D1F87130D6}
            //string strFilter = "";
            //string strFilter = "(ƴ���� like '%" + this.txtFilter.Text.Trim() + "%')";//{56EC4715-22E9-48b7-AD66-63D1F87130D6}
            strFilter = strFilter + " and ȡҩҩ�� like '%" + this.cbbPharmacy.Text.Trim() + "%'";
            if (this.rbRetail.Checked)
            {
                strFilter = strFilter + " and ���� like '%" + this.txtCaseNO.Text.Trim() + "%'";                
            }
            //if (this.rbRetail.Checked)
            //{
            //    strFilter = strFilter + " and סԺ�� like '%" + this.txtCaseNO.Text.Trim() + "%'";
            //}
            strFilter = strFilter + this.SetInvalidData();
            DataView dv = this.fpDrugList.DataSource as DataView;
            if (dv != null)
            {
                dv.RowFilter = strFilter;
            }

            if (this.IsHintColor)
            {
                this.SetColorHint();
            }

            if (IsFormatColumn)
            {
                if (this.rbTotal.Checked)
                {
                    this.FormatColumnWidth(true);
                }
                else
                {
                    this.FormatColumnWidth(false);
                }
            }
        }

        /// <summary>
        /// ���ò�ѯ���ݵ���Ч����ʾ
        /// </summary>
        /// <returns></returns>
        private string SetInvalidData()
        {
            if (this.IsShowInvalidData)
            {
                return "";
            }
            else
            {
                return " and ��Ч�� like '%��Ч%'";
            }
        }

        /// <summary>
        /// ��ʽ���п�
        /// </summary>
        /// <param name="isTotal"></param>
        private void FormatColumnWidth(bool isTotal)
        {
            if (this.fpDrugList.ColumnCount == 0)
            { return; }
            if (isTotal)
            {
                this.fpDrugList.Columns[0].Width = 160;
                this.fpDrugList.Columns[1].Width = 60;
                this.fpDrugList.Columns[2].Width = 60;
                this.fpDrugList.Columns[3].Width = 35;
                this.fpDrugList.Columns[4].Width = 100;
                this.fpDrugList.Columns[5].Width = 130;
                this.fpDrugList.Columns[6].Width = 80;
                this.fpDrugList.Columns[7].Width = 60;
                this.fpDrugList.Columns[8].Width = 20;
                this.fpDrugList.Columns[9].Width = 20;
                //this.fpDrugList.Columns[10].Width = 35;
                //this.fpDrugList.Columns[11].Width = 35;--������Ҫ
            }
            else
            {
                this.fpDrugList.Columns[0].Width = 40;//סԺ��
                this.fpDrugList.Columns[1].Width = 50;//����
                this.fpDrugList.Columns[2].Width = 80;//����
                this.fpDrugList.Columns[3].Width = 160;//ҩƷ����
                this.fpDrugList.Columns[4].Width = 50;//���
                this.fpDrugList.Columns[5].Width = 50;//ÿ������
                this.fpDrugList.Columns[6].Width = 35;//��λ
                this.fpDrugList.Columns[7].Width = 50;//Ƶ��
                this.fpDrugList.Columns[8].Width = 45;//�÷�
                this.fpDrugList.Columns[9].Width = 60;//����
                this.fpDrugList.Columns[10].Width = 35;//��λ
                this.fpDrugList.Columns[11].Width = 100;//�������
                this.fpDrugList.Columns[12].Width = 130;//ȡҩҩ��
                this.fpDrugList.Columns[13].Width = 80;//��ҩ��
                this.fpDrugList.Columns[14].Width = 35;//��Ч��
                this.fpDrugList.Columns[15].Width = 35;//ƴ����
                this.fpDrugList.Columns[16].Width = 35;//�����
                this.fpDrugList.Columns[17].Width = 70;//״̬
                this.fpDrugList.Columns[18].Width = 100;//��ҩʱ��
                //FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();  
                //FarPoint.Win.Spread.CellType.DesignTimeCellTypeConverter dtCellType = new FarPoint.Win.Spread.CellType.DesignTimeCellTypeConverter();
                //this.fpDrugList.Columns[19].CellType = dateTimeCellType1;
                //this.fpDrugList.Columns.Get(19).CellType = dateTimeCellType1;                
                //this.fpDrugList.Columns[19].Width = 70;//��ҩ����//{56F10C75-A5C0-405d-BF56-69C33B45CB19}
            }
        }

        /// <summary>
        /// ��ʽ���б�����
        /// </summary>
        /// <param name="isTotal"></param>
        private void FormatColumnSet(bool isTotal)
        {
            for (int i = 0; i < this.fpDrugList.ColumnCount; i++)
            {
                this.fpDrugList.Columns[i].Locked = true;
                this.fpDrugList.Columns[i].AllowAutoSort = true;
            }
        }

        /// <summary>
        /// ��ʽ������ʾ
        /// </summary>
        /// <param name="isTotal"></param>
        private void FormatColumnVisible(bool isTotal)
        {
            if (isTotal)
            {
                this.fpDrugList.Columns[6].Visible = false;
                this.fpDrugList.Columns[8].Visible = false;
                this.fpDrugList.Columns[9].Visible = false;
                //this.fpDrugList.Columns[10].Visible = false;
            }
            else
            {
                this.fpDrugList.Columns[5].Visible = false;
                this.fpDrugList.Columns[6].Visible = false;
                this.fpDrugList.Columns[7].Visible = false;
                this.fpDrugList.Columns[8].Visible = false;
                this.fpDrugList.Columns[11].Visible = false;
                this.fpDrugList.Columns[13].Visible = false;
                this.fpDrugList.Columns[14].Visible = false;
                this.fpDrugList.Columns[15].Visible = false;
                this.fpDrugList.Columns[16].Visible = false;
                this.fpDrugList.Columns[17].Visible = false;
                if (this.rbtUndo.Checked)
                {
                    this.fpDrugList.Columns[18].Visible = false;
                }
                else
                {
                    this.fpDrugList.Columns[18].Visible = true;
                }
            }
        }

        /// <summary>
        /// ���������ʾ
        /// </summary>
        private void ClearData()
        {
            this.dsDrugList = null;
            this.fpDrugList.RowCount = 0;
            this.fpDrugList.ColumnCount = 0;
        }

        /// <summary>
        /// ������ɫ��ʾ
        /// </summary>
        private void SetColorHint()
        {
            int columnIndex = 0;
            for (int i = 0; i < this.fpDrugList.ColumnCount; i++)
            {
                if (this.fpDrugList.Columns[i].Label == "��Ч��")
                {
                    columnIndex = i;
                }
            }

            for (int i = 0; i < this.fpDrugList.RowCount; i++)
            {
                if (this.fpDrugList.Cells[i, columnIndex].Text == "��Ч")
                {
                    this.fpDrugList.Rows[i].ForeColor = Color.Red;
                }                
            }
        }

        private void InitPrintInterface()
        {
            if (printInterface == null)
            {
                 this.printInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;
            }

        }

        #endregion

        #region �¼�

        /// <summary>
        /// �ؼ������¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.dtpStartDate.Value = DateTime.Today.Date;
            this.dtpEndDate.Value = DateTime.Today.Date;
            this.IniControls();
            base.OnLoad(e);
        }

        /// <summary>
        /// ��ѯ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryDrugList();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ��ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvSelectType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ToolTipText == "��ҩ��")
            {
                this.billClass = this.drugstoreManager.GetDrugBillClass(e.Node.Tag.ToString());
                this.QueryDrugList();
            }
            else
            {
                this.ClearData();
            }
        }

        /// <summary>
        /// ���ܰ�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbTotal_CheckedChanged(object sender, EventArgs e)
        {
            this.QueryDrugList();
        }

        /// <summary>
        /// ��ϸ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbRetail_CheckedChanged(object sender, EventArgs e)
        {
            this.QueryDrugList();
        }

        /// <summary>
        /// ҩƷ���˿�仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            //this.neuPanel7.Dock = DockStyle.Top;
            //int iHeight = 200;
            //DialogResult dr = MessageBox.Show("�Ƿ�ֻ��ӡѡ������ݣ�ѡ�������ӡȫ��!", "��ӡ��ʾ", MessageBoxButtons.YesNoCancel);
            //if (dr == DialogResult.Yes)
            //{
            //    for (int i = 0; i < this.fpDrugList.RowCount; i++)
            //    {
            //        if (this.fpDrugList.IsSelected(i, 0) == false)
            //        {
            //            this.fpDrugList.Rows[i].Visible = false;
            //        }
            //        iHeight = iHeight + 20;
            //    }
            //}
            //else if (dr == DialogResult.No)
            //{
            //    for (int i = 0; i < this.fpDrugList.RowCount; i++)
            //    {
            //        iHeight = iHeight + 20;
            //    }
            //}
            //else
            //{
            //    return 1;
            //}
                        
            //this.neuPanel7.Height = iHeight;
            //FS.FrameWork.WinForms.Classes.Print printObj = new FS.FrameWork.WinForms.Classes.Print();
            //FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("", this.neuPanel7.Width, iHeight);
            //printObj.SetPageSize(ps);
            //printObj.IsDataAutoExtend = false;
            //printObj.PrintPreview(10, 10, this.neuPanel7);
            //this.neuPanel7.Dock = DockStyle.Fill;
            //return base.OnPrint(sender, neuObject);
            //if (this.lbTitle.Text == "��ϸ")
            //{
            if (this.printInterface == null)
            {
                InitPrintInterface();
            }
            ArrayList drugList = null;
            this.billClass.User01 = "NurseType";//{31607136-EF3D-46af-A2F9-EE96F6F9209C}
            if (this.rbRetail.Checked)//{CC985758-A2AE-41da-9394-34AFCEB0E30E}
            {
                drugList = this.drugItem.QueryApplyOutListDetailByBillClassCode(this.tvSelectType.SelectedNode.Tag.ToString(),
                            this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(), "2");
                this.billClass.PrintType.ID = "D";
            }
            else if (this.rbTotal.Checked)
            {
                drugList = this.drugItem.QueryApplyOutListTotByBillClassCode(this.tvSelectType.SelectedNode.Tag.ToString(),
                            this.tvSelectType.SelectedNode.Parent.Tag.ToString(), this.dtpStartDate.Value.Date.ToString(), this.dtpEndDate.Value.Date.AddDays(1).ToString(), "2");
                this.billClass.PrintType.ID = "T";
            }

            if (drugList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut outObj in drugList)
                {
                    FS.HISFC.Models.Pharmacy.Storage storage = drugItem.GetStockInfoByDrugCode(outObj.StockDept.Name, outObj.Item.ID);
                    FS.HISFC.Models.Pharmacy.Item itemObj = drugItem.GetItem(outObj.Item.ID);
                    outObj.PlaceNO = storage.PlaceNO;
                    outObj.Item.UserCode = itemObj.UserCode;
                }
                if (this.printInterface != null)
                {
                    this.printInterface.AddAllData(drugList, this.billClass);
                    this.printInterface.Preview();
                }
            }
            //}
            return 1;
        }

        /// <summary>
        /// ����Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            return base.Export(sender, neuObject);
        }

        private void rbtUndo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtUndo.Checked)
            {
                this.queryDrugRange = QueryRange.NoneDrug;
                if (this.QueryDrugRange == ucQueryDrugList.QueryRange.NoneDrug)
                {
                    this.neuPanel6.Visible = false;
                }
                else
                {
                    this.neuPanel6.Visible = true;
                }
                if (this.tvSelectType.SelectedNode.ToolTipText == "��ҩ��")
                {
                    this.QueryDrugList();
                }
                else
                {
                    this.ClearData();
                }
            }
        }

        private void rbtDone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDone.Checked)
            {
                this.queryDrugRange = QueryRange.Druged;
                if (this.QueryDrugRange == ucQueryDrugList.QueryRange.NoneDrug)
                {
                    this.neuPanel6.Visible = false;
                }
                else
                {
                    this.neuPanel6.Visible = true;
                }
                if (this.tvSelectType.SelectedNode.ToolTipText == "��ҩ��")
                {
                    this.QueryDrugList();
                }
                else
                {
                    this.ClearData();
                }
            }
        }

        #endregion

        #region ö��

        /// <summary>
        /// ��ʾ�ڵ�����
        /// </summary>
        public enum NodeType
        {        
            /// <summary>
            /// ����Ԫ(����)
            /// </summary>
            NurseCell,
            /// <summary>
            /// ��������ʾ
            /// </summary>
            Dept,
            /// <summary>
            /// ����վ����ʾ����
            /// </summary>
            NurseDept,
            /// <summary>
            /// ��ҩ����ʾ
            /// </summary>
            Pharmacy,
            /// <summary>
            /// ����ǰ��½���Ұ�ҩ����ʾ
            /// </summary>
            DrugBill
        }

        /// <summary>
        /// ��ҩ��ѯ��Χ
        /// </summary>
        public enum QueryRange
        {
            /// <summary>
            /// δ��ҩ
            /// </summary>
            NoneDrug,
            /// <summary>
            /// �Ѱ�ҩ
            /// </summary>
            Druged,
            /// <summary>
            /// ȫ��ҩƷ
            /// </summary>
            AllDrug
        }

        #endregion

        private void txtCaseNO_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void cbbPharmacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint);

                return printType;
            }
        }

        #endregion
    }
}
