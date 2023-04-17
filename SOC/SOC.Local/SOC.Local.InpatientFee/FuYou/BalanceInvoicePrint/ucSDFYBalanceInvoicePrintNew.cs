using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.FuYou.BalanceInvoicePrint
{
    public partial class ucSDFYBalanceInvoicePrintNew : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy//, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        private FS.HISFC.Models.Base.EBlanceType isMidwayBalance;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;

        public ucSDFYBalanceInvoicePrintNew()
        {
            InitializeComponent();
        }

        #region IBalanceInvoicePrintmy ��Ա
        public FS.HISFC.Models.Base.EBlanceType IsMidwayBalance
        {
            get
            {
                return this.isMidwayBalance;
            }
            set
            {
                this.isMidwayBalance = value;
            }
        }
        #endregion

        #region IBalanceInvoicePrint ��Ա

        public int Clear()
        {
            return 1;
        }

        public string InvoiceType
        {
            get
            {
                return "ZY01";
            }
        }

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                this.patientInfo = value;
            }
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize ps = null;
            ps = new FS.HISFC.Models.Base.PageSize("ZYFP", 787, 400);
            p.SetPageSize(ps);
            p.PrintDocument.PrinterSettings.PrinterName = "ZYFP";
            p.IsHaveGrid = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsCanCancel = false;
            p.PrintPage(0, 0, this);

            return 1;
        }

        public int PrintPreview()
        {
            //FS.NFC.Interface.Classes.Print p = new FS.NFC.Interface.Classes.Print();
            //FS.UFC.Common.Classes.Function.GetPageSize("ipbalance", ref p);

            //p.PrintPreview(0, 0, this);
            //return 1;

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //��տؼ��߿�
            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString().Contains("Label"))
                {
                    System.Windows.Forms.Label lblControl;
                    lblControl = (System.Windows.Forms.Label)c;
                    lblControl.TextAlign = ContentAlignment.MiddleCenter;
                }
            }
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("newbalance", 694, 400);//���newbalance name�ǲ��ǹ̶��ģ�
            p.SetPageSize(size);
            p.PrintPreview(30, 0, this);

            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, true);
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, false);
        }

        public IDbTransaction Trans
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region ��������

        /// <summary>
        /// ��ӡ�ؼ���ֵ
        /// </summary>
        /// <param name="Pinfo"></param>
        /// <param name="Pinfo"></param>
        /// <param name="al">balancelist����</param>
        /// <param name="IsPreview">�Ƿ��ӡ�������ʾ����</param>
        /// <returns></returns>
        protected int SetPrintValue(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceHead, ArrayList alBalanceList, bool IsPreview)
        {
            if (alBalanceList.Count <= 0) return -1;
            if (!IsPreview)
            {
                lblIn.Visible = true;
                //��տؼ��߿�
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Substring(0, 3) == "lbl")
                    {
                        System.Windows.Forms.Label lblControl;
                        lblControl = (System.Windows.Forms.Label)c;
                        lblControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
                        c.Visible = true;
                    }
                }
            }
            //���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��
            if (IsPreview)
            {
                lblIn.Visible = false;
                foreach (Control c in this.Controls)
                {
                    if (c.Name.Length > 6)
                    {
                        if (c.Name.Substring(0, 6) == "lblPre")
                            c.Visible = IsPreview;
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
                            c.Visible = IsPreview;
                    }

                }

            }
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            ArrayList alBalancePay = new ArrayList();
            alBalancePay = myInpatient.QueryBalancePaysByInvoiceNOAndBalanceNO(balanceHead.Invoice.ID, int.Parse(balanceHead.ID));
            if (alBalancePay == null)
            {
                MessageBox.Show("��û���֧����ʽ����!");
                return -1;
            }
            //��ֵ

            //������Ϣ
            this.lblPriPatientNo.Text = patientInfo.PID.PatientNO;  //סԺ��

            this.lblPriNurseCell.Text = patientInfo.PVisit.PatientLocation.Dept.Name; //���� --�˴��ÿ���name ��Ʊ�հ�̫�� ��ɽԭ��ϵͳ�޲�������

            this.lblPriSwYear.Text = balanceHead.BalanceOper.OperTime.Year.ToString(); //��

            this.lblPriSwMonth.Text = balanceHead.BalanceOper.OperTime.Month.ToString(); //��

            this.lblPriSwDay.Text = balanceHead.BalanceOper.OperTime.Day.ToString(); //��

            //this.lblPriBalanceType.Text = balanceHead.BalanceType.Name.ToString();//��������//[2007/10/22]���ĵ�

            this.lblPriName.Text = patientInfo.Name;//����


            #region //סԺ����  -- By Maokb
            /*
             * סԺ���ڸ��ݱ��ν������ֹʱ��
             * ��ΪסԺ�����ļ���Ϊ ��ͷ����β
             * ���Գ�Ժ���� ��Ժ����-��Ժ����
             * �������;������+1
             */
            if (patientInfo.Pact.PayKind.ID == "03")
            {
                if (balanceHead.BeginTime < new DateTime(2008, 4, 18))
                {
                    balanceHead.BeginTime = new DateTime(2008, 4, 18);
                }
            }
            //int days = 0;
            //if (this.isMidwayBalance == FS.HISFC.Integrate.FeeInterface.EBlanceType.Mid || balanceHead.BalanceType.ID.ToString() == "I")
            //{
            //    days = ((TimeSpan)(balanceHead.EndTime.Date - balanceHead.BeginTime.Date)).Days + 1;
            //}
            //else
            //{
            //    days = ((TimeSpan)(patientInfo.PVisit.OutTime.Date - balanceHead.BeginTime.Date)).Days;
            //}

            //if (days <= 0)
            //{
            //    days = 1;
            //}
            if (this.isMidwayBalance == FS.HISFC.Models.Base.EBlanceType.Mid || balanceHead.BalanceType.ID.ToString() == "I")
            {
                this.lblPriDateIn.Text = balanceHead.BeginTime.ToString("yyyy.MM.dd");
                this.lblPriDateOut.Text = balanceHead.EndTime.ToString("yyyy.MM.dd");
            }
            else
            {
                this.lblPriDateIn.Text = balanceHead.BeginTime.ToString("yyyy.MM.dd");
                this.lblPriDateOut.Text = patientInfo.PVisit.OutTime.ToString("yyyy.MM.dd");
                //this.lblPriDateIn.Text = balanceHead.BeginTime.ToShortDateString() + "��" + patientInfo.PVisit.OutTime.ToShortDateString() + "�� " + days.ToString() + " ��";

            }
            #endregion

            string PactName = "";
            PactName = myInpatient.GetComDictionaryNameByID("PACTUNIT", patientInfo.Pact.ID);
            if (PactName == null)
            {
                MessageBox.Show(myInpatient.Err);
                return -1;
            }

            //this.lblPriPayKind.Text = PactName + " " + balanceHead.Invoice.ID; //��ͬ��λ+���Է�Ʊ��;//[2007/10/22]���ĵ�

            if (patientInfo.Pact.PayKind.ID == "03")
            {
                //���Ѳ�����ʾҽ��֤�ż�������
                //ȡ��ͬ��λ�ı���
                //FS.HISFC.Management.Fee.PactUnitInfo PactManagment = new FS.HISFC.Management.Fee.PactUnitInfo();
                FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                //FS.HISFC.Object.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(patientInfo.Pact.ID);
                FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(patientInfo.Pact.ID);

                if (PactUnitInfo == null)
                {
                    this.lblPriPayKind.Text = patientInfo.SSN + " " + balanceHead.BalanceType.Name.ToString();
                }
                else
                {
                    this.lblPriPayKind.Text = patientInfo.SSN + " "
                        + (PactUnitInfo.Rate.PayRate * 100).ToString() + "% " + balanceHead.BalanceType.Name.ToString();
                }
            }
            else if (patientInfo.Pact.PayKind.ID == "02")
            {
                this.lblPriPayKind.Text = "ҽ��" + balanceHead.BalanceType.Name.ToString();
            }
            else if (patientInfo.Pact.PayKind.ID == "06")
            {
                this.lblPriPayKind.Text = "����ҽ��" + balanceHead.BalanceType.Name.ToString();
            }
            else
            {
                this.lblPriPayKind.Text = balanceHead.BalanceType.Name.ToString();//[2007/10/22]���ĵ�
            }

            //����Ա
            //this.lblPriOper.Text = balanceHead.BalanceOper.ID;
            this.lblPriOper.Text = new FS.HISFC.BizLogic.Manager.Person().GetPersonByID(balanceHead.BalanceOper.ID).ID;

            //Ʊ����Ϣ
            decimal[] feeCostList = new decimal[38];
            string[] feeNameList = new string[38];
            for (int i = 0; i < alBalanceList.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList Blist = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                Blist = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                //��ʾ��������
                if (IsPreview)
                {
                    if (Blist.FeeCodeStat.SortID < 1 || Blist.FeeCodeStat.SortID > 37)
                    {
                        continue;
                    }
                    try
                    {
                        feeNameList[Blist.FeeCodeStat.SortID] = Blist.FeeCodeStat.StatCate.Name;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return -1;
                    }
                }
                //���ý�ֵ
                feeCostList[Blist.FeeCodeStat.SortID] = Blist.BalanceBase.FT.TotCost;
            }
            //1��ҩ��
            lblPriCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[1], 2);
            if (lblPriCost1.Text == "0.00" || lblPriCost1.Text == "")
            {
                lblPreFeeName1.Text = "";
                lblPriCost1.Text = "";
            }
            else
            {
                lblPreFeeName1.Visible = true;
            }
            //2�����
            lblPriCost2.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[2], 2);
            if (lblPriCost2.Text == "0.00" || lblPriCost2.Text == "")
            {
                lblPreFeeName2.Text = "";
                lblPriCost2.Text = "";
            }
            else
            {
                lblPreFeeName2.Visible = true;
            }
            //3��Ѫ��
            lblPriCost3.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[3], 2);
            if (lblPriCost3.Text == "0.00" || lblPriCost3.Text == "")
            {
                lblPreFeeName3.Text = "";
                lblPriCost3.Text = "";
            }
            else
            {
                lblPreFeeName3.Visible = true;
            }
            //4��λ��
            lblPriCost4.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[4], 2);
            if (lblPriCost4.Text == "0.00" || lblPriCost4.Text == "")
            {
                lblPreFeeName4.Text = "";
                lblPriCost4.Text = "";
            }
            else
            {
                lblPreFeeName4.Visible = true;
            }
            //5�г�ҩ
            lblPriCost5.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[5], 2);
            if (lblPriCost5.Text == "0.00" || lblPriCost5.Text == "")
            {
                lblPreFeeName5.Text = "";
                lblPriCost5.Text = "";
            }
            else
            {
                lblPreFeeName5.Visible = true;
            }
            //6����
            lblPriCost6.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[6], 2);
            if (lblPriCost6.Text == "0.00" || lblPriCost6.Text == "")
            {
                lblPreFeeName6.Text = "";
                lblPriCost6.Text = "";
            }
            else
            {
                lblPreFeeName6.Visible = true;
            }
            //7������
            lblPriCost7.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[7], 2);
            if (lblPriCost7.Text == "0.00" || lblPriCost7.Text == "")
            {
                lblPreFeeName7.Text = "";
                lblPriCost7.Text = "";
            }
            else
            {
                lblPreFeeName7.Visible = true;
            }
            //8�����
            lblPriCost8.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[8], 2);
            if (lblPriCost8.Text == "0.00" || lblPriCost8.Text == "")
            {
                lblPreFeeName8.Text = "";
                lblPriCost8.Text = "";
            }
            else
            {
                lblPreFeeName8.Visible = true;
            }
            //9�в�ҩ
            lblPriCost9.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[9], 2);
            if (lblPriCost9.Text == "0.00" || lblPriCost9.Text == "")
            {
                lblPreFeeName9.Text = "";
                lblPriCost9.Text = "";
            }
            else
            {
                lblPreFeeName9.Visible = true;
            }
            //10�������
            lblPriCost10.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[10], 2);
            if (lblPriCost10.Text == "0.00" || lblPriCost10.Text == "")
            {
                lblPreFeeName10.Text = "";
                lblPriCost10.Text = "";
            }
            else
            {
                lblPreFeeName10.Visible = true;
            }
            //11��������
            lblPriCost11.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[11], 2);
            if (lblPriCost11.Text == "0.00" || lblPriCost11.Text == "")
            {
                lblPreFeeName11.Text = "";
                lblPriCost11.Text = "";
            }
            else
            {
                lblPreFeeName11.Visible = true;
            }
            //12��������
            lblPriCost12.Text = FS.FrameWork.Public.String.FormatNumberReturnString(feeCostList[12], 2);
            if (lblPriCost12.Text == "0.00" || lblPriCost12.Text == "")
            {
                lblPreFeeName12.Text = "";
                lblPriCost12.Text = "";
            }
            else
            {
                lblPreFeeName12.Visible = true;
            }

            if (balanceHead.FT.PubCost <= 0)
            {
                this.lblPriPub.Text = string.Empty; //��ҽҽ������
            }
            else
            {
                //���ʽ��
                this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PubCost, 2);
            }

            if ((balanceHead.FT.OwnCost + balanceHead.FT.PayCost) <= 0)
            {
                this.lblPriPay.Text = string.Empty;
            }
            else
            {
                //���˽ɷѽ��
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost
                    + balanceHead.FT.PayCost, 2);
            }

            if (balanceHead.FT.DerateCost > 0)
            {
                this.lblPriPub.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.DerateCost, 2);
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(
                    balanceHead.FT.OwnCost + balanceHead.FT.PayCost - balanceHead.FT.DerateCost, 2);
            }

            if (patientInfo.Pact.PayKind.ID == "03")
            {
                this.lblPriPay.Text = "";
                //���˽ɷѽ�� -- By Maokb 
                //���Ѱ����ԷѺ��Ը�����
                this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost
                    + balanceHead.FT.PayCost, 2) + "(�Ը�:" + FS.FrameWork.Public.String.FormatNumberReturnString(
                    balanceHead.FT.PayCost, 2) + " Ԫ)";
            }
            //if (patientInfo.Pact.ID == "2")
            //{
            //    //���˽ɷѽ��
            //    this.lblPriPay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost + balanceHead.FT.PayCost, 2);
            //}
            //Сд�����ʾȫ��
            this.lblPriLower.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);

            string returnMoney = "";
            string supplyMoney = "";
            //foreach (FS.HISFC.Models.Fee.Inpatient.Balance b in alBalancePay)
            foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList b in alBalanceList)
            {
                if (b.BalanceBase.TransType == FS.HISFC.Models.Base.TransTypes.Positive)//�����
                {
                    if (b.BalanceBase.FT.ReturnCost > 0)//����
                    {
                        //returnMoney = returnMoney + " " + b.BalanceBase.FT.TotCost.ToString();
                        returnMoney = returnMoney + " " + b.BalanceBase.Name + ":" + b.BalanceBase.FT.TotCost.ToString();
                    }
                    else
                    {
                        string payType = "";//2010-01-12 ������ӣ�֧����ʽ��д
                        switch (b.BalanceBase.Name)
                        {
                            case "�ֽ�":
                                payType = "��";
                                break;
                            case "��ǿ�":
                            case "���ÿ�":
                                payType = "��";
                                break;
                            case "֧Ʊ":
                                payType = "֧";
                                break;
                            default:
                                payType = "����";
                                break;
                        }

                        //supplyMoney = supplyMoney + " " + b.BalanceBase.FT.TotCost.ToString();
                        supplyMoney = supplyMoney + " " + payType + b.BalanceBase.FT.TotCost.ToString();

                        supplyMoney = supplyMoney.Trim();
                    }
                }
            }

            if (balanceHead.FT.PrepayCost <= 0)
            {
                this.lblPriPrepay.Text = string.Empty;
            }
            else
            {
                //Ԥ��
                this.lblPriPrepay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PrepayCost, 2);
                //this.lblpayType.Text = returnMoney + supplyMoney;//[2007/12/13]�������ʾ,֧����ʽ��֧���ܶ�Ŀؼ�,����ҽԺ������,��������
            }

            if (balanceHead.FT.SupplyCost <= 0)
            {
                this.lblPriSupply.Text = string.Empty;
            }
            else
            {
                //����
                this.lblPriSupply.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.SupplyCost, 2);// +" (" + supplyMoney + ")";//2010-01-12 ������ӣ���ʾ֧����ʽ
            }

            if (balanceHead.FT.ReturnCost <= 0)
            {
                this.lblPriReturn.Text = string.Empty;
            }
            else
            {
                //�˿�
                this.lblPriReturn.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.ReturnCost, 2);
            }

            string upperMoney = string.Empty;
            string upper = string.Empty;
            //��д��ʾȫ�� - by maokb
            upperMoney = FS.FrameWork.Public.String.FormatNumber(balanceHead.FT.TotCost, 2).ToString();
            upper = ((int)balanceHead.FT.TotCost).ToString().Trim().PadLeft(6, '#');

            char[] dotpos = upperMoney.ToCharArray(upperMoney.IndexOf('.') + 1, 2);
            this.lblPriJ.Text = this.Convert(dotpos[0]);
            this.lblPriF.Text = this.Convert(dotpos[1]);

            char[] zspos = upper.ToCharArray();

            for (int i = 0, j = zspos.Length; i < j; i++)
            {
                switch (i)
                {
                    case 0:
                        this.lblPriSW.Text = this.Convert(zspos[i]);
                        break;
                    case 1:
                        this.lblPriW.Text = this.Convert(zspos[i]);
                        break;
                    case 2:
                        this.lblPriQ.Text = this.Convert(zspos[i]);
                        break;
                    case 3:
                        this.lblPriB.Text = this.Convert(zspos[i]);
                        break;
                    case 4:
                        this.lblPriS.Text = this.Convert(zspos[i]);
                        break;
                    case 5:
                        this.lblPriY.Text = this.Convert(zspos[i]);
                        break;
                }
            }

            this.lblInvoice.Text = balanceHead.Invoice.ID;
            this.lblPriSwBalanceType.Text = balanceHead.PrintedInvoiceNO;
            return 0;
        }

        private string Convert(char ch)
        {
            string[] sNumber = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
            string rets = string.Empty;
            switch (ch)
            {
                case '0':
                    rets = sNumber[0];
                    break;
                case '1':
                    rets = sNumber[1];
                    break;
                case '2':
                    rets = sNumber[2];
                    break;
                case '3':
                    rets = sNumber[3];
                    break;
                case '4':
                    rets = sNumber[4];
                    break;
                case '5':
                    rets = sNumber[5];
                    break;
                case '6':
                    rets = sNumber[6];
                    break;
                case '7':
                    rets = sNumber[7];
                    break;
                case '8':
                    rets = sNumber[8];
                    break;
                case '9':
                    rets = sNumber[9];
                    break;
                default:
                    rets = "��";
                    break;
            }
            return rets;
        }

        #endregion

        #region IBalanceInvoicePrint ��Ա
        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList, ArrayList alPayList)
        {
            return this.SetPrintValue(this.patientInfo, balanceInfo, alBalanceList, true);
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList, ArrayList alPayList)
        {
            return this.SetPrintValue(this.patientInfo, balanceInfo, alBalanceList, false);
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                //type[0]=typeof(FS.HISFC.BizProcess.Integrate.FeeInterface.i)
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy);
                return type;
            }
        }
        #endregion
    }
}
