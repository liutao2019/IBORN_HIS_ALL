using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using FS.HISFC.Models.Fee;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;
using System.Text.RegularExpressions;

namespace FoShanYDSI.Controls
{
    /// <summary>
    /// 项目对照维护
    /// </summary>
    public partial class ucUpFeeDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucUpFeeDetail()
        {
            InitializeComponent();


        }

        #region 事件
        /// 工具栏
        /// </summary>
        private ToolBarService toolBarService = new ToolBarService();

        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 患者入出转管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();


        FS.HISFC.Models.RADT.PatientInfo inPatientInfo = null;

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 佛山异地医保业务逻辑层
        /// </summary>
        FoShanYDSI.Business.InPatient.InPatientService inPatientService = new FoShanYDSI.Business.InPatient.InPatientService();
       
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //清屏
            this.Clear();

            base.OnLoad(e);
        }


        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            //this.toolBarService.AddToolButton("待上传患者", "待上传患者查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            //this.toolBarService.AddToolButton("一键上传", "一键上传病种分值付费接口", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //switch (e.ClickedItem.Text)
            //{
            //    case "待上传患者":
            //        this.Clear();
            //        break;
            //}

            base.ToolStrip_ItemClicked(sender, e);
        }
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            this.lblItemInfo.Text = "";
            //this.txtPatientNO.Text = "";
            inPatientInfo = null;
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Clear();
            string patientNo = this.txtPatientNO.Text.Trim();
            if (string.IsNullOrEmpty(patientNo))
            {
                return -1;
            }

            patientNo = patientNo.PadLeft(10,'0');


            ArrayList alPatientList = this.inPatientService.QueryInpatienInfo(patientNo);
               
            if (alPatientList == null || alPatientList.Count <= 0)
            {
                MessageBox.Show("该患者没有住院信息！！！");
                return -1;

            }
            FS.FrameWork.Models.NeuObject selectPatient = null;
            foreach (FS.HISFC.Models.Base.Spell obj in alPatientList)
            {
                string inState = "";
                switch (obj.Memo)
                {
                    case "I":
                        inState = "在院";
                        break;
                    case "R":
                        inState = "住院登记";
                        break;
                    case "B":
                        inState = "出院登记";
                        break;
                    case "O":
                        inState = "出院结算";
                        break;
                    case "P":
                        inState = "预约出院";
                        break;
                    case "N":
                        inState = "无费退院";
                        break;
                    default:
                        inState = obj.Memo;
                        break;

                }

                obj.Memo = inState;//在院状态
            }

            if (alPatientList.Count == 1)
            {
                selectPatient = alPatientList[0] as FS.FrameWork.Models.NeuObject;
            }
            else if (alPatientList.Count > 1)
            {
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alPatientList, new string[] { "住院流水号", "姓名", "在院状态", "科室名称", "入院时间", "科室编码" }, new bool[] { false, true, true, true, true, true }, new int[] { 50, 50, 70, 100, 100, 120 }, ref selectPatient) != 1)
                {
                    return -1;
                }
            }
            if (selectPatient == null || string.IsNullOrEmpty(selectPatient.ID))
            {
                MessageBox.Show("请选择一条住院记录！");
                return -1;
            }

            this.inPatientInfo = this.radtIntegrate.GetPatientInfomation(selectPatient.ID);

            FoShanYDSI.Objects.SIPersonInfo ps = new FoShanYDSI.Objects.SIPersonInfo();
            ArrayList al = new ArrayList();
            string msg = "";
            if (this.inPatientService.QueryInpatienRegInfo(selectPatient.ID, ref ps) < 1)
            {
                MessageBox.Show("查询异地医保患者已上传费用信息失败", "佛山异地医保");
                return -1;
            }
            string status = inPatientService.QueryInPatientUpFeeDetail(this.inPatientInfo, ps, ref al, ref msg);
            if (status != "1")
            {
                MessageBox.Show("查询异地医保患者已上传费用信息失败！" + msg, "佛山异地医保");
                return -1;
            }
            if (al != null && al.Count > 0)
            {
                this.ToFP(al);
            }
            else
            {
                MessageBox.Show("没有费用信息！" + msg, "佛山异地医保");
                this.fpItemInfo_Sheet1.Rows.Count = 0;
            }

            return base.OnQuery(sender, neuObject);
        }

        private void ToFP(ArrayList al)
        {
            this.fpItemInfo_Sheet1.Rows.Count = 0;

            int rowIndex = 0;
            decimal totCost = 0;
            string totalrow = "";
            if (al != null && al.Count > 0)
            {
                this.fpItemInfo_Sheet1.Rows.Count = al.Count;
                foreach (FoShanYDSI.Objects.SIPersonFeeInfo f in al)
                {
                    totalrow = f.totalrow ;
                    //this.fpItemInfo_Sheet1.Rows.Add(rowIndex, 1);
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 0].Value = f.seqno;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 1].Value = f.ykc700;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 2].Value = f.ykc618;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 3].Value = f.yka111;//
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 4].Value = f.yka112;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 5].Value = f.ake001;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 6].Value = f.ake002;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 7].Value = f.ake114;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 8].Value = f.aka185;//
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 9].Value = f.yke230;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 10].Value = f.yke231;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 11].Value = f.ake005;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 12].Value = f.ake006;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 13].Value = f.ykc610;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 14].Value = f.akc264;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 15].Value = f.aka069;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 16].Value = f.akc228;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 17].Value = f.akc253;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 18].Value = f.akc254;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 19].Value = f.yka319;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 20].Value = f.aka065;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 21].Value = f.akc226;
                    this.fpItemInfo_Sheet1.Cells[rowIndex, 22].Value = f.akc225;
                    rowIndex++;

                    totCost += decimal.Parse(f.akc264);
                }

                //this.fpItemInfo_Sheet1.Rows.Add(rowIndex, 1);
                //rowIndex = this.fpItemInfo_Sheet1.Rows.Count;
                //this.fpItemInfo_Sheet1.Cells[rowIndex, 0].Value = "合计";
                //this.fpItemInfo_Sheet1.Cells[rowIndex, 14].Value = totCost.ToString("F2");

            }
            this.lblItemInfo.Text = "总行数：" + totalrow + "   费用总金额合计：" + totCost.ToString("F2");
        }
       
        #endregion

        private void txtPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnQuery(null,null);
            }
        }


    }
}
