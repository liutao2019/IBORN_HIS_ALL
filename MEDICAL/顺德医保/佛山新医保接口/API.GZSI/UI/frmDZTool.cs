using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using API.GZSI.Business;
using API.GZSI.Models.Response;

namespace API.GZSI.UI
{
    public partial class frmDZTool : Form
    {
        public frmDZTool()
        {
            InitializeComponent();
            Init();
        }

        #region 变量
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        LocalManager LocalManager = new LocalManager();

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        private FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        #endregion

        private void Init()
        {
            //清算类别
            this.cmbClrType.AddItems(constMgr.GetAllList("GZSI_clr_type"));
            //设置默认值
            this.cmbClrType.Tag = "11";

            //清算类别
            this.ncbMed_type.AddItems(constMgr.GetAllList("GZSI_med_type"));
            //设置默认值
            this.cmbClrType.Tag = "11";
        }

        //设置基本信息
        public void setData(string psn_no, string mdtrt_id, string setl_id, string med_type, string clr_type)
        {
            this.tbMdtrt_id.Text = mdtrt_id;
            this.tbSetl_id.Text = setl_id;
            this.tbPsn_no.Text = psn_no;
            this.ncbMed_type.Tag = med_type;
            this.cmbClrType.Tag = clr_type;
            this.QueryInfo(this.tbPsn_no.Text, this.tbMdtrt_id.Text, this.tbSetl_id.Text, this.ncbMed_type.Tag.ToString(), this.cmbClrType.Tag.ToString());
        }

