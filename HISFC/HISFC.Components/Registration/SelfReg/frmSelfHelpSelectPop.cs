using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Registration.SelfReg
{ 
    /// <summary>
    /// [功能描述: 自助挂号弹出选择窗口]<br></br>
    /// [创 建 者: 牛鑫元]<br></br>
    /// [创建时间: 2009-9]<br></br>
    /// <说明
    ///		贵港本地化
    ///  />
    /// </summary>
    public partial class frmSelfHelpSelectPop : Form
    {
        public event EventHandler ChooseItem;
        public frmSelfHelpSelectPop()
        {
            InitializeComponent();
        }

        #region 变量域

        /// <summary>
        /// 弹出窗口类型
        /// </summary>
        private EnumPopType enumPopType;

        /// <summary>
        /// 综合管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        
        #endregion

        #region 属性
        /// <summary>
        /// 弹出窗口类型
        /// </summary>
        public EnumPopType EnumPopType
        {
            set
            {
                this.enumPopType = value;
                this.ShowInfo();
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 设置挂号信息
        /// </summary>
        /// <returns></returns>
        private int ShowDeptInfo()
        {
            ArrayList alDept = this.managerIntegrate.QueryRegDepartment();
            if (alDept == null)
            {
                MessageBox.Show("查询挂号科室出错" + this.managerIntegrate.Err);
            }

            this.SetFarpointValue(alDept);
            return 1;
        }


        /// <summary>
        /// 设置farpoint
        /// </summary>
        /// <param name="alColections"></param>
        private void SetFarpointValue(ArrayList alColections)
        {
            int myMod = 0;
            int rowCount = Math.DivRem(alColections.Count, 5, out myMod);

            if (myMod > 0)
            {
                rowCount = rowCount + 1;
            }

            for (int i = 0; i < alColections.Count; i++)
            {
                int k = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(i / 5))); //求余和商

                int mod = 0;

                Math.DivRem(i, 5, out mod);

                FS.FrameWork.Models.NeuObject obj = alColections[i] as FS.FrameWork.Models.NeuObject;

                FarPoint.Win.Spread.CellType.ButtonCellType btCell = new FarPoint.Win.Spread.CellType.ButtonCellType();
                this.neuSpread1_Sheet1.RowCount = FS.FrameWork.Function.NConvert.ToInt32(rowCount);
                this.neuSpread1_Sheet1.ColumnCount = 5;

                btCell.Text = obj.Name + "\n(" + obj.ID + ")";
                this.neuSpread1_Sheet1.Cells[k, mod].CellType = btCell;
                btCell.Picture = global::FS.HISFC.Components.Registration.Properties.Resources.科室;
                this.neuSpread1_Sheet1.Cells[k, mod].Tag = obj;
            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <returns></returns>
        private int ShowInfo()
        {
            if (this.enumPopType == EnumPopType.Dept)
            {
                this.ShowDeptInfo();
            }
            return 1;
        }
        #endregion

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = this.neuSpread1_Sheet1.ActiveCell.Tag as FS.FrameWork.Models.NeuObject;
            if (this.ChooseItem != null)
            {
                this.ChooseItem(obj, e);
                this.DialogResult = DialogResult.OK;
                this.Close();
            } 
        }

    }

    /// <summary>
    /// 弹出选着窗口类型
    /// </summary>
    public enum EnumPopType
    {
        /// <summary>
        /// 科室
        /// </summary>
        Dept = 1,

        /// <summary>
        /// 挂号级别
        /// </summary>
        RegLevel
    }
}