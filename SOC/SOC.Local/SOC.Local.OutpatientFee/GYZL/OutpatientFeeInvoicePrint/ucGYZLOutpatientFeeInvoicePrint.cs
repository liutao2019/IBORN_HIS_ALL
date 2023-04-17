using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.GYZL.OutpatientFeeInvoicePrint
{                                                         
    public partial class ucGYZLOutpatientFeeInvoicePrint : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucGYZLOutpatientFeeInvoicePrint()
        {
            InitializeComponent();
        }

        #region ����

        private bool _isPreView;//�Ƿ�Ԥ��
        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();
        //private FS.NFC.Management.Transaction trans = new FS.NFC.Management.Transaction();
        //ucFeeDetail otherPrint = new ucFeeDetail();
        //��ʾ��ҽ����
        string SpecialDisPlay = "";
        //��ҽ���������ʾ
        string TSNewRate = string.Empty;
        /// <summary>
        /// diffrent NewRate For GYTS
        /// </summary>
        ArrayList alTSNewRate = new ArrayList();
        /// <summary>
        /// has Find
        /// </summary>
        bool hsFind = false;

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private string _printer;
        #endregion

        #region ����
        public string Printer
        {
            get { return this._printer; }
            set { this._printer = value; }
        }
        #endregion

        #region ����

        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList alInvoiceDetail, ArrayList alFeeItemList, bool isPreview)
        {
            #region ����
            //////��Ϊ������������п���Ϊ�� ����ж��õ�
            //���������ϸΪ�գ��򷵻�
            //if (alFeeItemList.Count <= 0)
            //{
            //    return -1;
            //}

            //lblPriDateOut.Text = "";
            //lblPriDateIn.Text = "";

            //#region �����Ų���Ҫ��
            ////������ҩ��ȡҩ��
            //////��ҩ������
            ////string ZYRecipeNo = string.Empty;
            //////��ҩ������
            ////string XYRecipeNo = string.Empty;
            //////��ӡ������
            ////string printRecipeNo = string.Empty;

            ////foreach (FS.HISFC.Object.Fee.Outpatient.FeeItemList item in alFeeItemList)
            ////{
            ////    //if (ZYRecipeNo != string.Empty && XYRecipeNo != string.Empty)
            ////    //{
            ////    //    printRecipeNo = XYRecipeNo + "," + ZYRecipeNo;
            ////    //    break;
            ////    //}
            ////    //��ҩ������ѡ��
            ////    if (item.Item.MinFee.ID == "001")
            ////    {
            ////        if (item.RecipeNO != string.Empty && XYRecipeNo == string.Empty)
            ////        {
            ////            XYRecipeNo = item.RecipeNO;
            ////            continue;
            ////        }
            ////    }
            ////    else if (item.Item.MinFee.ID == "002" || item.Item.MinFee.ID == "003")
            ////    {
            ////        if (item.RecipeNO != string.Empty && ZYRecipeNo == string.Empty)
            ////        {
            ////            ZYRecipeNo = item.RecipeNO;
            ////            continue;
            ////        }
            ////    }
            ////}

            ////if (XYRecipeNo != string.Empty && printRecipeNo == string.Empty)
            ////{
            ////    printRecipeNo = XYRecipeNo;
            ////    if (ZYRecipeNo != string.Empty)
            ////    {
            ////        printRecipeNo += "," + ZYRecipeNo;
            ////    }
            ////}
            //#endregion

            /////ZFPha : �Է�ҩƷ
            /////ZFItem: �Էѷ�ҩƷ
            /////CBPha: ����ҩƷ
            //decimal ZFPha = 0m, CBPha = 0m, ZFItem = 0m;

            //foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFeeItemList)
            //{
            //    if (item.Item.MinFee.ID == "001" || item.Item.MinFee.ID == "002" || item.Item.MinFee.ID == "003")
            //    {
            //        if (item.ItemRateFlag == "1")
            //        {
            //            ZFPha += FS.FrameWork.Function.NConvert.ToDecimal(item.FT.DrugOwnCost);
            //        }
            //        CBPha += FS.FrameWork.Function.NConvert.ToDecimal(item.FT.ExcessCost);
            //    }
            //    else
            //    {
            //        ZFItem += item.FT.OwnCost;
            //    }
            //    //zhangq [add TS ItemNewRateList]
            //    if (alTSNewRate.Count == 0)
            //    {
            //        FS.FrameWork.Models.NeuObject objN = new FS.FrameWork.Models.NeuObject();
            //        if (item.NewItemRate < FS.FrameWork.Function.NConvert.ToDecimal("0.1"))
            //        {
            //            objN.ID = (item.NewItemRate * 100).ToString("0") + "%";
            //        }
            //        else
            //        {
            //            objN.ID = (item.NewItemRate * 100).ToString("00") + "%";
            //        }
            //        TSNewRate += objN.ID + ",";
            //        alTSNewRate.Add(objN);
            //    }
            //    else
            //    {
            //        hsFind = false;
            //        foreach (FS.FrameWork.Models.NeuObject obj in alTSNewRate)
            //        {
            //            string tempTSRate = obj.ID.Trim('%');
            //            decimal hsSameRate = FS.FrameWork.Function.NConvert.ToDecimal(tempTSRate) / 100;
            //            if (hsSameRate == item.NewItemRate)
            //            {
            //                hsFind = true;
            //                break;
            //            }
            //        }

            //        if (!hsFind)
            //        {
            //            FS.FrameWork.Models.NeuObject objN = new FS.FrameWork.Models.NeuObject();
            //            if (item.NewItemRate < FS.FrameWork.Function.NConvert.ToDecimal("0.1"))
            //            {
            //                objN.ID = (item.NewItemRate * 100).ToString("0") + "%";
            //            }
            //            else
            //            {
            //                objN.ID = (item.NewItemRate * 100).ToString("00") + "%";
            //            }
            //            TSNewRate += objN.ID + ",";
            //            alTSNewRate.Add(objN);
            //        }
            //    }
            //    this.label5.Text = item.RecipeOper.ID;
            //    //this.label5.Text = new FS.HISFC.BizLogic.Manager.Person().GetPersonByID(item.RecipeOper.ID).Name;
            //}

            ////����ҩ = �Է�ҩ+����ҩ
            //if (ZFPha + CBPha > 0)
            //{
            //    this.lblPriCost9.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ZFPha + CBPha, 2);
            //}
            ////this.lblPriCost5.Text = FS.FrameWork.Public.String.FormatNumberReturnString(CBPha, 2);
            ////�Է���
            ////if (regInfo.Pact.PayKind.ID == "03")
            ////{
            ////    this.lblPriCost24.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ZFItem, 2);
            ////}
            ////��տؼ��߿�
            //if (!isPreview)
            //{
            //    foreach (Control c in this.Controls)
            //    {
            //        if (c.Name.Substring(0, 3) == "lbl")
            //        {
            //            System.Windows.Forms.Label lblControl = null;
            //            lblControl = (System.Windows.Forms.Label)c;
            //            lblControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //            //c.Visible = isPreview;
            //        }
            //    }
            //}
            ////���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��

            //if (isPreview)
            //{
            //    foreach (Control c in this.Controls)
            //    {
            //        if (c.Name.Length > 6)
            //        {
            //            if (c.Name.Substring(0, 6) == "lblPre")
            //                c.Visible = isPreview;
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (Control c in this.Controls)
            //    {
            //        if (c.Name.Length > 6)
            //        {

            //            if (c.Name.Substring(0, 6) == "lblPre")
            //                c.Visible = isPreview;
            //                //c.Visible = true;// ������ 
            //        }
            //    }
            //}

            //FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
            //FS.HISFC.BizLogic.Fee.Outpatient myOutPatient = new FS.HISFC.BizLogic.Fee.Outpatient();
            //FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            //if (this.trans != null)
            //{
            //    dbManager.SetTrans(this.trans.Trans);
            //    myOutPatient.SetTrans(this.trans.Trans);
            //}

            //DateTime dtNow = dbManager.GetDateTimeFromSysDateTime();

            //string minute = "";
            //if (invoice.PrintTime.Minute > 9)
            //{
            //    minute = invoice.PrintTime.Minute.ToString();
            //}
            //else
            //{
            //    minute = "0" + invoice.PrintTime.Minute.ToString();
            //}
            ////������Ϣ
            //if (isPreview)
            //{

            //    this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();  //��
            //    this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();//��
            //    this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();
            //    this.lblPriOper.Text = invoice.BalanceOper.ID;//�������Ա
            //    //this.lblPriOper.Text = invoice.BalanceOper.ID + "    " + invoice.PrintTime.Hour.ToString() + ":" + minute + "  ";//�������Ա
            //    //this.lblPriOper.Text = new FS.HISFC.BizLogic.Manager.Person().GetPersonByID(invoice.BalanceOper.ID).Name + "    " + invoice.PrintTime.Hour.ToString() + ":" + minute + "  ";//�������Ա
            //    //ΪʲôҪ����ʱ�䣿
            //}
            //else
            //{
            //    this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();  //��
            //    this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();//��
            //    this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();    //��
            //    string docName = regInfo.DoctorInfo.Templet.Doct.ID;
            //    if (docName == "")
            //    {
            //        FS.HISFC.Models.Fee.Outpatient.FeeItemList f = alFeeItemList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

            //        if (f != null && f.RecipeOper != null)
            //        {
            //            this.lblPriOper.Text = invoice.BalanceOper.ID;//�������Ա

            //        }
            //    }
            //    else
            //    {
            //        this.lblPriOper.Text = invoice.BalanceOper.ID;//�������Ա

            //    }

            //}
            //this.lblInvoice.Text = invoice.Invoice.ID;//��Ʊ��
            //lblPriSwBalanceType.Text = invoice.PrintedInvoiceNO;//ʵ�ʷ�Ʊ��
            //if (regInfo.SSN != "")
            //{
            //    if (regInfo.SIMainInfo.MedicalType.ID == "7")
            //        this.lblPriName.Text = regInfo.Name;
            //    else
            //        this.lblPriName.Text = regInfo.Name + "(" + regInfo.SSN + ")";
            //}
            //else
            //{
            //    this.lblPriName.Text = regInfo.Name;//����
            //}
            //string tempRate = "";

            //if (regInfo.Pact.ID == "3" || regInfo.Pact.ID == "YTS")
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
            //                tempRate = "��" + "��ͨҽ��" + "��";
            //            }
            //            else if (regInfo.SIMainInfo.PersonType.ToString() == "2")
            //            {
            //                tempRate = "��" + "����ҽ��" + "��";
            //            }
            //        }
            //        else
            //        {
            //            if (regInfo.SIMainInfo.PersonType.ToString() == "3")
            //            {
            //                tempRate = "����ҽ��";
            //            }
            //            else if (regInfo.SIMainInfo.PersonType.ToString() == "4")
            //            {
            //                tempRate = "����ͳ��ҽ��";
            //            }
            //        }
            //    }
            //    TSNewRate = TSNewRate.TrimEnd(',');
            //    SpecialDisPlay = "(" + TSNewRate + ")";
            //}
            //else
            //{
            //    tempRate = regInfo.Pact.Name;
            //    SpecialDisPlay = string.Empty;
            //}
            //if (regInfo.Pact.PayKind.ID == "03")
            //{
            //    if (invoice.Memo == "1")
            //    {
            //        //tempRate += "100%";
            //    }
            //    if (invoice.Memo == "5")
            //    {
            //        tempRate += "";
            //    }
            //    if (invoice.Memo == "2")
            //    {
            //        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeItemList)
            //        {
            //            if (f.Item.SpecialFlag3 == "2")
            //            {
            //                tempRate += (f.NewItemRate * 100).ToString() + "%";
            //                break;
            //            }
            //        }
            //    }
            //    if (invoice.Memo == "3")
            //    {
            //        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeItemList)
            //        {
            //            if (f.Item.SpecialFlag3 == "3")
            //            {
            //                tempRate += (f.NewItemRate * 100).ToString() + "%";
            //                break;
            //            }
            //        }
            //    }
            //}

            //ArrayList al;
            //try
            //{
            //    al = myOutPatient.QueryBalancePaysByInvoiceSequence(invoice.CombNO);
            //    if (al == null)
            //    {
            //        return -1;
            //    }
            //}
            //catch
            //{
            //    return -1;
            //}
            //FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();
            ////helper.ArrayObject = FS.HISFC.Object.Fee.EnumPayTypeService.List();
            //helper.ArrayObject = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            //if (helper.ArrayObject == null)
            //{
            //    return -1;
            //}

            //string strPayMode = "";//֧����ʽ

            //for (int i = 0; i < al.Count; i++)
            //{
            //    FS.HISFC.Models.Fee.Outpatient.BalancePay payMode = al[i] as FS.HISFC.Models.Fee.Outpatient.BalancePay;

            //    strPayMode += " " + helper.GetObjectFromID(payMode.PayType.ID.ToString()).Name;// +" ";//+FS.NFC.Public.String.FormatNumber(payMode.Cost,2);//�������Ա
            //}

            //string payKind = "";
            //if (this.setPayModeType == "1")
            //{
            //    payKind = this.splitInvoicePayMode;
            //}
            //else
            //{
            //    payKind = tempRate;// +strPayMode + SpecialDisPlay;
            //}

            //if (payKind.Length > 8)
            //{
            //    Font f = new Font("����", 8);
            //    lblPriPayKind.Font  = f;
            //    payKind.Insert(8, "\n");
            //}
            //lblPriPayKind.Text = payKind;

            ////[2010-03-02] zhaozf ��ӣ�������Էѣ�����ʾ���Էѡ������֣����㲡�˱���
            //lblPriPayKind.Text = lblPriPayKind.Text.Replace("�Է�", "");

            ////lblPriSwDrugWindow.Text = invoice.User01;//��ҩҩ��

            ////decimal CTFee = 0m, MRIFee = 0m, SXFee = 0m, SYFee = 0m, PETFee = 0m; ;
            //////�����ܷ���
            ////decimal ZLTotFee = 0m;
            //////CT�ܷ��� 
            ////decimal CTTotFee = 0m;

            //decimal[] feeDetail = new decimal[25];
            //string[] feeNameList = new string[25];
            ////Ʊ����Ϣ
            //for (int i = 0; i < alInvoiceDetail.Count; i++)
            //{
            //    FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
            //    detail = (FS.HISFC.Models.Fee.Outpatient.BalanceList)alInvoiceDetail[i];
            //    //��ʾ��������
            //    if (isPreview)
            //    {
            //        //System.Windows.Forms.Label lblFeeName;
            //        if (detail.InvoiceSquence < 1 || detail.InvoiceSquence > 24)
            //        {
            //            continue;
            //        }
            //        //lblFeeName = (System.Windows.Forms.Label)this.GetFeeNameLable(detail.InvoiceSquence);
            //        //if (lblFeeName == null)
            //        //{
            //        //    MessageBox.Show("û���ҵ����ô���Ϊ" + detail.FeeCodeStat.Name + "�Ĵ�ӡ���!");
            //        //    return -1;
            //        //}
            //        try
            //        {
            //            feeNameList[detail.InvoiceSquence] = detail.FeeCodeStat.Name;
            //            //lblFeeName.Text = detail.FeeCodeStat.Name;
            //            //lblFeeName.Visible = true;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //            return -1;
            //        }
            //    }
            //    //���ý�ֵ
            //    System.Windows.Forms.Label lblFeeCost = null;
            //    lblFeeCost = (System.Windows.Forms.Label)this.GetFeeCostLable(detail.InvoiceSquence);
            //    if (lblFeeCost == null)
            //    {
            //        MessageBox.Show("û���ҵ����ô���Ϊ" + detail.FeeCodeStat.Name + "�Ĵ�ӡ���!");
            //        return -1;
            //    }

            //    if (regInfo.Pact.PayKind.ID != "03")
            //    {
            //        feeDetail[detail.InvoiceSquence] = detail.BalanceBase.FT.TotCost;
            //       // lblFeeCost.Text = FS.NFC.Public.String.FormatNumberReturnString(detail.BalanceBase.FT.TotCost, 2);
            //    }
            //    //else
            //    //{
            //    //    feeDetail[detail.InvoiceSquence] = detail.BalanceBase.FT.PayCost + detail.BalanceBase.FT.PubCost;
            //    //    //lblFeeCost.Text = FS.NFC.Public.String.FormatNumberReturnString(detail.BalanceBase.FT.PayCost + detail.BalanceBase.FT.PubCost, 2);
            //    //    detail.CTFee = 0;
            //    //    detail.MRIFee = 0;
            //    //    detail.PETFee = 0;
            //    //    detail.SXFee = 0;
            //    //    detail.SYFee = 0;
            //    //}

            //    //CTFee += detail.CTFee;
            //    //MRIFee += detail.MRIFee;
            //    //PETFee += detail.PETFee;
            //    //SXFee += detail.SXFee;
            //    //SYFee += detail.SYFee;
            //    //CTTotFee += detail.CTFee + detail.MRIFee + detail.PETFee;
            //    //ZLTotFee += detail.SXFee + detail.SYFee;
            //}
            ////��������Ŀ��Ϊ���ɼ�
            //lblPreFeeName1.Visible = false;//��ҩ��
            //lblPreFeeName5.Visible = false;  //��ҩ��ҩ��
            //lblPreFeeName9.Visible = false;    //����ҩ��
            //lblDiaFee.Visible = false; //����
            //lblPreFeeName2.Visible = false;  //�����
            //lblPreFeeName10.Visible = false;// ��������
            //lblPreFeeName7.Visible = false;//������
            //lblCure.Visible = false; //���Ʒ�
            //lblPreFeeName11.Visible = false;//�������Ʒ�
            //lblDiagFee.Visible = false; //���
            //lblOtherFee.Visible=false;//������
            ////���·�����Ŀͳ�ƴ����Ӧ�Ĵ�ӡ˳��
            //// 1��ҩ�� 5��ҩ�� 9��ҩ�� 2����� 6����� 10�������� 3��Ѫ��  7������ 11�������Ʒ� 4��λ�� 8����� 12��������
            ////��ҩ�� ��Ӧ��ӡ˳��1
            ////��Ϊ��Ʊ�е����������а�����������������ӡ��Ʊʱ��������ֻ��������������п��ܻ���ָ�ֵ��ygch
            //if (feeDetail[1] != 0)
            //{
            //    lblPriCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[1], 2);
            //    lblPreFeeName1.Visible = true;
            //}
            //else
            //{
            //    lblPreFeeName1.Visible = false;
            //}
            ////�г�ҩ�� ��Ӧ��ӡ˳��5 ��ҩ
            //if (feeDetail[5] != 0)
            //{
            //    lblPriCost5.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[5], 2);
            //    lblPreFeeName5.Visible = true;
            //}
            //else
            //{
            //    lblPreFeeName5.Visible = false;
            //}
            ////�в�ҩ�ѣ���Ӧ��ӡ˳��9 ��ҩ
            //if (feeDetail[9] != 0)
            //{
            //    lblPriCost9.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[9], 2);
            //    lblPreFeeName9.Visible = true;
            //}
            //else
            //{
            //    lblPreFeeName9.Visible = false;
            //}
            ////����� ��Ӧ��ӡ˳��2
            //if (feeDetail[2] != 0)
            //{
            //    lblPriCost2.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[2], 2);
            //    lblPreFeeName2.Visible = true;
            //}
            //else
            //{
            //    lblPreFeeName2.Visible = false;
            //}
            ////����� ��Ӧ��ӡ˳��6
            //if (feeDetail[6] != 0)
            //{
            //    this.lblPriCost6.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[6], 2);
            //    this.lblPreFeeName6.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName6.Visible = false;
            //}
            ////�������� ��Ӧ��ӡ˳��10
            //if (feeDetail[10] != 0)
            //{
            //    this.lblPriCost10.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[10], 2);
            //    this.lblPreFeeName10.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName10.Visible = false;
            //}

            ////��Ѫ�� ��Ӧ��ӡ˳��3
            //if (feeDetail[3] != 0)
            //{
            //    lblPriCost3.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[3], 2);
            //    this.lblPreFeeName3.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName3.Visible = false;
            //}
            ////������ ��Ӧ��ӡ˳��7
            //if (feeDetail[7] != 0)
            //{
            //    lblPriCost7.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[7], 2);
            //    lblPreFeeName7.Visible = true;
            //}
            //else
            //{
            //    lblPreFeeName7.Visible = false;
            //}
            ////�������Ʒ� ��Ӧ��ӡ˳��11 �������˷�Ʊ������������
            //if (feeDetail[11] != 0)
            //{
            //    this.lblPriCost11.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[11], 2);
            //    this.lblPreFeeName11.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName11.Visible = false;
            //}

            ////��λ�� ��Ӧ��ӡ˳��4
            //if (feeDetail[4] != 0)
            //{
            //    lblPriCost4.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[4], 2);
            //    this.lblPreFeeName4.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName4.Visible = false;
            //}
            ////����� ��Ӧ��ӡ˳��8
            //if (feeDetail[8] != 0)
            //{
            //    lblPriCost8.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[8], 2);
            //    this.lblPreFeeName8.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName8.Visible = false;
            //}

            ////�������� ��Ӧ��ӡ˳��12
            //if (feeDetail[12] != 0)
            //{
            //    lblPriCost12.Text = feeDetail[12].ToString();
            //    this.lblPreFeeName12.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName12.Visible = false;
            //}

            //if (regInfo.Pact.PayKind.ID == "02")
            //{
            //    //�����Ϊ����ʾҽ������ǰ��Ľ��  SIMainInfo.User03�ֶ��Ѿ���Ч�� ���Ҳ���������Ч���������� �����������֮�ڿ����ǲ��ǿ��Բ���ʾ
            //    if (regInfo.SIMainInfo.User03 == null)
            //    {
            //        this.label4.Text = "0 Ԫ";
            //        this.label3.Text = "0 Ԫ";
            //    }
            //    else
            //    {
            //        string[] tempMZZF = regInfo.SIMainInfo.User03.Split('|');
            //        for (int m = 0; m < tempMZZF.Length; m++)
            //        {
            //            string[] tempstr = tempMZZF[m].Split(',');
            //            for (int count = 0; count < tempstr.Length; count++)
            //            {
            //                if (tempstr[count] == invoice.Invoice.ID)
            //                {
            //                    if (tempstr[1] == "0303" && tempstr[2] != "0")
            //                        this.label4.Text = tempstr[2] + "Ԫ";////ҽ������ǰ���
            //                    else
            //                        neuLabel22.Visible = true;
            //                    if (tempstr[1] == "0306" && tempstr[2] != "0")
            //                        this.label3.Text = tempstr[2] + "Ԫ";//ҽ���������
            //                    else
            //                        neuLabel24.Visible = true;
            //                }
            //            }
            //        }
            //    }

            //    try
            //    {
            //        this.lblPreFeeName35.Text = "ҽ�������Ը�";
            //        this.lblPreFeeName36.Text = "ҽ�����ǽ��";
            //        //lblPriCost35.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[5], 2);  //ҽ�������Ը�  
            //        //lblPriCost36.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeDetail[6], 2);  //�����������ǵ�feeDetail���������ҵ� ����Ϊһ��ͳ�ƴ��࿴������
            //        this.lblPriCost36.Text = invoice.FT.OwnCost.ToString();  //�Էѽ�� //�����ҽ�������Ը�����סԺ��Ʊ�е��ֶ�һ����д   
            //        this.lblPriCost35.Text = invoice.FT.PayCost.ToString();  //�Ը����   
            //    }
            //    catch
            //    { }
            //}
            //if (regInfo.Pact.PayKind.ID == "02")  //�����Ǹ�if������Ҳ���������
            //{
            //    this.lblPriCost36.Text = invoice.FT.OwnCost.ToString();
            //    this.lblPriCost35.Text = invoice.FT.PayCost.ToString();               
            //    this.lblPreFeeName35.Visible = false;
            //    this.lblPreFeeName36.Visible = false;
            //    this.lblPriCost36.Visible = false;
            //    this.lblPriCost35.Visible = false; //Ϊʲô����ʾ�أ� 
            //}
            //else
            //{
            //    if (FS.FrameWork.Function.NConvert.ToDecimal(this.lblPriCost35.Text.Trim()) > 0)
            //    {
            //        this.lblPreFeeName35.Visible = true;
            //        this.lblPriCost35.Visible = true;
            //    }
            //    else
            //    {
            //        this.lblPreFeeName35.Visible = false;
            //        this.lblPriCost35.Visible = false;
            //    }
            //    this.lblPreFeeName36.Visible = false;
            //    this.lblPriCost36.Visible = false;
            //}

            //if (isPreview)
            //{
            //    this.lblPreFeeName35.Visible = true;
            //    this.lblPriCost35.Visible = true;
            //    this.lblPreFeeName36.Visible = true;
            //    this.lblPriCost36.Visible = true;
            //}
            //this.lblSimType.Visible = false;   //��������
            //this.lblSimMedType.Visible = false;//�α�����
            ////if (FS.NFC.Function.NConvert.ToDecimal(this.lblPriCost24.Text.Trim()) > 0 || isPreview)//[2010-4-14]zhaozf�޸ģ�Ԥ��ʱ��ǩȫ��ʾ
            ////{
            ////    this.lblPreFeeName24.Visible = true;
            ////    this.lblPriCost24.Visible = true;
            ////}
            ////else
            ////{
            ////    this.lblPreFeeName24.Visible = false;
            ////    this.lblPriCost24.Visible = false;
            ////}
            ////if (FS.NFC.Function.NConvert.ToDecimal(this.lblPriCost8.Text.Trim()) > 0 || isPreview)//[2010-4-14]zhaozf�޸ģ�Ԥ��ʱ��ǩȫ��ʾ
            ////{
            ////    this.lblPreFeeName8.Visible = true;
            ////    this.lblPriCost8.Visible = true;
            ////}
            ////else
            ////{
            ////    this.lblPreFeeName8.Visible = false;
            ////    this.lblPriCost8.Visible = false;
            ////}
            ////zhangq
            /////û�д���ķ�ƱӦ�ܽ��
            //decimal NoDealOwnPay = invoice.FT.OwnCost + invoice.FT.PayCost;
            /////�����ķ�ƱӦ�ܽ��
            //decimal DealOwnPay = FS.FrameWork.Public.String.FormatNumber(NoDealOwnPay, 1);
            /////����������
            ////decimal DealedCost = DealOwnPay - NoDealOwnPay;
            ////this.lblPriCost7.Text = DealedCost.ToString();//FS.NFC.Public.String.FormatNumberReturnString(DealedCost,2);
            ////if (FS.NFC.Function.NConvert.ToDecimal(this.lblPriCost7.Text.Trim()) != 0 || isPreview)//[2010-4-14]zhaozf�޸ģ�Ԥ��ʱ��ǩȫ��ʾ
            ////{
            ////    this.lblPreFeeName7.Visible = true;
            ////    this.lblPriCost7.Visible = true;
            ////}
            ////else
            ////{
            ////    this.lblPreFeeName7.Visible = false;
            ////    this.lblPriCost7.Visible = false;
            ////}

            //if (regInfo.Pact.PayKind.ID == "03")//����
            //{
            //    this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2) + 
            //                          " ��ҽ�ϼ�:" + FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost + invoice.FT.PayCost, 2);//ҽ��/��ҽ����
            //    //���ݺ�ͬ��λ��ȡ�Ը�����
            //    FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

            //    pactMgr.SetTrans(this.trans.Trans);
            //    FS.HISFC.Models.Base.PactInfo pactObj = pactMgr.GetPactUnitInfoByPactCode(regInfo.Pact.ID);
            //    if (pactObj != null && pactObj.Rate.PayRate > 0)
            //    {
            //        string priPay = FS.FrameWork.Public.String.FormatNumberReturnString(DealOwnPay, 2) + " (" + 
            //                        FS.FrameWork.Public.String.FormatNumberReturnString(pactObj.Rate.PayRate * 100, 0).Replace(".", "") + "%:" + 
            //                        FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PayCost, 2) + ")";
            //        if (priPay.Length > 15)
            //        {
            //            priPay.Insert(15, "\n");
            //            Font f = new Font("����", 8);
            //            this.lblPriPay.Font = f;
            //        }
            //        this.lblPriPay.Text = priPay;
            //        //this.lblPriPay.Text
            //    }
            //    else
            //    {
            //        this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(DealOwnPay, 2);
            //    }
            //}
            //else if (regInfo.Pact.PayKind.ID == "02")
            //{
            //    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //    //string[] tempzfstr = regInfo.SIPerson.User02.Split('|');    //��֪������ֶ���ʲô���塣�� �Ǹ�ҽ��/���Ѽ��˵�lbl��ֵ 
            //    ////string[] tempzfstr = regInfo.SIMainInfo.PersonType.
            //    //for (int m = 0; m < tempzfstr.Length; m++)
            //    //{
            //    //    string[] tempstr = tempzfstr[m].Split(',');
            //    //    for (int count = 0; count < tempstr.Length; count++)
            //    //    {
            //    //        if (tempstr[count] == invoice.Invoice.ID)
            //    //        {
            //    //            this.lblPriPub.Text = tempstr[2];
            //    //            this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost - FS.FrameWork.Function.NConvert.ToDecimal(tempstr[2]), 2);
            //    //        }
            //    //    }
            //    //}
            //    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //    if (regInfo.SIMainInfo.User01 != null)// || regInfo.SIMainInfo.User01 != "")
            //    {
            //        string[] tempstr1 = regInfo.SIMainInfo.User01.Split('|');
            //        for (int m = 0; m < tempstr1.Length; m++)
            //        {
            //            string[] tempstrr = tempstr1[m].Split(',');
            //            for (int count = 0; count < tempstrr.Length; count++)
            //            {
            //                if (tempstrr[count] == invoice.Invoice.ID)
            //                {
            //                    this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost - FS.FrameWork.Function.NConvert.ToDecimal(tempstrr[1]), 2);
            //                    this.lblPriPay.Text = tempstrr[1];// "�ֽ� " +
            //                }
            //            }
            //        }
            //    }
            //    //if (regInfo.SIPerson.BaseSITypeId == "1")
            //    //{
            //    //    this.lblSimType.Text = "ҽ�Ʊ���";
            //    //}
            //    //else if (regInfo.SIPerson.BaseSITypeId == "2")
            //    //{
            //    //    this.lblSimType.Text = "����ҽ��";
            //    //}
            //    //else if (regInfo.SIPerson.BaseSITypeId == "3")
            //    //{
            //    //    this.lblSimType.Text = "����ҽ��";
            //    //}
            //    //else if (regInfo.SIPerson.BaseSITypeId == "4")
            //    //{
            //    //    this.lblSimType.Text = "����ͳ��ҽ��";
            //    //}
            //    //else if (regInfo.SIPerson.BaseSITypeId == "5")
            //    //{
            //    //    this.lblSimType.Text = "����ҽ��";
            //    //}
            //    //else if (regInfo.SIPerson.BaseSITypeId == "6")
            //    //{
            //    //    this.lblSimType.Text = "����ҽ�Ʊ���";
            //    //}
            //    //else
            //    //{
            //    //    this.lblSimType.Text = "�ٶ�ҽ��";
            //    //}
            //    this.lblSimType.Visible = false;
            //    this.lblSimMedType.Visible = true;
            //    //if (regInfo.SIMainInfo.PersonType.ID == "1")
            //    //{
            //    //    this.lblSimType.Text = "ҽ�Ʊ���";
            //    //}
            //    if (regInfo.SIMainInfo.PersonType.ID == "2")
            //    {
            //        this.lblSimType.Visible = true;
            //        this.lblSimType.Text = "����ҽ��";
            //    }
            //    //else if (regInfo.SIMainInfo.PersonType.ID == "3")
            //    //{
            //    //    this.lblSimType.Text = "����ҽ��";
            //    //}
            //    //else if (regInfo.SIMainInfo.PersonType.ID == "4")
            //    //{
            //    //    this.lblSimType.Text = "����ͳ��ҽ��";
            //    //}
            //    //else if (regInfo.SIMainInfo.PersonType.ID == "5")
            //    //{
            //    //    this.lblSimType.Text = "����ҽ��";
            //    //}
            //    //else if (regInfo.SIMainInfo.PersonType.ID == "6")
            //    //{
            //    //    this.lblSimType.Text = "����ҽ�Ʊ���";
            //    //}
            //    //else
            //    //{
            //    //    this.lblSimType.Text = "�ٶ�ҽ��";
            //    //}

            //    if (regInfo.SIMainInfo.MedicalType.ID == "1")
            //    {
            //        //this.lblSimMedType.Text = "��ͨ����";
            //        this.lblSimMedType.Visible = false;
            //    }
            //    else if (regInfo.SIMainInfo.MedicalType.ID == "2")
            //    {
            //        this.lblSimMedType.Text = "�ز�����";
            //    }
            //    else if (regInfo.SIMainInfo.MedicalType.ID == "3")
            //    {
            //        this.lblSimMedType.Text = "�ؼ�����";
            //    }
            //    else if (regInfo.SIMainInfo.MedicalType.ID == "4")
            //    {
            //        this.lblSimMedType.Text = "��������";
            //    }
            //    else if (regInfo.SIMainInfo.MedicalType.ID == "5")
            //    {
            //        this.lblSimMedType.Text = "�������";
            //    }
            //    else if (regInfo.SIMainInfo.MedicalType.ID == "6")
            //    {
            //        this.lblSimMedType.Text = "Ԥ������";
            //    }
            //    else if (regInfo.SIMainInfo.MedicalType.ID =="7")
            //    {
            //        this.lblSimMedType.Text = "�ٶ�ͨ��";
            //        //this.lbsiJZP.Visible = true;// Ʊ����û��"������"���λ��  ��ʱע���� ����Ҫ��˵ 
            //        //this.lbsiJZP.Text = "������:" + regInfo.SIPerson.Name + "(" + regInfo.SSN+ ")";
            //        //this.lbsiJZP.Text = "������:" + regInfo.SIMainInfo.
            //    }
            //    //string pri = "";
            //    //if (invoice.FT.PubCost > 0)
            //    //{
            //    //    pri = FS.NFC.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2);
            //    //}
            //    //if (invoice.FT.OwnCost > 0)
            //    //{
            //    //    pri = FS.NFC.Public.String.FormatNumberReturnString(DealOwnPay, 2) + "(" + invoice.FT.PayCost.ToString() +
            //    //          "��������ҩƷ" + (DealOwnPay - invoice.FT.PayCost).ToString() + ")";
            //    //}
            //    //else
            //    //{
            //    //    pri = FS.NFC.Public.String.FormatNumberReturnString(DealOwnPay, 2);
            //    //}
            //    //if (pri.Length > 10)
            //    //{
            //    //    pri.Insert(10, "\n");
            //    //    Font f = new Font("����", 8);
            //    //    this.lblPriPay.Font = f;
            //    //}
            //    //this.lblPriPay.Text = pri;
            //}
            //else
            //{
            //    //if (invoice.FT.PubCost > 0)
            //    //{
            //    //    this.lblPriPub.Text = FS.NFC.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2);
            //    //}
            //    //this.lblPriPay.Text = FS.NFC.Public.String.FormatNumberReturnString(DealOwnPay, 2);
            //    this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(NoDealOwnPay, 2);  //���˽ɷ�
            //}
            ////zhangq
            ////��ʾҩ����ҩ����
            //try
            //{
            //    if (invoice.DrugWindowsNO != null && invoice.DrugWindowsNO != string.Empty && !isPreview)
            //    {
            //        string[] drugWindow = invoice.DrugWindowsNO.Split('|');
            //        Hashtable hsDrugWindow = new Hashtable();
            //        string disPlayWindow = string.Empty;
            //        for (int x = 0; x < drugWindow.Length; x++)
            //        {
            //            if (hsDrugWindow.ContainsValue(drugWindow[x]))
            //            {
            //                disPlayWindow = /*disPlayWindow + "��" + */ drugWindow[x].ToString();
            //            }
            //            else
            //            {
            //                hsDrugWindow.Add(x, drugWindow[x].ToString());
            //                disPlayWindow = /*disPlayWindow + "��" + */drugWindow[x].ToString();
            //            }
            //        }

            //        if (disPlayWindow != string.Empty)
            //        {
            //            this.lblDrugWindow.Visible = true;
            //            this.lblDrugWindow.Text += disPlayWindow.TrimStart('��');
            //        }
            //        else
            //        {
            //            this.lblDrugWindow.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        this.lblDrugWindow.Visible = false;
            //    }
            //}
            //catch
            //{ }
            //this.lblPriLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);

            //string[] strMoney = new string[8];

            //strMoney = this.GetUpperCashbyNumber(FS.FrameWork.Public.String.FormatNumber(invoice.FT.TotCost, 2));

            //this.lblPriF.Text = strMoney[0];
            //this.lblPriJ.Text = strMoney[1];
            //this.lblPriY.Text = strMoney[3];
            //this.lblPriS.Text = strMoney[4];
            //this.lblPriB.Text = strMoney[5];
            //this.lblPriQ.Text = strMoney[6];
            //this.lblPriW.Text = strMoney[7];
            //this.lblPriSW.Text = strMoney[8];

            //#region ��Ʊ�嵥��ϸ
            ////FS.HISFC.Object.Fee.Outpatient.BalanceList bb = null;
            ////bb = (FS.HISFC.Object.Fee.Outpatient.BalanceList)alInvoiceDetail[0];
            ////if (bb.FeeCodeStat.ID == "01")
            ////{
            ////    bool isSet = false;
            ////    foreach (FS.HISFC.Object.Fee.Outpatient.FeeItemList aa in alFeeItemList)
            ////    {
            ////        if (aa.ExecOper.Dept.ID == "0314")
            ////        {
            ////            if (isSet)
            ////            {
            ////                continue;
            ////            }
            ////            isSet = true;
            ////            this.SetQd(alFeeItemList);
            ////        }
            ////    }
            ////}
            //#endregion
            ////if (!string.IsNullOrEmpty(invoice.CanceledInvoiceNO))
            ////{
            ////    this.lblReprint.Text = "�ش�Ʊ��:" + invoice.CanceledInvoiceNO;
            ////    this.lblReprint.Visible = true;
            ////}
            ////else
            ////{
            ////    lblReprint.Visible = false;
            ////}
            //this.Print();
            #endregion
            return 0;
        }
        /// <summary>
        /// ��÷������������
        /// </summary>
        /// <param name="i">���</param>
        /// <returns>�������������ؼ�</returns>
        private Control GetFeeNameLable(int i)
        {
            Control c= this.Controls[string.Concat("lblPreFeeName", i.ToString())];
            if (c != null)
            {
                c.Visible = true;
            }

            return c;
            //foreach (Control c in this.Controls)
            //{
            //    if (c.Name == "lblPreFeeName" + i.ToString())
            //    {
            //        c.Visible = true;
            //        return c;
            //    }
            //}
            //return null;
        }
        /// <summary>
        /// ��÷��ý�������
        /// </summary>
        /// <param name="i">���</param>
        /// <returns>���ý�������ؼ�</returns>
        private Control GetFeeCostLable(int i)
        {
            Control c = this.Controls[string.Concat("lblPriCost", i.ToString())];
            if (c != null)
            {
                c.Visible = true;
            }

            return c;
            //foreach (Control c in this.Controls)
            //{
            //    if (c.Name == "lblPriCost" + i.ToString())
            //    {
            //        c.Visible = true;
            //        return c;
            //    }
            //}
            //return null;
        }

        //��������޴����� ����İ���
        ///// <summary>
        ///// ��÷�Ʊ�嵥��ϸ
        ///// </summary>
        ///// <param name="i">���</param>
        ///// <returns>���ý�������ؼ�</returns>
        //private void SetQd(ArrayList alQd)
        //{
        //    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList Qd in alQd)
        //    {
        //        //if (Qd.Item.SysClass.ID.ToString() == "P")
        //        if (Qd.ExecOper.Dept.ID == "0314")
        //        {
        //            int count = this.neuSpread1_Sheet1.Rows.Count;
        //            this.neuSpread1_Sheet1.Rows.Add(count, 1);
        //            count = this.neuSpread1_Sheet1.Rows.Count;
        //            if (Qd == alQd[0])
        //            {
        //                this.neuSpread1_Sheet1.Columns[0].Visible = true;
        //            }
        //            //else
        //            //{
        //            //    this.neuSpread1_Sheet1.Columns[0].Visible = false;
        //            //}
        //            this.neuSpread1_Sheet1.Cells[count - 1, 0].Text = Qd.Item.Name;
        //            this.neuSpread1_Sheet1.Cells[count - 1, 1].Text = Qd.Item.Specs;
        //            this.neuSpread1_Sheet1.Cells[count - 1, 2].Text = Qd.Order.Qty.ToString();
        //            this.neuSpread1_Sheet1.Cells[count - 1, 3].Text = Qd.Order.Unit;
        //            this.neuSpread1_Sheet1.Cells[count - 1, 4].Text = Qd.Item.Price.ToString();
        //            this.neuSpread1_Sheet1.Cells[count - 1, 5].Text = "Ԫ";
        //            this.neuSpread1_Sheet1.Cells[count - 1, 6].Text = Qd.FT.TotCost.ToString();
        //            //FS.HISFC.Object.Pharmacy.Item phaItem = phaManagement.GetItem(Qd.Item.ID);
        //            this.neuSpread1_Sheet1.Cells[count - 1, 7].Text = "Ԫ";
        //            //this.neuSpread1_Sheet1.Cells[count - 1, 8].Text = Qd.RecipeNO;
        //            //UFC.Order.Classes.Function.DrawCombo(this.fpSpread1_Sheet1, 2, 4, 0);
        //        }
        //    }
        //    //return null;
        //}

        /// <summary>
        /// ��Ʊֻ��ӡ��д���� ��ӡ��ʮ��
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        private string[] GetUpperCashbyNumber(decimal Cash)
        {
            string[] sNumber = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
            string[] sReturn = new string[9];
            string strCash = null;
            //���λ��
            int iLen = 0;
            strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
            if (strCash.Length > 9)
            {
                strCash = strCash.Substring(strCash.Length - 9);
            }

            //���λ��
            iLen = 9 - strCash.Length;
            for (int j = 0; j < iLen; j++)
            {
                int k = 0;
                k = 8 - j;
                sReturn[k] = "��";
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


        private string invoiceType;

        public string InvoiceType
        {
            get { return "MZ01"; }
        }

        private FS.HISFC.Models.Registration.Register register;
        public FS.HISFC.Models.Registration.Register Register
        {
            set
            {
                //register = value;
                //if (register.Pact.ID == "7")
                //{
                //    invoiceType = "MZ05";
                //}
                //else
                //{
                invoiceType = "MZ01";
                //}
            }
        }

        #endregion

        #region IInvoicePrint ��Ա

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
                // TODO:  ��� ucInvoiceGY.Description getter ʵ��
                return "����ҽѧԺ��������ҽԺ���﷢Ʊ";
            }
        }

        public void SetPreView(bool isPreView)
        {
            _isPreView = isPreView;
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
                    MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);
                    return -1;
                }
                if (this.trans == null)
                {
                    MessageBox.Show("û���������ݿ�����!");
                    return -1;
                }


                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZFP", 787, 400);
                print.SetPageSize(ps);

                if (_isPreView)
                {
                    print.PrintDocument.PrinterSettings.PrinterName = "MZFP";
                }

                //��ô�ӡ����
                string printer = this.controlIntegrate.GetControlParam<string>("MZFP", true, "");
                if (!string.IsNullOrEmpty(printer))
                {
                    print.PrintDocument.PrinterSettings.PrinterName = printer;
                }

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;
                print.PrintPage(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            return 0;
        }


        #endregion

        #region IInvoicePrint ��Ա

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

        #region IInvoicePrint ��Ա

        //public int SetPrintOtherInfomation(FS.HISFC.Object.Registration.Register regInfo, ArrayList Invoices, ArrayList invoiceDetails, ArrayList feeDetails)
        //{
        //    this.otherPrint.RInfo = regInfo;
        //    this.otherPrint.Trans = this.trans;
        //    this.otherPrint.Invoices = Invoices;
        //    this.otherPrint.FeeDetails = feeDetails;
        //    this.otherPrint.SetDisplay();
        //    return 0;
        //}


        public int PrintOtherInfomation()//СƱ��ӡ����?
        {
            FS.FrameWork.WinForms.Classes.Print print = null;
            try
            {
                print = new FS.FrameWork.WinForms.Classes.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);
                return -1;
            }
            if (this.trans == null)
            {
                MessageBox.Show("û���������ݿ�����!");
                return -1;
            }
            //FS.UFC.Common.Classes.Function.GetPageSize("MZFEEDETAIL", ref print, ref this.trans);

            //print.PrintDocument.PrinterSettings.PrinterName = "MZFEEDETAILPRINTER";
            //System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("MZFEEDETAIL", 669, 425);
            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("MZFEEDETAIL", 669, 425);
            print.IsDataAutoExtend = true;
            print.SetPageSize(ps);
            print.IsCanCancel = false;
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsDataAutoExtend = false;


            //ע��СƱ��ӡ���ܣ����� 09-07-02
            //for (int m = 0; m < otherPrint.fpSpread1.Sheets.Count; m++)
            //{
            //    this.otherPrint.fpSpread1.ActiveSheet = otherPrint.fpSpread1.Sheets[m];
            //    print.PrintPage(0, 0, this.otherPrint.fpSpread1);
            //}
            return 0;

        }

        #endregion

        #region IInvoicePrint ��Ա

        private string setPayModeType = "";
        private string splitInvoicePayMode = "";

        #endregion

        #region IInvoicePrint ��Ա

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
            ;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, ArrayList Invoices, ArrayList invoiceDetails, ArrayList feeDetails)
        {
            return 1;
        }

        //int FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint.SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, bool isPreview)
        //{
        //    this.SetPrintValue(regInfo, invoice, invoiceDetails, feeDetails, isPreview);
        //    return 1;
        //}

        /// <summary>
        /// ���÷�Ʊ��ӡ����
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="invoice">��Ʊ������Ϣ</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ��Ϣ</param>
        /// <param name="feeDetails">������ϸ��Ϣ</param>
        /// <param name="alPayModes">֧����ʽ����</param>
        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
        /// <returns></returns>
        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList alInvoiceDetail, ArrayList alFeeItemList, ArrayList alPayModes, bool isPreview)
        {
            if (alFeeItemList.Count <= 0)
            {
                return -1;
            }

            lblPriDateOut.Text = "";
            lblPriDateIn.Text = "";

            ///ZFPha : �Է�ҩƷ
            ///ZFItem: �Էѷ�ҩƷ
            ///CBPha: ����ҩƷ
            decimal ZFPha = 0m, CBPha = 0m, ZFItem = 0m;

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFeeItemList)
            {
                if (item.Item.ItemType== FS.HISFC.Models.Base.EnumItemType.Drug)
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
                this.label5.Text = item.RecipeOper.ID;
            }

            //����ҩ = �Է�ҩ+����ҩ
            if (ZFPha + CBPha > 0)
            {
                //this.lblPriCost30.Text = FS.FrameWork.Public.String.FormatNumberReturnString(ZFPha + CBPha, 2);
            }

            #region ֧����ʽ��ӡ

            //decimal ecoCost = 0;
            //foreach (FS.HISFC.Models.Fee.BalancePayBase payObj in alPayModes)
            //{
            //    if (payObj.PayType.ID == "RC")
            //    {
            //        ecoCost += payObj.FT.TotCost;
            //    }
            //}
            //if (ecoCost <= 0)
            //{
            //    this.lblEcoCost.Text = "";
            //}
            //else
            //{
            //    this.lblEcoCost.Text = "�����" + ecoCost.ToString();
            //}

            #endregion

            //��տؼ��߿�
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
            //���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��

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
                        //c.Visible = true;// ������ 
                    }
                }
            }

            FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
            if (this.trans != null)
            {
                dbManager.SetTrans(this.trans.Trans);
            }

            DateTime dtNow = dbManager.GetDateTimeFromSysDateTime();

            //������Ϣ
            if (isPreview)
            {

                this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();  //��
                this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();//��
                this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();
                this.lblPriOper.Text = invoice.BalanceOper.ID;//�������Ա

            }
            else
            {
                this.lblPriSwYear.Text = invoice.PrintTime.Year.ToString();  //��
                this.lblPriSwMonth.Text = invoice.PrintTime.Month.ToString();//��
                this.lblPriSwDay.Text = invoice.PrintTime.Day.ToString();    //��
                string docName = regInfo.DoctorInfo.Templet.Doct.ID;
                if (docName == "")
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList f = alFeeItemList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                    if (f != null && f.RecipeOper != null)
                    {
                        this.lblPriOper.Text = invoice.BalanceOper.ID;//�������Ա
                    }
                }
                else
                {
                    this.lblPriOper.Text = invoice.BalanceOper.ID;//�������Ա
                }

            }
            this.lblInvoice.Text = invoice.Invoice.ID;//��Ʊ��
            this.lblPriSwBalanceType.Text = invoice.PrintedInvoiceNO; // ʵ�ʷ�Ʊ��
            if (regInfo.SSN != "")
            {
                if (regInfo.SIMainInfo.MedicalType.ID == "7")
                    this.lblPriName.Text = regInfo.Name;
                else
                    this.lblPriName.Text = regInfo.Name + "(" + regInfo.SSN + ")";
            }
            else
            {
                this.lblPriName.Text = regInfo.Name;//����
            }
            string tempRate = "";

            if (regInfo.Pact.ID == "3" || regInfo.Pact.ID == "2")
            {
                //FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
                //if (this.trans != null)
                //{
                //    regMgr.SetTrans(this.trans.Trans);
                //}
                //FS.HISFC.Models.Registration.Register temp = regMgr.GetByClinic(regInfo.ID);
                //if (temp != null)
                //{
                //    if (temp.Pact.PayKind.ID == "02")
                //    {
                //        if (regInfo.Pact.ID == "2")
                //        {
                //            tempRate = "ְ��ҽ��";
                //        }
                //        else if (regInfo.Pact.ID == "3")
                //        {
                //            tempRate = "����ҽ��";
                //        }
                //        else if (regInfo.Pact.ID == "7")
                //        {
                //            tempRate = "�ض�����";
                //        }
                //    }
                //    else if (temp.Pact.PayKind.ID == "03")
                //    {
                //        if (regInfo.Pact.ID == "6")
                //        {
                //            tempRate = "ҽ���Ż�";
                //        }
                //        else if (regInfo.Pact.ID == "8")
                //        {
                //            tempRate = "��Լ��λ";
                //        }
                //    }
                //}
                TSNewRate = TSNewRate.TrimEnd(',');
                SpecialDisPlay = "(" + TSNewRate + ")";
            }
            else
            {
                tempRate = regInfo.Pact.Name;
                SpecialDisPlay = string.Empty;
            }
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

            //ArrayList al;
            //try
            //{
            //    al = myOutPatient.QueryBalancePaysByInvoiceSequence(invoice.CombNO);
            //    if (al == null)
            //    {
            //        return -1;
            //    }
            //}
            //catch
            //{
            //    return -1;
            //}
            FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();
            //helper.ArrayObject = FS.HISFC.Object.Fee.EnumPayTypeService.List();
            helper.ArrayObject = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (helper.ArrayObject == null)
            {
                return -1;
            }

            string strPayMode = "";//֧����ʽ

            for (int i = 0; i < alPayModes.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.BalancePay payMode = alPayModes[i] as FS.HISFC.Models.Fee.Outpatient.BalancePay;

                strPayMode += " " + helper.GetObjectFromID(payMode.PayType.ID.ToString()).Name;// +" ";//+FS.NFC.Public.String.FormatNumber(payMode.Cost,2);//�������Ա
            }

            string payKind = "";
            if (this.setPayModeType == "1")
            {
                payKind = this.splitInvoicePayMode;
            }
            else
            {
                payKind = tempRate;// +strPayMode + SpecialDisPlay;
                //payKind = strPayMode;
            }

            if (payKind.Length > 8)
            {
                Font f = new Font("����", 8);
                lblPriPayKind.Font = f;
                payKind.Insert(8, "\n");
            }
            lblPriPayKind.Text = payKind;

            //[2010-03-02] zhaozf ��ӣ�������Էѣ�����ʾ���Էѡ������֣����㲡�˱���
            //lblPriPayKind.Text = lblPriPayKind.Text.Replace("�Է�", "");

            //lblPriSwDrugWindow.Text = invoice.User01;//��ҩҩ��

            //decimal CTFee = 0m, MRIFee = 0m, SXFee = 0m, SYFee = 0m, PETFee = 0m; ;
            ////�����ܷ���
            //decimal ZLTotFee = 0m;
            ////CT�ܷ���
            //decimal CTTotFee = 0m;

            decimal[] feeDetail = new decimal[25];
            string[] feeNameList = new string[25];
            //Ʊ����Ϣ
            for (int i = 0; i < alInvoiceDetail.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
                detail = (FS.HISFC.Models.Fee.Outpatient.BalanceList)alInvoiceDetail[i];

                //��ʾ��������
                #region
                //if (isPreview)
                //{
                System.Windows.Forms.Label lblFeeName;
                if (detail.InvoiceSquence < 1 || detail.InvoiceSquence > 24)
                {
                    continue;
                }
                lblFeeName = (System.Windows.Forms.Label)this.GetFeeNameLable(detail.InvoiceSquence);
                if (lblFeeName == null)
                {
                    MessageBox.Show("û���ҵ����ô���Ϊ" + detail.FeeCodeStat.Name + "�Ĵ�ӡ���!");
                    return -1;
                }

                    feeNameList[detail.InvoiceSquence] = detail.FeeCodeStat.Name;
                    lblFeeName.Text = detail.FeeCodeStat.Name;
                    lblFeeName.Visible = true;
                //}
                #endregion

                //���ý�ֵ
                System.Windows.Forms.Label lblFeeCost = null;
                lblFeeCost = (System.Windows.Forms.Label)this.GetFeeCostLable(detail.InvoiceSquence);
                if (lblFeeCost == null)
                {
                    MessageBox.Show("û���ҵ����ô���Ϊ" + detail.FeeCodeStat.Name + "�Ĵ�ӡ���!");
                    return -1;
                }

                //if (regInfo.Pact.PayKind.ID != "03")
                //{
                feeDetail[detail.InvoiceSquence] = detail.BalanceBase.FT.TotCost;
                //lblFeeCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(detail.BalanceBase.FT.TotCost, 2);
                lblFeeCost.Text = detail.BalanceBase.FT.TotCost.ToString("0.00");
                //}

            }



            //zhangq
            ///û�д���ķ�ƱӦ�ܽ��
            decimal NoDealOwnPay = invoice.FT.OwnCost + invoice.FT.PayCost;

            #region �����ķ�ƱӦ�ܽ��
            // {46FCAD38-D9AB-46c6-9D8A-7A6453315E3F}

            ///�����ķ�ƱӦ�ܽ��
            //decimal DealOwnPay = FS.FrameWork.Public.String.FormatNumber(NoDealOwnPay, 1);

            decimal DealOwnPay = NoDealOwnPay; // ��ʵ�ʽ���

            #endregion

            ///����������
            //decimal DealedCost = DealOwnPay - NoDealOwnPay;
            //this.lblPriCost7.Text = DealedCost.ToString();//FS.NFC.Public.String.FormatNumberReturnString(DealedCost,2);
            //if (FS.NFC.Function.NConvert.ToDecimal(this.lblPriCost7.Text.Trim()) != 0 || isPreview)//[2010-4-14]zhaozf�޸ģ�Ԥ��ʱ��ǩȫ��ʾ
            //{
            //    this.lblPreFeeName7.Visible = true;
            //    this.lblPriCost7.Visible = true;
            //}
            //else
            //{
            //    this.lblPreFeeName7.Visible = false;
            //    this.lblPriCost7.Visible = false;
            //}

            if (regInfo.Pact.PayKind.ID == "03" || regInfo.Pact.PayKind.ID == "02")//���� ����ҽ��
            {
                this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost, 2); //+
                //" ��ҽ�ϼ�:" + FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.PubCost + invoice.FT.PayCost, 2);//ҽ��/��ҽ����
                //���ݺ�ͬ��λ��ȡ�Ը�����
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
                        Font f = new Font("����", 8);
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
            this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(DealOwnPay, 2);

            //��ʾҩ����ҩ����
            try
            {
                if (invoice.DrugWindowsNO != null && invoice.DrugWindowsNO != string.Empty && !isPreview)
                {
                    string[] drugWindow = invoice.DrugWindowsNO.Split('|');
                    Hashtable hsDrugWindow = new Hashtable();
                    string disPlayWindow = string.Empty;
                    for (int x = 0; x < drugWindow.Length; x++)
                    {
                        if (hsDrugWindow.ContainsValue(drugWindow[x]))
                        {
                            disPlayWindow = /*disPlayWindow + "��" + */ drugWindow[x].ToString();
                        }
                        else
                        {
                            hsDrugWindow.Add(x, drugWindow[x].ToString());
                            disPlayWindow = /*disPlayWindow + "��" + */drugWindow[x].ToString();
                        }
                    }

                    if (disPlayWindow != string.Empty)
                    {
                        this.lblDrugWindow.Visible = true;
                        this.lblDrugWindow.Text += disPlayWindow.TrimStart('��');
                    }
                    else
                    {
                        this.lblDrugWindow.Visible = false;
                    }
                }
                else
                {
                    this.lblDrugWindow.Visible = false;
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

            if (!isPreview)
            {
                this.Print();
            }
            return 0;
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

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                //type[0]=typeof(FS.HISFC.BizProcess.Integrate.FeeInterface.i)
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint);
                return type;
            }
        }
        #endregion
    }
}
