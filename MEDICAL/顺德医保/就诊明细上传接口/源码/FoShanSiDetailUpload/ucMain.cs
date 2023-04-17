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

namespace FoShanSiDetailUpload
{
    /// <summary>
    /// 佛山市定点医疗机构就诊明细上传主界面
    /// </summary>
    public partial class ucMain : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucMain()
        {
            InitializeComponent();
        }

        #region 变量和属性

        /// <summary>
        /// 工具栏
        /// </summary>
        private ToolBarService toolBarService = new ToolBarService();

        #region 业务变量

        /// <summary>
        /// 接口管理类
        /// </summary>
        private SIBizProcess siBizMgr = new SIBizProcess();

        /// <summary>
        /// 门诊业务类
        /// </summary>
        private BizLogic.OutManager outMgr = new FoShanSiDetailUpload.BizLogic.OutManager();

        /// <summary>
        /// 住院业务类
        /// </summary>
        private BizLogic.InManager inMgr = new FoShanSiDetailUpload.BizLogic.InManager();

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
        /// 会话ID
        /// </summary>
        private string seesionID = string.Empty;

        /// <summary>
        /// 用户ID
        /// </summary>
        private string userID = string.Empty;

        /// <summary>
        /// 用户密码
        /// </summary>
        private string userPw = string.Empty;

        #endregion

        #region 方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            //当前时间
            DateTime dtNow = this.outMgr.GetDateTimeFromSysDateTime();

            //医院编码
            this.txtHosInfo.Text = Function.HospitalCode;

            this.txtUserID.Text = string.Empty;
            this.txtPassWord.Text = string.Empty;
            this.txtSessionID.Text = string.Empty;

            this.lblLoginInfo.Text = "请先登录!";
            this.lblLoginInfo.ForeColor = System.Drawing.Color.Red;

            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            //this.fpUploaded_Sheet1.Rows.Count = 0;

            this.dtBeginTime.Value = dtNow.Date;
            this.dtEndTime.Value = dtNow.Date.AddDays(1).AddSeconds(-1);

            //变量
            this.seesionID = string.Empty;
            this.userID = string.Empty;
            this.userPw = string.Empty;
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void Login()
        {
            //医院编码
            if (string.IsNullOrEmpty(Function.HospitalCode))
            {
                MessageBox.Show("请联系信息，维护医院编码!", "错误");
                return;
            }


            if (string.IsNullOrEmpty(this.txtUserID.Text.Trim()))
            {
                this.txtUserID.Text = Function.UserID;
            }

            if (string.IsNullOrEmpty(this.txtPassWord.Text.Trim()))
            {
                this.txtPassWord.Text = Function.PassWord;
            }
            //用户名
            string userID = this.txtUserID.Text.Trim();
            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("请输入社保登录账户!", "错误");
                this.txtUserID.Focus();
                return;
            }
            //密码
            string pw = this.txtPassWord.Text.Trim();
            if (string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("请输入社保登录密码!", "错误");
                this.txtPassWord.Focus();
                return;
            }

            string transNO = Function.LoginTransNO;
            string inXML = string.Format(Function.LoginXML, Function.HospitalCode, userID, pw, "1", "1");

            Model.ResultHead result = this.siBizMgr.Login(transNO, inXML);

            if (result.Code == "1")
            {
                //用户ID
                this.userID = userID;
                //用户密码
                this.userPw = pw;

                //会话ID
                this.seesionID = result.SessionId;
                this.txtSessionID.Text = result.SessionId;

                this.lblLoginInfo.Text = "登录成功!";
                this.lblLoginInfo.ForeColor = System.Drawing.Color.Blue;

                //登录成功
                MessageBox.Show(result.Message);
            }
            else
            {
                //用户ID
                this.userID = string.Empty;
                //用户密码
                this.userPw = string.Empty;

                //会话ID
                this.seesionID = string.Empty;
                this.txtSessionID.Text = string.Empty;

                this.lblLoginInfo.Text = "登录失败，请重新登录!";
                this.lblLoginInfo.ForeColor = System.Drawing.Color.Red;

                //登录失败
                MessageBox.Show(result.Message);
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        private void ModifyPW()
        {
            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                MessageBox.Show("请先登录成功!", "错误");
                return;
            }

            Control.frmChangePassWord frmChange = new FoShanSiDetailUpload.Control.frmChangePassWord();
            frmChange.UserID = this.userID;
            frmChange.UserPw = this.userPw;
            frmChange.Show();

            if (frmChange.IsSuccess)
            {
                //修改成功
                this.Clear();
            }
            else
            {
                //无修改
            }


        }

