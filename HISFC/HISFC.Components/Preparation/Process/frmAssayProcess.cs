using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Preparation.Process
{
    /// <summary>
    /// <br></br>
    /// [��������: ��Ʒ���鹤������¼��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class frmAssayProcess : HISFC.Components.Preparation.Process.frmProcessBase
    {
        public frmAssayProcess()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// �Ƽ�����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();
      
        /// <summary>
        /// �����ӵ������ڵ�ģ����Ϣ
        /// </summary>
        private System.Collections.Hashtable hsStencilReport = new System.Collections.Hashtable();

        /// <summary>
        /// �Ƽ���Ʒ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Preparation.Preparation preparation = new FS.HISFC.Models.Preparation.Preparation();
     
        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected override void Init()
        {
            base.Init();

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numMarkCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            this.fsReport_Sheet1.Columns[2].CellType = numMarkCellType;
        }

        #endregion

        /// <summary>
        /// �Ƽ�������Ϣ����
        /// </summary>
        /// <param name="preparation"></param>
        /// <returns></returns>
        public int SetPreparation(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            this.preparation = preparation;

            this.ShowPreparationInfo();

            //ģ����Ϣ����
            this.QueryStencilData();

            //��¼��������������̼���
            this.hsStencilReport.Clear();
            this.fsReport_Sheet1.Rows.Count = 0;
            List<FS.HISFC.Models.Preparation.Process> processList = this.preparationManager.QueryProcess(preparation.PlanNO, preparation.Drug.ID, ((int)preparation.State).ToString());
            if (processList != null)
            {
                foreach (FS.HISFC.Models.Preparation.Process p in processList)
                {
                    this.AddReportData(p);
                }
            }

            return 1;
        }

        /// <summary>
        /// �Ƽ���Ʒ��Ϣ��ʾ
        /// </summary>
        protected void ShowPreparationInfo()
        {
            if (this.preparation != null)
            {
                this.lbTemplete.Text = this.preparation.Drug.Name + " ���Ʒ����ģ��";
                this.lbTitle.Text = this.preparation.Drug.Name + " �� �� Ʒ �� �� �� ��";
                this.lbPreparationInfo.Text = string.Format("�Ƽ���Ʒ: {0}  ���: {1}  �ͼ���: {2}", this.preparation.Drug.Name, this.preparation.Drug.Specs, this.preparation.AssayQty);
            }
        }

        #region ģ����Ϣ����

        /// <summary>
        /// ����ģ����Ϣ
        /// </summary>
        protected void QueryStencilData()
        {
            List<FS.HISFC.Models.Preparation.Stencil> stencilList = this.preparationManager.QueryStencil(this.preparation.Drug.ID, this.stencilType);
            if (stencilList == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ģ����Ϣ����ʧ��") + this.preparationManager.Err);
                return;
            }

            this.fsStencil_Sheet1.Rows.Count = 0;
            foreach (FS.HISFC.Models.Preparation.Stencil info in stencilList)
            {
                this.AddStencilData(info);
            }
        }

        /// <summary>
        /// ģ����Ϣ
        /// </summary>
        /// <param name="stencil">ģ����Ϣ</param>
        private void AddStencilData(FS.HISFC.Models.Preparation.Stencil stencil)
        {
            int row = this.fsStencil_Sheet1.Rows.Count;
            this.fsStencil_Sheet1.Rows.Add(row, 1);

            this.fsStencil_Sheet1.Cells[row, (int)StencilColumnEnum.ColType].Text = stencil.Type.Name;
            this.fsStencil_Sheet1.Cells[row, (int)StencilColumnEnum.ColItem].Text = stencil.Item.Name;
            this.fsStencil_Sheet1.Cells[row, (int)StencilColumnEnum.ColMin].Text = stencil.StandardMin.ToString();
            this.fsStencil_Sheet1.Cells[row, (int)StencilColumnEnum.ColMax].Text = stencil.StandardMax.ToString();
            this.fsStencil_Sheet1.Cells[row, (int)StencilColumnEnum.ColDes].Text = stencil.StandardDes;

            this.fsStencil_Sheet1.Rows[row].Tag = stencil;
        }

        #endregion

        #region ���浥

        /// <summary>
        /// ��ӱ��浥��ϸ��Ϣ
        /// </summary>
        /// <param name="p">��������������Ϣ</param>
        private void AddReportData(FS.HISFC.Models.Preparation.Process p)
        {
            int row = this.fsReport_Sheet1.Rows.Count;
            this.fsReport_Sheet1.Rows.Add(row, 1);

            this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColType].Text = p.ItemType;
            this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColItem].Text = p.ProcessItem.Name;
            this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColNum].Text = p.ResultQty.ToString();
            this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColDes].Text = p.ResultStr;
            this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColEli].Text = this.ConvertBoolToString(p.IsEligibility);
            if (p.IsEligibility)
            {
                this.fsReport_Sheet1.Rows[row].ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                this.fsReport_Sheet1.Rows[row].ForeColor = System.Drawing.Color.Red;
            }

            this.fsReport_Sheet1.Rows[row].Tag = p;

            this.hsStencilReport.Add(p.ItemType + p.ProcessItem.Name, null);
        }

        /// <summary>
        /// ��ȡ���浥��ϸ��Ϣ
        /// </summary>
        /// <param name="rowIndex">������</param>
        /// <returns>�ɹ����ر��浥��ϸ��Ϣ ʧ�ܷ���null</returns>
        protected FS.HISFC.Models.Preparation.Process GetProcessData(int row)
        {
            FS.HISFC.Models.Preparation.Process process = new FS.HISFC.Models.Preparation.Process();

            if (this.fsReport_Sheet1.Rows[row].Tag is FS.HISFC.Models.Preparation.Stencil)
            {
                FS.HISFC.Models.Preparation.Stencil stencil = this.fsReport_Sheet1.Rows[row].Tag as FS.HISFC.Models.Preparation.Stencil;
                if (stencil == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��ǰѡ��ı��浥��ϸ��Ϣʱ��������"));
                    return null;
                }
                process.Preparation = this.preparation;
                process.ItemType = stencil.Type.Name;
                process.ProcessItem = stencil.Item;
            }
            else if (this.fsReport_Sheet1.Rows[row].Tag is FS.HISFC.Models.Preparation.Process)
            {
                process = this.fsReport_Sheet1.Rows[row].Tag as FS.HISFC.Models.Preparation.Process;
            }

            process.ResultQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColNum].Text);       //ʵ��ֵ
            process.ResultStr = this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColDes].Text;
            process.IsEligibility = this.ConvertStringToBool(this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColEli].Text);

            return process;
        }

        /// <summary>
        /// ����ѡ���ģ����Ŀ��ʼ��¼����Ϣ
        /// </summary>
        /// <param name="stencil">ģ����Ϣ</param>
        private void AddStencilToReport(FS.HISFC.Models.Preparation.Stencil stencil)
        {
            if (this.hsStencilReport.ContainsKey(stencil.Type.Name + stencil.Item.Name))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����Ŀ����ӵ����浥��"));
                return;
            }

            int row = this.fsReport_Sheet1.Rows.Count;
            this.fsReport_Sheet1.Rows.Add(row, 1);

            this.SetReportCellType(this.fsReport,this.fsReport_Sheet1,stencil.ItemType, row, (int)ReportColumnEnum.ColDes);

            this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColType].Text = stencil.Type.Name;
            this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColItem].Text = stencil.Item.Name;

           

            this.fsReport_Sheet1.Rows[row].Tag = stencil;

            this.hsStencilReport.Add(stencil.Type.Name + stencil.Item.Name, null);
        }

        /// <summary>
        /// ���ݲ�ͬ����Ŀ���� ���õ�Ԫ������
        /// </summary>
        /// <param name="itemType">��Ŀ����</param>
        /// <param name="rowIndex">��Ԫ��������</param>
        /// <param name="columnIndex">��Ԫ��������</param>
        private void SetReportCellType(FS.HISFC.Models.Preparation.EnumStencilItemType itemType, int rowIndex,int columnIndex)
        {
            switch (itemType)
            {
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Person:
                    this.fsReport.SetColumnList(this.fsReport_Sheet1, this.alStaticEmployee, columnIndex);
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Dept:
                    this.fsReport.SetColumnList(this.fsReport_Sheet1, this.alStaticDept, columnIndex);
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Date:
                    FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType markDateCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType();
                    this.fsReport_Sheet1.Cells[rowIndex, columnIndex].CellType = markDateCellType;
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Number:
                    FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
                    this.fsReport_Sheet1.Cells[rowIndex, columnIndex].CellType = numCellType;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ɾ����ǰ�Ѵ��ڵı��浥
        /// </summary>
        /// <param name="rowIndex"></param>
        private int DelReport(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�ȷ��ɾ���ñ��浥��ϸ��Ϣ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.No)
                {
                    return -1;
                }
            }

            FS.HISFC.Models.Preparation.Process tempProcess = this.GetProcessData(rowIndex);
            if (tempProcess != null)
            {
                if (this.preparationManager.DelProcess(tempProcess) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��ʧ��") + this.preparationManager.Err);
                    return -1;
                }

                if (this.hsStencilReport.ContainsKey(tempProcess.ItemType + tempProcess.ProcessItem.Name))
                {
                    this.hsStencilReport.Remove(tempProcess.ItemType + tempProcess.ProcessItem.Name);
                }

                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ���ɹ�"));
            }

            this.fsReport_Sheet1.Rows.Remove(rowIndex, 1);
            this.fsReport.VisibleAllList();

            return 1;
        }

        /// <summary>
        /// �����������̱���
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SaveProcess()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.preparationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.preparationManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.fsReport_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Process p = this.GetProcessData(i);

                p.Oper.OperTime = sysTime;
                p.Oper.ID = this.preparationManager.Operator.ID;

                if (this.preparationManager.SetProcess(p) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����Ƽ�����������Ϣʧ��" + this.preparationManager.Err);
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("��������ִ����Ϣ����ɹ�");

            return 1;
        }

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                
                this.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ݳ�ʼ����������"));
                return;
            }

            base.OnLoad(e);
        }

        private void fsStencil_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = this.fsStencil_Sheet1.ActiveRowIndex;

            FS.HISFC.Models.Preparation.Stencil info = this.fsStencil_Sheet1.Rows[row].Tag as FS.HISFC.Models.Preparation.Stencil;

            if (info != null)
            {
                this.AddStencilToReport(info);
            }
        }
     
        #endregion

        #region ��ö��

        /// <summary>
        /// ģ����ö��
        /// </summary>
        private enum StencilColumnEnum
        {
            /// <summary>
            /// ���
            /// </summary>
            ColType,
            /// <summary>
            /// ��Ŀ
            /// </summary>
            ColItem,
            /// <summary>
            /// ��׼����ֵ
            /// </summary>
            ColMin,
            /// <summary>
            /// ��׼����ֵ
            /// </summary>
            ColMax,
            /// <summary>
            /// ��׼����
            /// </summary>
            ColDes
        }

        private enum ReportColumnEnum
        {
            /// <summary>
            /// ���
            /// </summary>
            ColType,
            /// <summary>
            /// ��Ŀ
            /// </summary>
            ColItem,
            /// <summary>
            /// ʵ��ֵ
            /// </summary>
            ColNum,
            /// <summary>
            /// ����
            /// </summary>
            ColDes,
            /// <summary>
            /// �Ƿ�ϸ�
            /// </summary>
            ColEli,
        }

        #endregion       

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveProcess();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fsReport_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == (int)ReportColumnEnum.ColType || e.Column == (int)ReportColumnEnum.ColItem)
            {
                this.DelReport(e.Row);
            }
        }

        private void fsReport_SelectItem(object sender, EventArgs e)
        {
            if (sender != null && sender is FS.FrameWork.Models.NeuObject)
            {
                this.fsReport_Sheet1.Cells[this.fsReport_Sheet1.ActiveRowIndex, (int)ReportColumnEnum.ColDes].Text = (sender as FS.FrameWork.Models.NeuObject).Name;
            }
        }

        private void fsReport_ComboSelChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //if (e.Column == (int)ReportColumnEnum.ColEli)
            //{
            //    if (this.ConvertStringToBool(this.fsReport_Sheet1.Cells[e.Row, e.Column].Text))
            //    {
            //        this.fsReport_Sheet1.Rows[e.Row].ForeColor = System.Drawing.Color.Red;
            //    }
            //    else
            //    {
            //        this.fsReport_Sheet1.Rows[e.Row].ForeColor = System.Drawing.Color.Black;
            //    }
            //}            
        }

        private void fsReport_ComboCloseUp(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)ReportColumnEnum.ColEli)
            {
                if (this.ConvertStringToBool(this.fsReport_Sheet1.Cells[e.Row, e.Column].Text))     //�ϸ�
                {
                    this.fsReport_Sheet1.Rows[e.Row].ForeColor = System.Drawing.Color.Black;
                }
                else                                                                               //���ϸ�
                {
                    this.fsReport_Sheet1.Rows[e.Row].ForeColor = System.Drawing.Color.Red;
                }
            }           
        }
    }
}