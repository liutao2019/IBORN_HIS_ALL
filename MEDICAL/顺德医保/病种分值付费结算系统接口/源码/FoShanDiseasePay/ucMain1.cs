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
using System.Text.RegularExpressions;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 佛山市按病种分值付费结算接口
    /// </summary>
    public partial class ucMain : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucMain()
        {
            InitializeComponent();
            ArrayList al = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject("ALL", "全部", "");
            al.Add(obj);
            obj = new Neusoft.FrameWork.Models.NeuObject("C", "门诊", "");
            al.Add(obj);
            obj = new Neusoft.FrameWork.Models.NeuObject("I", "住院", "");
            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
            {
                al.Add(obj);
                obj = new Neusoft.FrameWork.Models.NeuObject("Z", "六大类重症患者", "");
                al.Add(obj);
                obj = new Neusoft.FrameWork.Models.NeuObject("F", "非六大类重症患者", "");
            }
            al.Add(obj);
            this.cmbPatientType.AddItems(al);
            this.cmbPatientType.Tag = patientType;
            //ArrayList list = con.GetList("CASELEVEL");
            //if (list != null)
            //{
            //    LevelType.AddItems(list);
            //    LevelHelper.ArrayObject = list;
            //}


        }

        #region 变量和属性

        private Manager manger = new Manager();

        /// <summary>
        /// 接口基类
        /// </summary>
        private FoShanDiseasePay.Jobs.BaseJob baseJob = new BaseJob();
        //手术级别列表
        private Neusoft.FrameWork.WinForms.Controls.PopUpListBox LevelType = new Neusoft.FrameWork.WinForms.Controls.PopUpListBox();
        private Neusoft.FrameWork.Public.ObjectHelper LevelHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        Neusoft.HISFC.BizLogic.Manager.Constant con = new Neusoft.HISFC.BizLogic.Manager.Constant();

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

        /// <summary>
        /// 手术分类
        /// </summary>
        private Neusoft.HISFC.BizLogic.HealthRecord.Operation operationManager = new Neusoft.HISFC.BizLogic.HealthRecord.Operation();

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

        /// <summary>
        /// 患者类型
        /// </summary>
        private string patientType = "ALL";

        #endregion


        #region 控制参数

        [Category("控件设置"), Description("加载患者类型,默认是ALL，I 为住院 ， C 为门诊")]
        public string PatientType
        {
            get
            {
                return patientType;
            }
            set
            {
                patientType = value;
            }
        }

        #endregion
        #region 方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            //当前时间
            DateTime dtNow = this.outMgr.GetDateTimeFromSysDateTime();

            this.lblLoginInfo.Text = "请先登录!";
            this.lblLoginInfo.ForeColor = System.Drawing.Color.Red;

            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;

            this.dtBeginTime.Value = dtNow.Date;
            this.dtEndTime.Value = dtNow.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 待上传患者
        /// </summary>
        private void QueryNeedUploadDetail()
        {
            //清屏
            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            this.cbChooseAll.Checked = true;
            //时间段
            string strBeginTime = this.dtBeginTime.Value.ToString();
            string strEndTime = this.dtEndTime.Value.ToString();

            if (patientType == "C" || patientType == "ALL")
            {
                //待上传的门诊患者
                DataTable dtOutpatient = this.outMgr.QueryNeedUploadDetail(strBeginTime, strEndTime);
                if (dtOutpatient != null && dtOutpatient.Rows.Count > 0)
                {
                    this.AddNeedUpload(dtOutpatient);
                }
            }
            if (patientType == "I" || patientType == "ALL")
            {
                //待上传的住院患者
                DataTable dtInpatient = this.inMgr.QueryNeedUploadDetail(strBeginTime, strEndTime);
                if (dtInpatient != null && dtInpatient.Rows.Count > 0)
                {
                    this.AddNeedUpload(dtInpatient);
                }
            }
            if (patientType == "Z")//佛三要求区分重症患者
            {

                DataTable dtZZInpatient = this.inMgr.QueryNeedUploaSevereCasedDetail(strBeginTime, strEndTime);
                if (dtZZInpatient != null && dtZZInpatient.Rows.Count > 0)
                {
                    this.AddNeedUpload(dtZZInpatient);
                }
            }
            if (patientType == "F")//佛三非重症患者
            {

                DataTable dtZZInpatient = this.inMgr.QueryNeedUploaNoSevereCasedDetail(strBeginTime, strEndTime);
                if (dtZZInpatient != null && dtZZInpatient.Rows.Count > 0)
                {
                    this.AddNeedUpload(dtZZInpatient);
                }
            }

            //if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            //{
            //    MessageBox.Show("无需要上传的数据!", "警告");
            //    return;
            //}

        }

        /// <summary>
        /// 将待上传的患者显示在界面
        /// </summary>
        /// <param name="dtPatient"></param>
        private void AddNeedUpload(DataTable dtPatient)
        {
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                int rowIndex = this.fpNeedUpload_Sheet1.Rows.Count;
                foreach (DataRow dRow in dtPatient.Rows)
                {
                    this.fpNeedUpload_Sheet1.Rows.Add(rowIndex, 1);

                    //默认选上
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColChoose].Value = NConvert.ToBoolean(dRow[(int)ColPatient.ColChoose]);
                    //患者类别：1门诊；2住院
                    string patientType = dRow[(int)ColPatient.ColPatientType].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPatientType].Value = (patientType == "1" ? "门诊" : "住院");
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPatientType].Tag = patientType;

                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPatientNO].Value = dRow[(int)ColPatient.ColPatientNO].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPatientName].Value = dRow[(int)ColPatient.ColPatientName].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColIdNO].Value = dRow[(int)ColPatient.ColIdNO].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColSex].Value = dRow[(int)ColPatient.ColSex].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPactName].Value = dRow[(int)ColPatient.ColPactName].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColInvoiceNO].Value = dRow[(int)ColPatient.ColInvoiceNO].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPrintInvoice].Value = dRow[(int)ColPatient.ColPrintInvoice].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColTotCost].Value = dRow[(int)ColPatient.ColTotCost].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPubCost].Value = dRow[(int)ColPatient.ColPubCost].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColOwnCost].Value = dRow[(int)ColPatient.ColOwnCost].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColBalanceOper].Value = dRow[(int)ColPatient.ColBalanceOper].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColBalanceDate].Value = dRow[(int)ColPatient.ColBalanceDate].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPatientID].Value = dRow[(int)ColPatient.ColPatientID].ToString();
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, (int)ColPatient.ColPactID].Value = dRow[(int)ColPatient.ColPactID].ToString();

                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// 一键上传患者所有管理的信息
        /// </summary>
        private void AllUploadDetail()
        {
            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要上传信息!");
                return;
            }
            //提示
            string strTips = "一键上传将会上传的信息：\r\n 1、就诊单据信息；\r\n 2、病案信息上传；";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传!请稍后!");

            for (int k = 0; k < this.fpNeedUpload_Sheet1.Rows.Count; k++)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(k, this.fpNeedUpload_Sheet1.Rows.Count);

                bool isChoose = NConvert.ToBoolean(this.fpNeedUpload_Sheet1.Cells[k, (int)ColPatient.ColChoose].Value);
                if (isChoose)
                {
                    string patientType = this.fpNeedUpload_Sheet1.Cells[k, (int)ColPatient.ColPatientType].Tag.ToString();
                    string patientID = this.fpNeedUpload_Sheet1.Cells[k, (int)ColPatient.ColPatientID].Value.ToString();
                    string invoiceNO = this.fpNeedUpload_Sheet1.Cells[k, (int)ColPatient.ColInvoiceNO].Value.ToString();
                    string transType = "1"; //正交易

                    if (patientType == "1")
                    {
                        #region 门诊

                        int rev = 1;
                        string errInfo = string.Empty;

                        //收费单据号【即医保系统的结算顺序号，主单结算以后再上传】-作为上传的主线
                        string sfdjh = string.Empty;
                        //唯一流水号
                        string wylsh = string.Empty;
                        //病历号
                        string patientNo = string.Empty;
                        //身份证号
                        string idenNo = string.Empty;

                        //1、就诊单据信息上传(HJ001)
                        rev = this.UploadMainInfo(patientID, patientType, invoiceNO, transType, ref sfdjh, ref wylsh, ref patientNo, ref idenNo, ref errInfo);
                        if (rev <= 0)
                        {
                            continue;
                        }

                        ////2、病案信息上传(HB5001)
                        //rev = this.UploadPatientCase(patientID, patientType, invoiceNO, transType, sfdjh);
                        //if (rev == -1)
                        //{
                        //    continue;
                        //}

                        //3、保存上传日志
                        rev = this.inMgr.SaveLog(patientType, invoiceNO, patientNo, patientID, idenNo, sfdjh, wylsh, this.inMgr.Operator.ID);
                        if (rev == -1)
                        {
                            MessageBox.Show(this.inMgr.Err, "错误");
                            continue;
                        }
                        else
                        {
                            this.DeleteErrLog("1", patientID);
                        }

                        //MessageBox.Show("门诊发票信息【" + invoiceNO + "】病种分值付费接口上传成功!");

                        #endregion
                    }
                    else
                    {
                        #region 住院

                        int rev = 1;
                        string errInfo = string.Empty;

                        //收费单据号【即医保系统的结算顺序号，主单结算以后再上传】-作为上传的主线
                        string sfdjh = string.Empty;
                        //唯一流水号
                        string wylsh = string.Empty;
                        //住院号
                        string patientNo = string.Empty;
                        //身份证号
                        string idenNo = string.Empty;

                        //1、就诊单据信息上传(HJ001)
                        rev = this.UploadMainInfo(patientID, patientType, invoiceNO, transType, ref sfdjh, ref wylsh, ref patientNo, ref idenNo, ref errInfo);
                        bool isTrue = true;//就诊信息全部上传成功
                        if (rev <= 0)
                        {
                            isTrue = false;
                            if (MessageBox.Show("是否继续上传病案信息?", "疑问", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                continue;
                            }
                            //continue;
                        }

                        //2、病案信息上传(HB5001)
                        rev = this.UploadPatientCase(patientID, patientType, invoiceNO, transType, sfdjh);
                        if (rev == -1)
                        {
                            continue;
                        }

                        //3、保存上传日志
                        if (!isTrue)
                        {
                            continue;
                        }
                        rev = this.inMgr.SaveLog(patientType, invoiceNO, patientNo, patientID, idenNo, sfdjh, wylsh, this.inMgr.Operator.ID);
                        if (rev == -1)
                        {
                            MessageBox.Show(this.inMgr.Err, "错误");
                            continue;
                        }
                        else
                        {
                            this.DeleteErrLog("2", patientID);
                        }

                        //MessageBox.Show("住院发票信息【" + invoiceNO + "】病种分值付费接口上传成功!");

                        #endregion
                    }
                }
            }
            MessageBox.Show("上传完成!");
            //重新查询
            this.QueryNeedUploadDetail();

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

        }

        #region 佛山市按病种分值付费结算接口上传信息

        /// <summary>
        /// 1、就诊单据信息上传(HJ001)
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientType"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="transType"></param>
        /// <param name="ybMedNo"></param>
        /// <param name="pkNo"></param>
        /// <param name="patientNo"></param>
        /// <param name="idenNo"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int UploadMainInfo(string patientID, string patientType, string invoiceNO, string transType, ref string ybMedNo, ref string pkNo, ref string patientNo, ref string idenNo, ref string errInfo)
        {

            if (patientType == "1")
            {
                #region 门诊

                //查询门诊挂号记录
                ArrayList alReg = this.outRegMgr.QueryPatient(patientID);
                if (alReg == null || alReg.Count <= 0)
                {
                    //MessageBox.Show("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!").ToString());
                    return -1;
                }
                Neusoft.HISFC.Models.Registration.Register outPatientInfo = alReg[0] as Neusoft.HISFC.Models.Registration.Register;
                if (outPatientInfo == null || string.IsNullOrEmpty(outPatientInfo.ID))
                {
                    //MessageBox.Show("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!").ToString());
                    return -1;
                }
                //查询发票信息
                ArrayList balanceList = this.outMgr.QueryBalances(invoiceNO);
                if (balanceList == null || balanceList.Count <= 0)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,未找到对应的结算信息!").ToString());
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,已经退费!").ToString());
                    return -1;
                }

                //发票对应的社保返回信息
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

                //社保返回金额
                decimal siTotCost = 0m;
                decimal siPubCost = 0m;
                decimal siOwnCost = 0m;
                this.outMgr.GetSiCost(patientID, invoiceNO, "1", out siTotCost, out siPubCost, out siOwnCost);

                //发票信息
                Neusoft.HISFC.Models.Fee.Outpatient.Balance outBalance = balanceList[0] as Neusoft.HISFC.Models.Fee.Outpatient.Balance;
                if (outBalance == null || string.IsNullOrEmpty(outBalance.Invoice.ID))
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,未找到对应的结算信息!").ToString());
                    return -1;
                }

                //判断有社保金额
                if (siTotCost != 0 && siTotCost != outBalance.FT.TotCost)
                {
                    outBalance.FT.TotCost = siTotCost;
                    outBalance.FT.PubCost = siPubCost;
                    outBalance.FT.OwnCost = siOwnCost;
                }

                #region 四舍五入处理

                //发票对应的明细信息
                ArrayList alFee = this.outMgr.QueryFeeItemByInvoiceNO(outBalance.Patient.ID, outBalance.Invoice.ID);
                if (alFee == null || alFee.Count <= 0)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】未找到对应的处方信息!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,未找到对应的处方信息!").ToString());
                    return -1;
                }
                decimal sswrCost = 0;
                for (int k = 0; k < alFee.Count; k++)
                {
                    Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList fTemp = alFee[k] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (this.specialItemCode == fTemp.Item.ID)
                    {
                        sswrCost += fTemp.FT.OwnCost + fTemp.FT.PubCost + fTemp.FT.PayCost;
                    }
                }
                if (sswrCost != 0)
                {
                    outBalance.FT.OwnCost -= sswrCost;

                    outBalance.FT.TotCost = outBalance.FT.OwnCost + outBalance.FT.PubCost + outBalance.FT.PayCost;
                }

                #endregion

                //查询诊断信息
                ArrayList alDiag = this.diagManager.QueryCaseDiagnoseForClinicByState(outPatientInfo.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                string firstDiag = string.Empty;
                string secondDiag = string.Empty;

                #region 获取诊断

                if (alDiag != null && alDiag.Count > 0)
                {
                    int i = 0;
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.IsValid && diag.PerssonType == ServiceTypes.C)  //患者类别 0 门诊患者 1 住院患者
                        {
                            i++;
                            if (i == 1)
                            {
                                firstDiag = diag.DiagInfo.ICD10.ID;
                            }
                            else if (i == 2)
                            {
                                secondDiag = diag.DiagInfo.ICD10.ID;
                            }

                        }
                    }
                }

                #endregion


                #region 费用明细信息

                string strRowDetails = string.Empty;

                //发票对应的明细信息
                alFee = this.outMgr.QueryFeeItemByInvoiceNO(outBalance.Patient.ID, outBalance.Invoice.ID);
                if (alFee == null || alFee.Count <= 0)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】未找到对应的处方信息!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,未找到对应的处方信息1!").ToString());
                    return -1;
                }
                #region 剔除四舍五入项目

                ArrayList alTemp = new ArrayList();
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList fTemp in alFee)
                {
                    if (this.specialItemCode != fTemp.Item.ID)
                    {
                        alTemp.Add(fTemp);
                    }
                }
                alFee = alTemp;   //重新赋值

                if (alFee.Count <= 0)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】未找到对应的处方信息!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,未找到对应的处方信息2!").ToString());
                    return -1;
                }

                #endregion

                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = alFee[0] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;

                #region 挂号诊查费处理-【张槎、朝阳、澜石不适用】

                List<Neusoft.HISFC.Models.Account.AccountCardFee> lstAccFee = new List<Neusoft.HISFC.Models.Account.AccountCardFee>();
                if (this.outMgr.QueryAccCardFeeByClinic(outBalance.Patient.PID.CardNO, outBalance.Patient.ID, outBalance.Invoice.ID, out lstAccFee) == -1)
                {
                    //MessageBox.Show("查找患者的诊查费用失败!" + this.outMgr.Err);
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,查找患者的诊查费用失败!" + this.outMgr.Err).ToString());
                    return -1;
                }

                foreach (Neusoft.HISFC.Models.Account.AccountCardFee cardFee in lstAccFee)
                {
                    if (!string.IsNullOrEmpty(cardFee.ItemCode))
                    {
                        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList cfItem = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();
                        cfItem.Item.ItemType = EnumItemType.UnDrug;
                        cfItem.Item.ID = cardFee.ItemCode;
                        cfItem.Item.Name = cardFee.ItemName;
                        cfItem.Item.Qty = cardFee.ItemQty;
                        cfItem.Item.PriceUnit = cardFee.ItemUnit;
                        cfItem.Item.Price = cardFee.ItemPrice;
                        cfItem.Item.PackQty = 1;
                        cfItem.Days = 1;
                        cfItem.FT.TotCost = cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;
                        cfItem.FT.OwnCost = cardFee.Own_cost;
                        cfItem.FT.PubCost = cardFee.Pub_cost;
                        cfItem.FT.PayCost = cardFee.Pay_cost;

                        cfItem.RecipeNO = "DF" + cardFee.InvoiceNo;
                        cfItem.SequenceNO = 1;
                        cfItem.Order.ID = "MO" + cardFee.InvoiceNo;
                        cfItem.TransType = TransTypes.Positive;
                        cfItem.ChargeOper.OperTime = cardFee.FeeOper.OperTime;
                        cfItem.Order.DoseOnce = 1;

                        cfItem.FeeOper.OperTime = feeItem.FeeOper.OperTime;
                        cfItem.RecipeOper = feeItem.RecipeOper;

                        alFee.Add(cfItem);
                    }
                }

                #endregion


                if (feeItem != null)
                {
                    string clinicCode = feeItem.Patient.ID;   //门诊流水号作为处方号
                    decimal totCost = 0m;   //发票总金额
                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList fTemp in alFee)
                    {
                        totCost += fTemp.FT.OwnCost + fTemp.FT.PubCost + fTemp.FT.PayCost;  //总金额
                    }

                    decimal totCostTem = 0;

                    for (int k = 0; k < alFee.Count; k++)
                    {
                        feeItem = alFee[k] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                        string errMsg = string.Empty;

                        //项目编码   社保中心码 
                        string itemCode = string.Empty;
                        string itemName = feeItem.Item.Name.Replace('<', ' ').Replace('/', ' ').Replace('>', ' ');
                        if (this.itemCodeCompareType == "0")
                        {
                            #region 国标码处理

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                if (this.DealFeeItemList(feeItem, ref errMsg) == -1)
                                {
                                    MessageBox.Show(errMsg);
                                    return -1;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");

                                this.InsertErrLog("1", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护1！").ToString());
                                return -1;
                            }

                            //获取社保中心编码和名称
                            if (this.dictSiItem.ContainsKey(feeItem.Item.GBCode))
                            {
                                Const c = dictSiItem[feeItem.Item.GBCode];
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                    this.InsertErrLog("1", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护2！").ToString());
                                    return -1;
                                }
                                itemCode = c.UserCode;
                                itemName = c.Memo;
                            }
                            else
                            {
                                Const c = this.inMgr.QuerySiCompare(feeItem.Item.GBCode);
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                    this.InsertErrLog("1", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护3！").ToString());
                                    return -1;
                                }
                                
                                this.dictSiItem.Add(feeItem.Item.GBCode,c);
                                itemCode = c.UserCode;
                                itemName = c.Memo;

                            }

                            

                            #endregion
                        }
                        else
                        {
                            #region 自定义码处理

                            if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                            {
                                if (this.DealFeeItemList(feeItem, ref errMsg) == -1)
                                {
                                    MessageBox.Show(errMsg);
                                    return -1;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护！");
                                this.InsertErrLog("1", patientID, invoiceNO, ("项目【" + feeItem.Item.Name  + "(" + feeItem .Item.ID + ")" +"】没有维护医保对照码(自定义码为空)，请先进行维护1！").ToString());
                                return -1;
                            }

                            //获取社保中心编码和名称
                            if (this.dictSiItem.ContainsKey(feeItem.Item.UserCode))
                            {
                                Const c = dictSiItem[feeItem.Item.UserCode];
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护！");

                                    this.InsertErrLog("1", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护2！").ToString());
                                    return -1;
                                }
                                itemCode = c.UserCode;
                                itemName = c.Memo;
                            }
                            else
                            {
                                Const c = this.inMgr.QuerySiCompare(feeItem.Item.UserCode);
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                    this.InsertErrLog("1", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护4！").ToString());
                                    return -1;
                                }

                                this.dictSiItem.Add(feeItem.Item.UserCode, c);
                                itemCode = c.UserCode;
                                itemName = c.Memo;
                            }

                            #endregion
                        }

                        //项目总量，单价，总额，收费日期
                        string itemQty = feeItem.Item.Qty.ToString();
                        string itemPrice = System.Math.Round(feeItem.Item.PackQty == 0 ? feeItem.Item.Price : feeItem.Item.Price / feeItem.Item.PackQty, 4).ToString();
                        string itemCost = (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost).ToString();

                        totCostTem += NConvert.ToDecimal(itemCost);

                        DateTime feeDate = feeItem.FeeOper.OperTime;

                        #region 组装Row

                        string mxsfdjh = siReturnID;  // patientType + "-" + invoiceNO + "-" + transType;
                        string mxwylsh = feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString() + "-" + feeItem.Order.ID + "-" + feeItem.InvoiceCombNO;//RECIPE_NO, SEQUENCE_NO, TRANS_TYPE, MO_ORDER, INVOICE_SEQ
                        string fwrq = feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd");
                        string xmbm = itemCode;
                        string xmmc = itemName;
                        string xmlb = (feeItem.Item.ItemType == EnumItemType.Drug ? "1" : "0"); //1为“药品”，0为“其它”
                        string dj = itemPrice;
                        string sl = itemQty;
                        string zj = itemCost;
                        string ysbm = feeItem.RecipeOper.ID;
                        string ysmc = this.GetEmpoyeeName(feeItem.RecipeOper.ID, ref errMsg);
                        string ksbm = feeItem.RecipeOper.Dept.ID;
                        string ksmc = this.GetDeptName(feeItem.RecipeOper.Dept.ID, ref errMsg);

                        string yf = (feeItem.Item.ItemType == EnumItemType.Drug ? feeItem.Item.PriceUnit : "0");  //对应数量的唯一计价单位(比如片、支、瓶、袋等)，非药品填0

                        //给药途径【字典】  给药途径为空则取 900 其他用药途径
                        string gytj = (string.IsNullOrEmpty(feeItem.Order.Usage.ID) ? "900" : feeItem.Order.Usage.ID);
                        if (!string.IsNullOrEmpty(feeItem.Order.Usage.ID))
                        {
                            Neusoft.HISFC.Models.Base.Const objGytj = this.gytjHelper.GetObjectFromID(feeItem.Order.Usage.ID) as Neusoft.HISFC.Models.Base.Const;
                            if (objGytj != null && !string.IsNullOrEmpty(objGytj.ID))
                            {
                                gytj = objGytj.UserCode;
                            }
                        }


                        string yl = feeItem.Order.DoseOnce.ToString("F2");

                        //频次【字典】
                        string pc = (string.IsNullOrEmpty(feeItem.Order.Frequency.ID) ? "-1" : feeItem.Order.Frequency.ID);   //11	每天一次（qd）
                        if (!string.IsNullOrEmpty(feeItem.Order.Frequency.ID))
                        {
                            Neusoft.HISFC.Models.Base.Const objSypc = this.sypcHelper.GetObjectFromID(feeItem.Order.Frequency.ID) as Neusoft.HISFC.Models.Base.Const;
                            if (objSypc != null && !string.IsNullOrEmpty(objSypc.ID))
                            {
                                pc = objSypc.UserCode;
                            }
                        }

                        string yyts = feeItem.Days.ToString();
                        string ybnje = "";
                        string jylx = "1";   //标识是否为冲减单据，1是0否
                        string ypgg = (string.IsNullOrEmpty(feeItem.Item.Specs) ? "次" : feeItem.Item.Specs);
                        string cydybs = "0";
                        string zyh = patientID;
                        string yzzdid = feeItem.Order.ID;
                        string yzmxid = feeItem.Order.ID;  //feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString() + "-" + feeItem.Order.ID + "-" + feeItem.InvoiceCombNO;//RECIPE_NO, SEQUENCE_NO, TRANS_TYPE, MO_ORDER, INVOICE_SEQ

                        string rowJSON = string.Format(Function.FeeDetailJSON, mxsfdjh, mxwylsh, fwrq, xmbm, xmmc, xmlb, dj, sl, zj, ysbm,
                                                                            ysmc, ksbm, ksmc, yf, gytj, yl, pc, yyts, ybnje, jylx,
                                                                            ypgg, cydybs, zyh, yzzdid, yzmxid);

                        strRowDetails += "\r\n{" + rowJSON + "}";

                        if (k < alFee.Count - 1)
                        {
                            strRowDetails += ",";
                        }

                        #endregion

                    }
                }

                #endregion

                #region 组装Row

                string wylsh = Manager.setObj.HospitalID + patientType + "-" + invoiceNO + "-" + transType + siReturnID;   //主键
                string sfdjh = siReturnID; //outBalance.PrintedInvoiceNO;
                string djjsrq = outBalance.BalanceOper.OperTime.ToString("yyyy-MM-dd");
                string ddjgbm = Manager.setObj.HospitalID;
                string ddjgmc = Manager.setObj.HospitalName;
                string fyfsjgbm = Manager.setObj.HospitalID;
                string fyfsjgmc = Manager.setObj.HospitalName;
                string zwjgmc = "";
                string gmsfhm = outPatientInfo.IDCard;
                string cbrmc = outPatientInfo.Name;
                string cbrxb = Function.ConvertSexCode(outPatientInfo.Sex.ID.ToString());
                string cbrcsrq = outPatientInfo.Birthday.ToString("yyyy-MM-dd");
                string rylbbm = "";
                string jzfsbm = "12";   //参考就医方式字典表  12	特定门诊
                string sfydjy = "0";
                string ryzdbm = firstDiag;
                string cyzdbm = (string.IsNullOrEmpty(secondDiag) ? firstDiag : secondDiag);    //次要诊断如果为空  ??gmz??
                string fzd = "";  //共16个负诊断
                string sftbmbdjbz = "";
                string mbtbdm = "";
                string ryrq = outPatientInfo.DoctorInfo.SeeDate.ToString("yyyy-MM-dd");  //入院日期
                string cyrq = outPatientInfo.DoctorInfo.SeeDate.ToString("yyyy-MM-dd");  //出院日期
                string jzrq = outPatientInfo.DoctorInfo.SeeDate.ToString("yyyy-MM-dd");
                string sfhy = "0";   //标识参保人是否怀孕，1是0否
                string sfbrq = "0";  //标识是否处于哺乳期，1是0否
                string sg = "";
                string tz = "";
                string xy = "";
                string kfxt = "";
                string chxt = "";
                string tw = "";
                string xl = "";
                string sfzry = "0";
                string ybzxdm = "440604";   //医保中心代码 【禅城：440604】
                string ydrylb = "";
                string ydcbxzqydm = "";
                string dylx = "";
                string cblx = "";
                string zje = outBalance.FT.TotCost.ToString("F2");
                string ybnzje = outBalance.FT.PubCost.ToString("F2");
                string ybnzfje = "0.00";
                string zfbl = (outBalance.FT.OwnCost + outBalance.FT.PayCost).ToString("F2");
                string zhfbl = "";
                string xyf = "";    //西药费-提取自病案首页
                string zchyf = "";
                string zcyf = "";
                string ybylfwf = "";
                string ybzlczf = "";
                string hlf = "";
                string blzdf = "";
                string syszdf = "";
                string yxxzdf = "";
                string lczdxmf = "";
                string sszlf = "";
                string mjf = "";
                string fsszlxmf = "";
                string lcwlzlf = "";
                string kff = "";
                string xf = "";
                string bdblzpf = "";
                string qdblzpf = "";
                string nxyzlzpf = "";
                string xbyzlzpf = "";
                string jcyclf = "";
                string zlyclf = "";
                string ssyclf = "";
                string mzzyh = outPatientInfo.PID.CardNO;
                string cjrbs = "";
                string yylb = "10";   //医院类别 10市内地点医院
                string cjdjh = "";
                string jjlx = "";
                string ssdm = "";
                string ssqkfl = "";
                string cyzry = "";

                string json = string.Format(Function.MainInfoJSON, wylsh, sfdjh, djjsrq, ddjgbm, ddjgmc, fyfsjgbm, fyfsjgmc, zwjgmc, gmsfhm, cbrmc,
                                           cbrxb, cbrcsrq, rylbbm, jzfsbm, sfydjy, ryzdbm, cyzdbm, fzd, fzd, fzd,
                                           fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd,
                                           fzd, fzd, fzd, sftbmbdjbz, mbtbdm, ryrq, cyrq, jzrq, sfhy, sfbrq,
                                           sg, tz, xy, kfxt, chxt, tw, xl, sfzry, ybzxdm, ydrylb,
                                           ydcbxzqydm, dylx, cblx, zje, ybnzje, ybnzfje, zfbl, zhfbl, xyf, zchyf,
                                           zcyf, ybylfwf, ybzlczf, hlf, blzdf, syszdf, yxxzdf, lczdxmf, sszlf, mjf,
                                           fsszlxmf, lcwlzlf, kff, xf, bdblzpf, qdblzpf, nxyzlzpf, xbyzlzpf, jcyclf, zlyclf,
                                           ssyclf, mzzyh, cjrbs, yylb, cjdjh, jjlx, ssdm, ssqkfl, cyzry, strRowDetails
                                           );

                #endregion

                //回传
                ybMedNo = siReturnID;
                pkNo = wylsh;
                patientNo = outPatientInfo.PID.CardNO;
                idenNo = outPatientInfo.IDCard;

                errInfo = string.Empty;
                int result = this.baseJob.UploadInfo("HJ001", json, ref errInfo);
                if (result <= 0)
                {
                    if (errInfo.Contains("已经上传") || errInfo.Contains("重复上传"))
                    {
                        return 1;
                    }
                    else
                    {
                        if (result == 0)
                        {
                            MessageBox.Show("【门诊】" + patientID + "诊断为自定义诊断不上传！");
                            return -1;
                        }
                        MessageBox.Show("【门诊】" + patientID + " " + invoiceNO + "上传出错@" + errInfo);

                        this.InsertErrLog("1", patientID, invoiceNO, ("上传出错@" + errInfo).ToString());
                        return -1;
                    }
                }
                return result;

                #endregion
            }
            else
            {
                #region 住院

                //查询住院患者的主表
                Neusoft.HISFC.Models.RADT.PatientInfo inPatientInfo = this.radtIntegrate.GetPatientInfomation(patientID);
                if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
                {
                    MessageBox.Show("未找到【" + patientID + "】的住院信息!");
                    this.InsertErrLog("2", patientID, invoiceNO, ("未找到【" + patientID + "】的住院信息!").ToString());
                    return -1;
                }
                
                //查询住院患者的发票信息
                ArrayList balanceList = this.inMgr.QueryBalances(invoiceNO);
                if (balanceList == null || balanceList.Count <= 0)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    this.InsertErrLog("2", patientID, invoiceNO, ("发票号【" + invoiceNO + "】未找到对应的结算信息!").ToString());
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");
                    this.InsertErrLog("2", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,已经退费!").ToString());
                    return -1;
                }
                Neusoft.HISFC.Models.Fee.Inpatient.Balance balance = balanceList[0] as Neusoft.HISFC.Models.Fee.Inpatient.Balance;
                if (balance == null || string.IsNullOrEmpty(balance.Invoice.ID))
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");

                    this.InsertErrLog("2", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,未找到对应的结算信息1!").ToString());
                    return -1;
                }

                //发票对应的社保返回信息
                string siMemo = this.outMgr.GetSiInfo(patientID, invoiceNO, "2");
                string siReturnID = string.Empty;
                if (!string.IsNullOrEmpty(siMemo))
                {
                    string[] arr = siMemo.Split('|');
                    if (arr.Length > 0)
                    {
                        siReturnID = arr[0];
                    }
                }

                //查询诊断信息
                ArrayList alDiag = this.diagManager.QueryCaseDiagnoseForClinicByState(inPatientInfo.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                string firstDiag = string.Empty;
                string secondDiag = string.Empty;

                #region 获取诊断

                if (alDiag != null && alDiag.Count > 0)
                {
                    int i = 0;
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.IsValid && diag.PerssonType == ServiceTypes.I)  //患者类别 0 门诊患者 1 住院患者
                        {
                            i++;
                            if (i == 1)
                            {
                                firstDiag = diag.DiagInfo.ICD10.ID;
                            }
                            else if (i == 2)
                            {
                                secondDiag = diag.DiagInfo.ICD10.ID;
                            }

                        }
                    }
                }

                #endregion

                #region 费用明细

                string strRowDetails = string.Empty;

                //住院总费用
                ArrayList alFee = new ArrayList();

                //药品
                ArrayList alMedFee = this.inMgr.QueryMedItemListsByInvoiceNO(balance.Patient.ID, balance.Invoice.ID);
                if (alMedFee == null)
                {
                    alMedFee = new ArrayList();
                }
                alFee.AddRange(alMedFee);
                //非药品
                ArrayList alUndrugFee = this.inMgr.QueryFeeItemListsByInvoiceNO(balance.Patient.ID, balance.Invoice.ID);
                if (alUndrugFee == null)
                {
                    alUndrugFee = new ArrayList();
                }
                alFee.AddRange(alUndrugFee);

                if (alFee == null || alFee.Count <= 0)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】未找到对应的处方信息!");
                    this.InsertErrLog("2", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,未找到对应的处方信息1!").ToString());
                    return -1;
                }
                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = alFee[0] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (feeItem != null)
                {
                    string inpatientNO = feeItem.ID;   //住院流水号，作为处方号
                    decimal totCost = 0m;              //发票总金额

                    //存放项目汇总信息
                    Hashtable hsUpLoadFeeDetails = new Hashtable();
                    ArrayList feeGatherlsClone = new ArrayList();
                    foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList fTemp in alFee)
                    {
                        totCost += fTemp.FT.OwnCost + fTemp.FT.PubCost + fTemp.FT.PayCost;

                        //相同的项目进行累加汇总后再进行上传 【划价时间+项目编码】
                        if (hsUpLoadFeeDetails.ContainsKey(fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID))
                        {
                            Neusoft.HISFC.Models.Fee.FeeItemBase feeItemList = hsUpLoadFeeDetails[fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID] as Neusoft.HISFC.Models.Fee.FeeItemBase;

                            feeItemList.Item.Qty += fTemp.Item.Qty;//数量累加

                            feeItemList.FT.TotCost += fTemp.FT.TotCost;//金额累加
                            feeItemList.FT.OwnCost += fTemp.FT.OwnCost;   //自费金额累计
                            feeItemList.FT.PubCost += fTemp.FT.PubCost;   //医保金额累加
                            feeItemList.FT.PayCost += fTemp.FT.PayCost;
                        }
                        else
                        {
                            //Neusoft.HISFC.Models.Fee.FeeItemBase fCloce = fTemp.Clone();
                            hsUpLoadFeeDetails.Add(fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID, fTemp);
                            feeGatherlsClone.Add(fTemp);
                        }

                    }

                    for (int k = 0; k < feeGatherlsClone.Count; k++)
                    {
                        feeItem = feeGatherlsClone[k] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;
                        string errMsg = string.Empty;

                        //项目编码   传社保中心码 
                        string itemCode = string.Empty;
                        string itemName = feeItem.Item.Name.Replace('<', ' ').Replace('/', ' ').Replace('>', ' ');
                        if (this.itemCodeCompareType == "0")
                        {
                            #region 国标码处理

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                if (this.DealFeeItemList(feeItem, ref errMsg) == -1)
                                {
                                    MessageBox.Show(errMsg);
                                    return -1;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");

                                this.InsertErrLog("2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem .Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！").ToString());
                                return -1;
                            }

                            //获取社保中心编码和名称
                            if (this.dictSiItem.ContainsKey(feeItem.Item.GBCode))
                            {
                                Const c = dictSiItem[feeItem.Item.GBCode];
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                    this.InsertErrLog("2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem .Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护1！").ToString());
                                
                                    return -1;
                                }
                                itemCode = c.UserCode;
                                itemName = c.Memo;
                            }
                            else
                            {
                                Const c = this.inMgr.QuerySiCompare(feeItem.Item.GBCode);
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                    this.InsertErrLog("2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护2！").ToString());
                                    return -1;
                                }

                                this.dictSiItem.Add(feeItem.Item.GBCode, c);
                                itemCode = c.UserCode;
                                itemName = c.Memo;
                            }

                            #endregion
                        }
                        else
                        {
                            #region 自定义码处理
                            if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                            {
                                if (this.DealFeeItemList(feeItem, ref errMsg) == -1)
                                {
                                    MessageBox.Show(errMsg);
                                    return -1;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护！");
                                this.InsertErrLog("2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护1！").ToString());
                                return -1;
                            }

                            //获取社保中心编码和名称
                            if (this.dictSiItem.ContainsKey(feeItem.Item.UserCode))
                            {
                                Const c = dictSiItem[feeItem.Item.UserCode];
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护！");
                                    this.InsertErrLog("2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(自定义码为空)，请先进行维护2！").ToString());
                                    return -1;
                                }
                                itemCode = c.UserCode;
                                itemName = c.Memo;
                            }
                            else
                            {
                                Const c = this.inMgr.QuerySiCompare(feeItem.Item.UserCode);
                                if (c == null || string.IsNullOrEmpty(c.UserCode))
                                {
                                    MessageBox.Show("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                    this.InsertErrLog("2", patientID, invoiceNO, ("项目【" + feeItem.Item.Name + "(" + feeItem.Item.ID + ")" + "】没有维护医保对照码(国标码为空)，请先进行维护4！").ToString());
                                    return -1;
                                }

                                this.dictSiItem.Add(feeItem.Item.UserCode, c);
                                itemCode = c.UserCode;
                                itemName = c.Memo;
                            }

                            #endregion
                        }

                        //项目总量，单价，总额，收费日期
                        string itemQty = feeItem.Item.Qty.ToString();
                        string itemPrice = System.Math.Round(feeItem.Item.PackQty == 0 ? feeItem.Item.Price : feeItem.Item.Price / feeItem.Item.PackQty, 4).ToString();
                        string itemCost = (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost).ToString();
                        DateTime feeDate = balance.BalanceOper.OperTime;      //住院应为结算日期

                        //处方医生
                        string recipeDoctId = feeItem.RecipeOper.ID;
                        Employee emp = this.GetEmpoyee(recipeDoctId, ref errMsg);
                        if (emp == null)
                        {
                            MessageBox.Show("医生【" + recipeDoctId + "】找不到基本信息！");
                            this.InsertErrLog("2", patientID, invoiceNO, ("医生【" + recipeDoctId + "】找不到基本信息！").ToString());
                            return -1;
                        }
                        if (emp.EmployeeType.ID.ToString() != "D")
                        {
                            recipeDoctId = inPatientInfo.PVisit.AdmittingDoctor.ID;
                        }
                        string recipeDoctName = this.GetEmpoyeeName(recipeDoctId, ref errMsg);

                        //科室名称 字典bqdm
                        string deptName = this.GetDeptName(feeItem.RecipeOper.Dept.ID, ref errMsg);

                        #region 组装Row

                        string mxsfdjh = siReturnID;  //patientType + "-" + invoiceNO + "-" + transType;
                        string mxwylsh = feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString();//RECIPE_NO, TRANS_TYPE, SEQUENCE_NO
                        string fwrq = feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd");
                        string xmbm = itemCode;
                        string xmmc = itemName;
                        string xmlb = (feeItem.Item.ItemType == EnumItemType.Drug ? "1" : "0"); //1为“药品”，0为“其他”
                        string dj = itemPrice;
                        string sl = itemQty;
                        string zj = itemCost;
                        string ysbm = recipeDoctId;
                        string ysmc = recipeDoctName;
                        string ksbm = feeItem.RecipeOper.Dept.ID;
                        string ksmc = this.GetDeptName(feeItem.RecipeOper.Dept.ID, ref errMsg);

                        string yf = (feeItem.Item.ItemType == EnumItemType.Drug ? feeItem.Item.PriceUnit : "0"); //对应数量的唯一计价单位(比如片、支、瓶、袋等)，非药品填0

                        //给药途径【字典】给药途径为空则取 900 其他用药途径
                        string gytj = (string.IsNullOrEmpty(feeItem.Order.Usage.ID) ? "900" : feeItem.Order.Usage.ID);
                        if (!string.IsNullOrEmpty(feeItem.Order.Usage.ID))
                        {
                            Neusoft.HISFC.Models.Base.Const objGytj = this.gytjHelper.GetObjectFromID(feeItem.Order.Usage.ID) as Neusoft.HISFC.Models.Base.Const;
                            if (objGytj != null && !string.IsNullOrEmpty(objGytj.ID))
                            {
                                gytj = objGytj.UserCode;
                            }
                        }

                        string yl = feeItem.Order.DoseOnce.ToString("F2");

                        //频次【字典】
                        string pc = (string.IsNullOrEmpty(feeItem.Order.Frequency.ID) ? "-1" : feeItem.Order.Frequency.ID); //11	每天一次（qd）
                        if (!string.IsNullOrEmpty(feeItem.Order.Frequency.ID))
                        {
                            Neusoft.HISFC.Models.Base.Const objSypc = this.sypcHelper.GetObjectFromID(feeItem.Order.Frequency.ID) as Neusoft.HISFC.Models.Base.Const;
                            if (objSypc != null && !string.IsNullOrEmpty(objSypc.ID))
                            {
                                pc = objSypc.UserCode;
                            }
                        }

                        string yyts = feeItem.Days.ToString();
                        string ybnje = "";
                        string jylx = "1";   //标识是否为冲减单据，1是0否
                        string ypgg = (string.IsNullOrEmpty(feeItem.Item.Specs) ? "次" : feeItem.Item.Specs);
                        string cydybs = "0";
                        string zyh = patientID;
                        string yzzdid = (!string.IsNullOrEmpty(feeItem.Order.ID) ? feeItem.Order.ID : (feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString()));
                        string yzmxid = (!string.IsNullOrEmpty(feeItem.Order.ID) ? feeItem.Order.ID : (feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString())); //feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString();//RECIPE_NO, TRANS_TYPE, SEQUENCE_NO

                        string rowJSON = string.Format(Function.FeeDetailJSON, mxsfdjh, mxwylsh, fwrq, xmbm, xmmc, xmlb, dj, sl, zj, ysbm,
                                                                            ysmc, ksbm, ksmc, yf, gytj, yl, pc, yyts, ybnje, jylx,
                                                                            ypgg, cydybs, zyh, yzzdid, yzmxid);

                        strRowDetails += "\r\n{" + rowJSON + "}";

                        if (k < feeGatherlsClone.Count - 1)
                        {
                            strRowDetails += ",";
                        }

                        #endregion

                    }
                }

                #endregion

                #region 封装JSON

                string wylsh = Manager.setObj.HospitalID + patientType + "-" + invoiceNO + "-" + transType + siReturnID;  //主键
                string sfdjh = siReturnID;  //balance.PrintedInvoiceNO;
                string djjsrq = balance.BalanceOper.OperTime.ToString("yyyy-MM-dd");
                string ddjgbm = Manager.setObj.HospitalID;
                string ddjgmc = Manager.setObj.HospitalName;
                string fyfsjgbm = Manager.setObj.HospitalID;
                string fyfsjgmc = Manager.setObj.HospitalName;
                string zwjgmc = "";
                string gmsfhm = inPatientInfo.IDCard;
                string cbrmc = inPatientInfo.Name;
                string cbrxb = Function.ConvertSexCode(inPatientInfo.Sex.ID.ToString());
                string cbrcsrq = inPatientInfo.Birthday.ToString("yyyy-MM-dd");
                string rylbbm = "";
                string jzfsbm = "21";   //参考就医方式字典表  21	普通住院
                string sfydjy = "0";
                string ryzdbm = firstDiag;    //入院诊断   ??gmz??
                string cyzdbm = (string.IsNullOrEmpty(secondDiag) ? firstDiag : secondDiag);    //出院诊断  ??gmz??
                string fzd = "";  //共16个负诊断
                string sftbmbdjbz = "";
                string mbtbdm = "";
                string ryrq = inPatientInfo.PVisit.InTime.ToString("yyyy-MM-dd");  //入院日期
                string cyrq = inPatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd");  //出院日期
                string jzrq = inPatientInfo.PVisit.InTime.ToString("yyyy-MM-dd");
                string sfhy = "0";   //标识参保人是否怀孕，1是0否
                string sfbrq = "0";  //标识是否处于哺乳期，1是0否
                string sg = "";
                string tz = "";
                string xy = "";
                string kfxt = "";
                string chxt = "";
                string tw = "";
                string xl = "";
                string sfzry = "0";
                string ybzxdm = "440604";   //医保中心代码 【禅城：440604】
                string ydrylb = "";
                string ydcbxzqydm = "";
                string dylx = "";
                string cblx = "";
                string zje = balance.FT.TotCost.ToString("F2");
                string ybnzje = balance.FT.PubCost.ToString("F2");
                string ybnzfje = "0.00";
                string zfbl = (balance.FT.OwnCost + balance.FT.PayCost).ToString("F2");
                string zhfbl = "";
                string xyf = "";    //西药费-提取自病案首页
                string zchyf = "";
                string zcyf = "";
                string ybylfwf = "";
                string ybzlczf = "";
                string hlf = "";
                string blzdf = "";
                string syszdf = "";
                string yxxzdf = "";
                string lczdxmf = "";
                string sszlf = "";
                string mjf = "";
                string fsszlxmf = "";
                string lcwlzlf = "";
                string kff = "";
                string xf = "";
                string bdblzpf = "";
                string qdblzpf = "";
                string nxyzlzpf = "";
                string xbyzlzpf = "";
                string jcyclf = "";
                string zlyclf = "";
                string ssyclf = "";
                string mzzyh = inPatientInfo.PID.PatientNO;   //门诊/住院号
                string cjrbs = "";
                string yylb = "10";   //医院类别 10市内地点医院
                string cjdjh = "";
                string jjlx = "";
                string ssdm = "";
                string ssqkfl = "";
                string cyzry = "";

                string json = string.Format(Function.MainInfoJSON, wylsh, sfdjh, djjsrq, ddjgbm, ddjgmc, fyfsjgbm, fyfsjgmc, zwjgmc, gmsfhm, cbrmc,
                                           cbrxb, cbrcsrq, rylbbm, jzfsbm, sfydjy, ryzdbm, cyzdbm, fzd, fzd, fzd,
                                           fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd,
                                           fzd, fzd, fzd, sftbmbdjbz, mbtbdm, ryrq, cyrq, jzrq, sfhy, sfbrq,
                                           sg, tz, xy, kfxt, chxt, tw, xl, sfzry, ybzxdm, ydrylb,
                                           ydcbxzqydm, dylx, cblx, zje, ybnzje, ybnzfje, zfbl, zhfbl, xyf, zchyf,
                                           zcyf, ybylfwf, ybzlczf, hlf, blzdf, syszdf, yxxzdf, lczdxmf, sszlf, mjf,
                                           fsszlxmf, lcwlzlf, kff, xf, bdblzpf, qdblzpf, nxyzlzpf, xbyzlzpf, jcyclf, zlyclf,
                                           ssyclf, mzzyh, cjrbs, yylb, cjdjh, jjlx, ssdm, ssqkfl, cyzry, strRowDetails
                                           );

                #endregion

                //回传
                ybMedNo = siReturnID;
                pkNo = wylsh;
                if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")//佛山市第四人民医院去掉住院号的0
                {
                    patientNo = inPatientInfo.PID.PatientNO.TrimStart('0');
                }
                else
                {
                    patientNo = inPatientInfo.PID.PatientNO;
                }
                idenNo = inPatientInfo.IDCard;

                errInfo = string.Empty;
                int result = this.baseJob.UploadInfo("HJ001", json, ref errInfo);
                if (result <= 0)
                {
                    if (!errInfo.Contains("已经上传"))
                    {
                        if (result == 0)
                        {
                            MessageBox.Show("【住院】" + patientID + "诊断为自定义诊断不上传！");
                            return -1;
                        }
                        //MessageBox.Show("【住院】" + patientID + " " + invoiceNO + "上传【就诊单据信息】出错@" + errInfo);
                        this.InsertErrLog("2", patientID, invoiceNO, ("上传【就诊单据信息】出错@" + errInfo).ToString());
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                return result;

                #endregion
            }
        }

        /// <summary>
        /// 2、病案信息上传(HB5001)
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientType"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="transType"></param>
        /// <param name="ybMedNo"></param>
        /// <returns></returns>
        private int UploadPatientCase(string patientID, string patientType, string invoiceNO, string transType, string ybMedNo)
        {
            if (patientType == "1")
            {
                #region 门诊

                //查询门诊挂号记录
                ArrayList alReg = this.outRegMgr.QueryPatient(patientID);
                if (alReg == null || alReg.Count <= 0)
                {
                    //MessageBox.Show("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】找不到对应的挂号记录!").ToString());
                    return -1;
                }
                Neusoft.HISFC.Models.Registration.Register outPatientInfo = alReg[0] as Neusoft.HISFC.Models.Registration.Register;
                if (outPatientInfo == null || string.IsNullOrEmpty(outPatientInfo.ID))
                {
                    //MessageBox.Show("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!");
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】找不到对应的挂号记录1!").ToString());
                    return -1;
                }
                //查询发票信息
                ArrayList balanceList = this.outMgr.QueryBalances(invoiceNO);
                if (balanceList == null || balanceList.Count <= 0)
                {
                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】未找到对应的结算信息1!").ToString());
                    //MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");

                    this.InsertErrLog("1", patientID, invoiceNO, ("发票号【" + invoiceNO + "】,已经退费1!").ToString());
                    return -1;
                }

                //发票对应的社保返回信息
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
                ybMedNo = siReturnID;

                #region 组装Row

                string sfdjh = siReturnID;
                string ddjgbm = Manager.setObj.HospitalID;
                string gmsfhm = outPatientInfo.IDCard;    //身份证号
                string mzzyh = outPatientInfo.PID.CardNO; ;   //门诊/住院号
                string cjrbs = "";    //标识是否为残疾人，1是0否
                string zfbl = "";    //医保基金支付金额占总费用比例
                string yylb = "";    //医院类别
                string jylx = "0";   //是否为冲减单据，1是，0否
                string cjdjh = "";   //冲减单据号
                string rybzh = "";   //人员保障卡号
                string jzcs = outPatientInfo.InTimes.ToString();   //就诊次数
                string ysbm = outPatientInfo.SeeDoct.ID;   //医生ID
                string ysjb = "";    //住院医师/主治医师/副主任医师/主任医师
                string sfggnbq = "";    //标识参保人是否肝功能不全，1是0否
                string sfsgnbq = "";    //标识参保人是否肾功能不全，1是0否
                string gms = "";       //过敏史
                string gmy = "";   //过敏源
                string sfbyxjc = "";   //是否病原学检查
                string sfwswjy = "";    //是否微生物检验
                string jcsj = "";      //病原学或微生物检查时间
                string cfbm = "";     //区分单张处方
                string ssysbm = "";    //手术医师编码
                string ssysxm = "";    //手术医师姓名
                string ssysizbm = "";   //手术医师I助编码
                string ssysizxm = "";    //手术医师I助姓名
                string ssysiizbm = "";   //手术医师II助编码
                string ssysiizxm = "";   //手术医师II助姓名
                string mzsbm = "";     //麻醉师编码
                string mzsxm = "";     //麻醉师姓名
                string sszxsj = "";    //手术执行时间
                string mzfs = "";      //麻醉方式
                string ssdm = "";   //手术代码
                string ssqkfl = "";    //手术切口分类
                string ssyhfj = "";    //手术愈合分级
                string ssxh = "";    //手术序号
                string zcbz = "";   //主次标志
                string yyxss = "";   //医源性手术
                string ssbfz = "";    //手术并发症
                string yngr = "";     //院内感染
                string yngrzdbm = "";    //院内感染诊断编码
                string xx = "";    //血型
                string zs = "";     //主诉
                string zzms = "";    //症状描述
                string lyfs = "";   //离院方式
                string zryybm = "";   //转入医院编码
                string zryymc = "";   //转入医院名称

                string rowJSON = string.Format(Function.InCaseJSON, sfdjh, ddjgbm, gmsfhm, mzzyh, cjrbs, zfbl, yylb, jylx, cjdjh, rybzh,
                                                                  jzcs, ysbm, ysjb, sfggnbq, sfsgnbq, gms, gmy, sfbyxjc, sfwswjy, jcsj,
                                                                  cfbm, ssysbm, ssysxm, ssysizbm, ssysizxm, ssysiizbm, ssysiizxm, mzsbm, mzsxm, sszxsj,
                                                                  mzfs, ssdm, ssqkfl, ssyhfj, ssxh, zcbz, yyxss, ssbfz, yngr, yngrzdbm,
                                                                  xx, zs, zzms, lyfs, zryybm, zryymc);

                #endregion

                string inJSON = string.Format("", Manager.setObj.HospitalID, "", "", rowJSON);

                #endregion
            }
            else
            {
                #region 住院

                //查询住院患者的主表
                Neusoft.HISFC.Models.RADT.PatientInfo inPatientInfo = this.radtIntegrate.GetPatientInfomation(patientID);
                if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
                {
                    //MessageBox.Show("未找到【" + patientID + "】的住院信息!");
                    this.InsertErrLog("2", patientID, invoiceNO, ("未找到【" + patientID + "】的住院信息!").ToString());
                    return -1;
                }
                //查询住院患者的发票信息
                ArrayList balanceList = this.inMgr.QueryBalances(invoiceNO);
                if (balanceList == null || balanceList.Count <= 0)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    this.InsertErrLog("2", patientID, invoiceNO, ("发票号【" + patientID + "】,未找到对应的结算信息!").ToString());
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");

                    this.InsertErrLog("2", patientID, invoiceNO, ("发票号【" + patientID + "】,已经退费!").ToString());
                    return -1;
                }
                Neusoft.HISFC.Models.Fee.Inpatient.Balance balance = balanceList[0] as Neusoft.HISFC.Models.Fee.Inpatient.Balance;
                if (balance == null || string.IsNullOrEmpty(balance.Invoice.ID))
                {
                    //MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    this.InsertErrLog("2", patientID, invoiceNO, ("发票号【" + patientID + "】,未找到对应的结算信息1!").ToString());
                    return -1;
                }

                //发票对应的社保返回信息
                string siMemo = this.outMgr.GetSiInfo(patientID, invoiceNO, "2");
                string siReturnID = string.Empty;
                if (!string.IsNullOrEmpty(siMemo))
                {
                    string[] arr = siMemo.Split('|');
                    if (arr.Length > 0)
                    {
                        siReturnID = arr[0];
                    }
                }
                ybMedNo = siReturnID;

                //查询诊断信息
                ArrayList alDiag = this.diagManager.QueryCaseDiagnoseForClinicByState(inPatientInfo.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                string firstDiag = string.Empty;
                string firstDiagName = string.Empty;
                string secondDiag = string.Empty;
                string secondDiagName = string.Empty;

                string jsonDiagInfo = string.Empty;

                #region 获取诊断

                if (alDiag != null && alDiag.Count > 0)
                {
                    int mainIndex = 0;
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.IsValid && diag.PerssonType == ServiceTypes.I)  //患者类别 0 门诊患者 1 住院患者
                        {
                            mainIndex++;
                            if (mainIndex == 1)
                            {
                                firstDiag = diag.DiagInfo.ICD10.ID;
                                firstDiagName = diag.DiagInfo.ICD10.Name;
                            }
                            else if (mainIndex == 2)
                            {
                                secondDiag = diag.DiagInfo.ICD10.ID;
                                secondDiagName = diag.DiagInfo.ICD10.Name;
                            }
                        }                        
                    }

                }

                //取所有诊断
                for (int i = 1; i < 17; i++)
                {
                    if (alDiag != null && i <= alDiag.Count)
                    {
                        Neusoft.HISFC.Models.HealthRecord.Diagnose objDiag = alDiag[i - 1] as Neusoft.HISFC.Models.HealthRecord.Diagnose;
                        jsonDiagInfo += "'DiagnosisCode" + i.ToString() + "':'" + objDiag.DiagInfo.ICD10.ID + "','DiagnosisName" + i.ToString() + "':'" + objDiag.DiagInfo.ICD10.Name + "',";
                    }
                    else
                    {
                        jsonDiagInfo += "'DiagnosisCode" + i.ToString() + "':'','DiagnosisName" + i.ToString() + "':'',";
                    }
                }
                jsonDiagInfo = jsonDiagInfo.TrimEnd(',');

                #endregion

                #region 1、检查主单(ListCheck)【0条、一条或多条】

                //检查、检验结果
                string jsonCheckList = string.Empty;

                jsonCheckList = "'ListCheck': [" + jsonCheckList + "]";

                #endregion

                #region 2、手术列表(ListOperation)【0条、一条或多条】

                ArrayList alOperationList = new ArrayList();
                ArrayList alOperationList1 = new ArrayList();
                alOperationList1 = this.operationManager.QueryOperationByInpatientNo(inPatientInfo.ID);
                foreach (Neusoft.HISFC.Models.HealthRecord.OperationDetail obj1 in alOperationList1)
                {
                    if (obj1.OperationInfo.ID != "MS999")
                    {
                        alOperationList.Add(obj1);
                    }
                }
                string jsonOperationList = string.Empty;

                if (alOperationList != null && alOperationList.Count > 0)
                {
                    Neusoft.HISFC.Models.HealthRecord.OperationDetail info = new Neusoft.HISFC.Models.HealthRecord.OperationDetail();
                    Neusoft.HISFC.Models.HealthRecord.OperationDetail infoZZZ = null;
                    foreach (Neusoft.HISFC.Models.HealthRecord.OperationDetail obj in alOperationList)
                    {
                        if (obj.OperationInfo.ID == "94.2601")
                        {
                            infoZZZ = obj;
                            break;
                        }
                        else if (obj.OperationInfo.ID == "99.6001")
                        {
                            infoZZZ = obj;
                        }
                        if (!string.IsNullOrEmpty(obj.OperationInfo.ID))
                        {
                            info = obj;
                        }
                    }
                    if (infoZZZ == null)
                    {
                        infoZZZ = info;
                    }
                    string jsonOperationDetailList = string.Empty;

                    int iii = 0;
                    jsonOperationList = string.Empty;

                    //if (info1 != null && !string.IsNullOrEmpty(info1.OperationInfo.ID))
                    foreach (Neusoft.HISFC.Models.HealthRecord.OperationDetail info1 in alOperationList)
                    {

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
                        string AnaesthesiaType = string.Empty;
                        if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
                        {
                            if (!string.IsNullOrEmpty(info1.MarcKind))
                            {
                                switch (info1.MarcKind)
                                {
                                    case "4":
                                        AnaesthesiaType = "5";
                                        break;
                                    case "41":
                                        AnaesthesiaType = "51";
                                        break;
                                    case "42":
                                        AnaesthesiaType = "52";
                                        break;
                                    case "43":
                                        AnaesthesiaType = "53";
                                        break;
                                    case "44":
                                        AnaesthesiaType = "54";
                                        break;
                                    case "45":
                                        AnaesthesiaType = "55";
                                        break;
                                    default:
                                        AnaesthesiaType = info1.MarcKind;
                                        break;
                                }
                            }
                            else
                            {
                                AnaesthesiaType = "";
                            }
                        }
                        else
                        {
                            string sql = "select input_code from com_dictionary where   type ='CASEANESTYPE'  and   code ='{0}' and VALID_STATE= '1'";
                            sql = string.Format(sql, info1.MarcKind);
                            AnaesthesiaType = this.con.ExecSqlReturnOne(sql);
                            if (string.IsNullOrEmpty(AnaesthesiaType) || AnaesthesiaType == "-1")
                            {
                                AnaesthesiaType = info1.MarcKind;
                            }
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

                        string RecordDoctorCode = info1.OperationInfo.User01;//操作人
                        string RecordDoctorName = info1.OperationInfo.User02;//




                        #region 2、手术明细(ListOperationDetail)【0条、一条或多条】

                        string OperationRecordNo2 = inPatientInfo.ID + "000" + (iii+1).ToString();
                        string OperationNo = "1";
                        string OperationCode = info1.OperationInfo.ID;
                        string OperationName = info1.OperationInfo.Name;
                        string OperationLevel = info1.FourDoctInfo.Name;
                        if (string.IsNullOrEmpty(OperationLevel))
                        {
                            OperationLevel = "1";
                        }

                        string OperationIncisionClass = string.Empty;

                        if (!string.IsNullOrEmpty(info1.NickKind))
                        {
                            switch (info1.NickKind)
                            {
                                case "1":
                                    OperationIncisionClass = "2";
                                    break;
                                case "2":
                                    OperationIncisionClass = "3";
                                    break;
                                case "3":
                                    OperationIncisionClass = "4";
                                    break;
                                case "4":
                                    OperationIncisionClass = "1";
                                    break;
                                default:
                                    OperationIncisionClass = "1";
                                    break;
                            }
                        }
                        else
                        {
                            OperationIncisionClass = "1";
                        }
                        string OperationHealClass = string.Empty;
                        if (!string.IsNullOrEmpty(info1.CicaKind))
                        {
                            switch (info1.CicaKind)
                            {
                                case "1":
                                    OperationHealClass = "1";
                                    break;
                                case "2":
                                    OperationHealClass = "2";
                                    break;
                                case "3":
                                    OperationHealClass = "3";
                                    break;
                                default:
                                    OperationHealClass = "9";
                                    break;
                            }
                        }
                        else
                        {
                            OperationHealClass = "9";
                        }
                        string IsMajorIden = "0";
                        if (infoZZZ.OperationInfo.ID == info1.OperationInfo.ID && infoZZZ.HappenNO == info1.HappenNO)
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

                #region 3、封装出院小结(LeaveHospital)【0条或一条】

                string jsonOutRecord = string.Empty;
                Const conOutRecord = this.inMgr.QueryPatientOutRecord(patientID);
                if (conOutRecord != null)
                {
                    jsonOutRecord = string.Format(Function.LeaveHospitalJSON, inPatientInfo.PVisit.ZG.ID,  //出院转归
                                                                            conOutRecord.ID.Replace('<', ' ').Replace('>', ' ').Replace('/', '-').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", ""),              //入院情况
                                                                            conOutRecord.Name.Replace('<', ' ').Replace('>', ' ').Replace('/', '-').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", ""),            //诊疗过程
                                                                            conOutRecord.UserCode.Replace('<', ' ').Replace('>', ' ').Replace('/', '-').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", ""),        //出院情况
                                                                            conOutRecord.Memo.Replace('<', ' ').Replace('>', ' ').Replace('/', '-').Replace('"', ' ').Replace('“', ' ').Replace('”', ' ').Replace("'", "")           //出院医嘱
                                                                            );
                }
                jsonOutRecord = "'LeaveHospital': {" + jsonOutRecord + "}";

                #endregion

                #region 封装病案主页(Medical)【只能为一条】

                Neusoft.HISFC.Models.HealthRecord.Base metCase = this.inMgr.QueryInpatientCase(patientID);
                if (metCase == null)
                {
                    LogManager.WriteLog("没有查询到患者【" + inPatientInfo.PID.PatientNO + "】的病案首页信息");
                    MessageBox.Show("没有查询到患者【" + inPatientInfo.PID.PatientNO + "】的病案首页信息");
                    return -1;
                }

                string hospitalId = Manager.setObj.HospitalID;   //机构号
                string admissionNo = "";
                if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
                {
                    admissionNo = ybMedNo + "-" + inPatientInfo.PID.PatientNO.TrimStart('0') + "-" + inPatientInfo.IDCard; //住院号 【收单业务号+“-”+住院号 + “-” + 身份证号】

                }
                else
                {
                    admissionNo = ybMedNo + "-" + inPatientInfo.PID.PatientNO + "-" + inPatientInfo.IDCard; //住院号 【收单业务号+“-”+住院号 + “-” + 身份证号】
              
                }
                string sciCardNo = inPatientInfo.IDCard;   //身份证号
                string sciCardIdentified = "";    //社保卡识别码
                string outBedNum = metCase.PatientInfo.PVisit.PatientLocation.Bed.ID;   //出院床位号
                string admissionDate = metCase.PatientInfo.PVisit.InTime.ToString("yyyy-MM-dd");   //入院时间
                string dischargeDate = metCase.PatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd");   //出院时间
                string doctorCode = metCase.PatientInfo.DoctorReceiver.ID;   //责任医生编码
                string doctorName = metCase.PatientInfo.DoctorReceiver.Name;   //责任医生名称
                string isDrugAllergy = metCase.AnaphyFlag;    //是否药物过敏
                string allergyDrugCode = metCase.FirstAnaphyPharmacy.ID;   //过敏药物编码
                string allergyDrugName = metCase.FirstAnaphyPharmacy.Name; //过敏药物名称
                string isPathologicalExamination = metCase.YnFirst;   //是否有病理检查
                string pathologyCode = metCase.PathNum;    //病理号
                string isHospitalInfected = (metCase.InfectionNum > 0 ? "1" : "0");     //是否院内感染
                string hospitalInfectedCode = metCase.InfectionPosition.ID;   //院内感染诊断编码

                //血型（ABO）
                string bloodTypeS = "";
                if (!string.IsNullOrEmpty(metCase.ReactionBlood))
                {
                    switch (metCase.ReactionBlood)
                    {
                        case "A":
                            bloodTypeS = "1";
                            break;
                        case "B":
                            bloodTypeS = "2";
                            break;
                        case "AB":
                            bloodTypeS = "4";
                            break;
                        case "O":
                            bloodTypeS = "3";
                            break;
                        case "U":
                            bloodTypeS = "5";
                            break;
                        case "9":
                            bloodTypeS = "6";
                            break;
                        default:
                            bloodTypeS = metCase.ReactionBlood;
                            break;
                    }
                }
                else
                {
                    bloodTypeS = "6";
                }
                if (string.IsNullOrEmpty(bloodTypeS.Trim()))
                {
                    bloodTypeS = "6";    //6	未查
                }

                string bloodTypeE = metCase.RhBlood;    //血型（Rh）
                string leaveHospitalType = "";    //离院方式
                string leaveHospitalType1 = metCase.Out_Type;    //离院方式
                if (!string.IsNullOrEmpty(leaveHospitalType1))
                {
                    switch (leaveHospitalType1)
                    {
                        case "1":
                            leaveHospitalType = "1";
                            break;
                        case "2":
                            leaveHospitalType = "2";
                            break;
                        case "3":
                            leaveHospitalType = "3";
                            break;
                        case "4":
                            leaveHospitalType = "4";
                            break;
                        case "5":
                            leaveHospitalType = "5";
                            break;

                        default:
                            leaveHospitalType = "6";
                            break;
                    }
                }
                else
                {
                    leaveHospitalType = "1";
                }

                string chiefComplaint = "";   //主诉
                string medicalHistory = "";   //现病史
                string surgeryHistory = "";   //手术史
                string bloodTransHistory = "";   //输血史
                string marriage = "";
                string marriageStr = metCase.PatientInfo.MaritalStatus.ID.ToString();   //婚姻史
                if (!string.IsNullOrEmpty(marriageStr))
                {
                    switch (marriageStr)
                    {
                        case "M":
                            marriage = "2";
                            break;
                        case "W":
                            marriage = "3";
                            break;
                        case "A":
                            marriage = "9";
                            break;
                        case "D":
                            marriage = "4";
                            break;
                        case "R":
                            marriage = "9";
                            break;
                        case "S":
                            marriage = "1";
                            break;
                        default:
                            if (Regex.IsMatch(marriageStr, @"^\d*$"))
                            {
                                marriage = marriageStr;
                            }
                            else
                            {
                                marriage = "1";
                            }
                           
                            break;
                    }
                    //cmbMaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID;
                }
                string height = metCase.PatientInfo.Height;   //身高
                string weight = metCase.PatientInfo.Weight;  //体重
                string newbornDate = "";    //新生儿出生日期
                string newbornWeight = "";   //新生儿出生体重
                string newbornCurrentWeight = "";   //新生儿出院体重
                string bearPregnancy = "";    //生育史（孕）
                string bearYie = "";    //生育史（产）
                string admissionDiseaseId = (string.IsNullOrEmpty(metCase.InHospitalDiag.ID) ? firstDiag : metCase.InHospitalDiag.ID);     //入院诊断编码
                string admissionDiseaseName = (string.IsNullOrEmpty(metCase.InHospitalDiag.ID) ? firstDiagName : metCase.InHospitalDiag.Name);   //入院诊断名称
                string diagnosePosition1 = "";      //入院诊断方位
                string dischargeDiseaseId = (string.IsNullOrEmpty(metCase.OutDiag.ID) ? firstDiag : metCase.OutDiag.ID);     //出院诊断编码
                string dischargeDiseaseName = (string.IsNullOrEmpty(metCase.OutDiag.ID) ? firstDiagName : metCase.OutDiag.Name);   //出院诊断名称
                string diagnosePosition2 = "";      //出院诊断方位
                string tsblbs = "0";   //特殊病例 【0普通；1长期住院的精神类患者；2、临终关怀病床；3、长期康复住院病床】

                string rowJSON = string.Format(Function.InCaseJSON, hospitalId,//机构号
                                                                    admissionNo,//住院号 【收单业务号+“-”+住院号 + “-” + 身份证号】
                                                                    sciCardNo,//身份证号
                                                                    sciCardIdentified,//社保卡识别码
                                                                    outBedNum,//出院床位号
                                                                    admissionDate,//入院时间
                                                                    dischargeDate,//出院时间
                                                                    doctorCode,//责任医生编码
                                                                    doctorName,//责任医生名称
                                                                    isDrugAllergy,//是否药物过敏
                                                                    allergyDrugCode,//过敏药物编码
                                                                    allergyDrugName,  //过敏药物名称
                                                                    isPathologicalExamination,  //是否有病理检查
                                                                    pathologyCode,  //病理号
                                                                    isHospitalInfected,  //是否院内感染
                                                                    hospitalInfectedCode,  //院内感染诊断编码
                                                                    bloodTypeS,  //血型（ABO）
                                                                    bloodTypeE,  //血型（Rh）
                                                                    leaveHospitalType,  //离院方式
                                                                    chiefComplaint,  //主诉
                                                                    medicalHistory,  //现病史
                                                                    surgeryHistory,  //手术史
                                                                    bloodTransHistory,  //输血史
                                                                    marriage,  //婚姻史
                                                                    height,  //身高
                                                                    weight,  //体重
                                                                    newbornDate,  //新生儿出生日期
                                                                    newbornWeight,  //新生儿出生体重
                                                                    newbornCurrentWeight,  //新生儿出院体重
                                                                    bearPregnancy,  //生育史（孕）
                                                                    bearYie,  //生育史（产）
                                                                    admissionDiseaseId,  //入院诊断编码
                                                                    admissionDiseaseName,  //入院诊断名称
                                                                    diagnosePosition1,  //入院诊断方位
                                                                    dischargeDiseaseId,  //出院诊断编码
                                                                    dischargeDiseaseName,  //出院诊断名称
                                                                    diagnosePosition2,  //出院诊断方位
                                                                    tsblbs,  //特殊病例 
                                                                    jsonDiagInfo  //诊断
                                                                    );
                rowJSON = "'Medical':{" + rowJSON + "}";

                #endregion


                //格式 { 'Medical':{}, 'ListCheck': [{}],'ListOperation': [{}],'LeaveHospital': {} }
                rowJSON = rowJSON + "," + jsonCheckList + "," + jsonOperationList + "," + jsonOutRecord;

                string errInfo = string.Empty;
                int result = this.baseJob.UploadInfo("HB5001", rowJSON, ref errInfo);

                if (result <= 0)
                {
                    if (errInfo.Contains("已经上传") || errInfo.Contains("重复上传"))
                    {
                        return 1;
                    }
                    else
                    {
                        MessageBox.Show("【住院】" + patientID + " " + invoiceNO + "上传【病案信息】出错@" + errInfo);
                        this.InsertErrLog("2", patientID, invoiceNO, ("上传【病案信息】出错@" + errInfo).ToString());
                        return -1;
                    }
                }
                return result;

                #endregion
            }

            return 1;
        }


        /// <summary>
        /// 医师库上传[HK001]
        /// </summary>
        /// <returns></returns>
        private int UploadEmployee()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在上传医师信息!请稍后!");

            try
            {
                DoctInfoJob doctJob = new DoctInfoJob();
                doctJob.Start();
            }
            catch (Exception ex) { }
            finally
            {

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            return 1;
        }

        /// <summary>
        /// 作废上传的信息
        /// </summary>
        /// <returns></returns>
        private int CancelUploadInfo()
        {
            //判断
            if (this.fpHaveUploaded_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要作废的上传信息!");
                return -1;
            }
            //int rowIndex = this.fpHaveUploaded_Sheet1.ActiveRowIndex;
            //if (rowIndex < 0 || this.fpHaveUploaded_Sheet1.ActiveRow == null)
            //{
            //    MessageBox.Show("请选择需要作废的上传信息!");
            //    return -1;
            //}

            if (MessageBox.Show("是否继续作废?", "疑问", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return -1;
            }
            for (int rowIndex = 0; rowIndex < this.fpHaveUploaded_Sheet1.Rows.Count; rowIndex++)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(rowIndex, this.fpHaveUploaded_Sheet1.Rows.Count);

                bool isChoose = NConvert.ToBoolean(this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColPatient.ColChoose].Value);
                if (isChoose)
                {
                    string patientType = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientType].Tag.ToString();
                    string idenNo = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColIdNO].Value.ToString();
                    string patientNo = "";
                    if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
                    {
                        patientNo = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientNO].Value.ToString().TrimStart('0');
                    }
                    else
                    {
                        patientNo = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientNO].Value.ToString();
                    }
                    string wylsh = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColWylsh].Value.ToString();
                    string sfdjh = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColSfdjh].Value.ToString();

                    if (patientType == "1")
                    {
                        #region 门诊


                        //5.2 就诊信息作废(HJ002)
                        string json = @"'sfdjh': '{0}'";
                        json = string.Format(json, sfdjh);

                        string errInfo = string.Empty;
                        int result = this.baseJob.UploadInfo("HJ002", json, ref errInfo);
                        if (result <= 0)
                        {
                            MessageBox.Show("【门诊】" + patientNo + " " + sfdjh + " 作废就诊信息查询失败@" + errInfo);
                            return -1;
                        }


                        //5.9 病案信息作废(HB5003) - 无

                        //删除上传日志
                        result = this.inMgr.DeleteLog(patientType, idenNo, patientNo, wylsh, sfdjh);
                        if (result == -1)
                        {
                            MessageBox.Show(this.inMgr.Err, "错误");
                            return -1;
                        }

                        //MessageBox.Show("【门诊】" + patientNo + " " + sfdjh + " 作废就诊信息查询成功：\r\n" + errInfo);

                        #endregion
                    }
                    else if (patientType == "2")
                    {
                        #region 住院

                        //5.2 就诊信息作废(HJ002)
                        string json = @"'sfdjh': '{0}'";
                        json = string.Format(json, sfdjh);

                        string errInfo = string.Empty;
                        int result = this.baseJob.UploadInfo("HJ002", json, ref errInfo);
                        if (result <= 0)
                        {
                            MessageBox.Show("【住院】" + patientNo + " " + sfdjh + " 作废就诊信息失败@" + errInfo);
                            return -1;
                        }

                        //5.9 病案信息作废(HB5003)
                        json = @"'AdmissionNo':'{0}','HospitalId':'{1}'";
                        string admissionNo = sfdjh + "-" + patientNo + "-" + idenNo; //住院号 【收单业务号+“-”+住院号 + “-” + 身份证号】
                        json = string.Format(json, admissionNo, Manager.setObj.HospitalID);

                        errInfo = string.Empty;
                        result = this.baseJob.UploadInfo("HB5003", json, ref errInfo);
                        if (result <= 0)
                        {
                            MessageBox.Show("【住院】" + patientNo + " " + sfdjh + " 作废病案信息失败@" + errInfo);
                            return -1;
                        }

                        //删除上传日志
                        result = this.inMgr.DeleteLog(patientType, idenNo, patientNo, wylsh, sfdjh);
                        if (result == -1)
                        {
                            MessageBox.Show(this.inMgr.Err, "错误");
                            return -1;
                        }

                        //MessageBox.Show("【住院】" + patientNo + " " + sfdjh + " 作废【就诊信息】和【病案信息】成功：\r\n" + errInfo);

                        #endregion
                    }
                }
            }
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("作废完成！");

            this.QueryHaveUploadedDetail();

            return 1;
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
        /// 已上传处方
        /// </summary>
        private void QueryHaveUploadedDetail()
        {
            //清屏
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;

            //时间段
            string strBeginTime = this.dtBeginTime.Value.ToString();
            string strEndTime = this.dtEndTime.Value.ToString();

            //已上传的门诊患者
            DataTable dtOutpatient = this.outMgr.QueryHaveUploadedDetail(strBeginTime, strEndTime);
            if (dtOutpatient != null && dtOutpatient.Rows.Count > 0)
            {
                this.AddUploaded(dtOutpatient);
            }

            //已上传的住院患者
            DataTable dtInpatient = this.inMgr.QueryHaveUploadedDetail(strBeginTime, strEndTime);
            if (dtInpatient != null && dtInpatient.Rows.Count > 0)
            {
                this.AddUploaded(dtInpatient);
            }

        }

        /// <summary>
        /// 将已上传的患者显示在界面
        /// </summary>
        /// <param name="dtPatient"></param>
        private void AddUploaded(DataTable dtPatient)
        {
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                int rowIndex = this.fpHaveUploaded_Sheet1.Rows.Count;
                foreach (DataRow dRow in dtPatient.Rows)
                {
                    this.fpHaveUploaded_Sheet1.Rows.Add(rowIndex, 1);

                    //患者类别：1门诊；2住院
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColChooseALL].Value = NConvert.ToBoolean(dRow[(int)ColUploaded.ColChooseALL]);
                  
                    string patientType = dRow[(int)ColUploaded.ColPatientType].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientType].Value = (patientType == "1" ? "门诊" : "住院");
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientType].Tag = patientType;
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientNO].Value = dRow[(int)ColUploaded.ColPatientNO].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientName].Value = dRow[(int)ColUploaded.ColPatientName].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColIdNO].Value = dRow[(int)ColUploaded.ColIdNO].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColSex].Value = dRow[(int)ColUploaded.ColSex].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPactName].Value = dRow[(int)ColUploaded.ColPactName].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColInvoiceNO].Value = dRow[(int)ColUploaded.ColInvoiceNO].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPrintInvoice].Value = dRow[(int)ColUploaded.ColPrintInvoice].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColTotCost].Value = dRow[(int)ColUploaded.ColTotCost].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPubCost].Value = dRow[(int)ColUploaded.ColPubCost].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColOwnCost].Value = dRow[(int)ColUploaded.ColOwnCost].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColBalanceOper].Value = dRow[(int)ColUploaded.ColBalanceOper].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColBalanceDate].Value = dRow[(int)ColUploaded.ColBalanceDate].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientID].Value = dRow[(int)ColUploaded.ColPatientID].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPactID].Value = dRow[(int)ColUploaded.ColPactID].ToString();

                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColUploadOper].Value = dRow[(int)ColUploaded.ColUploadOper].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColUploadDate].Value = dRow[(int)ColUploaded.ColUploadDate].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColSfdjh].Value = dRow[(int)ColUploaded.ColSfdjh].ToString();
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColWylsh].Value = dRow[(int)ColUploaded.ColWylsh].ToString();

                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// 验证上传的信息
        /// </summary>
        private void CheckUploadInfo()
        {
            //判断
            if (this.fpHaveUploaded_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要验证的上传信息!");
                return;
            }
            int rowIndex = this.fpHaveUploaded_Sheet1.ActiveRowIndex;
            if (rowIndex < 0 || this.fpHaveUploaded_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择需要验证的信息!");
                return;
            }

            string patientType = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientType].Tag.ToString();
            string idenNo = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColIdNO].Value.ToString();
            string patientNo = "";
            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                patientNo = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientNO].Value.ToString().TrimStart('0');

            }
            else
            {
                patientNo = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColPatientNO].Value.ToString();
            
            }
            string wylsh = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColWylsh].Value.ToString();
            string sfdjh = this.fpHaveUploaded_Sheet1.Cells[rowIndex, (int)ColUploaded.ColSfdjh].Value.ToString();

            if (patientType == "1")
            {
                #region 门诊


                //5.3 就诊信息查询(HJ003)
                string json = @"'sfdjh': '{0}'";
                json = string.Format(json, sfdjh);

                string errInfo = string.Empty;
                int result = this.baseJob.UploadInfo("HJ003", json, ref errInfo);
                if (result <= 0)
                {
                    MessageBox.Show("【门诊】" + patientNo + " " + sfdjh + "就诊信息查询失败@" + errInfo);
                    return;
                }
                MessageBox.Show("【门诊】" + patientNo + " " + sfdjh + "就诊信息查询成功：\r\n" + errInfo);

                //5.8 病案信息查询(HB5002) - 无


                #endregion
            }
            else if (patientType == "2")
            {
                #region 住院

                //5.3 就诊信息查询(HJ003)
                string json = @"'sfdjh': '{0}'";
                json = string.Format(json, sfdjh);

                string errInfo = string.Empty;
                int result = this.baseJob.UploadInfo("HJ003", json, ref errInfo);
                if (result <= 0)
                {
                    MessageBox.Show("【住院】" + patientNo + " " + sfdjh + "就诊信息查询失败@" + errInfo);
                    return;
                }
                MessageBox.Show("【住院】" + patientNo + " " + sfdjh + "就诊信息查询成功：\r\n" + errInfo);

                //5.8 病案信息查询(HB5002)
                json = @"'AdmissionNo':'{0}','HospitalId':'{1}'";
                string admissionNo = sfdjh + "-" + patientNo + "-" + idenNo; //住院号 【收单业务号+“-”+住院号 + “-” + 身份证号】
                json = string.Format(json, admissionNo, Manager.setObj.HospitalID);
                
                errInfo = string.Empty;
                result = this.baseJob.UploadInfo("HB5002", json, ref errInfo);
                if (result <= 0)
                {
                    MessageBox.Show("【住院】" + patientNo + " " + sfdjh + "病案信息查询失败@" + errInfo);
                    return;
                }
                MessageBox.Show("【住院】" + patientNo + " " + sfdjh + "病案信息查询成功：\r\n" + errInfo);

                #endregion
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

            this.toolBarService.AddToolButton("待上传患者", "待上传患者查询", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.toolBarService.AddToolButton("一键上传", "一键上传病种分值付费接口", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            this.toolBarService.AddToolButton("已上传患者", "已上传患者查询", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);

            this.toolBarService.AddToolButton("上传医师", "医师库上传", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人员, true, false, null);

            this.toolBarService.AddToolButton("清屏", "清屏", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            this.toolBarService.AddToolButton("验证", "验证上传信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Y医保, true, false, null);

            this.toolBarService.AddToolButton("作废", "作废上传信息", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null);


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
                case "待上传患者":
                    this.QueryNeedUploadDetail();
                    break;
                case "一键上传":
                    this.AllUploadDetail();
                    break;

                case "已上传患者":
                    this.QueryHaveUploadedDetail();
                    break;

                case "上传医师":
                    this.UploadEmployee();
                    break;

                case "验证":
                    this.CheckUploadInfo();
                    break;

                case "作废":
                    this.CancelUploadInfo();
                    break;

                case "清屏":
                    this.Clear();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
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
        public int InsertErrLog(string type,string inpatientNo,string invoiceNo,string errInfo)
        {
            string sql = @"insert into upload_err_log
                            (INPATIENT_NO,
                            INVOICE_NO,
                            ERR,
                            TYPE,
                            OPER_CODE,
                            OPER_DATE) values
                            ('{0}','{1}','{2}','{3}','{4}',to_date('{5}','yyyy-mm-dd hh24:mi:ss') )
                            ";
            sql = string.Format(sql, inpatientNo, invoiceNo, errInfo, type, this.con.Operator.ID, this.con.GetDateTimeFromSysDateTime().ToString());

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

        /// <summary>
        /// 待上传患者
        /// </summary>
        private enum ColPatient
        {
            /// <summary>
            /// 选择
            /// </summary>
            ColChoose = 0,

            /// <summary>
            /// 类别
            /// </summary>
            ColPatientType = 1,

            /// <summary>
            /// 病历号
            /// </summary>
            ColPatientNO = 2,

            /// <summary>
            /// 姓名
            /// </summary>
            ColPatientName = 3,

            /// <summary>
            /// 证件号
            /// </summary>
            ColIdNO = 4,

            /// <summary>
            /// 性别
            /// </summary>
            ColSex = 5,

            /// <summary>
            /// 结算类型
            /// </summary>
            ColPactName = 6,

            /// <summary>
            /// 发票电脑号
            /// </summary>
            ColInvoiceNO = 7,

            /// <summary>
            /// 发票印刷号
            /// </summary>
            ColPrintInvoice = 8,

            /// <summary>
            /// 总金额
            /// </summary>
            ColTotCost = 9,

            /// <summary>
            /// 统筹金额
            /// </summary>
            ColPubCost = 10,

            /// <summary>
            /// 自费金额
            /// </summary>
            ColOwnCost = 11,

            /// <summary>
            /// 结算员
            /// </summary>
            ColBalanceOper = 12,

            /// <summary>
            /// 结算日期
            /// </summary>
            ColBalanceDate = 13,

            /// <summary>
            /// 门诊流水号/住院流水号
            /// </summary>
            ColPatientID = 14,

            /// <summary>
            /// 结算类型编码
            /// </summary>
            ColPactID = 15

        }


        /// <summary>
        /// 已上传患者
        /// </summary>
        private enum ColUploaded
        {
            /// <summary>
            /// 选择
            /// </summary>
            ColChooseALL = 0,
            /// <summary>
            /// 类别
            /// </summary>
            ColPatientType = 1,

            /// <summary>
            /// 病历号
            /// </summary>
            ColPatientNO = 2,

            /// <summary>
            /// 姓名
            /// </summary>
            ColPatientName = 3,

            /// <summary>
            /// 证件号
            /// </summary>
            ColIdNO = 4,

            /// <summary>
            /// 性别
            /// </summary>
            ColSex = 5,

            /// <summary>
            /// 结算类型
            /// </summary>
            ColPactName = 6,

            /// <summary>
            /// 发票电脑号
            /// </summary>
            ColInvoiceNO = 7,

            /// <summary>
            /// 发票印刷号
            /// </summary>
            ColPrintInvoice = 8,

            /// <summary>
            /// 总金额
            /// </summary>
            ColTotCost = 9,

            /// <summary>
            /// 统筹金额
            /// </summary>
            ColPubCost = 10,

            /// <summary>
            /// 自费金额
            /// </summary>
            ColOwnCost = 11,

            /// <summary>
            /// 结算员
            /// </summary>
            ColBalanceOper = 12,

            /// <summary>
            /// 结算日期
            /// </summary>
            ColBalanceDate = 13,

            /// <summary>
            /// 门诊流水号/住院流水号
            /// </summary>
            ColPatientID = 14,

            /// <summary>
            /// 结算类型编码
            /// </summary>
            ColPactID = 15,

            /// <summary>
            /// 上传人员
            /// </summary>
            ColUploadOper = 16,

            /// <summary>
            /// 上传日期
            /// </summary>
            ColUploadDate = 17,

            /// <summary>
            /// 上传人员
            /// </summary>
            ColSfdjh = 18,

            /// <summary>
            /// 上传日期
            /// </summary>
            ColWylsh = 19

        }

        #endregion

        private void cmbPatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbPatientType.Tag.ToString()))
            {
                patientType = this.cmbPatientType.Tag.ToString();
            }
            this.QueryNeedUploadDetail();
        }

        private void tabMain_Click(object sender, EventArgs e)
        {
            //if (this.fpNeedUpload.ActiveSheet != this.fpNeedUpload_Sheet1)
            //{
            //    this.neuLabel1.Visible= false;
            //    this.cmbPatientType.Visible = false;
            //}
            if (this.tabMain.SelectedIndex != 0)
            {
                this.neuLabel1.Visible = false;
                this.cmbPatientType.Visible = false;

            }
            else
            {
                this.neuLabel1.Visible = true;
                this.cmbPatientType.Visible = true;

            }

        }


    }
}