        //查询
        public void QueryInfo(string psn_no, string mdtrt_id, string setl_id, string med_type,string clr_type)
        {
            if (string.IsNullOrEmpty(psn_no))
            {
                MessageBox.Show("患者个人编号不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(mdtrt_id))
            {
                MessageBox.Show("患者医保就诊ID不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(setl_id))
            {
                MessageBox.Show("患者医保结算ID不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(med_type))
            {
                MessageBox.Show("患者医疗类别不能为空！");
                return;
            }


            this.fpSpread_5201.RowCount = 0;
            this.fpSpread_5203.RowCount = 0;
            this.fpSpread_5204.RowCount = 0;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据!请稍后!");
            #region 5201
            Patient5201 patient5201 = new Patient5201();
            Models.Request.RequestGzsiModel5201 RequestGzsiModel5201 = new Models.Request.RequestGzsiModel5201();
            Models.Response.ResponseGzsiModel5201 ResponseGzsiModel5201 = new Models.Response.ResponseGzsiModel5201();
            Models.Request.RequestGzsiModel5201.Data data5201 = new Models.Request.RequestGzsiModel5201.Data();
            data5201.psn_no = psn_no;
            data5201.begntime = "2021-5-1";
            data5201.endtime = System.DateTime.Now.ToShortDateString();
            data5201.med_type = med_type;
            data5201.mdtrt_id = mdtrt_id;

            RequestGzsiModel5201.data = data5201;
            if (patient5201.CallService(RequestGzsiModel5201, ref ResponseGzsiModel5201) < 0)
            {
                MessageBox.Show("人员结算信息查询失败：" + patient5201.ErrorMsg);
            }
            if (ResponseGzsiModel5201.infcode != "0")
            {
                MessageBox.Show("人员结算信息查询失败：" + patient5201.ErrorMsg);
            }

            if (ResponseGzsiModel5201.output.mdtrtinfo != null && ResponseGzsiModel5201.output.mdtrtinfo.Count > 0)
            {
                Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5201.Output.Mdtrtinfo>(this.fpSpread_5201, ResponseGzsiModel5201.output.mdtrtinfo);
            }

            #endregion

            #region 5203
            Patient5203 patient5203 = new Patient5203();
            Models.Request.RequestGzsiModel5203 RequestGzsiModel5203 = new Models.Request.RequestGzsiModel5203();
            Models.Response.ResponseGzsiModel5203 ResponseGzsiModel5203 = new Models.Response.ResponseGzsiModel5203();
            Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
            data5203.psn_no = psn_no;
            data5203.setl_id = setl_id;
            data5203.mdtrt_id = mdtrt_id;

            RequestGzsiModel5203.data = data5203;
            if (patient5203.CallService(RequestGzsiModel5203, ref ResponseGzsiModel5203) < 0)
            {
                MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
            }
            if (ResponseGzsiModel5203.infcode != "0")
            {
                MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
            }

            if (ResponseGzsiModel5203.output != null && ResponseGzsiModel5203.output.setlinfo != null)
            {
                List<Models.Response.ResponseGzsiModel5203.Output.SetlInfo> SetlinfoList = new List<ResponseGzsiModel5203.Output.SetlInfo>();
                SetlinfoList.Add(ResponseGzsiModel5203.output.setlinfo);
                if (SetlinfoList != null && SetlinfoList.Count > 0)
                {
                    Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5203.Output.SetlInfo>(this.fpSpread_5203, SetlinfoList);
                }
            }

            #endregion

            #region 5204
            Patient5204 patient5204 = new Patient5204();
            Models.Request.RequestGzsiModel5204 RequestGzsiModel5204 = new Models.Request.RequestGzsiModel5204();
            Models.Response.ResponseGzsiModel5204 ResponseGzsiModel5204 = new Models.Response.ResponseGzsiModel5204();
            Models.Request.RequestGzsiModel5204.Data data5204 = new Models.Request.RequestGzsiModel5204.Data();
            data5204.psn_no = psn_no;
            data5204.setl_id = setl_id;
            data5204.mdtrt_id = mdtrt_id;

            RequestGzsiModel5204.data = data5204;
            if (patient5204.CallService(RequestGzsiModel5204, ref ResponseGzsiModel5204) < 0)
            {
                MessageBox.Show("费用明细查询失败：" + patient5203.ErrorMsg);
            }
            if (ResponseGzsiModel5204.infcode != "0")
            {
                MessageBox.Show("费用明细查询失败：" + patient5203.ErrorMsg);
            }

            if (ResponseGzsiModel5204.output != null && ResponseGzsiModel5204.output.Count > 0)
            {
                Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5204.Output>(this.fpSpread_5204, ResponseGzsiModel5204.output.OrderBy(m => m.hilist_name).ToList());
            }
            #endregion


            this.fpSpread_SI.RowCount = 0;
            this.fpSpread_FPXX.RowCount = 0;

            #region //本地数据
            string certno = string.Empty;
            string patientNO = string.Empty;
            string setl_time = string.Empty;

            if (ResponseGzsiModel5201.output != null && ResponseGzsiModel5201.output.mdtrtinfo != null && ResponseGzsiModel5201.output.mdtrtinfo.Count > 0) {
                patientNO = ResponseGzsiModel5201.output.mdtrtinfo[0].ipt_otp_no;
            }

            if (ResponseGzsiModel5203.output != null && ResponseGzsiModel5203.output.setlinfo != null)
            {
                 certno = ResponseGzsiModel5203.output.setlinfo.certno;
                setl_time = ResponseGzsiModel5203.output.setlinfo.setl_time;
            }

            //门诊
            if (clr_type == "11")
            {
                ArrayList inPatientNos = new ArrayList();
                ArrayList invoiceNos = new ArrayList();

                #region 本地医保信息
                //查询条件：1.就诊ID相同 2.就算ID相同 3.证件号相同，结算为同一天
                DataTable dt_SI =  this.LocalManager.QuerySIdetail(mdtrt_id, setl_id, certno, setl_time);
                if (dt_SI != null && dt_SI.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_SI.Rows)
                    { 
                        this.fpSpread_SI.Rows.Add(this.fpSpread_SI.RowCount, 1);
                        int row = this.fpSpread_SI.RowCount - 1;
                        this.fpSpread_SI.Rows[row].Tag = dr;
                        this.fpSpread_SI.Cells[row, 0].Text = dr[0].ToString();//备注
                        this.fpSpread_SI.Cells[row, 1].Text = dr[1].ToString();//就诊id
                        this.fpSpread_SI.Cells[row, 2].Text = dr[2].ToString();//结算id
                        this.fpSpread_SI.Cells[row, 3].Text = dr[3].ToString();//人员编号
                        this.fpSpread_SI.Cells[row, 4].Text = dr[4].ToString();//1 有效 0 作废 
                        this.fpSpread_SI.Cells[row, 5].Text = dr[5].ToString();//1 有效 0 作废 
                        this.fpSpread_SI.Cells[row, 6].Text = dr[6].ToString();//住院流水号
                        this.fpSpread_SI.Cells[row, 7].Text = dr[7].ToString();//发票号
                        this.fpSpread_SI.Cells[row, 8].Text = dr[8].ToString();//住院号
                        this.fpSpread_SI.Cells[row, 9].Text = dr[9].ToString();//就诊卡号
                        this.fpSpread_SI.Cells[row, 10].Text = dr[10].ToString();//姓名
                        this.fpSpread_SI.Cells[row, 11].Text = dr[11].ToString();//性别
                        this.fpSpread_SI.Cells[row, 12].Text = dr[12].ToString();//身份证号
                        this.fpSpread_SI.Cells[row, 13].Text = dr[13].ToString();//结算分类1-门诊2-住院 
                        this.fpSpread_SI.Cells[row, 14].Text = dr[14].ToString();//结算时间
                        this.fpSpread_SI.Cells[row, 15].Text = dr[15].ToString();//医疗类别
                        this.fpSpread_SI.Cells[row, 16].Text = dr[16].ToString();//医疗费总额
                        this.fpSpread_SI.Cells[row, 17].Text = dr[17].ToString();//基金支付总额
                        //this.fpSpread_SI.Cells[row, 18].Text = dr[18].ToString();//个人负担总金额
                        inPatientNos.Add(dr[6].ToString());
                        invoiceNos.Add(dr[7].ToString());
                    }
                }
                else { 
                    //没有医保相关信息，可以继续查发票，不过只能根据，证件号和日期
                    //有医保相关信息，可以加上流水号的记录
                }
                #endregion

                #region 查询发票信息
                DataTable dt_FP = this.LocalManager.QueryFPdetail(inPatientNos, invoiceNos,certno,setl_time,"1", patientNO);
                if (dt_FP != null && dt_FP.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_FP.Rows)
                    {
                        this.fpSpread_FPXX.Rows.Add(this.fpSpread_FPXX.RowCount, 1);
                        int row = this.fpSpread_FPXX.RowCount - 1;
                        this.fpSpread_FPXX.Rows[row].Tag = dr;
                        this.fpSpread_FPXX.Cells[row, 0].Text = dr[0].ToString();//发票号
                        this.fpSpread_FPXX.Cells[row, 1].Text = dr[1].ToString();//病历卡号
                        this.fpSpread_FPXX.Cells[row, 2].Text = dr[2].ToString();//挂号流水号
                        this.fpSpread_FPXX.Cells[row, 3].Text = dr[3].ToString();//患者姓名
                        this.fpSpread_FPXX.Cells[row, 4].Text = dr[4].ToString();//合同单位代码
                        this.fpSpread_FPXX.Cells[row, 5].Text = dr[5].ToString();//合同单位名称
                        this.fpSpread_FPXX.Cells[row, 6].Text = dr[6].ToString();//挂号日期
                        this.fpSpread_FPXX.Cells[row, 7].Text = dr[7].ToString();//总额
                        this.fpSpread_FPXX.Cells[row, 8].Text = dr[8].ToString();//可报效金额
                        this.fpSpread_FPXX.Cells[row, 9].Text = dr[9].ToString();//不可报效金额
                        this.fpSpread_FPXX.Cells[row, 10].Text = dr[10].ToString();//自付金额
                        this.fpSpread_FPXX.Cells[row, 11].Text = dr[11].ToString();//结算人
                        this.fpSpread_FPXX.Cells[row, 12].Text = dr[12].ToString();//结算时间
                        this.fpSpread_FPXX.Cells[row, 13].Text = dr[13].ToString() == "1"? "有效":"退费";//"0" 退费 "1" 有效 "2" 重打 "3" 注销 
                    }
                }
                #endregion
            }
            //住院
            else {
                ArrayList inPatientNos = new ArrayList();
                ArrayList invoiceNos = new ArrayList();

                #region 本地医保信息
                //查询条件：1.就诊ID相同 2.就算ID相同 3.证件号相同，结算为同一天
                DataTable dt_SI = this.LocalManager.QuerySIdetail(mdtrt_id, setl_id, certno, setl_time);
                if (dt_SI != null && dt_SI.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_SI.Rows)
                    {
                        this.fpSpread_SI.Rows.Add(this.fpSpread_SI.RowCount, 1);
                        int row = this.fpSpread_SI.RowCount - 1;
                        this.fpSpread_SI.Rows[row].Tag = dr;
                        this.fpSpread_SI.Cells[row, 0].Text = dr[0].ToString();//备注
                        this.fpSpread_SI.Cells[row, 1].Text = dr[1].ToString();//就诊id
                        this.fpSpread_SI.Cells[row, 2].Text = dr[2].ToString();//结算id
                        this.fpSpread_SI.Cells[row, 3].Text = dr[3].ToString();//人员编号
                        this.fpSpread_SI.Cells[row, 4].Text = dr[4].ToString();//1 有效 0 作废 
                        this.fpSpread_SI.Cells[row, 5].Text = dr[5].ToString();//1 有效 0 作废 
                        this.fpSpread_SI.Cells[row, 6].Text = dr[6].ToString();//住院流水号
                        this.fpSpread_SI.Cells[row, 7].Text = dr[7].ToString();//发票号
                        this.fpSpread_SI.Cells[row, 8].Text = dr[8].ToString();//住院号
                        this.fpSpread_SI.Cells[row, 9].Text = dr[9].ToString();//就诊卡号
                        this.fpSpread_SI.Cells[row, 10].Text = dr[10].ToString();//姓名
                        this.fpSpread_SI.Cells[row, 11].Text = dr[11].ToString();//性别
                        this.fpSpread_SI.Cells[row, 12].Text = dr[12].ToString();//身份证号
                        this.fpSpread_SI.Cells[row, 13].Text = dr[13].ToString();//结算分类1-门诊2-住院 
                        this.fpSpread_SI.Cells[row, 14].Text = dr[14].ToString();//结算时间
                        this.fpSpread_SI.Cells[row, 15].Text = dr[15].ToString();//医疗类别
                        this.fpSpread_SI.Cells[row, 16].Text = dr[16].ToString();//医疗费总额
                        this.fpSpread_SI.Cells[row, 17].Text = dr[17].ToString();//基金支付总额
                        //this.fpSpread_SI.Cells[row, 18].Text = dr[18].ToString();//个人负担总金额
                        inPatientNos.Add(dr[6].ToString());
                        invoiceNos.Add(dr[7].ToString());
                    }
                }
                else
                {
                    //没有医保相关信息，可以继续查发票，不过只能根据，证件号和日期
                    //有医保相关信息，可以加上流水号的记录
                }
                #endregion

                #region 查询发票信息
                DataTable dt_FP = this.LocalManager.QueryFPdetail(inPatientNos, invoiceNos, certno, setl_time, "2", patientNO);
                if (dt_FP != null && dt_FP.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_FP.Rows)
                    {
                        this.fpSpread_FPXX.Rows.Add(this.fpSpread_FPXX.RowCount, 1);
                        int row = this.fpSpread_FPXX.RowCount - 1;
                        this.fpSpread_FPXX.Rows[row].Tag = dr;
                        this.fpSpread_FPXX.Cells[row, 0].Text = dr[0].ToString();//发票号
                        this.fpSpread_FPXX.Cells[row, 1].Text = dr[1].ToString();//病历卡号
                        this.fpSpread_FPXX.Cells[row, 2].Text = dr[2].ToString();//挂号流水号
                        this.fpSpread_FPXX.Cells[row, 3].Text = dr[3].ToString();//患者姓名
                        this.fpSpread_FPXX.Cells[row, 4].Text = dr[4].ToString();//合同单位代码
                        this.fpSpread_FPXX.Cells[row, 5].Text = dr[5].ToString();//合同单位名称
                        this.fpSpread_FPXX.Cells[row, 6].Text = dr[6].ToString();//挂号日期
                        this.fpSpread_FPXX.Cells[row, 7].Text = dr[7].ToString();//总额
                        this.fpSpread_FPXX.Cells[row, 8].Text = dr[8].ToString();//可报效金额
                        this.fpSpread_FPXX.Cells[row, 9].Text = dr[9].ToString();//不可报效金额
                        this.fpSpread_FPXX.Cells[row, 10].Text = dr[10].ToString();//自付金额
                        this.fpSpread_FPXX.Cells[row, 11].Text = dr[11].ToString();//结算人
                        this.fpSpread_FPXX.Cells[row, 12].Text = dr[12].ToString();//结算时间
                        this.fpSpread_FPXX.Cells[row, 13].Text = dr[13].ToString() == "1" ? "有效" : "退费";//"0" 退费 "1" 有效 "2" 重打 "3" 注销 
                    }
                }
                #endregion
            }


            #endregion
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        //取消医保结算
        private void bCancelYBJS_Click(object sender, EventArgs e)
        {
            //先判断本地是否有记录，若是有记录不允许直接操作
            //if (this.LocalManager.QuerySIInfo(this.tbMdtrt_id.Text, this.tbSetl_id.Text) == "1")
            //{
            //    MessageBox.Show("本地有相关结算记录，请联系信息科处理！");
            //    return;
            //}

            //门诊
            if (this.cmbClrType.Tag.ToString() == "11")
            {
                this.CancelBalanceOutpatient(this.tbPsn_no.Text, this.tbMdtrt_id.Text, this.tbSetl_id.Text);
            }
            //住院
            else
            {
                this.CancelBalanceInpatient(this.tbPsn_no.Text, this.tbMdtrt_id.Text, this.tbSetl_id.Text);
            }
        }

        //取消门诊结算
        public void CancelBalanceOutpatient(string psn_no, string mdtrt_id, string setl_id)
        {
            if (string.IsNullOrEmpty(psn_no))
            {
                MessageBox.Show("患者个人编号不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(mdtrt_id))
            {
                MessageBox.Show("患者医保就诊ID不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(setl_id))
            {
                MessageBox.Show("患者医保结算ID不能为空！");
                return;
            }


            #region 2208
            OutPatient2208 outPatient2208 = new OutPatient2208();
            Models.Request.RequestGzsiModel2208 requestGzsiModel2208 = new Models.Request.RequestGzsiModel2208();
            Models.Response.ResponseGzsiModel2208 responseGzsiModel2208 = new ResponseGzsiModel2208();
            Models.Request.RequestGzsiModel2208.Mdtrtinfo mdtrtinfo2208 = new Models.Request.RequestGzsiModel2208.Mdtrtinfo();

            mdtrtinfo2208.setl_id = setl_id; //结算ID
            mdtrtinfo2208.mdtrt_id = mdtrt_id; //就诊ID 
            mdtrtinfo2208.psn_no = psn_no; //人员编号
            requestGzsiModel2208.data = mdtrtinfo2208;

            if (outPatient2208.CallService(requestGzsiModel2208, ref responseGzsiModel2208) < 0)
            {
                MessageBox.Show("取消结算失败：" + outPatient2208.ErrorMsg);
                return;
            }
            if (responseGzsiModel2208.infcode != "0")
            {
                MessageBox.Show("取消结算失败：" + outPatient2208.ErrorMsg);
                return;
            }
            #endregion

            #region 2205
            //OutPatient2205 outPatient2205 = new OutPatient2205();
            //Models.Request.RequestGdsiModel2205 requestGdsiModel2205 = new Models.Request.RequestGdsiModel2205();
            //Models.Response.ResponseGdsiModel2205 responseGdsiModel2205 = new ResponseGdsiModel2205();
            //Models.Request.RequestGdsiModel2205.Data data2205 = new Models.Request.RequestGdsiModel2205.Data();
            //data2205.chrg_bchno = "0000";// "0000";//费用流水号 传入“0000”删除全部,不能撤销全部，医保接口BUG已经结算的还能撤销明细，吐了
            //data2205.mdtrt_id = mdtrt_id;//就诊ID
            //data2205.psn_no = psn_no;//人员编号
            //requestGdsiModel2205.data = data2205;
            //if (outPatient2205.CallService("", requestGdsiModel2205, ref responseGdsiModel2205) == -1)
            //{
            //    MessageBox.Show("门诊费用明细信息撤销失败：" + outPatient2205.ErrorMsg);
            //    return ;
            //}
            #endregion

            #region 2202
            //OutPatient2202 outPatient2202 = new OutPatient2202();
            //Models.Request.RequestGdsiModel2202 requestGzsiModel2202 = new API.GZSI.Models.Request.RequestGdsiModel2202();
            //Models.Response.ResponseGdsiModel2202 responseGzsiModel2202 = new API.GZSI.Models.Response.ResponseGdsiModel2202();
            //Models.Request.RequestGdsiModel2202.Data mdtrtinfo2202 = new API.GZSI.Models.Request.RequestGdsiModel2202.Data();

            //mdtrtinfo2202.mdtrt_id = mdtrt_id;//就诊ID 202011031615
            //mdtrtinfo2202.psn_no =psn_no;//人员编号 1000753288
            //mdtrtinfo2202.ipt_otp_no = "";//住院/门诊号  0000735959
            //requestGzsiModel2202.data = mdtrtinfo2202;

            //if (outPatient2202.CallService("",requestGzsiModel2202, ref responseGzsiModel2202) == -1)
            //{
            //    MessageBox.Show("门诊费用明细信息撤销失败：" + outPatient2202.ErrorMsg);
            //    return;
            //}
            #endregion

            MessageBox.Show("取消结算成功！");
        }

        //取消住院结算
        public void CancelBalanceInpatient(string psn_no, string mdtrt_id, string setl_id)
        {
            if (string.IsNullOrEmpty(psn_no))
            {
                MessageBox.Show("患者个人编号不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(mdtrt_id))
            {
                MessageBox.Show("患者医保就诊ID不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(setl_id))
            {
                MessageBox.Show("患者医保结算ID不能为空！");
                return;
            }


            #region 2305
            InPatient2305 inPatient2305 = new InPatient2305();
            Models.Request.RequestGzsiModel2305 requestGzsiModel2305 = new Models.Request.RequestGzsiModel2305();
            Models.Response.ResponseGzsiModel2305 responseGzsiModel2305 = new ResponseGzsiModel2305();

            requestGzsiModel2305.data.setl_id = setl_id;//结算ID
            requestGzsiModel2305.data.mdtrt_id = mdtrt_id;//就诊ID 
            requestGzsiModel2305.data.psn_no = psn_no;//人员编号

            if (inPatient2305.CallService(requestGzsiModel2305, ref responseGzsiModel2305) < 0)
            {
                MessageBox.Show("取消结算失败：" + inPatient2305.ErrorMsg);
                return;
            }
            if (responseGzsiModel2305.infcode != "0")
            {
                MessageBox.Show("取消结算失败：" + inPatient2305.ErrorMsg);
                return;
            }
            #endregion

            MessageBox.Show("取消结算成功！");
        }

        private void bQuery_Click(object sender, EventArgs e)
        {
            this.QueryInfo(this.tbPsn_no.Text, this.tbMdtrt_id.Text, this.tbSetl_id.Text, this.ncbMed_type.Tag.ToString(), this.cmbClrType.Tag.ToString());
        }

        private void bCancelBDYBJS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbPsn_no.Text))
            {
                MessageBox.Show("患者个人编号不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(this.tbMdtrt_id.Text))
            {
                MessageBox.Show("患者医保就诊ID不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(this.tbSetl_id.Text))
            {
                MessageBox.Show("患者医保结算ID不能为空！");
                return;
            }

            if(this.LocalManager.cancelSIinfo(this.tbMdtrt_id.Text,this.tbSetl_id.Text)<0){
                MessageBox.Show("作废失败！"+this.LocalManager.Err);
                return;
            }

            MessageBox.Show("作废成功！");
        }

        private void bInsertYBJS_Click(object sender, EventArgs e)
        {
            if (this.cmbClrType.Tag == null || string.IsNullOrEmpty(this.cmbClrType.Tag.ToString())) {
                MessageBox.Show("清算类型不能为空");
                return;
            }

            if (string.IsNullOrEmpty(this.tbInsertInvoiceNo.Text)) {
                MessageBox.Show("插入医保信息前，请填写相关的发票号！");
                return;
            }

            if (string.IsNullOrEmpty(this.tbPsn_no.Text))
            {
                MessageBox.Show("患者个人编号不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(this.tbMdtrt_id.Text))
            {
                MessageBox.Show("患者医保就诊ID不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(this.tbSetl_id.Text))
            {
                MessageBox.Show("患者医保结算ID不能为空！");
                return;
            }

            #region 门诊
            if (this.cmbClrType.Tag.ToString() == "11")
            {
                //根据发票号获取就诊信息
                string invoiceNo = this.tbInsertInvoiceNo.Text.PadLeft(12, '0');
                ArrayList comBalances = outpatientManager.QueryBalancesSameInvoiceCombNOByInvoiceNO(invoiceNo);
                if (comBalances == null || comBalances.Count <= 0)
                {
                    MessageBox.Show("没有查询发票信息！");
                    return;
                }

                #region 赋值
                FS.HISFC.Models.Fee.Outpatient.Balance balance = comBalances[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
                FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                //r.SIMainInfo.HosNo = Reader[0].ToString();//本级医疗机构编码
                r.ID = balance.Patient.ID;//住院流水号
                r.SIMainInfo.BalNo = "";//结算序号
                r.SIMainInfo.InvoiceNo = balance.Invoice.ID;//发票号
                //r.SIMainInfo.MedicalType.ID = Reader[4].ToString();//医疗类别
                //if (r.SIMainInfo.MedicalType.ID == "1")
                //    r.SIMainInfo.MedicalType.Name = "住院";
                //else
                //    r.SIMainInfo.MedicalType.Name = "门诊特定项目";
                //r.PID.PatientNO = Reader[5].ToString();//住院号
                r.PID.CardNO = balance.Patient.PID.CardNO;//就诊卡号
                //r.SSN = Reader[7].ToString();//医疗证号
                //r.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());//审批号
                //r.SIMainInfo.ProceatePcNo = Reader[9].ToString();//生育保险患者电脑号
                //r.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());//参保日期
                //r.SIMainInfo.SiState = Reader[11].ToString();//参保状态
                //r.Name = Reader[12].ToString();//姓名
                //r.Sex.ID = Reader[13].ToString();//性别
                //r.IDCard = Reader[14].ToString();//身份证号
                //r.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());//生日
                //r.SIMainInfo.EmplType = Reader[16].ToString();//人员类别 1 在职 2 退休
                //r.CompanyName = Reader[17].ToString();//工作单位
                r.SIMainInfo.InDiagnose.Name = "-";//门诊诊断
                r.PVisit.PatientLocation.Dept.ID = "-";//科室代码
                r.PVisit.PatientLocation.Dept.Name = "-";//科室名称
                r.DoctorInfo.Templet.Dept.ID = "-";//科室代码
                r.DoctorInfo.Templet.Dept.Name = "-";//科室名称
                r.Pact.PayKind.ID = "2";//结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                r.Pact.ID = balance.Patient.Pact.ID;//合同代码
                r.Pact.Name = balance.Patient.Pact.Name;//合同单位名称
                //r.PVisit.PatientLocation.Bed.ID = "";//床号
                //r.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());//入院日期
                //r.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());//入院日期
                //r.SIMainInfo.InDiagnose.ID = Reader[26].ToString();//入院诊断代码
                //r.SIMainInfo.InDiagnose.Name = Reader[27].ToString();//入院诊断名称
                //if (!Reader.IsDBNull(28))
                //r.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());//出院日期
                //r.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();//出院诊断代码
                //r.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();//出院诊断名称
                r.SIMainInfo.BalanceDate = balance.BalanceOper.OperTime;//结算日期(上次)
                r.SIMainInfo.TotCost = balance.FT.TotCost;//费用金额(未结)(住院总金额)
                r.SIMainInfo.PayCost = balance.FT.PayCost;//帐户支付
                r.SIMainInfo.PubCost = balance.FT.PubCost;//公费金额(未结)(社保支付金额)
                r.SIMainInfo.ItemPayCost = 0;//部分项目自付金额
                r.SIMainInfo.BaseCost = 0;//个人起付金额
                r.SIMainInfo.PubOwnCost = 0;//个人自费项目金额
                r.SIMainInfo.ItemYLCost = 0;//个人自付金额（乙类自付部分）
                r.SIMainInfo.OwnCost = balance.FT.OwnCost;//个人自负金额
                r.SIMainInfo.OverTakeOwnCost = 0;//超统筹支付限额个人自付金额

                r.SIMainInfo.Memo = "对账补充";//自费原因
                r.SIMainInfo.OperInfo.ID = "";//操作员
                r.SIMainInfo.OperDate = System.DateTime.Now;//操作日期
                r.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32("");//费用批次
                r.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal("");//医药机构分担金额
                r.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal("");//本年度可用定额
                r.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean("1");//1 有效 0 作废
                r.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean("1");//1 结算 0 未结算
                #endregion
                //获取医保信息
                #region 5203
                Patient5203 patient5203 = new Patient5203();
                Models.Request.RequestGzsiModel5203 RequestGdsiModel5203 = new Models.Request.RequestGzsiModel5203();
                Models.Response.ResponseGzsiModel5203 ResponseGdsiModel5203 = new ResponseGzsiModel5203();
                Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
                data5203.psn_no = this.tbPsn_no.Text;
                data5203.setl_id = this.tbSetl_id.Text;
                data5203.mdtrt_id = this.tbMdtrt_id.Text;

                RequestGdsiModel5203.data = data5203;
                if (patient5203.CallService(RequestGdsiModel5203, ref ResponseGdsiModel5203) < 0)
                {
                    MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
                }
                if (ResponseGdsiModel5203.infcode != "0")
                {
                    MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
                }

                if (ResponseGdsiModel5203.output != null && ResponseGdsiModel5203.output.setlinfo != null)
                {
                    //赋值
                    r.SIMainInfo.RegNo = ResponseGdsiModel5203.output.setlinfo.mdtrt_id;//就诊ID
                    r.Name = ResponseGdsiModel5203.output.setlinfo.psn_name;//姓名
                    r.Sex.ID = ResponseGdsiModel5203.output.setlinfo.gend=="2"?"F":"M";//性别
                    r.IDCard = ResponseGdsiModel5203.output.setlinfo.certno;//身份证号
                    r.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(ResponseGdsiModel5203.output.setlinfo.brdy);//生日
                    //r.SIMainInfo.EmplType = ResponseGdsiModel5203.output.setlinfo.psn_type;//人员类别 1 在职 2 退休
                    //医保api新加字段
                    r.SIMainInfo.Mdtrt_id = ResponseGdsiModel5203.output.setlinfo.mdtrt_id;//就诊ID
                    r.SIMainInfo.Setl_id = ResponseGdsiModel5203.output.setlinfo.setl_id;//结算ID
                    r.SIMainInfo.Psn_no = ResponseGdsiModel5203.output.setlinfo.psn_no;//人员编号
                    r.SIMainInfo.Psn_name = ResponseGdsiModel5203.output.setlinfo.psn_name;//人员姓名
                    r.SIMainInfo.Psn_cert_type = ResponseGdsiModel5203.output.setlinfo.psn_cert_type;//人员证件类型
                    r.SIMainInfo.Certno = ResponseGdsiModel5203.output.setlinfo.certno;//证件号码
                    r.SIMainInfo.Gend = ResponseGdsiModel5203.output.setlinfo.gend;//性别
                    r.SIMainInfo.Naty = ResponseGdsiModel5203.output.setlinfo.naty;//民族
                    r.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(ResponseGdsiModel5203.output.setlinfo.brdy);//出生日期
                    r.SIMainInfo.Age = ResponseGdsiModel5203.output.setlinfo.age;//年龄
                    r.SIMainInfo.Insutype = ResponseGdsiModel5203.output.setlinfo.insutype;//险种类型
                    r.SIMainInfo.Psn_type = ResponseGdsiModel5203.output.setlinfo.psn_type;//人员类别
                    r.SIMainInfo.Cvlserv_flag = ResponseGdsiModel5203.output.setlinfo.cvlserv_flag;//公务员标志
                    r.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(ResponseGdsiModel5203.output.setlinfo.setl_time);//结算时间
                    r.SIMainInfo.Psn_setlway = "";//个人结算方式 TOOL
                    r.SIMainInfo.Mdtrt_cert_type = "";//就诊凭证类型
                    r.SIMainInfo.Med_type = ResponseGdsiModel5203.output.setlinfo.med_type;//医疗类别
                    r.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.medfee_sumamt);//医疗费总额
                    r.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal("");//全自费金额 TOOL
                    r.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.overlmt_selfpay);//超限价自费费用
                    r.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.preselfpay_amt);//先行自付金额
                    r.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.inscp_scp_amt);//符合范围金额
                    r.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal("");//医保认可费用总额 TOOL
                    r.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.act_pay_dedc);//实际支付起付线
                    r.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifes_pay);//基本医疗保险统筹基金支出
                    r.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.pool_prop_selfpay);//基本医疗保险统筹基金支付比例
                    r.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.cvlserv_pay);//公务员医疗补助资金支出
                    r.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifes_pay);//企业补充医疗保险基金支出
                    r.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifmi_pay);//居民大病保险资金支出
                    r.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifob_pay);//职工大额医疗费用补助基金支出
                    r.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal("");//伤残人员医疗保障基金支出
                    r.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.maf_pay);//医疗救助基金支出
                    r.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.oth_pay);//其他基金支出
                    r.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.fund_pay_sumamt);//基金支付总额
                    r.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal("");//医院负担金额TOOL
                    r.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.psn_pay);//个人负担总金额
                    r.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.acct_pay);//个人账户支出
                    r.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.cash_payamt);//现金支付金额
                    r.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.acct_mulaid_pay);//账户共济支付金额
                    r.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.balc);//个人账户支出后余额
                    r.SIMainInfo.Clr_optins = ResponseGdsiModel5203.output.setlinfo.clr_optins;//清算经办机构
                    r.SIMainInfo.Clr_way = ResponseGdsiModel5203.output.setlinfo.clr_way;//清算方式
                    r.SIMainInfo.Clr_type = ResponseGdsiModel5203.output.setlinfo.clr_type;//清算类别
                    r.SIMainInfo.Medins_setl_id = ResponseGdsiModel5203.output.setlinfo.medins_setl_id;//医药机构结算ID
                    r.SIMainInfo.Vola_type = "";//违规类型
                    r.SIMainInfo.Vola_dscr = "";//违规说明
                    r.SIMainInfo.TypeCode = "";//
                    r.SIMainInfo.Insuplc_admdvs = "440600";//TOOL
                }

