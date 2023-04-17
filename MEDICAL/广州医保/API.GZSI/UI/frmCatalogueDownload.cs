﻿using System;
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
using ICSharpCode.SharpZipLib.Zip;
namespace API.GZSI.UI
{
    public partial class frmCatalogueDownload : Form
    {
        /// <summary>
        /// 业务回滚管理类
        /// </summary>
        private RollbackManager rollbackMgr = new RollbackManager();

        int returnvalue = 0;
        string SerialNumber = string.Empty;//交易流水号
        string strTransVersion = string.Empty;//交易版本号
        string strVerifyCode = string.Empty;//交易验证码

        public frmCatalogueDownload()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is FS.FrameWork.WinForms.Controls.NeuButton)
                {
                    FS.FrameWork.WinForms.Controls.NeuButton btn = control as FS.FrameWork.WinForms.Controls.NeuButton;
                    btn.Click += new EventHandler(btn_Click);
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Controls.NeuButton btn = sender as FS.FrameWork.WinForms.Controls.NeuButton;
            string file_qury_no = "";
            string filename = "";
            string dld_end_time = "";
            string data_cnt = "";
            switch (btn.Tag.ToString())
            {
                case "1301":
                    CommonService1301 commonService1301 = new CommonService1301();
                    Models.Request.RequestGzsiModel1301 requestGzsiModel1301 = new API.GZSI.Models.Request.RequestGzsiModel1301();
                    Models.Response.ResponseGzsiModel1301 responseGzsiModel1301 = new API.GZSI.Models.Response.ResponseGzsiModel1301();
                    Models.Request.RequestGzsiModel1301.Data data1301 = new API.GZSI.Models.Request.RequestGzsiModel1301.Data();
                    data1301.ver = "0";
                    requestGzsiModel1301.data = data1301;
                    returnvalue = commonService1301.CallService(requestGzsiModel1301, ref responseGzsiModel1301, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1301.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1301.output.file_qury_no;
                    filename = responseGzsiModel1301.output.filename;
                    dld_end_time = responseGzsiModel1301.output.dld_end_time;
                    data_cnt = responseGzsiModel1301.output.data_cnt;
                    break;
                case "1302":
                    CommonService1302 commonService1302 = new CommonService1302();
                    Models.Request.RequestGzsiModel1302 requestGzsiModel1302 = new API.GZSI.Models.Request.RequestGzsiModel1302();
                    Models.Response.ResponseGzsiModel1302 responseGzsiModel1302 = new API.GZSI.Models.Response.ResponseGzsiModel1302();

                    Models.Request.RequestGzsiModel1302.Data data1302 = new API.GZSI.Models.Request.RequestGzsiModel1302.Data();
                    data1302.ver = "0";
                    requestGzsiModel1302.data = data1302;
                    returnvalue = commonService1302.CallService(requestGzsiModel1302, ref responseGzsiModel1302, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1302.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1302.output.file_qury_no;
                    filename = responseGzsiModel1302.output.filename;
                    dld_end_time = responseGzsiModel1302.output.dld_end_time;
                    data_cnt = responseGzsiModel1302.output.data_cnt;
                    break;
                case "1303":
                    CommonService1303 commonService1303 = new CommonService1303();
                    Models.Request.RequestGzsiModel1303 requestGzsiModel1303 = new API.GZSI.Models.Request.RequestGzsiModel1303();
                    Models.Response.ResponseGzsiModel1303 responseGzsiModel1303 = new API.GZSI.Models.Response.ResponseGzsiModel1303();

                    Models.Request.RequestGzsiModel1303.Data data1303 = new API.GZSI.Models.Request.RequestGzsiModel1303.Data();
                    data1303.ver = "0";
                    requestGzsiModel1303.data = data1303;
                    returnvalue = commonService1303.CallService(requestGzsiModel1303, ref responseGzsiModel1303, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1303.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1303.output.file_qury_no;
                    filename = responseGzsiModel1303.output.filename;
                    dld_end_time = responseGzsiModel1303.output.dld_end_time;
                    data_cnt = responseGzsiModel1303.output.data_cnt;
                    break;
                case "1305":
                    CommonService1305 commonService1305 = new CommonService1305();
                    Models.Request.RequestGzsiModel1305 requestGzsiModel1305 = new API.GZSI.Models.Request.RequestGzsiModel1305();
                    Models.Response.ResponseGzsiModel1305 responseGzsiModel1305 = new API.GZSI.Models.Response.ResponseGzsiModel1305();

                    Models.Request.RequestGzsiModel1305.Data data1305 = new API.GZSI.Models.Request.RequestGzsiModel1305.Data();
                    data1305.ver = "0";
                    requestGzsiModel1305.data = data1305;
                    returnvalue = commonService1305.CallService(requestGzsiModel1305, ref responseGzsiModel1305, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1305.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1305.output.file_qury_no;
                    filename = responseGzsiModel1305.output.filename;
                    dld_end_time = responseGzsiModel1305.output.dld_end_time;
                    data_cnt = responseGzsiModel1305.output.data_cnt;
                    break;
                case "1306":
                    CommonService1306 commonService1306 = new CommonService1306();
                    Models.Request.RequestGzsiModel1306 requestGzsiModel1306 = new API.GZSI.Models.Request.RequestGzsiModel1306();
                    Models.Response.ResponseGzsiModel1306 responseGzsiModel1306 = new API.GZSI.Models.Response.ResponseGzsiModel1306();

                    Models.Request.RequestGzsiModel1306.Data data1306 = new API.GZSI.Models.Request.RequestGzsiModel1306.Data();
                    data1306.ver = "0";
                    requestGzsiModel1306.data = data1306;
                    returnvalue = commonService1306.CallService(requestGzsiModel1306, ref responseGzsiModel1306, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1306.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1306.output.file_qury_no;
                    filename = responseGzsiModel1306.output.filename;
                    dld_end_time = responseGzsiModel1306.output.dld_end_time;
                    data_cnt = responseGzsiModel1306.output.data_cnt;
                    break;
                case "1307":
                    CommonService1307 commonService1307 = new CommonService1307();
                    Models.Request.RequestGzsiModel1307 requestGzsiModel1307 = new API.GZSI.Models.Request.RequestGzsiModel1307();
                    Models.Response.ResponseGzsiModel1307 responseGzsiModel1307 = new API.GZSI.Models.Response.ResponseGzsiModel1307();

                    Models.Request.RequestGzsiModel1307.Data data1307 = new API.GZSI.Models.Request.RequestGzsiModel1307.Data();
                    data1307.ver = "0";
                    requestGzsiModel1307.data = data1307;
                    returnvalue = commonService1307.CallService(requestGzsiModel1307, ref responseGzsiModel1307, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1307.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1307.output.file_qury_no;
                    filename = responseGzsiModel1307.output.filename;
                    dld_end_time = responseGzsiModel1307.output.dld_end_time;
                    data_cnt = responseGzsiModel1307.output.data_cnt;
                    break;
                case "1308":
                    CommonService1308 commonService1308 = new CommonService1308();
                    Models.Request.RequestGzsiModel1308 requestGzsiModel1308 = new API.GZSI.Models.Request.RequestGzsiModel1308();
                    Models.Response.ResponseGzsiModel1308 responseGzsiModel1308 = new API.GZSI.Models.Response.ResponseGzsiModel1308();

                    Models.Request.RequestGzsiModel1308.Data data1308 = new API.GZSI.Models.Request.RequestGzsiModel1308.Data();
                    data1308.ver = "0";
                    requestGzsiModel1308.data = data1308;
                    returnvalue = commonService1308.CallService(requestGzsiModel1308, ref responseGzsiModel1308, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1308.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1308.output.file_qury_no;
                    filename = responseGzsiModel1308.output.filename;
                    dld_end_time = responseGzsiModel1308.output.dld_end_time;
                    data_cnt = responseGzsiModel1308.output.data_cnt;
                    break;
                case "1309":
                    CommonService1309 commonService1309 = new CommonService1309();
                    Models.Request.RequestGzsiModel1309 requestGzsiModel1309 = new API.GZSI.Models.Request.RequestGzsiModel1309();
                    Models.Response.ResponseGzsiModel1309 responseGzsiModel1309 = new API.GZSI.Models.Response.ResponseGzsiModel1309();

                    Models.Request.RequestGzsiModel1309.Data data1309 = new API.GZSI.Models.Request.RequestGzsiModel1309.Data();
                    data1309.ver = "0";
                    requestGzsiModel1309.data = data1309;
                    returnvalue = commonService1309.CallService(requestGzsiModel1309, ref responseGzsiModel1309, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1309.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1309.output.file_qury_no;
                    filename = responseGzsiModel1309.output.filename;
                    dld_end_time = responseGzsiModel1309.output.dld_end_time;
                    data_cnt = responseGzsiModel1309.output.data_cnt;
                    break;
                case "1310":
                    CommonService1310 commonService1310 = new CommonService1310();
                    Models.Request.RequestGzsiModel1310 requestGzsiModel1310 = new API.GZSI.Models.Request.RequestGzsiModel1310();
                    Models.Response.ResponseGzsiModel1310 responseGzsiModel1310 = new API.GZSI.Models.Response.ResponseGzsiModel1310();

                    Models.Request.RequestGzsiModel1310.Data data1310 = new API.GZSI.Models.Request.RequestGzsiModel1310.Data();
                    data1310.ver = "0";
                    requestGzsiModel1310.data = data1310;
                    returnvalue = commonService1310.CallService(requestGzsiModel1310, ref responseGzsiModel1310, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1310.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1310.output.file_qury_no;
                    filename = responseGzsiModel1310.output.filename;
                    dld_end_time = responseGzsiModel1310.output.dld_end_time;
                    data_cnt = responseGzsiModel1310.output.data_cnt;
                    break;
                case "1311":
                    CommonService1311 commonService1311 = new CommonService1311();
                    Models.Request.RequestGzsiModel1311 requestGzsiModel1311 = new API.GZSI.Models.Request.RequestGzsiModel1311();
                    Models.Response.ResponseGzsiModel1311 responseGzsiModel1311 = new API.GZSI.Models.Response.ResponseGzsiModel1311();

                    Models.Request.RequestGzsiModel1311.Data data1311 = new API.GZSI.Models.Request.RequestGzsiModel1311.Data();
                    data1311.ver = "0";
                    requestGzsiModel1311.data = data1311;
                    returnvalue = commonService1311.CallService(requestGzsiModel1311, ref responseGzsiModel1311, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1311.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1311.output.file_qury_no;
                    filename = responseGzsiModel1311.output.filename;
                    dld_end_time = responseGzsiModel1311.output.dld_end_time;
                    data_cnt = responseGzsiModel1311.output.data_cnt;
                    break;
                case "1313":
                    CommonService1313 commonService1313 = new CommonService1313();
                    Models.Request.RequestGzsiModel1313 requestGzsiModel1313 = new API.GZSI.Models.Request.RequestGzsiModel1313();
                    Models.Response.ResponseGzsiModel1313 responseGzsiModel1313 = new API.GZSI.Models.Response.ResponseGzsiModel1313();

                    Models.Request.RequestGzsiModel1313.Data data1313 = new API.GZSI.Models.Request.RequestGzsiModel1313.Data();
                    data1313.ver = "0";
                    requestGzsiModel1313.data = data1313;
                    returnvalue = commonService1313.CallService(requestGzsiModel1313, ref responseGzsiModel1313, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1313.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1313.output.file_qury_no;
                    filename = responseGzsiModel1313.output.filename;
                    dld_end_time = responseGzsiModel1313.output.dld_end_time;
                    data_cnt = responseGzsiModel1313.output.data_cnt;
                    break;
                case "1314":
                    CommonService1314 commonService1314 = new CommonService1314();
                    Models.Request.RequestGzsiModel1314 requestGzsiModel1314 = new API.GZSI.Models.Request.RequestGzsiModel1314();
                    Models.Response.ResponseGzsiModel1314 responseGzsiModel1314 = new API.GZSI.Models.Response.ResponseGzsiModel1314();

                    Models.Request.RequestGzsiModel1314.Data data1314 = new API.GZSI.Models.Request.RequestGzsiModel1314.Data();
                    data1314.ver = "0";
                    requestGzsiModel1314.data = data1314;
                    returnvalue = commonService1314.CallService(requestGzsiModel1314, ref responseGzsiModel1314, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1314.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1314.output.file_qury_no;
                    filename = responseGzsiModel1314.output.filename;
                    dld_end_time = responseGzsiModel1314.output.dld_end_time;
                    data_cnt = responseGzsiModel1314.output.data_cnt;
                    break;
                case "1315":
                    CommonService1315 commonService1315 = new CommonService1315();
                    Models.Request.RequestGzsiModel1315 requestGzsiModel1315 = new API.GZSI.Models.Request.RequestGzsiModel1315();
                    Models.Response.ResponseGzsiModel1315 responseGzsiModel1315 = new API.GZSI.Models.Response.ResponseGzsiModel1315();

                    Models.Request.RequestGzsiModel1315.Data data1315 = new API.GZSI.Models.Request.RequestGzsiModel1315.Data();
                    data1315.ver = "0";
                    requestGzsiModel1315.data = data1315;
                    returnvalue = commonService1315.CallService(requestGzsiModel1315, ref responseGzsiModel1315, SerialNumber, strTransVersion, strVerifyCode);
                    if (returnvalue == -1)
                    {
                        MessageBox.Show("获取目录失败：" + commonService1315.ErrorMsg);
                        return;
                    }

                    file_qury_no = responseGzsiModel1315.output.file_qury_no;
                    filename = responseGzsiModel1315.output.filename;
                    dld_end_time = responseGzsiModel1315.output.dld_end_time;
                    data_cnt = responseGzsiModel1315.output.data_cnt;
                    break;
                default:
                    return;
            }

            this.DownloadCatalogue(file_qury_no, filename, dld_end_time, data_cnt, btn.Tag.ToString());
        }

        private void DownloadCatalogue(string file_qury_no, string filename, string dld_end_time, string data_cnt, string tag)
        {
            CommonService9102 commonService9102 = new CommonService9102();
            Models.Request.RequestGzsiModel9102 requestGzsiModel9102 = new API.GZSI.Models.Request.RequestGzsiModel9102();
            Models.Response.ResponseGzsiModel9102 responseGzsiModel9102 = new API.GZSI.Models.Response.ResponseGzsiModel9102();
            Models.Request.RequestGzsiModel9102.FsDownloadIn data9102 = new API.GZSI.Models.Request.RequestGzsiModel9102.FsDownloadIn();
            data9102.fixmedins_code = Models.UserInfo.Instance.fixmedins_code;
            data9102.file_qury_no = file_qury_no;
            data9102.filename = filename;
            requestGzsiModel9102.fsDownloadIn = new API.GZSI.Models.Request.RequestGzsiModel9102.FsDownloadIn();
            requestGzsiModel9102.fsDownloadIn = data9102;
            returnvalue = commonService9102.CallService(requestGzsiModel9102, ref responseGzsiModel9102, SerialNumber, strTransVersion, strVerifyCode);
            if (returnvalue == -1)
            {
                MessageBox.Show("下载目录失败：" + commonService9102.ErrorMsg);
                return;
            }
            string strPdfPath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "9102" + ".ZIP";
            string fileNames = UnZipFile(strPdfPath);
            switch (tag)
            {
                case "1301":
                    InsertYB1301(fileNames);
                    break;
                case "1302":
                    InsertYB1302(fileNames);
                    break;
                case "1303":
                    InsertYB1303(fileNames);
                    break;
                case "1305":
                    InsertYB1305(fileNames);
                    break;
                case "1306":
                    InsertYB1306(fileNames);
                    break;
                case "1307":
                    InsertYB1307(fileNames);
                    break;
                case "1308":
                    InsertYB1308(fileNames);
                    break;
                case "1309":
                    InsertYB1309(fileNames);
                    break;
                case "1310":
                    InsertYB1310(fileNames);
                    break;
                case "1311":
                    InsertYB1311(fileNames);
                    break;
                case "1313":
                    InsertYB1313(fileNames);
                    break;
                case "1314":
                    InsertYB1314(fileNames);
                    break;
                case "1315":
                    InsertYB1315(fileNames);
                    break;
                default:
                    return;
            }

        }

        #region 插入数据
        public int InsertYB1301(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            //int updateCount = 0;
            int count = lmr.QueryYB1301();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("西药中成药目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1301();

                }
                else
                {
                    return 1;
                }
            }

            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1301 YB_1301 = new FS.HISFC.Models.SIInterface.YB_1301();
                    YB_1301.med_list_codg = values[0].ToString();//医疗目录编码
                    YB_1301.drug_prodname = values[1].ToString();//药品商品名
                    YB_1301.genname_codg = values[2].ToString();//通用名编号
                    YB_1301.drug_genname = values[3].ToString();//药品通用名
                    YB_1301.chemname = values[4].ToString();//化学名称
                    YB_1301.alis = values[5].ToString();//别名
                    YB_1301.eng_name = values[6].ToString();//英文名称
                    YB_1301.reg_name = values[7].ToString();//注册名称
                    YB_1301.drugadm_strdcod = values[8].ToString();//药监本位码
                    YB_1301.drug_dosform = values[9].ToString();//药品剂型
                    YB_1301.drug_dosform_name = values[10].ToString();//药品剂型名称
                    YB_1301.drug_type = values[11].ToString();//药品类别
                    YB_1301.drug_type_name = values[12].ToString();//药品类别名称
                    YB_1301.drug_spec = values[13].ToString();//药品规格
                    YB_1301.drug_spec_code = values[14].ToString();//药品规格代码
                    YB_1301.reg_dosform = values[15].ToString();//注册剂型
                    YB_1301.reg_spec = values[16].ToString();//注册规格
                    YB_1301.reg_spec_code = values[17].ToString();//注册规格代码
                    YB_1301.each_dos = values[18].ToString();//每次用量
                    YB_1301.used_frqu = values[19].ToString();//使用频次
                    YB_1301.acdbas = values[20].ToString();//酸根盐基
                    YB_1301.nat_drug_no = values[21].ToString();//国家药品编号
                    YB_1301.used_mtd = values[22].ToString();//用法
                    YB_1301.tcmpat_flag = values[23].ToString();//中成药标志
                    YB_1301.prodplac_type = values[24].ToString();//生产地类别
                    YB_1301.prodplac_type_name = values[25].ToString();//生产地类别名称
                    YB_1301.prcunt_type = values[26].ToString();//计价单位类型
                    YB_1301.otc_flag = values[27].ToString();//非处方药标志
                    YB_1301.otc_flag_name = values[28].ToString();//非处方药标志名称
                    YB_1301.pacmatl = values[29].ToString();//包装材质
                    YB_1301.pacmatl_name = values[30].ToString();//包装材质名称
                    YB_1301.pacspec = values[31].ToString();//包装规格
                    YB_1301.pac_cnt = values[32].ToString();//包装数量
                    YB_1301.efcc_atd = values[33].ToString();//功能主治
                    YB_1301.rute = values[34].ToString();//给药途径
                    YB_1301.manl = values[35].ToString();//说明书
                    YB_1301.begndate = values[36].ToString();//开始日期
                    YB_1301.enddate = values[37].ToString();//结束日期
                    YB_1301.min_useunt = values[38].ToString();//最小使用单位
                    YB_1301.min_salunt = values[39].ToString();//最小销售单位
                    YB_1301.min_unt = values[40].ToString();//最小计量单位
                    YB_1301.min_pac_cnt = values[41].ToString();//最小包装数量
                    YB_1301.min_pacunt = values[42].ToString();//最小包装单位
                    YB_1301.min_prepunt = values[43].ToString();//最小制剂单位
                    YB_1301.min_pacunt_name = values[44].ToString();//最小包装单位名称
                    YB_1301.min_prepunt_name = values[45].ToString();//最小制剂单位名称
                    YB_1301.convrat = values[46].ToString();//转换比
                    YB_1301.drug_expy = values[47].ToString();//药品有效期
                    YB_1301.min_prcunt = values[48].ToString();//最小计价单位
                    YB_1301.wubi = values[49].ToString();//五笔助记码
                    YB_1301.pinyin = values[50].ToString();//拼音助记码
                    YB_1301.subpck_fcty = values[51].ToString();//分包装厂家
                    YB_1301.prodentp_code = values[52].ToString();//生产企业编号
                    YB_1301.prodentp_name = values[53].ToString();//生产企业名称
                    YB_1301.sp_lmtpric_drug_flag = values[54].ToString();//特殊限价药品标志
                    YB_1301.sp_drug_flag = values[55].ToString();//特殊药品标志
                    YB_1301.lmt_usescp = values[56].ToString();//限制使用范围
                    YB_1301.lmt_used_flag = values[57].ToString();//限制使用标志
                    YB_1301.drug_regcertno = values[58].ToString();//药品注册证号
                    YB_1301.drug_regcert_begndate = values[59].ToString();//药品注册证号开始日期
                    YB_1301.drug_regcert_enddate = values[60].ToString();//药品注册证号结束日期
                    YB_1301.aprvno = values[61].ToString();//批准文号
                    YB_1301.aprvno_begndate = values[62].ToString();//批准文号开始日期
                    YB_1301.aprvno_enddate = values[63].ToString();//批准文号结束日期
                    YB_1301.market_condition_code = values[64].ToString();//市场状态
                    YB_1301.market_condition_name = values[65].ToString();//市场状态名称
                    YB_1301.regdrug_elcword = values[66].ToString();//药品注册批件电子档案
                    YB_1301.regdrug_add_elcword = values[67].ToString();//药品补充申请批件电子档案
                    YB_1301.yb_drug_memo = values[68].ToString();//国家医保药品目录备注
                    YB_1301.drugbase_flagname = values[69].ToString();//基本药物标志名称
                    YB_1301.drugbase_flag = values[70].ToString();//基本药物标志
                    YB_1301.vat__adjust_drugflag = values[71].ToString();//增值税调整药品标志
                    YB_1301.vat__adjust_drugname = values[72].ToString();//增值税调整药品名称
                    YB_1301.drugslist_onmarket = values[73].ToString();//上市药品目录集药品
                    YB_1301.yb_negotiatedrug_flag = values[74].ToString();//医保谈判药品标志
                    YB_1301.yb_negotiatedrug_name = values[75].ToString();//医保谈判药品名称
                    YB_1301.nhc_drugcode = values[76].ToString();//卫健委药品编码
                    YB_1301.memo = values[77].ToString();//备注
                    YB_1301.vali_flag = values[78].ToString();//有效标志
                    YB_1301.record_num = values[79].ToString();//唯一记录号
                    YB_1301.create_time = Time(values[80].ToString());//数据创建时间
                    YB_1301.update_time = Time(values[81].ToString());//数据更新时间
                    YB_1301.ver_num = values[82].ToString();//版本号
                    YB_1301.ver_name = values[83].ToString();//版本名称
                    YB_1301.child_drug = values[84].ToString();//儿童用药
                    YB_1301.company = values[85].ToString();//公司名称
                    YB_1301.fda_about = values[86].ToString();//仿制药一致性评价药品
                    YB_1301.distribution_enterprise = values[87].ToString();//经销企业
                    YB_1301.de_linkname = values[88].ToString();//经销企业联系人
                    YB_1301.elefile_authorization_de = values[89].ToString();//经销企业授权书电子档案
                    YB_1301.yb_drug_catalogue = values[90].ToString();//国家医保药品目录剂型
                    YB_1301.yb_drug_abtype = values[91].ToString();//国家医保药品目录甲乙类标识

                    if (lmr.InsertYB1301(YB_1301) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }


                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }

        public int InsertYB1302(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1302();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("中药饮片目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1302();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1302 YB_1302 = new FS.HISFC.Models.SIInterface.YB_1302();
                    YB_1302.med_list_codg = values[0].ToString();//医疗目录编码
                    YB_1302.sindrug_name = values[1].ToString();//单味药名称
                    YB_1302.cpnd_flag = values[2].ToString();//单复方标志
                    YB_1302.qlt_lv = values[3].ToString();//质量等级
                    YB_1302.tcmdrug_year = values[4].ToString();//中草药年份
                    YB_1302.med_part = values[5].ToString();//药用部位
                    YB_1302.safe_mtr = values[6].ToString();//安全计量
                    YB_1302.cnvl_used = values[7].ToString();//常规用法
                    YB_1302.natfla = values[8].ToString();//性味
                    YB_1302.mertpm = values[9].ToString();//归经
                    YB_1302.cat = values[10].ToString();//品种
                    YB_1302.begndate = values[11].ToString();//开始日期
                    YB_1302.enddate = values[12].ToString();//结束日期
                    YB_1302.vali_flag = values[13].ToString();//有效标志
                    YB_1302.record_num = values[14].ToString();//唯一记录号
                    YB_1302.create_time = Time(values[15].ToString());//数据创建时间
                    YB_1302.update_time = Time(values[16].ToString());//数据更新时间
                    YB_1302.ver_num = values[17].ToString();//版本号
                    YB_1302.ver_name = values[18].ToString();//版本名称
                    YB_1302.drug_name = values[19].ToString();//药材名称
                    YB_1302.efcc_atd = values[20].ToString();//功能主治
                    YB_1302.processing_method = values[21].ToString();//炮制方法
                    YB_1302.functional_classification = values[22].ToString();//功效分类
                    YB_1302.source = values[23].ToString();//药材种来源
                    YB_1302.yb_gj_paypolicy = values[24].ToString();//国家医保支付政策
                    YB_1302.yb_sj_paypolicy = values[25].ToString();//省级医保支付政策
                    YB_1302.standard_name = values[26].ToString();//标准名称
                    YB_1302.standard_page = values[27].ToString();//标准页码
                    YB_1302.standard_elcword = values[28].ToString();//标准电子档案
                    if (lmr.InsertYB1302(YB_1302) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }


                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }

        public int InsertYB1303(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1303();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("医疗机构制剂目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1303();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1303 YB_1303 = new FS.HISFC.Models.SIInterface.YB_1303();
                    YB_1303.med_list_codg = values[0].ToString();//医疗目录编码
                    YB_1303.drug_prodname = values[1].ToString();//药品商品名
                    YB_1303.alis = values[2].ToString();//别名
                    YB_1303.eng_name = values[3].ToString();//英文名称
                    YB_1303.dosform = values[4].ToString();//剂型
                    YB_1303.dosform_name = values[5].ToString();//剂型名称
                    YB_1303.reg_dosform = values[6].ToString();//注册剂型
                    YB_1303.ing = values[7].ToString();//成分
                    YB_1303.efcc_atd = values[8].ToString();//功能主治
                    YB_1303.chrt = values[9].ToString();//性状
                    YB_1303.drug_spec = values[10].ToString();//药品规格
                    YB_1303.drug_spec_code = values[11].ToString();//药品规格代码
                    YB_1303.reg_spec = values[12].ToString();//注册规格
                    YB_1303.reg_spec_code = values[13].ToString();//注册规格代码
                    YB_1303.rute = values[14].ToString();//给药途径
                    YB_1303.stog = values[15].ToString();//贮藏
                    YB_1303.used_frqu_dscr = values[16].ToString();//使用频次
                    YB_1303.each_dos = values[17].ToString();//每次用量
                    YB_1303.drug_tye = values[18].ToString();//药品类别
                    YB_1303.drug_typename = values[19].ToString();//药品类别名称
                    YB_1303.otc_flag = values[20].ToString();//非处方药标志
                    YB_1303.otc_flag_name = values[21].ToString();//非处方药标志名称
                    YB_1303.pacmatl = values[22].ToString();//包装材质
                    YB_1303.pacmatl_name = values[23].ToString();//包装材质名称
                    YB_1303.pacspec = values[24].ToString();//包装规格
                    YB_1303.manl = values[25].ToString();//说明书
                    YB_1303.pac_cnt = values[26].ToString();//包装数量
                    YB_1303.min_useunt = values[27].ToString();//最小使用单位
                    YB_1303.min_salunt = values[28].ToString();//最小销售单位
                    YB_1303.min_unt = values[29].ToString();//最小计量单位
                    YB_1303.min_pac_cnt = values[30].ToString();//最小包装数量
                    YB_1303.min_pacunt = values[31].ToString();//最小包装单位
                    YB_1303.min_prepunt = values[32].ToString();//最小制剂单位
                    YB_1303.min_prepunt_name = values[33].ToString();//最小制剂单位名称
                    YB_1303.drug_expy = values[34].ToString();//药品有效期
                    YB_1303.min_prcunt = values[35].ToString();//最小计价单位
                    YB_1303.defs = values[36].ToString();//不良反应
                    YB_1303.mnan = values[37].ToString();//注意事项
                    YB_1303.tabo = values[38].ToString();//禁忌
                    YB_1303.manufacturer_num = values[39].ToString();//生产企业编号
                    YB_1303.manufacturer_name = values[40].ToString();//生产企业名称
                    YB_1303.manufacturer_add = values[41].ToString();//生产企业地址
                    YB_1303.sp_lmtpric_drug_flag = values[42].ToString();//特殊限价药品标志
                    YB_1303.aprvno = values[43].ToString();//批准文号
                    YB_1303.aprvno_begndate = values[44].ToString();//批准文号开始日期
                    YB_1303.aprvno_enddate = values[45].ToString();//批准文号结束日期
                    YB_1303.drug_regcertno = values[46].ToString();//药品注册证号
                    YB_1303.drug_regcert_begndate = values[47].ToString();//药品注册证号开始日期
                    YB_1303.drug_regcert_enddate = values[48].ToString();//药品注册证号结束日期
                    YB_1303.convrat = values[49].ToString();//转换比
                    YB_1303.lmt_usescp = values[50].ToString();//限制使用范围
                    YB_1303.minpackageuint_name = values[51].ToString();//最小包装单位名称
                    YB_1303.reg_name = values[52].ToString();//注册名称
                    YB_1303.subpck_fcty = values[53].ToString();//分包装厂家
                    YB_1303.market_condition = values[54].ToString();//市场状态
                    YB_1303.regdrug_elcword = values[55].ToString();//药品注册批件电子档案
                    YB_1303.regdrug_add_elcword = values[56].ToString();//药品补充申请批件电子档案
                    YB_1303.yb_drug_code = values[57].ToString();//国家医保药品目录编号
                    YB_1303.yb_drug_memo = values[58].ToString();//国家医保药品目录备注
                    YB_1303.vat_adjust_drugflag = values[59].ToString();//增值税调整药品标志
                    YB_1303.vat_adjust_drugname = values[60].ToString();//增值税调整药品名称
                    YB_1303.drugslist_onmarket = values[61].ToString();//上市药品目录集药品
                    YB_1303.nhc_drugcode = values[62].ToString();//卫健委药品编码
                    YB_1303.memo = values[63].ToString();//备注
                    YB_1303.vali_flag = values[64].ToString();//有效标志
                    YB_1303.begin_time = Time(values[65].ToString());//开始时间
                    YB_1303.end_time = Time(values[66].ToString());//结束时间
                    YB_1303.record_num = values[67].ToString();//唯一记录号
                    YB_1303.create_time = Time(values[68].ToString());//数据创建时间
                    YB_1303.update_time = Time(values[69].ToString());//数据更新时间
                    YB_1303.ver_num = values[70].ToString();//版本号
                    YB_1303.ver_name = values[71].ToString();//版本名称
                    YB_1303.homebrew_license_no = values[72].ToString();//自制剂许可证号
                    YB_1303.child_drug = values[73].ToString();//儿童用药
                    YB_1303.gpatient_drug = values[74].ToString();//老年患者用药
                    YB_1303.institution_linkname = values[75].ToString();//医疗机构联系人姓名
                    YB_1303.institution_linktel = values[76].ToString();//医疗机构联系人电话
                    YB_1303.homebrew_license_file = values[77].ToString();//自制剂许可证电子档案

                    if (lmr.InsertYB1303(YB_1303) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }


                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1305(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1305();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("医疗服务项目目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1305();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1305 YB_1305 = new FS.HISFC.Models.SIInterface.YB_1305();
                    YB_1305.med_list_codg = values[0].ToString();//医疗目录编码
                    YB_1305.prcunt = values[1].ToString();//计价单位
                    YB_1305.prcunt_name = values[2].ToString();//计价单位名称
                    YB_1305.trt_item_dscr = values[3].ToString();//诊疗项目说明
                    YB_1305.trt_exct_cont = values[4].ToString();//诊疗除外内容
                    YB_1305.trt_item_cont = values[5].ToString();//诊疗项目内涵
                    YB_1305.vali_flag = values[6].ToString();//有效标志
                    YB_1305.memo = values[7].ToString();//备注
                    YB_1305.servitem_type = values[8].ToString();//服务项目类别
                    YB_1305.servitem_name = values[9].ToString().Replace("'", "''");//医疗服务项目名称
                    YB_1305.item_name = values[10].ToString();//项目说明
                    YB_1305.begin_time = Time(values[11].ToString());//开始日期
                    YB_1305.end_time = Time(values[12].ToString());//结束日期
                    YB_1305.record_num = values[13].ToString();//唯一记录号
                    YB_1305.ver_num = values[14].ToString();//版本号
                    YB_1305.ver_name = values[15].ToString();//版本名称

                    if (lmr.InsertYB1305(YB_1305) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }

        public int InsertYB1306(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1306();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("医用耗材目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1306();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1306 YB_1306 = new FS.HISFC.Models.SIInterface.YB_1306();
                    YB_1306.wm_dise_id = values[0].ToString();//医疗目录编码
                    YB_1306.cpr = values[1].ToString();//耗材名称
                    YB_1306.cpr_code_scp = values[2].ToString();//医疗器械唯一标识码
                    YB_1306.cpr_name = values[3].ToString();//医保通用名代码
                    YB_1306.sec_code_scp = values[4].ToString();//医保通用名
                    YB_1306.prod_mol = values[5].ToString();//产品型号
                    YB_1306.spec_code = values[6].ToString();//规格代码
                    YB_1306.spec = values[7].ToString();//规格
                    YB_1306.mcs_type = values[8].ToString();//耗材分类
                    YB_1306.spec_mol = values[9].ToString();//规格型号
                    YB_1306.dise_code = values[10].ToString();//材质代码
                    YB_1306.mcs_matl = values[11].ToString();//耗材材质
                    YB_1306.pacspec = values[12].ToString();//包装规格
                    YB_1306.pac_cnt = values[13].ToString();//包装数量
                    YB_1306.prod_pacmatl = values[14].ToString();//产品包装材质
                    YB_1306.pacunt = values[15].ToString();//包装单位
                    YB_1306.prod_convrat = values[16].ToString();//产品转换比
                    YB_1306.min_useunt = values[17].ToString();//最小使用单位
                    YB_1306.prodplac_type = values[18].ToString();//生产地类别
                    YB_1306.prodplac_name = values[19].ToString();//生产地类别名称
                    YB_1306.cpbz = values[20].ToString();//产品标准
                    YB_1306.prodexpy = values[21].ToString();//产品有效期
                    YB_1306.xnjgyzc = values[22].ToString();//性能结构与组成
                    YB_1306.syfw = values[23].ToString();//适用范围
                    YB_1306.cpsyff = values[24].ToString();//产品使用方法
                    YB_1306.cptpbh = values[25].ToString();//产品图片编号
                    YB_1306.cpzlbz = values[26].ToString();//产品质量标准
                    YB_1306.manl = values[27].ToString();//说明书
                    YB_1306.qtzmcl = values[28].ToString();//其他证明材料
                    YB_1306.zjzybz = values[29].ToString();//专机专用标志
                    YB_1306.zj_name = values[30].ToString();//专机名称
                    YB_1306.zt_name = values[31].ToString();//组套名称
                    YB_1306.zt_flag = values[32].ToString();//机套标志
                    YB_1306.lmt_used_flag = values[33].ToString();//限制使用标志
                    YB_1306.lmt_usescp = values[34].ToString();//医保限用范围
                    YB_1306.min_salunt = values[35].ToString();//最小销售单位
                    YB_1306.highval_mcs_flag = values[36].ToString();//高值耗材标志
                    YB_1306.yyclfl_code = values[37].ToString();//医用材料分类代码
                    YB_1306.impt_matl_hmorgn_flag = values[38].ToString();//植入材料和人体器官标志
                    YB_1306.mj_flag = values[39].ToString();//灭菌标志
                    YB_1306.mj_name = values[40].ToString();//灭菌标志名称
                    YB_1306.impt_itvt_clss_flag = values[41].ToString();//植入或介入类标志
                    YB_1306.impt_itvt_clss_name = values[42].ToString();//植入或介入类名称
                    YB_1306.dspo_used_flag = values[43].ToString();//一次性使用标志
                    YB_1306.dspo_used_name = values[44].ToString();//一次性使用标志名称
                    YB_1306.rzcbar_name = values[45].ToString();//注册备案人名称
                    YB_1306.begndate = values[46].ToString();//开始日期
                    YB_1306.enddate = values[47].ToString();//结束日期
                    YB_1306.ylqxgllb_flag = values[48].ToString();//医疗器械管理类别
                    YB_1306.ylqxgllb_name = values[49].ToString();//医疗器械管理类别名称
                    YB_1306.reg_fil_no = values[50].ToString();//注册备案号
                    YB_1306.reg_fil_name = values[51].ToString();//注册备案产品名称
                    YB_1306.jgjzc = values[52].ToString();//结构及组成
                    YB_1306.qtnr = values[53].ToString();//其他内容
                    YB_1306.aprv_date = values[54].ToString();//批准日期
                    YB_1306.zcbar_addr = values[55].ToString();//注册备案人住所
                    YB_1306.zcz_begndate = values[56].ToString();//注册证有效期开始时间
                    YB_1306.zcz_enddate = values[57].ToString();//注册证有效期结束时间
                    YB_1306.scqy_code = values[58].ToString();//生产企业编号
                    YB_1306.scqy_name = values[59].ToString();//生产企业名称
                    YB_1306.sc_addr = values[60].ToString();//生产地址
                    YB_1306.dlrqy = values[61].ToString();//代理人企业
                    YB_1306.dlrqy_addr = values[62].ToString();//代理人企业地址
                    YB_1306.scghdq = values[63].ToString();//生产国或地区
                    YB_1306.shfwjg = values[64].ToString();//售后服务机构
                    YB_1306.zchbazdzda = values[65].ToString();//注册或备案证电子档案
                    YB_1306.cpyx = values[66].ToString();//产品影像
                    YB_1306.valid_state = values[67].ToString();//有效标志
                    YB_1306.wyjlh = values[68].ToString();//唯一记录号
                    YB_1306.ver = values[69].ToString();//版本号
                    YB_1306.ver_name = values[70].ToString();//版本名称


                    if (lmr.InsertYB1306(YB_1306) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }


                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1307(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1307();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("疾病与诊断目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1307();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1307 YB_1307 = new FS.HISFC.Models.SIInterface.YB_1307();
                    YB_1307.wm_dise_id = values[0].ToString();//西医疾病诊断id
                    YB_1307.cpr = values[1].ToString();//章
                    YB_1307.cpr_code_scp = values[2].ToString();//章代码范围
                    YB_1307.cpr_name = values[3].ToString();//章名称
                    YB_1307.sec_code_scp = values[4].ToString();//节代码范围
                    YB_1307.sec_name = values[5].ToString();//节名称
                    YB_1307.cgy_code = values[6].ToString();//类目代码
                    YB_1307.cgy_name = values[7].ToString();//类目名称
                    YB_1307.sor_code = values[8].ToString();//亚目代码
                    YB_1307.sor_name = values[9].ToString();//亚目名称
                    YB_1307.dise_code = values[10].ToString();//诊断代码
                    YB_1307.dise_name = values[11].ToString();//诊断名称
                    YB_1307.used_std = values[12].ToString();//使用标记
                    YB_1307.gb_dise_code = values[13].ToString();//国标版诊断代码
                    YB_1307.gb_dise_name = values[14].ToString();//国标版诊断名称
                    YB_1307.lc_dise_code = values[15].ToString();//临床版诊断代码
                    YB_1307.lc_dise_name = values[16].ToString();//临床版诊断名称
                    YB_1307.mark = values[17].ToString();//备注
                    YB_1307.valid_state = values[18].ToString();//有效标志
                    YB_1307.wyjlh = values[19].ToString();//唯一记录号
                    YB_1307.oper_date = values[20].ToString();//数据创建时间
                    YB_1307.update_date = values[21].ToString();//数据更新时间
                    YB_1307.ver = values[22].ToString();//版本号
                    YB_1307.ver_name = values[23].ToString();//版本名称


                    if (lmr.InsertYB1307(YB_1307) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }


                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1308(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1308();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("手术操作目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1308();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1308 YB_1308 = new FS.HISFC.Models.SIInterface.YB_1308();
                    YB_1308.ssbzml_id = values[0].ToString();//手术标准目录id
                    YB_1308.cpr = values[1].ToString();//章
                    YB_1308.cpr_code_scp = values[2].ToString();//章代码范围
                    YB_1308.cpr_name = values[3].ToString();//章名称
                    YB_1308.cgy_code = values[4].ToString();//类目代码
                    YB_1308.cgy_name = values[5].ToString();//类目名称
                    YB_1308.sor_code = values[6].ToString();//亚目代码
                    YB_1308.sor_name = values[7].ToString();//亚目名称
                    YB_1308.xm_code = values[8].ToString();//细目代码
                    YB_1308.xm_name = values[9].ToString();//细目名称
                    YB_1308.oprn_oprt_code = values[10].ToString();//手术操作代码
                    YB_1308.oprn_oprt_name = values[11].ToString().Replace("'", "''");//手术操作名称
                    YB_1308.used_std = values[12].ToString();//使用标记
                    YB_1308.tb_oprn_oprt_code = values[13].ToString();//团标版手术操作代码
                    YB_1308.tb_oprn_oprt_name = values[14].ToString();//团标版手术操作名称
                    YB_1308.lc_oprn_oprt_code = values[15].ToString();//临床版手术操作代码
                    YB_1308.lc_oprn_oprt_name = values[16].ToString();//临床版手术操作名称
                    YB_1308.mark = values[17].ToString();//备注
                    YB_1308.valid_state = values[18].ToString();//有效标志
                    YB_1308.wyjlh = values[19].ToString();//唯一记录号
                    YB_1308.oper_date = values[20].ToString();//数据创建时间
                    YB_1308.update_date = values[21].ToString();//数据更新时间
                    YB_1308.ver = values[22].ToString();//版本号
                    YB_1308.ver_name = values[23].ToString();//版本名称


                    if (lmr.InsertYB1308(YB_1308) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }

        public int InsertYB1309(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1309();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("门诊慢特病种目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1309();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1309 YB_1309 = new FS.HISFC.Models.SIInterface.YB_1309();
                    YB_1309.mmmtbzml_code = values[0].ToString();//门慢门特病种目录代码
                    YB_1309.mmmtbzdl_name = values[1].ToString();//门慢门特病种大类名称
                    YB_1309.mmmtbzxfl_code = values[2].ToString();//门慢门特病种细分类名称
                    YB_1309.ybqf = values[3].ToString();//医保区划
                    YB_1309.mark = values[4].ToString();//备注
                    YB_1309.valid_state = values[5].ToString();//有效标志
                    YB_1309.wyjlh = values[6].ToString();//唯一记录号
                    YB_1309.oper_date = values[7].ToString();//数据创建时间
                    YB_1309.update_date = values[8].ToString();//数据更新时间
                    YB_1309.ver = values[9].ToString();//版本号
                    YB_1309.bznh = values[10].ToString();//病种内涵
                    YB_1309.ver_name = values[11].ToString();//版本名称
                    YB_1309.zlznym = values[12].ToString();//诊疗指南页码
                    YB_1309.zlzndzda = values[13].ToString();//诊疗指南电子档案
                    YB_1309.mmmtbz_name = values[14].ToString();//门慢门特病种名称
                    YB_1309.mmmtbzdl_code = values[15].ToString();//门慢门特病种大类代码

                    if (lmr.InsertYB1309(YB_1309) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1310(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1310();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("按病种付费病种目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1310();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1310 YB_1310 = new FS.HISFC.Models.SIInterface.YB_1310();
                    YB_1310.bzjsml_id = values[0].ToString();//病种结算目录id
                    YB_1310.bzjsml_code = values[1].ToString();//按病种结算病种目录代码
                    YB_1310.bzjsml_name = values[2].ToString();//按病种结算病种名称
                    YB_1310.ydsscz_code = values[3].ToString();//限定手术操作代码
                    YB_1310.ydsscz_name = values[4].ToString();//限定手术操作名称
                    YB_1310.valid_state = values[5].ToString();//有效标志
                    YB_1310.wyjlh = values[6].ToString();//唯一记录号
                    YB_1310.oper_date = values[7].ToString();//数据创建时间
                    YB_1310.update_date = values[8].ToString();//数据更新时间
                    YB_1310.ver = values[9].ToString();//版本号
                    YB_1310.bznh = values[10].ToString();//病种内涵
                    YB_1310.mark = values[11].ToString();//备注
                    YB_1310.ver_name = values[12].ToString();//版本名称
                    YB_1310.zlznym = values[13].ToString();//诊疗指南页码
                    YB_1310.zlzndzda = values[14].ToString();//诊疗指南电子档案


                    if (lmr.InsertYB1310(YB_1310) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1311(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1311();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("日间手术治疗病种目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1311();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1311 YB_1311 = new FS.HISFC.Models.SIInterface.YB_1311();
                    YB_1311.rjsszlml_id = values[0].ToString();//日间手术治疗目录id
                    YB_1311.rjssbzml_code = values[1].ToString();//日间手术病种目录代码
                    YB_1311.rjssbzml_name = values[2].ToString();//日间手术病种名称
                    YB_1311.valid_state = values[3].ToString();//有效标志
                    YB_1311.wyjlh = values[4].ToString();//唯一记录号
                    YB_1311.oper_date = Time(values[5].ToString());//数据创建时间
                    YB_1311.update_date = Time(values[6].ToString());//数据更新时间
                    YB_1311.ver = values[7].ToString();//版本号
                    YB_1311.ver_name = values[8].ToString();//版本名称
                    YB_1311.bznh = values[9].ToString();//病种内涵
                    YB_1311.mark = values[10].ToString();//备注
                    YB_1311.zlznym = values[11].ToString();//诊疗指南页码
                    YB_1311.zlzndzda = values[12].ToString();//诊疗指南电子档案
                    YB_1311.sscz_name = values[13].ToString();//手术操作名称
                    YB_1311.sscz_code = values[14].ToString();//手术操作代码

                    if (lmr.InsertYB1311(YB_1311) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1313(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1313();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("肿瘤形态学目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1313();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1313 YB_1313 = new FS.HISFC.Models.SIInterface.YB_1313();
                    YB_1313.zlxtx_id = values[0].ToString();//肿瘤形态学id
                    YB_1313.zllx_code = values[1].ToString();//肿瘤/细胞类型代码
                    YB_1313.zllx_name = values[2].ToString();//肿瘤/细胞类型
                    YB_1313.xtxfl_code = values[3].ToString();//形态学分类代码
                    YB_1313.xtxfl_name = values[4].ToString();//形态学分类
                    YB_1313.valid_state = values[5].ToString();//有效标志
                    YB_1313.wyjlh = values[6].ToString();//唯一记录号
                    YB_1313.oper_date = Time(values[7].ToString());//数据创建时间
                    YB_1313.update_date = Time(values[8].ToString());//数据更新时间
                    YB_1313.ver = values[9].ToString();//版本号
                    YB_1313.ver_name = values[10].ToString();//版本名称

                    if (lmr.InsertYB1313(YB_1313) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1314(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1314();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("中医疾病目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1314();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1314 YB_1314 = new FS.HISFC.Models.SIInterface.YB_1314();
                    YB_1314.zyjbzd_id = values[0].ToString();//中医疾病诊断id
                    YB_1314.kblm_code = values[1].ToString();//科别类目代码
                    YB_1314.kblm_name = values[2].ToString();//科别类目名称
                    YB_1314.zkxtfl_code = values[3].ToString();//专科系统分类目代码
                    YB_1314.zkxtfl_name = values[4].ToString();//专科系统分类目名称
                    YB_1314.jbfl_code = values[5].ToString();//疾病分类代码
                    YB_1314.jbfl_name = values[6].ToString();//疾病分类名称
                    YB_1314.mark = values[7].ToString();//备注
                    YB_1314.valid_state = values[8].ToString();//有效标志
                    YB_1314.wyjlh = values[9].ToString();//唯一记录号
                    YB_1314.oper_date = Time(values[10].ToString());//数据创建时间
                    YB_1314.update_date = Time(values[11].ToString());//数据更新时间
                    YB_1314.ver = values[12].ToString();//版本号
                    YB_1314.ver_name = values[13].ToString();//版本名称

                    if (lmr.InsertYB1314(YB_1314) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }
                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }


        public int InsertYB1315(string sfilename)
        {
            API.GZSI.LocalManager lmr = new API.GZSI.LocalManager();
            Class.Log log = new Class.Log();
            FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //string[] temp = System.IO.File.ReadAllLines("E:\\新建文本文档.txt");
            string[] temp = System.IO.File.ReadAllLines(sfilename);

            // 打开文件准备读取数据   
            StreamReader rd = File.OpenText(sfilename);
            string line;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待,正在下载项目...");
            //设置事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(constMgr.Connection);
            //t.BeginTransaction();
            //constMgr.SetTrans(t.Trans);
            int insertCount = 0;
            int count = lmr.QueryYB1315();//查表数据是否有数据
            if (count > 0)
            {
                if (MessageBox.Show("中医证候目录下载已存在数据!是否继续下载更新？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lmr.DeleteYB1315();

                }
                else
                {
                    return 1;
                }
            }
            // 循环读出文件的每一行
            while ((line = rd.ReadLine()) != null)
            {
                // 拆分出一行的所有用空格分割的数据项
                string[] values = line.Split('	');
                if (values.Length > 5)
                {
                    FS.HISFC.Models.SIInterface.YB_1315 YB_1315 = new FS.HISFC.Models.SIInterface.YB_1315();
                    YB_1315.zyzh_id = values[0].ToString();//中医证候id
                    YB_1315.zhlm_code = values[1].ToString();//证候类目代码
                    YB_1315.zhlm_name = values[2].ToString();//证候类目名称
                    YB_1315.zhsx_code = values[3].ToString();//证候属性代码
                    YB_1315.zhsx_name = values[4].ToString();//证候属性
                    YB_1315.zhfl_code = values[5].ToString();//证候分类代码
                    YB_1315.zhfl_name = values[6].ToString();//证候分类名称
                    YB_1315.mark = values[7].ToString();//备注
                    YB_1315.valid_state = values[8].ToString();//有效标志
                    YB_1315.wyjlh = values[9].ToString();//唯一记录号
                    YB_1315.oper_date = Time(values[10].ToString());//数据创建时间
                    YB_1315.update_date = Time(values[11].ToString());//数据更新时间
                    YB_1315.ver = values[12].ToString();//版本号
                    YB_1315.ver_name = values[13].ToString();//版本名称
                    if (lmr.InsertYB1315(YB_1315) <= 0)
                    {
                        MessageBox.Show("保存码表到本地失败！" + constMgr.Err);
                        return -1;
                    }
                    else
                    {
                        insertCount += 1;
                    }


                }
                else
                {
                    log.WriteLog(rd.ReadLine());
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("此次西药中成药目录下载数量为:" + insertCount);
            return 1;
        }

        public string Time(string value)
        {
            string time = string.Empty;
            if (!string.IsNullOrEmpty(value) && value != "null" && value != "NULL")
            {
                DateTime dt = DateTime.ParseExact(value, "ddd MMM dd HH:mm:ss CST yyyy", new CultureInfo("en-US"));
                time = dt.ToString();
            }
            else
            {
                time = "0001-01-01";
            }
            return time;
        }
        #endregion

        private static string UnZipFile(string zipFilePath)
        {
            if (!File.Exists(zipFilePath))
            {
                Console.WriteLine("Cannot find file '{0}'", zipFilePath);
                return "";
            }

            string sfilename = "";
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    sfilename = theEntry.Name;
                    Console.WriteLine(theEntry.Name);

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(theEntry.Name))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return sfilename;
        }
    }
}
