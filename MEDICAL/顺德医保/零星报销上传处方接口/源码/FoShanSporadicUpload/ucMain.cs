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

namespace FoShanSporadicUpload
{
    /// <summary>
    /// 佛山社保零星报销处方上传主界面
    /// </summary>
    public partial class ucMain : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucMain()
        {
            InitializeComponent();

            this.txtUserID.Text = Function.UserID;
            this.txtPassWord.Text = Function.PassWord;
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
        /// 门诊费用业务类
        /// </summary>
        private BizLogic.OutFeeManager outFeeMgr = new FoShanSporadicUpload.BizLogic.OutFeeManager();

        /// <summary>
        /// 住院费用业务类
        /// </summary>
        private BizLogic.InFeeManager inFeeMgr = new FoShanSporadicUpload.BizLogic.InFeeManager();

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
        /// 患者入出转管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
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
        /// 中心费用大类对照
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper fldmHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 中心科室对照
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper bqdmHelper = new FS.FrameWork.Public.ObjectHelper();


        /// <summary>
        /// 工伤医保管理
        /// </summary>
        //FoShanGSSI.MedicalProcess medicalProcess = new FoShanGSSI.MedicalProcess();

        /// <summary>
        /// 错误描述
        /// </summary>
        protected string errMsg = string.Empty;

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
            DateTime dtNow = this.outFeeMgr.GetDateTimeFromSysDateTime();

            //医院编码
            this.txtHosInfo.Text = Function.HospitalCode;

            this.txtUserID.Text = string.Empty;
            this.txtPassWord.Text = string.Empty;
            this.txtSessionID.Text = string.Empty;

            this.lblLoginInfo.Text = "请先登录!";
            this.lblLoginInfo.ForeColor = System.Drawing.Color.Red;

            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;

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
        private int Login()
        {
            //医院编码
            if (string.IsNullOrEmpty(Function.HospitalCode))
            {
                MessageBox.Show("请联系信息，维护医院编码!", "错误");
                return -1;
            }

            //用户名
            string userID = this.txtUserID.Text.Trim();
            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("请输入社保登录账户!", "错误");
                this.txtUserID.Focus();
                return -1;
            }
            //密码
            string pw = this.txtPassWord.Text.Trim();
            if (string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("请输入社保登录密码!", "错误");
                this.txtPassWord.Focus();
                return -1;
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
                //MessageBox.Show(result.Message);
                return 1;
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
                //MessageBox.Show(result.Message);
                return -1;
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

            Control.frmChangePassWord frmChange = new FoShanSporadicUpload.Control.frmChangePassWord();
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
        private void QueryNeedUploadRecipe()
        {
            //清屏
            this.fpNeedUpload_Sheet1.Rows.Count = 0;

            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                if (this.Login() < 0)
                {
                    MessageBox.Show("登录失败!", "错误");
                    return;
                }
            }
            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                MessageBox.Show("请先登录成功!", "错误");
                return;
            }

            string strBeginTime = this.dtBeginTime.Value.ToString("yyyy-MM-dd");
            string strEndTime = this.dtEndTime.Value.ToString("yyyy-MM-dd");

            string transNO = Function.NeedUploadRecipeTransNO;
            string inXML = string.Format(Function.NeedUploadRecipeXML, Function.HospitalCode, this.userID, this.seesionID, strBeginTime, strEndTime);
            List<Model.NeedUploadRecipe> listRecipe = new List<FoShanSporadicUpload.Model.NeedUploadRecipe>();

            Model.ResultHead result = this.siBizMgr.QueryNeedUploadRecipe(transNO, inXML, ref listRecipe);
            if (result.Code == "1")
            {
                #region 显示在界面

                if (listRecipe != null && listRecipe.Count > 0)
                {
                    this.ShowNeedUploadRecipe(listRecipe);
                }

                #endregion

                //查询成功
                MessageBox.Show(result.Message + "\r\n查询成功!");
            }
            else
            {
                //查询失败
                MessageBox.Show(result.Message);
            }

        }

