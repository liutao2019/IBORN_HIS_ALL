using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;


///<summary>
///桥头医院门诊收费外接屏 
///</summary>


namespace FS.SOC.Local.OutpatientFee.QiaoTou
{
    public partial class frmChargeMultiScreen : Form, FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen
    {
        public frmChargeMultiScreen()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 项目信息
        /// </summary>
        private DataSet dsItem = null;

        /// <summary>
        /// 费用待遇接口
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = null;

        /// <summary>
        /// 属于药品的最小费用列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region 属性

        /// <summary>
        /// 属于药品的最小费用列表
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper DrugFeeCodeHelper
        {
            set
            {
                this.drugFeeCodeHelper = value;
            }
        }

        /// <summary>
        /// 设置待遇接口变量
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy
        {
            set
            {
                this.medcareInterfaceProxy = value;
            }
        }
        private bool isPreeFee = false;
        ///
        /// <summary>
        /// 医保患者是否预结算
        /// </summary>
        public bool IsPreFee
        {
            set
            {
                this.isPreeFee = value;
            }
            get
            {
                return this.isPreeFee;
            }
        }


        #endregion

        #region 方法

        /// <summary>
        /// 设置属于药品的最小费用列表
        /// </summary>
        /// <param name="drugFeeCodeHelper">属于药品的最小费用列表</param>
        public void SetFeeCodeIsDrugArrayListObj(FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper)
        {
            this.drugFeeCodeHelper = drugFeeCodeHelper;
        }

        /// <summary>
        /// 设置待遇接口变量
        /// </summary>
        /// <param name="medcareInterfaceProxy">接口变量</param>
        public void SetMedcareInterfaceProxy(FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy)
        {
            this.medcareInterfaceProxy = medcareInterfaceProxy;
        }

