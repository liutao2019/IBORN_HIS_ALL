using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// [功能描述: 住院药房摆药台选择]<br></br>
    /// [创 建 者: liangzj]<br></br>
    /// [创建时间: ]<br></br>
    /// 说明：
    /// 1、去掉了科室树，保留了总部大部分功能
    /// </summary>
    public partial class frmChooseDrugControl : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmChooseDrugControl()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.neuSpread1.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
            this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            if (System.IO.File.Exists(settingFileName))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, settingFileName);
            }
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, settingFileName);
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        private string settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ChooseDrugControlSetting.xml";

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 当前选择的摆药台
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugControl drugControl;

        /// <summary>
        /// 当前选择的摆药台
        /// </summary>
        public FS.HISFC.Models.Pharmacy.DrugControl DrugControl
        {
            get { return drugControl; }
        }

        /// <summary>
        /// 显示本科室全部摆药台列表
        /// </summary>
        public virtual int ShowControlList(FS.FrameWork.Models.NeuObject dept)
        {
            //判断科室编码是否存在
            if (dept == null)
            {
                MessageBox.Show(Language.Msg("无效的摆药科室！没有可以选择的摆药台"));
                return -1;
            }

            string deptCode = dept.ID;
           
            //判断科室编码是否存在
            if (deptCode == "")
            {
                MessageBox.Show(Language.Msg("无效的摆药科室！没有可以选择的摆药台"));
                return -1;
            }

            this.nlbTitly.Text = dept.Name + "摆药台信息";

            //清除当前显示的摆药台
            this.neuSpread1_Sheet1.Rows.Count = 0;


            //定义药房管理类
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            //取本科室全部摆药台列表
            ArrayList al = drugStoreManager.QueryDrugControlList(deptCode);
            if (al == null)
            {
                MessageBox.Show(drugStoreManager.Err);
                return -1;
            }
            if (al.Count == 0)
            {
                MessageBox.Show(Language.Msg("您所在的科室没有设置摆药台，请先设置本科室的摆药台。"));
                return -1;
            }

            this.neuSpread1_Sheet1.Rows.Add(0, al.Count);
            FS.HISFC.Models.Pharmacy.DrugControl drugControl;
            for (int i = 0; i < al.Count; i++)
            {
                drugControl = al[i] as FS.HISFC.Models.Pharmacy.DrugControl;

                FarPoint.Win.Spread.CellType.ButtonCellType btnType = new FarPoint.Win.Spread.CellType.ButtonCellType();
                btnType.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(225)), ((System.Byte)(243)));
                btnType.Text = drugControl.Name;
                btnType.TextDown = drugControl.Name;
                this.neuSpread1_Sheet1.Cells[i, 0].CellType = btnType;

                this.neuSpread1_Sheet1.Cells[i, 1].Text = drugControl.SendType == 0 ? "全部" : (drugControl.SendType == 1 ? "集中" : "临时");
                this.neuSpread1_Sheet1.Cells[i, 2].Text = drugControl.ShowLevel == 0 ? "显示科室汇总" : (drugControl.ShowLevel == 1 ? "显示科室明细" : "显示患者明细");
                this.neuSpread1_Sheet1.Rows[i].Tag = drugControl;
            }

            if (al.Count == 1)
            {
                this.drugControl = al[0] as FS.HISFC.Models.Pharmacy.DrugControl;
                this.DialogResult = DialogResult.OK;
            }

            return al.Count;
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                this.drugControl = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Pharmacy.DrugControl;
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
        }

    }
}
