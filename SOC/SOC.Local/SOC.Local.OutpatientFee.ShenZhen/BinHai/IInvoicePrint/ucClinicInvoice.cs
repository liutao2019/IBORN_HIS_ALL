using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IInvoicePrint
{
    public partial class ucClinicInvoice : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucClinicInvoice()
        {
            InitializeComponent();
        }


        #region 变量

        private bool _isPreView;//是否预览
        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        //显示公医特殊
        string SpecialDisPlay = "";
        //公医特殊比例显示
        string TSNewRate = string.Empty;
        /// <summary>
        /// diffrent NewRate For GYTS
        /// </summary>
        ArrayList alTSNewRate = new ArrayList();
        /// <summary>
        /// has Find
        /// </summary>
        bool hsFind = false;

        private string _printer;


        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 控制参数
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        FS.HISFC.BizLogic.Registration.Register regA = new FS.HISFC.BizLogic.Registration.Register();
        FS.HISFC.Models.Registration.Register regInfoA = new FS.HISFC.Models.Registration.Register();
        ArrayList alRegList=new ArrayList ();
        #endregion

        #region 属性

        public string Printer
        {
            get
            {
                return this._printer;
            }
            set
            {
                this._printer = value;
            }
        }

        #endregion

        #region 函数
       public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList alInvoiceDetail, ArrayList alFeeItemList, ArrayList alPayModes, bool isPreview)
        {
            //如果费用明细为空，则返回
            if (alFeeItemList.Count <= 0)
            {
                return -1;
            }

            lblPriDateOut.Text = "";
            lblPriDateIn.Text = "";
            lblHosName.Text = FS.FrameWork.Management.Connection.Hospital.Name;

            ///ZFPha : 自费药品
            ///ZFItem: 自费非药品
            ///CBPha: 超标药品
            decimal ZFPha = 0m, CBPha = 0m, ZFItem = 0m;

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFeeItemList)
            {
                if (item.Item.MinFee.ID == "001" || item.Item.MinFee.ID == "002" || item.Item.MinFee.ID == "003")
                {
                    if (item.ItemRateFlag == "1")
                    {
                        ZFPha += FS.FrameWork.Function.NConvert.ToDecimal(item.FT.DrugOwnCost);
                    }
                    CBPha += FS.FrameWork.Function.NConvert.ToDecimal(item.FT.ExcessCost);
                }
                else
                {
                    ZFItem += item.FT.OwnCost;
                }
                //zhangq [add TS ItemNewRateList]
                if (alTSNewRate.Count == 0)
                {
                    FS.FrameWork.Models.NeuObject objN = new FS.FrameWork.Models.NeuObject();
                    if (item.NewItemRate < FS.FrameWork.Function.NConvert.ToDecimal("0.1"))
                    {
                        objN.ID = (item.NewItemRate * 100).ToString("0") + "%";
                    }
                    else
                    {
                        objN.ID = (item.NewItemRate * 100).ToString("00") + "%";
                    }
                    TSNewRate += objN.ID + ",";
                    alTSNewRate.Add(objN);
                }
                else
                {
                    hsFind = false;
                    foreach (FS.FrameWork.Models.NeuObject obj in alTSNewRate)
                    {
                        string tempTSRate = obj.ID.Trim('%');
                        decimal hsSameRate = FS.FrameWork.Function.NConvert.ToDecimal(tempTSRate) / 100;
                        if (hsSameRate == item.NewItemRate)
                        {
                            hsFind = true;
                            break;
                        }
                    }

                    if (!hsFind)
                    {
                        FS.FrameWork.Models.NeuObject objN = new FS.FrameWork.Models.NeuObject();
                        if (item.NewItemRate < FS.FrameWork.Function.NConvert.ToDecimal("0.1"))
                        {
                            objN.ID = (item.NewItemRate * 100).ToString("0") + "%";
                        }
                        else
                        {
                            objN.ID = (item.NewItemRate * 100).ToString("00") + "%";
                        }
                        TSNewRate += objN.ID + ",";
                        alTSNewRate.Add(objN);
                    }
                }
                //this.label5.Text = item.RecipeOper.ID;
            }

            //其它药 = 自费药+超标药
            if (ZFPha + CBPha > 0)
            {
                //this.lblPriCost30.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ZFPha + CBPha, 2);
            }

            #region 支付方式打印
            this.lblEcoCost.Text = string.Empty;
            foreach (FS.HISFC.Models.Fee.BalancePayBase payObj in alPayModes)
            {
                if (payObj.FT.TotCost > 0)
                {
                    this.lblEcoCost.Text += string.Format("{0}：{1}", payObj.PayType.Name, payObj.FT.TotCost.ToString("F2"));
                }
            }

            #endregion

            //就诊卡号
            this.lblPriPatientNo.Text = regInfo.PID.CardNO.TrimStart('0');

            //清空控件边框
            if (!isPreview)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Substring(0, 3) == "lbl")
                    {
                        System.Windows.Forms.Label lblControl = null;
                        lblControl = (System.Windows.Forms.Label)c;
                        lblControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                        //c.Visible = isPreview;
                    }
                }
            }
            //控制根据打印和预览显示选项

            if (isPreview)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Length > 6)
                    {
                        if (c.Name.Substring(0, 6) == "lblPre")
                            c.Visible = isPreview;
                    }
                }
            }
            else
            {
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Length > 6)
                    {

                        if (c.Name.Substring(0, 6) == "lblPre")
                            c.Visible = isPreview;
                        //c.Visible = true;// 测试用 
                    }
                }
            }

            FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
            FS.HISFC.BizLogic.Fee.Outpatient myOutPatient = new FS.HISFC.BizLogic.Fee.Outpatient();
            if (this.trans != null)
            {
                dbManager.SetTrans(this.trans.Trans);
                myOutPatient.SetTrans(this.trans.Trans);
            }

            DateTime dtNow = dbManager.GetDateTimeFromSysDateTime();

            string minute = "";
            if (invoice.PrintTime.Minute > 9)
            {
                minute = invoice.PrintTime.Minute.ToString();
            }
            else
            {
                minute = "0" + invoice.PrintTime.Minute.ToString();
            }
            //基本信息
            if (isPreview)
            {

                this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();  //年
                this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();//月
                this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();
                this.lblPriOper.Text = invoice.BalanceOper.ID;//结算操作员

            }
            else
            {
                this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();  //年
                this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();//月
                this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();    //日
                string docName = regInfo.DoctorInfo.Templet.Doct.ID;
                if (docName == "")
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList f = alFeeItemList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                    if (f != null && f.RecipeOper != null)
                    {
                        this.lblPriOper.Text = invoice.BalanceOper.ID;//结算操作员
                    }
                }
                else
                {
                    this.lblPriOper.Text = invoice.BalanceOper.ID;//结算操作员
                }
            }
            string seeNo = "";
            if (string.IsNullOrEmpty(regInfo.DoctorInfo.Templet.Dept.ID)
                && ((FS.HISFC.Models.Registration.Register)((FS.HISFC.Models.Fee.Outpatient.FeeItemList)alFeeItemList[0]).Patient).DoctorInfo.Templet.Dept.ID.ToString()=="3110"
                )
            {
                this.alRegList = this.regA.QueryPatient(((FS.HISFC.Models.Fee.Outpatient.FeeItemList)alFeeItemList[0]).Patient.ID);
                this.regInfoA = (FS.HISFC.Models.Registration.Register)alRegList[0];
                seeNo = regInfoA.DoctorInfo.Templet.Dept.ID.ToString() == "3110" ? regInfoA.DoctorInfo.SeeNO.ToString() : "";
            }
            else
            {
                seeNo = regInfo.DoctorInfo.Templet.Dept.ID.ToString() == "3110" ? regInfo.DoctorInfo.SeeNO.ToString() : "";
            }
            if (!string.IsNullOrEmpty(seeNo))
            {
                this.lblSeeNo.Text = seeNo;
                this.lblSeeNo.BorderStyle = BorderStyle.FixedSingle;
                this.lblSeeNo.Visible = true;
                Label lbTmp = new Label();
                lbTmp.Location = new Point(this.lblSeeNo.Location.X - 1, this.lblSeeNo.Location.Y - 1);
                lbTmp.Size = new Size(this.lblSeeNo.Width + 2, this.lblSeeNo.Height + 2);
                lbTmp.BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(lbTmp);
                lbTmp.SendToBack();
                this.lblSeeNo.BringToFront();
            }
            else
            {
                this.lblSeeNo.Visible = false;
            }

            this.lblInvoice.Text = invoice.Invoice.ID;//发票号
            this.lblRealInvoiceNo.Text = invoice.PrintedInvoiceNO; // 实际发票号
            if (regInfo.SSN != "")
            {
                if (regInfo.SIMainInfo.MedicalType.ID == "7")
                    this.lblPriName.Text = regInfo.Name;
                else
                    this.lblPriName.Text = regInfo.Name + "(" + regInfo.SSN + ")";
            }
            else
            {
                this.lblPriName.Text = regInfo.Name;//姓名
            }

            #region 如果超过20个字节，增宽

            int nameHeight = this.lblPriName.Height;
            if (this.lblPriName.Text.Length > 10)
            {
                this.lblPriName.Height = 45;
            }

            #endregion

            string tempRate = "";

            //if (regInfo.Pact.ID == "3" && regInfo.Pact.ID == "YTS")
            //{
            //    FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            //    if (this.trans != null)
            //    {
            //        regMgr.SetTrans(this.trans.Trans);
            //    }
            //    FS.HISFC.Models.Registration.Register temp = regMgr.GetByClinic(regInfo.ID);
            //    if (temp != null)
            //    {
            //        if (temp.Pact.PayKind.ID == "02")
            //        {
            //            if (regInfo.Pact.ID == "2")
            //            {
            //                tempRate = "【" + "普通医保" + "】";
            //            }
            //            else if (regInfo.SIMainInfo.PersonType.ToString() == "2")
            //            {
            //                tempRate = "【" + "生育医疗" + "】";
            //            }
            //        }
            //        else
            //        {
            //            if (regInfo.SIMainInfo.PersonType.ToString() == "3")
            //            {
            //                tempRate = "离休医疗";
            //            }
            //            else if (regInfo.SIMainInfo.PersonType.ToString() == "4")
            //            {
            //                tempRate = "家属统筹医疗";
            //            }
            //        }
            //    }
            //    TSNewRate = TSNewRate.TrimEnd(',');
            //    SpecialDisPlay = "(" + TSNewRate + ")";
            //}
            //else
            //{
            tempRate = regInfo.Pact.Name;
            SpecialDisPlay = string.Empty;
            //}
            if (regInfo.Pact.PayKind.ID == "03")
            {
                if (invoice.Memo == "1")
                {
                    //tempRate += "100%";
                }
                if (invoice.Memo == "5")
                {
                    tempRate += "";
                }
                if (invoice.Memo == "2")
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeItemList)
                    {
                        if (f.Item.SpecialFlag3 == "2")
                        {
                            tempRate += (f.NewItemRate * 100).ToString() + "%";
                            break;
                        }
                    }
                }
                if (invoice.Memo == "3")
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeItemList)
                    {
                        if (f.Item.SpecialFlag3 == "3")
                        {
                            tempRate += (f.NewItemRate * 100).ToString() + "%";
                            break;
                        }
                    }
                }
            }

            ArrayList al;
            try
            {
                al = myOutPatient.QueryBalancePaysByInvoiceSequence(invoice.CombNO);
                if (al == null)
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
            FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();
            //helper.ArrayObject = FS.HISFC.Object.Fee.EnumPayTypeService.List();
            helper.ArrayObject = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (helper.ArrayObject == null)
            {
                return -1;
            }

            string strPayMode = "";//支付方式

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.BalancePay payMode = al[i] as FS.HISFC.Models.Fee.Outpatient.BalancePay;

                strPayMode += " " + helper.GetObjectFromID(payMode.PayType.ID.ToString()).Name;// +" ";//+FS.NFC.Public.String.FormatNumber(payMode.Cost,2);//结算操作员
            }

            string payKind = "";
            if (this.setPayModeType == "1")
            {
                payKind = this.splitInvoicePayMode;
            }
            else
            {
                payKind = tempRate;// +strPayMode + SpecialDisPlay;
            }

            if (payKind.Length > 8)
            {
                Font f = new Font("宋体", 8);
                lblPriPayKind.Font = f;
                payKind.Insert(8, "\n");
            }
            lblPriPayKind.Text = payKind;

            //[2010-03-02] zhaozf 添加，如果是自费，则不显示“自费”两个字，方便病人报销
            lblPriPayKind.Text = lblPriPayKind.Text.Replace("自费", "");

            //lblPriSwDrugWindow.Text = invoice.User01;//发药药房

            //decimal CTFee = 0m, MRIFee = 0m, SXFee = 0m, SYFee = 0m, PETFee = 0m; ;
            ////治疗总费用
            //decimal ZLTotFee = 0m;
            ////CT总费用 
            //decimal CTTotFee = 0m;

            //将费用名称的Text值清空.gmz(2011-07-29)
            this.SetFeeNameTextInvinsible();

            decimal[] feeDetail = new decimal[25];
            string[] feeNameList = new string[25];
            //票面信息
            for (int i = 0; i < alInvoiceDetail.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
                detail = (FS.HISFC.Models.Fee.Outpatient.BalanceList)alInvoiceDetail[i];

                //显示费用名称
                #region
                /*if (isPreview)
                {
                    System.Windows.Forms.Label lblFeeName;
                    if (detail.InvoiceSquence < 1 || detail.InvoiceSquence > 24)
                    {
                        continue;
                    }
                    lblFeeName = (System.Windows.Forms.Label)this.GetFeeNameLable(detail.InvoiceSquence);
                    if (lblFeeName == null)
                    {
                        MessageBox.Show("没有找到费用大类为" + detail.FeeCodeStat.Name + "的打印序号!");
                        return -1;
                    }
                    try
                    {
                        feeNameList[detail.InvoiceSquence] = detail.FeeCodeStat.Name;
                        lblFeeName.Text = detail.FeeCodeStat.Name;
                        lblFeeName.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return -1;
                    }
                }*/
                #endregion

                //费用金额赋值
                System.Windows.Forms.Label lblFeeCost = null;
                lblFeeCost = (System.Windows.Forms.Label)this.GetFeeCostLable(detail.InvoiceSquence);

                #region 费用名称赋值 gmz(2011-07-29)

                System.Windows.Forms.Label lblFeeName = null;
                lblFeeName = (System.Windows.Forms.Label)this.GetFeeNameLable(detail.InvoiceSquence);
                if (lblFeeName == null)
                {
                    MessageBox.Show("没有找到费用大类为" + detail.FeeCodeStat.Name + "的打印序号!");
                    return -1;
                }

                try
                {
                    feeNameList[detail.InvoiceSquence] = detail.FeeCodeStat.Name;
                    lblFeeName.Text = detail.FeeCodeStat.Name;
                    lblFeeName.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }

                #endregion

                if (lblFeeCost == null)
                {
                    MessageBox.Show("没有找到费用大类为" + detail.FeeCodeStat.Name + "的打印序号!");
                    return -1;
                }

                if (regInfo.Pact.PayKind.ID != "03")
                {
                    feeDetail[detail.InvoiceSquence] = detail.BalanceBase.FT.TotCost;
                    lblFeeCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(detail.BalanceBase.FT.TotCost, 2);
                }

            }



            //zhangq
            /// 没有处理的发票应受金额
            /// 去除减免
            decimal NoDealOwnPay = invoice.FT.OwnCost + invoice.FT.PayCost - invoice.FT.RebateCost;

            #region 处理后的发票应受金额
            // {46FCAD38-D9AB-46c6-9D8A-7A6453315E3F}

            ///处理后的发票应受金额
            //decimal DealOwnPay = FS.FrameWork.Public.String.FormatNumber(NoDealOwnPay, 1);

            decimal DealOwnPay = NoDealOwnPay; // 按实际金额处理

            #endregion

            #region 取医保金额
            if (regInfo.Pact.ID == "2" && regInfo.Pact.User03 == "SIReprint")  
            {
                regInfo.SIMainInfo.User01 = null;
                regInfo.SIMainInfo.User02 = null;
                regInfo.SIPerson.User02 = null;
                regInfo.SIMainInfo.User03 = null;


                string[,] strblZFmx = this.GetSIItemDetail(invoice.CanceledInvoiceNO);
                for (int i = 0; i < strblZFmx.Length / strblZFmx.Rank; i++)
                {

                    if (strblZFmx[i, 0] == "01")  // 
                    {
                        regInfo.SIMainInfo.OwnCost = NConvert.ToDecimal(strblZFmx[i, 1]);
                        if (regInfo.SIMainInfo.User01 == null)
                        {
                            regInfo.SIMainInfo.User01 = invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[i, 1];
                        }
                        else
                        {
                            regInfo.SIMainInfo.User01 = regInfo.SIMainInfo.User01 + "|" + invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[i, 1];

                        }
                    }

                    if (strblZFmx[i, 0] == "0201")
                    {
                        regInfo.SIMainInfo.PayCost = NConvert.ToDecimal(strblZFmx[i, 1]);
                        if (regInfo.SIMainInfo.User02 == null)
                        {
                            regInfo.SIMainInfo.User02 = invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[i, 1];

                        }
                        else
                        {
                            regInfo.SIMainInfo.User02 = regInfo.SIMainInfo.User02 + "|" + invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[i, 1];


                        }
                    }
                    if (strblZFmx[i, 0] == "03")
                        regInfo.SIMainInfo.TotCost = NConvert.ToDecimal(strblZFmx[i, 1]);
                    if (strblZFmx[i, 0] == "02")
                    {
                        if (regInfo.SIPerson.User02 == null)  //
                        {
                            regInfo.SIPerson.User02 = invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[i, 0] + "," + strblZFmx[i, 1];
                        }
                        else
                        {
                            regInfo.SIPerson.User02 = regInfo.SIPerson.User02 + "|" + invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[0, i] + "," + strblZFmx[1, i];

                        }

                    }
                    if (strblZFmx[i, 0] == "0303" || strblZFmx[i, 0] == "0306")
                    {
                        if (regInfo.SIMainInfo.User03 == null)
                            regInfo.SIMainInfo.User03 = invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[i, 0] + "," + strblZFmx[i, 1];
                        else
                            regInfo.SIMainInfo.User03 = regInfo.SIMainInfo.User03 + "|" + invoice.CanceledInvoiceNO.ToString() + "," + strblZFmx[i, 0] + "," + strblZFmx[i, 1];
                    }



                }
                regInfo.SIMainInfo.PubCost = regInfo.SIMainInfo.TotCost - regInfo.SIMainInfo.OwnCost - regInfo.SIMainInfo.PayCost;


            }

            if (regInfo.Pact.PayKind.ID == "02")
            {

                if (regInfo.SIMainInfo.User03 == null)
                {
                    this.lblYbBalance.Visible = true;//结算前金额
                    this.lblYbBalanced.Visible = true;//结算后金额
                    //string YbBalance = "0 元";
                    //string YbBalanced = "0 元";
                }
                else
                {
                    string[] tempMZZF = regInfo.SIMainInfo.User03.Split('|');
                    for (int m = 0; m < tempMZZF.Length; m++)
                    {
                        string[] tempstr = tempMZZF[m].Split(',');
                        for (int count = 0; count < tempstr.Length; count++)
                        {
                            if (tempstr[count] == invoice.Invoice.ID || tempstr[count] == invoice.CanceledInvoiceNO)
                            {
                                try
                                {    //chenxin 补打发票时无法获取医保金额
                                    if (tempstr[1] == "0303" && tempstr[2] != "0")
                                    {
                                        this.lblYbBalance.Text += tempstr[2] + "元";////医保结算前金额
                                        lblYbBalance.Visible = true; //2012-10-17
                                    }
                                    else
                                        lblYbBalance.Visible = true;
                                    if (tempstr[1] == "0306" && tempstr[2] != "0")
                                    {
                                        this.lblYbBalanced.Text += tempstr[2] + "元";//医保结算后金额
                                        lblYbBalanced.Visible = true; //2012-10-17
                                    }
                                    else
                                        lblYbBalanced.Visible = true;
                                }
                                catch
                                {
                                    lblYbBalanced.Visible = false;
                                }

                            }
                        }

                    }
                }

                try
                {
                    lblPriCost35.Text = "医保个人自付：" + FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[5], 2);
                    lblPriCost36.Text = "医保补记金额：" + FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[6], 2);
                }
                catch
                { }
            }
            if (regInfo.Pact.PayKind.ID == "02")
            {
                this.lblPriCost36.Text = "现金个人自付：" + invoice.FT.OwnCost.ToString();
                this.lblPriCost35.Text = "医保个人金额：" + invoice.FT.PayCost.ToString();
                this.lblPriCost35.Visible = true;
                this.lblPriCost36.Visible = true;

            }
            else
            {
                if (NConvert.ToDecimal(this.lblPriCost35.Text.Trim()) > 0)
                {
                    this.lblPriCost35.Visible = true;
                }
                else
                {
                    this.lblPriCost35.Visible = false;
                }
                this.lblPriCost36.Visible = false;
            }

            if (isPreview)
            {
                this.lblPriCost35.Visible = true;
                this.lblPriCost36.Visible = true;
            }
            #endregion
            ///四舍五入金额
            //decimal DealedCost = DealOwnPay - NoDealOwnPay;
            //this.lblPriCost7.Text = DealedCost.ToString();//FS.NFC.Public.String.FormatNumberReturnString(DealedCost,2);
            //if (FS.NFC.Function.NConvert.ToDecimal(this.lblPriCost7.Text.Trim()) != 0 || isPreview)//[2010-4-14]zhaozf修改：预览时标签全显示
            //{
            //    this.lblPreFeeName7.Visible = true;
            //    this.lblPriCost7.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName7.Visible = false;
            //    this.lblPriCost7.Visible = false;
            //}

            if (regInfo.Pact.PayKind.ID == "03")//公费  || regInfo.Pact.PayKind.ID == "02"
            {
                //this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2) +
                //                      " 公医合计:" + FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost + invoice.FT.PayCost, 2);//医保/公医记账

                this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2);

                //根据合同单位获取自付比例
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                pactMgr.SetTrans(this.trans.Trans);
                FS.HISFC.Models.Base.PactInfo pactObj = pactMgr.GetPactUnitInfoByPactCode(regInfo.Pact.ID);
                if (pactObj != null && pactObj.Rate.PayRate > 0)
                {
                    string priPay = FS.FrameWork.Public.String.FormatNumberReturnString(DealOwnPay, 2) + " (" +
                                    FS.FrameWork.Public.String.FormatNumberReturnString(pactObj.Rate.PayRate * 100, 0).Replace(".", "") + "%:" +
                                    FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PayCost, 2) + ")";
                    if (priPay.Length > 15)
                    {
                        priPay.Insert(15, "\n");
                        Font f = new Font("宋体", 8);
                        this.lblPriPay.Font = f;
                    }
                    this.lblPriPay.Text = priPay;
                    //this.lblPriPay.Text
                }
                else
                {
                    this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(DealOwnPay, 2);
                }
            }
            else if (regInfo.Pact.PayKind.ID == "02")//增加医保
            {
                if (regInfo.SIPerson.User02 != null)
                {
                    string[] tempzfstr = regInfo.SIPerson.User02.Split('|');
                    for (int m = 0; m < tempzfstr.Length; m++)
                    {
                        string[] tempstr = tempzfstr[m].Split(',');
                        for (int count = 0; count < tempstr.Length; count++)
                        {
                            if (tempstr[count] == invoice.Invoice.ID || tempstr[count] == invoice.CanceledInvoiceNO) //发票重打
                            {
                                try
                                {
                                    this.lblPriPub.Text = tempstr[2];
                                    this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost - NConvert.ToDecimal(tempstr[2]), 2);

                                }
                                catch
                                {
                                    this.lblPriPub.Visible = false;
                                    this.lblPriPay.Visible = false;
                                }
                            }
                        }
                    }
                }

                if (regInfo.SIMainInfo.User01 != null)// || regInfo.SIMainInfo.User01 != "")
                {
                    string[] tempstr1 = regInfo.SIMainInfo.User01.Split('|');
                    for (int m = 0; m < tempstr1.Length; m++)
                    {
                        string[] tempstrr = tempstr1[m].Split(',');
                        for (int count = 0; count < tempstrr.Length; count++)
                        {
                            if (tempstrr[count] == invoice.Invoice.ID || tempstrr[count] == invoice.CanceledInvoiceNO)
                            {
                                try
                                {
                                    this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost - NConvert.ToDecimal(tempstrr[1]), 2);
                                    this.lblPriPay.Text = tempstrr[1];// "现金 " +
                                }
                                catch
                                {
                                    this.lblPriPub.Visible = false;
                                    this.lblPriPay.Visible = false;
                                }
                            }
                        }
                    }
                }
                this.lblSimType.Visible = false;
                this.lblSimMedType.Visible = true;
                if (regInfo.SIMainInfo.PersonType.ID == "2")
                {
                    this.lblSimType.Visible = true;
                    this.lblSimType.Text = "生育医疗";
                }
                if (regInfo.SIMainInfo.MedicalType.ID == "1")
                {
                    //this.lblSimMedType.Text = "普通门诊";
                    this.lblSimMedType.Visible = false;
                }
                else if (regInfo.SIMainInfo.MedicalType.ID == "2")
                {
                    this.lblSimMedType.Text = "特病门诊";
                }
                else if (regInfo.SIMainInfo.MedicalType.ID == "3")
                {
                    this.lblSimMedType.Text = "特检门诊";
                }
                else if (regInfo.SIMainInfo.MedicalType.ID == "4")
                {
                    this.lblSimMedType.Text = "病种门诊";
                }
                else if (regInfo.SIMainInfo.MedicalType.ID == "5")
                {
                    this.lblSimMedType.Text = "健康体检";
                }
                else if (regInfo.SIMainInfo.MedicalType.ID == "6")
                {
                    this.lblSimMedType.Text = "预防接种";
                }
                else if (regInfo.SIMainInfo.MedicalType.ID == "7")
                {
                    this.lblSimMedType.Text = "家庭通道";
                    this.lbsiJZP.Visible = true;
                    this.lbsiJZP.Text = "记账人:" + regInfo.SIPerson.Name + "(" + regInfo.SSN + ")";
                }
            }
            else
            {
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(NoDealOwnPay, 2);
                lblSimType.Visible = false;
                lblSimMedType.Visible = false;
            }

            //显示药房领药窗口
            try
            {
                lblSendDrug.Text = "";
                if (invoice.DrugWindowsNO != null && invoice.DrugWindowsNO != string.Empty && !isPreview)
                {
                    string[] drugWindow = invoice.DrugWindowsNO.Split('|');
                    Hashtable hsDrugWindow = new Hashtable();
                    string disPlayWindow = string.Empty;
                    for (int x = 0; x < drugWindow.Length; x++)
                    {
                        if (hsDrugWindow.ContainsValue(drugWindow[x]))
                        {
                            //disPlayWindow = /*disPlayWindow + "，" + */ drugWindow[x].ToString();
                        }
                        else
                        {
                            hsDrugWindow.Add(x, drugWindow[x].ToString());
                            disPlayWindow += /*disPlayWindow + "，" + */drugWindow[x].ToString() + "，";
                        }
                    }
                    disPlayWindow = disPlayWindow.TrimEnd(new char[] { '，' });

                    lblSendDrug.Text = disPlayWindow;
                }
            }
            catch
            { }
            this.lblPriLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);

            string[] strMoney = new string[8];

            strMoney = this.GetUpperCashbyNumber(FS.FrameWork.Public.String.FormatNumber(invoice.FT.TotCost, 2));

            this.lblPriF.Text = strMoney[0];
            this.lblPriJ.Text = strMoney[1];
            this.lblPriY.Text = strMoney[3];
            this.lblPriS.Text = strMoney[4];
            this.lblPriB.Text = strMoney[5];
            this.lblPriQ.Text = strMoney[6];
            this.lblPriW.Text = strMoney[7];
            this.lblPriSW.Text = strMoney[8];

            //就诊日期
            //重新获取 挂号实体
            FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();
            regInfo = registerManager.GetByClinic(regInfo.ID);
            if (regInfo != null)
            {
                this.lblPriDateIn.Text = regInfo.DoctorInfo.SeeDate.ToString("yyyy-MM-dd");
                //预约病人打印时间段
                if (regInfo.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                {
                    this.label12.Text += regInfo.DoctorInfo.Templet.Begin.ToString("HH：mm") + "--" + regInfo.DoctorInfo.Templet.End.ToString("HH：mm");
                }
            }

            if (!isPreview)
            {
                this.Print();
            }

            #region  打完之后修改回控件的大小

            if (this.lblPriName.Height != nameHeight)
            {
                this.lblPriName.Height = nameHeight;
            }

            #endregion

            return 0;
        }

        /// <summary>
        /// 获取结算信息支付项目
        /// </summary>
        /// <param name="InpatientNO">发票号</param>
        /// <returns>详细信息</returns>
        private string[,] GetSIItemDetail(string invoiceNO)
        {
            string strSql = "select t.zfxm, t.je from si_yb_mzzf t where t.fphm = '" + invoiceNO + "' and t.trantype ='1'";


            DataSet dsItem = new DataSet();
            FS.HISFC.BizLogic.RADT.OutPatient Manager = new FS.HISFC.BizLogic.RADT.OutPatient();
            Manager.ExecQuery(strSql, ref dsItem);

            string[,] itemDetail = new string[0, 2];

            if (dsItem.Tables[0].Rows.Count > 0)
            {
                itemDetail = new string[dsItem.Tables[0].Rows.Count, 2];

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    itemDetail[i, 0] = dsItem.Tables[0].Rows[i][0].ToString();
                    itemDetail[i, 1] = dsItem.Tables[0].Rows[i][1].ToString();
                }
            }
            return itemDetail;
        }


        /// <summary>
        /// 获得费用名称输入框
        /// </summary>
        /// <param name="i">序号</param>
        /// <returns>费用名称输入框控件</returns>
        private Control GetFeeNameLable(int i)
        {

            foreach (Control c in this.Controls)
            {
                //以前是：lblPreFee.(2011-07-29)
                if (c.Name == "lblPriFeeName" + i.ToString())
                {
                    c.Visible = true;
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// 费用名称输入框的Text值清空.gmz(2011-07-29)
        /// </summary>
        private void SetFeeNameTextInvinsible()
        {
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.Name.Length > 13 && c.Name.Substring(0, 13) == "lblPriFeeName")
                {
                    c.Visible = false;
                }
            }
        }

        /// <summary>
        /// 获得费用金额输入框
        /// </summary>
        /// <param name="i">序号</param>
        /// <returns>费用金额输入框控件</returns>
        private Control GetFeeCostLable(int i)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "lblPriCost" + i.ToString())
                {
                    c.Visible = true;
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// 发票只打印大写数字 打印到十万
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        private string[] GetUpperCashbyNumber(decimal Cash)
        {
            string[] sNumber = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] sReturn = new string[9];
            string strCash = null;
            //填充位数
            int iLen = 0;
            strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
            if (strCash.Length > 9)
            {
                strCash = strCash.Substring(strCash.Length - 9);
            }

            //填充位数
            iLen = 9 - strCash.Length;
            for (int j = 0; j < iLen; j++)
            {
                int k = 0;
                k = 8 - j;
                sReturn[k] = "零";
            }
            for (int i = 0; i < strCash.Length; i++)
            {
                string Temp = null;

                Temp = strCash.Substring(strCash.Length - 1 - i, 1);

                if (Temp == ".")
                {
                    continue;
                }
                sReturn[i] = sNumber[int.Parse(Temp)];
            }
            return sReturn;
        }




        public string InvoiceType
        {
            get { return "MZ01"; }
        }

        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return new FS.HISFC.Models.Registration.Register();
            }
        }

        #endregion

        #region IInvoicePrint 成员

        public bool IsPreView
        {
            set
            {
                _isPreView = value;
            }
        }

        public string Description
        {
            get
            {
                // TODO:  添加 ucInvoiceGY.Description getter 实现
                return "门诊发票打印";
            }
        }

        public void SetPreView(bool isPreView)
        {
            _isPreView = isPreView;
            lblIn.Visible = !isPreView;
        }

        public int Print()
        {
            try
            {
                //FS.NFC.Interface.Classes.Print print = null; 
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化打印机失败!" + ex.Message);
                    return -1;
                }
                if (this.trans == null)
                {
                    MessageBox.Show("没有设置数据库连接!");
                    return -1;
                }

                // 打印纸张设置
                FS.HISFC.Models.Base.PageSize ps = null;
                ps = psManager.GetPageSize("MZFP");
                if (ps == null)
                {
                    ps = new FS.HISFC.Models.Base.PageSize("MZFP", 787, 400);
                    ps.Top = 0;
                    ps.Left = 0;

                    ps.Printer = this.controlIntegrate.GetControlParam<string>("MZFP", true, "");
                }
                print.SetPageSize(ps);

                if (_isPreView)
                {
                    print.PrintDocument.PrinterSettings.PrinterName = "MZFEEDETAILPRINTER";
                }

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                print.PrintPage(ps.Left, ps.Top, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            return 0;
        }


        #endregion

        #region IInvoicePrint 成员

        public void SetTrans(FS.FrameWork.Management.Transaction t)
        {
            this.trans = t;
        }

        public FS.FrameWork.Management.Transaction Trans
        {
            set
            {
                this.trans = value;
            }
        }

        #endregion

        #region IInvoicePrint 成员


        public int PrintOtherInfomation()
        {
            return 0;
        }

        #endregion

        #region IInvoicePrint 成员

        private string setPayModeType = "";
        private string splitInvoicePayMode = "";

        #endregion

        #region IInvoicePrint 成员

        public string SetPayModeType
        {
            set
            {
                this.setPayModeType = value;
            }
        }

        bool FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.IsPreView
        {
            set { }
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.Print()
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.PrintOtherInfomation()
        {
            return 1;
        }

        FS.HISFC.Models.Registration.Register FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.Register
        {
            set { }
        }

        string FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SetPayModeType
        {
            set { }
        }

        void FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SetPreView(bool isPreView)
        {

        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, ArrayList Invoices, ArrayList invoiceDetails, ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList alPayModes, bool isPreview)
        {
            this.SetPrintValue(regInfo, invoice, invoiceDetails, feeDetails, alPayModes, isPreview);
            return 1;
        }

        void FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SetTrans(IDbTransaction trans)
        {

        }

        string FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SplitInvoicePayMode
        {
            set { }
        }


        public void SetTrans(IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public string SplitInvoicePayMode
        {
            set
            {
                this.splitInvoicePayMode = value;
            }
        }

        IDbTransaction FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.Trans
        {
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint);
                return type;
            }
        }
        #endregion


        #region IInvoicePrint 成员


        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, bool isPreview)
        {
            throw new NotImplementedException();
        }

        #endregion
    
}
}
