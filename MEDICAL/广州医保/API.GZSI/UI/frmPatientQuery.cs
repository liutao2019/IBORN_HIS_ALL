using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using API.GZSI.Models;
using API.GZSI.Business;

namespace API.GZSI.UI
{
    /// <summary>
    /// 就诊获取人员信息，门诊住院通用
    /// </summary>
    public partial class frmPatientQuery : Form
    {
        #region 管理类

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        #endregion

        #region 属性

        private FS.HISFC.Models.RADT.Patient patient;

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        /// <summary>
        /// 获取患者信息返回实体
        /// </summary>
        public Models.Response.ResponseGzsiModel1101 res = new API.GZSI.Models.Response.ResponseGzsiModel1101();

        #endregion

        #region 变量

        /// <summary>
        /// 读卡返回的社保卡基本信息
        /// 持社保卡就诊必须传入，对应ReadCardBas函数的输出参数pOutBuff
        /// </summary>
        public string hcard_basinfo = string.Empty;

        /// <summary>
        /// 持卡就诊登记许可号
        /// 持社保卡就诊必须传入，对应ReadCardBas函数的输出参数pSignBuff
        /// </summary>
        public string hcard_chkinfo = string.Empty;

        /// <summary>
        /// 异地就医标志
        /// </summary>
        bool IsOtherCity = false;

        /// <summary>
        /// 就诊类型（1-门诊，2-住院）
        /// </summary>
        public string OperateType = string.Empty;

        /// <summary>
        /// 公共返回值
        /// </summary>
        int returnvalue = 0;

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
        /// 病种列表
        /// </summary>
        ArrayList diseCdgList = new ArrayList();

        /// <summary>
        /// 就诊类型列表
        /// </summary>
        ArrayList medTypeList = new ArrayList();

        /// <summary>
        /// 结算类型列表
        /// </summary>
        ArrayList setlWayList = new ArrayList();

        /// <summary>
        /// 生育资格类型列表
        /// </summary>
        ArrayList matnTypeList = new ArrayList();

        #endregion

        /// <summary>
        /// 获取医保信息
        /// </summary>
        public frmPatientQuery()
        {
            InitializeComponent();
            initEvents();
        }

        /// <summary>
        /// 加载界面信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQueryPatientInfo_Load(object sender, EventArgs e)
        {
            this.initControls();
            this.initValue();
            //this.QueryData();
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void initEvents()
        {
            this.Load += frmQueryPatientInfo_Load;
            this.btnSign.Click += new EventHandler(btnSign_Click);
            this.btnReadCard.Click += new EventHandler(btnReadCard_Click);
            this.btnReadIDCard.Click += new EventHandler(btnReadIDCard_Click);
            this.btnScan.Click += new EventHandler(btnScan_Click);
            this.btnQuery.Click += new EventHandler(btnQuery_Click);
            this.btnTest.Click += new EventHandler(btnTest_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.cmbMedType.SelectedIndexChanged += new EventHandler(cmbMedType_SelectedIndexChanged);
            this.cbSd.CheckedChanged += new EventHandler(cbSd_CheckedChanged);

            this.txtIdCode.KeyDown += new KeyEventHandler(txtIdCode_KeyDown);

        }

        private void txtIdCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                string queryStr = this.txtIdCode.Text;
                if (!string.IsNullOrEmpty(queryStr))
                {
                    StringBuilder pOutBuff = new StringBuilder(1024); //输出参数pOutBuff
                    StringBuilder pSignBuff = new StringBuilder(1024); //输入参数pSignBuff

                    string[] InfoGZ = queryStr.Split('|');

                    #region  cs
                    //440600|441781198309233810|E5305627X|440600D15600000508029A3798718C31|谢汝森|0081544C978684440608029A37|3.00|20210118|20310118|000000000000|SOOW0200202102001070000000000000|
                    //Bt3RuWYzi6BDe+nPcgSBcX+OoCWrMJa/b+YTBoKW9rfEidXfjvKaVzDOS21YlQzd7SZqnqowPZIybsxxgUO2T7zsLfe6U9zX4XmME8a3ZiaqZ5RNu4ZP1pu7bPMVuIEEGBniZ4h5dIwJR4QIwU37WUE/1qE2SdNjXUn3EiSHsFYxNjI5MzU5MTM0Q0VBNTM3OTU1QUFDMTI5RTY4MzMyQ0QwODc5MjQ4N0M=
                    //string[] InfoGZ = "440600|441781198309233810|E5305627X|440600D15600000508029A3798718C31|谢汝森|0081544C978684440608029A37|3.00|20210118|20310118|000000000000|SOOW0200202102001070000000000000|".Split('|');
                    //string[] InfoGZ = "510300|51032120010124128X|CC6947546|510300D156000005E3966013F45B98D5|汪红丽|0081544C978684440608029A37|3.00|20210118|20310118|000000000000|SOOW0200202102001070000000000000|".Split('|');
                    //440100|440103199011161510|205687450|440100D15600000500129BD8A89A7863|陈钊豪|0081544C708684440100129BD8|1.0|20130207|20230207|440100903471|SOOW0200202102001068440100903471|

                    #endregion

                    if (InfoGZ.Count() >= 5)
                    {
                        this.cmbMdtrtCertType.Tag = "03";
                        //省内患者读卡是读不到患者信息的，但可以通过参保地来获取数据
                        if (InfoGZ[0].StartsWith("440"))
                        {
                            this.cmbMdtrtCertType.Tag = "02";
                        }
                    }
                    else
                    {
                        MessageBox.Show("手动填充内容有误，请重新填充！！");
                        return;
                    }

                    this.cmbInsuplc.Tag = InfoGZ[0];//参保地
                    this.tbMdtrtCertNo.Text = InfoGZ[1];//身份证(社会保障号码)
                    this.cardNO = InfoGZ[2];//身份证(社会保障号码)
                    this.card_sn = InfoGZ[3];//参保地
                    this.tbName.Text = InfoGZ[4];//姓名
                    this.hcard_basinfo = pOutBuff.ToString();
                    this.hcard_chkinfo = pSignBuff.ToString();
                    this.QueryData();
                }
                else
                {
                    MessageBox.Show("手动填充内容不能为空！！");
                }
            }
        }


