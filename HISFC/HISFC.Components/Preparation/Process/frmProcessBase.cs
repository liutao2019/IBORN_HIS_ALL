using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Preparation.Process
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 工艺流程(根据模版录入)基类]<br></br>
    /// [创 建 者: 飞斯]<br></br>
    /// [创建时间: 2008-03]<br></br>
    /// <待解决问题>
    /// </待解决问题>
    /// </summary>
    public partial class frmProcessBase : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmProcessBase()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 人员列表
        /// </summary>
        protected System.Collections.ArrayList alStaticEmployee = null;

        /// <summary>
        /// 科室列表
        /// </summary>
        protected System.Collections.ArrayList alStaticDept = null;

        /// <summary>
        /// 模版类型
        /// </summary>
        protected FS.HISFC.Models.Preparation.EnumStencialType stencilType = FS.HISFC.Models.Preparation.EnumStencialType.SemiAssayStencial;

        #endregion

        #region 属性

        /// <summary>
        /// 模版类型
        /// </summary>
        public FS.HISFC.Models.Preparation.EnumStencialType StencilType
        {
            get
            {
                return this.stencilType;
            }
            set
            {
                this.stencilType = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            if (alStaticEmployee == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                alStaticEmployee = managerIntegrate.QueryEmployeeAll();
                if (alStaticEmployee == null)
                {
                    MessageBox.Show("加载人员列表发生错误" + managerIntegrate.Err);
                    return;
                }
            }

            if (this.alStaticDept == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                this.alStaticDept = managerIntegrate.QueryDeptmentsInHos(false);
                if (alStaticDept == null)
                {
                    MessageBox.Show("加载科室列表发生错误" + managerIntegrate.Err);
                    return;
                }
            }
        }

        #endregion

        /// <summary>
        /// 将"是"、"否" 字符串转换为Bool
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected bool ConvertStringToBool(string str)
        {
            if (str == "是")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将Bool转换为"是"、"否" 字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string ConvertBoolToString(bool bl)
        {
            if (bl)
            {
                return "是";
            }
            else
            {
                return "否";
            }
        }

        /// <summary>
        /// 根据不同的项目类型 设置单元格属性
        /// </summary>
        /// <param name="fp">需更改的FarPoint</param>
        /// <param name="itemType">项目类型</param>
        /// <param name="rowIndex">单元格行索引</param>
        /// <param name="columnIndex">单元格列索引</param>
        protected void SetReportCellType(FPItem fp,FarPoint.Win.Spread.SheetView sv,FS.HISFC.Models.Preparation.EnumStencilItemType itemType, int rowIndex, int columnIndex)
        {
            switch (itemType)
            {
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Person:
                    fp.SetColumnList(sv, this.alStaticEmployee, columnIndex);
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Dept:
                    fp.SetColumnList(sv, this.alStaticDept, columnIndex);
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Date:
                    FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType markDateCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType();
                    sv.Cells[rowIndex, columnIndex].CellType = markDateCellType;
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Number:
                    FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
                    sv.Cells[rowIndex, columnIndex].CellType = numCellType;
                    break;
                default:
                    break;
            }
        }        
    }
}