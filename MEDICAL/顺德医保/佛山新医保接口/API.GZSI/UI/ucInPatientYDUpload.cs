using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using API.GZSI.Business;


namespace API.GZSI.UI
{
    /// <summary>
    /// 创建日期：2021-06-09
    /// 创建人：Giiber
    /// 功能说明：异地清分
    /// </summary>
    public partial class ucInPatientYDUpload : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        #region 业务管理类

        /// <summary>
        /// 住院患者信息管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        #endregion 

        #region 变量

        /// <summary>
        /// 交易流水号
        /// </summary>
        string SerialNumber = string.Empty;

        /// <summary>
        /// 交易版本号
        /// </summary>
        string strTransVersion = string.Empty;

        /// <summary>
        /// 交易验证码
        /// </summary>
        string strVerifyCode = string.Empty;

        /// <summary>
        /// 公共返回值
        /// </summary>
        int returnvalue = 0;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public ucInPatientYDUpload()
        {
            InitializeComponent();
            InitContorls();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <returns></returns>
        protected int InitContorls()
        {
            this.dtpMonth.Value = DateTime.Now.Date.AddMonths(-1);
            return 1;
        }


        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("清分确认", "确定上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("清分确认取消", "医嘱上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.refreshData();
            return 1;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清分确认":
                    this.QFCheck();
                    break;
                case "清分确认取消":
                    this.QFCancel();
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        private int refreshData()
        {
            this.ClearSheet();
            DateTime month = this.dtpMonth.Value.Date;

            List<FS.HISFC.Models.RADT.PatientInfo> siPatientList = this.localMgr.GetPatientForUpload(new DateTime(month.Year,month.Month,1),new DateTime(month.Year,month.Month + 1,1), "ALL", "ALL");

            if (siPatientList == null)
            {
                MessageBox.Show("查询患者里列表失败：" + this.localMgr.Err);
                return -1;
            }
            else
            {
                this.setData(siPatientList);
            }

            return 1;
        }

        /// <summary>
        /// 获取需要上传的明细
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> getSelectedPatientForCancel()
        {
            List<FS.HISFC.Models.RADT.PatientInfo> selectedPatient = new List<FS.HISFC.Models.RADT.PatientInfo>();

            FarPoint.Win.Spread.Model.CellRange cr = this.fpList_Sheet1.GetSelection(0);

            if (cr != null)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = (this.fpList_Sheet1.Rows[cr.Row]).Tag as FS.HISFC.Models.RADT.PatientInfo;
                selectedPatient.Add(patient);
            }

            return selectedPatient;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="siPatientList"></param>
        private void setData(List<FS.HISFC.Models.RADT.PatientInfo> siPatientList)
        {
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in siPatientList)
            {
                this.addRowToFp(patient);
            }
        }

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>  
        private int addRowToFp(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            int rowIndex = this.fpList_Sheet1.RowCount;
            this.fpList_Sheet1.Rows.Add(this.fpList_Sheet1.RowCount, 1);
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.PatientType].Text = obj.SIMainInfo.MedicalType.Name;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.CardNO].Text = obj.PID.CardNO;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.Name].Text = obj.Name;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.IDCard].Text = obj.IDCard;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.Sex].Text = obj.Sex.ID.ToString() == "F" ? "女" : "男";
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.PayKind].Text = obj.Pact.Name;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.InvoiceNO].Text = obj.SIMainInfo.InvoiceNo;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.ZJE].Value = obj.SIMainInfo.Medfee_sumamt;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.TCJE].Value = obj.SIMainInfo.Fund_pay_sumamt;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.ZFJE].Value = obj.SIMainInfo.Psn_part_am;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.JSY].Text = obj.SIMainInfo.OperInfo.ID;
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.CYSJ].Text = obj.PVisit.OutTime.ToString();
            this.fpList_Sheet1.Cells[rowIndex, (int)EnumListCol.JSSJ].Text = obj.SIMainInfo.OperDate.ToString();
            this.fpList_Sheet1.Rows[rowIndex].Tag = obj;
            return 1;
        }

        /// <summary>
        /// 清空表格
        /// </summary>
        /// <returns></returns>
        protected int ClearSheet()
        {
            this.fpList_Sheet1.RowCount = 0;
            return 1;
        }

        /// <summary>
        /// 清空所有信息
        /// </summary>
        /// <returns></returns>
        protected int Clear()
        {
            this.ClearSheet();
            return 1;
        }

        #region 清分确认/取消

        /// <summary>
        /// 清分确认
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int QFCheck()
        {
            string errMsg = string.Empty;
            Patient3261 patient3261 = new Patient3261();
            Models.Request.RequestGzsiModel3261 requestGzsiModel3261 = new API.GZSI.Models.Request.RequestGzsiModel3261();
            Models.Response.ResponseGzsiModel3261 responseGzsiModel3261 = new Models.Response.ResponseGzsiModel3261();

            Models.Request.RequestGzsiModel3261.Input input = new API.GZSI.Models.Request.RequestGzsiModel3261.Input();

            Models.Request.RequestGzsiModel3261.Data data = new API.GZSI.Models.Request.RequestGzsiModel3261.Data();
            data.trt_year = this.dtpMonth.Value.Year.ToString();
            data.trt_month = this.dtpMonth.Value.Month.ToString();
            data.totalrow = "0";
            input.data = data;

            List<Models.Request.RequestGzsiModel3261.Detail> detail = new List<API.GZSI.Models.Request.RequestGzsiModel3261.Detail>();

            input.data = data;
            input.detail = detail;
            requestGzsiModel3261.input = input;

            returnvalue = patient3261.CallService(requestGzsiModel3261, ref responseGzsiModel3261, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                errMsg = patient3261.ErrorMsg;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 清分确认取消
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int QFCancel()
        {
            string errMsg = string.Empty;
            Patient3262 patient3262 = new Patient3262();
            Models.Request.RequestGzsiModel3262 requestGzsiModel3262 = new API.GZSI.Models.Request.RequestGzsiModel3262();
            Models.Response.ResponseGzsiModel3262 responseGzsiModel3262 = new Models.Response.ResponseGzsiModel3262();

            Models.Request.RequestGzsiModel3262.Input input = new API.GZSI.Models.Request.RequestGzsiModel3262.Input();
            input.trt_year = this.dtpMonth.Value.Year.ToString();
            input.trt_month = this.dtpMonth.Value.Month.ToString();
            input.otransid = "0";

            requestGzsiModel3262.input = input;

            returnvalue = patient3262.CallService(requestGzsiModel3262, ref responseGzsiModel3262, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                errMsg = patient3262.ErrorMsg;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 列枚举

        /// <summary>
        /// 已上传列表
        /// </summary>
        private enum EnumListCol
        {
            /// <summary>
            /// 类别
            /// </summary>
            PatientType = 0,

            /// <summary>
            /// 病历号
            /// </summary>
            CardNO = 1,

            /// <summary>
            /// 姓名
            /// </summary>
            Name = 2,

            /// <summary>
            /// 身份证号
            /// </summary>
            IDCard = 3,

            /// <summary>
            /// 性别
            /// </summary>
            Sex = 4,

            /// <summary>
            /// 结算类型
            /// </summary>
            PayKind = 5,

            /// <summary>
            /// 发票号
            /// </summary>
            InvoiceNO = 6,

            /// <summary>
            /// 总金额
            /// </summary>
            ZJE = 7,

            /// <summary>
            /// 统筹金额
            /// </summary>
            TCJE = 8,

            /// <summary>
            /// 自费金额
            /// </summary>
            ZFJE = 9,

            /// <summary>
            /// 结算员
            /// </summary>
            JSY = 10,

            /// <summary>
            /// 出院时间
            /// </summary>
            CYSJ = 11,

            /// <summary>
            /// 结算时间
            /// </summary>
            JSSJ = 12
        }

        #endregion 
    }
}
