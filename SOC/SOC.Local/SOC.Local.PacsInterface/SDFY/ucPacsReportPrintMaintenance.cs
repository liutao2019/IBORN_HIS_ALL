using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;

namespace SOC.Local.PacsInterface.SDFY
{
    /// <summary>
    /// 检查申请单打印维护
    /// </summary>
    public partial class ucPacsReportPrintMaintenance : UserControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucPacsReportPrintMaintenance()
        {
            InitializeComponent();
        }

        #region 变量

        #region 对照的常数维护

        /// <summary>
        /// 打印单据类型
        /// </summary>
        private string constType_ReportType = "PACSREPORTTYPE";

        /// <summary>
        /// 单据类型和项目对照
        /// </summary>
        private string constType_ReportType_Item = "REPORTTYPE_ITEM";

        #endregion

        /// <summary>
        /// 单据类型列表
        /// </summary>
        private ArrayList alConst_ReportType = null;

        /// <summary>
        /// 单据类型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper constHelper_ReportType = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 非药品项目列表
        /// </summary>
        private ArrayList alConst_UndrugItem = null;

        /// <summary>
        /// 非药品项目帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper constHelper_UndrugItem = null;

        /// <summary>
        /// 单据类型和项目对照列表
        /// </summary>
        private ArrayList alConst_ReportType_Item = new ArrayList();

        /// <summary>
        /// 单据类型和项目对照帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper constHelper_ReportType_Item = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 挂号综合管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 显示的对照数据
        /// </summary>
        private DataSet myDataSet = new DataSet();

        /// <summary>
        /// 交叉管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        #endregion

        #region 方法

        /// <summary>
        /// 初始化列表
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);

            #region 初始化列表

            //单据类型
            this.alConst_ReportType = consManager.GetList(this.constType_ReportType);
            if (alConst_ReportType == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("加载常数[" + constType_ReportType + "]列表发生错误！"));
                return -1;
            }
            this.constHelper_ReportType = new FS.FrameWork.Public.ObjectHelper(alConst_ReportType);

            //非药品项目
            alConst_UndrugItem = this.feeIntegrate.QueryValidItems();
            if (alConst_UndrugItem == null)
            {
                MessageBox.Show("查询收费项目出错：" + this.feeIntegrate.Err);
                return -1;
            }
            constHelper_UndrugItem = new FS.FrameWork.Public.ObjectHelper(alConst_UndrugItem);

            #endregion

            this.neuSpread1_Sheet1.Columns[1].Locked = true;
            this.neuSpread1_Sheet1.Columns[3].Locked = true;
            this.neuSpread1_Sheet1.Columns[5].Locked = true;
            this.neuSpread1_Sheet1.Columns[6].Locked = true;

            return this.Query();
        }

        /// <summary>
        /// 双击弹出选择项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            if (e.Column == 0)
            {
                this.PopItem(this.alConst_ReportType, 0);
            }
            else if (e.Column == 2)
            {
                this.PopItem(this.alConst_UndrugItem, 2);
            }
        }

        /// <summary>
        /// 弹出常数选择
        /// </summary>
        private void PopItem(ArrayList al, int index)
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                //单据类型
                if (index == 0)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Value = info.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Value = info.Name;
                }
                //收费项目
                else
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Value = info.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 3].Value = info.Name;
                }

                this.neuSpread1_Sheet1.ActiveColumnIndex = 2;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.neuSpread1_Sheet1.ActiveColumnIndex == 0)
                    {
                        this.PopItem(this.alConst_ReportType, 0);
                    }
                    else if (this.neuSpread1_Sheet1.ActiveColumnIndex == 2)
                    {
                        this.PopItem(this.alConst_UndrugItem, 2);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            alConst_ReportType_Item = this.interManager.GetConstantList(this.constType_ReportType_Item);
            if (alConst_ReportType_Item == null)
            {
                MessageBox.Show("查询单据类型和项目对照信息出错：" + interManager.Err);
                return -1;
            }
            else if (alConst_ReportType_Item.Count == 0)
            {
                return 1;
            }

            foreach (FS.HISFC.Models.Base.Const conObj in alConst_ReportType_Item)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                //单据类型编码
                this.neuSpread1_Sheet1.Cells[0, 0].Text = conObj.Name;
                //单据类型名称
                this.neuSpread1_Sheet1.Cells[0, 1].Text = this.constHelper_ReportType.GetName(conObj.Name);
                //项目编码
                this.neuSpread1_Sheet1.Cells[0, 2].Text = conObj.ID;
                //项目名称
                this.neuSpread1_Sheet1.Cells[0, 3].Text = this.constHelper_UndrugItem.GetName(conObj.ID);
                //是否有效
                this.neuSpread1_Sheet1.Cells[0, 4].Value = conObj.IsValid;
                //操作员
                this.neuSpread1_Sheet1.Cells[0, 5].Text = conObj.OperEnvironment.ID;
                //操作日期
                this.neuSpread1_Sheet1.Cells[0, 6].Text = conObj.OperEnvironment.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (!this.Valid())
                return -1;

            //现在全部更新效率太低，后面有空再改吧

            //this.neuSpread1.StopCellEditing();

            //for (int i = 0; i <= this.myDataSet.Tables[0].Rows.Count; i++)
            //{
            //    this.myDataSet.Tables[0].Rows[i].EndEdit();
            //}

            Hashtable hs_DoctLvl_RegLvl = new Hashtable();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                #region 保存单据类型和项目对照

                FS.HISFC.Models.Base.Const conObj_ReportType_Item = new FS.HISFC.Models.Base.Const();

                conObj_ReportType_Item.ID = this.neuSpread1_Sheet1.Cells[i, 2].Text.Trim();
                conObj_ReportType_Item.Name = this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                conObj_ReportType_Item.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 4].Value);
                conObj_ReportType_Item.OperEnvironment.OperTime = consManager.GetDateTimeFromSysDateTime();
                conObj_ReportType_Item.OperEnvironment.ID = this.neuSpread1_Sheet1.Cells[i, 5].Text.Trim();

                if (consManager.SetConstant(this.constType_ReportType_Item, conObj_ReportType_Item) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存失败：" + consManager.Err));
                    return -1;
                }
                #endregion
                try
                {
                    //    hs_DoctLvl_RegLvl.Add(conObj_DoctLvl_RegLvl.ID, conObj_DoctLvl_RegLvl.Name);
                    //    hs_RegLvl_DiagFee.Add(conObj_RegLvl_DiagFee.ID, conObj_RegLvl_DiagFee.Name);
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("第" + (i + 1).ToString() + "行数据重复，请修改！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"));
            return 0;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                for (int index = 0; index < this.neuSpread1_Sheet1.Columns.Count; index++)
                {
                    if (index == 0 || index == 2)
                    {
                        if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, index].Text))
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行编码不能为空！");
                            this.neuSpread1_Sheet1.ActiveRowIndex = i;
                            return false;
                        }
                        else if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, index + 1].Text))
                        {
                            MessageBox.Show("第" + (i + 1).ToString() + "行名称不能为空！");
                            this.neuSpread1_Sheet1.ActiveRowIndex = i;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        #endregion

        #region IMaintenanceControlable 成员

        public int Add()
        {
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.Rows.Count, 1);
            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
            this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 4].Value = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 5].Value = consManager.Operator.ID;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 6].Value = this.consManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Locked = false;
            return 0;
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

        public bool IsDirty
        {
            get
            {
                return true;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Modify()
        {
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

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