        /// <summary>
        /// 显示待上传的处方
        /// </summary>
        /// <param name="listRecipe"></param>
        private void ShowNeedUploadRecipe(List<Model.NeedUploadRecipe> listRecipe)
        {
            this.fpNeedUpload_Sheet1.Rows.Count = 0;
            if (listRecipe != null && listRecipe.Count > 0)
            {
                int rowIndex = this.fpNeedUpload_Sheet1.Rows.Count;
                foreach (Model.NeedUploadRecipe recipe in listRecipe)
                {
                    this.fpNeedUpload_Sheet1.Rows.Add(rowIndex, 1);

                    this.fpNeedUpload_Sheet1.Cells[rowIndex, 0].Value = true;  //默认选上
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, 1].Value = Function.GetServiceName(recipe.ServiceType);  //类别
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, 2].Value = recipe.InvoiceNO;  //发票号
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, 3].Value = recipe.PatientName;  //姓名
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, 4].Value = recipe.IdNO;   //证件号码
                    this.fpNeedUpload_Sheet1.Cells[rowIndex, 5].Value = recipe.MedicalCardNO;   //医保编号
                    string GSType = "";
                    if (recipe.GSType == "0")
                    {
                        GSType = "医疗";
                    }
                    else if (recipe.GSType == "1")// {E99E6DC2-581D-4f32-B00A-51B5F144897E}
                    {
                        GSType = "工伤";
                    }

                    this.fpNeedUpload_Sheet1.Cells[rowIndex, 6].Value = GSType;   //工伤标志

                    this.fpNeedUpload_Sheet1.Rows[rowIndex].Tag = recipe;

                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        private void UploadRecipe()
        {

            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                if (this.Login() < 0)
                {
                    MessageBox.Show("登录失败!", "错误");
                    return;
                }
            }
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

            for (int k = 0; k < this.fpNeedUpload_Sheet1.Rows.Count; k++)
            {
                bool isChoose = NConvert.ToBoolean(this.fpNeedUpload_Sheet1.Cells[k, 0].Value);
                if (isChoose)
                {
                    Model.NeedUploadRecipe recipe = this.fpNeedUpload_Sheet1.Rows[k].Tag as Model.NeedUploadRecipe;
                    if (recipe != null && !string.IsNullOrEmpty(recipe.InvoiceNO))
                    {
                        this.UploadRecipeByInvoiceNO(recipe);
                    }
                }
            }

            ////测试
            //Model.NeedUploadRecipe r = new FoShanSporadicUpload.Model.NeedUploadRecipe();
            //r.IdNO = "440682198604145047";
            //r.MedicalCardNO = "440682198604145047";
            //r.PatientName = "陈淑冰";
            //r.ServiceType = "1";
            //r.InvoiceNO = "160626280028";
            //this.UploadRecipeByInvoiceNO(r);

        }

        /// <summary>
        /// 上传处方信息
        /// </summary>
        /// <param name="recipe"></param>
        private void UploadRecipeByInvoiceNO(Model.NeedUploadRecipe recipe)
        {
            if (recipe == null || string.IsNullOrEmpty(recipe.InvoiceNO))
            {
                return;
            }
            
           decimal totCost1 = 0m;
            //交易号
            string transNO = Function.UploadRecipeTransNO;

            if (recipe.ServiceType == "1")
            {
                #region 门诊

                ArrayList balanceList = this.outFeeMgr.QueryBalances(recipe.InvoiceNO);
                if (balanceList == null )
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，未找到对应的结算信息1!");
                    return;
                }
                else if (balanceList.Count <= 0)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，未找到对应的结算信息2!");
                    return;
                }
                if (balanceList.Count > 1)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，已经退费!");
                    return;
                }
                FS.HISFC.Models.Fee.Outpatient.Balance balance = balanceList[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
                if (balance == null)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，未找到对应的结算信息3!");
                    return;
                }
                //判读患者信息是否一致
                if (balance.Patient.Name != recipe.PatientName)
                {
                    MessageBox.Show("本地结算信息【" + balance.Patient.Name + "】与社保结算信息【" + recipe.PatientName + "】不一致!");
                    return;
                }

                //发票对应的明细信息
                ArrayList alFee = this.outFeeMgr.QueryFeeItemByInvoiceNO(balance.Patient.ID, balance.Invoice.ID);
                if (alFee == null || alFee.Count <= 0)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】未找到对应的处方信息!");
                    return;
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

                    //上传-最多能上传100条
                    string strRows = string.Empty;
                    for (int k = 0; k < alFee.Count; k++)
                    {
                        feeItem = alFee[k] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                        string errMsg = string.Empty;

                        //大类-字典fldm
                        string statCode = string.Empty; //大类代码
                        string statName = string.Empty; //大类名称
                        FS.HISFC.Models.Base.Const objFee = this.fldmHelper.GetObjectFromID(feeItem.Item.MinFee.ID) as FS.HISFC.Models.Base.Const;
                        if (objFee != null && !string.IsNullOrEmpty(objFee.ID))
                        {
                            statCode = objFee.UserCode;
                            statName = objFee.Memo;
                        }
                        else
                        {
                            //找不到默认为其它
                            statCode = "B";
                            statCode = "其它";
                        }
                        

                        //项目编码   ??gmz-中心码 或 本地码?? 
                        string itemCode = string.Empty;

                        string itemName = feeItem.Item.Name;
                        if (itemName.Length > 20)
                        {
                            itemName = itemName.Substring(0, 20);   //长度限制，只能截取前20位
                        }

                        if (this.itemCodeCompareType == "0")
                        {
                            #region 国标码处理

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                if (this.DealFeeItemList(feeItem, ref errMsg) == -1)
                                {
                                    MessageBox.Show(errMsg);
                                    return;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                return;
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
                                    return;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(自定义码为空)，请先进行维护！");
                                return;
                            }

                            itemCode = feeItem.Item.UserCode;

                            #endregion
                        }
                        //项目总量，单价，总额，收费日期
                        string itemQty = feeItem.Item.Qty.ToString();
                        string itemPrice = System.Math.Round(feeItem.Item.PackQty == 0 ? feeItem.Item.Price : feeItem.Item.Price / feeItem.Item.PackQty, 4).ToString();
                        string itemCost = (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost).ToString();// feeItem.FT.TotCost.ToString();// 
                        totCost1 += (feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost);//feeItem.FT.TotCost;
                        DateTime feeDate = feeItem.FeeOper.OperTime;
                        //处方医生
                        string doctName = this.GetEmpoyeeName(feeItem.RecipeOper.ID, ref errMsg);
                        //科室名称 字典bqdm
                        string deptName = string.Empty;
                        FS.HISFC.Models.Base.Const objDept = this.bqdmHelper.GetObjectFromID(feeItem.RecipeOper.Dept.ID) as FS.HISFC.Models.Base.Const;
                        if (objDept != null && !string.IsNullOrEmpty(objDept.ID))
                        {
                            deptName = objDept.Memo;
                        }
                        //唯一标识，保证同一单号里面不重复就可以了，使用顺序号。  //RECIPE_NO, SEQUENCE_NO, TRANS_TYPE, MO_ORDER, INVOICE_SEQ 【简化为 SEQUENCE_NO, TRANS_TYPE, MO_ORDER】
                        string ykc610 = k.ToString();                             //feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString() + "-" + feeItem.Order.ID; //feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString() + "-" + feeItem.Order.ID + "-" + feeItem.InvoiceCombNO;

                        string strFeeItem = string.Format(Function.UploadRecipeDetailXML, ykc610,
                                                          statCode, statName, itemCode, itemName,
                                                          "", "0", "", "",
                                                          itemQty, itemPrice, itemCost, "",
                                                          feeItem.Item.Specs.Replace('&',' '), feeItem.Item.PriceUnit, "", "",
                                                          feeDate.ToString("yyyy-MM-dd"), feeDate.ToString("yyyy-MM-dd"), doctName, deptName,
                                                          feeDate.ToString("yyyy-MM-dd"));
                        strRows += "\r\n" + strFeeItem;

                        if ((k + 1) % 100 == 0 || (k + 1) == alFee.Count)
                        {
                            string inXML = string.Format(Function.UploadRecipeHeadXML, Function.HospitalCode, this.userID, this.seesionID,
                                                         recipe.IdNO, recipe.MedicalCardNO, recipe.ServiceType,
                                                         clinicCode, "", recipe.InvoiceNO,
                                                         totCost.ToString(), strRows);

                            Model.ResultHead result = this.siBizMgr.UploadRecipe(transNO, inXML);
                            strRows = string.Empty;

                            if (result.Code == "1")
                            {
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("门诊发票号【" + recipe.InvoiceNO + "】上传信息失败!\r\n" + result.Message);
                                return;
                            }
                        }


                    }

                }

                MessageBox.Show("门诊发票号【" + recipe.InvoiceNO + "】上传信息成功!上传总金额为： " + totCost1.ToString());

                return;

                #endregion
            }
            else
            {
                #region 住院

                ArrayList balanceList = this.inFeeMgr.QueryBalances(recipe.InvoiceNO);
                if (balanceList == null)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，未找到对应的结算信息!");
                    return;
                }
                else if (balanceList.Count <= 0)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，未找到对应的结算信息!");
                    return;
                }
                if (balanceList.Count > 1)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，已经退费!");
                    return;
                }
                FS.HISFC.Models.Fee.Inpatient.Balance balance = balanceList[0] as FS.HISFC.Models.Fee.Inpatient.Balance;
                if (balance == null)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】，未找到对应的结算信息!");
                    return;
                }
                //判读患者信息是否一致
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntegrate.GetPatientInfomation(balance.Patient.ID);
                if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
                {
                    MessageBox.Show("未找到【" + recipe.PatientName + "】的住院信息!");
                    return;
                }
                if (patientInfo.Name != recipe.PatientName)
                {
                    MessageBox.Show("本地结算信息【" + patientInfo.Name + "】与社保结算信息【" + recipe.PatientName + "】不一致!");
                    return;
                }

                //住院总费用
                ArrayList alFee = new ArrayList();

                //药品
                ArrayList alMedFee = this.inFeeMgr.QueryMedItemListsByInvoiceNO(balance.Patient.ID, balance.Invoice.ID);
                if (alMedFee == null)
                {
                    alMedFee = new ArrayList();
                }
                alFee.AddRange(alMedFee);
                //非药品
                ArrayList alUndrugFee = this.inFeeMgr.QueryFeeItemListsByInvoiceNO(balance.Patient.ID, balance.Invoice.ID);
                if (alUndrugFee == null)
                {
                    alUndrugFee = new ArrayList();
                }
                alFee.AddRange(alUndrugFee);

                if (alFee == null || alFee.Count <= 0)
                {
                    MessageBox.Show("发票号【" + recipe.InvoiceNO + "】未找到对应的处方信息!");
                    return;
                }

                #region 工伤标志的上传工伤的明细

                if (recipe.GSType == "1")
                {
                    if (this.GSUploadFeeDetail(patientInfo, ref alFee) < 0)
                    {
                        //MessageBox.Show("住院发票号【" + recipe.InvoiceNO + "】上传工伤明细信息失败！\r\n" + this.errMsg);
                        DialogResult result = MessageBox.Show("住院发票号【" + recipe.InvoiceNO + "】上传工伤明细信息失败！\r\n" + this.errMsg +"，是否继续？", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }
                            
                         
                     
                }

                #endregion
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = alFee[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (feeItem != null)
                {
                    string inpatientNO = feeItem.ID;   //住院流水号，作为处方号
                    decimal totCost = 0m;              //发票总金额
                    string inTimes = patientInfo.InTimes.ToString();  //住院次数

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

                    int l = 1;

                    //上传-最多能上传100条
                    string strRows = string.Empty;
                    int index = 0;
                    int indexCount = 0;
                    for (int k = 0; k < feeGatherlsClone.Count; k++)
                    {
                        index++;
                        indexCount++;
                        feeItem = feeGatherlsClone[k] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                        if (feeItem.FT.TotCost == 0)
                        {
                            continue;
                        }
                        string errMsg = string.Empty;

                        //大类-字典fldm
                        string statCode = string.Empty; //大类代码
                        string statName = string.Empty; //大类名称
                        FS.HISFC.Models.Base.Const objFee = this.fldmHelper.GetObjectFromID(feeItem.Item.MinFee.ID) as FS.HISFC.Models.Base.Const;
                        if (objFee != null && !string.IsNullOrEmpty(objFee.ID))
                        {
                            statCode = objFee.UserCode;
                            statName = objFee.Memo;
                        }
                        else
                        {
                            //找不到默认为其它
                            statCode = "B";
                            statCode = "其它";
                        }

                        //项目编码   ??gmz-中心码 或 本地码?? 
                        string itemCode = string.Empty;

                        string itemName = feeItem.Item.Name;
                        if (itemName.Length > 20)
                        {
                            itemName = itemName.Substring(0, 20);   //长度限制，只能截取前20位
                        }

                        if (this.itemCodeCompareType == "0")
                        {
                            #region 国标码处理

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                if (this.DealFeeItemList(feeItem, ref errMsg) == -1)
                                {
                                    MessageBox.Show(errMsg);
                                    return;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.GBCode) || feeItem.Item.GBCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(国标码为空)，请先进行维护！");
                                return;
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
                                    return;
                                }
                            }

                            if (string.IsNullOrEmpty(feeItem.Item.UserCode) || feeItem.Item.UserCode == "0")
                            {
                                MessageBox.Show("项目【" + feeItem.Item.Name + "】没有维护医保对照码(自定义码为空)，请先进行维护！");
                                return;
                            }

                            itemCode = feeItem.Item.UserCode;

                            #endregion
                        }
                        //项目总量，单价，总额，收费日期
                        string itemQty = feeItem.Item.Qty.ToString();
                        string itemPrice = System.Math.Round(feeItem.Item.PackQty == 0 ? feeItem.Item.Price : feeItem.Item.Price / feeItem.Item.PackQty, 4).ToString();
                        string itemCost = feeItem.FT.TotCost.ToString();//(feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost).ToString();
                        totCost1 += feeItem.FT.TotCost;
                        DateTime feeDate = balance.BalanceOper.OperTime;      //住院应为结算日期
                        DateTime chargeDate = feeItem.ChargeOper.OperTime;
                        //处方医生
                        string doctName = this.GetEmpoyeeName(feeItem.RecipeOper.ID, ref errMsg);
                        //科室名称 字典bqdm
                        string deptName = string.Empty;
                        FS.HISFC.Models.Base.Const objDept = this.bqdmHelper.GetObjectFromID(feeItem.RecipeOper.Dept.ID) as FS.HISFC.Models.Base.Const;
                        if (objDept != null && !string.IsNullOrEmpty(objDept.ID))
                        {
                            deptName = objDept.Memo;
                        }
                        //唯一标识，保证同一单号里面不重复就可以了，使用顺序号。
                        string ykc610 = l.ToString();// feeItem.RecipeNO + "-" + feeItem.SequenceNO.ToString() + "-" + ((int)feeItem.TransType).ToString();//RECIPE_NO, TRANS_TYPE, SEQUENCE_NO
                        l++;

                        if (string.IsNullOrEmpty(feeItem.Item.Specs))
                        {
                            feeItem.Item.Specs = "";
                        }
                        string strFeeItem = "";
                        if (recipe.GSType != "1")
                        {
                            strFeeItem = string.Format(Function.UploadRecipeDetailXML, ykc610,
                                                              statCode, statName, itemCode, itemName,
                                                              "", "0", "", "",
                                                              itemQty, itemPrice, itemCost, "",
                                                              feeItem.Item.Specs.Replace('&', ' '), feeItem.Item.PriceUnit, "", "",
                                                              feeDate.ToString("yyyy-MM-dd"), feeDate.ToString("yyyy-MM-dd"), doctName, deptName,
                                                              feeDate.ToString());
                        }
                        else
                        {
                            strFeeItem = string.Format(Function.UploadRecipeDetailXML, ykc610,
                                                              statCode, statName, itemCode, itemName,
                                                              "", "0", "", "",
                                                              itemQty, itemPrice, itemCost, "",
                                                              feeItem.Item.Specs.Replace('&', ' '), feeItem.Item.PriceUnit, "", "",
                                                              feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd"), feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd"), doctName, deptName,
                                                              feeItem.ChargeOper.OperTime);
                        }
                        strRows += "\r\n" + strFeeItem;

                        if (indexCount == 100 || index == feeGatherlsClone.Count)
                        {
                            indexCount = 0;
                            string inXML = string.Format(Function.UploadRecipeHeadXML, Function.HospitalCode, this.userID, this.seesionID,
                                                         recipe.IdNO, recipe.MedicalCardNO, recipe.ServiceType,
                                                         inpatientNO, inTimes, recipe.InvoiceNO,
                                                         totCost.ToString(), strRows);

                            Model.ResultHead result = this.siBizMgr.UploadRecipe(transNO, inXML);
                            strRows = string.Empty;

                            if (result.Code == "1")
                            {
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("住院发票号【" + recipe.InvoiceNO + "】上传信息失败!\r\n 明细总金额：" +totCost1.ToString()+"，发票金额："+totCost.ToString() +" "+ result.Message);
                                return; 
                            }
                        }

                    }
                }
                MessageBox.Show("住院发票号【" + recipe.InvoiceNO + "】上传信息成功,共金额：" + totCost1.ToString());

                return;
                #endregion
            }
        }

        /// <summary>
        /// 已上传处方查询
        /// </summary>
        private void QueryHaveUploadedRecipe()
        {
            //清屏
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;

            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                if (this.Login() < 0)
                {
                    MessageBox.Show("登录失败!", "错误");
                    return;
                }
            }
            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                MessageBox.Show("请先登录成功!", "提示");
                return;
            }
            //证件号码
            string idNO = this.txtIdNO.Text.Trim();
            if (string.IsNullOrEmpty(idNO))
            {
                MessageBox.Show("请输入证件号码!", "提示");
                this.txtIdNO.Focus();
                return;
            }
            //医保编号
            string mcCardNO = this.txtMcCardNO.Text.Trim();
            if (string.IsNullOrEmpty(mcCardNO))
            {
                MessageBox.Show("请输入医保编号!", "提示");
                this.txtMcCardNO.Focus();
                return;
            }
            //发票号
            string invoiceNO = this.txtInvoiceNO.Text.Trim();
            if (string.IsNullOrEmpty(invoiceNO))
            {
                MessageBox.Show("请输入发票号!", "提示");
                this.txtInvoiceNO.Focus();
                return;
            }

            //时间段
            string strBeginTime = "";// this.dtBeginTime.Value.ToString("yyyy-MM-dd");
            string strEndTime = "";// this.dtEndTime.Value.ToString("yyyy-MM-dd");

            string transNO = Function.HaveUploadedRecipeTransNO;
            string inXML = string.Format(Function.HaveUploadedRecipeXML, Function.HospitalCode, this.userID, this.seesionID,
                                                                        idNO, mcCardNO, invoiceNO,
                                                                        strBeginTime, strEndTime);

            List<Model.HaveUploadedRecipe> listHaveUploadedRecipe = new List<FoShanSporadicUpload.Model.HaveUploadedRecipe>();
            Model.ResultHead result = this.siBizMgr.QueryHaveUploadedRecipe(transNO, inXML, ref listHaveUploadedRecipe);

            if (result.Code == "1")
            {
                #region 显示在界面

                if (listHaveUploadedRecipe != null && listHaveUploadedRecipe.Count > 0)
                {
                    this.ShowHaveUploadedRecipe(listHaveUploadedRecipe);
                }

                #endregion

                //查询成功
                MessageBox.Show(result.Message + "\r\n查询成功!");
            }
            else
            {
                //查询失败
                MessageBox.Show(result.Message);
            }

        }

        /// <summary>
        /// 显示已上传的处方
        /// </summary>
        /// <param name="listHaveUploadedRecipe"></param>
        private void ShowHaveUploadedRecipe(List<Model.HaveUploadedRecipe> listHaveUploadedRecipe)
        {
            this.fpHaveUploaded_Sheet1.Rows.Count = 0;
            Dictionary<string, string> dictInvoiceRecipe = new Dictionary<string, string>();

            if (listHaveUploadedRecipe != null && listHaveUploadedRecipe.Count > 0)
            {
                int rowIndex = this.fpHaveUploaded_Sheet1.Rows.Count;
                foreach (Model.HaveUploadedRecipe recipe in listHaveUploadedRecipe)
                {
                    //显示发票记录
                    if (dictInvoiceRecipe.ContainsKey(recipe.InvoiceNO))
                    {
                        continue;
                    }

                    this.fpHaveUploaded_Sheet1.Rows.Add(rowIndex, 1);

                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, 0].Value = true;  //默认选上
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, 1].Value = Function.GetServiceName(recipe.ServiceType);  //类别
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, 2].Value = recipe.InvoiceNO;  //发票号

                    //姓名
                    string patientName = string.Empty;
                    if (recipe.ServiceType == "1")
                    {
                        #region 门诊

                        string cardNo = this.GetCardNo(recipe.PatientID);
                        if (!string.IsNullOrEmpty(cardNo))
                        {
                            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.manager.QueryComPatientInfo(cardNo);
                            if (patientInfo == null)
                            {
                                MessageBox.Show("未找到【" + recipe.PatientID + "】的住院信息!");
                            }
                            else
                            {
                                patientName = patientInfo.Name;
                            }

                        }
                        #endregion
                    }
                    else
                    {
                        #region 住院

                        FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntegrate.GetPatientInfomation(recipe.PatientID);
                        if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
                        {
                            MessageBox.Show("未找到【" + recipe.PatientID + "】的住院信息!");
                        }
                        else
                        {
                            patientName = patientInfo.Name;
                        }


                        #endregion
                    }
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, 3].Value = patientName; 


                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, 4].Value = recipe.IdNO;   //证件号码
                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, 5].Value = recipe.MedicalCardNO;   //医保编号
                    string GSType = "";
                    if (recipe.GSType == "0")
                    {
                        GSType = "医疗";
                    }
                    else if (recipe.GSType == "1")
                    {
                        GSType = "工伤";
                    }

                    this.fpHaveUploaded_Sheet1.Cells[rowIndex, 6].Value = GSType;   //工伤标志
                    this.fpHaveUploaded_Sheet1.Columns.Get(6).Width = 0F;
                    this.fpHaveUploaded_Sheet1.Rows[rowIndex].Tag = recipe;

                    //显示发票记录
                    dictInvoiceRecipe.Add(recipe.InvoiceNO, recipe.InvoiceNO);

                    rowIndex++;
                }
            }
        }

        private int GSUploadFeeDetail(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //this.gsSiBizProcess = new FoShanGSSI.Management.SIBizProcess();

            //if (this.medicalProcess.UploadFeeDetailsInpatient(patient, ref feeDetails) < 0)
            //{
            //    this.errMsg = this.medicalProcess.ErrMsg;
            //    return -1;
            //}
            //else
            //{
            //    this.errMsg = "";
            //}

            return 1;
        }

        public string GetCardNo(string clinicNo)
        {
            if (string.IsNullOrEmpty(clinicNo))
                return "";
            string sql = "select r.card_no from fin_opr_register r where r.clinic_code = '{0}'";
            sql = string.Format(sql, clinicNo);
            string str = outFeeMgr.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                return "";
            }
            return str;
        }
        /// <summary>
        /// 回退处方
        /// </summary>
        private void CancelUploadRecipe()
        {
            //判断
            if (string.IsNullOrEmpty(this.seesionID) || string.IsNullOrEmpty(this.userID) || string.IsNullOrEmpty(this.userPw))
            {
                MessageBox.Show("请先登录成功!", "错误");
                return;
            }
            if (this.fpHaveUploaded_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("无需要回退的处方信息!");
                return;
            }

            for (int k = 0; k < this.fpHaveUploaded_Sheet1.Rows.Count; k++)
            {
                bool isChoose = NConvert.ToBoolean(this.fpHaveUploaded_Sheet1.Cells[k, 0].Value);
                if (isChoose)
                {
                    Model.HaveUploadedRecipe recipe = this.fpHaveUploaded_Sheet1.Rows[k].Tag as Model.HaveUploadedRecipe;
                    if (recipe != null && !string.IsNullOrEmpty(recipe.PatientID))
                    {
                        this.CancelUploadRecipeByInvoiceNO(recipe);
                    }
                }
            }
        }

        /// <summary>
        /// 回退处方信息
        /// </summary>
        /// <param name="recipe"></param>
        private void CancelUploadRecipeByInvoiceNO(Model.HaveUploadedRecipe recipe)
        {
            if (recipe == null || string.IsNullOrEmpty(recipe.PatientID))
            {
                return;
            }
            //交易号
            string transNO = Function.CancelUploadRecipeTransNO;
            if (recipe.ServiceType == "1")
            {
                #region 门诊

                string inXML = string.Format(Function.CancelUploadRecipeXML, Function.HospitalCode, this.userID, this.seesionID,
                                                                            recipe.IdNO, recipe.MedicalCardNO, recipe.PatientID,
                                                                            "", "", recipe.InvoiceNO);

                Model.ResultHead result = this.siBizMgr.CancelUploadRecipe(transNO, inXML);

                if (result.Code == "1")
                {
                    MessageBox.Show("门诊发票号【" + recipe.InvoiceNO + "】回退信息成功!\r\n" + result.Message);
                    return;
                }
                else
                {
                    MessageBox.Show("门诊发票号【" + recipe.InvoiceNO + "】回退信息失败!\r\n" + result.Message);
                    return;
                }

                #endregion
            }
            else
            {
                #region 住院

                //判读患者信息是否一致
                FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntegrate.GetPatientInfomation(recipe.PatientID);
                if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
                {
                    MessageBox.Show("未找到【" + recipe.PatientID + "】的住院信息!");
                    return;
                }

                string inTimes = patientInfo.InTimes.ToString();  //住院次数

                string inXML = string.Format(Function.CancelUploadRecipeXML, Function.HospitalCode, this.userID, this.seesionID,
                                                                            recipe.IdNO, recipe.MedicalCardNO, recipe.PatientID,
                                                                            inTimes, "", recipe.InvoiceNO);

                Model.ResultHead result = this.siBizMgr.CancelUploadRecipe(transNO, inXML);

                if (result.Code == "1")
                {
                    MessageBox.Show("住院发票号【" + recipe.InvoiceNO + "】回退信息成功!\r\n" + result.Message);
                    return;
                }
                else
                {
                    MessageBox.Show("住院发票号【" + recipe.InvoiceNO + "】回退信息失败!\r\n" + result.Message);
                    return;
                }


                

                #endregion
            }

        }

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

            this.toolBarService.AddToolButton("待上传处方", "待上传处方查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.toolBarService.AddToolButton("上传", "上传处方", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            this.toolBarService.AddToolButton("已上传处方", "已上传处方查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询历史, true, false, null);
            this.toolBarService.AddToolButton("回退", "回退处方", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

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

                case "待上传处方":
                    this.QueryNeedUploadRecipe();
                    break;
                case "上传":
                    this.UploadRecipe();
                    break;

                case "已上传处方":
                    this.QueryHaveUploadedRecipe();
                    break;
                case "回退":
                    this.CancelUploadRecipe();
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



                #endregion
            }
            else if (this.tabMain.SelectedTab == this.tpUploaded)
            {
                #region 已上传

                #endregion
            }
        }

        #endregion

    }


}
