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
using Neusoft.HISFC.Models.Fee;
using Neusoft.FrameWork.WinForms.Forms;
using Neusoft.HISFC.BizProcess.Integrate.FeeInterface;
using Neusoft.HISFC.Models.Base;
using Neusoft.FrameWork.Function;
using FoShanDiseasePay.DataBase;
using FoShanDiseasePay.Jobs;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 佛山市【药品】阳光采购平台接口
    /// </summary>
    public partial class ucUploadDrugSun : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucUploadDrugSun()
        {
            InitializeComponent();
        }

        #region 变量和属性

        private Manager manger = new Manager();

        /// <summary>
        /// 接口基类
        /// </summary>
        private FoShanDiseasePay.Jobs.BaseJob baseJob = new BaseJob();

        /// <summary>
        /// 医疗机构信息上传
        /// </summary>
        private HospitalInfoJob hosJob = new HospitalInfoJob();

        /// <summary>
        /// 药品信息上传
        /// </summary>
        private SunDrugJob drugJob = new SunDrugJob();


        /// <summary>
        /// 工具栏
        /// </summary>
        private ToolBarService toolBarService = new ToolBarService();

        /// <summary>
        /// 常数管理业务层 com_dictionary -- 公共_常数表
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Constant conManager = new Neusoft.HISFC.BizLogic.Manager.Constant();
        #region 业务变量


        /// <summary>
        /// 药品业务管理类
        /// </summary>
        private BizLogic.DrugManager drugMgr = new FoShanDiseasePay.BizLogic.DrugManager();

        /// <summary>
        /// 住院业务类
        /// </summary>
        private BizLogic.InManager inMgr = new FoShanDiseasePay.BizLogic.InManager();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager ConsMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 非药品业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.Item itemManager = new Neusoft.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Pharmacy.Item itemPharmacyManager = new Neusoft.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 人员管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Person empMgr = new Neusoft.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 科室业务管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Department deptMgr = new Neusoft.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 患者入出转管理
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 门诊就诊记录管理类
        /// </summary>
        private Neusoft.HISFC.BizLogic.Registration.Register outRegMgr = new Neusoft.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 诊断分类
        /// </summary>
        private Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        #endregion

        /// <summary>
        /// 药品缓存
        /// </summary>
        private Dictionary<string, Neusoft.HISFC.Models.Pharmacy.Item> dictDrug = new Dictionary<string, Neusoft.HISFC.Models.Pharmacy.Item>();

        /// <summary>
        /// 非药品缓存
        /// </summary>
        private Dictionary<string, Neusoft.HISFC.Models.Base.Item> dictUndrug = new Dictionary<string, Item>();

        /// <summary>
        /// 人员缓存
        /// </summary>
        private Dictionary<string, Employee> dictEmp = new Dictionary<string, Employee>();

        /// <summary>
        /// 科室缓存
        /// </summary>
        private Dictionary<string, Department> dictDept = new Dictionary<string, Department>();

        /// <summary>
        /// 中心费用大类对照
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper fldmHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 中心科室对照
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper bqdmHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 医保项目编码对照：0国标码；1自定义码；默认为0国标码
        /// </summary>
        private string itemCodeCompareType = "0";

        /// <summary>
        /// 特需项目：四舍五入费
        /// </summary>
        private string specialItemCode = string.Empty;

        /// <summary>
        /// 使用频次
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper sypcHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 给药途径
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper gytjHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 社保对照码
        /// </summary>
        private Dictionary<string, Const> dictSiItem = new Dictionary<string, Const>();

        //过虑串
        private string filter = "1=1";
        #endregion

        #region 方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            //当前时间
            DateTime dtNow = this.drugMgr .GetDateTimeFromSysDateTime();


            this.lblLoginInfo.Text = "请先登录!";
            this.lblLoginInfo.ForeColor = System.Drawing.Color.Red;

            this.fpNeedUpload_Sheet1.Rows.Count = 0;

            this.dtBeginTime.Value = dtNow.Date;
            this.dtEndTime.Value = dtNow.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 所有基本信息查询
        /// </summary>
        private void QueryDrugInfo()
        {
            //清屏
            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            this.cbChooseAll.Checked = true;
            string queryCode = "";
            queryCode = this.txtQueryCode.Text.Trim();
            string upType = this.cmbUpType.Tag.ToString();
            if(string.IsNullOrEmpty(upType))
            {
                upType = "ALL";
            }
            //药品信息
            ArrayList alDrug = this.drugMgr.QueryDrugBaseInfoByAll(queryCode, upType);
            if (alDrug != null && alDrug.Count > 0)
            {
                this.AddNeedUpload(alDrug);
            }

            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("查询基本信息失败！", "警告");
                return;
            }
        }
        /// <summary>
        /// 刷新药品信息
        /// </summary>
        private void QueryNeedUploadDrug()
        {
            //清屏
            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            this.cbChooseAll.Checked = true;

            //时间段
            string strBeginTime = this.dtBeginTime.Value.ToString();
            string strEndTime = this.dtEndTime.Value.ToString();

            string queryCode = "";
            queryCode = this.txtQueryCode.Text.Trim();
            string deptCode = "";
            if (string.IsNullOrEmpty(this.cmbDrugDept.Tag.ToString()) || string.IsNullOrEmpty(this.cmbDrugDept.Text))
            {
                deptCode = "";
            }
            else
            {
                deptCode = this.cmbDrugDept.Tag.ToString();
            }
            //待上传的药品信息
            ArrayList alDrug = this.drugMgr.QueryDrugBaseInfoByDate(queryCode, dtBeginTime.Value.Date, this.dtEndTime.Value.AddDays(1).Date, deptCode);// {385E49F2-947B-43e3-931D-BE89481BA68C}
            if (alDrug != null && alDrug.Count > 0)
            {
                this.AddNeedUpload(alDrug);
            }

            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要上传的数据!", "警告");
                return;
            }

        }

        /// <summary>
        /// 将待上传的药品基本信息显示在界面
        /// </summary>
        /// <param name="dtPatient"></param>
        private void AddNeedUpload(ArrayList alDrug)
        {
            if (alDrug != null && alDrug.Count > 0)
            {
                int rowIndex = this.fpNeedUpload_Sheet1.Rows.Count;
                foreach (Neusoft.HISFC.Models.Pharmacy.Item drug in alDrug)
                {
                    this.fpNeedUpload_Sheet1.Rows.Add(rowIndex, 1);

                    //默认选上
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColChoose].Value = true;

                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColDrugId].Value = drug.ID;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColDrugName].Value = drug.Name;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColUserCode].Value = drug.UserCode;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColGBCode].Value = drug.NameCollection.GbCode;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColSpecs].Value = drug.Specs;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColPrice].Value = drug.PriceCollection.RetailPrice;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColApproveInfo].Value = drug.Product.ApprovalInfo;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColCompany].Value = drug.Product.Producer.Name;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColGHS].Value = drug.Product.Company.Name;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColTotSum].Value = drug.Memo;
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColInvoice].Value = drug.Product.Memo;//发票号
                    if (drug.Product.User01 == "9999")
                    {
                        this.fpNeedUpload_Sheet1.Columns.Get(12).Width = 0F;
                    }
                    else
                    {
                        this.fpNeedUpload_Sheet1.Columns.Get(12).Width = 90F;
                    }
                    string type = "未上传";
                    if (drug.Product.User01 == "1")
                    {
                        type = "已上传";
                    }
                    else if (drug.Product.User01 == "0")
                    {
                        type = "未上传";
                    }
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColUploadType].Value = type;//上传标志

                    //实体
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColDrug.ColDrugId].Tag = drug;

                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// 一键上传患者所有药品的信息
        /// </summary>
        private void AllUploadDrugDetail()
        {
            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要上传信息!");
                return;
            }
            //提示
            string strTips = "一键上传将会上传的信息：\r\n 1、药品信息；\r\n 2、药品库存信息；\r\n3、药品使用详情；\r\n4、药品采购订单；\r\n5、药品退货信息；\r\n6、药品采购订单发票";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传!请稍后!");

            for (int k = 0; k < this.fpNeedUpload_Sheet1.Rows.Count; k++)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.fpNeedUpload_Sheet1.Rows.Count);

                bool isChoose = NConvert.ToBoolean(this.fpNeedUpload_Sheet1.Cells[k, (int)ColDrug.ColChoose].Value);
                if (isChoose)
                {

                }
            }

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }
        /// <summary>
        /// 是否有数据上传
        /// </summary>
        bool isHaveUpload = false;



        /// <summary>
        /// 已上传的药品编码--避免一次上传多条数据
        /// </summary>
        private Hashtable uploadDrugBaseInfoHS = new Hashtable();
        /// <summary>
        /// 逐个逐个上传药品基本信息
        /// </summary>
        /// <param name="upload"></param>
        private void UploadDrugIfo(string upload)
        {
            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要上传信息!");
                return;
            }
            //提示
            if (MessageBox.Show(upload, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传!请稍后!");

            Neusoft.HISFC.Models.Pharmacy.Item drug = null;
            uploadDrugBaseInfoHS = new Hashtable();
            for (int k = 0; k < this.fpNeedUpload_Sheet1.Rows.Count; k++)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.fpNeedUpload_Sheet1.Rows.Count);

                bool isChoose = NConvert.ToBoolean(this.fpNeedUpload_Sheet1.Cells[k, (int)ColDrug.ColChoose].Value);
                if (isChoose)
                {
                    
                    drug = this.fpNeedUpload_Sheet1.Cells[k, (int)ColDrug.ColDrugId].Tag as Neusoft.HISFC.Models.Pharmacy.Item;
                    if (drug != null && !string.IsNullOrEmpty(drug.ID))
                    {
                        if (!uploadDrugBaseInfoHS.Contains(drug.ID))//已经上传的药品则不重复上传
                        {
                            uploadDrugBaseInfoHS.Add(drug.ID, drug);
                        }
                        else
                        {
                            continue;
                        }
                        switch (upload)
                        {
                            case "上传药品基本信息":

                                this.UploadDrugBaseInfo(drug);
                                break;

                            case "上传药品库存信息":
                                this.UploadDrugStoreInfo(drug);
                                break;

                            case "上传药品使用详情":
                                this.UploadDrugUseInfo(drug);
                                break;

                            case "上传药品采购订单":
                                this.UploadDrugPurchaseOrder(drug);
                                break;

                            case "上传药品退货信息":
                                this.UploadDrugReturnInfo(drug);
                                break;

                            case "上传药品采购订单发票":
                                this.UploadDrugInvoiceInfo(drug);
                                break;

                            case "上传药品采购订单发票附件":
                                this.UploadDrugInvoiceInfoFile(drug);
                                break;

                            case "上传结算信息":
                                this.UploadBalanceInfo();
                                break;

                            case "上传医疗机构信息":
                                this.UploadHospitalInfo();
                                break;
                            case "作废基本信息":
                                this.CancelBaseInfo(drug);
                                break;
                        }
                    }
                }
            }

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            if (isHaveUpload)
            {
                MessageBox.Show("上传成功！");
                isHaveUpload = false;
            }

        }

        #region 佛山市【药品】阳光采购平台接口

        /// <summary>
        /// 4.3	上报医疗机构信息(HQ001)
        /// </summary>
        /// <returns></returns>
        private int UploadHospitalInfo()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传医疗机构信息!请稍后!");

            try
            {
                this.hosJob.Start();
            }
            catch (Exception ex) { }
            finally
            {

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            return 1;
        }
        /// <summary>
        /// 上报医疗机构药品信息(HD001)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadDrugBaseInfo(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选择需要上传的药品!");
                return -1;
            }

            drug = this.drugMgr.QueryDrugBaseInfo(drug.ID);
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("查询药品【" + drug.ID + "-" + drug.Name + "】基本信息出错!");
                return -1;
            }

            string error = string.Empty;
            int result = this.drugJob.UploadDrugBaseInfo(drug, ref error);
            if (error.Contains("重复提交") || error.Contains("已经匹配"))
            {
                result = 0;
            }
            if (result < 0)
            {
                MessageBox.Show("上报医疗机构药品信息出错【" + drug.Name + "】\r\n" + error);
                return -1;
            }
            else
            {
                isHaveUpload = true;
                //MessageBox.Show("上报医疗机构药品信息成功【" + drug.Name + "】");
                this.drugMgr.UpdateDrugUploadFlat(drug.ID,"1");
                return 1;
            }
        }
        /// <summary>
        /// 作废药品基本信息
        /// </summary>
        /// <param name="matBase"></param>
        private int CancelBaseInfo(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选作废的药品!");
                return -1;
            }

            drug = this.drugMgr.QueryDrugBaseInfo(drug.ID);
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("查询药品【" + drug.ID + "-" + drug.Name + "】基本信息出错!");
                return -1;
            }

            string error = string.Empty;
            int result = this.drugJob.DeleteBaseInfo(drug, ref error);
            if (result < 0)
            {
                MessageBox.Show("作废药品信息出错【" + drug.Name + "】\r\n" + error);
                return -1;
            }
            else
            {
                this.drugMgr.UpdateDrugUploadFlat(drug.ID, "0");
                return 1;
            }
        }

        /// <summary>
        /// 上报医疗机构药品库存信息(HD002)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadDrugStoreInfo(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选择需要上传的药品!");
                return -1;
            }

            ArrayList alDrug = new ArrayList();
            alDrug.Add(drug);

            string error = string.Empty;
            int result = this.drugJob.UploadDrugStoreInfo(alDrug, ref error);
            if (error.Contains("重复提交"))
            {
                result = 0;
            }
            if (result < 0)
            {
                MessageBox.Show("上报医疗机构药品库存信息出错【" + drug.Name + "】\r\n" + error);
                return -1;
            }
            else
            {
                isHaveUpload = true;
                //MessageBox.Show("上报医疗机构药品库存信息成功【" + drug.Name + "】");
                return 1;
            }
        }

        /// <summary>
        /// 上报医疗机构药品使用详情(HD003)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadDrugUseInfo(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选择需要上传的药品!");
                return -1;
            }

            ArrayList alOutPut = this.drugMgr.QueryDrugUseDetail(drug.ID, dtBeginTime.Value.Date, this.dtEndTime.Value.AddDays(1).Date);
            if (alOutPut == null || alOutPut.Count <= 0)
            {
                return 1;
            }

            string error = string.Empty;
            int result = 0;
            foreach (Neusoft.HISFC.Models.Pharmacy.Output output in alOutPut)
            {
                result = this.drugJob.UploadDrugUseInfo(output, ref error);
                if (error.Contains("重复提交"))
                {
                    result = 0;
                }
                if (result < 0)
                {
                    MessageBox.Show("上报医疗机构药品使用详情出错【" + drug.Name + "】\r\n" + error);
                    return -1;
                }
            }
            isHaveUpload = true;
            //MessageBox.Show("上报医疗机构药品使用详情成功【" + drug.Name + "】");
            return 1;
        }

        /// <summary>
        /// 上报医疗机构药品采购订单(HD005)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadDrugPurchaseOrder(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选择需要上传的药品!");
                return -1;
            }

            DateTime dtNow = this.drugMgr.GetDateTimeFromSysDateTime();
            ArrayList alInput = this.drugMgr.QueryDrugInputList(dtBeginTime.Value.Date, this.dtEndTime.Value.AddDays(1).Date, drug.ID);
            if (alInput == null || alInput.Count <= 0)
            {
                //MessageBox.Show("没有数据上传！");
                return 1;
            }

            string error = string.Empty;
            int result = this.drugJob.UploadDrugOrderInfo(alInput, ref error);

            if (error.Contains("重复提交"))
            {
                result = 0;
            }
            if (result < 0)
            {
                MessageBox.Show("上报医疗机构药品采购订单出错【" + drug.Name + "】\r\n" + error);
                return -1;
            }
            else
            {
                //MessageBox.Show("上报医疗机构药品采购订单成功【" + drug.Name + "】");
                isHaveUpload = true;
                return 1;
            }
        }

        /// <summary>
        /// 上报医疗机构药品退货信息(HD006)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadDrugReturnInfo(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选择需要上传的药品!");
                return -1;
            }

            DateTime dtNow = this.drugMgr.GetDateTimeFromSysDateTime();
            ArrayList alReturnList = this.drugMgr.QueryDrugReturnList(dtBeginTime.Value.Date, this.dtEndTime.Value.AddDays(1).Date, drug.ID);
            if (alReturnList == null || alReturnList.Count <= 0)
            {
                return 1;
            }

            string error = string.Empty;
            int result = 0;
            foreach (Neusoft.HISFC.Models.Pharmacy.Input reInfo in alReturnList)
            {
                result = this.drugJob.UploadDrugReturnInfo(reInfo, ref error);

                if (error.Contains("重复提交"))
                {
                    result = 0;
                }
                if (result < 0)
                {
                    MessageBox.Show("上报医疗机构药品退货信息出错【" + drug.Name + "】\r\n" + error);
                    return -1;
                }
            }

            //MessageBox.Show("上报医疗机构药品退货信息成功【" + drug.Name + "】");
            isHaveUpload = true;
            return 1;
        }

        /// <summary>
        /// 上报医疗机构药品采购订单发票(HD007)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadDrugInvoiceInfo(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选择需要上传的药品!");
                return -1;
            }

            DateTime dtNow = this.drugMgr.GetDateTimeFromSysDateTime();
            ArrayList alInvoiceList = this.drugMgr.QueryDrugInvoiceInfo(dtBeginTime.Value.Date, this.dtEndTime.Value.AddDays(1).Date, drug.ID);
            if (alInvoiceList == null || alInvoiceList.Count <= 0)
            {
                return 1;
            }

            string error = string.Empty;
            int result = this.drugJob.UploadDrugInvoiceInfo(alInvoiceList, ref error);

            if (error.Contains("重复提交"))
            {
                result = 0;
            }
            if (result < 0)
            {
                MessageBox.Show("上报医疗机构药品采购订单发票出错【" + drug.Name + "】\r\n" + error);
                return -1;
            }
            else
            {
                //MessageBox.Show("上报医疗机构药品采购订单发票成功【" + drug.Name + "】");
                isHaveUpload = true;
                return 1;
            }
        }

        /// <summary>
        /// 上报医疗机构采购订单发票附件(HV001)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadDrugInvoiceInfoFile(Neusoft.HISFC.Models.Pharmacy.Item drug)
        {
            if (drug == null || string.IsNullOrEmpty(drug.ID))
            {
                MessageBox.Show("请选择需要上传的药品!");
                return -1;
            }

            string fileName = string.Empty;
            string filePath = string.Empty;

            //定义一个文件打开控件
            OpenFileDialog ofd = new OpenFileDialog();
            //设置打开对话框的初始目录，默认目录为exe运行文件所在的路径
            ofd.InitialDirectory = Application.StartupPath;
            //设置打开对话框的标题
            ofd.Title = "请选择要打开的文件";
            //设置打开对话框可以多选
            ofd.Multiselect = true;
            //设置对话框打开的文件类型
            ofd.Filter = "文本文件|*.txt|音频文件|*.wav|图片文件|*.jpg|所有文件|*.*";
            //设置文件对话框当前选定的筛选器的索引
            ofd.FilterIndex = 4;
            //设置对话框是否记忆之前打开的目录
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择的文件完整路径 "用户选择的文件目录为:" + filePath
                filePath = ofd.FileName;

                //获取对话框中所选文件的文件名和扩展名，文件名不包括路径 "用户选择的文件名称为:" + fileName
                fileName = ofd.SafeFileName;
            }
            else
            {
                return 1;
            }
           
            string imageBase64String = Function.ToBase64String(filePath, System.Drawing.Imaging.ImageFormat.Png);

            Neusoft.HISFC.Models.Pharmacy.Input input = new Neusoft.HISFC.Models.Pharmacy.Input();
            if (fileName.Contains("."))
            {
                int index = fileName.IndexOf('.');
                if (index > 0)
                {
                    input.InvoiceNO = fileName.Substring(0, index);
                }
                else
                {
                    input.InvoiceNO = fileName;
                }
            }
            input.InvoiceType = fileName;
            input.Memo = imageBase64String;

            string error = string.Empty;
            int result = this.drugJob.UploadDrugInvoiceFileInfo(input, ref error);

            if (error.Contains("重复提交"))
            {
                result = 0;
            }
            if (result < 0)
            {
                MessageBox.Show("上报医疗机构采购订单发票附件出错【" + fileName + "】\r\n" + error);
                return -1;
            }
            else
            {
                //MessageBox.Show("上报医疗机构采购订单发票附件成功【" + fileName + "】");
                isHaveUpload = true;
                return 1;
            }
        }

        /// <summary>
        /// 上报医疗机构采购结算信息(HC001)
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        private int UploadBalanceInfo()
        {
            ArrayList alBalanceInfo = this.drugMgr.QueryBalanceInfo(dtBeginTime.Value.Date, this.dtEndTime.Value.AddDays(1).Date);
            if (alBalanceInfo == null || alBalanceInfo.Count <= 0)
            {
                return 1;
            }

            string error = string.Empty;
            int result = this.drugJob.UploadBalanceInfo(alBalanceInfo, ref error);

            if (error.Contains("重复提交"))
            {
                result = 0;
            }
            if (result < 0)
            {
                MessageBox.Show("上报医疗机构采购结算信息出错\r\n" + error);
                return -1;
            }
            else
            {
                //MessageBox.Show("上报医疗机构采购结算信息成功");
                isHaveUpload = true;
                return 1;
            }
        }

        #endregion

        /// <summary>
        /// 处理项目对照 - 处理国标码
        /// </summary>
        /// <param name="f">要处理的项目</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>成功返回1；失败返回-1</returns>
        private int DealFeeItemList(Neusoft.HISFC.Models.Fee.FeeItemBase f, ref string errMsg)
        {
            if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                #region 药品处理

                if (this.dictDrug.ContainsKey(f.Item.ID))
                {
                    Neusoft.HISFC.Models.Pharmacy.Item phaItem = this.dictDrug[f.Item.ID];
                    if (phaItem == null)
                    {
                        errMsg = "获得药品信息出错!";
                        return -1;
                    }
                    //自定义码和国标码全部赋值
                    f.Item.GBCode = phaItem.GBCode;
                    f.Item.UserCode = phaItem.UserCode;
                }
                else
                {

                    Neusoft.HISFC.Models.Pharmacy.Item phaItem = this.itemPharmacyManager.GetItem(f.Item.ID);
                    if (phaItem == null)
                    {
                        errMsg = "获得药品信息出错!";
                        return -1;
                    }

                    //自定义码和国标码全部赋值
                    f.Item.GBCode = phaItem.GBCode;
                    f.Item.UserCode = phaItem.UserCode;

                    this.dictDrug.Add(f.Item.ID, phaItem);
                }

                #endregion
            }
            else if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                #region 非药品处理

                if (this.specialItemCode == f.Item.ID)
                {
                    #region 四舍五入项目处理

                    f.Item.GBCode = "100000000";
                    f.Item.UserCode = "100000000";

                    #endregion
                }
                else
                {
                    #region 正常项目

                    if (this.dictUndrug.ContainsKey(f.Item.ID))
                    {
                        Neusoft.HISFC.Models.Base.Item baseItem = this.dictUndrug[f.Item.ID];
                        if (baseItem == null)
                        {
                            errMsg = "获得非药品信息出错!";
                            return -1;
                        }

                        //自定义码和国标码全部赋值
                        f.Item.GBCode = baseItem.GBCode;
                        f.Item.UserCode = baseItem.UserCode;
                    }
                    else
                    {

                        Neusoft.HISFC.Models.Base.Item baseItem = this.itemManager.GetUndrugByCode(f.Item.ID);
                        if (baseItem == null)
                        {
                            errMsg = "获得非药品信息出错!";
                            return -1;
                        }

                        //自定义码和国标码全部赋值
                        f.Item.GBCode = baseItem.GBCode;
                        f.Item.UserCode = baseItem.UserCode;

                        this.dictUndrug.Add(f.Item.ID, baseItem);
                    }

                    #endregion
                }

                #endregion
            }

            return 1;

        }

        /// <summary>
        /// 获取员工
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private Employee GetEmpoyee(string emplCode, ref string errMsg)
        {
            if (this.dictEmp.ContainsKey(emplCode))
            {
                Employee emp = this.dictEmp[emplCode];
                if (emp == null || string.IsNullOrEmpty(emp.ID))
                {
                    errMsg = "获取员工信息出错!";
                    return null;
                }
                return emp;
            }
            else
            {
                Employee emp = this.empMgr.GetPersonByID(emplCode);
                if (emp == null || string.IsNullOrEmpty(emp.ID))
                {
                    errMsg = "获取员工信息出错!";
                    return null;
                }

                this.dictEmp.Add(emplCode, emp);
                return emp;
            }
        }

        /// <summary>
        /// 获取员工姓名
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private string GetEmpoyeeName(string emplCode, ref string errMsg)
        {
            if (this.dictEmp.ContainsKey(emplCode))
            {
                Employee emp = this.dictEmp[emplCode];
                if (emp == null || string.IsNullOrEmpty(emp.ID))
                {
                    errMsg = "获取员工信息出错!";
                    return "";
                }
                return emp.Name;
            }
            else
            {
                Employee emp = this.empMgr.GetPersonByID(emplCode);
                if (emp == null || string.IsNullOrEmpty(emp.ID))
                {
                    errMsg = "获取员工信息出错!";
                    return "";
                }

                this.dictEmp.Add(emplCode, emp);
                return emp.Name;
            }
        }

        /// <summary>
        /// 获取科室名字
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private string GetDeptName(string deptCode, ref string errMsg)
        {
            if (this.dictDept.ContainsKey(deptCode))
            {
                Department dept = this.dictDept[deptCode];
                if (dept == null || string.IsNullOrEmpty(dept.ID))
                {
                    errMsg = "获取科室信息出错!";
                    return "";
                }
                return dept.Name;
            }
            else
            {
                Department dept = this.deptMgr.GetDeptmentById(deptCode);
                if (dept == null || string.IsNullOrEmpty(dept.ID))
                {
                    errMsg = "获取科室信息出错!";
                    return "";
                }
                this.dictDept.Add(deptCode, dept);
                if (dept.Name.Length > 10)
                {
                    dept.Name = dept.Name.Substring(0, 10);
                }
                return dept.Name;
            }
        }

        /// <summary>
        /// 验证上传的信息
        /// </summary>
        private void CheckUploadInfo()
        {

        }

        #endregion

        #region 事件

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //清屏
            this.Clear();

            //初始化
            this.itemCodeCompareType = this.controlParamIntegrate.GetControlParam<string>("FSMZ16", false, "0");
            this.specialItemCode = this.controlParamIntegrate.GetControlParam<string>("MZ9926", false, "");

            //费用类大类
            ArrayList alFLDM = this.ConsMgr.GetConstantList("SporadicFee");
            if (alFLDM != null)
            {
                this.fldmHelper.ArrayObject = alFLDM;
            }
            //科室
            ArrayList alBQDM = this.ConsMgr.GetConstantList("SporadicDept");
            if (alBQDM != null)
            {
                this.bqdmHelper.ArrayObject = alBQDM;
            }

            //使用频次
            ArrayList alSypc = this.ConsMgr.GetConstantList("SYPC");
            if (alSypc != null)
            {
                this.sypcHelper.ArrayObject = alSypc;
            }
            //给药途径
            ArrayList alGytj = this.ConsMgr.GetConstantList("GYTJ");
            if (alGytj != null)
            {
                this.gytjHelper.ArrayObject = alGytj;
            }

            //医保对照关系
            this.dictSiItem = this.inMgr.QuerySiCompare();
            if (this.dictSiItem == null)
            {
                this.dictSiItem = new Dictionary<string, Const>();
            }

            //读取url配置 并赋值全局静态变量
            manger = new Manager();
            if (manger.Init() == -1)
            {
                MessageBox.Show(manger.Err);
                LogManager.WriteLog("读取url配置并赋值全局静态变量错误：" + manger.Err);
                return;
            }
            LogManager.WriteLog("读取url配置并赋值全局静态变量成功！");

            ArrayList alDrugDeptList = new ArrayList();
            alDrugDeptList = this.conManager.GetList("DrugDept");
            if (alDrugDeptList != null)
            {
                this.cmbDrugDept.AddItems(alDrugDeptList);
            }
            else
            {
                MessageBox.Show("没有维护到库房科室，维护参数：DrugDept");
            }
            
            ArrayList al = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject("ALL", "全部", "");
            al.Add(obj);
            obj = new Neusoft.FrameWork.Models.NeuObject("0", "未上传", "");
            al.Add(obj);
            obj = new Neusoft.FrameWork.Models.NeuObject("1", "已上传", "");
            al.Add(obj);
            this.cmbUpType.AddItems(al);
            this.cmbUpType.Tag = "0";

            base.OnLoad(e);
        }

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            this.toolBarService.AddToolButton("药品入库记录查询", "查询药品基本信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            
            this.toolBarService.AddToolButton("一键上传", "一键上传药品所有信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);


            this.toolBarService.AddToolButton("上传药品基本信息", "上传药品基本信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);

            this.toolBarService.AddToolButton("上传药品库存信息", "上传药品库存信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.J接诊, true, false, null);

            this.toolBarService.AddToolButton("上传药品使用详情", "上传药品使用详情", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.J借出, true, false, null);

            this.toolBarService.AddToolButton("上传药品采购订单", "上传药品采购订单", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.C出库单, true, false, null);

            this.toolBarService.AddToolButton("上传药品退货信息", "上传药品退货信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退费, true, false, null);

            this.toolBarService.AddToolButton("上传药品采购订单发票", "上传药品采购订单发票", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.C采购单 , true, false, null);

            this.toolBarService.AddToolButton("上传医疗机构信息", "上传医疗机构信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.K科室, true, false, null);

            this.toolBarService.AddToolButton("上传药品采购订单发票附件", "上传药品采购订单发票附件", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.D导入, true, false, null);

            this.toolBarService.AddToolButton("上传结算信息", "上传结算信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.D导出, true, false, null);


            this.toolBarService.AddToolButton("清屏", "清屏", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            this.toolBarService.AddToolButton("验证", "验证上传信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Y医保, true, false, null);

            this.toolBarService.AddToolButton("药品基本信息查询", "药品基本信息查询", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);

            this.toolBarService.AddToolButton("作废基本信息", "作废基本信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z作废, true, false, null);


            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "药品入库记录查询":
                    this.QueryNeedUploadDrug();
                    break;
                case "药品基本信息查询":
                    this.QueryDrugInfo();
                    break;
                case "一键上传":
                    this.AllUploadDrugDetail();
                    break;

                case "上传药品基本信息":
                    this.UploadDrugIfo("上传药品基本信息");
                    break;

                case "上传药品库存信息":
                    this.UploadDrugIfo("上传药品库存信息");
                    break;

                case "上传药品使用详情":
                    this.UploadDrugIfo("上传药品使用详情");
                    break;

                case "上传药品采购订单":
                    this.UploadDrugIfo("上传药品采购订单");
                    break;

                case "上传药品退货信息":
                    this.UploadDrugIfo("上传药品退货信息");
                    break;

                case "上传药品采购订单发票":
                    this.UploadDrugIfo("上传药品采购订单发票");
                    break;

                case "上传药品采购订单发票附件":
                    this.UploadDrugIfo("上传药品采购订单发票附件");
                    break;

                case "上传结算信息":
                    this.UploadDrugIfo("上传结算信息");
                    break;

                case "上传医疗机构信息":
                    this.UploadHospitalInfo();
                    break;

                case "验证":
                    this.CheckUploadInfo();
                    break;

                case "清屏":
                    this.Clear();
                    break;
                case "作废基本信息":
                    this.UploadDrugIfo("作废基本信息");
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedTab == this.tpNeedUpload)
            {
                #region 待上传

                if (this.fpNeedUpload_Sheet1.Rows.Count > 0)
                {
                    for (int k = 0; k < this.fpNeedUpload_Sheet1.Rows.Count; k++)
                    {
                        if (this.cbChooseAll.Checked)
                        {
                            this.fpNeedUpload_Sheet1.Cells[k, 0].Value = true;
                        }
                        else
                        {
                            this.fpNeedUpload_Sheet1.Cells[k, 0].Value = false;
                        }
                    }
                }

                #endregion
            }
        }


        /// <summary>
        /// 测试查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region 枚举

        /// <summary>
        /// 待上传药品
        /// </summary>
        private enum ColDrug
        {
            /// <summary>
            /// 选择
            /// </summary>
            ColChoose = 0,

            /// <summary>
            /// 药品编码
            /// </summary>
            ColDrugId = 1,

            /// <summary>
            /// 药品名称
            /// </summary>
            ColDrugName = 2,

            /// <summary>
            /// 自定义码
            /// </summary>
            ColUserCode = 3,

            /// <summary>
            /// 国标码
            /// </summary>
            ColGBCode = 4,

            /// <summary>
            /// 规格
            /// </summary>
            ColSpecs = 5,

            /// <summary>
            /// 零售价
            /// </summary>
            ColPrice = 6,

            /// <summary>
            /// 批文号
            /// </summary>
            ColApproveInfo = 7,

            /// <summary>
            /// 厂家
            /// </summary>
            ColCompany = 8,

            /// <summary>
            /// 供货商
            /// </summary>
            ColGHS = 9,

            /// <summary>
            /// 库存
            /// </summary>
            ColTotSum = 10,

            /// <summary>
            /// 发票号
            /// </summary>
            ColInvoice = 11,
            /// <summary>
            /// 上传标志
            /// </summary>
            ColUploadType = 12

        }

        #endregion

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            if (this.fpNeedUpload_Sheet1.Rows.Count == 0) return;

            try
            {
                string queryCode = "";
                queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";

                string str = "((拼音码 LIKE '" + queryCode + "') OR " +
                    "(五笔码 LIKE '" + queryCode + "') OR " +
                    "(自定义码 LIKE '" + queryCode + "') OR " +
                    "(商品名称 LIKE '" + queryCode + "') OR" +
                    "(通用名拼音码 LIKE '" + queryCode + "') OR " +
                    "(通用名五笔码 LIKE '" + queryCode + "') OR " +
                    "(通用名 LIKE '" + queryCode + "') )";

                //设置过滤条件
                //this.fpNeedUpload_Sheet1.RowFilter.RowFilter = this.filter + " AND " + str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQueryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryNeedUploadDrug();
            }
        }

    }
}