        /// <summary>
        /// 设置显示信息
        /// </summary>
        /// <param name="patient">挂号基本信息</param>
        /// <param name="ft">计算后的费用分项信息</param>
        /// <param name="feeItemLists">费用明细基本信息</param>
        /// <param name="diagLists">诊断信息</param>
        /// <param name="otherInfomations">其他信息</param>
        public void SetInfomation(FS.HISFC.Models.Registration.Register patient, FS.HISFC.Models.Base.FT ft, ArrayList feeItemLists, ArrayList diagLists, string[] otherinformation)
        {
            if (otherinformation[0].ToString() != "0" && otherinformation[0].ToString() != "1" && otherinformation[0].ToString() != "2" && otherinformation[0].ToString() != "3" && otherinformation[0].ToString() != "4")
            {
                this.neuPanel4.Visible = false;
                this.neuLabel3.Visible = true;
                this.neuLabel3.Text = otherinformation[0].ToString() + "为您服务";
                this.neuLabel4.Visible = true;
            }
            else
            {
                this.neuLabel3.Visible = false;
                this.neuLabel4.Visible = false;
                this.neuPanel4.Visible = true;
                this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;

                if (this.medcareInterfaceProxy == null)
                {
                    this.medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
                }

                if (this.medcareInterfaceProxy == null)
                {
                    return;
                }

                if (patient == null)
                {
                    this.lblPaientInfo.Text = "";
                    return;
                }

                this.medcareInterfaceProxy.SetPactCode(patient.Pact.ID);
                // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
                this.medcareInterfaceProxy.IsLocalProcess = false;

                if (feeItemLists == null)// || feeItemLists.Count <= 0)
                {
                    if (ft != null)
                    {
                        if (patient.Pact.PayKind.ID == "01" || patient.Pact.PayKind.ID == "03")
                        {
                            this.tbDrugSendInfo.Text = ft.User01;
                            if (string.IsNullOrEmpty(tbDrugSendInfo.Text))
                            {
                                tbDrugSendInfo.Text = "";//"取药药房";
                            }
                            else
                            {
                                tbDrugSendInfo.Text = "请到  " + tbDrugSendInfo + "  取药";
                            }
                        }
                        this.tbRealOwnCost.Text = ft.RealCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbReturnCost.Text = ft.ReturnCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                    }
                    this.lblPaientInfo.Text = patient.Name + "  正在办理划价"+"\n"+"请稍后";
                }

                decimal sumTotCost = 0, sumPayCost = 0, sumPubCost = 0, sumOwnCost = 0; decimal sumDrugCost = 0;

                if (feeItemLists != null)
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeItemLists)
                    {
                        if (f.IsAccounted)
                        {
                            if (f.FT.OwnCost > 0)
                            {
                                sumPayCost += f.FT.OwnCost;
                                sumOwnCost += 0;
                            }
                            else
                            {
                                sumPayCost += f.FT.PayCost;
                            }

                        }
                        else
                        {
                            sumTotCost += f.FT.TotCost;
                            sumPayCost += f.FT.PayCost;
                            //{C623A693-19A7-4378-859D-5C07CFF9BEB1}
                            sumPubCost += f.FT.PubCost + f.FT.RebateCost;
                            sumOwnCost += f.FT.OwnCost - f.FT.RebateCost;
                        }
                        if (this.drugFeeCodeHelper.ArrayObject != null && this.drugFeeCodeHelper.ArrayObject.Count > 0 && this.drugFeeCodeHelper.GetObjectFromID(f.Item.MinFee.ID) != null)
                        {
                            sumDrugCost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;
                        }
                    }

                    if (patient.Pact.PayKind.ID == "01")
                    {
                        this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbPayCost.Text = sumPayCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                        this.tbPubCost.Text = sumPubCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbDrugCost.Text = sumDrugCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                        this.lblPayTitle.Visible = false;
                        this.tbPayCost.Visible = false;
                        this.lblPubTitle.Visible = false;
                        this.tbPubCost.Visible = false;
                    }
                    else if (patient.Pact.PayKind.ID == "03")
                    {
                        this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbPayCost.Text = FS.FrameWork.Public.String.FormatNumber(sumPayCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbPubCost.Text = FS.FrameWork.Public.String.FormatNumber(sumPubCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbDrugCost.Text = sumDrugCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                        this.lblPayTitle.Visible = false;
                        this.tbPayCost.Visible = false;
                        this.lblPubTitle.Visible = true;
                        this.tbPubCost.Visible = true;
                        this.lblPubTitle.Text = "公费：";
                    }
                    else if (patient.Pact.PayKind.ID == "02")
                    {
                        this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.OwnCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.TotCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbPayCost.Text = "0.00";
                        this.tbPubCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PubCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.tbDrugCost.Text = sumDrugCost.ToString("F4").TrimEnd('0').TrimEnd('.');

                        this.lblPayTitle.Visible = true;
                        this.tbPayCost.Visible = true;
                        this.lblPubTitle.Visible = true;
                        this.tbPubCost.Visible = true;
                        this.lblPubTitle.Text = "医保：";
                    }
                }

                if (ft != null)
                {
                    this.tbDrugSendInfo.Text = ft.User01;

                    if (string.IsNullOrEmpty(tbDrugSendInfo.Text))
                    {
                        //tbDrugSendInfo.Text = "取药药房";
                        tbDrugSendInfo.Text = "";//不需要取药的不显示取药药房
                    }
                    else
                    {
                        tbDrugSendInfo.Text = "请到 " + tbDrugSendInfo.Text + " 取药";
                    }

                    if (ft.RealCost == 0)
                    {
                        ft.RealCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbOwnCost.Text);
                    }

                    this.tbRealOwnCost.Text = ft.RealCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.tbReturnCost.Text = ft.ReturnCost.ToString("F4").TrimEnd('0').TrimEnd('.');
                    Decimal ownpay = FS.FrameWork.Function.NConvert.ToDecimal(this.tbOwnCost.Text) + FS.FrameWork.Function.NConvert.ToDecimal(this.tbPayCost.Text);
                    this.tbPayCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PayCost, 2).ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.lblPaientInfo.Text = patient.Name + "  应缴金额为 "+"\n" + ownpay.ToString("F4").TrimEnd('0').TrimEnd('.') + " 元";
                }

            }

        }
        #endregion

        /// <summary>
        /// 设置项目信息
        /// </summary>
        /// <param name="dsItem">项目信息集合</param>
        public void SetDataSet(DataSet dsItem)
        {
            this.dsItem = dsItem;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public int Init()
        {
            if (this.drugFeeCodeHelper != null && (this.drugFeeCodeHelper.ArrayObject == null || this.drugFeeCodeHelper.ArrayObject.Count == 0))
            {
                ArrayList drugFeeCodeList = this.managerIntegrate.GetConstantList("DrugMinFee");
                if (drugFeeCodeList == null)
                {
                    MessageBox.Show(Language.Msg("获得药品最小费用列表出错!") + this.managerIntegrate.Err);

                    return -1;
                }

                this.drugFeeCodeHelper.ArrayObject = drugFeeCodeList;
            }

            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            this.tbDrugCost.Text = "0.00";
            this.tbDrugSendInfo.Text = "";
            this.tbRealOwnCost.Text = "0.00";
            this.tbReturnCost.Text = "0.00";
            this.tbOwnCost.Text = "0.00";
            this.tbTotCost.Text = "0.00";
            this.tbPayCost.Text = "0.00";
            this.tbPubCost.Text = "0.00";
            this.tbDrugCost.Text = "0.00"; 
        }

        #region IMultiScreen 成员

        public System.Collections.Generic.List<Object> ListInfo
        {
            get
            {
                return null;
            }
            set
            {
                this.Clear();
                if (value != null)
                {
                    this.SetInfomation(
                   value[0] as FS.HISFC.Models.Registration.Register
                        , value[1] as FS.HISFC.Models.Base.FT
                            , value[2] as ArrayList
                    , value[3] as ArrayList, value[4] as string[]
                    );
                }
                else
                {
                    this.lblPaientInfo.Text = "";
                }
            }
        }

        public int ShowScreen()
        {
            this.Clear();

            if (Screen.AllScreens.Length > 1)
            {
                this.Show();
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            return 0;
        }

        #endregion

        #region IMultiScreen 成员

        public int CloseScreen()
        {
             
            this.ScreenInvisible();
            return 0;
        }

        public void ScreenInvisible()
        {
            this.Visible = false;
        }
        #endregion

       public void frmChargeMultiScreen_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.neuPanel3.BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + "\\Setting\\backgroundimage\\门诊收费背景.jpg");
            }
            catch
            {
            }
        }
    }
}
