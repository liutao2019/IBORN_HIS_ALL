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
    /// [功能描述: 成品检验工艺流程录入]<br></br>
    /// [创 建 者: 飞斯]<br></br>
    /// [创建时间: 2008-03]<br></br>
    /// <说明>
    /// </说明>
    /// </summary>
    public partial class frmAssayProcess : HISFC.Components.Preparation.Process.frmProcessBase
    {
        public frmAssayProcess()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// 制剂管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Preparation preparationManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();
      
        /// <summary>
        /// 已增加到报告内的模版信息
        /// </summary>
        private System.Collections.Hashtable hsStencilReport = new System.Collections.Hashtable();

        /// <summary>
        /// 制剂成品信息
        /// </summary>
        private FS.HISFC.Models.Preparation.Preparation preparation = new FS.HISFC.Models.Preparation.Preparation();
     
        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            base.Init();

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numMarkCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            this.fsReport_Sheet1.Columns[2].CellType = numMarkCellType;
        }

        #endregion

        /// <summary>
        /// 制剂配置信息设置
        /// </summary>
        /// <param name="preparation"></param>
        /// <returns></returns>
        public int SetPreparation(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            this.preparation = preparation;

            this.ShowPreparationInfo();

            //模版信息加载
            this.QueryStencilData();

            //已录入的生产工艺流程加载
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
        /// 制剂成品信息显示
        /// </summary>
        protected void ShowPreparationInfo()
        {
            if (this.preparation != null)
            {
                this.lbTemplete.Text = this.preparation.Drug.Name + " 半成品检验模版";
                this.lbTitle.Text = this.preparation.Drug.Name + " 半 成 品 检 验 报 告";
                this.lbPreparationInfo.Text = string.Format("制剂成品: {0}  规格: {1}  送检量: {2}", this.preparation.Drug.Name, this.preparation.Drug.Specs, this.preparation.AssayQty);
            }
        }

        #region 模版信息处理

        /// <summary>
        /// 加载模版信息
        /// </summary>
        protected void QueryStencilData()
        {
            List<FS.HISFC.Models.Preparation.Stencil> stencilList = this.preparationManager.QueryStencil(this.preparation.Drug.ID, this.stencilType);
            if (stencilList == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("模版信息加载失败") + this.preparationManager.Err);
                return;
            }

            this.fsStencil_Sheet1.Rows.Count = 0;
            foreach (FS.HISFC.Models.Preparation.Stencil info in stencilList)
            {
                this.AddStencilData(info);
            }
        }

        /// <summary>
        /// 模版信息
        /// </summary>
        /// <param name="stencil">模版信息</param>
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

        #region 报告单

        /// <summary>
        /// 添加报告单明细信息
        /// </summary>
        /// <param name="p">生产工艺流程信息</param>
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
        /// 获取报告单明细信息
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <returns>成功返回报告单明细信息 失败返回null</returns>
        protected FS.HISFC.Models.Preparation.Process GetProcessData(int row)
        {
            FS.HISFC.Models.Preparation.Process process = new FS.HISFC.Models.Preparation.Process();

            if (this.fsReport_Sheet1.Rows[row].Tag is FS.HISFC.Models.Preparation.Stencil)
            {
                FS.HISFC.Models.Preparation.Stencil stencil = this.fsReport_Sheet1.Rows[row].Tag as FS.HISFC.Models.Preparation.Stencil;
                if (stencil == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取当前选择的报告单明细信息时发生错误"));
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

            process.ResultQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColNum].Text);       //实际值
            process.ResultStr = this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColDes].Text;
            process.IsEligibility = this.ConvertStringToBool(this.fsReport_Sheet1.Cells[row, (int)ReportColumnEnum.ColEli].Text);

            return process;
        }

        /// <summary>
        /// 根据选择的模版项目初始化录入信息
        /// </summary>
        /// <param name="stencil">模版信息</param>
        private void AddStencilToReport(FS.HISFC.Models.Preparation.Stencil stencil)
        {
            if (this.hsStencilReport.ContainsKey(stencil.Type.Name + stencil.Item.Name))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("该项目已添加到报告单内"));
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
        /// 根据不同的项目类型 设置单元格属性
        /// </summary>
        /// <param name="itemType">项目类型</param>
        /// <param name="rowIndex">单元格行索引</param>
        /// <param name="columnIndex">单元格列索引</param>
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
        /// 删除当前已存在的报告单
        /// </summary>
        /// <param name="rowIndex"></param>
        private int DelReport(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("是否确认删除该报告单明细信息"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除失败") + this.preparationManager.Err);
                    return -1;
                }

                if (this.hsStencilReport.ContainsKey(tempProcess.ItemType + tempProcess.ProcessItem.Name))
                {
                    this.hsStencilReport.Remove(tempProcess.ItemType + tempProcess.ProcessItem.Name);
                }

                MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除成功"));
            }

            this.fsReport_Sheet1.Rows.Remove(rowIndex, 1);
            this.fsReport.VisibleAllList();

            return 1;
        }

        /// <summary>
        /// 生产工艺流程保存
        /// </summary>
        /// <returns>成功返回1 失败返回-1</returns>
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
                    MessageBox.Show("保存制剂工艺流程信息失败" + this.preparationManager.Err);
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("工艺流程执行信息保存成功");

            return 1;
        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                
                this.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("数据初始化发生错误"));
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

        #region 列枚举

        /// <summary>
        /// 模版列枚举
        /// </summary>
        private enum StencilColumnEnum
        {
            /// <summary>
            /// 类别
            /// </summary>
            ColType,
            /// <summary>
            /// 项目
            /// </summary>
            ColItem,
            /// <summary>
            /// 标准下限值
            /// </summary>
            ColMin,
            /// <summary>
            /// 标准上限值
            /// </summary>
            ColMax,
            /// <summary>
            /// 标准现象
            /// </summary>
            ColDes
        }

        private enum ReportColumnEnum
        {
            /// <summary>
            /// 类别
            /// </summary>
            ColType,
            /// <summary>
            /// 项目
            /// </summary>
            ColItem,
            /// <summary>
            /// 实际值
            /// </summary>
            ColNum,
            /// <summary>
            /// 现象
            /// </summary>
            ColDes,
            /// <summary>
            /// 是否合格
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
                if (this.ConvertStringToBool(this.fsReport_Sheet1.Cells[e.Row, e.Column].Text))     //合格
                {
                    this.fsReport_Sheet1.Rows[e.Row].ForeColor = System.Drawing.Color.Black;
                }
                else                                                                               //不合格
                {
                    this.fsReport_Sheet1.Rows[e.Row].ForeColor = System.Drawing.Color.Red;
                }
            }           
        }
    }
}