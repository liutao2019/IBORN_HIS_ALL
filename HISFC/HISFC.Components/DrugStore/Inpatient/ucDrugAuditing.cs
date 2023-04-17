using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// [控件描述: 药房发药审核 {6DB2E467-CFBE-4cf1-B5E9-C48BAEFDC487}]
    /// [创 建 人: Sunjh]
    /// [创建时间: 2010-9-14]
    /// </summary>
    public partial class ucDrugAuditing : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugAuditing()
        {
            InitializeComponent();
        }

        #region 变量

        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        private FS.HISFC.BizLogic.Pharmacy.DrugStore dsManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 审核状态（结果）
        /// </summary>
        private string auditingStates = "审核通过;审核未通过;";

        /// <summary>
        /// 是否允许查询其他库存科室
        /// </summary>
        private bool isQueryOthersDept = false;

        #endregion

        #region 属性

        /// <summary>
        /// 审核状态（结果）
        /// </summary>
        [Description("审核状态（结果），请用 1位数字+状态名称+英文分号“;” 进行状态设置"), Category("设置"), DefaultValue("审核通过;审核未通过;")]
        public string AuditingStates
        {
            get 
            { 
                return auditingStates; 
            }
            set 
            { 
                auditingStates = value;
            }
        }

        /// <summary>
        /// 是否允许查询其他库存科室
        /// </summary>
        [Description("是否允许查询其他库存科室"), Category("设置"), DefaultValue(false)]
        public bool IsQueryOthersDept
        {
            get
            {
                return isQueryOthersDept;
            }
            set
            {
                isQueryOthersDept = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化界面控件
        /// </summary>
        public void InitControls()
        {            
            //加载药房列表
            ArrayList alStockDept = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            this.cbbStockDept.AddItems(alStockDept);
            this.cbbStockDept.Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;
            if (this.isQueryOthersDept)
            {
                if (this.cbbStockDept.SelectedIndex < 0)
                {
                    this.cbbStockDept.SelectedIndex = 0;
                }
            }
            else
            {
                this.cbbStockDept.Enabled = false;
            }

            //加载审核状态列表
            ArrayList alStates = this.GetAuditingStates(this.auditingStates);
            this.cbbStates.AddItems(alStates);
            this.cbbStates.SelectedIndex = 0;
            this.cbbAuditingResult.AddItems(alStates);
            this.cbbAuditingResult.SelectedIndex = 0;

            //加载列表设置
            FarPoint.Win.Spread.CellType.ComboBoxCellType cellCbbType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            string[] strArg = new string[alStates.Count];
            for (int i = 0; i < alStates.Count; i++)
            {
                FS.FrameWork.Models.NeuObject tempColumnType = alStates[i] as FS.FrameWork.Models.NeuObject;
                strArg[i] = tempColumnType.Name;                
            }

            cellCbbType.Items = strArg;// new string[] { strArg };
            this.fpAuditingList.Columns[9].CellType = cellCbbType;

            //设置查询时间
            this.dtpBegin.Value = this.itemManager.GetDateTimeFromSysDateTime().Date;
            this.dtpEnd.Value = this.itemManager.GetDateTimeFromSysDateTime().Date.AddDays(1);
            
        }

        /// <summary>
        /// 分解状态字符串返回数组
        /// </summary>
        /// <param name="statesStr"></param>
        /// <returns></returns>
        private ArrayList GetAuditingStates(string statesStr)
        {
            ArrayList alStates = new ArrayList();
            string tempStr = "";            
            int divIndex = 0;

            FS.FrameWork.Models.NeuObject tempModel = new FS.FrameWork.Models.NeuObject();
            tempModel.ID = "0";
            tempModel.Name = "未审核";
            alStates.Add(tempModel);

            while (statesStr.Length > 0)
            {
                divIndex = statesStr.IndexOf(";") + 1;
                tempStr = statesStr.Substring(0, divIndex);
                tempStr = tempStr.Replace(";", "");
                statesStr = statesStr.Substring(divIndex);
                tempModel = new FS.FrameWork.Models.NeuObject();
                tempModel.ID = tempStr.Trim();
                tempModel.Name = tempStr.Trim();
                alStates.Add(tempModel);
            }

            return alStates;
        }

        /// <summary>
        /// 查询并显示审核数据
        /// </summary>
        public void QueryData()
        {            
            ArrayList alTemp = this.dsManager.QueryDrugAuditing("Z", this.cbbStates.SelectedItem.ID, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
            this.fpAuditingList.RowCount = 0;
            if (alTemp != null)
            {
                for (int i = 0; i < alTemp.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut appObj = alTemp[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    this.fpAuditingList.RowCount = i + 1;
                    this.fpAuditingList.Cells[i, 0].Text = appObj.Name;
                    this.fpAuditingList.Cells[i, 1].Text = appObj.RecipeInfo.Dept.Name;
                    this.fpAuditingList.Cells[i, 2].Text = appObj.Item.Name;
                    this.fpAuditingList.Cells[i, 3].Text = appObj.Item.Specs;
                    this.fpAuditingList.Cells[i, 4].Text = appObj.Item.Qty.ToString();
                    this.fpAuditingList.Cells[i, 5].Text = appObj.Item.MinUnit;
                    this.fpAuditingList.Cells[i, 6].Text = appObj.Frequency.Name;
                    this.fpAuditingList.Cells[i, 7].Text = appObj.Usage.Name;
                    this.fpAuditingList.Cells[i, 8].Text = appObj.State;
                    this.fpAuditingList.Cells[i, 9].Text = appObj.User01;
                    this.fpAuditingList.Cells[i, 10].Text = appObj.Memo;
                    this.fpAuditingList.Cells[i, 11].Text = appObj.Operation.ApproveOper.Name;
                    if (appObj.Operation.ApproveOper.OperTime < Convert.ToDateTime("2010-1-2"))
                    {
                        this.fpAuditingList.Cells[i, 12].Text = "";
                    }
                    else
                    {
                        this.fpAuditingList.Cells[i, 12].Text = appObj.Operation.ApproveOper.OperTime.ToString();
                    }
                    this.fpAuditingList.Rows[i].Tag = appObj;
                }
            }
        }

        public void SaveAuditing()
        {
            if (this.fpAuditingList.RowCount < 1)
            {
                MessageBox.Show("没有需要审核的数据!");

                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            for (int i = 0; i < this.fpAuditingList.RowCount; i++)
            {
                if (this.cbbStates.SelectedItem.ID != "0")
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut appObj = this.fpAuditingList.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (this.fpAuditingList.Cells[i, 9].Text != appObj.User01)
                    {
                        if (this.dsManager.DeleteDrugAuditing(appObj.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("取消审核失败" + this.dsManager.Err);

                            return;
                        }
                        if (this.fpAuditingList.Cells[i, 9].Text != "未审核")
                        {
                            FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                            tempObj.ID = appObj.ID;
                            tempObj.Name = FS.FrameWork.Management.Connection.Operator.ID;
                            tempObj.Memo = this.fpAuditingList.Cells[i, 10].Text;
                            tempObj.User01 = this.fpAuditingList.Cells[i, 9].Text;
                            tempObj.User02 = this.itemManager.GetDateTimeFromSysDateTime().ToString();
                            if (this.dsManager.InsertDrugAuditing("Z", tempObj) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("保存失败" + this.dsManager.Err);

                                return;
                            }
                        }                        
                    }
                }
                else
                {
                    if (this.fpAuditingList.Cells[i, 9].Text != "未审核" && this.fpAuditingList.Cells[i, 9].Text != "")
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut appObj = this.fpAuditingList.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                        FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                        tempObj.ID = appObj.ID;
                        tempObj.Name = FS.FrameWork.Management.Connection.Operator.ID;
                        tempObj.Memo = this.fpAuditingList.Cells[i, 10].Text;
                        tempObj.User01 = this.fpAuditingList.Cells[i, 9].Text;
                        tempObj.User02 = this.itemManager.GetDateTimeFromSysDateTime().ToString();
                        if (this.dsManager.InsertDrugAuditing("Z", tempObj) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败" + this.dsManager.Err);

                            return;
                        }
                    }
                }                
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功");
            this.QueryData();
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveAuditing();
            return base.OnSave(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print pp = new FS.FrameWork.WinForms.Classes.Print();
            pp.PrintPreview(5, 5, this.pnlMain);
            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            return base.Export(sender, neuObject);
        }

        private void chkIsQueryByTime_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIsQueryByTime.Checked)
            {
                this.dtpBegin.Enabled = true;
                this.dtpEnd.Enabled = true;
            }
            else
            {
                this.dtpBegin.Enabled = false;
                this.dtpEnd.Enabled = false;
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            if (this.fpAuditingList.SelectionCount > 0)
            {
                for (int i = 0; i < this.fpAuditingList.RowCount; i++)
                {
                    if (this.fpAuditingList.IsSelected(i, 0))
                    {
                        this.fpAuditingList.Cells[i, 9].Text = this.cbbAuditingResult.Text;
                    }
                }
            }
            else
            {
                MessageBox.Show("没有选择需要审批的数据!");
                return;
            }
        }

        private void cbbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fpAuditingList.RowCount = 0;
            this.QueryData();
        }

    }
}
