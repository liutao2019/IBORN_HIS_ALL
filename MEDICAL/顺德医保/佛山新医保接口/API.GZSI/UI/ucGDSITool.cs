using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using API.GZSI.Business;
using API.GZSI.Models.Response;

namespace API.GZSI.UI
{
    public partial class ucGDSITool : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 变量
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public string errMsg = string.Empty;

        LocalManager LocalManager = new LocalManager();

        //对数明细信息
        private List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo> setlinfo
                = new List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>();

        //结算基金分项信息
        private List<Models.Response.ResponseGzsiModel90502.Output.Setldetail> setldetail
            = new List<Models.Response.ResponseGzsiModel90502.Output.Setldetail>();
        #endregion

        public ucGDSITool()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //险种类型
            this.cmbInsutype.AddItems(constMgr.GetAllList("GZSI_insutype_dz"));
            //设置默认值
            this.cmbInsutype.Tag = "310";

            //清算类别
            this.cmbClrType.AddItems(constMgr.GetAllList("GZSI_clr_type_dz"));
            //设置默认值
            this.cmbClrType.Tag = "11";

            #region
            ArrayList al = new ArrayList();
            //2-医保服务查询
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "证件号码";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "2";
            obj.Name = "姓名";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "3";
            obj.Name = "就诊ID";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "4";
            obj.Name = "结算ID";
            al.Add(obj);

            this.ncbQueryType.AddItems(al);
            //设置默认值
            this.ncbQueryType.Tag = "1";
            #endregion