                #endregion

                #region 插入
                if (!this.LocalManager.QueryIsSave(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id))
                {
                    if (this.LocalManager.InsertOutPatientRegInfo(r) < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.LocalManager.Err);
                        return;
                    }
                    if (this.LocalManager.UpdateSiMainInfoOutBalanceInfo(r) < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.LocalManager.Err);
                        return;
                    }

                }
                #endregion

            }
            else {
                //根据发票号获取就诊信息
                string invoiceNo = this.tbInsertInvoiceNo.Text.PadLeft(12, '0');
                ArrayList comBalances = this.feeInpatient.QueryBalancesByInvoiceNO(invoiceNo);
                if (comBalances == null || comBalances.Count <= 0)
                {
                    MessageBox.Show("没有查询发票信息！");
                    return;
                }

                #region 赋值
                FS.HISFC.Models.Fee.Inpatient.Balance balance = comBalances[0] as FS.HISFC.Models.Fee.Inpatient.Balance;
                FS.HISFC.Models.RADT.PatientInfo r = new FS.HISFC.Models.RADT.PatientInfo();
                //r.SIMainInfo.HosNo = Reader[0].ToString();//本级医疗机构编码
                r.ID = balance.Patient.ID;//住院流水号
                r.SIMainInfo.BalNo = "";//结算序号
                r.SIMainInfo.InvoiceNo = balance.Invoice.ID;//发票号
                //r.SIMainInfo.MedicalType.ID = Reader[4].ToString();//医疗类别
                //if (r.SIMainInfo.MedicalType.ID == "1")
                //    r.SIMainInfo.MedicalType.Name = "住院";
                //else
                //    r.SIMainInfo.MedicalType.Name = "门诊特定项目";
                //r.PID.PatientNO = Reader[5].ToString();//住院号
                r.PID.CardNO = this.LocalManager.GetCardNOByInpatientID(balance.Patient.ID);//balance.Patient.PID.CardNO;//就诊卡号 //根据流水号查询患者卡号
                //r.SSN = Reader[7].ToString();//医疗证号
                //r.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());//审批号
                //r.SIMainInfo.ProceatePcNo = Reader[9].ToString();//生育保险患者电脑号
                //r.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());//参保日期
                //r.SIMainInfo.SiState = Reader[11].ToString();//参保状态
                //r.Name = Reader[12].ToString();//姓名
                //r.Sex.ID = Reader[13].ToString();//性别
                //r.IDCard = Reader[14].ToString();//身份证号
                //r.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());//生日
                //r.SIMainInfo.EmplType = Reader[16].ToString();//人员类别 1 在职 2 退休
                //r.CompanyName = Reader[17].ToString();//工作单位
                r.SIMainInfo.InDiagnose.Name = "-";//门诊诊断
                r.PVisit.PatientLocation.Dept.ID = "-";//科室代码
                r.PVisit.PatientLocation.Dept.Name = "-";//科室名称
                r.PVisit.PatientLocation.Dept.ID = "-";//科室代码
                r.PVisit.PatientLocation.Dept.Name = "-";//科室名称
                r.Pact.PayKind.ID = balance.Patient.Pact.PayKind.ID;// "2";//结算类别 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                r.Pact.ID = balance.Patient.Pact.ID;//合同代码
                r.Pact.Name = this.LocalManager.GetPactName(balance.Patient.Pact.ID);//balance.Patient.Pact.Name;//合同单位名称
                //r.PVisit.PatientLocation.Bed.ID = "";//床号
                //r.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());//入院日期
                //r.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());//入院日期
                //r.SIMainInfo.InDiagnose.ID = Reader[26].ToString();//入院诊断代码
                //r.SIMainInfo.InDiagnose.Name = Reader[27].ToString();//入院诊断名称
                //if (!Reader.IsDBNull(28))
                //r.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());//出院日期
                //r.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();//出院诊断代码
                //r.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();//出院诊断名称
                r.SIMainInfo.BalanceDate = balance.BalanceOper.OperTime;//结算日期(上次)
                r.SIMainInfo.TotCost = balance.FT.TotCost;//费用金额(未结)(住院总金额)
                r.SIMainInfo.PayCost = balance.FT.PayCost;//帐户支付
                r.SIMainInfo.PubCost = balance.FT.PubCost;//公费金额(未结)(社保支付金额)
                r.SIMainInfo.ItemPayCost = 0;//部分项目自付金额
                r.SIMainInfo.BaseCost = 0;//个人起付金额
                r.SIMainInfo.PubOwnCost = 0;//个人自费项目金额
                r.SIMainInfo.ItemYLCost = 0;//个人自付金额（乙类自付部分）
                r.SIMainInfo.OwnCost = balance.FT.OwnCost;//个人自负金额
                r.SIMainInfo.OverTakeOwnCost = 0;//超统筹支付限额个人自付金额

                r.SIMainInfo.Memo = "对账补充";//自费原因
                r.SIMainInfo.OperInfo.ID = "";//操作员
                r.SIMainInfo.OperDate = System.DateTime.Now;//操作日期
                r.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32("");//费用批次
                r.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal("");//医药机构分担金额
                r.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal("");//本年度可用定额
                r.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean("1");//1 有效 0 作废
                r.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean("1");//1 结算 0 未结算
                #endregion
                //获取医保信息
                #region 5203
                Patient5203 patient5203 = new Patient5203();
                Models.Request.RequestGzsiModel5203 RequestGdsiModel5203 = new Models.Request.RequestGzsiModel5203();
                Models.Response.ResponseGzsiModel5203 ResponseGdsiModel5203 = new ResponseGzsiModel5203();
                Models.Request.RequestGzsiModel5203.Data data5203 = new Models.Request.RequestGzsiModel5203.Data();
                data5203.psn_no = this.tbPsn_no.Text;
                data5203.setl_id = this.tbSetl_id.Text;
                data5203.mdtrt_id = this.tbMdtrt_id.Text;

                RequestGdsiModel5203.data = data5203;
                if (patient5203.CallService( RequestGdsiModel5203, ref ResponseGdsiModel5203) < 0)
                {
                    MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
                }
                if (ResponseGdsiModel5203.infcode != "0")
                {
                    MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
                }

                if (ResponseGdsiModel5203.output != null && ResponseGdsiModel5203.output.setlinfo != null)
                {
                    //赋值
                    r.PVisit.InTime =Convert.ToDateTime( ResponseGdsiModel5203.output.setlinfo.begndate);
                    r.PVisit.OutTime = Convert.ToDateTime(ResponseGdsiModel5203.output.setlinfo.enddate);
                    r.SIMainInfo.RegNo = ResponseGdsiModel5203.output.setlinfo.mdtrt_id;//就诊ID
                    r.Name = ResponseGdsiModel5203.output.setlinfo.psn_name;//姓名
                    r.Sex.ID = ResponseGdsiModel5203.output.setlinfo.gend == "2"?"F":"M";//性别
                    r.IDCard = ResponseGdsiModel5203.output.setlinfo.certno;//身份证号
                    r.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(ResponseGdsiModel5203.output.setlinfo.brdy);//生日
                    //r.SIMainInfo.EmplType = ResponseGdsiModel5203.output.setlinfo.psn_type;//人员类别 1 在职 2 退休
                    //医保api新加字段
                    r.SIMainInfo.Mdtrt_id = ResponseGdsiModel5203.output.setlinfo.mdtrt_id;//就诊ID
                    r.SIMainInfo.Setl_id = ResponseGdsiModel5203.output.setlinfo.setl_id;//结算ID
                    r.SIMainInfo.Psn_no = ResponseGdsiModel5203.output.setlinfo.psn_no;//人员编号
                    r.SIMainInfo.Psn_name = ResponseGdsiModel5203.output.setlinfo.psn_name;//人员姓名
                    r.SIMainInfo.Psn_cert_type = ResponseGdsiModel5203.output.setlinfo.psn_cert_type;//人员证件类型
                    r.SIMainInfo.Certno = ResponseGdsiModel5203.output.setlinfo.certno;//证件号码
                    r.SIMainInfo.Gend = ResponseGdsiModel5203.output.setlinfo.gend;//性别
                    r.SIMainInfo.Naty = ResponseGdsiModel5203.output.setlinfo.naty;//民族
                    r.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(ResponseGdsiModel5203.output.setlinfo.brdy);//出生日期
                    r.SIMainInfo.Age = ResponseGdsiModel5203.output.setlinfo.age;//年龄
                    r.SIMainInfo.Insutype = ResponseGdsiModel5203.output.setlinfo.insutype;//险种类型
                    r.SIMainInfo.Psn_type = ResponseGdsiModel5203.output.setlinfo.psn_type;//人员类别
                    r.SIMainInfo.Cvlserv_flag = ResponseGdsiModel5203.output.setlinfo.cvlserv_flag;//公务员标志
                    r.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(ResponseGdsiModel5203.output.setlinfo.setl_time);//结算时间
                    r.SIMainInfo.Psn_setlway = "";//个人结算方式 TOOL
                    r.SIMainInfo.Mdtrt_cert_type = "";//就诊凭证类型
                    r.SIMainInfo.Med_type = ResponseGdsiModel5203.output.setlinfo.med_type;//医疗类别
                    r.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.medfee_sumamt);//医疗费总额
                    r.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal("");//全自费金额 TOOL
                    r.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.overlmt_selfpay);//超限价自费费用
                    r.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.preselfpay_amt);//先行自付金额
                    r.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.inscp_scp_amt);//符合范围金额
                    r.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal("");//医保认可费用总额 TOOL
                    r.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.act_pay_dedc);//实际支付起付线
                    r.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifes_pay);//基本医疗保险统筹基金支出
                    r.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.pool_prop_selfpay);//基本医疗保险统筹基金支付比例
                    r.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.cvlserv_pay);//公务员医疗补助资金支出
                    r.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifes_pay);//企业补充医疗保险基金支出
                    r.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifmi_pay);//居民大病保险资金支出
                    r.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.hifob_pay);//职工大额医疗费用补助基金支出
                    r.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal("");//伤残人员医疗保障基金支出
                    r.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.maf_pay);//医疗救助基金支出
                    r.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.oth_pay);//其他基金支出
                    r.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.fund_pay_sumamt);//基金支付总额
                    r.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal("");//医院负担金额TOOL
                    r.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.psn_pay);//个人负担总金额
                    r.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.acct_pay);//个人账户支出
                    r.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.cash_payamt);//现金支付金额
                    r.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.acct_mulaid_pay);//账户共济支付金额
                    r.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(ResponseGdsiModel5203.output.setlinfo.balc);//个人账户支出后余额
                    r.SIMainInfo.Clr_optins = ResponseGdsiModel5203.output.setlinfo.clr_optins;//清算经办机构
                    r.SIMainInfo.Clr_way = ResponseGdsiModel5203.output.setlinfo.clr_way;//清算方式
                    r.SIMainInfo.Clr_type = ResponseGdsiModel5203.output.setlinfo.clr_type;//清算类别
                    r.SIMainInfo.Medins_setl_id = ResponseGdsiModel5203.output.setlinfo.medins_setl_id;//医药机构结算ID
                    r.SIMainInfo.Vola_type = "";//违规类型
                    r.SIMainInfo.Vola_dscr = "";//违规说明
                    r.SIMainInfo.TypeCode = "";//
                    r.SIMainInfo.Insuplc_admdvs = "440600";//TOOL
                }

                #endregion

                #region 插入
                if (!this.LocalManager.QueryIsSave(r.SIMainInfo.Mdtrt_id, r.SIMainInfo.Setl_id))
                {
                    if (this.LocalManager.InsertInPatientReg(r) < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.LocalManager.Err);
                        return;
                    }
                    if (this.LocalManager.UpdateSiMainInfoBalanceInfo(r) < 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.LocalManager.Err);
                        return;
                    }

                }
                #endregion
            }
            #endregion

            MessageBox.Show("插入成功！");
        }
    }
}
