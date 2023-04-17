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
using FoShanDiseasePay.DataBase;
using FoShanDiseasePay.Jobs;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 佛山市按病种分值付费结算接口
    /// </summary>
    public partial class ucMainBZ : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucMainBZ()
        {
            InitializeComponent();

        }

        #region 变量和属性

        private Manager manger = new Manager();

        /// <summary>
        /// 接口基类
        /// </summary>
        private FoShanDiseasePay.Jobs.BaseJob baseJob = new BaseJob();
        //手术级别列表
        private FS.FrameWork.WinForms.Controls.PopUpListBox LevelType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper LevelHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 工具栏
        /// </summary>
        private ToolBarService toolBarService = new ToolBarService();

        #region 业务变量

        /// <summary>
        /// 接口管理类
        /// </summary>
        //private SIBizProcess siBizMgr = new SIBizProcess();

        /// <summary>
        /// 门诊业务类
        /// </summary>
        private BizLogic.OutManager outMgr = new FoShanDiseasePay.BizLogic.OutManager();

        /// <summary>
        /// 住院业务类
        /// </summary>
        private BizLogic.InManager inMgr = new FoShanDiseasePay.BizLogic.InManager();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager ConsMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 非药品业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品业务层
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemPharmacyManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 人员管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person empMgr = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 科室业务管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 患者入出转管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 门诊就诊记录管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register outRegMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 诊断分类
        /// </summary>
        private FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 手术分类
        /// </summary>
        private FS.HISFC.BizLogic.HealthRecord.Operation operationManager = new FS.HISFC.BizLogic.HealthRecord.Operation();

        #endregion

        /// <summary>
        /// 药品缓存
        /// </summary>
        private Dictionary<string, FS.HISFC.Models.Pharmacy.Item> dictDrug = new Dictionary<string, FS.HISFC.Models.Pharmacy.Item>();

        /// <summary>
        /// 非药品缓存
        /// </summary>
        private Dictionary<string, FS.HISFC.Models.Base.Item> dictUndrug = new Dictionary<string, Item>();

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
        private FS.FrameWork.Public.ObjectHelper fldmHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 中心科室对照
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper bqdmHelper = new FS.FrameWork.Public.ObjectHelper();

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
        private FS.FrameWork.Public.ObjectHelper sypcHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 给药途径
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper gytjHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 社保对照码
        /// </summary>
        private Dictionary<string, Const> dictSiItem = new Dictionary<string, Const>();


        #endregion


        #region 控制参数


        #endregion
        #region 方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            //当前时间
            DateTime dtNow = this.outMgr.GetDateTimeFromSysDateTime();

            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;

            this.dtBeginTime.Value = dtNow.Date;
            this.dtEndTime.Value = dtNow.Date.AddDays(1).AddSeconds(-1);
        }
        #region 佛山市按病种分值付费结算接口上传信息

        /// <summary>
        /// 医师库上传[HK001]
        /// </summary>
        /// <returns></returns>
        private int UploadEmployee()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传医师信息!请稍后!");

            try
            {
                DoctInfoJob doctJob = new DoctInfoJob();
                doctJob.StartNew();
            }
            catch (Exception ex) { }
            finally
            {

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            return 1;
        }


        #endregion

        #region 佛山市按病种分值付费结算接口上传信息(新升级改造)

        /// <summary>
        /// 清单信息
        /// </summary>
        private Newtonsoft.Json.Linq.JArray objList = null;

        private void QueryNeedUploadDetail(bool isTip)
        {
            this.objList = null;
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;
            this.fpNeedUpload_Sheet1.Rows.Count = 0;

            string CX001Json = @"'BILLDATE': '{0}','DDYLJGDM': '{1}'";
            if (isTip)
            {
                MessageBox.Show("将按月查询 【" + this.dtBeginTime.Value.ToString("yyyy年MM月") + "】 整月的【已填报】和【未填报】数据！","提示");
            }
            string date = this.dtBeginTime.Value.ToString("yyyyMM");

            StringBuilder json = new StringBuilder();
            json.Append(string.Format(CX001Json, date, Manager.setObj.HospitalID));

            string errInfo = string.Empty;
            int result = this.baseJob.UploadInfoNew("CX001", json,true, ref errInfo);


            if (result <= 0)
            {
                MessageBox.Show("查询数据失败！" + errInfo);
                return;
            }
            if (string.IsNullOrEmpty(errInfo))
            {
                MessageBox.Show("无上传数据！");
                return;
            }
            this.objList = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(errInfo);
            if (this.objList == null || this.objList.Count <= 0)
            {
                MessageBox.Show("数据为空！");
                return;
            }
            Dictionary<string, string> personType = new Dictionary<string, string>();

            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject("ALL", "全部", "");
            this.cmbPatientType.ClearItems();

            for (int j = 0; j < this.objList.Count; j++)
            {
                JObject jsonobj = (JObject)objList[j];
                if (!string.IsNullOrEmpty(jsonobj["CLAIM_TYPE"].ToString()))
                {
                    if (!personType.ContainsKey(jsonobj["CLAIM_TYPE"].ToString()))
                    {
                        personType.Add(jsonobj["CLAIM_TYPE"].ToString(), jsonobj["CLAIM_TYPE"].ToString());
                        obj = new FS.FrameWork.Models.NeuObject(jsonobj["CLAIM_TYPE"].ToString(), jsonobj["CLAIM_TYPE"].ToString(), "");
                        al.Add(obj);
                    }
                }
            }
            this.cmbPatientType.AddItems(al);
            SetFP();
             
        }
        /// <summary>
        /// 患者基本信息缓存
        /// </summary>
        private Dictionary<string, DataRow> patientInfoList = new Dictionary<string, DataRow>();

        private void SetFP()
        {

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询信息!请稍后!");
            //清屏
            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;
            if (this.objList == null || this.objList.Count <= 0)
            {
                MessageBox.Show("数据为空！");
                return;
            }
            string PatientType = "";
            if (this.cmbPatientType.Tag != null && !string.IsNullOrEmpty(this.cmbPatientType.Tag.ToString()))
            {
                PatientType = this.cmbPatientType.Tag.ToString();
            }
            if (string.IsNullOrEmpty(PatientType))
            {
                PatientType = "ALL";
            }
            for (int j = 0; j < this.objList.Count; j++)
            {
                JObject jsonobj = (JObject)objList[j];

                if (jsonobj["CLAIM_TYPE"].ToString() != PatientType && PatientType != "ALL")
                {
                    continue;
                }
                if (jsonobj["TBZT"].ToString() != "已填报")
                {
                    AddNeedUpload(jsonobj);
                }
                else
                {
                    AddUploaded(jsonobj);
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

        private void AddNeedUpload(JObject jsonobj)
        {
                DataTable dt = null;
                DataRow dRow = null;

                string lsh = "";//门诊、住院流水号
                string sfz = "";//身份证号
                string jslsh = "";//HIS结算流水号
                string hzlx = "";//患者类型
                string cardNo = "";//住院号 卡号
                string err = "";//报错信息

                if (patientInfoList.ContainsKey(jsonobj["HISID"].ToString()))
                {
                    dRow = patientInfoList[jsonobj["HISID"].ToString()] as DataRow;
                }
                else
                {
                    if (!string.IsNullOrEmpty(jsonobj["HISID"].ToString()))
                    {
                        dt = this.inMgr.GetBaseInfoList(jsonobj["HISID"].ToString());
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dRowT in dt.Rows)
                        {
                            dRow = dRowT;
                            break;
                        }
                    }

                    if (dRow != null)
                    {
                        patientInfoList.Add(jsonobj["HISID"].ToString(), dRow);
                    }
                }

                if (dRow != null)
                {
                    lsh = dRow["inpatient_no"].ToString();
                    sfz = dRow["idenno"].ToString();
                    jslsh = dRow["invoice_no"].ToString();
                    hzlx = dRow["person"].ToString();
                    cardNo = dRow["patient_no"].ToString();
                    err = dRow["err"].ToString();
                }
                if (!string.IsNullOrEmpty(this.lblPatientNo.Text.Trim()) && cardNo != this.lblPatientNo.Text.Trim().PadLeft(10, '0'))
                {
                    return;
                }

                int rowIndex = this.fpNeedUpload_Sheet1.Rows.Count;
                this.fpNeedUpload_Sheet1.Rows.Add(rowIndex, 1);
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 0].Value = true;

                this.fpNeedUpload_Sheet1.Cells[rowIndex, 1].Value = jsonobj["HISID"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 1].Tag = lsh;//jsonobj["HISID"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 2].Value = jsonobj["CLAIM_TYPE"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 2].Tag = hzlx;//jsonobj["CLAIM_TYPE"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 3].Value = cardNo;// jsonobj["ADMISSION_NUMBER"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 3].Tag = jslsh;
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 4].Value = sfz;//jsonobj["IDCard"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 5].Value = jsonobj["PATIENT_NAME"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 6].Value = DateTime.Parse(jsonobj["BILLDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 7].Value = jsonobj["TOTAL_AMOUNT"].ToString();
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 8].Value = DateTime.Parse(jsonobj["ADMISSION_DATE"].ToString()).ToString("yyyy-MM-dd");
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 9].Value = DateTime.Parse(jsonobj["DISCHARGE_DATE"].ToString()).ToString("yyyy-MM-dd");
                this.fpNeedUpload_Sheet1.Cells[rowIndex, 10].Value = err;


        }
        private void AddUploaded(JObject jsonobj)
        {
            DataTable dt = null;
            DataRow dRow = null;

            string lsh = "";//门诊、住院流水号
            string sfz = "";//身份证号
            string jslsh = "";//HIS结算流水号
            string hzlx = "";//患者类型
            string cardNo = "";//住院号 卡号
            string err = "";//报错信息

            if (patientInfoList.ContainsKey(jsonobj["HISID"].ToString()))
            {
                dRow = patientInfoList[jsonobj["HISID"].ToString()] as DataRow;
            }
            else
            {
                if (!string.IsNullOrEmpty(jsonobj["HISID"].ToString()))
                {
                    dt = this.inMgr.GetBaseInfoList(jsonobj["HISID"].ToString());
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dRowT in dt.Rows)
                    {
                        dRow = dRowT;
                        break;
                    }
                }

                if (dRow != null)
                {
                    patientInfoList.Add(jsonobj["HISID"].ToString(), dRow);
                }
            }

            if (dRow != null)
            {
                lsh = dRow["inpatient_no"].ToString();
                sfz = dRow["idenno"].ToString();
                jslsh = dRow["invoice_no"].ToString();
                hzlx = dRow["person"].ToString();
                cardNo = dRow["patient_no"].ToString();
                err = dRow["err"].ToString();
            }
            if (!string.IsNullOrEmpty(this.lblPatientNo.Text.Trim()) && cardNo != this.lblPatientNo.Text.Trim().PadLeft(10, '0'))
            {
                return;
            }
            int rowIndex = this.fpHaveUploaded_Sheet1.Rows.Count;
            this.fpHaveUploaded_Sheet1.Rows.Add(rowIndex, 1);
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 0].Value = true;

            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 1].Value = jsonobj["HISID"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 1].Tag = lsh;//jsonobj["HISID"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 2].Value = jsonobj["CLAIM_TYPE"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 2].Tag = hzlx;//jsonobj["CLAIM_TYPE"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 3].Value = cardNo;// jsonobj["ADMISSION_NUMBER"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 4].Value = sfz;//jsonobj["IDCard"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 5].Value = jsonobj["PATIENT_NAME"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 6].Value = DateTime.Parse(jsonobj["BILLDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 7].Value = jsonobj["TOTAL_AMOUNT"].ToString();
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 8].Value = DateTime.Parse(jsonobj["ADMISSION_DATE"].ToString()).ToString("yyyy-MM-dd");
            this.fpHaveUploaded_Sheet1.Cells[rowIndex, 9].Value = DateTime.Parse(jsonobj["DISCHARGE_DATE"].ToString()).ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 结算清单作废
        /// </summary>
        /// <returns></returns>
        private int CancelUploadInfoNew()
        {
            //判断
            if (this.fpHaveUploaded_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要作废的上传信息!");
                return -1;
            }

            if (MessageBox.Show("是否继续作废?", "疑问", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return -1;
            }
            for (int rowIndex = 0; rowIndex < this.fpHaveUploaded_Sheet1.Rows.Count; rowIndex++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(rowIndex, this.fpHaveUploaded_Sheet1.Rows.Count);

                bool isChoose = NConvert.ToBoolean(this.fpHaveUploaded_Sheet1.Cells[rowIndex, 0].Value);
                if (isChoose)
                {
                    string wylsh = this.fpHaveUploaded_Sheet1.Cells[rowIndex, 1].Value.ToString();
                    string err = "";
                    if (this.CancelCase(wylsh, ref err) <= 0)
                    {
                        MessageBox.Show("作废失败！" + err);
                        continue;
                    }
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("作废完成！");

            this.QueryNeedUploadDetail(false);

            return 1;
        }

        /// <summary>
        /// 结算清单作废
        /// </summary>
        /// <param name="jsdh">结算单号</param>
        /// <returns></returns>
        private int CancelCase(string jsdh, ref string errInfo)
        {
            string jsonXML = @" 'QDLSH':'{0}',
                              'DDYLJGDM':'{1}'";
            StringBuilder json = new StringBuilder();
            json.Append(string.Format(jsonXML, jsdh, Manager.setObj.HospitalID));
            

            errInfo = string.Empty;
            int result = this.baseJob.UploadInfoNew("JSQD5003", json,false, ref errInfo);

            if (result <= 0)
            {
                if (errInfo.Contains("不存在"))
                {
                    return 1;
                }
                return -1;
            }
            return result;
        }
        
        /// <summary>
        /// 结算清单上传
        /// </summary>
        private void AllUploadPatientCaseNew()
        {
            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要上传信息!");
                return;
            }
            //提示
            string strTips = "是否确定结算清单上传？";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传!请稍后!");

            for (int k = 0; k < this.fpNeedUpload_Sheet1.Rows.Count; k++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.fpNeedUpload_Sheet1.Rows.Count);

                bool isChoose = NConvert.ToBoolean(this.fpNeedUpload_Sheet1.Cells[k, 0].Value);
                if (isChoose)
                {
                    string patientType = this.fpNeedUpload_Sheet1.Cells[k, 2].Tag.ToString();
                    string patientID = this.fpNeedUpload_Sheet1.Cells[k, 1].Tag.ToString();
                    string invoiceNO = this.fpNeedUpload_Sheet1.Cells[k, 3].Tag.ToString();
                    string transType = "1"; //正交易


                    int rev = 1;
                    string errInfo = string.Empty;

                    //收费单据号【即医保系统的结算顺序号，主单结算以后再上传】-作为上传的主线
                    string sfdjh = this.fpNeedUpload_Sheet1.Cells[k, 1].Value.ToString();
                    //唯一流水号
                    string wylsh = string.Empty;
                    //住院号
                    string patientNo = this.fpNeedUpload_Sheet1.Cells[k, 3].Value.ToString();
                    //身份证号
                    string idenNo = this.fpNeedUpload_Sheet1.Cells[k, 4].Value.ToString();

                    rev = this.UploadPatientCaseNew(patientID, patientType, invoiceNO, transType, sfdjh);
                    if (rev == -1)
                    {
                        continue;
                    }

                    DataRow dt = null;
                    if (this.patientInfoList.ContainsKey(sfdjh))
                    {
                        dt = patientInfoList[sfdjh] as DataRow;
                        if (dt != null)
                        {
                            dt["err"] = "";
                            //dt["TBZT"] = "已填报";
                            patientInfoList.Remove(sfdjh);
                            patientInfoList.Add(sfdjh, dt);
                        }
                    }
                    //if (patientType == "1")//新接口编码，避免影响原来接口
                    //{
                    //    patientType = "3";                                     
                    //}
                    //else if (patientType == "2")
                    //{
                    //    patientType = "4";
                    //}
                    this.DeleteErrLog(patientType, patientID);  

                    //不需要插入数据库，从平台上获取
                    //if (this.inMgr.UpdateLog(patientType, patientID, invoiceNO, "3") <= 0)
                    //{
                    //    rev = this.inMgr.SaveLog(patientType, invoiceNO, patientNo, patientID, idenNo, sfdjh, wylsh, this.inMgr.Operator.ID, "3");
                    //    if (rev == -1)
                    //    {
                    //        //MessageBox.Show(this.inMgr.Err, "错误");
                    //        continue;
                    //    }
                    //}
                }
            }
            //重新查询

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("操作完毕！");
            this.QueryNeedUploadDetail(false);

        }



        /// <summary>
        /// 2、结算清单上传(JSQD5001)
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientType"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="transType"></param>
        /// <param name="ybMedNo"></param>
        /// <returns></returns>
        private int UploadPatientCaseNew(string patientID, string patientType, string invoiceNO, string transType, string ybMedNo)
        {

            if (patientType == "1")
            {

            }
            else
            {
                #region 住院

                //查询住院患者的主表
                FS.HISFC.Models.RADT.PatientInfo inPatientInfo = this.radtIntegrate.GetPatientInfomation(patientID);
                if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
                {
                    this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("未找到【" + patientID + "】的住院信息!").ToString());
                    return -1;
                }

                //发票对应的社保返回信息
                if (string.IsNullOrEmpty(ybMedNo))
                {
                    string siMemo = this.outMgr.GetSiInfo(patientID, invoiceNO, "1");
                    string siReturnID = string.Empty;
                    if (!string.IsNullOrEmpty(siMemo))
                    {
                        string[] arr = siMemo.Split('|');
                        if (arr.Length > 0)
                        {
                            siReturnID = arr[0];
                        }
                    }

                    if (siReturnID == "-1")
                    {
                        this.InsertErrLog(ybMedNo, patientType, patientID, invoiceNO, ("结算单号【" + siReturnID + "】不正确,未找到对应的结算信息!").ToString());
                        return -1;
                    }

                    ybMedNo = siReturnID;
                }

                string errInfoR = string.Empty;
                if (this.CancelCase(ybMedNo, ref errInfoR) < 0)
                {
                    this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, (errInfoR).ToString());
                }

                // {E1865220-C459-45ca-966E-F111E0A6B560}
                string feeDate = "";
                string totCostStr = "";
                string pubCostStr = "";
                string ownCostStr = "";
                string jydjh = "";
                this.outMgr.GetFeeInfo(patientID, invoiceNO, "2", ref feeDate, ref totCostStr, ref pubCostStr, ref ownCostStr, ref jydjh);
                #region 获取诊断

                //查询诊断信息
                ArrayList alDiag = new ArrayList();
                string firstDiag = string.Empty;
                string firstDiagName = string.Empty;
                string jsonDiagInfo = string.Empty;
                //查询诊断信息
                DataTable caseDiagnose = this.inMgr.GetCaseDiagnoseByBAZD(inPatientInfo.PID.PatientNO, inPatientInfo.InTimes.ToString());//inPatientInfo.ID
                if (caseDiagnose != null)
                {
                    foreach (DataRow dRow in caseDiagnose.Rows)
                    {
                        if (dRow["ZDLX"].ToString() == "1" && string.IsNullOrEmpty(firstDiag))
                        {
                            firstDiag = dRow["JBDM"].ToString();
                            firstDiagName = dRow["JBMC"].ToString();
                            continue;
                        }
                        FS.HISFC.Models.HealthRecord.Diagnose diag = new FS.HISFC.Models.HealthRecord.Diagnose();
                        diag.DiagInfo.ICD10.ID =  dRow["JBDM"].ToString();
                        diag.DiagInfo.ICD10.Name =  dRow["JBMC"].ToString();
                        diag.IsValid = true;
                        diag.PerssonType = ServiceTypes.I;
                        alDiag.Add(diag);

                    }
                }
                #endregion

                int diagCount = 0;
                int operationCount = 0;

                StringBuilder ALLJson = new StringBuilder();

                StringBuilder JsonJBXX = new StringBuilder();

                StringBuilder JsonZYZLXX = new StringBuilder();

                StringBuilder JsonYYSFXX = new StringBuilder();

                StringBuilder JsonJCD = new StringBuilder();

                StringBuilder JsonCYXJ = new StringBuilder();

                StringBuilder JsonJZMX = new StringBuilder();

                #region 基本信息(JBXX)

                //
                DataTable dtJBXX = this.inMgr.GetCaseMainInfo(inPatientInfo.PID.PatientNO, inPatientInfo.InTimes.ToString());

                //仅一条
                if (dtJBXX != null && dtJBXX.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dtJBXX.Rows)
                    {
                        JsonJBXX.Append("{" + string.Format(Function.JBXXJson,
                                                        ybMedNo, //0
                                                        Manager.setObj.HospitalName, //1
                                                        Manager.setObj.HospitalID, //2
                                                        dRow["YBJSDJ"].ToString(), //3
                                                        dRow["YBBH"].ToString(), //4
                                                        dRow["BAH"].ToString(), //5
                                                        feeDate + " 00:00:00",//dRow["SBSJ"].ToString(), //6
                                                        dRow["XM"].ToString(), //7
                                                        dRow["XB"].ToString(), //8
                                                        dRow["CSRQ"].ToString(), //9
                                                        dRow["NL_S"].ToString(), //10
                                                        dRow["NL_BZZS"].ToString(), //11
                                                        dRow["GJ"].ToString(), //12
                                                        dRow["MZ"].ToString(), //13
                                                        dRow["HZZJLB"].ToString(), //14
                                                        dRow["HZZJHM"].ToString(), //15
                                                        dRow["ZY"].ToString(), //16
                                                        dRow["XZZ"].ToString(), //17
                                                        dRow["GZDWMC"].ToString(), //18
                                                        dRow["GZDWDZ"].ToString(), //19
                                                        dRow["DWDH"].ToString(), //20
                                                        dRow["GZDWYB"].ToString(), //21
                                                        dRow["LXRXM"].ToString(), //22
                                                        dRow["LXRYHZGX"].ToString(), //23
                                                        dRow["LXRDZ"].ToString(), //24
                                                        dRow["LXRDH"].ToString(), //25
                                                        dRow["YBLX"].ToString(), //26
                                                        dRow["TSRYLX"].ToString(), //27
                                                        dRow["CBD"].ToString(), //28
                                                        dRow["XSERYLX"].ToString(), //29
                                                        dRow["XSECSTZ"].ToString(), //30
                                                        dRow["XSERYTZ"].ToString() //31
                                                        ) + "}");
                        break;
                    }
                }
                else
                {
                    this.InsertErrLog(ybMedNo, "2", patientID, invoiceNO, ("bafzff基本信息为空!").ToString());
                    return -1;
                }

                #endregion

                #region 住院诊疗信息(ZYZLXX)


                //其他西医诊断列表
                string QTZDJson = @"'XYQTZD': '{0}',
		                            'XYJBDM': '{1}',
		                            'XYRYBQ': '{2}'";

                string QTZDStr = "";


                #region 获取诊断
                if (alDiag != null && alDiag.Count > 0)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.IsValid && diag.PerssonType == ServiceTypes.I)  //患者类别 0 门诊患者 1 住院患者
                        {
                            if (diag.DiagInfo.ICD10.ID == "MS999")
                            {
                                continue;
                            }
                            diagCount++;
                            if (diagCount == 1 && string.IsNullOrEmpty(firstDiag))
                            {
                                firstDiag = diag.DiagInfo.ICD10.ID;
                                firstDiagName = diag.DiagInfo.ICD10.Name;
                            }
                            if (firstDiag == diag.DiagInfo.ICD10.ID)
                            {
                                continue;
                            }
                            QTZDStr += "{" + string.Format(QTZDJson, diag.DiagInfo.ICD10.Name, diag.DiagInfo.ICD10.ID, "4") + "},";

                        }
                    }

                }
                if (!string.IsNullOrEmpty(QTZDStr) && QTZDStr.Length > 0)
                {
                    QTZDStr = QTZDStr.Substring(0, QTZDStr.Length - 1);
                }
                else
                {
                    this.InsertErrLog(ybMedNo, "2", patientID, invoiceNO, ("bafzffzd诊断信息为空！").ToString());
                    return -1;
                }

                #endregion



                //其他中医诊断列表
                string QTZYZDJson = @"'ZZ': '{0}',
		                            'ZZJBDM': '{1}',
		                            'ZZRYBQ': '{2}'";

                string QTZYZDStr = "";



                #region 2、手术列表(ListOperation)【0条、一条或多条】

                ArrayList alOperationList = new ArrayList();

                alOperationList = this.inMgr.QueryOperationByInpatientNo(inPatientInfo.PID.PatientNO.Substring(2), inPatientInfo.InTimes.ToString());//目前写死，以后修改
                string jsonOperationList = string.Empty;

                if (alOperationList != null && alOperationList.Count > 0)
                {
                    alOperationList.Sort(new OperationSortByHappenNO());
                    string jsonOperationDetailList = string.Empty;

                    int iii = 0;
                    jsonOperationList = string.Empty;
                    //手术主记录与手术明细一对一
                    foreach (FS.HISFC.Models.HealthRecord.OperationDetail info1 in alOperationList)
                    {
                        if (string.IsNullOrEmpty(info1.FirDoctInfo.ID) && !string.IsNullOrEmpty(info1.FirDoctInfo.Name))
                        {
                            info1.FirDoctInfo.ID = this.inMgr.GetDoctCodeByName(info1.FirDoctInfo.Name);
                        }
                        if (string.IsNullOrEmpty(info1.SecDoctInfo.ID) && !string.IsNullOrEmpty(info1.SecDoctInfo.Name))
                        {
                            info1.SecDoctInfo.ID = this.inMgr.GetDoctCodeByName(info1.SecDoctInfo.Name);
                        }
                        if (string.IsNullOrEmpty(info1.ThrDoctInfo.ID) && !string.IsNullOrEmpty(info1.ThrDoctInfo.Name))
                        {
                            info1.ThrDoctInfo.ID = this.inMgr.GetDoctCodeByName(info1.ThrDoctInfo.Name);
                        }
                        if (string.IsNullOrEmpty(info1.NarcDoctInfo.ID) && !string.IsNullOrEmpty(info1.NarcDoctInfo.Name))
                        {
                            info1.NarcDoctInfo.ID = this.inMgr.GetDoctCodeByName(info1.NarcDoctInfo.Name);
                        }
                        string OperationRecordNo1 = inPatientInfo.ID + "000" + (iii + 1).ToString();
                        string OperationDoctorCode = info1.FirDoctInfo.ID;
                        string OperationDoctorName = info1.FirDoctInfo.Name;
                        string FirstOperdoctorcode = info1.SecDoctInfo.ID;
                        string FirstOperdoctorname = string.IsNullOrEmpty(info1.SecDoctInfo.ID) ? "" : info1.SecDoctInfo.Name;
                        string SecondOperdoctorcode = info1.ThrDoctInfo.ID;
                        string SecondOperdoctorname = string.IsNullOrEmpty(info1.ThrDoctInfo.ID) ? "" : info1.ThrDoctInfo.Name;
                        string AnesthesiologistCode = info1.NarcDoctInfo.ID;
                        string AnesthesiologistName = string.IsNullOrEmpty(info1.NarcDoctInfo.ID) ? "" : info1.NarcDoctInfo.Name;
                        string OperationDate = info1.OperationDate.ToString();
                        string OperationFinishDate = info1.OperationDate.ToString();
                        string AnaesthesiaType = "";

                        if (!string.IsNullOrEmpty(info1.MarcKind))//麻醉方式
                        {
                            AnaesthesiaType = this.inMgr.GetAnaesthesiaType(info1.MarcKind);
                            if (string.IsNullOrEmpty(AnaesthesiaType))
                            {
                                AnaesthesiaType = info1.MarcKind;
                            }
                        }
                        else
                        {
                            AnaesthesiaType = "9";
                        }
                        string IsComplication = string.Empty;
                        if (string.IsNullOrEmpty(info1.SYNDFlag))
                        {
                            IsComplication = "0";
                        }
                        else
                        {
                            IsComplication = info1.SYNDFlag;
                        }
                        string ComplicationCode = string.Empty;
                        string ComplicationName = string.Empty;
                        string OperationRecord = string.Empty;

                        string RecordDoctorCode = info1.FirDoctInfo.ID;//操作人
                        string RecordDoctorName = info1.FirDoctInfo.Name;//




                        #region 2、手术明细(ListOperationDetail)【0条、一条或多条】

                        string OperationRecordNo2 = inPatientInfo.ID + "000" + (iii + 1).ToString();
                        string OperationNo = "1";
                        string OperationCode = info1.OperationInfo.ID;
                        string OperationName = info1.OperationInfo.Name;
                        string OperationLevel = info1.FourDoctInfo.Name;
                        if (string.IsNullOrEmpty(OperationLevel))
                        {
                            OperationLevel = "1";
                        }
                        string NickKindCode = this.inMgr.GetDictionaryCode("NickKind", info1.NickKind);
                        if (string.IsNullOrEmpty(NickKindCode))
                        {
                            NickKindCode = "1";//0类
                        }
                        string OperationIncisionClass = NickKindCode;//手术切口分类

                        string CicaKindCode = this.inMgr.GetDictionaryCode("CicaKind", info1.CicaKind);
                        if (string.IsNullOrEmpty(CicaKindCode))
                        {
                            CicaKindCode = "9";//其他
                        }


                        string OperationHealClass = CicaKindCode;
                        string IsMajorIden = "0";
                        //if (alOperationList.Count == (iii + 1))
                        if (info1.StatFlag == "1")
                        {
                            IsMajorIden = "1";
                        }
                        string IsIatrogenic = string.Empty;


                        string rownOperationDetailJSON = string.Format(Function.ListOperationDetailJSON, OperationRecordNo2, OperationNo, OperationCode, OperationName,
                                                                            OperationLevel, OperationIncisionClass, OperationHealClass, IsMajorIden, IsIatrogenic);
                        rownOperationDetailJSON = "{" + rownOperationDetailJSON + "}";
                        string rownOperationJSON = string.Format(Function.ListOperationJSON, OperationRecordNo1, OperationDoctorCode, OperationDoctorName, FirstOperdoctorcode,
                                                                FirstOperdoctorname, SecondOperdoctorcode, SecondOperdoctorname, AnesthesiologistCode, AnesthesiologistName,
                                                                OperationDate, OperationFinishDate, AnaesthesiaType, IsComplication, ComplicationCode, ComplicationName, OperationRecord,
                                                                RecordDoctorCode, RecordDoctorName, rownOperationDetailJSON);


                        jsonOperationDetailList += "{" + rownOperationJSON + "}";
                        if (iii < alOperationList.Count - 1)
                        {
                            jsonOperationDetailList += ",";
                        }
                        iii++;
                    }
                        #endregion

                    jsonOperationList = "'ListOperation': [" + jsonOperationDetailList + "]";
                }
                else
                {
                    jsonOperationList = string.Empty;
                    jsonOperationList = "'ListOperation': [" + jsonOperationList + "]";

                }
                #endregion

                //
                //DataTable dtZYZLXX = this.inMgr.DRGSZYZLXX(patientID);

                //仅一条
                if (dtJBXX != null && dtJBXX.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dtJBXX.Rows)
                    {

                        JsonZYZLXX.Append("{" + string.Format(Function.ZYZLXXJson,
                                                    dRow["ZYYLLX"].ToString(), //0
                                                    dRow["RYTJ"].ToString(), //1
                                                    dRow["ZLLB"].ToString(), //2
                                                    dRow["RYSJ"].ToString(), //3
                                                    dRow["RYKB"].ToString(), //4
                                                    dRow["ZKKB"].ToString(), //5
                                                    dRow["CYSJ"].ToString(), //6
                                                    dRow["CYKB"].ToString(), //7
                                                    dRow["SJZY"].ToString(), //8
                                                    firstDiagName, //9
                                                    firstDiag, //10
                                                    dRow["MJZZD1"].ToString(), //11门急诊诊断2
                                                    dRow["MJZXYJBDM"].ToString(), //12门急诊中医疾病代码
                                                    firstDiagName, //13
                                                    firstDiag, //14
                                                    "4", //15西医主要诊断入院病情
                                                    string.IsNullOrEmpty(QTZDStr) ? "" : "'QTZD':[" + QTZDStr + "],", //16
                                                    "", //17主病 出院中医诊断
                                                    "", //18主病疾病代码 出院中医诊断，中医主病疾病代码
                                                    "", //19主病入院病情
                                                    string.IsNullOrEmpty(QTZYZDStr) ? "" : "'QTZYZD':[" + QTZYZDStr + "],", //20
                                                    diagCount.ToString(), //21诊断代码计数
                                                    string.IsNullOrEmpty(jsonOperationList) ? "" : jsonOperationList + ",", //22
                                                    operationCount.ToString(), //23手术及操作代码计数
                                                    "0", //24呼吸机使用时间
                                                    dRow["RYQLNSSHZHMSJ"].ToString(), //25
                                                    dRow["RYHLNSSHZHMSJ"].ToString(), //26
                                                    dRow["ZZJHBFLX"].ToString(), //27
                                                    dRow["JZZJHSSJ"].ToString(), //28
                                                    dRow["CZZJHSSJ"].ToString(), //29
                                                    dRow["HJ"].ToString(), //30
                                                    dRow["SXPZ"].ToString(), //31
                                                    dRow["SXL"].ToString(), //32
                                                    dRow["SXJLDW"].ToString(), //33
                                                    dRow["TJHLTS"].ToString(), //34
                                                    dRow["YJHLTS"].ToString(), //35
                                                    dRow["EJHLTS"].ToString(), //36
                                                    dRow["SJHLTS"].ToString(), //37
                                                    dRow["LYFS"].ToString(), //38
                                                    dRow["NJSJGMC"].ToString(), //39
                                                    dRow["NJSJGDM"].ToString(), //40
                                                    dRow["SFYCY31TNZZYJH"].ToString(), //41
                                                    dRow["ZZYJHMD"].ToString(), //42
                                                    dRow["ZZYSXM"].ToString(), //43
                                                    dRow["ZZYSDM"].ToString(), //44
                                                    dRow["SciCardNo"].ToString(), //45
                                                    dRow["OutBedNum"].ToString(), //46
                                                    dRow["DoctorCode"].ToString(), //47
                                                    dRow["DoctorName"].ToString(), //48
                                                    dRow["IsDrugAllergy"].ToString(), //49
                                                    dRow["AllergyDrugCode"].ToString(), //50
                                                    dRow["AllergyDrugName"].ToString(), //51
                                                    dRow["IsPathologicalExamination"].ToString(), //52
                                                    dRow["PathologyCode"].ToString(), //53
                                                    dRow["IsHospitalInfected"].ToString(), //54
                                                    dRow["HospitalInfectedCode"].ToString(), //55
                                                    dRow["BloodTypeS"].ToString(), //56
                                                    dRow["BloodTypeE"].ToString(), //57
                                                    dRow["ChiefComplaint"].ToString(), //58
                                                    dRow["MedicalHistory"].ToString(), //59
                                                    dRow["SurgeryHistory"].ToString(), //60
                                                    dRow["BloodTransHistory"].ToString(), //61
                                                    dRow["Marriage"].ToString(), //62
                                                    dRow["Height"].ToString(), //63
                                                    dRow["Weight"].ToString(), //64
                                                    dRow["BearPregnancy"].ToString(), //65
                                                    dRow["BearYie"].ToString(), //66
                                                    dRow["DiagnosePosition1"].ToString(), //67
                                                    dRow["DiagnosePosition2"].ToString(), //68
                                                    dRow["Tsblbs"].ToString() //69
                                                    ) + "}");
                        break;
                    }
                }
                #endregion

                #region 医疗收费信息(YYSFXX)

                //住院总费用
                ArrayList alFee = new ArrayList();

                alFee = this.inMgr.QueryAllFeeItemListsByInvoiceNO(invoiceNO, inPatientInfo.ID, jydjh);
                if (alFee == null || alFee.Count <= 0)
                {
                    this.InsertErrLog(ybMedNo, "2", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,GZSI_HIS_CFXM未找到对应的处方信息1!").ToString());
                    return -1;
                }
                decimal totCost = 0m;              //明细总金额
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fTemp in alFee)
                {
                    totCost += fTemp.FT.TotCost;
                }
                //
                DataTable dtYYSFXX = this.inMgr.DRGSYYSFXX(patientID, invoiceNO, jydjh);

                decimal sbTotCE = 0m;
                //仅一条
                if (dtYYSFXX != null && dtYYSFXX.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dtYYSFXX.Rows)
                    {
                        sbTotCE = totCost - decimal.Parse(dRow["SBTOT"].ToString());
                        decimal qtTot = 0m;
                        if (sbTotCE <= 1)
                        {
                            qtTot = decimal.Parse(dRow["QTF_JE"].ToString()) - sbTotCE;
                        }
                        else
                        {
                            qtTot = decimal.Parse(dRow["QTF_JE"].ToString());
                        }
                        JsonYYSFXX.Append("{" + string.Format(Function.YYSFXXJson,
                                                        dRow["YWLSH"].ToString(), //0
                                                        dRow["PJDM"].ToString(), //1
                                                        dRow["PJHM"].ToString(), //2
                                                        dRow["JSRQ"].ToString(), //3
                                                        dRow["SBTOT"].ToString(),//dRow["JEHJ_JE"].ToString(), //4
                                                        "0",//dRow["JEHJ_JL"].ToString(), //5
                                                        "0",//dRow["JEHJ_YL"].ToString(), //6
                                                        "0",// dRow["JEHJ_ZF"].ToString(), //7
                                                        dRow["SBTOT"].ToString(),//dRow["JEHJ_QT"].ToString(), //8
                                                        dRow["CWF_JE"].ToString(), //9
                                                        dRow["CWF_JL"].ToString(), //10
                                                        dRow["CWF_YL"].ToString(), //11
                                                        dRow["CWF_ZF"].ToString(), //12
                                                        dRow["CWF_QT"].ToString(), //13
                                                        dRow["ZCF_JE"].ToString(), //14
                                                        dRow["ZCF_JL"].ToString(), //15
                                                        dRow["ZCF_YL"].ToString(), //16
                                                        dRow["ZCF_ZF"].ToString(), //17
                                                        dRow["ZCF_QT"].ToString(), //18
                                                        dRow["JCF_JE"].ToString(), //19
                                                        dRow["JCF_JL"].ToString(), //20
                                                        dRow["JCF_YL"].ToString(), //21
                                                        dRow["JCF_ZF"].ToString(), //22
                                                        dRow["JCF_QT"].ToString(), //23
                                                        dRow["HYF_JE"].ToString(), //24
                                                        dRow["HYF_JL"].ToString(), //25
                                                        dRow["HYF_YL"].ToString(), //26
                                                        dRow["HYF_ZF"].ToString(), //27
                                                        dRow["HYF_QT"].ToString(), //28
                                                        dRow["ZLF_JE"].ToString(), //29
                                                        dRow["ZLF_JL"].ToString(), //30
                                                        dRow["ZLF_YL"].ToString(), //31
                                                        dRow["ZLF_ZF"].ToString(), //32
                                                        dRow["ZLF_QT"].ToString(), //33
                                                        dRow["SSF_JE"].ToString(), //34
                                                        dRow["SSF_JL"].ToString(), //35
                                                        dRow["SSF_YL"].ToString(), //36
                                                        dRow["SSF_ZF"].ToString(), //37
                                                        dRow["SSF_QT"].ToString(), //38
                                                        dRow["HLF_JE"].ToString(), //39
                                                        dRow["HLF_JL"].ToString(), //40
                                                        dRow["HLF_YL"].ToString(), //41
                                                        dRow["HLF_ZF"].ToString(), //42
                                                        dRow["HLF_QT"].ToString(), //43
                                                        dRow["WSCLF_JE"].ToString(), //44
                                                        dRow["WSCLF_JL"].ToString(), //45
                                                        dRow["WSCLF_YL"].ToString(), //46
                                                        dRow["WSCLF_ZF"].ToString(), //47
                                                        dRow["WSCLF_QT"].ToString(), //48
                                                        dRow["XYF_JE"].ToString(), //49
                                                        dRow["XYF_JL"].ToString(), //50
                                                        dRow["XYF_YL"].ToString(), //51
                                                        dRow["XYF_ZF"].ToString(), //52
                                                        dRow["XYF_QT"].ToString(), //53
                                                        dRow["ZYYPF_JE"].ToString(), //54
                                                        dRow["ZYYPF_JL"].ToString(), //55
                                                        dRow["ZYYPF_YL"].ToString(), //56
                                                        dRow["ZYYPF_ZF"].ToString(), //57
                                                        dRow["ZYYPF_QT"].ToString(), //58
                                                        dRow["ZCYF_JE"].ToString(), //59
                                                        dRow["ZCYF_JL"].ToString(), //60
                                                        dRow["ZCYF_YL"].ToString(), //61
                                                        dRow["ZCYF_ZF"].ToString(), //62
                                                        dRow["ZCYF_QT"].ToString(), //63
                                                        dRow["YBZLF_JE"].ToString(), //64
                                                        dRow["YBZLF_JL"].ToString(), //65
                                                        dRow["YBZLF_YL"].ToString(), //66
                                                        dRow["YBZLF_ZF"].ToString(), //67
                                                        dRow["YBZLF_QT"].ToString(), //68
                                                        dRow["GHF_JE"].ToString(), //69
                                                        dRow["GHF_JL"].ToString(), //70
                                                        dRow["GHF_YL"].ToString(), //71
                                                        dRow["GHF_ZF"].ToString(), //72
                                                        dRow["GHF_QT"].ToString(), //73
                                                        qtTot.ToString(),//dRow["QTF_JE"].ToString(), //74
                                                        "0",//dRow["QTF_JL"].ToString(), //75
                                                        "0",//dRow["QTF_YL"].ToString(), //76
                                                        "0",//dRow["QTF_ZF"].ToString(), //77
                                                        qtTot.ToString(),//dRow["QTF_QT"].ToString(), //78
                                                        dRow["YBTCJJZF"].ToString(), //79
                                                        dRow["QTZF"].ToString(), //80
                                                        dRow["DBBXZF"].ToString(), //81
                                                        dRow["YLJZZF"].ToString(), //82
                                                        dRow["GWYYLBZ"].ToString(), //83
                                                        dRow["DEBC"].ToString(), //84
                                                        dRow["QYBC"].ToString(), //85
                                                        dRow["GRZFU"].ToString(), //86
                                                        dRow["GRZFEI"].ToString(), //87
                                                        dRow["GRZHZF"].ToString(), //88
                                                        dRow["GRXJZF"].ToString(), //89
                                                        dRow["YBZFFS"].ToString(), //90
                                                        dRow["YLJGTBBM"].ToString(), //91
                                                        dRow["YLJGTBR"].ToString(), //92
                                                        dRow["YBJG"].ToString(), //93
                                                        dRow["YBJGJBR"].ToString() //94
                                                        ) + "}");
                        break;
                    }
                }

                #endregion

                #region 检查单(JCD)

                #endregion

                #region 出院小结(CYXJ)

                string jsonOutRecord = string.Empty;
                Const conOutRecord = this.inMgr.QueryPatientOutRecord(patientID);
                if (conOutRecord != null)
                {
                    jsonOutRecord = string.Format(Function.LeaveHospitalJSON, inPatientInfo.PVisit.ZG.ID,  //出院转归
                                                                            conOutRecord.ID.Replace('<', ' ').Replace('>', ' ').Replace('/', ' ').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", "").Replace("\\", "").Replace("℃", ""),              //入院情况
                                                                            conOutRecord.Name.Replace('<', ' ').Replace('>', ' ').Replace('/', ' ').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", "").Replace("\\", "").Replace("℃", ""),            //诊疗过程
                                                                            conOutRecord.UserCode.Replace('<', ' ').Replace('>', ' ').Replace('/', ' ').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", "").Replace("\\", "").Replace("℃", ""),        //出院情况
                                                                            conOutRecord.Memo.Replace('<', ' ').Replace('>', ' ').Replace('/', ' ').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", "").Replace("\\", "").Replace("℃", "")         //出院医嘱
                                                                            );
                }
                JsonCYXJ.Append("{" + jsonOutRecord + "}");


                #endregion

                #region 就诊明细(JZMX)

                StringBuilder strRowDetails = new StringBuilder();
                decimal totdetailCostT = 0m;
                decimal totCostT = 0m;
                try
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = alFee[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    if (feeItem != null)
                    {
                        string inpatientNO = feeItem.ID;   //住院流水号，作为处方号

                        //存放项目汇总信息
                        Hashtable hsUpLoadFeeDetails = new Hashtable();
                        List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeGatherlsClone = new List<FS.HISFC.Models.Fee.Inpatient.FeeItemList>();

                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fTemp in alFee)
                        {

                            //相同的项目进行累加汇总后再进行上传 【划价时间+项目编码】
                            if (hsUpLoadFeeDetails.ContainsKey(fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID))
                            {
                                FS.HISFC.Models.Fee.FeeItemBase feeItemList = hsUpLoadFeeDetails[fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID] as FS.HISFC.Models.Fee.FeeItemBase;

                                feeItemList.Item.Qty += fTemp.Item.Qty;//数量累加

                                feeItemList.FT.TotCost += fTemp.FT.TotCost;//金额累加
                                feeItemList.FT.OwnCost += fTemp.FT.OwnCost;   //自费金额累计
                                feeItemList.FT.PubCost += fTemp.FT.PubCost;   //医保金额累加
                                feeItemList.FT.PayCost += fTemp.FT.PayCost;
                            }
                            else
                            {
                                hsUpLoadFeeDetails.Add(fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID, fTemp);
                                feeGatherlsClone.Add(fTemp);
                            }

                        }
                        //补差额项目
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemCE = null;
                        //MessageBox.Show("差额：" + sbTotCE.ToString() + " 总额：" + totCost.ToString());
                        for (int k = 0; k < feeGatherlsClone.Count; k++)
                        {
                            feeItem = feeGatherlsClone[k] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                            if (feeItem.FT.TotCost > sbTotCE && sbTotCE > 0 && feeItemCE == null)
                            {
                                feeItemCE = feeItem.Clone();
                                feeItemCE.FT.TotCost = feeItem.FT.TotCost - sbTotCE;
                                feeItemCE.FT.OwnCost = feeItem.FT.TotCost - sbTotCE;

                                feeItem = feeItemCE.Clone();

                            }
                            string errMsg = string.Empty;
                            if ((feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost) == 0)
                            {
                                continue;
                            }
                            // {E1865220-C459-45ca-966E-F111E0A6B560}
                            string dose_once = "";
                            string frequency = "";
                            string usage = "";
                            string comb_no = "";
                            string type_code = "";
                            string recipe_no = "";
                            this.inMgr.GetOrderInfo(inPatientInfo.ID, feeItem.Item.GBCode, ref dose_once, ref frequency, ref usage, ref comb_no, ref type_code, ref recipe_no);


                            //项目编码   传社保中心码 
                            string itemCode = string.Empty;
                            string itemName = feeItem.Item.Name.Replace('<', ' ').Replace('/', ' ').Replace('>', ' ');

                            #region 自定义码处理
                            if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                            {
                                this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护1！").ToString());
                                return -1;
                            }

                            //获取社保中心编码和名称
                            if (this.dictSiItem.ContainsKey(feeItem.Item.UserCode))
                            {
                                Const c = dictSiItem[feeItem.Item.UserCode];
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护2！").ToString());
                                    return -1;
                                }
                                itemCode = c.UserCode;
                            }
                            else
                            {
                                Const c = this.inMgr.QuerySiCompare(feeItem.Item.UserCode);
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护4！").ToString());
                                    return -1;
                                }

                                this.dictSiItem.Add(feeItem.Item.UserCode, c);
                                itemCode = c.UserCode;
                            }

                            #endregion


                            //项目总量，单价，总额，收费日期
                            string itemQty = feeItem.Item.Qty.ToString();
                            //string itemPrice = System.Math.Round(feeItem.Item.PackQty == 0 ? feeItem.Item.Price : feeItem.Item.Price / feeItem.Item.PackQty, 4).ToString();
                            string itemPrice = System.Math.Round((feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost) / feeItem.Item.Qty, 4).ToString();
                            string itemCost = (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost).ToString();
                            totCostT += (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost);
                            totdetailCostT += System.Math.Round((feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost) / feeItem.Item.Qty, 4) * feeItem.Item.Qty;
                            //DateTime feeDate = feeDate;      //住院应为结算日期
                            if (string.IsNullOrEmpty(inPatientInfo.PVisit.AdmittingDoctor.ID))
                            {
                                inPatientInfo.PVisit.AdmittingDoctor.ID = inPatientInfo.PVisit.AttendingDoctor.ID;
                                if (string.IsNullOrEmpty(inPatientInfo.PVisit.AdmittingDoctor.ID))
                                {
                                    inPatientInfo.PVisit.AdmittingDoctor.ID = inPatientInfo.PVisit.ConsultingDoctor.ID;
                                }
                            }
                            //处方医生
                            if (string.IsNullOrEmpty(feeItem.RecipeOper.ID))
                            {
                                feeItem.RecipeOper.ID = inPatientInfo.PVisit.AdmittingDoctor.ID;
                            }
                            string recipeDoctId = feeItem.RecipeOper.ID;
                            Employee emp = this.GetEmpoyee(recipeDoctId, ref errMsg);
                            if (emp == null && !string.IsNullOrEmpty(inPatientInfo.PVisit.AdmittingDoctor.ID))
                            {
                                //MessageBox.Show("医生【" + recipeDoctId + "】找不到基本信息！");
                                //this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("医生【" + recipeDoctId + "】找不到基本信息！").ToString());
                                //return -1;
                                recipeDoctId = inPatientInfo.PVisit.AdmittingDoctor.ID;
                            }
                            else
                            {
                            }
                            if (emp != null && emp.EmployeeType.ID.ToString() != "D")
                            {
                                recipeDoctId = inPatientInfo.PVisit.AdmittingDoctor.ID;
                            }
                            string recipeDoctName = this.GetEmpoyeeName(recipeDoctId, ref errMsg);

                            //科室名称 字典bqdm

                            if (string.IsNullOrEmpty(feeItem.RecipeOper.Dept.ID))
                            {
                                feeItem.RecipeOper.Dept.ID = inPatientInfo.PVisit.PatientLocation.Dept.ID;
                            }
                            string deptName = this.GetDeptName(feeItem.RecipeOper.Dept.ID, ref errMsg);

                            #region 组装Row

                            string mxsfdjh = ybMedNo;  //patientType + "-" + invoiceNO + "-" + transType;
                            string mxwylsh = (k + 1).ToString();//feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString();//RECIPE_NO, TRANS_TYPE, SEQUENCE_NO
                            DateTime inDate = DateTime.Parse(inPatientInfo.PVisit.InTime.ToShortDateString());
                            DateTime outDate = DateTime.Parse(inPatientInfo.PVisit.OutTime.AddDays(1).ToShortDateString());

                            if (!(inDate <= feeItem.ChargeOper.OperTime && outDate > feeItem.ChargeOper.OperTime))
                            {
                                feeItem.ChargeOper.OperTime = inPatientInfo.PVisit.OutTime;//费用不在出入院时间内的取出院时间
                            }
                            string fwrq = feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
                            string xmbm = itemCode;
                            string xmmc = itemName;
                            string xmlb = (feeItem.Item.ItemType == EnumItemType.Drug ? "1" : "0"); //1为“药品”，0为“其他”
                            string dj = itemPrice;
                            string sl = itemQty;
                            string zj = itemCost;
                            string ysbm = recipeDoctId;
                            string ysmc = recipeDoctName;
                            string ksbm = this.inMgr.GetKB(feeItem.RecipeOper.Dept.ID);
                            string ksmc = deptName;

                            string yf = (feeItem.Item.ItemType == EnumItemType.Drug ? feeItem.Item.PriceUnit : "0"); //对应数量的唯一计价单位(比如片、支、瓶、袋等)，非药品填0
                            //给药途径【字典】给药途径为空则取 900 其他用药途径
                            feeItem.Order.Usage.ID = usage;
                            string gytj = (string.IsNullOrEmpty(feeItem.Order.Usage.ID) ? "900" : feeItem.Order.Usage.ID);
                            if (!string.IsNullOrEmpty(feeItem.Order.Usage.ID))
                            {
                                FS.HISFC.Models.Base.Const objGytj = this.gytjHelper.GetObjectFromID(feeItem.Order.Usage.ID) as FS.HISFC.Models.Base.Const;
                                if (objGytj != null && !string.IsNullOrEmpty(objGytj.ID))
                                {
                                    gytj = objGytj.UserCode;
                                }
                                else
                                {
                                    gytj = "900";
                                }
                            }

                            string yl = string.IsNullOrEmpty(dose_once) ? "0" : dose_once;//feeItem.Order.DoseOnce.ToString("F2");

                            //频次【字典】
                            feeItem.Order.Frequency.ID = frequency;
                            string pc = (string.IsNullOrEmpty(feeItem.Order.Frequency.ID) ? "-1" : feeItem.Order.Frequency.ID); //11	每天一次（qd）
                            if (!string.IsNullOrEmpty(feeItem.Order.Frequency.ID))
                            {
                                FS.HISFC.Models.Base.Const objSypc = this.sypcHelper.GetObjectFromID(feeItem.Order.Frequency.ID) as FS.HISFC.Models.Base.Const;
                                if (objSypc != null && !string.IsNullOrEmpty(objSypc.ID))
                                {
                                    pc = objSypc.UserCode;
                                }
                            }

                            string yyts = "1";//feeItem.Days.ToString();
                            string ybnje = "";
                            string jylx = "1";   //标识是否为冲减单据，1是0否
                            string ypgg = (string.IsNullOrEmpty(feeItem.Item.Specs) ? "次" : feeItem.Item.Specs).Replace('"', ' ');
                            string cydybs = "0";
                            string zyh = patientID;
                            feeItem.RecipeNO = recipe_no;
                            string yzzdid = (!string.IsNullOrEmpty(feeItem.Order.ID) ? feeItem.Order.ID : (feeItem.RecipeNO));
                            string yzmxid = (!string.IsNullOrEmpty(feeItem.Order.ID) ? feeItem.Order.ID : (feeItem.RecipeNO)); //feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString();//RECIPE_NO, TRANS_TYPE, SEQUENCE_NO
                            string sbxh = "";
                            string ypbwm = "-";//药品本位码
                            string yyxmbm = feeItem.Item.UserCode;
                            try
                            {
                                string rowJSON = string.Format(Function.FeeDetailJSON, mxsfdjh, mxwylsh, fwrq, xmbm, xmmc, xmlb, dj, sl, zj, ysbm,
                                                                                    ysmc, ksbm, ksmc, yf, gytj, yl, pc, yyts, ybnje, jylx,
                                                                                    ypgg, cydybs, zyh, yzzdid, yzmxid, sbxh, ypbwm, yyxmbm);

                                strRowDetails.Append("\r\n{" + rowJSON + "},");

                            }
                            catch (OutOfMemoryException e)
                            {
                                this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("调用异常：" + "System.OutOfMemoryException" + e.Message).ToString());
                                return -1;
                            }

                            #endregion

                        }

                    }

                    //MessageBox.Show("费用明细总额（单价* 数量）：" + totdetailCostT.ToString() + " 费用总价合计：" + totCostT.ToString());

                }
                catch (Exception e)
                {
                    this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("调用异常：" + e.Message).ToString());
                }

                if (strRowDetails != null && !string.IsNullOrEmpty(strRowDetails.ToString()) && strRowDetails.ToString().Length > 1)
                {
                    JsonJZMX.Append("[" + strRowDetails.ToString().Substring(0, strRowDetails.ToString().Length - 1) + "]");
                }
                else
                {
                    this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("就诊明细实体为空！").ToString());
                    return -1;
                }
                #endregion


                ALLJson.Append(string.Format(Function.BaseMainJsonZY, JsonJBXX.ToString(),
                    JsonZYZLXX.ToString(), JsonYYSFXX.ToString(), JsonCYXJ.ToString(), JsonJZMX.ToString()));


                string errInfo = string.Empty;
                int result = this.baseJob.UploadInfoNew("JSQD5001", ALLJson,false, ref errInfo);

                if (result <= 0)
                {
                    this.InsertErrLog(ybMedNo,"2", patientID, invoiceNO, ("上传【结算单信息】出错@" + errInfo).ToString());
                    return -1;
                }
                return result;

                #endregion
            }

            return 1;
        }


        public void WriteLog(string logInfo)
        {
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Log/DRGSLOG") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/DRGSLOG");
            }
            //保存一周的日志
            System.IO.File.Delete("./Log/DRGSLOG/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/DRGSLOG/" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
        #endregion

        /// <summary>
        /// 处理项目对照 - 处理国标码
        /// </summary>
        /// <param name="f">要处理的项目</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>成功返回1；失败返回-1</returns>
        private int DealFeeItemList(FS.HISFC.Models.Fee.FeeItemBase f, ref string errMsg)
        {
            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                #region 药品处理

                if (this.dictDrug.ContainsKey(f.Item.ID))
                {
                    FS.HISFC.Models.Pharmacy.Item phaItem = this.dictDrug[f.Item.ID];
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

                    FS.HISFC.Models.Pharmacy.Item phaItem = this.itemPharmacyManager.GetItem(f.Item.ID);
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
            else if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
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
                        FS.HISFC.Models.Base.Item baseItem = this.dictUndrug[f.Item.ID];
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

                        FS.HISFC.Models.Base.Item baseItem = this.itemManager.GetUndrugByCode(f.Item.ID);
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

            ////费用类大类
            //ArrayList alFLDM = this.ConsMgr.GetConstantList("SporadicFee");
            //if (alFLDM != null)
            //{
            //    this.fldmHelper.ArrayObject = alFLDM;
            //}
            ////科室
            //ArrayList alBQDM = this.ConsMgr.GetConstantList("SporadicDept");
            //if (alBQDM != null)
            //{
            //    this.bqdmHelper.ArrayObject = alBQDM;
            //}

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

            //this.lblPatientNo.Visible = true;
            //this.neuLabel2.Visible = true;
            base.OnLoad(e);
        }
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        private bool isInit = false;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            if (!isInit)
            {
                this.OnLoad(null);

                isInit = true;
            }

            return 1;
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
            
            this.toolBarService.AddToolButton("上传医师", "医师库上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.R人员, true, false, null);

            this.toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

               
            this.toolBarService.AddToolButton("更新出院记录", "更新出院记录", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);

      
            this.toolBarService.AddToolButton("查询结算清单", "查询结算清单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
           

            this.toolBarService.AddToolButton("结算清单上传", "结算清单上传", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            this.toolBarService.AddToolButton("结算清单作废", "结算清单作废", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            this.toolBarService.AddToolButton("插入自费结算信息", "插入自费结算信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

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

                case "上传医师":
                    this.UploadEmployee();
                    break;
                case "更新出院记录":
                    this.UpdateVest();
                    break;
                case "清屏":
                    this.Clear();
                    break;
                case "查询结算清单":
                    this.QueryNeedUploadDetail(true);
                    break;
                case "结算清单上传":
                    this.AllUploadPatientCaseNew();
                    break;
                case "结算清单作废":
                    this.CancelUploadInfoNew();
                    break;
                case "插入自费结算信息":
                    DiseasePay.frmInsertSIFee frmInsertSIFee = new DiseasePay.frmInsertSIFee();
                    frmInsertSIFee.Show(this);
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        /// <summary>
        /// 出院记录更新弹出窗口
        /// </summary>
        protected Form fPopWin = new Form();
        /// <summary>
        /// 出院记录更新
        /// </summary>
        private ucUpdateVest ucUpdateVest = new ucUpdateVest();
        /// <summary>
        /// 初始化多患者弹出窗口
        /// </summary>
        protected virtual void InitPopShowPatient()
        {
            fPopWin = new Form();
            fPopWin.Width = ucUpdateVest.Width + 10;
            fPopWin.MinimizeBox = false;
            fPopWin.MaximizeBox = false;
            fPopWin.Controls.Add(ucUpdateVest);
            ucUpdateVest.Dock = DockStyle.Fill;
            fPopWin.Height = ucUpdateVest.Height + 500;
            fPopWin.Visible = false;
            fPopWin.KeyDown += new KeyEventHandler(fPopWin_KeyDown);
        }

        /// <summary>
        /// 打开患者多次挂号UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void fPopWin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.fPopWin.Close();
            }
        }

        /// <summary>
        /// 更新出院记录
        /// </summary>
        private void UpdateVest()
        {

            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("没有患者信息!");
                return;
            }
            int rowIndex = this.fpNeedUpload_Sheet1.ActiveRowIndex;
            if (rowIndex < 0 || this.fpNeedUpload_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择更新的患者!");
                return;
            }
            string patientID = "";
            patientID = this.fpNeedUpload_Sheet1.Cells[rowIndex, 1].Tag.ToString();
            if (string.IsNullOrEmpty(patientID))
            {
                MessageBox.Show("患者流水号为空！");
                return;
            }
            FS.HISFC.Models.RADT.PatientInfo inPatientInfo = this.radtIntegrate.GetPatientInfomation(patientID);

            if (inPatientInfo == null)
            {
                MessageBox.Show("查找不到患者信息！");
                return;
            }

            ucUpdateVest = new ucUpdateVest();
            this.ucUpdateVest.InPatientNo = patientID;
            this.ucUpdateVest.IsShowSave = true;
            this.ucUpdateVest.PatientNo = inPatientInfo.PID.PatientNO;
            this.ucUpdateVest.Qty = inPatientInfo.InTimes.ToString();

            //string RYQK = "";
            //string ZLGC = "";
            //string CYQK = "";
            //string CYYZ = "";
            //string SWJLRYQK = "";
            //string SWJLZLGC = "";

            //RYQK = this.ucUpdateVest.QueryRYQK(patientID);
            //ZLGC = this.ucUpdateVest.QueryZLGC(patientID);
            //CYQK = this.ucUpdateVest.QueryCYQK(patientID);
            //CYYZ = this.ucUpdateVest.QueryCYYZ(patientID);
            //SWJLRYQK = this.ucUpdateVest.QuerySWJLRYQK(patientID);
            //SWJLZLGC = this.ucUpdateVest.QuerySWJLZLGC(patientID);

            //this.ucUpdateVest.RYQK1 = RYQK;
            //this.ucUpdateVest.ZLGC1 = ZLGC;
            //this.ucUpdateVest.CYQK1 = CYQK;
            //this.ucUpdateVest.CYYZ1 = CYYZ;
            //this.ucUpdateVest.SWJLRYQK1 = SWJLRYQK;
            //this.ucUpdateVest.SWJLZLGC1 = SWJLZLGC;
            this.InitPopShowPatient();
            fPopWin.ShowDialog();

        }

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
            else if (this.tabMain.SelectedTab == this.tpUploaded)
            {
                #region 已上传

                if (this.fpHaveUploaded_Sheet1.Rows.Count > 0)
                {
                    for (int k = 0; k < this.fpHaveUploaded_Sheet1.Rows.Count; k++)
                    {
                        if (this.cbChooseAll.Checked)
                        {
                            this.fpHaveUploaded_Sheet1.Cells[k, 0].Value = true;
                        }
                        else
                        {
                            this.fpHaveUploaded_Sheet1.Cells[k, 0].Value = false;
                        }
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// 插入失败记录
        /// </summary>
        /// <param name="type"></param>
        /// <param name="inpatientNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int InsertErrLog(string jslsh,string type, string inpatientNo, string invoiceNo, string errInfo)
        {
            DataRow dt = null;
            if (this.patientInfoList.ContainsKey(jslsh))
            {
                dt = patientInfoList[jslsh] as DataRow;
                if (dt != null)
                {
                    dt["err"] = errInfo;
                    patientInfoList.Remove(jslsh);
                    patientInfoList.Add(jslsh, dt);
                }
            }
            string sql = @"insert into upload_err_log
                            (INPATIENT_NO,
                            INVOICE_NO,
                            ERR,
                            TYPE,
                            OPER_CODE,
                            OPER_DATE) values
                            ('{0}','{1}','{2}','{3}','{4}',to_date('{5}','yyyy-mm-dd hh24:mi:ss') )
                            ";
            errInfo.Replace("'", " ");
            sql = string.Format(sql, inpatientNo, invoiceNo, errInfo.Replace("'", " "), type, this.con.Operator.ID, this.con.GetDateTimeFromSysDateTime().ToString());

            return this.con.ExecNoQuery(sql);
        }

        /// <summary>
        /// 成功后删除报错记录
        /// </summary>
        /// <param name="type"></param>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int DeleteErrLog(string type, string inpatientNo)
        {
            string sql = "delete from upload_err_log where type = '{0}' and INPATIENT_NO = '{1}'";
            sql = string.Format(sql, type, inpatientNo);
            return this.con.ExecNoQuery(sql);
        }
        /// <summary>
        /// 测试查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            ////门诊
            //int rev = this.QueryUploadedMainInfo("1", "440602197903020010", "0000503397", "A-031-170905350009-177139392", "77139392");

            ////住院
            //rev = this.QueryUploadedMainInfo("2", "442821196004183163", "0000071724", "A-032-170905050002-177149470", "77149470");


            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region 枚举


        #endregion

        private void cmbPatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFP();
        }

        private void tabMain_Click(object sender, EventArgs e)
        {

        }


    }
}
