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
    public partial class frmDZToolOLD : Form
    {
        public frmDZToolOLD()
        {
            InitializeComponent();
            Init();
        }

        #region 变量
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        LocalManager LocalManager = new LocalManager();
        #endregion

        private void Init() {
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
        public void setData(string psn_no, string mdtrt_id, string setl_id, string med_type,string clr_type) {
            this.tbMdtrt_id.Text = mdtrt_id;
            this.tbSetl_id.Text = setl_id;
            this.tbPsn_no.Text = psn_no;
            this.ncbMed_type.Tag = med_type;
            this.cmbClrType.Tag = clr_type;
            this.QueryInfo(this.tbPsn_no.Text, this.tbMdtrt_id.Text, this.tbSetl_id.Text, this.ncbMed_type.Tag.ToString());
        }

        //查询
        public void QueryInfo(string psn_no, string mdtrt_id ,string setl_id,string med_type) {
            if (string.IsNullOrEmpty(psn_no)) {
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

            if (string.IsNullOrEmpty(setl_id))
            {
                MessageBox.Show("患者医疗类别不能为空！");
                return;
            }


            this.fpSpread_5201.RowCount = 0;
            this.fpSpread_5203.RowCount = 0;
            this.fpSpread_5204.RowCount = 0;

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

            if (ResponseGzsiModel5201.output.mdtrtinfo != null && ResponseGzsiModel5201.output.mdtrtinfo.Count>0) {
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
            data5203.mdtrt_id =mdtrt_id;

            RequestGzsiModel5203.data = data5203;
            if (patient5203.CallService(RequestGzsiModel5203, ref ResponseGzsiModel5203) < 0)
            {
                MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
            }
            if (ResponseGzsiModel5203.infcode != "0")
            {
                MessageBox.Show("人员结算信息查询失败：" + patient5203.ErrorMsg);
            }

            if (ResponseGzsiModel5203.output != null && ResponseGzsiModel5203.output.setlinfo != null) {
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

            if (ResponseGzsiModel5204.output != null && ResponseGzsiModel5204.output.Count > 0) {
                Common.Function.ShowOutDateToFarpoint<ResponseGzsiModel5204.Output>(this.fpSpread_5204, ResponseGzsiModel5204.output.OrderBy(m => m.hilist_name).ToList());
            }
            #endregion

        }

        //取消医保结算
        private void bCancelYBJS_Click(object sender, EventArgs e)
        {
            //先判断本地是否有记录，若是有记录不允许直接操作
            if (this.LocalManager.QuerySIInfo(this.tbMdtrt_id.Text,this.tbSetl_id.Text)  == "1") {
                MessageBox.Show("本地有相关结算记录，请联系信息科处理！");
                return;
            }

            //门诊
            if (this.cmbClrType.Tag.ToString() == "11")
            {
                this.CancelBalanceOutpatient(this.tbPsn_no.Text, this.tbMdtrt_id.Text, this.tbSetl_id.Text);
            }
            //住院
            else {
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
            Models.Request.RequestGzsiModel2208 requestGzsiModel2208 = new API.GZSI.Models.Request.RequestGzsiModel2208();
            Models.Response.ResponseGzsiModel2208 responseGzsiModel2208 = new API.GZSI.Models.Response.ResponseGzsiModel2208();
            Models.Request.RequestGzsiModel2208.Mdtrtinfo mdtrtinfo2208 = new API.GZSI.Models.Request.RequestGzsiModel2208.Mdtrtinfo();

            mdtrtinfo2208.setl_id = setl_id; //结算ID
            mdtrtinfo2208.mdtrt_id = mdtrt_id; //就诊ID 
            mdtrtinfo2208.psn_no = psn_no; //人员编号
            requestGzsiModel2208.data = new API.GZSI.Models.Request.RequestGzsiModel2208.Mdtrtinfo();
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
            //Models.Request.RequestGzsiModel2205 requestGdsiModel2205 = new Models.Request.RequestGzsiModel2205();
            //Models.Response.ResponseGzsiModel2205 responseGdsiModel2205 = new ResponseGzsiModel2205();
            //Models.Request.RequestGzsiModel2205.Data data2205 = new Models.Request.RequestGzsiModel2205.Data();
            //data2205.chrg_bchno = "0000";// "0000";//费用流水号 传入“0000”删除全部,不能撤销全部，医保接口BUG已经结算的还能撤销明细，吐了
            //data2205.mdtrt_id = mdtrt_id;//就诊ID
            //data2205.psn_no = psn_no;//人员编号
            //requestGdsiModel2205.data = data2205;
            //if (outPatient2205.CallService( requestGdsiModel2205, ref responseGdsiModel2205) == -1)
            //{
            //    MessageBox.Show("门诊费用明细信息撤销失败：" + outPatient2205.ErrorMsg);
            //    return ;
            //}
            #endregion

            #region 2202
            //OutPatient2202 outPatient2202 = new OutPatient2202();
            //Models.Request.RequestGzsiModel2202 requestGzsiModel2202 = new API.GZSI.Models.Request.RequestGzsiModel2202();
            //Models.Response.ResponseGzsiModel2202 responseGzsiModel2202 = new API.GZSI.Models.Response.ResponseGzsiModel2202();
            //Models.Request.RequestGzsiModel2202.Mdtrtinfo mdtrtinfo2202 = new API.GZSI.Models.Request.RequestGzsiModel2202.Mdtrtinfo();

            //mdtrtinfo2202.mdtrt_id = mdtrt_id;//就诊ID 202011031615
            //mdtrtinfo2202.psn_no =psn_no;//人员编号 1000753288
            //mdtrtinfo2202.ipt_otp_no = "";//住院/门诊号  0000735959
            //requestGzsiModel2202.data = new API.GZSI.Models.Request.RequestGzsiModel2202.Mdtrtinfo();
            //requestGzsiModel2202.data = mdtrtinfo2202;

            //if (outPatient2202.CallService(requestGzsiModel2202, ref responseGzsiModel2202) == -1)
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
            Models.Request.RequestGzsiModel2305 requestGzsiModel2305 = new API.GZSI.Models.Request.RequestGzsiModel2305();
            Models.Response.ResponseGzsiModel2305 responseGzsiModel2305 = new API.GZSI.Models.Response.ResponseGzsiModel2305();
            requestGzsiModel2305.data = new API.GZSI.Models.Request.RequestGzsiModel2305.Mdtrtinfo();

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
            this.QueryInfo(this.tbPsn_no.Text, this.tbMdtrt_id.Text, this.tbSetl_id.Text, this.ncbMed_type.Tag.ToString());
        }

        private void bCancelBDYBJS_Click(object sender, EventArgs e)
        {

        }
    }
}
