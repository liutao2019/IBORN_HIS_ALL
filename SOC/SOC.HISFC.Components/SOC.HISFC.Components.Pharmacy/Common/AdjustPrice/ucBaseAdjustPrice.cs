using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    /// <summary>
    /// [功能描述: 调价基类控件]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-12]<br></br>
    /// 说明：
    /// 1、调价应该有调零售、购入、批发价的功能
    /// </summary>
    public partial class ucBaseAdjustPrice : Base.ucBaseControl//, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucBaseAdjustPrice()
        {
            InitializeComponent();
        }

        #region 保护变量

        protected SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail curIDataDetail = null;
        protected FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList curIDataChooseList = null;
        protected FS.SOC.HISFC.Components.Common.Base.baseTreeView curTreeView = null;
        protected FS.FrameWork.Models.NeuObject curPriveType = null;
        protected FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting curChooseDataSetting = null;

        protected System.Data.DataTable dtDetail = null;
        protected Hashtable hsAdjustPrice = new Hashtable();

        protected string settingFileName = "";
        protected uint costDecimals = 2;

        protected FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();

        protected FS.SOC.HISFC.BizLogic.Pharmacy.Adjust adjustMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Adjust();

        #endregion

        #region 属性及所用变量

        string class2Code = "0304";

        /// <summary>
        /// 二级权限
        /// </summary>
        [Description("二级权限"), Category("设置"), Browsable(true)]
        public string Class2Code
        {
            get { return class2Code; }
            set { class2Code = value; }
        }

        string class3Code = "01";

        /// <summary>
        /// 三级权限
        /// </summary>
        [Description("三级权限"), Category("设置"), Browsable(true)]
        public string Class3Code
        {
            get { return class3Code; }
            set { class3Code = value; }
        }

        private uint daySpan = 3;

        /// <summary>
        /// 时间间隔天数
        /// </summary>
        [Description("时间间隔天数"), Category("设置"), Browsable(true)]
        public uint DaySpan
        {
            get { return daySpan; }
            set { daySpan = value; }
        }
        #endregion       

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("列表", "切换到树形列表", FS.FrameWork.WinForms.Classes.EnumImageList.L列表, false, false, null);
            toolBarService.AddToolButton("制单", "新增加调价单", FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("全部删除", "", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("公式", "设置价格生成公式", FS.FrameWork.WinForms.Classes.EnumImageList.R日消耗, true, false, null);

            return toolBarService;
        }

        public override int Export(object sender, object neuObject)
        {
            this.curIDataDetail.FpSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);
        }

        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.curIDataDetail = ucDataDetail;
            this.curIDataDetail.Info = "";
            this.curIDataChooseList = ucTreeViewChooseList;
            this.curTreeView = ucTreeViewChooseList.TreeView;

            //设置开始和结束时间
            this.ndtpEnd.Value = DateTime.Now.AddDays(1).AddSeconds(-1);
            this.ndtpBegin.Value = this.ndtpEnd.Value.Date.AddDays(-this.DaySpan);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {

                if (this.curIDataDetail.IsContainsFocus)
                {
                    if (this.curIDataDetail.SetFocus() == 0 && !this.curTreeView.Visible)
                    {
                        this.curIDataChooseList.SetFocusToFilter();
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion

    }
}