        /// <summary>
        /// 待上传处方
        /// </summary>
        private void QueryNeedUploadDetail()
        {
            //清屏
            this.fpNeedUpload_Sheet1.Rows.Count = 0;

            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                MessageBox.Show("请先登录成功!", "错误");
                return;
            }
            //时间段
            string strBeginTime = this.dtBeginTime.Value.ToString();
            string strEndTime = this.dtEndTime.Value.ToString();

            //待上传的门诊患者
            DataTable dtOutpatient = this.outMgr.QueryNeedUploadDetail(strBeginTime, strEndTime);
            if (dtOutpatient != null && dtOutpatient.Rows.Count > 0)
            {
                this.AddNeedUpload(dtOutpatient);
            }

            //待上传的住院患者
            DataTable dtInpatient = this.inMgr.QueryNeedUploadDetail(strBeginTime, strEndTime);
            if (dtInpatient != null && dtInpatient.Rows.Count > 0)
            {
                this.AddNeedUpload(dtInpatient);
            }

            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要上传的数据!", "警告");
                return;
            }

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
            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                MessageBox.Show("请先登录成功!", "错误");
                return;
            }
            if (this.fpNeedUpload_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要上传的处方信息!");
                return;
            }
            //提示
            string strTips = "一键上传将会上传的信息：\r\n 1、主单信息；\r\n 2、费用明细信息；\r\n 3、住院医嘱信息；\r\n 4、住院检治执行信息；\r\n 5、住院药品执行信息；\r\n 6、门诊费用明细信息；\r\n 7、病案信息；\r\n 8、化验检查单据信息；";
            if (MessageBox.Show(strTips, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            for (int k = 0; k < this.fpNeedUpload_Sheet1.Rows.Count; k++)
            {
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

                        //1、主单信息上传
                        rev = this.UploadMainInfo(patientID, patientType, invoiceNO, transType);
                        if (rev == -1)
                        {
                            continue;
                        }

                        //2、费用明细信息上传
                        rev = this.UploadFeeDetail(patientID, patientType, invoiceNO, transType);
                        if (rev == -1)
                        {
                            continue;
                        }

                        MessageBox.Show("门诊发票信息【" + invoiceNO + "】就诊明细上传成功!");

                        #endregion
                    }
                    else
                    {
                        #region 住院

                        int rev = 1;

                        //1、主单信息上传
                        rev = this.UploadMainInfo(patientID, patientType, invoiceNO, transType);
                        if (rev == -1)
                        {
                            continue;
                        }

                        //2、费用明细信息上传
                        rev = this.UploadFeeDetail(patientID, patientType, invoiceNO, transType);
                        if (rev == -1)
                        {
                            continue;
                        }

                        MessageBox.Show("住院发票信息【" + invoiceNO + "】就诊明细上传成功!");

                        #endregion
                    }
                }
            }

            //重新查询
            this.QueryNeedUploadDetail();

        }

        #region 上传信息

        /// <summary>
        /// 主单信息上传
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientType"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="transType"></param>
        /// <returns></returns>
        private int UploadMainInfo(string patientID, string patientType, string invoiceNO, string transType)
        {
            //交易号
            string transNO = Function.MainInfoTransNO;

            if (patientType == "1")
            {
                #region 门诊

                //查询门诊挂号记录
                ArrayList alReg = this.outRegMgr.QueryPatient(patientID);
                if (alReg == null || alReg.Count <= 0)
                {
                    MessageBox.Show("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!");
                    return -1;
                }
                FS.HISFC.Models.Registration.Register outPatientInfo = alReg[0] as FS.HISFC.Models.Registration.Register;
                if (outPatientInfo == null || string.IsNullOrEmpty(outPatientInfo.ID))
                {
                    MessageBox.Show("门诊发票号【" + invoiceNO + "】找不到对应的挂号记录!");
                    return -1;
                }
                //查询发票信息
                ArrayList balanceList = this.outMgr.QueryBalances(invoiceNO);
                if (balanceList == null && balanceList.Count <= 0)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");
                    return -1;
                }
                FS.HISFC.Models.Fee.Outpatient.Balance outBalance = balanceList[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
                if (outBalance == null || string.IsNullOrEmpty(outBalance.Invoice.ID))
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }
                //查询诊断信息
                ArrayList alDiag = this.diagManager.QueryCaseDiagnoseForClinicByState(outPatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                string firstDiag = string.Empty;
                string secondDiag = string.Empty;

                #region 获取诊断

                if (alDiag != null && alDiag.Count > 0)
                {
                    int i = 0;
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.IsValid)
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


                //入参
                string inXML = Function.CommonHeadXML;

                #region 组装Row

                string wylsh = patientType + "-" + invoiceNO + "-" + transType;
                string sfdjh = outBalance.PrintedInvoiceNO;
                string djjsrq = outBalance.BalanceOper.OperTime.ToString("yyyy-MM-dd");
                string ddjgbm = Function.HospitalCode;
                string ddjgmc = FS.FrameWork.Management.Connection.Hospital.Name;
                string fyfsjgbm = Function.HospitalCode;
                string fyfsjgmc = FS.FrameWork.Management.Connection.Hospital.Name;
                string zwjgmc = "";
                string gmsfhm = outPatientInfo.IDCard;
                string cbrmc = outPatientInfo.Name;
                string cbrxb = Function.ConvertSexCode(outPatientInfo.Sex.ID.ToString());
                string cbrcsrq = outPatientInfo.Birthday.ToString("yyyy-MM-dd");
                string rylbbm = "";
                string jzfsbm = "1";   //参考就医方式字典表  ??gmz??
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
                string ybzxdm = "";
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
                string yylb = "";
                string cjdjh = "";
                string jjlx = "";
                string ssdm = "";
                string ssqkfl = "";
                string cyzry = "";

                string rowXML = string.Format(Function.MainInfoXML, wylsh, sfdjh, djjsrq, ddjgbm, ddjgmc, fyfsjgbm, fyfsjgmc, zwjgmc, gmsfhm, cbrmc,
                                           cbrxb, cbrcsrq, rylbbm, jzfsbm, sfydjy, ryzdbm, cyzdbm, fzd, fzd, fzd,
                                           fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd,
                                           fzd, fzd, fzd, sftbmbdjbz, mbtbdm, ryrq, cyrq, jzrq, sfhy, sfbrq,
                                           sg, tz, xy, kfxt, chxt, tw, xl, sfzry, ybzxdm, ydrylb,
                                           ydcbxzqydm, dylx, cblx, zje, ybnzje, ybnzfje, zfbl, zhfbl, xyf, zchyf,
                                           zcyf, ybylfwf, ybzlczf, hlf, blzdf, syszdf, yxxzdf, lczdxmf, sszlf, mjf,
                                           fsszlxmf, lcwlzlf, kff, xf, bdblzpf, qdblzpf, nxyzlzpf, xbyzlzpf, jcyclf, zlyclf,
                                           ssyclf, mzzyh, cjrbs, yylb, cjdjh, jjlx, ssdm, ssqkfl, cyzry
                                           );

                #endregion

                inXML = string.Format(inXML, Function.HospitalCode, this.userID, this.seesionID, rowXML);

                Model.ResultHead result = this.siBizMgr.UploadMainInfo(transNO, inXML);

                if (result.Code == "1")
                {
                    return 1;
                }
                else
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】上传信息失败!\r\n" + result.Message);
                    return -1;
                }

                #endregion
            }
            else
            {
                #region 住院

                //查询住院患者的主表
                FS.HISFC.Models.RADT.PatientInfo inPatientInfo = this.radtIntegrate.GetPatientInfomation(patientID);
                if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
                {
                    MessageBox.Show("未找到【" +patientID + "】的住院信息!");
                    return -1;
                }
                //查询住院患者的发票信息
                ArrayList balanceList = this.inMgr.QueryBalances(invoiceNO);
                if (balanceList == null && balanceList.Count <= 0)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");
                    return -1 ;
                }
                FS.HISFC.Models.Fee.Inpatient.Balance balance = balanceList[0] as FS.HISFC.Models.Fee.Inpatient.Balance;
                if (balance == null || string.IsNullOrEmpty(balance.Invoice.ID))
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }

                //查询诊断信息  ??gmz??

                //入参
                string inXML = Function.CommonHeadXML;

                #region 组装Row

                string wylsh = patientType + "-" + invoiceNO + "-" + transType;
                string sfdjh = balance.PrintedInvoiceNO;
                string djjsrq = balance.BalanceOper.OperTime.ToString("yyyy-MM-dd");
                string ddjgbm = Function.HospitalCode;
                string ddjgmc = FS.FrameWork.Management.Connection.Hospital.Name;
                string fyfsjgbm = Function.HospitalCode;
                string fyfsjgmc = FS.FrameWork.Management.Connection.Hospital.Name;
                string zwjgmc = "";
                string gmsfhm = inPatientInfo.IDCard;
                string cbrmc = inPatientInfo.Name;
                string cbrxb = Function.ConvertSexCode(inPatientInfo.Sex.ID.ToString());
                string cbrcsrq = inPatientInfo.Birthday.ToString("yyyy-MM-dd");
                string rylbbm = "";
                string jzfsbm = "1";   //参考就医方式字典表  ??gmz??
                string sfydjy = "0";
                string ryzdbm = "MS999";     //入院诊断   ??gmz??
                string cyzdbm = "MS999";    //出院诊断  ??gmz??
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
                string ybzxdm = "";
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
                string mzzyh = inPatientInfo.PID.PatientNO;   //住院流水号
                string cjrbs = "";
                string yylb = "";
                string cjdjh = "";
                string jjlx = "";
                string ssdm = "";
                string ssqkfl = "";
                string cyzry = "";

                string rowXML = string.Format(Function.MainInfoXML, wylsh, sfdjh, djjsrq, ddjgbm, ddjgmc, fyfsjgbm, fyfsjgmc, zwjgmc, gmsfhm, cbrmc,
                                           cbrxb, cbrcsrq, rylbbm, jzfsbm, sfydjy, ryzdbm, cyzdbm, fzd, fzd, fzd,
                                           fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd, fzd,
                                           fzd, fzd, fzd, sftbmbdjbz, mbtbdm, ryrq, cyrq, jzrq, sfhy, sfbrq,
                                           sg, tz, xy, kfxt, chxt, tw, xl, sfzry, ybzxdm, ydrylb,
                                           ydcbxzqydm, dylx, cblx, zje, ybnzje, ybnzfje, zfbl, zhfbl, xyf, zchyf,
                                           zcyf, ybylfwf, ybzlczf, hlf, blzdf, syszdf, yxxzdf, lczdxmf, sszlf, mjf,
                                           fsszlxmf, lcwlzlf, kff, xf, bdblzpf, qdblzpf, nxyzlzpf, xbyzlzpf, jcyclf, zlyclf,
                                           ssyclf, mzzyh, cjrbs, yylb, cjdjh, jjlx, ssdm, ssqkfl, cyzry
                                           );

                #endregion

                inXML = string.Format(inXML, Function.HospitalCode, this.userID, this.seesionID, rowXML);
                Model.ResultHead result = this.siBizMgr.UploadMainInfo(transNO, inXML);

                if (result.Code == "1")
                {
                    return 1;
                }
                else
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】上传信息失败!\r\n" + result.Message);
                    return -1;
                }

                #endregion
            }


        }

        /// <summary>
        /// 费用明细信息上传
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="patientType"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="transType"></param>
        /// <returns></returns>
        private int UploadFeeDetail(string patientID, string patientType, string invoiceNO, string transType)
        {
            //交易号
            string transNO = Function.FeeDetailTransNO;

            if (patientType == "1")
            {
                #region 门诊

                ArrayList balanceList = this.outMgr.QueryBalances(invoiceNO);
                if (balanceList == null && balanceList.Count <= 0)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");
                    return -1;
                }
                FS.HISFC.Models.Fee.Outpatient.Balance balance = balanceList[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
                if (balance == null)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }
                //发票对应的明细信息
                ArrayList alFee = this.outMgr.QueryFeeItemByInvoiceNO(balance.Patient.ID, balance.Invoice.ID);
                if (alFee == null || alFee.Count <= 0)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】未找到对应的处方信息!");
                    return -1;
                }
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = alFee[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                if (feeItem != null)
                {
                    string clinicCode = feeItem.Patient.ID;   //门诊流水号作为处方号
                    decimal totCost = 0m;   //发票总金额
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList fTemp in alFee)
                    {
                        totCost += fTemp.FT.OwnCost + fTemp.FT.PubCost + fTemp.FT.PayCost;
                    }

                    string strRows = string.Empty;
                    for (int k = 0; k < alFee.Count; k++)
                    {
                        feeItem = alFee[k] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                        string errMsg = string.Empty;

                        //项目编码   ??gmz-中心码 或 本地码?? 
                        string itemCode = string.Empty;
                        string itemName = feeItem.Item.Name;
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
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                return -1;
                            }

                            itemCode = feeItem.Item.GBCode;

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
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(自定义码为空)，请先进行维护！");
                                return -1;
                            }

                            itemCode = feeItem.Item.UserCode;

                            #endregion
                        }

                        //项目总量，单价，总额，收费日期
                        string itemQty = feeItem.Item.Qty.ToString();
                        string itemPrice = System.Math.Round(feeItem.Item.PackQty == 0 ? feeItem.Item.Price : feeItem.Item.Price / feeItem.Item.PackQty, 4).ToString();
                        string itemCost = (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost).ToString();
                        DateTime feeDate = feeItem.FeeOper.OperTime;

                        #region 组装Row

                        string zdbm = patientType + "-" + invoiceNO + "-" + transType;
                        string wylsh = feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString() + "-" + feeItem.Order.ID + "-" + feeItem.InvoiceCombNO;//RECIPE_NO, SEQUENCE_NO, TRANS_TYPE, MO_ORDER, INVOICE_SEQ
                        string fwrq = feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd");
                        string xmbm = itemCode;
                        string xmmc = itemName;
                        string xmlb = (feeItem.Item.ItemType == EnumItemType.Drug ? "1" : "2"); //1为“药品”，2为“诊疗/服务设施”
                        string dj = itemPrice;
                        string sl = itemQty;
                        string zj = itemCost;
                        string ysbm = feeItem.RecipeOper.ID;
                        string ysmc = this.GetEmpoyeeName(feeItem.RecipeOper.ID, ref errMsg);
                        string ksbm = feeItem.RecipeOper.Dept.ID;
                        string ksmc = this.GetDeptName(feeItem.RecipeOper.Dept.ID, ref errMsg);
                        string yf = (string.IsNullOrEmpty(feeItem.Order.Usage.Name) ? "0" : feeItem.Order.Usage.Name);
                        string gytj = (string.IsNullOrEmpty(feeItem.Order.Usage.Name) ? "0" : feeItem.Order.Usage.Name);
                        string yl = feeItem.Order.DoseOnce.ToString("F2");
                        string pc = (string.IsNullOrEmpty(feeItem.Order.Frequency.ID) ? "0" : feeItem.Order.Frequency.ID);
                        string yyts = feeItem.Days.ToString();
                        string ybnje = "";
                        string jylx = "1";   //标识是否为冲减单据，1是0否
                        string ypgg = (string.IsNullOrEmpty(feeItem.Item.Specs) ? "次" : feeItem.Item.Specs);
                        string cydybs = "0";
                        string zyh = patientID;
                        string yzzdid = feeItem.Order.ID;
                        string yzmxid = feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString() + "-" + feeItem.Order.ID + "-" + feeItem.InvoiceCombNO;//RECIPE_NO, SEQUENCE_NO, TRANS_TYPE, MO_ORDER, INVOICE_SEQ

                        string rowXML = string.Format(Function.FeeDetailXML, zdbm, wylsh, fwrq, xmbm, xmmc, xmlb, dj, sl, zj, ysbm,
                                                                            ysmc, ksbm, ksmc, yf, gytj, yl, pc, yyts, ybnje, jylx,
                                                                            ypgg, cydybs, zyh, yzzdid, yzmxid);

                        strRows += "\r\n" + rowXML;

                        #endregion

                    }

                    string inXML = string.Format(Function.CommonHeadXML, Function.HospitalCode, this.userID, this.seesionID, strRows);
                    Model.ResultHead result = this.siBizMgr.UploadFeeDetail(transNO, inXML);

                    if (result.Code == "1")
                    {
                        return 1;
                    }
                    else
                    {
                        MessageBox.Show("发票号【" + invoiceNO + "】费用明细信息上传失败!\r\n" + result.Message);
                        return -1;
                    }
                }

                return 1;

                #endregion
            }
            else
            {
                #region 住院

                //查询住院患者的主表
                FS.HISFC.Models.RADT.PatientInfo inPatientInfo = this.radtIntegrate.GetPatientInfomation(patientID);
                if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
                {
                    MessageBox.Show("未找到【" + patientID + "】的住院信息!");
                    return -1;
                }
                //查询住院患者的发票信息
                ArrayList balanceList = this.inMgr.QueryBalances(invoiceNO);
                if (balanceList == null && balanceList.Count <= 0)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }
                if (balanceList.Count > 1)
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，已经退费!");
                    return -1;
                }
                FS.HISFC.Models.Fee.Inpatient.Balance balance = balanceList[0] as FS.HISFC.Models.Fee.Inpatient.Balance;
                if (balance == null || string.IsNullOrEmpty(balance.Invoice.ID))
                {
                    MessageBox.Show("发票号【" + invoiceNO + "】，未找到对应的结算信息!");
                    return -1;
                }

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
                    MessageBox.Show("发票号【" + invoiceNO + "】未找到对应的处方信息!");
                    return -1;
                }
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = alFee[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (feeItem != null)
                {
                    string inpatientNO = feeItem.ID;   //住院流水号，作为处方号
                    decimal totCost = 0m;              //发票总金额

                    //存放项目汇总信息
                    Hashtable hsUpLoadFeeDetails = new Hashtable();
                    ArrayList feeGatherlsClone = new ArrayList();
                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fTemp in alFee)
                    {
                        totCost += fTemp.FT.OwnCost + fTemp.FT.PubCost + fTemp.FT.PayCost;

                        //相同的项目进行累加汇总后再进行上传 【划价时间+项目编码】
                        if (hsUpLoadFeeDetails.ContainsKey(fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID))
                        {
                            FS.HISFC.Models.Fee.FeeItemBase feeItemList = hsUpLoadFeeDetails[fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID] as FS.HISFC.Models.Fee.FeeItemBase;

                            feeItemList.Item.Qty += fTemp.Item.Qty;//数量累加
                            feeItemList.FT.TotCost += fTemp.FT.TotCost;//金额累加
                        }
                        else
                        {
                            FS.HISFC.Models.Fee.FeeItemBase fCloce = fTemp.Clone();
                            hsUpLoadFeeDetails.Add(fTemp.ChargeOper.OperTime.ToString() + fTemp.Item.ID, fCloce);
                            feeGatherlsClone.Add(fCloce);
                        }

                    }

                    string strRows = string.Empty;
                    for (int k = 0; k < feeGatherlsClone.Count; k++)
                    {
                        feeItem = feeGatherlsClone[k] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        string errMsg = string.Empty;

                        //项目编码   ??gmz-中心码 或 本地码?? 
                        string itemCode = string.Empty;
                        string itemName = feeItem.Item.Name;
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
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                return -1;
                            }

                            itemCode = feeItem.Item.GBCode;

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
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(自定义码为空)，请先进行维护！");
                                return -1;
                            }

                            itemCode = feeItem.Item.UserCode;

                            #endregion
                        }
                        //项目总量，单价，总额，收费日期
                        string itemQty = feeItem.Item.Qty.ToString();
                        string itemPrice = System.Math.Round(feeItem.Item.PackQty == 0 ? feeItem.Item.Price : feeItem.Item.Price / feeItem.Item.PackQty, 4).ToString();
                        string itemCost = (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost).ToString();
                        DateTime feeDate = balance.BalanceOper.OperTime;      //住院应为结算日期
                        //处方医生
                        string doctName = this.GetEmpoyeeName(feeItem.RecipeOper.ID, ref errMsg);
                        //科室名称 字典bqdm
                        string deptName = this.GetDeptName(feeItem.RecipeOper.Dept.ID, ref errMsg);

                        #region 组装Row

                        string zdbm = patientType + "-" + invoiceNO + "-" + transType;
                        string wylsh = feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString();//RECIPE_NO, TRANS_TYPE, SEQUENCE_NO
                        string fwrq = feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd");
                        string xmbm = itemCode;
                        string xmmc = itemName;
                        string xmlb = (feeItem.Item.ItemType == EnumItemType.Drug ? "1" : "2"); //1为“药品”，2为“诊疗/服务设施”
                        string dj = itemPrice;
                        string sl = itemQty;
                        string zj = itemCost;
                        string ysbm = feeItem.RecipeOper.ID;
                        string ysmc = this.GetEmpoyeeName(feeItem.RecipeOper.ID, ref errMsg);
                        string ksbm = feeItem.RecipeOper.Dept.ID;
                        string ksmc = this.GetDeptName(feeItem.RecipeOper.Dept.ID, ref errMsg);
                        string yf = (string.IsNullOrEmpty(feeItem.Order.Usage.Name) ? "0" : feeItem.Order.Usage.Name);
                        string gytj = (string.IsNullOrEmpty(feeItem.Order.Usage.Name) ? "0" : feeItem.Order.Usage.Name);
                        string yl = feeItem.Order.DoseOnce.ToString("F2");
                        string pc = (string.IsNullOrEmpty(feeItem.Order.Frequency.ID) ? "0" : feeItem.Order.Frequency.ID);
                        string yyts = feeItem.Days.ToString();
                        string ybnje = "";
                        string jylx = "1";   //标识是否为冲减单据，1是0否
                        string ypgg = (string.IsNullOrEmpty(feeItem.Item.Specs) ? "次" : feeItem.Item.Specs);
                        string cydybs = "0";
                        string zyh = patientID;
                        string yzzdid = (!string.IsNullOrEmpty(feeItem.Order.ID) ? feeItem.Order.ID : (feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString()));
                        string yzmxid = feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString();//RECIPE_NO, TRANS_TYPE, SEQUENCE_NO

                        string rowXML = string.Format(Function.FeeDetailXML, zdbm, wylsh, fwrq, xmbm, xmmc, xmlb, dj, sl, zj, ysbm,
                                                                            ysmc, ksbm, ksmc, yf, gytj, yl, pc, yyts, ybnje, jylx,
                                                                            ypgg, cydybs, zyh, yzzdid, yzmxid);

                        strRows += "\r\n" + rowXML;

                        #endregion
                       
                    }

                    string inXML = string.Format(Function.CommonHeadXML, Function.HospitalCode, this.userID, this.seesionID, strRows); 
                    Model.ResultHead result = this.siBizMgr.UploadFeeDetail(transNO, inXML);

                    if (result.Code == "1")
                    {
                        return 1;
                    }
                    else
                    {
                        MessageBox.Show("发票号【" + invoiceNO + "】费用明细信息上传失败!\r\n" + result.Message);
                        return -1;
                    }
                }

                return 1;

                #endregion
            }
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
        /// 获取姓名
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

            //医院编码
            this.txtHosInfo.Text = Function.HospitalCode;

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

            base.OnLoad(e);
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
            this.toolBarService.AddToolButton("登录", "登陆", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("修改密码", "修改登录密码", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);

            this.toolBarService.AddToolButton("待上传患者", "待上传患者查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.toolBarService.AddToolButton("一键上传", "一键上传就诊明细", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            this.toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);


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
                case "登录":
                    this.Login();
                    break;
                case "修改密码":
                    this.ModifyPW();
                    break;

                case "待上传患者":
                    this.QueryNeedUploadDetail();
                    break;
                case "一键上传":
                    this.AllUploadDetail();
                    break;

                case "清屏":
                    this.Clear();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
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

        #endregion
    }
}
