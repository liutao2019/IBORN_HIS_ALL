using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucFeeStat : UserControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        /// <summary>
        /// 
        /// </summary>
        public ucFeeStat()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.WinForms.Forms.IMaintenanceForm maintenanceForm;

        /// <summary>
        /// ͳ�ƴ���ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStatManager = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

        /// <summary>
        /// ����UC
        /// </summary>
        private ucFeeCodeStatModify ucModify = new ucFeeCodeStatModify();

        /// <summary>
        /// [2007/02/07] �����ӵĴ���,�ж��Ƿ��޸Ĺ�
        /// </summary>
        private bool isDirty = false;

        /// <summary>
        /// ҽ����𼯺�{CFCDEC81-53A3-4de2-9871-99B7990A4F0C}
        /// </summary>
        ArrayList aLCenterType = new ArrayList();
        #endregion

        #region ˽�з���

        /// <summary>
        /// ���������С����
        /// </summary>
        /// <returns>��С�����б�</returns>
        protected ArrayList QueryValidAllMinFee() 
        {
            return this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
        }

        /// <summary>
        /// ���δά������С�����б�
        /// </summary>
        /// <returns>δά������С�����б�</returns>
        protected ArrayList QueryValidMinFee()
        {
            ArrayList minFeeList = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);

            if (minFeeList == null)
            {
                return null;

            }
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return minFeeList;
            }
            for (int i = 0; i < minFeeList.Count; i++)
            {
                FS.HISFC.Models.Base.Const objCon = new FS.HISFC.Models.Base.Const();
                objCon = (FS.HISFC.Models.Base.Const)minFeeList[i];
                for (int j = 0; j < this.fpSpread1_Sheet1.RowCount; j++)
                {
                    if (objCon.ID == this.fpSpread1_Sheet1.Cells[j, (int)EnumColumns.MinFeeName].Tag.ToString() && this.fpSpread1_Sheet1.Cells[j, (int)EnumColumns.ValidState].Tag.ToString() == "0")
                    {
                        minFeeList.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }
            return minFeeList;
        }

        /// <summary>
        /// ��ʼ��ҽ���������{CFCDEC81-53A3-4de2-9871-99B7990A4F0C}
        /// </summary>
        /// <returns></returns>
        private int QueryCenterType()
        {
            this.aLCenterType = this.managerIntegrate.GetConstantList("CENTERFEECODE");
            if (this.aLCenterType == null)
            {
                MessageBox.Show("��ѯҽ��������");
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ����ҽ������������ת������{CFCDEC81-53A3-4de2-9871-99B7990A4F0C}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetCenterTypeName(string id)
        {
            foreach (FS.FrameWork.Models.NeuObject var in this.aLCenterType)
            {
                if (var.ID == id)
                {
                    return var.Name;
                }
            }
            return "";
        }


        /// <summary>
        /// ��ʼ��treeview
        /// </summary>
        protected virtual int InitTree()
        {
            this.tvFeeType.Nodes.Clear();

            this.tvFeeType.Nodes.AddRange(
                new TreeNode[] {
						  new TreeNode("��Ʊ"),
						  new TreeNode("ͳ��"),
						  new TreeNode("����"),
						  new TreeNode("֪��Ȩ")
                                });
            
            foreach (TreeNode node in this.tvFeeType.Nodes) 
            {
                if (node.Text == "��Ʊ") 
                {
                    node.Tag = "FP";
                }
                if (node.Text == "ͳ��") 
                {
                    node.Tag = "TJ";
                }
                if (node.Text == "����") 
                {
                    node.Tag = "BA";
                }
                if (node.Text == "֪��Ȩ") 
                {
                    node.Tag = "ZQ";
                }
            }

            //�������
            //ͳ�ƹ����б�
            try
            {
                ArrayList reportTypes = new ArrayList();
                reportTypes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.FEECODESTAT);

                for (int j = 0; j < reportTypes.Count; j++)
                {
                    FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
                    obj = (FS.HISFC.Models.Base.Const)reportTypes[j];
                    TreeNode tnReportName = new TreeNode();
                    tnReportName = new TreeNode(obj.Name);
                    tnReportName.Tag = obj.ID;
                    tnReportName.Text = obj.Name;
                    if (obj.Memo == "��Ʊ")
                    {
                        tvFeeType.Nodes[0].Nodes.Add(tnReportName);
                    }
                    if (obj.Memo == "ͳ��")
                    {
                        tvFeeType.Nodes[1].Nodes.Add(tnReportName);
                    }
                    if (obj.Memo == "����")
                    {
                        tvFeeType.Nodes[2].Nodes.Add(tnReportName);
                    }
                    if (obj.Memo == "֪��Ȩ")
                    {
                        tvFeeType.Nodes[3].Nodes.Add(tnReportName);
                    }
                }

                // [2007/02/07] �����ӵĴ���,ѡ�����ؼ�
                this.tvFeeType.Select();
                // �����ӵĴ������
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return -1;
            }

            return 1;
        }

        private void SetValue(FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat, int row) 
        {

            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.ReportCode, feeCodeStat.ID);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.ReportName, feeCodeStat.Name);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.MinFeeName, feeCodeStat.MinFee.Name);
            this.fpSpread1_Sheet1.Cells[row, (int)EnumColumns.MinFeeName].Tag = feeCodeStat.MinFee.ID;
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.StatCateCode, feeCodeStat.StatCate.ID);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.StatCateName, feeCodeStat.StatCate.Name);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.PrintOrder, feeCodeStat.SortID);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.ExtendCode, feeCodeStat.FeeStat.ID);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.ExecDept, feeCodeStat.ExecDept.Name);
            //{CFCDEC81-53A3-4de2-9871-99B7990A4F0C}
            //this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.CenterType, feeCodeStat.CenterStat);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.CenterType, this.GetCenterTypeName(feeCodeStat.CenterStat));
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.ValidState, ((int)feeCodeStat.ValidState).ToString());
            this.fpSpread1_Sheet1.Cells[row, (int)EnumColumns.ValidState].Tag = ((int)feeCodeStat.ValidState).ToString();
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.Oper, feeCodeStat.Oper.ID);
            this.fpSpread1_Sheet1.SetValue(row, (int)EnumColumns.OperTime, feeCodeStat.Oper.OperTime);

            this.fpSpread1_Sheet1.Rows[row].Tag = feeCodeStat;
        }

        #endregion

        #region ö��

        /// <summary>
        /// ��ʾ������
        /// </summary>
        public enum EnumColumns 
        {
            /// <summary>
            /// �������
            /// </summary>
            ReportCode = 0,

            /// <summary>
            /// �������
            /// </summary>
            ReportName,

            /// <summary>
            /// ���ô���
            /// </summary>
            MinFeeName,

            /// <summary>
            /// ͳ�Ʊ���
            /// </summary>
            StatCateCode,

            /// <summary>
            /// ͳ������
            /// </summary>
            StatCateName,

            /// <summary>
            /// ��ӡ˳��
            /// </summary>
            PrintOrder,

            /// <summary>
            /// ��չ����
            /// </summary>
            ExtendCode,

            /// <summary>
            /// ִ�п���
            /// </summary>
            ExecDept,

            /// <summary>
            /// ҽ�����
            /// </summary>
            CenterType,

            /// <summary>
            /// ҽ�����
            /// </summary>
            ValidState,

            /// <summary>
            /// ����Ա
            /// </summary>
            Oper,

            /// <summary>
            /// ����ʱ��
            /// </summary>
            OperTime
        }

        #endregion


        #region IMaintenanceControlable ��Ա

        /// <summary>
        /// ������ͳ�����
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            // [2007/02/07] �����ӵĴ���,����Ƿ�ѡ����Ч�ڵ�
            if ( this.tvFeeType.SelectedNode == null || this.tvFeeType.SelectedNode.Parent == null )
            {
                MessageBox.Show("��ѡ��һ����Ч�ڵ�", "��ʾ", MessageBoxButtons.OK);
                return 1;
            }
            // �����ӵĴ������

            int activeRow = this.fpSpread1_Sheet1.ActiveRowIndex;

            FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.Models.Fee.FeeCodeStat();

            feeCodeStat.ID = tvFeeType.SelectedNode.Tag.ToString();
            feeCodeStat.Name = tvFeeType.SelectedNode.Text.ToString();
            feeCodeStat.ReportType.Name = tvFeeType.SelectedNode.Parent.Text.ToString();
            feeCodeStat.ReportType.ID = tvFeeType.SelectedNode.Parent.Tag;//.ToString();

            ucModify.MinFeeList = this.QueryValidMinFee();
            ucModify.SaveType = ucFeeCodeStatModify.EnumSaveTypes.Add;
            ucModify.FeeCodeStat = feeCodeStat;

            // [2007/02/07] �����ӵĴ���
            //this.isDirty = true;
            //�����ӵĴ������

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucModify);

            return 1;
        }

        public int Copy()
        {
            return 1;
        }

        public int Cut()
        {
            return 1;
        }

        public int Delete()
        {
            return 1;
        }

        public int Export()
        {
            return 1;
        }

        public int Import()
        {
            return 1;
        }

        public int Init()
        {
            return this.InitTree();
        }

        public bool IsDirty
        {
            get
            {
                return this.isDirty;
                //return true;
            }
            set
            {
               
            }
        }

        public int Modify()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) 
            {
                return -1;
            }

            int activeRow = this.fpSpread1_Sheet1.ActiveRowIndex;

            FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat = this.fpSpread1_Sheet1.Rows[activeRow].Tag as FS.HISFC.Models.Fee.FeeCodeStat;

            ucModify.MinFeeList = this.QueryValidAllMinFee();
            ucModify.SaveType = ucFeeCodeStatModify.EnumSaveTypes.Modify;
            ucModify.FeeCodeStat = feeCodeStat;

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucModify);
            
            return 1;
        }

        public int NextRow()
        {
            return 1;
        }

        public int Paste()
        {
            return 1;
        }

        public int PreRow()
        {
            return 1;
        }

        public int Print()
        {
            return 1;
        }

        public int PrintConfig()
        {
            return 1;
        }

        public int PrintPreview()
        {
            return 1;
        }
      

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            
            TreeNode treeNode = this.tvFeeType.SelectedNode;
          
           
            if (treeNode == null) 
            {
                return -1;
            }
         
            if (treeNode.Parent == null)
            {
                return -1 ;
            }
            if (treeNode.Tag == null)
            {
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����Ŀ��Ϣ,��ȴ�...");
            ArrayList feeCodeStats = this.feeCodeStatManager.QueryFeeCodeStatByReportCode(treeNode.Tag.ToString());

            if(feeCodeStats == null)
            {
                MessageBox.Show(Language.Msg("���ͳ�ƴ�����ϸ����!") + this.feeCodeStatManager.Err);

                return -1;
            }

            this.fpSpread1_Sheet1.RowCount = feeCodeStats.Count;

            for (int i = 0; i < feeCodeStats.Count; i++) 
            {
                FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat = feeCodeStats[i] as FS.HISFC.Models.Fee.FeeCodeStat;

                SetValue(feeCodeStat, i);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return this.maintenanceForm;
            }
            set
            {
                this.maintenanceForm = value;
            }
        }

        public int Save()
        {
            return 1;
        }

        #endregion

        private void tvFeeType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Query();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Modify();
        }

        private void ucFeeStat_Load(object sender, EventArgs e)
        {
            this.ucModify.Save += new ucFeeCodeStatModify.ClickSave(ucModify_Save);

            // [2007/02/07] �����ӵĴ���
            this.maintenanceForm.ShowDeleteButton = false;
            this.maintenanceForm.ShowCopyButton = false;
            this.maintenanceForm.ShowCutButton = false;
            this.maintenanceForm.ShowExportButton = false;
            this.maintenanceForm.ShowImportButton = false;
            this.maintenanceForm.ShowNextRowButton = false;
            this.maintenanceForm.ShowPasteButton = false;
            this.maintenanceForm.ShowPreRowButton = false;
            this.maintenanceForm.ShowPrintButton = false;
            this.maintenanceForm.ShowPrintConfigButton = false;
            this.maintenanceForm.ShowPrintPreviewButton = false;
            this.maintenanceForm.ShowSaveButton = false;
            //this.maintenanceForm.s
            // �����ӵĴ������
            //{CFCDEC81-53A3-4de2-9871-99B7990A4F0C}
            this.QueryCenterType();
        }

        void ucModify_Save(FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat)
        {
            if (this.ucModify.SaveType == ucFeeCodeStatModify.EnumSaveTypes.Add)
            {

                int row = this.fpSpread1_Sheet1.RowCount;

                this.fpSpread1_Sheet1.Rows.Add(row, 1);

                row = this.fpSpread1_Sheet1.Rows.Count - 1;

                this.SetValue(feeCodeStat, row);
            }
            if (this.ucModify.SaveType == ucFeeCodeStatModify.EnumSaveTypes.Modify) 
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++) 
                {
                    FS.HISFC.Models.Fee.FeeCodeStat temp = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.FeeCodeStat;

                    if (temp.MinFee.ID == feeCodeStat.MinFee.ID && temp.ID == feeCodeStat.ID) 
                    {
                        this.SetValue(feeCodeStat, i);
                        break;
                    }
                }
            }
        }
    }
}