            //月结报表类型 //ID = insutype|clr_type|clr_type_lv2
            this.ncbMBReportType.AddItems(constMgr.GetAllList("GDSI_MBReportType"));
        }

        #region 事件方法
        /// <summary>
        /// 医疗机构费用明细对总账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3101_Click(object sender, EventArgs e)
        {

            if (this.cmbInsutype.SelectedItem == null)
            {
                MessageBox.Show("请选择险种类型");
                return;
            }
            if (this.cmbClrType.SelectedItem == null)
            {
                MessageBox.Show("请选择清算类别");
                return;
            }

            string clrType = this.cmbClrType.SelectedItem.ID;
            string ifSY = "0";
            if (clrType == "51" || clrType == "52") {
                clrType = clrType == "52" ? "21" : "11";
                ifSY = "1";
            }

            //获取数据
            FS.HISFC.Models.RADT.PatientInfo objInfo = new FS.HISFC.Models.RADT.PatientInfo();
            if (LocalManager.getTotBalanceInfo(ifSY,this.cmbInsutype.SelectedItem.ID, clrType, this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd HH:mm:ss"), this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"), ref objInfo) < 0)
            {
                MessageBox.Show("获取清算数据失败");
                return;
            }

            Business.Patient3201 Patient3201 = new Business.Patient3201();
            Models.Request.RequestGzsiModel3201 RequestGdsiModel3201 = new Models.Request.RequestGzsiModel3201();
            Models.Response.ResponseGzsiModel3201 ResponseGdsiModel3201 = new Models.Response.ResponseGzsiModel3201();
            RequestGdsiModel3201.data = new Models.Request.RequestGzsiModel3201.Data();

            //入参
            RequestGdsiModel3201.data.insutype = this.cmbInsutype.SelectedItem.ID;//险种类型
            RequestGdsiModel3201.data.clr_type = clrType;//清算类别
            RequestGdsiModel3201.data.setl_optins = objInfo.SIMainInfo.Clr_optins;//结算经办机构
            RequestGdsiModel3201.data.stmt_begndate = this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");//开始时间
            RequestGdsiModel3201.data.stmt_enddate = this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");//结束时间
            RequestGdsiModel3201.data.medfee_sumamt = objInfo.SIMainInfo.Medfee_sumamt.ToString("0.00");//医疗费总额
            RequestGdsiModel3201.data.fund_pay_sumamt = objInfo.SIMainInfo.Fund_pay_sumamt.ToString("0.00");//基金支付总额
            RequestGdsiModel3201.data.acct_pay = objInfo.SIMainInfo.Acct_pay.ToString("0.00");//个人账户支付金额
            RequestGdsiModel3201.data.fixmedins_setl_cnt = objInfo.SIMainInfo.Memo;//定点医药机构结算笔数

            if (Patient3201.CallService( RequestGdsiModel3201, ref ResponseGdsiModel3201) < 0)
            {
                // this.errMsg = "医疗机构费用明细对总账失败:" + Tool3201.ErrorMsg;
                MessageBox.Show("医疗机构费用明细对总账失败" + Patient3201.ErrorMsg);
                return;
            }
            if (ResponseGdsiModel3201.infcode != "0")
            {
                MessageBox.Show("医疗机构费用明细对总账失败" + ResponseGdsiModel3201.err_msg);
                return;

            }

            if (!String.IsNullOrEmpty(ResponseGdsiModel3201.output.stmtinfo.stmt_rslt_dscr))
            {
                MessageBox.Show(ResponseGdsiModel3201.output.stmtinfo.stmt_rslt_dscr);
                return;
            }

            //保存到本地
            if (this.LocalManager.updateStmtInfo(ifSY,
                                                                        this.cmbInsutype.SelectedItem.ID, //险种
                                                                        clrType, //对账类型
                                                                        this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd HH:mm:ss"), //开始时间
                                                                        this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),//结束时间
                                                                        ResponseGdsiModel3201.output.stmtinfo.setl_optins,
                                                                        ResponseGdsiModel3201.output.stmtinfo.stmt_rslt,
                                                                        ResponseGdsiModel3201.output.stmtinfo.stmt_rslt_dscr,
                                                                        "1"
                                                                        ) < 0)
            {
                MessageBox.Show("医疗机构费用明细对总账成功,但保存本地数据失败!");
                return;
            }

            //核酸
            if (this.cmbClrType.SelectedItem.ID == "11" && ifSY == "0") {
                if (this.LocalManager.updateStmtInfoHS(this.cmbInsutype.SelectedItem.ID, //险种
                                                               this.cmbClrType.SelectedItem.ID, //对账类型
                                                               this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd HH:mm:ss"), //开始时间
                                                               this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),//结束时间
                                                               ResponseGdsiModel3201.output.stmtinfo.setl_optins,
                                                               ResponseGdsiModel3201.output.stmtinfo.stmt_rslt,
                                                               ResponseGdsiModel3201.output.stmtinfo.stmt_rslt_dscr,
                                                               "1"
                                                               ) < 0)
                {
                    MessageBox.Show("医疗机构费用明细对总账成功,但保存本地数据失败!");
                    return;
                }
            }
     

            MessageBox.Show("医疗机构费用明细对总账成功!");
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region 本地数据
            if (this.cmbInsutype.SelectedItem == null)
            {
                MessageBox.Show("请选择险种类型！");
                return;
            }
            if (this.cmbClrType.SelectedItem == null)
            {
                MessageBox.Show("请选择清算类型！");
                return;
            }
            ArrayList blanceInfoList = new ArrayList();

            string clrType = this.cmbClrType.SelectedItem.ID;
            string ifSY = "0";
            if (clrType == "51" || clrType == "52")
            {
                clrType = clrType == "52" ? "21" : "11";
                ifSY = "1";
            }

            if (this.LocalManager.getBalanceDetail(ifSY,this.cmbInsutype.SelectedItem.ID, clrType, this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd HH:mm:ss"), this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"), ref blanceInfoList) < 0)
            {
                MessageBox.Show("获取对账数据失败!");
                return;
            }

            setData(blanceInfoList);
            #endregion
        }

        public void setData(ArrayList blanceInfoList)
        {
            if (blanceInfoList.Count == 0)
            {
                return;
            }

            this.neuSpread1_Sheet1.RowCount = 0;//清空
            this.fpSpread_YDZ.RowCount = 0;//
            this.fpSpread_YCJS.RowCount = 0;//异常表也清空一下

            foreach (FS.HISFC.Models.RADT.PatientInfo objInfo in blanceInfoList)
            {
                if (objInfo.SIMainInfo.Stmt_state != "1")
                {
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    int row = this.neuSpread1_Sheet1.RowCount - 1;
                    this.neuSpread1_Sheet1.Rows[row].Tag = objInfo;
                    this.neuSpread1_Sheet1.Cells[row, 0].Text = objInfo.PID.CardNO;//门诊号
                    this.neuSpread1_Sheet1.Cells[row, 1].Text = objInfo.PID.PatientNO;//住院号
                    this.neuSpread1_Sheet1.Cells[row, 2].Text = objInfo.Name;//姓名
                    this.neuSpread1_Sheet1.Cells[row, 3].Text = objInfo.SIMainInfo.Mdtrt_id;//就诊ID
                    this.neuSpread1_Sheet1.Cells[row, 4].Text = objInfo.SIMainInfo.Setl_id;//结算ID
                    this.neuSpread1_Sheet1.Cells[row, 5].Text = objInfo.SIMainInfo.Psn_no;//人员编号
                    this.neuSpread1_Sheet1.Cells[row, 6].Text = objInfo.SIMainInfo.Insutype;//险种
                    this.neuSpread1_Sheet1.Cells[row, 7].Text = objInfo.SIMainInfo.Med_type;//医疗类别
                    this.neuSpread1_Sheet1.Cells[row, 8].Text = objInfo.SIMainInfo.Clr_type;//清算类别
                    this.neuSpread1_Sheet1.Cells[row, 9].Text = objInfo.PVisit.InTime.ToShortDateString();
                    this.neuSpread1_Sheet1.Cells[row, 10].Text = objInfo.PVisit.OutTime.ToShortDateString();
                    this.neuSpread1_Sheet1.Cells[row, 11].Text = objInfo.SIMainInfo.BalanceDate.ToShortDateString();
                    this.neuSpread1_Sheet1.Cells[row, 12].Text = objInfo.SIMainInfo.TotCost.ToString();
                    this.neuSpread1_Sheet1.Cells[row, 13].Text = objInfo.SIMainInfo.PubCost.ToString();
                    this.neuSpread1_Sheet1.Cells[row, 14].Text = objInfo.SIMainInfo.OwnCost.ToString();
                    this.neuSpread1_Sheet1.Cells[row, 15].Text = "99";
                }
                else
                {
                    this.fpSpread_YDZ.Rows.Add(this.fpSpread_YDZ.RowCount, 1);
                    int row = this.fpSpread_YDZ.RowCount - 1;
                    this.fpSpread_YDZ.Rows[row].Tag = objInfo;
                    this.fpSpread_YDZ.Cells[row, 0].Text = objInfo.PID.CardNO;//门诊号
                    this.fpSpread_YDZ.Cells[row, 1].Text = objInfo.PID.PatientNO;//住院号
                    this.fpSpread_YDZ.Cells[row, 2].Text = objInfo.Name;//姓名
                    this.fpSpread_YDZ.Cells[row, 3].Text = objInfo.SIMainInfo.Mdtrt_id;//就诊ID
                    this.fpSpread_YDZ.Cells[row, 4].Text = objInfo.SIMainInfo.Setl_id;//结算ID
                    this.fpSpread_YDZ.Cells[row, 5].Text = objInfo.SIMainInfo.Psn_no;//人员编号
                    this.fpSpread_YDZ.Cells[row, 6].Text = objInfo.SIMainInfo.Insutype;//险种
                    this.fpSpread_YDZ.Cells[row, 7].Text = objInfo.SIMainInfo.Med_type;//医疗类别
                    this.fpSpread_YDZ.Cells[row, 8].Text = objInfo.SIMainInfo.Clr_type;//清算类别
                    this.fpSpread_YDZ.Cells[row, 9].Text = objInfo.PVisit.InTime.ToShortDateString();
                    this.fpSpread_YDZ.Cells[row, 10].Text = objInfo.PVisit.OutTime.ToShortDateString();
                    this.fpSpread_YDZ.Cells[row, 11].Text = objInfo.SIMainInfo.BalanceDate.ToShortDateString();
                    this.fpSpread_YDZ.Cells[row, 12].Text = objInfo.SIMainInfo.TotCost.ToString();
                    this.fpSpread_YDZ.Cells[row, 13].Text = objInfo.SIMainInfo.PubCost.ToString();
                    this.fpSpread_YDZ.Cells[row, 14].Text = objInfo.SIMainInfo.OwnCost.ToString();
                    this.fpSpread_YDZ.Cells[row, 15].Text = objInfo.SIMainInfo.Stmt_relt.ToString();
                    this.fpSpread_YDZ.Cells[row, 16].Text = objInfo.SIMainInfo.OperInfo.ID.ToString();
                    this.fpSpread_YDZ.Cells[row, 17].Text = objInfo.SIMainInfo.OperDate.ToShortDateString();
                }
            }
        }

        private void nbQueryYB_Click(object sender, EventArgs e)
        {
            #region 医保数据
            ////DateTime queryTime = this.dtBeginTime.Value.Date;
            //setlinfo = new List<Models.Response.ResponseGdsiModel90502.Output.Setlinfo>();
            //setldetail = new List<FS.HIT.Plugins.SI.GDSI.Models.Response.ResponseGdsiModel90502.Output.Setldetail>();

            //for (DateTime queryTime = this.dtBeginTime.Value.Date; queryTime <= this.dtEndTime.Value.Date; queryTime = queryTime.AddDays(1))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在获取医保中心明细！获取日期：" + queryTime.ToString("yyyy-MM-dd"));
            //    Business.Tool90502 Tool90502 = new FS.HIT.Plugins.SI.GDSI.Business.Tool90502();
            //    Models.Request.RequestGdsiModel90502 RequestGdsiModel90502 = new FS.HIT.Plugins.SI.GDSI.Models.Request.RequestGdsiModel90502();
            //    Models.Response.ResponseGdsiModel90502 ResponseGdsiModel90502 = new FS.HIT.Plugins.SI.GDSI.Models.Response.ResponseGdsiModel90502();
            //    RequestGdsiModel90502.data = new FS.HIT.Plugins.SI.GDSI.Models.Request.RequestGdsiModel90502.Data();

            //    RequestGdsiModel90502.data.setl_time = queryTime.ToString("yyyy-MM-dd");
            //    RequestGdsiModel90502.data.insutype = this.cmbInsutype.Tag.ToString();
            //    RequestGdsiModel90502.data.clr_type = this.cmbClrType.Tag.ToString();
            //    RequestGdsiModel90502.data.pageNum = "1";

            //    if (Tool90502.CallService("", RequestGdsiModel90502, ref ResponseGdsiModel90502) < 0)
            //    {
            //        //MessageBox.Show("医疗机构费用明细查询失败" + Tool90502.ErrorMsg);
            //        //return;
            //        continue;
            //    }
            //    if (ResponseGdsiModel90502.infcode != "0")
            //    {
            //        //MessageBox.Show("医疗机构费用明细查询失败" + ResponseGdsiModel90502.err_msg);
            //        //return;
            //        continue;
            //    }

            //    if (ResponseGdsiModel90502.output.setlinfo != null && ResponseGdsiModel90502.output.setlinfo.Count > 0)
            //    {
            //        setlinfo.AddRange(ResponseGdsiModel90502.output.setlinfo);
            //    }

            //    if (ResponseGdsiModel90502.output.setldetail != null && ResponseGdsiModel90502.output.setlinfo.Count > 0) {
            //        setldetail.AddRange(ResponseGdsiModel90502.output.setldetail);
            //    }
            //}

            ////显示对数明细
            //if (setlinfo != null && setlinfo.Count > 0)
            //{
            //    Common.Function.ShowOutDateToFarpointNOChange<Models.Response.ResponseGdsiModel90502.Output.Setlinfo>(this.fpSpread_YBMX, setlinfo);
            //}
            //else
            //{
            //    MessageBox.Show("没有查询到对数明细信息！");
            //}
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #endregion
            string clrType = this.cmbClrType.SelectedItem.ID;
            string med_type = clrType;
            if (clrType == "51" || clrType == "52")
            {
                clrType = clrType == "52" ? "21" : "11";
            }

            this.setDataYB(this.cmbInsutype.Tag.ToString(), clrType, med_type, this.dtBeginTime.Value, this.dtEndTime.Value);
        }

        private void setDataYB(string insutype, string clr_type,string med_type, DateTime beginTime, DateTime endTime)
        {
            setlinfo = new List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>();
            setldetail = new List<Models.Response.ResponseGzsiModel90502.Output.Setldetail>();

            for (DateTime queryTime = beginTime.Date; queryTime <= endTime.Date; queryTime = queryTime.AddDays(1))
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在获取医保中心明细！获取日期：" + queryTime.ToString("yyyy-MM-dd"));
                int pagenum = 1;
                while (true)
                {
                    Business.Patient90502 Tool90502 = new Business.Patient90502();
                    Models.Request.RequestGzsiModel90502 RequestGdsiModel90502 = new Models.Request.RequestGzsiModel90502();
                    Models.Response.ResponseGzsiModel90502 ResponseGdsiModel90502 = new Models.Response.ResponseGzsiModel90502();
                    RequestGdsiModel90502.data = new Models.Request.RequestGzsiModel90502.Data();

                    RequestGdsiModel90502.data.setl_time = queryTime.ToString("yyyy-MM-dd");
                    RequestGdsiModel90502.data.insutype = insutype;
                    RequestGdsiModel90502.data.clr_type = clr_type;

                    //省内住院
                    if (clr_type == "9902" || clr_type == "9903") {
                        med_type = clr_type == "9902" ? "21" : "11";
                    }

                    RequestGdsiModel90502.data.med_type = med_type;
                    RequestGdsiModel90502.data.pageNum = pagenum.ToString();

                    if (Tool90502.CallService(RequestGdsiModel90502, ref ResponseGdsiModel90502) < 0)
                    {
                        break;
                    }
                    if (ResponseGdsiModel90502.infcode != "0")
                    {
                        break;
                    }

                    if (ResponseGdsiModel90502.output.setlinfo != null && ResponseGdsiModel90502.output.setlinfo.Count > 0)
                    {
                        setlinfo.AddRange(ResponseGdsiModel90502.output.setlinfo);
                    }

                    if (ResponseGdsiModel90502.output.setldetail != null && ResponseGdsiModel90502.output.setlinfo.Count > 0)
                    {
                        setldetail.AddRange(ResponseGdsiModel90502.output.setldetail);
                    }

                    if (ResponseGdsiModel90502.output.setldetail != null && ResponseGdsiModel90502.output.setlinfo.Count == 1000)
                    {
                        pagenum++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //显示对数明细
            if (setlinfo != null && setlinfo.Count > 0)
            {
                Common.Function.ShowOutDateToFarpoint<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>(this.fpSpread_YBMX, setlinfo);
                #region //颜色区分
                for (int i = 0; i < this.fpSpread_YBMX.RowCount; i++)
                {
                    //取消结算
                    if (this.fpSpread_YBMX.Cells[i, 46].Text == "1")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Red;
                        continue;
                    }
                    //异地
                    if (!this.fpSpread_YBMX.Cells[i, 16].Text.StartsWith("4406"))
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Blue;
                        continue;
                    }
                    //零星
                    if (this.fpSpread_YBMX.Cells[i, 49].Text != "2")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Orange;
                        continue;
                    }
                    //以对账
                    if (this.fpSpread_YBMX.Cells[i, 45].Text == "1")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Green;
                        continue;
                    }
                }
                #endregion
            }
            else
            {
                //MessageBox.Show("没有查询到对数明细信息！");
            }





            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }


        /// <summary>
        /// 医疗机构费用明细对明细账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuButton1_Click(object sender, EventArgs e)
        {
            #region 导出txt
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在导出保存明细!请稍后!");
            //明细导出路径：.\log\SIAPI_DZMX\YYYY-MM-DD\YYYYMMDDHHmmss\file.txt
            string name = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = @".\log\GZAPI_DZMX\" + name;
            string param = string.Empty;
            string clrType = string.Empty;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            if (this.cmbClrType.Tag != null && string.IsNullOrEmpty(this.cmbClrType.Tag.ToString()))
            {
                string clr = this.cmbClrType.Tag.ToString();
                if (clr == "11" || clr == "51")
                {
                    clrType = "0";
                }
                if (clr == "21" || clr == "52")
                {
                    clrType = "1";
                }
            }

            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                param += this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text + "\t";//就诊ID
                param += this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text + "\t";//结算ID
                param += this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text + "\t";//个人编号
                param += this.neuSpread1_Sheet1.Cells[rowIndex, 12].Text + "\t";//总金额
                param += this.neuSpread1_Sheet1.Cells[rowIndex, 13].Text + "\t";//报销金额
                param += "0" + "\t";//this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text + "\t";//门诊住院，0
                param += "0" + "\t";// this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text + "\t";//结算退费标志，0
                if (rowIndex != this.neuSpread1_Sheet1.RowCount - 1) param += "\n";
                dic.Add(this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text + this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text, rowIndex);
            }

            if (!string.IsNullOrEmpty(param))
            {
                this.Derive(path, param);
            }
            else
            {
                MessageBox.Show("没有需要上传对账的明细");
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            #region //压缩
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在压缩明细!请稍后!");
            if (!Common.Function.CreateZipFile(path, path + "\\file.zip"))
            {
                MessageBox.Show("压缩失败！");
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            #region //上传
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传明细文件!请稍后!");
            string fileQuryNo9101 = this.up9101(path + "\\file.zip");
            if (string.IsNullOrEmpty(fileQuryNo9101))
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            #region//对账
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在对账!请稍后!");
            string fileQuryNo3202 = this.CheckDetail3202(fileQuryNo9101);
            if (string.IsNullOrEmpty(fileQuryNo3202))
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            #region  //下载对账结果
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在下载对账结果!请稍后!");
            if (!this.down9102(path + "\\outputfile.zip", fileQuryNo3202))
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            #region //解压下载结果
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在解析对账结果!请稍后!");
            List<FileInfo> fileInfoList = Common.Function.UnZipFile(path + "\\outputfile.zip");
            //解析内容
            if (fileInfoList != null && fileInfoList.Count > 0)
            {
                FileInfo fileInfo = fileInfoList[0];//获取文本
                string text = "";//行文本内容
                int j = 0;//解析行数
                this.fpSpread_ZXD.RowCount = 0;
                this.fpSpread_JEBYZ.RowCount = 0;

                StreamReader sr = fileInfo.OpenText();
                while (!string.IsNullOrEmpty((text = sr.ReadLine())))
                {
                    //0人员编号 1就诊ID 2结算ID 3发送报文ID 4对账结果 5退费结果标志 6备注&&险种 7总金额 8医保基金支付 9个账支出
                    //备注：退费结果标志为0时，textList[7] = 备注;为null时，textList[6] = 备注
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在解析对账结果中，第" + (++j).ToString() + "行！");
                    string[] textList = text.Split('\t');
                    int markindex = textList[5] == "0" ? 7 : 6;

                    #region //非指定险种
                    if (!textList[markindex].EndsWith(this.cmbInsutype.Tag.ToString()))
                    {
                        //this.fpSpread_DB.Rows.Add(this.fpSpread_DB.RowCount, 1);
                        //int row = this.fpSpread_DB.RowCount - 1;
                        //this.fpSpread_DB.Rows[row].Tag = textList;
                        //this.fpSpread_DB.Cells[row, 0].Text = textList[0]+"";
                        //this.fpSpread_DB.Cells[row, 1].Text = textList[1];
                        //this.fpSpread_DB.Cells[row, 2].Text = textList[2];
                        //this.fpSpread_DB.Cells[row, 3].Text = textList[3];
                        //this.fpSpread_DB.Cells[row, 4].Text = textList[4];
                        //this.fpSpread_DB.Cells[row, 5].Text = textList[5];
                        //this.fpSpread_DB.Cells[row, 6].Text = textList[markindex];
                        //this.fpSpread_DB.Cells[row, 7].Text = textList[markindex+1];
                        //this.fpSpread_DB.Cells[row, 8].Text = textList[markindex+2];
                        //this.fpSpread_DB.Cells[row, 9].Text = textList[markindex+3];
                        //this.fpSpread_DB.Rows[row].ForeColor = Color.Gray;
                        continue;
                    }
                    #endregion

                    #region //医保中心多
                    if (!dic.ContainsKey(textList[2] + textList[1]))
                    {
                        this.fpSpread_ZXD.Rows.Add(this.fpSpread_ZXD.RowCount, 1);
                        int row = this.fpSpread_ZXD.RowCount - 1;
                        this.fpSpread_ZXD.Rows[row].Tag = textList;
                        this.fpSpread_ZXD.Cells[row, 0].Text = textList[0] + " ";
                        this.fpSpread_ZXD.Cells[row, 1].Text = textList[1];
                        this.fpSpread_ZXD.Cells[row, 2].Text = textList[2];
                        this.fpSpread_ZXD.Cells[row, 3].Text = textList[3];
                        this.fpSpread_ZXD.Cells[row, 4].Text = textList[4];
                        this.fpSpread_ZXD.Cells[row, 5].Text = textList[5];
                        this.fpSpread_ZXD.Cells[row, 6].Text = textList[markindex];
                        this.fpSpread_ZXD.Cells[row, 7].Text = textList[markindex + 1];
                        this.fpSpread_ZXD.Cells[row, 8].Text = textList[markindex + 2];
                        this.fpSpread_ZXD.Cells[row, 9].Text = textList[markindex + 3];
                        this.fpSpread_ZXD.Rows[row].ForeColor = Color.Red;
                        continue;
                    }
                    #endregion

                    int i = dic[textList[2] + textList[1]];
                    this.neuSpread1_Sheet1.Cells[i, 15].Text = textList[4];
                    this.neuSpread1_Sheet1.Cells[i, 16].Text = textList[markindex];
                    if (textList[4] == "0")
                    {
                        //对账成功
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = Color.Green;
                    }
                    else
                    {
                        #region //对账失败不平
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = Color.Red;

                        //添加到异常数据表中
                        this.fpSpread_JEBYZ.Rows.Add(this.fpSpread_ZXD.RowCount, 1);
                        int row = this.fpSpread_JEBYZ.RowCount - 1;
                        this.fpSpread_JEBYZ.Cells[row, 0].Text = this.neuSpread1_Sheet1.Cells[i, 0].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 1].Text = this.neuSpread1_Sheet1.Cells[i, 1].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 2].Text = this.neuSpread1_Sheet1.Cells[i, 2].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 3].Text = this.neuSpread1_Sheet1.Cells[i, 3].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 4].Text = this.neuSpread1_Sheet1.Cells[i, 4].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 5].Text = this.neuSpread1_Sheet1.Cells[i, 5].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 6].Text = this.neuSpread1_Sheet1.Cells[i, 6].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 7].Text = this.neuSpread1_Sheet1.Cells[i, 7].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 8].Text = this.neuSpread1_Sheet1.Cells[i, 8].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 9].Text = this.neuSpread1_Sheet1.Cells[i, 9].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 10].Text = this.neuSpread1_Sheet1.Cells[i, 10].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 11].Text = this.neuSpread1_Sheet1.Cells[i, 11].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 12].Text = this.neuSpread1_Sheet1.Cells[i, 12].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 13].Text = this.neuSpread1_Sheet1.Cells[i, 13].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 14].Text = this.neuSpread1_Sheet1.Cells[i, 14].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 15].Text = this.neuSpread1_Sheet1.Cells[i, 15].ToString();
                        this.fpSpread_JEBYZ.Cells[row, 16].Text = this.neuSpread1_Sheet1.Cells[i, 16].ToString();
                        #endregion
                    }
                }
                this.neuSpread1_Sheet1.SortRows(15, false, true);
                this.fpSpread_ZXD.SortRows(4, false, true);

                #region //院内多
                this.fpSpread_YND.RowCount = 0;//清空
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 15].Text == "99")
                    {
                        this.fpSpread_YND.Rows.Add(this.fpSpread_YND.RowCount, 1);
                        int row = this.fpSpread_YND.RowCount - 1;
                        this.fpSpread_YND.Cells[row, 0].Text = this.neuSpread1_Sheet1.Cells[i, 0].Text.ToString();
                        this.fpSpread_YND.Cells[row, 1].Text = this.neuSpread1_Sheet1.Cells[i, 1].Text.ToString();
                        this.fpSpread_YND.Cells[row, 2].Text = this.neuSpread1_Sheet1.Cells[i, 2].Text.ToString();
                        this.fpSpread_YND.Cells[row, 3].Text = this.neuSpread1_Sheet1.Cells[i, 3].Text.ToString();
                        this.fpSpread_YND.Cells[row, 4].Text = this.neuSpread1_Sheet1.Cells[i, 4].Text.ToString();
                        this.fpSpread_YND.Cells[row, 5].Text = this.neuSpread1_Sheet1.Cells[i, 5].Text.ToString();
                        this.fpSpread_YND.Cells[row, 6].Text = this.neuSpread1_Sheet1.Cells[i, 6].Text.ToString();
                        this.fpSpread_YND.Cells[row, 7].Text = this.neuSpread1_Sheet1.Cells[i, 7].Text.ToString();
                        this.fpSpread_YND.Cells[row, 8].Text = this.neuSpread1_Sheet1.Cells[i, 8].Text.ToString();
                        this.fpSpread_YND.Cells[row, 9].Text = this.neuSpread1_Sheet1.Cells[i, 9].Text.ToString();
                        this.fpSpread_YND.Cells[row, 10].Text = this.neuSpread1_Sheet1.Cells[i, 10].Text.ToString();
                        this.fpSpread_YND.Cells[row, 11].Text = this.neuSpread1_Sheet1.Cells[i, 11].Text.ToString();
                        this.fpSpread_YND.Cells[row, 12].Text = this.neuSpread1_Sheet1.Cells[i, 12].Text.ToString();
                        this.fpSpread_YND.Cells[row, 13].Text = this.neuSpread1_Sheet1.Cells[i, 13].Text.ToString();
                        this.fpSpread_YND.Cells[row, 14].Text = this.neuSpread1_Sheet1.Cells[i, 14].Text.ToString();
                        this.fpSpread_YND.Cells[row, 15].Text = this.neuSpread1_Sheet1.Cells[i, 15].Text.ToString();
                        this.fpSpread_YND.Cells[row, 16].Text = this.neuSpread1_Sheet1.Cells[i, 16].Text.ToString();
                        continue;
                    }
                    break;
                }
                #endregion
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #endregion
        }

        /// <summary>
        /// txt文件保存
        /// </summary>
        public void Derive(string path, string param)
        {
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            System.IO.StreamWriter w = new System.IO.StreamWriter(path + "\\file.txt", true);
            w.WriteLine(param);
            w.Flush();
            w.Close();
        }

        //明细对账
        public string CheckDetail3202(string FileQueyNo)
        {
            if (this.cmbClrType.SelectedItem == null)
            {
                MessageBox.Show("请选择清算类别");
                return null;
            }

            string clrType = this.cmbClrType.SelectedItem.ID;
            string ifSY = "0";
            if (clrType == "51" || clrType == "52")
            {
                clrType = clrType == "52" ? "21" : "11";
                ifSY = "1";
            }

            //获取数据
            FS.HISFC.Models.RADT.PatientInfo objInfo = new FS.HISFC.Models.RADT.PatientInfo();
            if (LocalManager.getTotBalanceInfo(ifSY, this.cmbInsutype.SelectedItem.ID, clrType, this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd HH:mm:ss"), this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"), ref objInfo) < 0)
            {
                MessageBox.Show("获取清算数据失败");
                return null;
            }

            Business.Patient3202 Tool3202 = new Business.Patient3202();
            Models.Request.RequestGzsiModel3202 RequestGdsiModel3202 = new Models.Request.RequestGzsiModel3202();
            Models.Response.ResponseGzsiModel3202 ResponseGdsiModel3202 = new Models.Response.ResponseGzsiModel3202();
            RequestGdsiModel3202.data = new Models.Request.RequestGzsiModel3202.Data();

            //RequestGdsiModel3202.data.insutype = this.cmbInsutype.SelectedItem.ID;//险种类型
            RequestGdsiModel3202.data.clr_type = clrType;// this.cmbClrType.SelectedItem.ID;//清算类别
            RequestGdsiModel3202.data.setl_optins = objInfo.SIMainInfo.Clr_optins;//结算经办机构 TOOL
            RequestGdsiModel3202.data.stmt_begndate = this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");//开始时间
            RequestGdsiModel3202.data.stmt_enddate = this.dtEndTime.Value.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");//结束时间
            RequestGdsiModel3202.data.medfee_sumamt = objInfo.SIMainInfo.Medfee_sumamt.ToString("0.00");//医疗费总额
            RequestGdsiModel3202.data.fund_pay_sumamt = objInfo.SIMainInfo.Fund_pay_sumamt.ToString("0.00");//基金支付总额
            RequestGdsiModel3202.data.cash_payamt = objInfo.SIMainInfo.Acct_pay.ToString("0.00");//个人账户支付金额
            RequestGdsiModel3202.data.fixmedins_setl_cnt = objInfo.SIMainInfo.Memo;//定点医药机构结算笔数
            RequestGdsiModel3202.data.file_qury_no = FileQueyNo;//文件查询号
            RequestGdsiModel3202.data.REFD_SETL_FLAG = "0";
            //RequestGdsiModel3202.data.setl_optins = "H44020300006";

            if (Tool3202.CallService( RequestGdsiModel3202, ref ResponseGdsiModel3202) < 0)
            {
                // this.errMsg = "医疗机构费用明细对总账失败:" + Tool3201.ErrorMsg;
                MessageBox.Show("医疗机构费用明细对明细账失败" + Tool3202.ErrorMsg);
                return null;
            }
            if (ResponseGdsiModel3202.infcode != "0")
            {
                MessageBox.Show("医疗机构费用明细对明细账失败" + ResponseGdsiModel3202.err_msg);
                return null;
            }

            //MessageBox.Show("医疗机构费用明细对明细账成功,请下载文件查看对账明细");
            return ResponseGdsiModel3202.output.fileinfo.file_qury_no;
        }

        //上传文件
        public string up9101(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("上传文件为空");
                return null;
            }

            List<int> In = new List<int>();
            sbyte[] intsByte = Common.Function.AuthGetFileData(filePath);
            for (int i = 0; i < intsByte.Length; i++)
            {
                In.Add(Convert.ToInt32(intsByte[i]));
            }

            string fileName = Path.GetFileName(filePath);
            Business.CommonService9101 Upload9101 = new Business.CommonService9101();
            Models.Request.RequestGzsiModel9101 RequestGdsiModel9101 = new Models.Request.RequestGzsiModel9101();
            Models.Response.ResponseGzsiModel9101 ResponseGdsiModel9101 = new Models.Response.ResponseGzsiModel9101();
            RequestGdsiModel9101.fsUploadIn = new Models.Request.RequestGzsiModel9101.FsUploadIn();
            RequestGdsiModel9101.fsUploadIn.@in = In;//文件字节流
            RequestGdsiModel9101.fsUploadIn.filename = fileName;//文件名
            RequestGdsiModel9101.fsUploadIn.fixmedins_code = Models.UserInfo.Instance.userId;//医疗机构编号

            if (Upload9101.CallService(RequestGdsiModel9101, ref ResponseGdsiModel9101) < 0)
            {
                // this.errMsg = "医疗机构费用明细对总账失败:" + Tool3201.ErrorMsg;
                MessageBox.Show("文件上传失败" + Upload9101.ErrorMsg);
                return null;
            }
            if (ResponseGdsiModel9101.infcode != "0")
            {
                MessageBox.Show("文件上传失败" + ResponseGdsiModel9101.err_msg);
                return null;
            }

            //MessageBox.Show("文件上传成功!");
            return ResponseGdsiModel9101.output.file_qury_no;

        }

        //下载文件
        public bool down9102(string filepath, string file_qury_no)
        {
            Business.CommonService9102 Upload9102 = new Business.CommonService9102();
            Models.Request.RequestGzsiModel9102 RequestGdsiModel9102 = new Models.Request.RequestGzsiModel9102();
            Models.Response.ResponseGzsiModel9102 ResponseGzsiModel9102 = new Models.Response.ResponseGzsiModel9102();
            RequestGdsiModel9102.fsDownloadIn = new Models.Request.RequestGzsiModel9102.FsDownloadIn();
            RequestGdsiModel9102.fsDownloadIn.file_qury_no = file_qury_no;
            RequestGdsiModel9102.fsDownloadIn.filename = Path.GetFileName(filepath);
            RequestGdsiModel9102.fsDownloadIn.fixmedins_code = Models.UserInfo.Instance.userId;
            if (Upload9102.CallService( RequestGdsiModel9102, ref ResponseGzsiModel9102, filepath) < 0)
            {
                // this.errMsg = "医疗机构费用明细对总账失败:" + Tool3201.ErrorMsg;
                MessageBox.Show("文件下载失败" + Upload9102.ErrorMsg);
                return false;
            }
            return true;
        }

        //医保中心查询
        private void nbZXQuery_Click(object sender, EventArgs e)
        {
            if (setlinfo == null || setlinfo.Count <= 0)
            {
                MessageBox.Show("没有获取医保中心数据！");
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("查询中!请稍后!");
            List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo> setlinfoT
                = new List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>();
            if (this.ncbQueryType.Tag == null || string.IsNullOrEmpty(this.tbQueryInfo.Text))
            {
                Common.Function.ShowOutDateToFarpoint<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>(this.fpSpread_YBMX, setlinfo);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }

            foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
            {
                string queryinfo = string.Empty;
                if (this.ncbQueryType.Tag.ToString() == "1")
                {
                    queryinfo = sinfo.certno;
                }
                else if (this.ncbQueryType.Tag.ToString() == "2")
                {
                    queryinfo = sinfo.psn_name;
                }
                else if (this.ncbQueryType.Tag.ToString() == "3")
                {
                    queryinfo = sinfo.mdtrt_id;
                }
                else if (this.ncbQueryType.Tag.ToString() == "4")
                {
                    queryinfo = sinfo.setl_id;
                }
                if (queryinfo.Contains(this.tbQueryInfo.Text))
                {
                    setlinfoT.Add(sinfo);
                }
            }

            if (setlinfoT != null && setlinfoT.Count > 0)
            {
                Common.Function.ShowOutDateToFarpoint<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>(this.fpSpread_YBMX, setlinfoT);
                #region //颜色区分
                for (int i = 0; i < this.fpSpread_YBMX.RowCount; i++)
                {
                    //以对账
                    if (this.fpSpread_YBMX.Cells[i, 45].Text == "1")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Green;
                        continue;
                    }
                    //取消结算
                    if (this.fpSpread_YBMX.Cells[i, 46].Text == "1")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Red;
                        continue;
                    }
                    //异地
                    if (!this.fpSpread_YBMX.Cells[i, 16].Text.StartsWith("4406"))
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Blue;
                        continue;
                    }
                    //零星
                    if (this.fpSpread_YBMX.Cells[i, 49].Text != "2")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Orange;
                        continue;
                    }
                }
                #endregion
            }
            else
            {
                MessageBox.Show("没有查询到相关数据！");
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        //生成月结报表
        private void nbMonthlyBalanceReport_Click(object sender, EventArgs e)
        {

            if (this.ncbMBReportType.Tag == null || string.IsNullOrEmpty(this.ncbMBReportType.Tag.ToString())) {
                MessageBox.Show("生成报表类型不能为空！");
                return;
            }

            if (MessageBox.Show("是否确定生成【" + this.dtBeginTime.Value.Date.ToString("yyyy-MM-dd") + "到" +
                this.dtEndTime.Value.Date.ToString("yyyy-MM-dd") + "】【" + this.ncbMBReportType.Text + "】的月结报表？", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }


            #region //选择报表类型
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在生成月结报表!请稍后!");
            if (this.ncbMBReportType.Tag == null || string.IsNullOrEmpty(this.ncbMBReportType.Tag.ToString()))
            {
                MessageBox.Show("请选择生成月结报表类型！");
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            //insutype|clr_type|clr_type_lv2
            string[] queryInfo = this.ncbMBReportType.Tag.ToString().Split('|');
            string insutype = queryInfo[0];
            string clr_type = queryInfo[1];
            string clr_type_lv2 = queryInfo[2];
            #endregion

            if (queryInfo[2].Contains( "SY"))
            {
                ArrayList insutypeList = new ArrayList();//险种列表 insutype
                insutypeList.Add("310");
                if (insutypeList == null && insutypeList.Count <= 0)
                {
                    MessageBox.Show("请先维护需要汇总的险种类型！");
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }

                ArrayList clrTypeList = new ArrayList();//类型列表 clr_type

                if (queryInfo[2] == "SY") {
                    clrTypeList.Add("11");
                    clrTypeList.Add("21");
                }
                else if (queryInfo[2] == "SYMZ")
                {
                    clrTypeList.Add("11");
                }
                else if (queryInfo[2] == "SYZY")
                {
                    clrTypeList.Add("21");
                }

                if (clrTypeList == null && clrTypeList.Count <= 0)
                {
                    MessageBox.Show("请先维护需要汇总的清算类型！");
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }


                foreach (string Insutype in insutypeList)
                {
                    foreach (string clr_Type in clrTypeList)
                    {
                        string med_type = clr_Type == "11"?"51":"52";
                        #region //查询90502
                        this.setDataYB(Insutype, clr_Type, med_type, this.dtBeginTime.Value, this.dtEndTime.Value);
                        if (setlinfo == null || setlinfo.Count <= 0)
                        {
                            continue;
                        }
                        #endregion

                        #region 清空数据
                        //this.LocalManager.deleteLogarithm();
                        #endregion

                        #region 对数明细信息
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在导出保存明细!请稍后!");
                        foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
                        {
                            //先清数据
                            this.LocalManager.deleteLogarithm(sinfo.mdtrt_id, sinfo.setl_id);

                            //过滤零报数据
                            if (sinfo.pay_loc == "1")
                            {
                                continue;
                            }

                            //存在异地的数据
                            if (!sinfo.insu_optins.StartsWith("4406"))
                            {
                                continue;
                            }

                            //退费
                            if (sinfo.refd_setl_flag == "1")
                            {
                                continue;
                            }

                            //未对账
                            if (sinfo.medins_stmt_flag == "0")
                            {
                                MessageBox.Show("存在未对账结算，请先进行对账!对账日期：" + sinfo.setl_time.Substring(0, 10));
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                return;
                            }
                            else
                            {
                                #region //保存90502的数据，保存已对账数据
                                if (this.LocalManager.ExisteLogarithmSetlinfo(sinfo.mdtrt_id, sinfo.setl_id) == "1")
                                {
                                    continue;
                                }
                                else
                                {
                                    if (this.LocalManager.insertLogarithmSetlinfo(sinfo) < 0)
                                    {
                                        MessageBox.Show("保存对数明细信息失败!结算ID【" + sinfo.setl_id + "】" + this.LocalManager.Err);
                                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                        return;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region 基金支付
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在导出保存基金支付信息!请稍后!");
                        foreach (Models.Response.ResponseGzsiModel90502.Output.Setldetail sdetail in setldetail)
                        {
                            if (this.LocalManager.ExisteLogarithmSetldetail(sdetail.mdtrt_id, sdetail.setl_id, sdetail.fund_pay_type) == "1")
                            {
                                continue;
                            }
                            else
                            {
                                if (this.LocalManager.insertLogarithmSetldetail(sdetail) < 0)
                                {
                                    MessageBox.Show("保存失败结算基金分项信息!结算ID【" + sdetail.setl_id + "】" + this.LocalManager.Err);
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    return;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            else {
                #region //查询90502
                string med_type = clr_type;
                this.setDataYB(insutype, clr_type, med_type, this.dtBeginTime.Value, this.dtEndTime.Value);
                if (setlinfo == null || setlinfo.Count <= 0)
                {
                    return;
                }
                #endregion

                #region 清空数据
                //this.LocalManager.deleteLogarithm();
                #endregion

                #region 对数明细信息
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在导出保存明细!请稍后!");
                foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
                {
                    //先清数据
                    this.LocalManager.deleteLogarithm(sinfo.mdtrt_id, sinfo.setl_id);

                    //过滤零报数据
                    if (sinfo.pay_loc == "1")
                    {
                        continue;
                    }

                    //存在异地的数据
                    if (!sinfo.insu_optins.StartsWith("4406"))
                    {
                        continue;
                    }

                    //退费
                    if (sinfo.refd_setl_flag == "1")
                    {
                        continue;
                    }

                    //未对账
                    if (sinfo.medins_stmt_flag == "0")
                    {
                        MessageBox.Show("存在未对账结算，请先进行对账!对账日期：" + sinfo.setl_time.Substring(0, 10));
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return;
                    }
                    else
                    {
                        #region //保存90502的数据，保存已对账数据
                        if (this.LocalManager.ExisteLogarithmSetlinfo(sinfo.mdtrt_id, sinfo.setl_id) == "1")
                        {
                            continue;
                        }
                        else
                        {
                            if (this.LocalManager.insertLogarithmSetlinfo(sinfo) < 0)
                            {
                                MessageBox.Show("保存对数明细信息失败!结算ID【" + sinfo.setl_id + "】" + this.LocalManager.Err);
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                return;
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                #region 基金支付
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在导出保存基金支付信息!请稍后!");
                foreach (Models.Response.ResponseGzsiModel90502.Output.Setldetail sdetail in setldetail)
                {
                    if (this.LocalManager.ExisteLogarithmSetldetail(sdetail.mdtrt_id, sdetail.setl_id, sdetail.fund_pay_type) == "1")
                    {
                        continue;
                    }
                    else
                    {
                        if (this.LocalManager.insertLogarithmSetldetail(sdetail) < 0)
                        {
                            MessageBox.Show("保存失败结算基金分项信息!结算ID【" + sdetail.setl_id + "】" + this.LocalManager.Err);
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            return;
                        }
                    }
                }
                #endregion
            }

            #region //生成数据
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在计算费用信息!请稍后!");
            System.Data.DataTable dt = this.LocalManager.getLogarithmInfo(insutype, clr_type, clr_type_lv2,
                     this.dtBeginTime.Value.ToString("yyyy-MM-dd"), this.dtEndTime.Value.AddDays(1).ToShortDateString());

            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("生成报表失败：没有获取到相关数据！");
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            #endregion

            #region //赋值
            //表头
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在生成表格!请稍后!");
            //this.fpSpread_YJBB.Cells["ReportType"].Text = "佛山市基本医疗保险医疗费用现场结算月结统计表(" + this.ncbMBReportType.Text + ")";
            //this.fpSpread_YJBB.Cells["HosName"].Text = "医疗机构名称（顺德爱博恩妇产医院 ）";
            //this.fpSpread_YJBB.Cells["insuplc_admdvs"].Text = "(顺德区)";
            //this.fpSpread_YJBB.Cells["printDate"].Text = "打印日期：" + System.DateTime.Now.Date.ToString("yyyy-MM-dd");
            this.fpSpread_YJBB.ColumnHeader.Cells[0, 0].Text = "佛山市基本医疗保险医疗费用现场结算月结统计表(" + this.ncbMBReportType.Text + ")";
            this.fpSpread_YJBB.ColumnHeader.Cells[1, 0].Text = "医疗机构名称（盖章）：佛山顺德爱博恩妇产医院有限公司";
            //this.fpSpread_YJBB.ColumnHeader.Cells[1, 5].Text = "医院所在地：顺德区";
            this.fpSpread_YJBB.ColumnHeader.Cells[1, 11].Text = "清算年月："+this.dtBeginTime.Value.Date.ToString("yyyyMM")+ "                                打印日期：" + System.DateTime.Now.Date.ToString("yyyy -MM-dd");


            //数据,第5行开始
            int rowindex = 0;
            decimal fund_pay_sumamt = 0;
            this.fpSpread_YJBB.RowCount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                this.fpSpread_YJBB.Rows.Add(this.fpSpread_YJBB.RowCount, 1);
                this.fpSpread_YJBB.Cells[rowindex, 0].Text = this.constMgr.GetConstant("GZSI_clr_way", dr[0].ToString()).Name;//清算方式
                this.fpSpread_YJBB.Cells[rowindex, 1].Text = this.constMgr.GetConstant("GZSI_clr_type", dr[1].ToString()).Name;//清算类别
                this.fpSpread_YJBB.Cells[rowindex, 2].Text = this.constMgr.GetConstant("GZSI_clr_type_lv2", dr[2].ToString()).Name;//二级清算类别
                this.fpSpread_YJBB.Cells[rowindex, 3].Text = dr[3].ToString();//人数
                this.fpSpread_YJBB.Cells[rowindex, 4].Text = dr[4].ToString();//实际支付人次
                this.fpSpread_YJBB.Cells[rowindex, 5].Text = decimal.Parse(dr[5].ToString()).ToString("F2");//医疗总费用
                this.fpSpread_YJBB.Cells[rowindex, 6].Text = decimal.Parse(dr[6].ToString()).ToString("F2");//基本医疗保险统筹基金支付
                this.fpSpread_YJBB.Cells[rowindex, 7].Text = decimal.Parse(dr[7].ToString()).ToString("F2");//基本医疗超1万以上金额
                this.fpSpread_YJBB.Cells[rowindex, 8].Text = decimal.Parse(dr[8].ToString()).ToString("F2");//大病基金支付
                this.fpSpread_YJBB.Cells[rowindex, 9].Text = decimal.Parse(dr[9].ToString()).ToString("F2");//公务员基金支付（市直）
                this.fpSpread_YJBB.Cells[rowindex, 10].Text = decimal.Parse(dr[10].ToString()).ToString("F2");//公务员基金支付（禅城区）
                this.fpSpread_YJBB.Cells[rowindex, 11].Text = decimal.Parse(dr[11].ToString()).ToString("F2");//公务员基金支付（南海区）
                this.fpSpread_YJBB.Cells[rowindex, 12].Text = decimal.Parse(dr[12].ToString()).ToString("F2");//公务员基金支付（三水区）
                this.fpSpread_YJBB.Cells[rowindex, 13].Text = decimal.Parse(dr[13].ToString()).ToString("F2");//公务员基金支付（高明区）
                this.fpSpread_YJBB.Cells[rowindex, 14].Text = decimal.Parse(dr[14].ToString()).ToString("F2");//离休医疗保障（市直）
                this.fpSpread_YJBB.Cells[rowindex, 15].Text = decimal.Parse(dr[15].ToString()).ToString("F2");//离休医疗保障（禅城区）
                this.fpSpread_YJBB.Cells[rowindex, 16].Text = decimal.Parse(dr[16].ToString()).ToString("F2");//离休医疗保障（南海区）
                this.fpSpread_YJBB.Cells[rowindex, 17].Text = decimal.Parse(dr[17].ToString()).ToString("F2");//离休医疗保障（顺德区）
                this.fpSpread_YJBB.Cells[rowindex, 18].Text = decimal.Parse(dr[18].ToString()).ToString("F2");//离休医疗保障（三水区）
                this.fpSpread_YJBB.Cells[rowindex, 19].Text = decimal.Parse(dr[19].ToString()).ToString("F2");//离休医疗保障（高明区）
                this.fpSpread_YJBB.Cells[rowindex, 20].Text = decimal.Parse(dr[20].ToString()).ToString("F2");//医疗救助基金支付_市直
                this.fpSpread_YJBB.Cells[rowindex, 21].Text = decimal.Parse(dr[21].ToString()).ToString("F2");//医疗救助基金支付（禅城区）
                this.fpSpread_YJBB.Cells[rowindex, 22].Text = decimal.Parse(dr[22].ToString()).ToString("F2");//医疗救助基金支付（南海区）
                this.fpSpread_YJBB.Cells[rowindex, 23].Text = decimal.Parse(dr[23].ToString()).ToString("F2");//医疗救助基金支付（顺德区）
                this.fpSpread_YJBB.Cells[rowindex, 24].Text = decimal.Parse(dr[24].ToString()).ToString("F2");//医疗救助基金支付（三水区）
                this.fpSpread_YJBB.Cells[rowindex, 25].Text = decimal.Parse(dr[25].ToString()).ToString("F2");//医疗救助基金支付（高明区）
                this.fpSpread_YJBB.Cells[rowindex, 26].Text = decimal.Parse(dr[26].ToString()).ToString("F2");//优抚医疗补助
                this.fpSpread_YJBB.Cells[rowindex, 27].Text = decimal.Parse(dr[27].ToString()).ToString("F2");//个人支付
                rowindex++;
                if (!string.IsNullOrEmpty(dr[28].ToString())) {
                    fund_pay_sumamt += decimal.Parse(dr[28].ToString());
                }
            }
            //this.fpSpread_YJBB.Cells["fund_pay_sumamt"].Text = "基金支付总额（" + fund_pay_sumamt.ToString("F2") + "）";
            //this.fpSpread_YJBB.ColumnHeader.Cells[2, 5].Text = "基金支付总额（" + fund_pay_sumamt.ToString("F2") + "）";

            //合计
            this.fpSpread_YJBB.Rows.Add(this.fpSpread_YJBB.RowCount, 1);
            this.fpSpread_YJBB.Cells[rowindex, 2].Text = "合计";
            for (int i = 3; i <= 27; i++)
            {
                decimal sum = 0;
                for (int j = 0; j < this.fpSpread_YJBB.Rows.Count - 1; j++)
                {
                    sum += decimal.Parse(this.fpSpread_YJBB.Cells[j, i].Text);
                }

                if (i == 3 || i == 4)
                {
                    this.fpSpread_YJBB.Cells[rowindex, i].Text = sum.ToString();
                }
                else
                {
                    this.fpSpread_YJBB.Cells[rowindex, i].Text = decimal.Parse(sum.ToString()).ToString("F2");
                }
            }

            //表尾
            rowindex++;
            this.fpSpread_YJBB.Rows.Add(this.fpSpread_YJBB.RowCount, 1);
            this.fpSpread_YJBB.Cells[rowindex, 6].Text = "医疗机构经办人：";
            this.fpSpread_YJBB.Cells[rowindex, 8].Text = "医疗机构复核人：";
            this.fpSpread_YJBB.Cells[rowindex, 11].Text = "医疗机构审核人：";
            this.fpSpread_YJBB.Cells[rowindex, 14].Text = "驻院医管员签名：";

            if (clr_type_lv2 != "SYMZ")
            {
                this.fpSpread_YJBB.Columns[3].Visible = false;
            }
            else {
                this.fpSpread_YJBB.Columns[3].Visible = true;
            }
            #endregion

            #region 获取明细
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在汇总报表明细信息!请稍后!");
            System.Data.DataTable dt_MX = this.LocalManager.getLogarithmInfo_MX(insutype, clr_type, clr_type_lv2,
                     this.dtBeginTime.Value.ToString("yyyy-MM-dd"), this.dtEndTime.Value.AddDays(1).ToShortDateString());

            #region 明细头
            this.fpSpread_YJBBMX.ColumnHeader.Cells[0, 0].Text = "佛山市基本医疗保险医疗费用现场结算月结明细表(" + this.ncbMBReportType.Text + ")";
            this.fpSpread_YJBBMX.ColumnHeader.Cells[1, 11].Text = "清算年月：" + this.dtBeginTime.Value.Date.ToString("yyyyMM") + "                                打印日期：" + System.DateTime.Now.Date.ToString("yyyy -MM-dd");
            #endregion'

            #region 明细
            if (dt_MX == null || dt_MX.Rows.Count <= 0)
            {
                MessageBox.Show("生成报表明细失败：没有获取到相关数据！");
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return;
            }
            this.fpSpread_YJBBMX.RowCount = 0;
            foreach (DataRow dr in dt_MX.Rows)
            {
                this.fpSpread_YJBBMX.Rows.Add(this.fpSpread_YJBBMX.RowCount, 1);
                int row = this.fpSpread_YJBBMX.RowCount - 1;
                this.fpSpread_YJBBMX.Cells[row, 0].Text = (row+1).ToString();
                this.fpSpread_YJBBMX.Cells[row, 1].Text = dr[0].ToString();//this.constMgr.GetConstant("ProvinceDist", dr[0].ToString()).Name;//参保人所在区
                this.fpSpread_YJBBMX.Cells[row, 2].Text = dr[1].ToString();//身份证
                this.fpSpread_YJBBMX.Cells[row, 3].Text = dr[2].ToString();//姓名
                this.fpSpread_YJBBMX.Cells[row, 4].Text = dr[3].ToString();//就诊id
                this.fpSpread_YJBBMX.Cells[row, 5].Text = dr[4].ToString();//结算id
                this.fpSpread_YJBBMX.Cells[row, 6].Text = dr[6].ToString();//就诊开始时间
                this.fpSpread_YJBBMX.Cells[row, 7].Text = dr[7].ToString();//就诊结束时间
                this.fpSpread_YJBBMX.Cells[row, 8].Text = dr[5].ToString();//结算时间（经办时间）
                this.fpSpread_YJBBMX.Cells[row, 9].Text = this.constMgr.GetConstant("GZSI_med_type", dr[8].ToString()).Name;//业务类别
                this.fpSpread_YJBBMX.Cells[row, 10].Text = this.constMgr.GetConstant("GZSI_clr_way", dr[9].ToString()).Name;//清算方式
                this.fpSpread_YJBBMX.Cells[row, 11].Text = this.constMgr.GetConstant("GZSI_clr_type", dr[10].ToString()).Name;//清算类别
                this.fpSpread_YJBBMX.Cells[row, 12].Text = this.constMgr.GetConstant("GZSI_clr_type_lv2", dr[11].ToString()).Name;//二级清算类别
                this.fpSpread_YJBBMX.Cells[row, 13].Text = this.constMgr.GetConstant("GZSI_birctrl_type", dr[12].ToString()).Name;//生育类别
                this.fpSpread_YJBBMX.Cells[row, 14].Text = decimal.Parse(string.IsNullOrEmpty(dr[13].ToString()) ? "0" : dr[13].ToString()).ToString("F2");//医疗总费用
                this.fpSpread_YJBBMX.Cells[row, 15].Text = decimal.Parse(string.IsNullOrEmpty(dr[14].ToString()) ? "0" : dr[14].ToString()).ToString("F2");//基本医疗保险统筹基金支付
                this.fpSpread_YJBBMX.Cells[row, 16].Text = decimal.Parse(string.IsNullOrEmpty(dr[15].ToString()) ? "0" : dr[15].ToString()).ToString("F2");//大病基金支付
                this.fpSpread_YJBBMX.Cells[row, 17].Text = decimal.Parse(string.IsNullOrEmpty(dr[16].ToString()) ? "0" : dr[16].ToString()).ToString("F2");//公务员基金支付
                this.fpSpread_YJBBMX.Cells[row, 18].Text = decimal.Parse(string.IsNullOrEmpty(dr[17].ToString()) ? "0" : dr[17].ToString()).ToString("F2");//离休医疗保障
                this.fpSpread_YJBBMX.Cells[row, 19].Text = decimal.Parse(string.IsNullOrEmpty(dr[18].ToString()) ? "0" : dr[18].ToString()).ToString("F2");//医疗救助基金支付
                this.fpSpread_YJBBMX.Cells[row, 20].Text = decimal.Parse(string.IsNullOrEmpty(dr[19].ToString()) ? "0" : dr[19].ToString()).ToString("F2");//优抚医疗补助
                this.fpSpread_YJBBMX.Cells[row, 21].Text = decimal.Parse(string.IsNullOrEmpty(dr[20].ToString()) ? "0" : dr[20].ToString()).ToString("F2");//补充医疗保险基金支付
                this.fpSpread_YJBBMX.Cells[row, 22].Text = decimal.Parse(string.IsNullOrEmpty(dr[21].ToString()) ? "0" : dr[21].ToString()).ToString("F2");//基金支付总额
                this.fpSpread_YJBBMX.Cells[row, 23].Text = decimal.Parse(string.IsNullOrEmpty(dr[22].ToString()) ? "0" : dr[22].ToString()).ToString("F2");//个人支付
                this.fpSpread_YJBBMX.Cells[row, 24].Text = dr[23].ToString();//医疗救助所在区
            }

            //合计
            this.fpSpread_YJBBMX.Rows.Add(this.fpSpread_YJBBMX.RowCount, 1);
            rowindex = this.fpSpread_YJBBMX.RowCount - 1;
            if (!clr_type_lv2.StartsWith("SY"))
            {
                this.fpSpread_YJBBMX.Cells[rowindex, 12].Text = "合计";
            }
            else
            {
                this.fpSpread_YJBBMX.Cells[rowindex, 13].Text = "合计";
            }

            for (int i = 14; i <= 23; i++)
            {
                decimal sum = 0;
                for (int j = 0; j < this.fpSpread_YJBBMX.Rows.Count - 1; j++)
                {
                    sum += decimal.Parse(this.fpSpread_YJBBMX.Cells[j, i].Text);
                }

                this.fpSpread_YJBBMX.Cells[rowindex, i].Text = decimal.Parse(sum.ToString()).ToString("F2");
            }

            #endregion

            #region 明细尾
            this.fpSpread_YJBBMX.Rows.Add(this.fpSpread_YJBBMX.RowCount, 1);
            rowindex = this.fpSpread_YJBBMX.RowCount - 1;
            this.fpSpread_YJBBMX.Cells[rowindex, 5].Text = "医疗机构经办人：";
            this.fpSpread_YJBBMX.Cells[rowindex, 7].Text = "医疗机构复核人：";
            this.fpSpread_YJBBMX.Cells[rowindex, 10].Text = "医疗机构审核人：";
            this.fpSpread_YJBBMX.Cells[rowindex, 13].Text = "驻院医管员签名：";
            #endregion


            if (!clr_type_lv2.StartsWith("SY"))
            {
                this.fpSpread_YJBBMX.Columns[13].Visible = false;
            }
            else
            {
                this.fpSpread_YJBBMX.Columns[13].Visible = true;
            }

            #endregion
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        //导出
        private void nbExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "月结报表_" + this.ncbMBReportType.Text + "(" + this.dtBeginTime.Value.Date.ToString("yyyyMMdd") +
                                            "_" + this.dtEndTime.Value.Date.ToString("yyyyMMdd") + ")";/// this.neuSpread3.Sheets[0].SheetName;
            dlg.Filter = "(*.xls)|*.xls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.neuSpread3.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                }
                catch
                {
                    this.neuSpread3.SaveExcel(dlg.FileName);
                }
            }
        }

        //快速查询异常数据
        private void nbQueryUnusual_Click(object sender, EventArgs e)
        {

            //先查询医保数据
            this.nbQueryYB_Click(sender, e);
            List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo> setlinfoT
                = new List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在核对异常结果!请稍后!");
            this.fpSpread_YCJS.RowCount = 0;

            #region //院内多
            for (int i = 0; i < this.fpSpread_YND.RowCount; i++)
            {
                //for (int j = 0; j < this.fpSpread_YBMX.RowCount; j++)
                //{
                //    if (this.fpSpread_YND.Cells[i, 4].Text == this.fpSpread_YBMX.Cells[j, 1].Text) {
                //        this.fpSpread_YBMX.Rows[j].ForeColor = Color.Red;
                //        break;
                //    }
                //}

                foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
                {
                    if (this.fpSpread_YND.Cells[i, 4].Text == sinfo.setl_id)
                    {
                        setlinfoT.Add(sinfo);
                        break;
                    }
                }
            }
            #endregion
            #region //中心多
            for (int i = 0; i < this.fpSpread_ZXD.RowCount; i++)
            {
                //for (int j = 0; j < this.fpSpread_YBMX.RowCount; j++)
                //{
                //    if (this.fpSpread_ZXD.Cells[i, 2].Text == this.fpSpread_YBMX.Cells[j, 1].Text)
                //    {
                //        this.fpSpread_YBMX.Rows[j].ForeColor = Color.Red;
                //        break;
                //    }
                //}

                foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
                {
                    if (this.fpSpread_ZXD.Cells[i, 2].Text == sinfo.setl_id)
                    {
                        setlinfoT.Add(sinfo);
                        break;
                    }
                }
            }
            #endregion
            #region  //金额不一致
            for (int i = 0; i < this.fpSpread_JEBYZ.RowCount; i++)
            {
                //for (int j = 0; j < this.fpSpread_YBMX.RowCount; j++)
                //{
                //    if (this.fpSpread_JEBYZ.Cells[i, 4].Text == this.fpSpread_YBMX.Cells[j, 1].Text)
                //    {
                //        this.fpSpread_YBMX.Rows[j].ForeColor = Color.Red;
                //        break;
                //    }
                //}

                foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
                {
                    if (this.fpSpread_JEBYZ.Cells[i, 4].Text == sinfo.setl_id)
                    {
                        setlinfoT.Add(sinfo);
                        break;
                    }
                }
            }
            #endregion

            if (setlinfoT != null || setlinfoT.Count > 0)
            {
                Common.Function.ShowOutDateToFarpoint<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>(this.fpSpread_YCJS, setlinfoT);
            }
            else
            {
                MessageBox.Show("未检索到异常数据!");
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void neuSpread8_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //获取行号
            int rownum = e.Row;
            if (rownum < 0)
            {
                return;
            }
            //获取流水号
            string psn_no = this.fpSpread_YCJS.Cells[rownum, 3].Text;//个人编号
            string mdtrt_id = this.fpSpread_YCJS.Cells[rownum, 2].Text;//医保就诊ID
            string setl_id = this.fpSpread_YCJS.Cells[rownum, 1].Text;//医保结算ID
            string med_type = this.fpSpread_YCJS.Cells[rownum, 25].Text;//业务类型
            string clr_type = this.fpSpread_YCJS.Cells[rownum, 26].Text;//清算类型

            frmDZTool frmDZTool = new frmDZTool();
            frmDZTool.setData(psn_no, mdtrt_id, setl_id, med_type, clr_type);
            frmDZTool.ShowDialog(this);

        }

        private void nbSaveInfo5203_Click(object sender, EventArgs e)
        {
            if (setlinfo == null || setlinfo.Count <= 0)
            {
                MessageBox.Show("没有数据！");
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存数据!请稍后!");
            foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
            {
                //过滤零报数据
                if (sinfo.pay_loc == "1")
                {
                    continue;
                }

                //存在异地的数据
                if (!sinfo.insu_optins.StartsWith("4406"))
                {
                    continue;
                }

                //退费
                if (sinfo.refd_setl_flag == "1")
                {
                    continue;
                }

                #region //保存5203的数据
                Patient5203 patient5203 = new Patient5203();
                Models.Request.RequestGzsiModel5203 RequestGzsiModel5203 = new Models.Request.RequestGzsiModel5203();
                Models.Response.ResponseGzsiModel5203 ResponseGzsiModel5203 = new Models.Response.ResponseGzsiModel5203();
                Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
                data5203.psn_no = sinfo.psn_no;
                data5203.setl_id = sinfo.setl_id;
                data5203.mdtrt_id = sinfo.mdtrt_id;

                RequestGzsiModel5203.data = data5203;
                if (patient5203.CallService(RequestGzsiModel5203, ref ResponseGzsiModel5203) < 0)
                {
                    continue;
                }
                if (ResponseGzsiModel5203.infcode != "0")
                {
                    continue;
                }
                if (ResponseGzsiModel5203.output == null)
                {
                    continue;
                }
                if (ResponseGzsiModel5203.output.setlinfo == null)
                {
                    continue;
                }
                if (this.LocalManager.ExisteSetlinfo5203(sinfo.mdtrt_id, sinfo.setl_id) != "1")
                {
                    this.LocalManager.insertSetlinfo5203(ResponseGzsiModel5203.output.setlinfo);
                }

                #endregion
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void nbExportMX_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "月结报表明细_" + this.ncbMBReportType.Text + "(" + this.dtBeginTime.Value.Date.ToString("yyyyMMdd") +
                                            "_" + this.dtEndTime.Value.Date.ToString("yyyyMMdd") + ")";/// this.neuSpread3.Sheets[0].SheetName;
            dlg.Filter = "(*.xls)|*.xls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.neuSpread9.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                }
                catch
                {
                    this.neuSpread9.SaveExcel(dlg.FileName);
                }
            }
        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            //获取行号
            int rownum = e.Row;
            if (rownum < 0)
            {
                return;
            }
            //获取流水号
            string psn_no = this.fpSpread_YBMX.Cells[rownum, 3].Text;//个人编号
            string mdtrt_id = this.fpSpread_YBMX.Cells[rownum, 2].Text;//医保就诊ID
            string setl_id = this.fpSpread_YBMX.Cells[rownum, 1].Text;//医保结算ID
            string med_type = this.fpSpread_YBMX.Cells[rownum, 25].Text;//业务类型
            string clr_type = this.fpSpread_YBMX.Cells[rownum, 26].Text;//清算类型

            frmDZTool frmDZTool = new frmDZTool();
            frmDZTool.setData(psn_no, mdtrt_id, setl_id, med_type, clr_type);
            frmDZTool.ShowDialog(this);
        }

        private void nbQueryLX_Click(object sender, EventArgs e)
        {
            if (setlinfo == null || setlinfo.Count <= 0)
            {
                MessageBox.Show("没有获取医保中心数据！");
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("查询中!请稍后!");
            List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo> setlinfoT
                = new List<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>();

            foreach (Models.Response.ResponseGzsiModel90502.Output.Setlinfo sinfo in setlinfo)
            {
                if (sinfo.pay_loc == "1")
                {
                    setlinfoT.Add(sinfo);
                }
            }

            if (setlinfoT != null && setlinfoT.Count > 0)
            {
                Common.Function.ShowOutDateToFarpoint<Models.Response.ResponseGzsiModel90502.Output.Setlinfo>(this.fpSpread_YBMX, setlinfoT);
                #region //颜色区分
                for (int i = 0; i < this.fpSpread_YBMX.RowCount; i++)
                {
                    //取消结算
                    if (this.fpSpread_YBMX.Cells[i, 46].Text == "1")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Red;
                        continue;
                    }

                    //零星
                    if (this.fpSpread_YBMX.Cells[i, 49].Text != "2")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Orange;
                        continue;
                    }

                    //异地
                    if (!this.fpSpread_YBMX.Cells[i, 16].Text.StartsWith("4406"))
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Blue;
                        continue;
                    }

                    //以对账
                    if (this.fpSpread_YBMX.Cells[i, 45].Text == "1")
                    {
                        this.fpSpread_YBMX.Rows[i].ForeColor = Color.Green;
                        continue;
                    }
                }
                #endregion
            }
            else
            {
                MessageBox.Show("没有查询到相关数据！");
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void nbExportMX_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "月结报表明细_" + this.ncbMBReportType.Text + "(" + this.dtBeginTime.Value.Date.ToString("yyyyMMdd") +
                                            "_" + this.dtEndTime.Value.Date.ToString("yyyyMMdd") + ")";/// this.neuSpread3.Sheets[0].SheetName;
            dlg.Filter = "(*.xls)|*.xls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.neuSpread9.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                }
                catch
                {
                    this.neuSpread9.SaveExcel(dlg.FileName);
                }
            }

            //重新读取文件绘制边框
        }

        private void neuSpread5_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //获取行号
            int rownum = e.Row;
            if (rownum < 0)
            {
                return;
            }
            //获取流水号
            string psn_no = this.fpSpread_ZXD.Cells[rownum, 0].Text.Trim();//个人编号
            string mdtrt_id = this.fpSpread_ZXD.Cells[rownum, 1].Text.Trim();//医保就诊ID
            string setl_id = this.fpSpread_ZXD.Cells[rownum, 2].Text.Trim();//医保结算ID
            string med_type = this.cmbClrType.Tag.ToString();//业务类型
            if (med_type == "9902" || med_type == "9903")
            {
                med_type = med_type == "9902" ? "21" : "11";
            }

            string clr_type =this.cmbClrType.Tag.ToString();//清算类型
            if (clr_type == "51" || clr_type == "52")
            {
                clr_type = clr_type == "52" ? "21" : "11";
            }
            else if (clr_type == "9902" || clr_type == "9903")
            {
                clr_type = clr_type == "9902" ? "21" : "11";
            }

            frmDZTool frmDZTool = new frmDZTool();
            frmDZTool.setData(psn_no, mdtrt_id, setl_id, med_type, clr_type);
            frmDZTool.ShowDialog(this);
        }

        private void neuSpread6_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //获取行号
            int rownum = e.Row;
            if (rownum < 0)
            {
                return;
            }
            //获取流水号
            string psn_no = this.fpSpread_YND.Cells[rownum, 5].Text.Trim();//个人编号
            string mdtrt_id = this.fpSpread_YND.Cells[rownum, 3].Text.Trim();//医保就诊ID
            string setl_id = this.fpSpread_YND.Cells[rownum, 4].Text.Trim();//医保结算ID
            string med_type = this.cmbClrType.Tag.ToString();//业务类型
            if (med_type == "9902" || med_type == "9903")
            {
                med_type = med_type == "9902" ? "21" : "11";
            }

            string clr_type = this.cmbClrType.Tag.ToString();//清算类型
            if (clr_type == "51" || clr_type == "52")
            {
                clr_type = clr_type == "52" ? "21" : "11";
            }
            else if (clr_type == "9902" || clr_type == "9903")
            {
                clr_type = clr_type == "9902" ? "21" : "11";
            }

            frmDZTool frmDZTool = new frmDZTool();
            frmDZTool.setData(psn_no, mdtrt_id, setl_id, med_type, clr_type);
            frmDZTool.ShowDialog(this);
        }

        private void neuSpread7_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //获取行号
            int rownum = e.Row;
            if (rownum < 0)
            {
                return;
            }
            //获取流水号
            string psn_no = this.fpSpread_JEBYZ.Cells[rownum, 5].Text.Trim();//个人编号
            string mdtrt_id = this.fpSpread_JEBYZ.Cells[rownum, 3].Text.Trim();//医保就诊ID
            string setl_id = this.fpSpread_JEBYZ.Cells[rownum, 4].Text.Trim();//医保结算ID
            string med_type = this.cmbClrType.Tag.ToString();//业务类型
            if (med_type == "9902" || med_type == "9903")
            {
                med_type = med_type == "9902" ? "21" : "11";
            }

            string clr_type = this.cmbClrType.Tag.ToString();//清算类型
            if (clr_type == "51" || clr_type == "52")
            {
                clr_type = clr_type == "52" ? "21" : "11";
            }
            else if (clr_type == "9902" || clr_type == "9903")
            {
                clr_type = clr_type == "9902" ? "21" : "11";
            }

            frmDZTool frmDZTool = new frmDZTool();
            frmDZTool.setData(psn_no, mdtrt_id, setl_id, med_type, clr_type);
            frmDZTool.ShowDialog(this);
        }
    }
}
