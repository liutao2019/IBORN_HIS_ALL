using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using API.GZSI;
using API.GZSI.Business;

namespace API.GZSI.UI
{
    public partial class ucOwnPayUpload : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOwnPayUpload()
        {
            InitializeComponent();
        }

        #region 域变量
        /// <summary>
        /// 综合管理层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 工具栏按钮
        /// </summary>
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        FS.HISFC.Models.Base.PageSize pageSize;

        FS.HISFC.BizLogic.Fee.InPatient inPatientFee = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 日志业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient radtMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        LocalManager Lmr = new LocalManager();


        #endregion

        #region 属性

        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            #region 患者类型
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            al.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "门诊";
            al.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "2";
            obj.Name = "住院";
            al.Add(obj);
            this.cmbPatientType.AddItems(al);
            this.cmbPatientType.Tag = "2";
            #endregion

            #region 发票状态
            ArrayList alFPState = new ArrayList();
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            alFPState.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "有效";
            alFPState.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "0";
            obj.Name = "无效";
            alFPState.Add(obj);
            this.ncbFPState.AddItems(alFPState);
            this.ncbFPState.Tag = "1";
            #endregion

            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }
        #endregion

        #region 菜单
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("自费患者结算清单上传", "自费患者结算清单上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.P批量复制, true, false, null);
            this.toolBarService.AddToolButton("取消上传", "取消上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "自费患者结算清单上传":
                    {
                        OwnPayUpload();
                        break;
                    }
                case "取消上传":
                    {
                        OwnPayDelete();
                        break;
                    }
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            Cursor = Cursors.WaitCursor;
            QueryOwnPay();
            Cursor = Cursors.Default;
            return 1;
        }

        /// <summary>
        /// 查询自费患者上传列表
        /// </summary>
        /// <returns></returns>
        private int QueryOwnPay()
        {
            this.fpOwnPay.RowCount = 0;

            //上传状态
            string isUpload = "0";
            if (this.rbJSDYES.Checked)
            {
                isUpload = "1";
            }
            else if (this.rbJSDCancel.Checked)
            {
                isUpload = "-1";
            }

            //发票状态
            string isVaild = this.ncbFPState.Tag.ToString();
            string beginDate = this.dtJSDBegTime.Value.Date.ToString();
            string endDate = this.dtJSDEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString();
            string patientType = this.cmbPatientType.Tag.ToString();
            string patientNo = this.lblPatientNo.Text;

            List<FS.HISFC.Models.RADT.PatientInfo> patientObjList
                = this.Lmr.GetOwnPayPatientByUpload(beginDate, endDate, isUpload, patientType, patientNo, isVaild);

            this.SetOwnPayData(patientObjList);

            return 1;
        }

        /// <summary>
        /// 显示自费患者列表
        /// </summary>
        /// <returns></returns>
        private int SetOwnPayData(List<FS.HISFC.Models.RADT.PatientInfo> patientObjList)
        {
            if (patientObjList == null)
            {
                MessageBox.Show("查询信息为空！");
                return -1;
            }
            this.fpOwnPay.RowCount = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in patientObjList)
            {
                this.fpOwnPay.Rows.Add(0, 1);
                FarPoint.Win.Spread.CellType.CheckBoxCellType t = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                FarPoint.Win.Spread.CellType.TextCellType a = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread3.ActiveSheet.Cells[0, 0].CellType = t;
                this.neuSpread3.ActiveSheet.Cells[0, 6].CellType = a;
                this.fpOwnPay.Rows[0].Tag = patientInfo;
                this.fpOwnPay.Cells[0, 0].Value = true;
                this.fpOwnPay.Cells[0, 1].Text = patientInfo.SIMainInfo.TypeCode == "2" ? "住院" : "门诊";//患者类型
                this.fpOwnPay.Cells[0, 2].Text = patientInfo.ID;//就诊流水号
                this.fpOwnPay.Cells[0, 3].Text = patientInfo.SIMainInfo.InvoiceNo;//发票号
                this.fpOwnPay.Cells[0, 4].Text = patientInfo.PID.PatientNO;//住院号
                this.fpOwnPay.Cells[0, 5].Text = patientInfo.PID.CardNO;//门诊号
                this.fpOwnPay.Cells[0, 6].Text = patientInfo.Name;//姓名
                this.fpOwnPay.Cells[0, 7].Text = patientInfo.Sex.ID.ToString() == "F" ? "女" : "男";//性别
                this.fpOwnPay.Cells[0, 8].Text = patientInfo.IDCard;//证件号
                this.fpOwnPay.Cells[0, 9].Text = patientInfo.Birthday.ToString("yyyy-MM-dd");//出生日期
                this.fpOwnPay.Cells[0, 10].Text = patientInfo.PVisit.InTime.ToString();//入院日期
                this.fpOwnPay.Cells[0, 11].Text = patientInfo.PVisit.PatientLocation.Dept.Name;//科室
                this.fpOwnPay.Cells[0, 12].Text = patientInfo.PVisit.OutTime.ToString();//出院日期
                this.fpOwnPay.Cells[0, 13].Text = patientInfo.Pact.Name;//合同单位
                this.fpOwnPay.Cells[0, 14].Text = patientInfo.SIMainInfo.User01;//错误消息
                this.fpOwnPay.Cells[0, 15].Text = patientInfo.SIMainInfo.BalanceState == "1" ? "有效" : "无效";//发票状态
                #region 上传状态
                string stateMsg = "";
                if (patientInfo.SIMainInfo.BalanceState == "1")
                {
                    if (patientInfo.SIMainInfo.User02 == "-1")
                    {
                        stateMsg = "待上传";
                    }
                    else if (patientInfo.SIMainInfo.User02 == "1")
                    {
                        stateMsg = "已上传";
                    }
                    else
                    {
                        stateMsg = "待上传";
                    }
                }
                else
                {

                    if (patientInfo.SIMainInfo.User02 == "-1")
                    {
                        stateMsg = "已取消";
                    }
                    else if (patientInfo.SIMainInfo.User02 == "1")
                    {
                        stateMsg = "待取消";
                    }
                    else
                    {
                        stateMsg = "无需上传";
                    }
                }
                #endregion
                this.fpOwnPay.Cells[0, 16].Text = stateMsg;
            }

            return 1;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.fpOwnPay.Rows.Count > 0)
            {
                for (int k = 0; k < this.fpOwnPay.Rows.Count; k++)
                {
                    if (this.cbChooseAll.Checked)
                    {
                        this.fpOwnPay.Cells[k, 0].Value = true;
                    }
                    else
                    {
                        this.fpOwnPay.Cells[k, 0].Value = false;
                    }
                }
            }
        }
        #endregion

        #region 自费结算清单上传
        /// <summary>
        /// 自费患者上传
        /// </summary>
        /// <returns></returns>
        private int OwnPayUpload()
        {

            if (this.fpOwnPay.RowCount < 1)
            {
                MessageBox.Show("请选择数据后上传");
                return 0;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传!请稍后!");
            Application.DoEvents();
            for (int k = 0; k < this.fpOwnPay.Rows.Count; k++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.fpOwnPay.Rows.Count);

                bool isChoose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpOwnPay.Cells[k, 0].Value);
                if (isChoose)
                {
                    //FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    //patientInfo.SIMainInfo = new FS.HISFC.Models.SIInterface.SIMainInfo();
                    //patientInfo.SIMainInfo.TypeCode = this.fpOwnPay.Cells[k, 1].Text == "住院" ? "2" : "1";//住院门诊
                    //patientInfo.ID = this.fpOwnPay.Cells[k, 2].Text;//就诊流水号
                    //patientInfo.SIMainInfo.InvoiceNo = this.fpOwnPay.Cells[k, 3].Text;//发票号

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.fpOwnPay.Rows[k].Tag as FS.HISFC.Models.RADT.PatientInfo;
                    if (patientInfo.SIMainInfo.User02 == "1")
                    {
                        //MessageBox.Show("结算清单已上传！");
                        continue;
                    }

                    if (patientInfo.SIMainInfo.BalanceState != "1")
                    {
                        continue;
                    }

                    if (patientInfo.SIMainInfo.TypeCode == "2")
                    {
                        this.InOwnPayUpload(patientInfo);
                    }
                    else
                    {
                        this.OutOwnPayUpload(patientInfo);
                    }

                }
            }
            this.QueryOwnPay();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// 住院
        /// </summary>
        /// <returns></returns>
        private int InOwnPayUpload(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region 【4204】自费病人住院费用明细删除
            Upload4204 upload4204 = new Upload4204();
            API.GZSI.Models.Request.RequestGzsiModel4204 RequestGzsiModel4204 = new Models.Request.RequestGzsiModel4204();
            API.GZSI.Models.Response.ResponseGzsiModel4204 ResponseGzsiModel4204 = new Models.Response.ResponseGzsiModel4204();

            RequestGzsiModel4204.feedetail = new Models.Request.RequestGzsiModel4204.Feedetail();
            RequestGzsiModel4204.feedetI = new Models.Request.RequestGzsiModel4204.FeedetI();
            RequestGzsiModel4204.feedetail.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
            RequestGzsiModel4204.feedetail.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
            RequestGzsiModel4204.feedetI.bkkp_sn = "";

            if (upload4204.CallService(RequestGzsiModel4204, ref ResponseGzsiModel4204) < 0)
            {
                //Lmr.UpdateSiMainInfoForOwnPayERR(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "【4201A】" + upload4201A.ErrorMsg);
                //return -1;
            }
            #endregion

            #region 【4201A】自费病人住院费用明细信息上传
            Upload4201A upload4201A = new Upload4201A();
            API.GZSI.Models.Request.RequestGzsiModel4201A RequestGzsiModel4201A = new Models.Request.RequestGzsiModel4201A();
            API.GZSI.Models.Response.ResponseGzsiModel4201A ResponseGzsiModel4201A = new Models.Response.ResponseGzsiModel4201A();

            #region 数据获取
            //明细信息(节点标识：fsiOwnpayPatnFeeListDDTO)
            List<API.GZSI.Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO> fsiOwnpayPatnFeeListDDTO = Lmr.Get4201AFsiOwnpayPatnFeeListDDTO(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo);

            #region 调整价格
            RequestGzsiModel4201A.fsiOwnpayPatnFeeListDDTO = new List<Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO>();
            foreach (API.GZSI.Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO data in fsiOwnpayPatnFeeListDDTO)
            {
                API.GZSI.Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO temp = new Models.Request.RequestGzsiModel4201A.FsiOwnpayPatnFeeListDDTO();
                temp = data;
                string itemCode = data.medins_list_codg;//医药机构目录编码
                decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(data.cnt);//数量
                decimal price = 0.0m;// FS.FrameWork.Function.NConvert.ToDecimal(data.pric);//单价

                if (!this.GetInItem(itemCode, qty, ref price))
                {
                    continue;
                }

                temp.cnt = qty.ToString();
                temp.pric = price.ToString();
                temp.det_item_fee_sumamt = (qty * price).ToString();
                temp.fulamt_ownpay_amt = (qty * price).ToString();
                RequestGzsiModel4201A.fsiOwnpayPatnFeeListDDTO.Add(temp);
            }
            #endregion

            //RequestGzsiModel4201A.fsiOwnpayPatnFeeListDDTO = fsiOwnpayPatnFeeListDDTO;
            if (RequestGzsiModel4201A.fsiOwnpayPatnFeeListDDTO == null || RequestGzsiModel4201A.fsiOwnpayPatnFeeListDDTO.Count <= 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4201A】" + "费用信息为空！");
                return -1;
            }
            #endregion

            #region 接口上传
            if (upload4201A.CallService(RequestGzsiModel4201A, ref ResponseGzsiModel4201A) < 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4201A】" + upload4201A.ErrorMsg);
                return -1;
            }
            #endregion

            #region 更新上传状态
            if (ResponseGzsiModel4201A.infcode != "0")
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4201A】" + ResponseGzsiModel4201A.err_msg);
                return -1;
            }
            #endregion
            #endregion

            #region 【4202】自费病人住院就诊和诊断信息上传
            Upload4202 upload4202 = new Upload4202();
            API.GZSI.Models.Request.RequestGzsiModel4202 RequestGzsiModel4202 = new Models.Request.RequestGzsiModel4202();
            API.GZSI.Models.Response.ResponseGzsiModel4202 ResponseGzsiModel4202 = new Models.Response.ResponseGzsiModel4202();

            #region 数据获取
            //自费病人就诊信息(节点标识：ownPayPatnMdtrtD)
            API.GZSI.Models.Request.RequestGzsiModel4202.OwnPayPatnMdtrtD ownPayPatnMdtrtD = Lmr.Get4202OwnPayPatnMdtrtD(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo);
            RequestGzsiModel4202.ownPayPatnMdtrtD = ownPayPatnMdtrtD;

            if (RequestGzsiModel4202.ownPayPatnMdtrtD == null)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4202】" + "就诊信息为空！");
                return -1;
            }

            //自费病人诊断信息(节点标识：ownPayPatnDiagListD)
            List<API.GZSI.Models.Request.RequestGzsiModel4202.OwnPayPatnDiagListD> ownPayPatnDiagListD = Lmr.Get4202OwnPayPatnDiagListD(patientInfo.ID);
            RequestGzsiModel4202.ownPayPatnDiagListD = ownPayPatnDiagListD;

            if (RequestGzsiModel4202.ownPayPatnDiagListD == null || RequestGzsiModel4202.ownPayPatnDiagListD.Count <= 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4202】" + "诊断信息为空！");
                return -1;
            }
            #endregion

            #region 接口上传
            if (upload4202.CallService(RequestGzsiModel4202, ref ResponseGzsiModel4202) < 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4202】" + upload4202.ErrorMsg);
                return -1;
            }
            #endregion

            #region 更新上传状态
            if (ResponseGzsiModel4202.infcode != "0")
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4202】" + ResponseGzsiModel4202.err_msg);
                return -1;
            }
            #endregion
            #endregion

            #region 【4203】自费病人就诊以及费用明细上传完成
            Upload4203 upload4203 = new Upload4203();
            API.GZSI.Models.Request.RequestGzsiModel4203 RequestGzsiModel4203 = new Models.Request.RequestGzsiModel4203();
            API.GZSI.Models.Response.ResponseGzsiModel4203 ResponseGzsiModel4203 = new Models.Response.ResponseGzsiModel4203();

            RequestGzsiModel4203.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
            RequestGzsiModel4203.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
            RequestGzsiModel4203.cplt_flag = "1";

            if (upload4203.CallService(RequestGzsiModel4203, ref ResponseGzsiModel4203) < 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + upload4201A.ErrorMsg);
                return -1;
            }

            if (ResponseGzsiModel4203.infcode != "0")
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + ResponseGzsiModel4203.err_msg);
                return -1;
            }
            #endregion

            if (this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "1", "") < 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 门诊
        /// </summary>
        /// <returns></returns>
        private int OutOwnPayUpload(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region 【4205】自费病人门诊就医信息上传
            Upload4205 upload4205 = new Upload4205();
            API.GZSI.Models.Request.RequestGzsiModel4205 RequestGzsiModel4205 = new Models.Request.RequestGzsiModel4205();
            API.GZSI.Models.Response.ResponseGzsiModel4205 ResponseGzsiModel4205 = new Models.Response.ResponseGzsiModel4205();

            #region 数据获取
            //自费病人门诊就诊信息(节点标识：mdtrtinfo)
            API.GZSI.Models.Request.RequestGzsiModel4205.Mdtrtinfo mdtrtinfo = Lmr.Get4205Mdtrtinfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo);
            RequestGzsiModel4205.mdtrtinfo = mdtrtinfo;
            if (RequestGzsiModel4205.mdtrtinfo == null)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4205】" + "就诊信息为空！");
                return -1;
            }

            //自费病人门诊诊断信息(节点标识：diseinfo)
            List<API.GZSI.Models.Request.RequestGzsiModel4205.Diseinfo> diseinfo = Lmr.Get4205Diseinfo(patientInfo.ID);
            RequestGzsiModel4205.diseinfo = diseinfo;
            if (RequestGzsiModel4205.diseinfo == null || RequestGzsiModel4205.diseinfo.Count <= 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4205】" + "诊断信息为空！");
                return -1;
            }

            //自费病人门诊费用明细信息(节点标识：feedetail)
            List<API.GZSI.Models.Request.RequestGzsiModel4205.Feedetail> feedetail = Lmr.Get4205Feedetail(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo);
            RequestGzsiModel4205.feedetail = feedetail;
            if (RequestGzsiModel4205.feedetail == null || RequestGzsiModel4205.feedetail.Count <= 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4205】" + "费用信息为空！");
                return -1;
            }
            #endregion

            #region 接口上传
            if (upload4205.CallService(RequestGzsiModel4205, ref ResponseGzsiModel4205) < 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4205】" + upload4205.ErrorMsg);
                return -1;
            }

            if (ResponseGzsiModel4205.infcode != "0")
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4205】" + ResponseGzsiModel4205.err_msg);
                return -1;
            }
            #endregion
            #endregion

            #region 【4203】自费病人就诊以及费用明细上传完成
            Upload4203 upload4203 = new Upload4203();
            API.GZSI.Models.Request.RequestGzsiModel4203 RequestGzsiModel4203 = new Models.Request.RequestGzsiModel4203();
            API.GZSI.Models.Response.ResponseGzsiModel4203 ResponseGzsiModel4203 = new Models.Response.ResponseGzsiModel4203();

            RequestGzsiModel4203.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
            RequestGzsiModel4203.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
            RequestGzsiModel4203.cplt_flag = "1";

            if (upload4203.CallService(RequestGzsiModel4203, ref ResponseGzsiModel4203) < 0)
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + upload4203.ErrorMsg);
                return -1;
            }

            if (ResponseGzsiModel4203.infcode != "0")
            {
                this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + ResponseGzsiModel4203.err_msg);
                return -1;
            }
            #endregion

            if (this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "1", "") < 0)
            {
                return -1;
            }

            return 1;
        }

        #region 项目处理

        FS.HISFC.BizLogic.Fee.Item itemFee = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.BizLogic.Pharmacy.Item itemPharmacy = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 获取住院项目信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private bool GetInItem(string itemCode, decimal qty, ref decimal price)
        {
            //获取信息基本信息
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

            //非药品
            FS.HISFC.Models.Fee.Item.Undrug undrug = itemFee.GetItemByUndrugCode(itemCode);
            if (undrug != null && !string.IsNullOrEmpty(undrug.ID))
            {
                feeItem.Item = undrug;
            }

            //药品
            FS.HISFC.Models.Pharmacy.Item drug = itemPharmacy.GetItem(itemCode);
            if (drug != null && !string.IsNullOrEmpty(drug.ID))
            {
                feeItem.Item = drug;
            }

            feeItem.Item.Qty = qty;

            price = 0.0m;
            this.GetPrice(feeItem, ref price);
            //this.GetCount(feeItem, ref qty);

            //价格为0的非药品项目不上传
            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug && price == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取门诊项目信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private bool GetOutItem(string itemCode, decimal qty, ref decimal price)
        {
            //获取信息基本信息
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

            //非药品
            FS.HISFC.Models.Fee.Item.Undrug undrug = itemFee.GetItemByUndrugCode(itemCode);
            if (undrug != null && !string.IsNullOrEmpty(undrug.ID))
            {
                feeItem.Item = undrug;
            }

            //药品
            FS.HISFC.Models.Pharmacy.Item drug = itemPharmacy.GetItem(itemCode);
            if (drug != null && !string.IsNullOrEmpty(drug.ID))
            {
                feeItem.Item = drug;
            }

            price = 0.0m;
            this.GetPrice(feeItem, ref price);
            //this.GetCount(feeItem, ref qty);

            //价格为0的非药品项目不上传
            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug && price == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取项目价格
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetPrice(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal price)
        {
            //若没有维护医保价，则直接取普通价
            if (f.Item.SpecialPrice == 0 && f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                f.Item.SpecialPrice = f.Item.Price;
            }

            //处理包装单位，部分项目没有包装单位
            if (f.Item.PackQty == 0)
            {
                f.Item.PackQty = 1;
            }

            if (f is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
            {
                //参考门诊的价格计算规则
                //门诊价格计算规则代码路径：
                //FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.ITruncFee.ITruncFeeImplement
                f.SIft.TotCost = FS.FrameWork.Public.String.TruncateNumber(f.Item.SpecialPrice * f.Item.Qty / f.Item.PackQty, 2);
                f.SIft.RebateCost = FS.FrameWork.Public.String.TruncateNumber(f.FT.RebateCost * f.Item.Qty / f.Item.PackQty, 2);
                f.SIft.OwnCost = f.SIft.TotCost;
            }
            else if (f is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
            {
                //参考门诊的价格计算规则
                //住院价格计算规则代码路径：
                //FS.HISFC.BizProcess.Integrate.Fee   ConvertOrderToFeeItemList函数
                f.SIft.TotCost = FS.FrameWork.Public.String.FormatNumber((f.Item.SpecialPrice / f.Item.PackQty), 2) * f.Item.Qty;
                f.SIft.TotCost = FS.FrameWork.Public.String.FormatNumber(f.SIft.TotCost, 2);
                f.SIft.RebateCost = FS.FrameWork.Public.String.FormatNumber((f.SIft.RebateCost / f.Item.PackQty), 2) * f.Item.Qty;
                f.SIft.RebateCost = FS.FrameWork.Public.String.FormatNumber(f.SIft.RebateCost, 2);
                f.SIft.OwnCost = f.SIft.TotCost;
            }
            else
            {
                return false;
            }

            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && f.Item.Qty > 300)//中草药
            {
                price = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost * 10 / f.Item.Qty), 4);
            }
            else
            {
                price = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost / f.Item.Qty), 4);
            }

            return true;
        }

        /// <summary>
        /// 获取项目数量
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetCount(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal qty)
        {
            //数量大于300的，基本上就是中药了
            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && f.Item.Qty > 300)
            {
                qty = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.Item.Qty / 10), 4);
            }
            else
            {
                qty = f.Item.Qty;
            }

            return true;
        }
        #endregion

        #endregion

        #region 取消上传
        /// <summary>
        /// 自费患者上传
        /// </summary>
        /// <returns></returns>
        private int OwnPayDelete()
        {

            if (this.fpOwnPay.RowCount < 1)
            {
                MessageBox.Show("请选择数据后取消上传");
                return 0;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在取消!请稍后!");
            Application.DoEvents();
            for (int k = 0; k < this.fpOwnPay.Rows.Count; k++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.fpOwnPay.Rows.Count);

                bool isChoose = FS.FrameWork.Function.NConvert.ToBoolean(this.fpOwnPay.Cells[k, 0].Value);
                if (isChoose)
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.fpOwnPay.Rows[k].Tag as FS.HISFC.Models.RADT.PatientInfo;
                    if (patientInfo.SIMainInfo.User02 != "1")
                    {
                        //MessageBox.Show("结算清单未上传！");
                        continue;
                    }

                    if (patientInfo.SIMainInfo.TypeCode == "2")
                    {
                        if (this.InOwnPayDelete(patientInfo) < 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (this.OutOwnPayDelete(patientInfo) < 0)
                        {
                            break;
                        }
                    }

                }
            }
            this.QueryOwnPay();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }


        /// <summary>
        /// 住院
        /// </summary>
        /// <returns></returns>
        private int InOwnPayDelete(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region 【4203】自费病人就诊以及费用明细上传完成
            Upload4203 upload4203 = new Upload4203();
            API.GZSI.Models.Request.RequestGzsiModel4203 RequestGzsiModel4203 = new Models.Request.RequestGzsiModel4203();
            API.GZSI.Models.Response.ResponseGzsiModel4203 ResponseGzsiModel4203 = new Models.Response.ResponseGzsiModel4203();

            RequestGzsiModel4203.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
            RequestGzsiModel4203.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
            RequestGzsiModel4203.cplt_flag = "0";

            if (upload4203.CallService(RequestGzsiModel4203, ref ResponseGzsiModel4203) < 0)
            {
                //this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + upload4203.ErrorMsg);
                MessageBox.Show("[" + patientInfo.SIMainInfo.InvoiceNo + "]取消上传失败:" + upload4203.ErrorMsg);
                return -1;
            }

            if (ResponseGzsiModel4203.infcode != "0")
            {
                //this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + ResponseGzsiModel4203.err_msg);
                MessageBox.Show("[" + patientInfo.SIMainInfo.InvoiceNo + "]取消上传失败:" + ResponseGzsiModel4203.err_msg);
                return -1;
            }
            #endregion

            #region 【4204】自费病人住院费用明细删除
            Upload4204 upload4204 = new Upload4204();
            API.GZSI.Models.Request.RequestGzsiModel4204 RequestGzsiModel4204 = new Models.Request.RequestGzsiModel4204();
            API.GZSI.Models.Response.ResponseGzsiModel4204 ResponseGzsiModel4204 = new Models.Response.ResponseGzsiModel4204();

            RequestGzsiModel4204.feedetail = new Models.Request.RequestGzsiModel4204.Feedetail();
            RequestGzsiModel4204.feedetI = new Models.Request.RequestGzsiModel4204.FeedetI();
            RequestGzsiModel4204.feedetail.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
            RequestGzsiModel4204.feedetail.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
            RequestGzsiModel4204.feedetI.bkkp_sn = "";

            if (upload4204.CallService(RequestGzsiModel4204, ref ResponseGzsiModel4204) < 0)
            {
                //Lmr.UpdateSiMainInfoForOwnPayERR(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "【4201A】" + upload4201A.ErrorMsg);
                return -1;
            }
            #endregion

            if (this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "-1", "取消上传") < 0)
            {
                MessageBox.Show(Lmr.Err);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 门诊
        /// </summary>
        /// <returns></returns>
        private int OutOwnPayDelete(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region 【4203】自费病人就诊以及费用明细上传完成
            Upload4203 upload4203 = new Upload4203();
            API.GZSI.Models.Request.RequestGzsiModel4203 RequestGzsiModel4203 = new Models.Request.RequestGzsiModel4203();
            API.GZSI.Models.Response.ResponseGzsiModel4203 ResponseGzsiModel4203 = new Models.Response.ResponseGzsiModel4203();

            RequestGzsiModel4203.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
            RequestGzsiModel4203.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
            RequestGzsiModel4203.cplt_flag = "0";

            if (upload4203.CallService(RequestGzsiModel4203, ref ResponseGzsiModel4203) < 0)
            {
                //this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + upload4203.ErrorMsg);
                MessageBox.Show("[" + patientInfo.SIMainInfo.InvoiceNo + "]取消上传失败:" + upload4203.ErrorMsg);
                return -1;
            }

            if (ResponseGzsiModel4203.infcode != "0")
            {
                //this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "0", "【4203】" + ResponseGzsiModel4203.err_msg);
                MessageBox.Show("[" + patientInfo.SIMainInfo.InvoiceNo + "]取消上传失败:" + ResponseGzsiModel4203.err_msg);
                return -1;
            }
            #endregion

            #region 【4206】自费病人住院费用明细删除
            Upload4206 upload4206 = new Upload4206();
            API.GZSI.Models.Request.RequestGzsiModel4206 RequestGzsiModel4206 = new Models.Request.RequestGzsiModel4206();
            API.GZSI.Models.Response.ResponseGzsiModel4206 ResponseGzsiModel4206 = new Models.Response.ResponseGzsiModel4206();

            RequestGzsiModel4206.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
            RequestGzsiModel4206.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;

            if (upload4206.CallService(RequestGzsiModel4206, ref ResponseGzsiModel4206) < 0)
            {
                //Lmr.UpdateSiMainInfoForOwnPayERR(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "【4201A】" + upload4201A.ErrorMsg);
                MessageBox.Show("[" + patientInfo.SIMainInfo.InvoiceNo + "]取消上传失败:" + upload4206.ErrorMsg);
                return -1;
            }

            if (ResponseGzsiModel4206.infcode != "0")
            {
                MessageBox.Show("[" + patientInfo.SIMainInfo.InvoiceNo + "]取消上传失败:" + ResponseGzsiModel4206.err_msg);
                return -1;
            }

            #endregion

            if (this.SaveUploadInfo(patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo, "-1", "取消上传") < 0)
            {
                MessageBox.Show(Lmr.Err);
                return -1;
            }

            return 1;
        }
        #endregion

        #region 日志
        /// <summary>
        /// 保存日志，-1取消上传、0上传失败、1上传成功
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="state"></param>
        /// <param name="errinfo"></param>
        /// <returns></returns>
        private int SaveUploadInfo(string inpatientNo, string invoiceNo, string state, string errinfo)
        {
            int res = Lmr.InsertOwnPayLog(inpatientNo, invoiceNo, state, errinfo);
            if (res < 0)
            {
                res = Lmr.UpdateOwnPayLog(inpatientNo, invoiceNo, state, errinfo);
            }
            return res;
        }
        #endregion

        #region 接口查询
        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread3_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //获取行号
            int rownum = e.Row;
            if (rownum < 0)
            {
                return;
            }

            //获取流水号
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.fpOwnPay.Rows[rownum].Tag as FS.HISFC.Models.RADT.PatientInfo;

            //过滤未上传
            if (patientInfo.SIMainInfo.User02 == "0")
            {
                return;
            }

            this.Query4207(patientInfo);
            this.Query4208(patientInfo);
            this.Query4209(patientInfo);
            this.tabControl1.SelectedIndex = 1;

        }

        /// <summary>
        /// 【4207】自费病人就医费用明细查询
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private int Query4207(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                return -1;
            }

            try
            {
                #region 【4207】自费病人就医费用明细查询
                Upload4207 upload4207 = new Upload4207();
                API.GZSI.Models.Request.RequestGzsiModel4207 RequestGzsiModel4207 = new API.GZSI.Models.Request.RequestGzsiModel4207();
                API.GZSI.Models.Response.ResponseGzsiModel4207 ResponseGzsiModel4207 = new API.GZSI.Models.Response.ResponseGzsiModel4207();

                RequestGzsiModel4207.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
                RequestGzsiModel4207.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
                RequestGzsiModel4207.page_num = "1";
                RequestGzsiModel4207.page_size = "300";

                if (upload4207.CallService(RequestGzsiModel4207, ref ResponseGzsiModel4207) < 0)
                {
                    MessageBox.Show("【4207】自费病人就医费用明细查询失败:" + upload4207.ErrorMsg);
                    return -1;
                }

                if (ResponseGzsiModel4207.infcode != "0")
                {
                    MessageBox.Show("【4207】自费病人就医费用明细查询失败:" + ResponseGzsiModel4207.err_msg);
                    return -1;
                }

                if (ResponseGzsiModel4207.output == null)
                {
                    MessageBox.Show("【4207】自费病人就医费用明细查询失败:" + "查询结果为空！");
                    return -1;
                }

                if (ResponseGzsiModel4207.output.data == null || ResponseGzsiModel4207.output.data.Count <= 0)
                {
                    MessageBox.Show("【4207】自费病人就医费用明细查询失败:" + "查询结果为空！");
                    return -1;
                }

                #endregion

                Common.Function.ShowOutDateToFarpoint<API.GZSI.Models.Response.ResponseGzsiModel4207.Data>(this.fp4207, ResponseGzsiModel4207.output.data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 【4208】自费病人就医就诊信息查询
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private int Query4208(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                return -1;
            }

            try
            {
                #region 【4208】自费病人就医就诊信息查询
                Upload4208 upload4208 = new Upload4208();
                API.GZSI.Models.Request.RequestGzsiModel4208 RequestGzsiModel4208 = new Models.Request.RequestGzsiModel4208();
                API.GZSI.Models.Response.ResponseGzsiModel4208 ResponseGzsiModel4208 = new Models.Response.ResponseGzsiModel4208();

                string beginDate = this.dtJSDBegTime.Value.Date.ToString();
                string endDate = this.dtJSDEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString();

                RequestGzsiModel4208.psn_cert_type = "01";
                RequestGzsiModel4208.certno = patientInfo.IDCard;
                //RequestGzsiModel4208.begntime = this.dtJSDBegTime.Value.Date.ToString("yyyy-MM-dd");
                //RequestGzsiModel4208.endtime = this.dtJSDEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd");
                RequestGzsiModel4208.page_num = "1";
                RequestGzsiModel4208.page_size = "300";

                if (upload4208.CallService(RequestGzsiModel4208, ref ResponseGzsiModel4208) < 0)
                {
                    MessageBox.Show("【4208】自费病人就医就诊信息查询失败:" + upload4208.ErrorMsg);
                    return -1;
                }

                if (ResponseGzsiModel4208.infcode != "0")
                {
                    MessageBox.Show("【4208】自费病人就医就诊信息查询失败:" + ResponseGzsiModel4208.err_msg);
                    return -1;
                }

                if (ResponseGzsiModel4208.output == null)
                {
                    MessageBox.Show("【4208】自费病人就医就诊信息查询失败:" + "查询结果为空！");
                    return -1;
                }

                if (ResponseGzsiModel4208.output.data == null || ResponseGzsiModel4208.output.data.Count <= 0)
                {
                    MessageBox.Show("【4208】自费病人就医就诊信息查询失败:" + "查询结果为空！");
                    return -1;
                }

                #region 筛选数据
                string fixmedinsMdtrtId = patientInfo.SIMainInfo.InvoiceNo;
                List<API.GZSI.Models.Response.ResponseGzsiModel4208.Data> dataList = new List<Models.Response.ResponseGzsiModel4208.Data>();
                foreach (API.GZSI.Models.Response.ResponseGzsiModel4208.Data data in ResponseGzsiModel4208.output.data)
                {
                    if (data.fixmedinsMdtrtId == fixmedinsMdtrtId && data.fixmedinsCode == Models.UserInfo.Instance.fixmedins_code)
                    {
                        dataList.Add(data);
                    }
                }
                #endregion
                #endregion

                Common.Function.ShowOutDateToFarpoint<API.GZSI.Models.Response.ResponseGzsiModel4208.Data>(this.fp4208, dataList);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 【4209】自费病人就医诊断信息查询
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private int Query4209(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                return -1;
            }

            try
            {
                #region 【4209】自费病人就医诊断信息查询
                Upload4209 upload4209 = new Upload4209();
                API.GZSI.Models.Request.RequestGzsiModel4209 RequestGzsiModel4209 = new API.GZSI.Models.Request.RequestGzsiModel4209();
                API.GZSI.Models.Response.ResponseGzsiModel4209 ResponseGzsiModel4209 = new API.GZSI.Models.Response.ResponseGzsiModel4209();

                RequestGzsiModel4209.fixmedins_mdtrt_id = patientInfo.SIMainInfo.InvoiceNo;
                RequestGzsiModel4209.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
                RequestGzsiModel4209.page_num = "1";
                RequestGzsiModel4209.page_size = "300";

                if (upload4209.CallService(RequestGzsiModel4209, ref ResponseGzsiModel4209) < 0)
                {
                    MessageBox.Show("【4209】自费病人就医诊断信息查询失败:" + upload4209.ErrorMsg);
                    return -1;
                }

                if (ResponseGzsiModel4209.infcode != "0")
                {
                    MessageBox.Show("【4209】自费病人就医诊断信息查询失败:" + ResponseGzsiModel4209.err_msg);
                    return -1;
                }

                if (ResponseGzsiModel4209.output == null)
                {
                    MessageBox.Show("【4209】自费病人就医诊断信息查询失败:" + "查询结果为空！");
                    return -1;
                }

                if (ResponseGzsiModel4209.output.data == null || ResponseGzsiModel4209.output.data.Count <= 0)
                {
                    MessageBox.Show("【4209】自费病人就医诊断信息查询失败:" + "查询结果为空！");
                    return -1;
                }

                #endregion

                Common.Function.ShowOutDateToFarpoint<API.GZSI.Models.Response.ResponseGzsiModel4209.Data>(this.fp4209, ResponseGzsiModel4209.output.data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }
        #endregion
    }
}