        private void cbSd_CheckedChanged(object sender, EventArgs e)
        {
            this.txtIdCode.Enabled = this.cbSd.Checked;
        }
        private void cmbMedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbDiseCdg.Tag = "";
            string med_type = this.cmbMedType.Tag.ToString();
            ArrayList currentList = new ArrayList();
            currentList.AddRange(diseCdgList.Cast<FS.HISFC.Models.Base.Const>().Where(t => t.Memo == med_type).ToArray<FS.HISFC.Models.Base.Const>());
            this.cmbDiseCdg.AddItems(currentList);
            if (currentList.Count > 0)
            {
                this.cmbDiseCdg.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void initControls()
        {
            this.cmbMdtrtCertType.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "mdtrt_cert_type")); // 凭证类型
            this.cmbSex.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "sex")); // 性别
            medTypeList = constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "med_type");
            diseCdgList = constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "dise_codg");
            setlWayList = constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "psn_setlway");
            matnTypeList = constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "matn_trt_dclaer_type");
            ArrayList currentList = new ArrayList();
            currentList.AddRange(medTypeList.Cast<FS.HISFC.Models.Base.Const>().Where(t => ((t.Memo == this.OperateType || string.IsNullOrEmpty(t.Memo)) && t.IsValid)).ToArray<FS.HISFC.Models.Base.Const>());
            this.cmbMedType.AddItems(currentList);
            this.cmbSetlWay.AddItems(setlWayList);
            this.cmbMatnType.AddItems(matnTypeList);
            //添加参保地
            this.cmbInsuplc.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "insuplc"));
        }

        /// <summary>
        /// 初始化赋值
        /// </summary>
        private void initValue()
        {
            this.cmbMedType.Tag = (OperateType == "1") ? "11" : "21";
            this.tbName.Text = this.Patient.Name;
            this.cmbSex.Tag = this.Patient.Sex.ID.ToString();
            this.cmbMdtrtCertType.Tag = "02";
            this.tbMdtrtCertNo.Text = this.Patient.IDCard;
            this.cmbSetlWay.Tag = "01";
            this.cmbMatnType.Tag = "1";
            this.cmbInsuplc.Tag = "440100";

            // {3CCAB886-9648-4DE8-B5DC-276D9212353F}
            if (OperateType == "1")
            {
                this.repeat_ipt_flag.Visible = false;
                this.ttp_resp.Visible = false;
                this.merg_setl_flag.Visible = false;
            }
            else
            {
                this.repeat_ipt_flag.Visible = true;
                this.ttp_resp.Visible = true;
                this.merg_setl_flag.Visible = true;
            }

            this.txtIdCode.Enabled = false;
        }

        #region 各种事件

        /// <summary>
        /// 手动签到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSign_Click(object sender, EventArgs e)
        {
            //签到时间
            DateTime sNow = FS.FrameWork.Function.NConvert.ToDateTime(this.constMgr.GetSysDateTime());

            //调用实体
            CommonService9001 commonService9001 = new CommonService9001();
            int ret = commonService9001.ServiceSign(sNow);

            if (ret < 0)
            {
                MessageBox.Show("签到失败：" + commonService9001.ErrorMsg);
                return;
            }

            MessageBox.Show("签到成功");
        }

        private string card_sn = "";//卡识别号
        private string cardNO = "";//卡号

        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadCard_Click(object sender, EventArgs e)
        {
            #region 变量
            //医保提供的API访问地址。
            string pUrl = API.GZSI.Models.UserInfo.Instance.readcard_url; //医保读卡url
            string pUser = API.GZSI.Models.UserInfo.Instance.userId; //用户账户。
            StringBuilder pOutBuff = new StringBuilder(1024); //输出参数pOutBuff
            int nOutBuffLen = 1024;  //对应pOutBuff内存分配长度
            StringBuilder pSignBuff = new StringBuilder(1024); //输入参数pSignBuff
            int nSignBuffLen = 1024; //对应pSignBuff内存分配长度
            #endregion

            try
            {
                returnvalue = Init(pUrl, pUser);


                returnvalue = ReadCardBasGZ(pOutBuff, nOutBuffLen, pSignBuff, nSignBuffLen);



                string[] InfoGZ = pOutBuff.ToString().Split('|');

           

                #region  cs
                //440600|441781198309233810|E5305627X|440600D15600000508029A3798718C31|谢汝森|0081544C978684440608029A37|3.00|20210118|20310118|000000000000|SOOW0200202102001070000000000000|
                //Bt3RuWYzi6BDe+nPcgSBcX+OoCWrMJa/b+YTBoKW9rfEidXfjvKaVzDOS21YlQzd7SZqnqowPZIybsxxgUO2T7zsLfe6U9zX4XmME8a3ZiaqZ5RNu4ZP1pu7bPMVuIEEGBniZ4h5dIwJR4QIwU37WUE/1qE2SdNjXUn3EiSHsFYxNjI5MzU5MTM0Q0VBNTM3OTU1QUFDMTI5RTY4MzMyQ0QwODc5MjQ4N0M=
                //string[] InfoGZ = "440600|441781198309233810|E5305627X|440600D15600000508029A3798718C31|谢汝森|0081544C978684440608029A37|3.00|20210118|20310118|000000000000|SOOW0200202102001070000000000000|".Split('|');
                //string[] InfoGZ = "510300|51032120010124128X|CC6947546|510300D156000005E3966013F45B98D5|汪红丽|0081544C978684440608029A37|3.00|20210118|20310118|000000000000|SOOW0200202102001070000000000000|".Split('|');
                //440100|440103199011161510|205687450|440100D15600000500129BD8A89A7863|陈钊豪|0081544C708684440100129BD8|1.0|20130207|20230207|440100903471|SOOW0200202102001068440100903471|

                #endregion

                if (pOutBuff != null && !string.IsNullOrEmpty(pOutBuff.ToString()) && InfoGZ.Count() > 0)
                {
                    this.cmbMdtrtCertType.Tag = "03";
                    //省内患者读卡是读不到患者信息的，但可以通过参保地来获取数据
                    if (InfoGZ[0].StartsWith("440"))
                    {
                        this.cmbMdtrtCertType.Tag = "02";
                    }
                }

                this.cmbInsuplc.Tag = InfoGZ[0];//参保地
                this.tbMdtrtCertNo.Text = InfoGZ[1];//身份证(社会保障号码)
                this.cardNO = InfoGZ[2];//身份证(社会保障号码)
                this.card_sn = InfoGZ[3];//参保地
                this.tbName.Text = InfoGZ[4];//姓名
                this.hcard_basinfo = pOutBuff.ToString();
                this.hcard_chkinfo = pSignBuff.ToString();
                this.QueryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读卡失败！" + ex.Message);
            }

        }

        // {FF3AF953-EAEA-4813-BF92-537EC2E96BFF} 读电子凭证
        private void btnScan_Click(object sender, EventArgs e)
        {
            #region 变量
            //医保提供的API访问地址。
            string pUrl = API.GZSI.Models.UserInfo.Instance.readcard_url; //医保读卡url
            string pUser = API.GZSI.Models.UserInfo.Instance.userId; //用户账户。
            StringBuilder pOutBuff = new StringBuilder(1024); //输出参数pOutBuff
            int nOutBuffLen = 1024;  //对应pOutBuff内存分配长度
            StringBuilder pSignBuff = new StringBuilder(1024); //输入参数pSignBuff
            int nSignBuffLen = 1024; //对应pSignBuff内存分配长度
            #endregion

            try
            {
                returnvalue = Init(pUrl, pUser);
                returnvalue = GetTwoBarCodes(10000, pOutBuff, nOutBuffLen, pSignBuff, nSignBuffLen);
                string[] InfoGZ = pOutBuff.ToString().Split('|');

                if (pOutBuff != null && !string.IsNullOrEmpty(pOutBuff.ToString()) && InfoGZ.Count() > 0)
                {
                    this.cmbMdtrtCertType.Tag = "01";
                }

                this.tbMdtrtCertNo.Text = InfoGZ[0];
                this.hcard_basinfo = pOutBuff.ToString();
                this.hcard_chkinfo = pSignBuff.ToString();
                this.QueryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读卡失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 读身份证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadIDCard_Click(object sender, EventArgs e)
        {
            #region 变量
            //医保提供的API访问地址。
            string pUrl = API.GZSI.Models.UserInfo.Instance.readcard_url; //医保读卡url
            string pUser = API.GZSI.Models.UserInfo.Instance.userId; //用户账户。
            StringBuilder pOutBuff = new StringBuilder(1024); //输出参数pOutBuff
            int nOutBuffLen = 1024;  //对应pOutBuff内存分配长度
            StringBuilder pSignBuff = new StringBuilder(1024); //输入参数pSignBuff
            int nSignBuffLen = 1024; //对应pSignBuff内存分配长度
            #endregion

            try
            {
                returnvalue = Init(pUrl, pUser);
                returnvalue = ReadIDCardGZ(pOutBuff, nOutBuffLen, pSignBuff, nSignBuffLen);
                string[] InfoGZ = pOutBuff.ToString().Split('|');

                if (pOutBuff != null && !string.IsNullOrEmpty(pOutBuff.ToString()) && InfoGZ.Count() > 0)
                {
                    this.cmbMdtrtCertType.Tag = "02";
                }

                this.tbMdtrtCertNo.Text = InfoGZ[1];
                this.hcard_basinfo = pOutBuff.ToString();
                this.hcard_chkinfo = pSignBuff.ToString();
                this.QueryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读卡失败！" + ex.Message);
            }

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.QueryData();
        }

        /// <summary>
        /// 测试函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            int ret = 1;

            if (ret > 0)
            {
                return;
            }

            #region 挂号测试
            {
                OutPatient5303 outPatient5303 = new OutPatient5303();
                Models.Request.RequestGzsiModel5303 requestGzsiModel5303 = new API.GZSI.Models.Request.RequestGzsiModel5303();
                Models.Response.ResponseGzsiModel5303 responseGzsiModel5303 = new API.GZSI.Models.Response.ResponseGzsiModel5303();
                Models.Request.RequestGzsiModel5303.Data data = new API.GZSI.Models.Request.RequestGzsiModel5303.Data();
                data.psn_no = "1000024528";
                data.begntime = "2020-11-24 00:00:00";
                data.endtime = "2020-11-24 23:59:00";
                requestGzsiModel5303.data = data;
                returnvalue = outPatient5303.CallService(requestGzsiModel5303, ref responseGzsiModel5303, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 码表下载

            {
                #region 请求下载文件

                CommonService1901 commonService1901 = new CommonService1901();
                Models.Request.RequestGzsiModel1901 requestGzsiModel1901 = new API.GZSI.Models.Request.RequestGzsiModel1901();
                Models.Response.ResponseGzsiModel1901 responseGzsiModel1901 = new API.GZSI.Models.Response.ResponseGzsiModel1901();
                Models.Request.RequestGzsiModel1901.Data data1901 = new API.GZSI.Models.Request.RequestGzsiModel1901.Data();
                //API.GZSI.Models.Request.RequestGzsiModel1101Data
                data1901.admdvs = "440100";
                requestGzsiModel1901.data = new API.GZSI.Models.Request.RequestGzsiModel1901.Data();
                requestGzsiModel1901.data = data1901;
                returnvalue = commonService1901.CallService(requestGzsiModel1901, ref responseGzsiModel1901, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    return;
                }

                if (responseGzsiModel1901.infcode != "0")
                {
                    return;
                }

                #endregion

                #region 文件下载

                CommonService9102 commonService9102 = new CommonService9102();
                Models.Request.RequestGzsiModel9102 requestGzsiModel9102 = new API.GZSI.Models.Request.RequestGzsiModel9102();
                Models.Response.ResponseGzsiModel9102 responseGzsiModel9102 = new API.GZSI.Models.Response.ResponseGzsiModel9102();
                Models.Request.RequestGzsiModel9102.FsDownloadIn data9102 = new API.GZSI.Models.Request.RequestGzsiModel9102.FsDownloadIn();
                //API.GZSI.Models.Request.RequestGzsiModel1101Data
                data9102.file_qury_no = responseGzsiModel1901.output.fileinfo.file_qury_no;
                data9102.filename = responseGzsiModel1901.output.fileinfo.filename;
                requestGzsiModel9102.fsDownloadIn = new API.GZSI.Models.Request.RequestGzsiModel9102.FsDownloadIn();
                requestGzsiModel9102.fsDownloadIn = data9102;
                returnvalue = commonService9102.CallService(requestGzsiModel9102, ref responseGzsiModel9102, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    return;
                }

                if (responseGzsiModel9102.infcode != "0")
                {
                    return;
                }

                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件路径";
                //if (dialog.ShowDialog() == DialogResult.OK)
                //{
                //    string fileName = dialog.SelectedPath + data9102.filename;
                //    System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate);
                //    fs.Write(responseGzsiModel9102.output, 0, responseGzsiModel9102.output.Length);
                //    fs.Close();
                //}

                string errMsg = string.Empty;
                string fileNames = string.Empty; ;
                //if (API.GZSI.Class.ZipFloClass.unZipFile(responseGzsiModel9102.output, System.AppDomain.CurrentDomain.BaseDirectory, ref fileNames, ref errMsg) < 0)
                //{
                //    return;
                //}

                if (ret > 0)
                {
                    return;
                }
            }
                #endregion


            #endregion

            #region 目录对照上传3301
            {
                CommonService3301 qq = new CommonService3301();
                Models.Request.RequestGzsiModel3301 aa = new API.GZSI.Models.Request.RequestGzsiModel3301();
                Models.Response.ResponseGzsiModel3301 bb = new API.GZSI.Models.Response.ResponseGzsiModel3301();
                API.GZSI.Models.Request.RequestGzsiModel3301.Catalogcompin cc = new API.GZSI.Models.Request.RequestGzsiModel3301.Catalogcompin();
                cc.fixmedins_hilist_id = "10512";
                cc.fixmedins_hilist_name = "维生素C片";
                cc.list_type = "101";//101 西药
                cc.med_list_codg = "86901201000526";
                cc.drugadm_strdcod = "X-J01GA-L084-B001-0017";
                cc.begndate = "2020-11-04";
                cc.aprvno = "1001";
                cc.spec = "0.1g*100片/瓶";
                cc.pric = "2.56";
                aa.catalogcompin = new List<API.GZSI.Models.Request.RequestGzsiModel3301.Catalogcompin>();
                aa.catalogcompin.Add(cc);
                returnvalue = qq.CallService(aa, ref bb, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 目录对照上传3360
            {
                CommonService3360 qq = new CommonService3360();
                Models.Request.RequestGzsiModel3360 aa = new API.GZSI.Models.Request.RequestGzsiModel3360();
                Models.Response.ResponseGzsiModel3360 bb = new API.GZSI.Models.Response.ResponseGzsiModel3360();
                API.GZSI.Models.Request.RequestGzsiModel3360.Catalog cc = new API.GZSI.Models.Request.RequestGzsiModel3360.Catalog();
                cc.fixmedins_hilist_id = "10511";
                cc.fixmedins_hilist_name = "维生素C片";
                cc.list_type = "101";//101 西药
                cc.med_list_codg = "86901201000526";
                cc.enddate = "2020-11-04";
                aa.catalog = cc;
                returnvalue = qq.CallService(aa, ref bb, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 科室上传3401
            {
                CommonService3401 commonService3401 = new CommonService3401();
                Models.Request.RequestGzsiModel3401 aa = new API.GZSI.Models.Request.RequestGzsiModel3401();
                Models.Response.ResponseGzsiModel3401 bb = new API.GZSI.Models.Response.ResponseGzsiModel3401();
                API.GZSI.Models.Request.RequestGzsiModel3401.Deptinfo cc = new API.GZSI.Models.Request.RequestGzsiModel3401.Deptinfo();
                cc.hosp_dept_codg = "2101";
                cc.caty = "内科";
                cc.hosp_dept_name = "急诊内科";
                cc.begntime = "2020-11-02 21:04:57";
                cc.endtime = "2020-11-02 22:04:57";
                cc.itro = "专业内科";
                cc.dept_resper_name = "李主任";
                cc.dept_resper_tel = "1324563456";
                cc.dept_estbdat = "2020-1-1";
                cc.medserv_type = "02";
                cc.dept_type = "302";
                cc.aprv_bed_cnt = "30";
                cc.hi_crtf_bed_cnt = "22";
                cc.poolarea_no = "440100";
                cc.dr_psncnt = "15";
                cc.phar_psncnt = "5";
                cc.nurs_psncnt = "11";
                cc.tecn_psncnt = "7";
                cc.memo = "备注";
                cc.medtech_flag = "0";
                cc.dept_med_serv_scp = "1";
                aa.deptinfo = new List<API.GZSI.Models.Request.RequestGzsiModel3401.Deptinfo>();
                aa.deptinfo.Add(cc);
                returnvalue = commonService3401.CallService(aa, ref bb, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 病床3461
            {
                CommonService3461 commonService3461 = new CommonService3461();
                Models.Request.RequestGzsiModel3461 requestGzsiModel3401 = new API.GZSI.Models.Request.RequestGzsiModel3461();
                Models.Response.ResponseGzsiModel3461 responseGzsiModel3401 = new API.GZSI.Models.Response.ResponseGzsiModel3461();
                API.GZSI.Models.Request.RequestGzsiModel3461.Bedinfo bedinfo3461 = new API.GZSI.Models.Request.RequestGzsiModel3461.Bedinfo();
                bedinfo3461.hosp_bed_no = "4047";
                bedinfo3461.hosp_bed_type = "01";
                bedinfo3461.bed_ward = "4047";
                bedinfo3461.hosp_bed_room = "住院部03楼";
                bedinfo3461.hosp_dept_code = "3105";
                bedinfo3461.hosp_dept_name = "内一科";
                bedinfo3461.inpa_area_code = "41064047";
                bedinfo3461.inpa_area_name = "内一科护理组";
                bedinfo3461.memo = "备注";
                requestGzsiModel3401.bedinfo = new List<API.GZSI.Models.Request.RequestGzsiModel3461.Bedinfo>();
                requestGzsiModel3401.bedinfo.Add(bedinfo3461);
                returnvalue = commonService3461.CallService(requestGzsiModel3401, ref responseGzsiModel3401, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }

            #endregion

            #region 医师上传
            {
                CommonService3467 qq = new CommonService3467();
                Models.Request.RequestGzsiModel3467 aa = new API.GZSI.Models.Request.RequestGzsiModel3467();
                Models.Response.ResponseGzsiModel3467 bb = new API.GZSI.Models.Response.ResponseGzsiModel3467();
                API.GZSI.Models.Request.RequestGzsiModel3467.Doctorinfo cc = new API.GZSI.Models.Request.RequestGzsiModel3467.Doctorinfo();
                API.GZSI.Models.Request.RequestGzsiModel3467.Mgtinfo dd = new API.GZSI.Models.Request.RequestGzsiModel3467.Mgtinfo();
                dd.dr_codg = "9999";
                dd.hi_serv_type = "1";
                dd.hi_serv_name = "1";
                dd.hi_serv_stas = "0";
                dd.begndate = "2020-02-02";
                dd.enddate = "2020-12-30";
                dd.memo = "API上传";
                cc.mgtinfo = new List<API.GZSI.Models.Request.RequestGzsiModel3467.Mgtinfo>();
                cc.mgtinfo.Add(dd);

                cc.dr_codg = "009999";
                cc.dr_name = "测试";
                cc.prac_dr_id = "";
                cc.dr_prac_type = "1";
                cc.dr_pro_tech_duty = "11";
                cc.dr_prac_scp_code = "1";
                cc.pro_code = "369";
                cc.dcl_prof_flag = "1";
                cc.practice_code = "1";
                cc.dr_profttl_code = "231";
                cc.psn_itro = "1";
                cc.mul_prac_flag = "0";
                cc.main_pracins_flag = "1";
                cc.hosp_dept_code = "1000";
                cc.bind_flag = "1";
                cc.siprof_flag = "1";
                cc.loclprof_flag = "1";
                cc.hi_dr_flag = "1";
                cc.cert_type = "02";
                cc.certno = "420101199311137516";
                cc.memo = "1";
                aa.doctorinfo = new List<API.GZSI.Models.Request.RequestGzsiModel3467.Doctorinfo>();
                aa.doctorinfo.Add(cc);
                returnvalue = qq.CallService(aa, ref bb, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 查询患者信息1101
            {
                PatientService1101 patientService1101 = new PatientService1101();
                Models.Request.RequestGzsiModel1101 inParam = new API.GZSI.Models.Request.RequestGzsiModel1101();
                Models.Response.ResponseGzsiModel1101 outParam = new Models.Response.ResponseGzsiModel1101();
                Models.Request.RequestGzsiModel1101.Data bb = new API.GZSI.Models.Request.RequestGzsiModel1101.Data();
                bb.mdtrt_cert_type = "02";//就诊类型
                bb.mdtrt_cert_no = "440105194911064513";//卡号
                bb.card_sn = "";
                bb.psn_cert_type = "02";//就诊类型
                //bb.psn_name = this.Patient.Name;
                bb.certno = "440105194911064513";//卡号
                inParam.data = bb;
                returnvalue = patientService1101.CallService(inParam, ref outParam, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 挂号测试
            {
                OutPatient2201 outPatient2201 = new OutPatient2201();
                Models.Request.RequestGzsiModel2201 requestGzsiModel2201 = new API.GZSI.Models.Request.RequestGzsiModel2201();
                Models.Response.ResponseGzsiModel2201 responseGzsiModel2201 = new API.GZSI.Models.Response.ResponseGzsiModel2201();
                Models.Request.RequestGzsiModel2201.Mdtrtinfo mdtrtinfo2201 = new API.GZSI.Models.Request.RequestGzsiModel2201.Mdtrtinfo();
                mdtrtinfo2201.psn_no = "1000753288";
                mdtrtinfo2201.insutype = "310";
                mdtrtinfo2201.mdtrt_cert_type = "02";
                mdtrtinfo2201.mdtrt_cert_no = "440105194911064513";
                mdtrtinfo2201.begntime = "2020-11-04 09:00:00";
                mdtrtinfo2201.ipt_otp_no = "0000735959";
                mdtrtinfo2201.dept_code = "1000";
                mdtrtinfo2201.dept_name = "普通外科门诊";
                mdtrtinfo2201.atddr_no = "009999";
                mdtrtinfo2201.dr_name = "信息科";
                Models.Agnterinfo cc = new Models.Agnterinfo();
                cc.agnter_name = "阿黄";
                cc.agnter_rlts = "1";
                cc.agnter_cert_type = "01";
                cc.agnter_certno = "440125195909091731";
                cc.agnter_tel = "111";
                requestGzsiModel2201.mdtrtinfo = mdtrtinfo2201;
                requestGzsiModel2201.agnterinfo = cc;
                returnvalue = outPatient2201.CallService(requestGzsiModel2201, ref responseGzsiModel2201, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 撤销挂号测试
            {
                OutPatient2202 qq = new OutPatient2202();
                Models.Request.RequestGzsiModel2202 aa = new API.GZSI.Models.Request.RequestGzsiModel2202();
                Models.Response.ResponseGzsiModel2202 outParam = new API.GZSI.Models.Response.ResponseGzsiModel2202();
                Models.Request.RequestGzsiModel2202.Mdtrtinfo bb = new API.GZSI.Models.Request.RequestGzsiModel2202.Mdtrtinfo();
                bb.mdtrt_id = "202011041872";
                bb.psn_no = "1000753288";
                bb.ipt_otp_no = "0080000948";
                bb.mdtrt_mode = "";
                aa.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2202.Mdtrtinfo();
                aa.mdtrtinfo = bb;
                returnvalue = qq.CallService(aa, ref outParam, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 门诊就诊信息上传2203
            {
                OutPatient2203 outPatient2203 = new OutPatient2203();
                Models.Request.RequestGzsiModel2203 requestGzsiModel2203 = new API.GZSI.Models.Request.RequestGzsiModel2203();
                Models.Response.ResponseGzsiModel2203 responseGzsiModel2203 = new API.GZSI.Models.Response.ResponseGzsiModel2203();
                Models.Request.RequestGzsiModel2203.Mdtrtinfo mdtrtinfo2203 = new API.GZSI.Models.Request.RequestGzsiModel2203.Mdtrtinfo();
                mdtrtinfo2203.mdtrt_id = "202011041723";
                mdtrtinfo2203.psn_no = "1000753288";
                mdtrtinfo2203.med_type = "21";
                mdtrtinfo2203.begntime = "2020-11-04 09:00:00";
                requestGzsiModel2203.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2203.Mdtrtinfo();
                requestGzsiModel2203.mdtrtinfo = mdtrtinfo2203;
                Models.Request.RequestGzsiModel2203.Diseinfo Diseinfo2203 = new Models.Request.RequestGzsiModel2203.Diseinfo();
                Diseinfo2203.diag_type = "01";
                Diseinfo2203.diag_srt_no = "2";
                Diseinfo2203.diag_code = "12332";
                Diseinfo2203.diag_name = "dsfsdf";
                Diseinfo2203.diag_dept = "11311";
                Diseinfo2203.dise_dor_no = "3324324";
                Diseinfo2203.dise_dor_name = "dsfdsfs";
                Diseinfo2203.diag_time = "2020-11-04 09:00:00";
                Diseinfo2203.vali_flag = "0";
                Diseinfo2203.maindiag_flag = "1";
                requestGzsiModel2203.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel2203.Diseinfo>();
                requestGzsiModel2203.diseinfo.Add(Diseinfo2203);
                returnvalue = outPatient2203.CallService(requestGzsiModel2203, ref responseGzsiModel2203, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 门诊费用明细信息上传2204
            {
                OutPatient2204 qq = new OutPatient2204();
                Models.Request.RequestGzsiModel2204 aa = new API.GZSI.Models.Request.RequestGzsiModel2204();
                Models.Response.ResponseGzsiModel2204 outParam = new API.GZSI.Models.Response.ResponseGzsiModel2204();
                Models.Request.RequestGzsiModel2204.Feedetail bb = new API.GZSI.Models.Request.RequestGzsiModel2204.Feedetail();
                bb.feedetl_sn = "202011041111";
                bb.psn_no = "1000753288";
                bb.mdtrt_id = "202011041723";
                bb.chrg_bchno = "1001";
                bb.dise_codg = "M03701";
                bb.rx_circ_flag = "1";
                bb.fee_ocur_time = "2020-11-04 09:30:00";
                bb.med_list_codg = "86901201000526";
                bb.medins_list_codg = "10512";
                bb.det_item_fee_sumamt = "2200";
                bb.cnt = "22";
                bb.pric = "10";
                bb.bilg_dept_codg = "1000";
                bb.bilg_dept_name = "普通外科门诊";
                bb.bilg_dr_codg = "009999";
                bb.bilg_dr_name = "信息科";
                bb.hosp_appr_flag = "1";
                aa.feedetail = new List<API.GZSI.Models.Request.RequestGzsiModel2204.Feedetail>();
                aa.feedetail.Add(bb);
                returnvalue = qq.CallService(aa, ref outParam, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 门诊费用明细信息撤销2205
            {
                OutPatient2205 qq = new OutPatient2205();
                Models.Request.RequestGzsiModel2205 aa = new API.GZSI.Models.Request.RequestGzsiModel2205();
                Models.Response.ResponseGzsiModel2205 outParam = new API.GZSI.Models.Response.ResponseGzsiModel2205();
                Models.Request.RequestGzsiModel2205.Data bb = new API.GZSI.Models.Request.RequestGzsiModel2205.Data();
                bb.chrg_bchno = "202011031111";
                bb.psn_no = "1000753288";
                bb.mdtrt_id = "202011031620";
                aa.data = new API.GZSI.Models.Request.RequestGzsiModel2205.Data();
                aa.data = bb;
                returnvalue = qq.CallService(aa, ref outParam, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 门诊预结算2206
            {
                OutPatient2206 qq = new OutPatient2206();
                Models.Request.RequestGzsiModel2206 aa = new API.GZSI.Models.Request.RequestGzsiModel2206();
                Models.Response.ResponseGzsiModel2206 outParam = new API.GZSI.Models.Response.ResponseGzsiModel2206();
                Models.Request.RequestGzsiModel2206.Mdtrtinfo bb = new API.GZSI.Models.Request.RequestGzsiModel2206.Mdtrtinfo();
                bb.psn_no = "1000753288";
                bb.mdtrt_cert_type = "02";
                bb.mdtrt_cert_no = "440105194911064513";
                bb.med_type = "11";
                bb.medfee_sumamt = "333";
                bb.psn_setlway = "02";
                bb.mdtrt_id = "202011041723";
                bb.chrg_bchno = "202011041111";
                bb.acct_used_flag = "1";
                bb.insutype = "310";
                bb.mdtrt_mode = "0";
                aa.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2206.Mdtrtinfo();
                aa.mdtrtinfo = bb;
                returnvalue = qq.CallService(aa, ref outParam, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 入院办理2401
            {
                InPatient2401 inPatient2401 = new InPatient2401();
                Models.Request.RequestGzsiModel2401 requestGzsiModel2401 = new API.GZSI.Models.Request.RequestGzsiModel2401();
                Models.Response.ResponseGzsiModel2401 responseGzsiModel2401 = new API.GZSI.Models.Response.ResponseGzsiModel2401();
                Models.Request.RequestGzsiModel2401.Mdtrtinfo mdtrtinfo2401 = new API.GZSI.Models.Request.RequestGzsiModel2401.Mdtrtinfo();
                mdtrtinfo2401.psn_no = "1000753288";
                mdtrtinfo2401.insutype = "310";
                mdtrtinfo2401.begntime = "2020-11-02 21:42:39";
                mdtrtinfo2401.mdtrt_cert_type = "02";
                mdtrtinfo2401.med_type = "21";
                mdtrtinfo2401.ipt_otp_no = "1023";
                mdtrtinfo2401.atddr_no = "009999";
                mdtrtinfo2401.chfpdr_name = "信息科";
                mdtrtinfo2401.adm_diag_dscr = "泌尿";
                mdtrtinfo2401.adm_dept_codg = "3105";
                mdtrtinfo2401.adm_dept_name = "内一科";
                mdtrtinfo2401.adm_bed = "4047";
                mdtrtinfo2401.dscg_maindiag_code = "OYBDM06.291";
                mdtrtinfo2401.dscg_maindiag_name = "并发症诊断";
                mdtrtinfo2401.clnc_flag = "1";
                mdtrtinfo2401.er_flag = "1";
                mdtrtinfo2401.matn_type = "1";
                mdtrtinfo2401.birctrl_type = "1";
                mdtrtinfo2401.latechb_flag = "0";
                mdtrtinfo2401.pret_flag = "1";
                requestGzsiModel2401.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2401.Mdtrtinfo();
                requestGzsiModel2401.mdtrtinfo = mdtrtinfo2401;
                Models.Agnterinfo agnterinfo = new Models.Agnterinfo();
                agnterinfo.agnter_name = "阿黄";
                agnterinfo.agnter_rlts = "1";
                agnterinfo.agnter_cert_type = "01";
                agnterinfo.agnter_certno = "440125195909091731";
                agnterinfo.agnter_tel = "111";
                requestGzsiModel2401.agnterinfo = agnterinfo;
                Models.Request.RequestGzsiModel2401.Diseinfo diseinfo2401 = new API.GZSI.Models.Request.RequestGzsiModel2401.Diseinfo();
                diseinfo2401.adm_cond = "入院病情描述";
                diseinfo2401.diag_code = "OYBDM06.291";
                diseinfo2401.diag_dept = "19";
                diseinfo2401.diag_name = "肺炎";
                diseinfo2401.diag_srt_no = "0";
                diseinfo2401.diag_time = "2020-10-14 14:40:54";
                diseinfo2401.diag_type = "03";
                diseinfo2401.dise_dor_name = "信息科";
                diseinfo2401.dise_dor_no = "009999";
                diseinfo2401.maindiag_flag = "1";
                diseinfo2401.psn_no = "1000753288";
                requestGzsiModel2401.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel2401.Diseinfo>();
                requestGzsiModel2401.diseinfo.Add(diseinfo2401);
                returnvalue = inPatient2401.CallService(requestGzsiModel2401, ref responseGzsiModel2401, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            #region 入院办理撤销2404
            {
                InPatient2404 inPatient2404 = new InPatient2404();
                Models.Request.RequestGzsiModel2404 requestGzsiModel2404 = new API.GZSI.Models.Request.RequestGzsiModel2404();
                Models.Response.ResponseGzsiModel2404 responseGzsiModel2404 = new API.GZSI.Models.Response.ResponseGzsiModel2404();
                requestGzsiModel2404.mdtrt_id = "202011031185";
                requestGzsiModel2404.psn_no = "1000753288";
                returnvalue = inPatient2404.CallService(requestGzsiModel2404, ref responseGzsiModel2404, SerialNumber, strTransVersion, strVerifyCode);

                if (ret > 0)
                {
                    return;
                }
            }
            #endregion

            return;


        }

        /// <summary>
        /// 确定返回患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Models.Baseinfo pInfo = null;
            Models.SpInfo spInfo = null;
            Models.Insuinfo insuinfo = null;

            //if (string.IsNullOrEmpty(this.hcard_basinfo) || string.IsNullOrEmpty(this.hcard_chkinfo))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg("请读医保卡！", 111);
            //    return;
            //}

            //人员信息
            if (this.fpBaseinfo_Sheet1.RowCount == 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("医保人员信息为空！", 111);
                return;
            }

            pInfo = this.fpBaseinfo_Sheet1.ActiveRow.Tag as Models.Baseinfo;
            if (pInfo == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("医保人员信息为空！", 111);
                return;
            }

            //参保信息
            if (this.fpInsuinfo_Sheet1.RowCount > 0)
            {
                insuinfo = this.fpInsuinfo_Sheet1.ActiveRow.Tag as Models.Insuinfo;
                if (insuinfo == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("请选择参保信息！", 111);
                    return;
                }
            }

            //门特疾病信息
            if (this.fpSpinfo_Sheet1.RowCount > 0)
            {
                spInfo = this.fpSpinfo_Sheet1.ActiveRow.Tag as Models.SpInfo;
                if (spInfo == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("请选择门特疾病信息！", 111);
                    return;
                }
            }

            //资格类型
            if (this.cmbMatnType.Tag == null || string.IsNullOrEmpty(this.cmbMatnType.Tag.ToString()))
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择资格类型！", 111);
                return;
            }

            if (this.cmbMatnType.Tag.ToString() == "1" && (this.Patient.Name != pInfo.psn_name || this.patient.IDCard != pInfo.certno))
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("当前患者信息与医保信息不符！", 111);
                return;
            }

            if (this.cmbDiseCdg.Tag == null || string.IsNullOrEmpty(this.cmbDiseCdg.Tag.ToString()))
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择病种！", 111);
                return;
            }

            //结算类型
            if (this.cmbSetlWay.Tag == null || string.IsNullOrEmpty(this.cmbSetlWay.Tag.ToString()))
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择结算类型！", 111);
                return;
            }

            //确认选择
            string tips = "当前选中人员信息：\r\n\r\n"
                          + "姓名：" + pInfo.psn_name + "\r\n"
                          + "编码：" + pInfo.psn_no + "\r\n"
                          + "参保信息：" + insuinfo.psn_type + "\r\n\r\n"
                          + "确认选择请点击【是】否继续操作";
            DialogResult dr = FS.FrameWork.WinForms.Classes.Function.Msg(tips, 121);
            if (dr != DialogResult.OK)
            {
                return;
            }

            if (OperateType == "1")
            {
                //门诊
                FS.HISFC.Models.Registration.Register reg = this.Patient as FS.HISFC.Models.Registration.Register;
                if (reg != null)
                {
                    reg.SIMainInfo.Hcard_basinfo = this.hcard_basinfo; //读卡返回的社保卡基本信息
                    reg.SIMainInfo.Hcard_chkinfo = this.hcard_chkinfo; //持卡就诊登记许可号
                    reg.SIMainInfo.Psn_type = insuinfo.psn_type; //人员类别
                    reg.SIMainInfo.Insutype = insuinfo.insutype; //险种类型
                    reg.SIMainInfo.Psn_no = pInfo.psn_no; //个人编号
                    reg.SIMainInfo.Psn_name = pInfo.psn_name; //姓名
                    reg.SIMainInfo.Gend = pInfo.gend; //性别
                    reg.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(pInfo.brdy); //生日
                    reg.SIMainInfo.Insutype = insuinfo.insutype; //参保类型
                    reg.SIMainInfo.Mdtrt_cert_type = this.cmbMdtrtCertType.SelectedItem == null ? string.Empty : this.cmbMdtrtCertType.SelectedItem.ID; //就诊类型
                    reg.SIMainInfo.Certno = this.tbMdtrtCertNo.Text.Trim(); //就诊凭证编号
                    reg.SIMainInfo.Med_type = this.cmbMedType.Tag.ToString(); //医疗类别
                    reg.SIMainInfo.Dise_code = this.cmbDiseCdg.Tag.ToString(); //病种编码
                    reg.SIMainInfo.Dise_name = this.cmbDiseCdg.Text; //病种编码
                    reg.SIMainInfo.Psn_setlway = this.cmbSetlWay.Tag.ToString(); //结算类型
                    reg.SIMainInfo.Insuplc_admdvs = insuinfo.insuplc_admdvs;

                }

            }
            else if (OperateType == "2")
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.Patient as FS.HISFC.Models.RADT.PatientInfo;
                //住院
                patientInfo.SIMainInfo.Hcard_basinfo = this.hcard_basinfo; //读卡返回的社保卡基本信息
                patientInfo.SIMainInfo.Hcard_chkinfo = this.hcard_chkinfo; //持卡就诊登记许可号
                patientInfo.SIMainInfo.Psn_type = insuinfo.psn_type; //人员类别
                patientInfo.SIMainInfo.Insutype = insuinfo.insutype; //险种类型
                patientInfo.SIMainInfo.Psn_no = pInfo.psn_no; //个人编号
                patientInfo.SIMainInfo.Psn_name = pInfo.psn_name; //姓名
                patientInfo.SIMainInfo.Gend = pInfo.gend; //性别
                patientInfo.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(pInfo.brdy); //生日
                patientInfo.SIMainInfo.Insutype = insuinfo.insutype; //参保类型
                patientInfo.SIMainInfo.Mdtrt_cert_type = this.cmbMdtrtCertType.SelectedItem == null ? string.Empty : this.cmbMdtrtCertType.SelectedItem.ID; //就诊类型
                patientInfo.SIMainInfo.Certno = this.tbMdtrtCertNo.Text.Trim(); //就诊凭证编号
                patientInfo.SIMainInfo.Med_type = this.cmbMedType.Tag.ToString(); //医疗类别
                patientInfo.SIMainInfo.Dise_code = this.cmbDiseCdg.Tag.ToString(); //病种编码
                patientInfo.SIMainInfo.Dise_name = this.cmbDiseCdg.Text; //病种编码
                patientInfo.SIMainInfo.Psn_setlway = this.cmbSetlWay.Tag.ToString(); //结算类型
                patientInfo.SIMainInfo.Insuplc_admdvs = insuinfo.insuplc_admdvs;

                // {3CCAB886-9648-4DE8-B5DC-276D9212353F}
                patientInfo.SIMainInfo.Repeat_ipt_flag = this.repeat_ipt_flag.Checked ? "1" : "0";
                patientInfo.SIMainInfo.Ttp_resp = this.ttp_resp.Checked ? "1" : "0";
                patientInfo.SIMainInfo.Merg_setl_flag = this.merg_setl_flag.Checked ? "1" : "0";


                //入院登记需设置诊断
                //patientInfo.Diagnoses = this.localMgr.InpatientDiagnoseList(patient.ID);
                if (patientInfo.Diagnoses == null || patientInfo.Diagnoses.Count == 0)
                {
                    FS.HISFC.Models.RADT.Diagnose diagTemp = new FS.HISFC.Models.RADT.Diagnose();
                    diagTemp.ID = patientInfo.ExtendFlag2;
                    diagTemp.Name = patientInfo.ClinicDiagnose;
                    diagTemp.ICD10.ID = patientInfo.ExtendFlag2;
                    diagTemp.ICD10.Name = patientInfo.ClinicDiagnose;
                    patientInfo.Diagnoses.Add(diagTemp);
                }
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("未知的就诊类型！", 111);
                return;
            }

            this.Patient.Sex.ID = pInfo.gend == "1" ? "M" : pInfo.gend == "2" ? "F" : string.Empty; //性别
            this.Patient.Birthday = Common.Function.StringYYYYMMDDToDateTime(pInfo.brdy); //出生日期
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 选择基本信息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpBaseinfo_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.fpSpinfo_Sheet1.RowCount = 0;
            Models.Baseinfo pInfo = this.fpBaseinfo_Sheet1.ActiveRow.Tag as Models.Baseinfo;
            Models.Insuinfo insuinfo = this.fpInsuinfo_Sheet1.ActiveRow.Tag as Models.Insuinfo;

            PatientService1160 patientService1160 = new PatientService1160();
            Models.Request.RequestGzsiModel1160 inParam = new API.GZSI.Models.Request.RequestGzsiModel1160();
            Models.Response.ResponseGzsiModel1160 outParam = new Models.Response.ResponseGzsiModel1160();
            Models.Request.RequestGzsiModel1160.Data bb = new Models.Request.RequestGzsiModel1160.Data();
            bb.psn_no = pInfo.psn_no;
            bb.med_type = insuinfo.psn_type;//res.output[0].insuinfo[0].psn_type
            inParam.data = bb;
            returnvalue = patientService1160.CallService(inParam, ref outParam, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show(patientService1160.ErrorMsg);
                return;
            }

            ShowDataDetails(outParam);

        }

        #endregion

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <returns></returns>
        private int QueryData()
        {
            this.Clear();
            int returnvalue = 0;
            PatientService1101 patientService1101 = new PatientService1101();
            Models.Request.RequestGzsiModel1101 requestGzsiModel1101 = new API.GZSI.Models.Request.RequestGzsiModel1101();
            Models.Response.ResponseGzsiModel1101 responseGzsiModel1101 = new Models.Response.ResponseGzsiModel1101();
            Models.Request.RequestGzsiModel1101.Data data = new API.GZSI.Models.Request.RequestGzsiModel1101.Data();

            data.mdtrt_cert_type = this.cmbMdtrtCertType.Tag.ToString();//就诊凭证类型
            data.mdtrt_cert_no = data.mdtrt_cert_type == "03" ? this.cardNO : this.tbMdtrtCertNo.Text.Trim(); //凭证号码
            //卡识别码，凭证类型为03时必填
            data.card_sn = data.mdtrt_cert_type == "03" ? this.card_sn : "";
            data.psn_cert_type = "";// this.cmbMdtrtCertType.Tag.ToString();//就诊类型
            data.psn_name = this.Patient.Name;
            data.certno = this.tbMdtrtCertNo.Text.Trim(); //证件号
            requestGzsiModel1101.data = data;

            returnvalue = patientService1101.CallService(requestGzsiModel1101, ref responseGzsiModel1101, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show(patientService1101.ErrorMsg);
                return -1;
            }

            this.ShowData(responseGzsiModel1101);
            return 1;
        }

        /// <summary>
        /// 患者信息展示
        /// </summary>
        /// <param name="rb"></param>
        private void ShowData(Models.Response.ResponseGzsiModel1101 rb)
        {
            res = rb as Models.Response.ResponseGzsiModel1101;

            if (rb.output == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("获取人员信息失败！", 111);
                return;
            }

            if (rb.output.Count < 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("没有数据！", 111);
                return;
            }
            else
            {
                List<Baseinfo> baseinfo = new List<Baseinfo>();
                for (int i = 0; i < rb.output.Count; i++)
                {
                    Baseinfo Row = new Baseinfo();
                    Row.psn_no = rb.output[i].baseinfo.psn_no;
                    Row.psn_cert_type = rb.output[i].baseinfo.psn_cert_type;
                    Row.certno = rb.output[i].baseinfo.certno;
                    Row.gend = rb.output[i].baseinfo.gend;
                    Row.naty = rb.output[i].baseinfo.naty;
                    Row.psn_name = rb.output[i].baseinfo.psn_name;
                    Row.brdy = rb.output[i].baseinfo.brdy;
                    Row.age = rb.output[i].baseinfo.age;
                    baseinfo.Add(Row);
                    Common.Function.ShowOutDateToFarpoint<Models.Insuinfo>(this.fpInsuinfo_Sheet1, rb.output[i].insuinfo);
                    Common.Function.ShowOutDateToFarpoint<Models.Idetinfo>(this.fpIdetinfo_Sheet1, rb.output[i].idetinfo);
                }
                Common.Function.ShowOutDateToFarpoint<Models.Baseinfo>(this.fpBaseinfo_Sheet1, baseinfo);
            }
        }

        /// <summary>
        /// 特殊待遇信息
        /// </summary>
        /// <param name="rb"></param>
        private void ShowDataDetails(Models.Response.ResponseGzsiModel1160 rb)
        {
            if (rb.output != null && rb.output.spinfo != null && rb.output.spinfo.Count > 0)
            {
                List<SpInfo> spInfoList = new List<SpInfo>();
                for (int i = 0; i < rb.output.spinfo.Count; i++)
                {
                    SpInfo Row = new SpInfo();
                    Row.psn_trt_no = rb.output.spinfo[i].psn_trt_no;
                    Row.psn_trt_type = rb.output.spinfo[i].psn_trt_type;
                    Row.psn_trt_type_name = rb.output.spinfo[i].psn_trt_type_name;
                    Row.dise_no = rb.output.spinfo[i].dise_no;
                    Row.dise_name = rb.output.spinfo[i].dise_name;
                    Row.begntime = rb.output.spinfo[i].begntime;
                    Row.endtime = rb.output.spinfo[i].endtime;
                    spInfoList.Add(Row);
                }
                Common.Function.ShowOutDateToFarpoint<Models.SpInfo>(this.fpSpinfo_Sheet1, spInfoList);
            }
        }

        /// <summary>
        /// 所有信息
        /// </summary>
        private void Clear()
        {
            this.fpBaseinfo_Sheet1.RowCount = 0;
            this.fpInsuinfo_Sheet1.RowCount = 0;
            this.fpIdetinfo_Sheet1.RowCount = 0;
            this.fpSpinfo_Sheet1.RowCount = 0;
        }

        #region 读卡接口方法

        //返回值 0表示成功；非0表示失败。
        //初始化，每次调用读卡其他方法时调用一次
        [DllImport(@".\Plugins\SI\SSCard.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public extern static int Init(string pUrl, string pUser);

        //读取社保卡接口
        [DllImport(@".\Plugins\SI\SSCard.dll", CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "ReadCardBasGZ")]
        public extern static int ReadCardBasGZ(StringBuilder pOutBuff, int nOutBuffLen, StringBuilder pSignBuff, int nSignBuffLen);

        //读取医保卡接口
        [DllImport(@".\Plugins\SI\SSCard.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public extern static int ReadMedicalCardGZ(StringBuilder pOutBuff, int nOutBuffLen, StringBuilder pSignBuff, int nSignBuffLen);

        //读取身份证接口
        [DllImport(@".\Plugins\SI\SSCard.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public extern static int ReadIDCardGZ(StringBuilder pOutBuff, int nOutBuffLen, StringBuilder pSignBuff, int nSignBuffLen);

        //读取二维码信息
        [DllImport(@".\Plugins\SI\SSCard.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public extern static int GetTwoBarCodes(int nTimeout, StringBuilder pOutBuff, int nOutBuffLen, StringBuilder pSignBuff, int nSignBuffLen);

        #endregion

        private void cmbInsuplc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Models.UserInfo.Instance.insuplc = this.cmbInsuplc.Tag.ToString();
        }
    }
}
