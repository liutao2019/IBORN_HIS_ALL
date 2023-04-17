using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using API.GZSI.Business;
using API.GZSI.Models.Request;
using API.GZSI.Models.Response;
using System.IO;
using System.Globalization;
namespace API.GZSI.UI
{
    public partial class frmOtherQuery : Form
    {
        /// <summary>
        /// 业务回滚管理类
        /// </summary>
        private RollbackManager rollbackMgr = new RollbackManager();

        int returnvalue = 0;
        string SerialNumber = string.Empty;//交易流水号
        string strTransVersion = string.Empty;//交易版本号
        string strVerifyCode = string.Empty;//交易验证码

        public frmOtherQuery()
        {
            InitializeComponent();
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            CommonService5102 emplyoeeInfo = new CommonService5102();
            Models.Request.RequestGzsiModel5102 request = new Models.Request.RequestGzsiModel5102();
            request.data = new Models.Request.RequestGzsiModel5102.Data();
            request.data.prac_psn_type = "2";	//执业人员分类
            request.data.psn_cert_type = "02";	//人员证件类型
            request.data.certno ="";	//证件号码
            request.data.prac_psn_name ="";	//执业人员姓名
            // 出参
            Models.Response.ResponseGzsiModel5102 response = new Models.Response.ResponseGzsiModel5102();
            if (emplyoeeInfo.CallService(request, ref response) < 0)
            {
                MessageBox.Show(emplyoeeInfo.ErrorMsg);
                return;
            }
            
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            CommonService5401 projectInfo = new CommonService5401();
            Models.Request.RequestGzsiModel5401 tempRequest = new Models.Request.RequestGzsiModel5401();
            tempRequest.bilgiteminfo = new Models.Request.RequestGzsiModel5401.Bilgiteminfo();

            tempRequest.bilgiteminfo.psn_no = "51";//人员编号
            tempRequest.bilgiteminfo.exam_org_code = "2";
            tempRequest.bilgiteminfo.exam_org_name = "机构名称";
            tempRequest.bilgiteminfo.exam_item_code = "5";
            tempRequest.bilgiteminfo.exam_item_name = "测试项目";
            // 出参
            Models.Response.ResponseGzsiModel5401 tempResponse = new Models.Response.ResponseGzsiModel5401();

            if (projectInfo.CallService(tempRequest, ref tempResponse) < 0)
            {
               // MessageBox.Show(reportInfo.ErrorMsg);
                return;
            }
        }

        private void neuButton3_Click(object sender, EventArgs e)
        {
            CommonService5402 reportInfo = new CommonService5402();
            Models.Request.RequestGzsiModel5402 request = new Models.Request.RequestGzsiModel5402();
            request.rptdetailinfo = new Models.Request.RequestGzsiModel5402.Rptdetailinfo();
            request.rptdetailinfo.psn_no = "1";//人员编号
            request.rptdetailinfo.rpotc_no = "2";//报告单号
            request.rptdetailinfo.fixmedins_code = "5";//机构编码
            // 出参
            Models.Response.ResponseGzsiModel5402 response = new Models.Response.ResponseGzsiModel5402();

            if (reportInfo.CallService(request, ref response) < 0)
            {
                MessageBox.Show(reportInfo.ErrorMsg);
                return;
            }           
        }
    }
}
