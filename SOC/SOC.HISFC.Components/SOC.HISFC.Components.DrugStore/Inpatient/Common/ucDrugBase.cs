using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    /// <summary>
    /// [功能描述: 住院药房发药基类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// 说明：
    /// 1、不允许添加业务流程相关的代码
    /// </summary>
    public partial class ucDrugBase : Base.ucDrugBaseComponent
    {
        public ucDrugBase()
        {
            InitializeComponent();
        }

        #region 属性变量
        protected FS.FrameWork.Models.NeuObject priveDept;

        /// <summary>
        /// 权限科室，当前获取、显示数据的科室
        /// </summary>
        [Description("权限科室"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject PriveDept
        {
            get { return priveDept; }
            set { priveDept = value; }
        }


        private FS.FrameWork.Models.NeuObject stockDept;

        /// <summary>
        /// 当前操作数据保存、扣库等动作的科室
        /// </summary>
        [Description("扣库科室"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject StockDept
        {
            get { return stockDept; }
            set { stockDept = value; }
        }

        private bool isCheckPrivePower = false;

        /// <summary>
        /// 是否判断发药权限
        /// </summary>
        [Description("是否判断发药权限"), Category("设置"), Browsable(true)]
        public bool IsCheckPrivePower
        {
            get { return isCheckPrivePower; }
            set { isCheckPrivePower = value; }
        }

        private string privePowerString = "0320+Z1";

        [Description("使用窗口需要的权限,如：0320+Z1"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }

        private bool isOtherDeptDrug = false;

        /// <summary>
        /// 是否属于它科发药，必须检查操作员是否有当前科室和对方科室的权限
        /// </summary>
        [Description("是否它科发药，将检查是否有双方科室的权限"), Category("设置"), Browsable(true)]
        public bool IsOtherDeptDrug
        {
            get { return isOtherDeptDrug; }
            set
            {
                isOtherDeptDrug = value;
                this.IsCheckPrivePower = true;
            }
        }

        private FS.HISFC.Models.Pharmacy.DrugControl drugControl;

        /// <summary>
        /// 摆药台
        /// </summary>
        public FS.HISFC.Models.Pharmacy.DrugControl DrugControl
        {
            get { return drugControl; }
            set { drugControl = value; }
        }

        /// <summary>
        /// 药房发药总量显示单位
        /// </summary>
        private Function.EnumQtyShowType enumQtyShowType = Function.EnumQtyShowType.中成药包装单位;

        /// <summary>
        /// 药房发药总量显示单位
        /// </summary>
        [Description("药房发药总量显示单位"), Category("设置"),Browsable(true)]
        public Function.EnumQtyShowType EnumQtyShowType
        {
            get { return enumQtyShowType; }
            set { enumQtyShowType = value; }
        }

        private bool isAutoSelected = false;

        /// <summary>
        /// 添加摆药通知（汇总信息）时是否自动选中
        /// </summary>
        [Description("添加摆药通知（汇总信息）时是否自动选中"), Category("设置"), Browsable(true)]
        public bool IsAutoSelected
        {
            get { return isAutoSelected; }
            set { isAutoSelected = value; }
        }
        #endregion

        #region 接口

        private bool isCreatedInpatientDrug = false;

        /// <summary>
        /// 住院药房接口，主要是打印
        /// </summary>
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug myIInpatientDrug;

        /// <summary>
        /// 住院药房接口，主要是打印
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug IInpatientDrug
        {
            get
            {
                if (this.DesignMode)
                {
                    return null;
                }
                if (myIInpatientDrug == null && !isCreatedInpatientDrug)
                {
                    try
                    {
                        isCreatedInpatientDrug = true; 
                        myIInpatientDrug = (InterfaceManager.GetInpatientDrug() as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug);
                    }
                    catch
                    {
                        myIInpatientDrug = null;
                    }
                }
                return myIInpatientDrug;
            }
        }

        #endregion

        #region 提示信息

        /// <summary>
        /// 提供在状态栏第一panel显示信息的功能
        /// </summary>
        /// <param name="text">显示信息的文本</param>
        public void ShowStatusBarTip(string text)
        {
            if (this.ParentForm != null)
            {
                if (this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).statusBar1.Panels[1].Text = "       " + text + "       ";
                }
            }
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        public void ShowMessage(string text)
        {
            ShowMessage(text, MessageBoxIcon.Information);
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        public void ShowMessage(string text, MessageBoxIcon messageBoxIcon)
        {
            string caption = "";
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }

            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, messageBoxIcon);
        }

        #endregion

        #region 配发药公用的工具栏

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        public FS.FrameWork.WinForms.Forms.ToolBarService ToolBarService
        {
            get { return toolBarService; }
            set { toolBarService = value; }
        }
                
        #endregion
    }
}
