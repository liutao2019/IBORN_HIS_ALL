using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Registration ;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucBalanceInvoice : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy
    {
        /// <summary>
        ///  ���캯��
        /// </summary>
        public ucBalanceInvoice()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        private System.Data.IDbTransaction trans;
        /// <summary>
        /// ��ӡ�õı�ǩ����
        /// </summary>
        public Collection<Label> lblPrint;
        /// <summary>
        /// Ԥ���õı�ǩ����
        /// </summary>
        public Collection<Label> lblPreview;

        #endregion

        protected FS.HISFC.Models.Base.EBlanceType MidBalanceFlag;

        //��;������

        //protected bool MidBalanceFlag;
        ///// <summary>
        ///// ��;������

        ///// </summary>
        //public bool IsMidwayBalance
        //{
        //    get
        //    {
        //        return MidBalanceFlag;
        //    }
        //    set
        //    {
        //        MidBalanceFlag = value;
        //    }
        //}

       
        


        /// <summary>
        /// ��ӡ�ؼ���ֵ
        /// </summary>
        /// <param name="Pinfo"></param>
        /// <param name="Pinfo"></param>
        /// <param name="al">balancelist����</param>
        /// <param name="IsPreview">�Ƿ��ӡ�������ʾ����</param>
        /// <returns></returns>
        protected int SetPrintValue(
            FS.HISFC.Models.RADT.PatientInfo patientInfo,
            FS.HISFC.Models.Fee.Inpatient.Balance balanceHead,
            ArrayList alBalanceList,
            bool IsPreview)
        {
            this.Controls.Clear();
            //���������ϸΪ�գ��򷵻�
            if (alBalanceList.Count <= 0)
            {
                return -1;
            }

            #region ��¡һ��������ϸ��Ϣ�б���Ϊ���������Ҫ���б�Ԫ����ɾ��������
            ArrayList alBalanceListClone = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList det in alBalanceList)
            {
                alBalanceListClone.Add(det.Clone());
            }
            #endregion

            Control c;
            
            if (this.InvoiceType  == "ZY01")
            {
                c = new ucDianLiZY01();

                this.Controls.Add(c);
                this.Size = c.Size;
                this.InitReceipt();
                SetZY01PrintValue(patientInfo,
                       balanceHead,
                       alBalanceListClone,
                       IsPreview);
            }
            else if (this.InvoiceType == "ZY02")
            {
                c = new ucDianLiZY02();
                this.Controls.Add(c);
                this.Size = c.Size;
                this.InitReceipt();
                SetZY02PrintValue(patientInfo,
                       balanceHead,
                       alBalanceListClone,
                       IsPreview);
            }
            else if (this.InvoiceType == "ZY03")
            {
                c = new ucDianLiZY01();
                this.Controls.Add(c);
                this.Size = c.Size;
                this.InitReceipt();
                SetZY03PrintValue(patientInfo,
                       balanceHead,
                       alBalanceListClone,
                       IsPreview);
            }
            //���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��
            if (IsPreview)
            {
                SetToPreviewMode();
            }
            else
            {
                SetToPrintMode();
            }
            return 0;
        }

        /// <summary>
        /// ��ӡ�ؼ���ֵ
        /// </summary>
        /// <param name="Pinfo"></param>
        /// <param name="Pinfo"></param>
        /// <param name="al">balancelist����</param>
        /// <param name="IsPreview">�Ƿ��ӡ�������ʾ����</param>
        /// <returns></returns>
        protected int SetZY02PrintValue(
            FS.HISFC.Models.RADT.PatientInfo patientInfo,
            FS.HISFC.Models.Fee.Inpatient.Balance balanceHead,
            ArrayList alBalanceList,
            bool IsPreview)
        {
            #region �����Էѷ�Ʊ��ӡ����
            ucDianLiZY02 ucReceipt = (ucDianLiZY02)this.Controls[0];

            #region ҽ�ƻ���
            ucReceipt.lblYiLiaoJiGou.Text = "������������ҽԺ";
            #endregion

            //������Ϣ
            //������
            ucReceipt.lblCaseNO.Text = patientInfo.PID.CaseNO;
            //��������
            ucReceipt.lblOperDate.Text = balanceHead.BalanceOper.OperTime.ToShortDateString();
            //����
            ucReceipt.lblName.Text = patientInfo.Name;

            //סԺ����
            ucReceipt.lblInTime.Text = patientInfo.PVisit.InTime.ToShortDateString();
            ucReceipt.lblOutTime.Text = patientInfo.PVisit.OutTime.ToShortDateString();
            ucReceipt.lblInDay.Text = new TimeSpan(patientInfo.PVisit.OutTime.Ticks - patientInfo.PVisit.InTime.Ticks).Days.ToString();
            ////����
            //ucReceipt.lblDeptName.Text = patientInfo.PVisit.PatientLocation.Dept.Name;

            //����Ա
            ucReceipt.lblOperName.Text = balanceHead.BalanceOper.ID;

            ////����Ա
            //ucReceipt.lblBalanceName.Text = balanceHead.Oper.ID;

            #region ҽ����Ϣ
            decimal GeRenCost = decimal.Zero;
            decimal TongChouCost = decimal.Zero;
            decimal XianJinCost = decimal.Zero;
            decimal GongWuYuanCost = decimal.Zero;
            decimal DaECost = decimal.Zero;
            //�����˻�֧��
            GeRenCost = patientInfo.SIMainInfo.PayCost;
            if (GeRenCost > 0)
            {
                ucReceipt.lblGeRenCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GeRenCost, 2);
            }
            //ͳ�����֧��
            TongChouCost = patientInfo.SIMainInfo.PubCost - patientInfo.SIMainInfo.OverCost - patientInfo.SIMainInfo.OfficalCost;
            if (TongChouCost > 0)
            {
                ucReceipt.lblTongChouCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(TongChouCost, 2);
            }
            //�ֽ�֧��
            XianJinCost = patientInfo.SIMainInfo.OwnCost;
            if (XianJinCost > 0)
            {
                ucReceipt.lblXianJinCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(XianJinCost, 2);
            }
            //����Ա����
            //GongWuYuanCost = patientInfo.SIMainInfo.OfficalCost;
            //if (GongWuYuanCost > 0)
            //{
            //    ucReceipt.lblGongWuYuanCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GongWuYuanCost, 2);
            //}
            //����
            DaECost = patientInfo.SIMainInfo.OverCost;
            if (DaECost > 0)
            {
                ucReceipt.lblDaECost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(DaECost, 2);
            }
            #endregion
            //Ʊ����Ϣ
            decimal[] FeeInfo =
                //---------------------1-----------2------------3------------4-------------5-----------------
                new decimal[23]{decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero};


            for (int i = 0; i < alBalanceList.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList detail = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                detail = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
                {
                    FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
                }
            }
            int FeeInfoIndex = 0;
            foreach (decimal d in FeeInfo)
            {
                Label l = Function.GetFeeNameLable("lblFeeInfo" + FeeInfoIndex.ToString(), ucReceipt);
                if (l != null)
                {
                    if (FeeInfo[FeeInfoIndex] > 0)
                    {
                        l.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2);
                    }
                }
                FeeInfoIndex++;
            }
            //Ԥ��
            ucReceipt.lblPriPrepay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PrepayCost, 2);
            //����
            ucReceipt.lblPriSupply.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.SupplyCost, 2);
            //�˿�
            ucReceipt.lblPriReturn.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.ReturnCost, 2);

            ucReceipt.lblReceiptNo.Text = balanceHead.Invoice.BeginNO;
            if (balanceHead.FT.TotCost > 0)
            {
                ucReceipt.lblTotCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);
            }
            #region ��д�ܽ��

            ucReceipt.lblDaXie.Text = Function.GetUpperCashByNumber(FS.FrameWork.Public.String.FormatNumber(balanceHead.FT.TotCost, 2));

            #endregion
            #endregion

            return 0;
        } /// <summary>
        /// ��ӡ�ؼ���ֵ
        /// </summary>
        /// <param name="Pinfo"></param>
        /// <param name="Pinfo"></param>
        /// <param name="al">balancelist����</param>
        /// <param name="IsPreview">�Ƿ��ӡ�������ʾ����</param>
        /// <returns></returns>
        protected int SetZY03PrintValue(
            FS.HISFC.Models.RADT.PatientInfo patientInfo,
            FS.HISFC.Models.Fee.Inpatient.Balance balanceHead,
            ArrayList alBalanceList,
            bool IsPreview)
        {
            #region �����Էѷ�Ʊ��ӡ����
            ucDianLiZY03 ucReceipt = (ucDianLiZY03)this.Controls[0];

            #region ҽ�ƻ���
            ucReceipt.lblYiLiaoJiGou.Text =  "������������ҽԺ";
            #endregion

            //������Ϣ
            //������
            ucReceipt.lblCaseNO.Text = patientInfo.PID.CaseNO;
            //��������
            ucReceipt.lblOperDate.Text = balanceHead.BalanceOper.OperTime.ToShortDateString();
            //����
            ucReceipt.lblName.Text = patientInfo.Name;
            //סԺ����
            ucReceipt.lblInTime.Text = balanceHead.BeginTime.ToShortDateString();
            ucReceipt.lblOutTime.Text = balanceHead.EndTime.ToShortDateString();
            ucReceipt.lblInDay.Text = new TimeSpan(balanceHead.EndTime.Ticks - balanceHead.BeginTime.Ticks).Days.ToString();
            ////����
            //ucReceipt.lblDeptName.Text = patientInfo.PVisit.PatientLocation.Dept.Name;

            //����Ա
            ucReceipt.lblOperName.Text = balanceHead.BalanceOper.ID;

            ////����Ա
            //ucReceipt.lblBalanceName.Text = balanceHead.Oper.ID;

            #region ҽ����Ϣ
            decimal GeRenCost = decimal.Zero;
            decimal TongChouCost = decimal.Zero;
            decimal XianJinCost = decimal.Zero;
            decimal GongWuYuanCost = decimal.Zero;
            decimal DaECost = decimal.Zero;
            ////�����˻�֧��
            //GeRenCost = patientInfo.SIMainInfo.PayCost;
            //if (GeRenCost > 0)
            //{
            //    ucReceipt.lblGeRenCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GeRenCost, 2);
            //}
            //ͳ�����֧��
            TongChouCost = patientInfo.SIMainInfo.PubCost - patientInfo.SIMainInfo.OverCost - patientInfo.SIMainInfo.OfficalCost;
            if (TongChouCost > 0)
            {
                ucReceipt.lblTongChouCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(TongChouCost, 2);
            }
            //�ֽ�֧��
            XianJinCost = patientInfo.SIMainInfo.OwnCost;
            if (XianJinCost > 0)
            {
                ucReceipt.lblXianJinCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(XianJinCost, 2);
            }
            ////����Ա����
            //GongWuYuanCost = patientInfo.SIMainInfo.OfficalCost;
            //if (GongWuYuanCost > 0)
            //{
            //    ucReceipt.lblGongWuYuanCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GongWuYuanCost, 2);
            //}
            ////����
            //DaECost = patientInfo.SIMainInfo.OverCost;
            //if (DaECost > 0)
            //{
            //    ucReceipt.lblDaECost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(DaECost, 2);
            //}
            #endregion
            //Ʊ����Ϣ
            decimal[] FeeInfo =
                //---------------------1-----------2------------3------------4-------------5-----------------
                new decimal[21]{decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero};


            for (int i = 0; i < alBalanceList.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList detail = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                detail = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
                {
                    FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
                }
            }
            int FeeInfoIndex = 0;
            foreach (decimal d in FeeInfo)
            {
                Label l = Function.GetFeeNameLable("lblFeeInfo" + FeeInfoIndex.ToString(), ucReceipt);
                if (l != null)
                {
                    if (FeeInfo[FeeInfoIndex] > 0)
                    {
                        l.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2);
                    }
                }
                FeeInfoIndex++;
            }
            //Ԥ��
            ucReceipt.lblPriPrepay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PrepayCost, 2);
            //����
            ucReceipt.lblPriSupply.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.SupplyCost, 2);
            //�˿�
            ucReceipt.lblPriReturn.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.ReturnCost, 2);

            ucReceipt.lblReceiptNo.Text = balanceHead.Invoice.BeginNO;
            if (balanceHead.FT.TotCost > 0)
            {
                ucReceipt.lblTotCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);
            }
            #region ��д�ܽ��

            ucReceipt.lblDaXie.Text = Function.GetUpperCashByNumber(FS.FrameWork.Public.String.FormatNumber(balanceHead.FT.TotCost, 2));

            #endregion
            #endregion

            return 0;
        }
       

        /// <summary>
        /// ��ӡ�ؼ���ֵ
        /// </summary>
        /// <param name="Pinfo"></param>
        /// <param name="Pinfo"></param>
        /// <param name="al">balancelist����</param>
        /// <param name="IsPreview">�Ƿ��ӡ�������ʾ����</param>
        /// <returns></returns>
        protected int SetZY01PrintValue(
            FS.HISFC.Models.RADT.PatientInfo patientInfo,
            FS.HISFC.Models.Fee.Inpatient.Balance balanceHead,
            ArrayList alBalanceList,
            bool IsPreview)
        {
            #region �����Էѷ�Ʊ��ӡ����
            ucDianLiZY01 ucReceipt = (ucDianLiZY01)this.Controls[0];
            //#region ҽ�ƻ���
            //ucReceipt.lblYiLiaoJiGou.Text = "������������ҽԺ";
            //#endregion
            //������Ϣ
            ucReceipt.lblCaseNO.Text = patientInfo.PID.CaseNO;  //������
            ucReceipt.lblPrintYear.Text = balanceHead.BalanceOper.OperTime.Year.ToString(); //��
            ucReceipt.lblPrintMonth.Text = balanceHead.BalanceOper.OperTime.Month.ToString(); //��
            ucReceipt.lblPrintDay.Text = balanceHead.BalanceOper.OperTime.Day.ToString(); //��
            //ucReceipt.lblName.Text = patientInfo.Name;//����
            //סԺ����
            ucReceipt.lblInTime.Text = patientInfo.PVisit.InTime.ToShortDateString();
            ucReceipt.lblOutTime.Text = patientInfo.PVisit.OutTime.ToShortDateString();
            ucReceipt.lblInDay.Text = new TimeSpan(patientInfo.PVisit.OutTime.Ticks - patientInfo.PVisit.InTime.Ticks).Days.ToString();
            //����
            //ucReceipt.lblDeptName.Text = patientInfo.PVisit.PatientLocation.Dept.Name;

            //����Ա
            ucReceipt.lblOperName.Text = balanceHead.BalanceOper.ID;

            //����Ա
            //ucReceipt.lblBalanceName.Text = balanceHead.Oper.ID;

            //Ʊ����Ϣ
            decimal[] FeeInfo =
                //---------------------1-----------2------------3------------4-------------5-----------------
                new decimal[18]{decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,
                                decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero};


            for (int i = 0; i < alBalanceList.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList detail = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                detail = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
                {
                    FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
                }
            }
            int FeeInfoIndex = 0;
            foreach (decimal d in FeeInfo)
            {
                Label l = Function.GetFeeNameLable("lblFeeInfo" + FeeInfoIndex.ToString(), ucReceipt);
                if (l != null)
                {
                    if (FeeInfo[FeeInfoIndex]>0)
                    {
                        l.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2); 
                    }
                }
                FeeInfoIndex++;
            }
            //Ԥ��
            if (balanceHead.FT.PrepayCost>0)
            {
                ucReceipt.lblPriPrepay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PrepayCost, 2); 
            }
            //ʵ��
            if (balanceHead.FT.SupplyCost>0)
            {
                ucReceipt.lblPriSupply.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.OwnCost, 2); 
            }
            //�˿�
            if (balanceHead.FT.ReturnCost>0)
            {
                ucReceipt.lblPriReturn.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.ReturnCost, 2); 
            }
            //Сд�ܽ��
            if (balanceHead.FT.TotCost > 0)
            {
                ucReceipt.lblTotCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost , 2);
                ucReceipt.lblSum.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);
            }
            #region ��д�ܽ��
            ucReceipt.lblDaXie.Text = Function.GetUpperCashByNumber(FS.FrameWork.Public.String.FormatNumber(balanceHead.FT.TotCost, 2));
            #endregion
            #endregion
            return 0;
        }

        #region IBalanceInvoicePrint ��Ա

        public int Clear()
        {
            SetLableText(null, lblPrint);
            return 1;
        }
        public int PrintPreview()
        {

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPreview(0, 0, this);
            return 1;
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            //����ֽ�Ŵ�С
            //FS.Common.Class.Function.GetPageSize("bill", ref Print);

            print.PrintPage(0, 0, this);

            return 1;
        }

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
        }
        //{1FADBEC0-514A-46f0-9A4B-037F5B65892A}
        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList,ArrayList alPayList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, true);
        }
        //{1FADBEC0-514A-46f0-9A4B-037F5B65892A}
        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, ArrayList alBalanceList, ArrayList alPayList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, false);
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public IDbTransaction Trans
        {
            set { this.trans = value; }
            get { return this.trans ; }
        }
        /// <summary>
        /// ����Ϊ��ӡģʽ
        /// </summary>
        public void SetToPrintMode()
        {
            //��Ԥ������Ϊ���ɼ�
            SetLableVisible(false, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }
        /// <summary>
        /// ����ΪԤ��ģʽ
        /// </summary>
        public void SetToPreviewMode()
        {
            //��Ԥ������Ϊ�ɼ�
            SetLableVisible(true, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }
        /// <summary>
        /// ���ñ�ǩ���ϵĿɼ���
        /// </summary>
        /// <param name="v">�Ƿ�ɼ�</param>
        /// <param name="l">��ǩ����</param>
        private void SetLableVisible(bool v, Collection<Label> l)
        {
            if (l == null || l.Count == 0) return;
            foreach (Label lbl in l)
            {
                lbl.Visible = v;
            }
        }
        /// <summary>
        /// ��ʼ���վ�
        /// </summary>
        /// <remarks>
        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
        /// </remarks>
        private void InitReceipt()
        {

            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            foreach (Control c in this.Controls[0].Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            l.Text = "";
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }

        /// <summary>
        /// ���ô�ӡ���ϵ�ֵ
        /// </summary>
        /// <param name="t">ֵ����</param>
        /// <param name="l">��ǩ����</param>
        private void SetLableText(string[] t, Collection<Label> l)
        {
            if (l == null || l.Count == 0) return;
            foreach (Label lbl in l)
            {
                lbl.Text = "";
            }
            if (t != null)
            {
                if (t.Length <= l.Count)
                {
                    int i = 0;
                    foreach (string s in t)
                    {
                        l[i].Text = s;
                        i++;
                    }
                }
                else
                {
                    if (t.Length > l.Count)
                    {
                        int i = 0;
                        foreach (Label lbl in l)
                        {
                            lbl.Text = t[i];
                            i++;
                        }
                    }
                }
            }
        }
        #endregion

        #region IBalanceInvoicePrint ��Ա

        private string _invoiceType;

        public string InvoiceType
        {
            get { return _invoiceType; }
        }

        private FS.HISFC.Models.RADT.PatientInfo _patientInfo;
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set { _patientInfo = value;
            if (_patientInfo.Pact.PayKind.ID=="01")
            {
                _invoiceType = "ZY01";
            }
            else if (_patientInfo.Pact.PayKind.ID=="02")
            {
                _invoiceType = "ZY02";
                if (_patientInfo.PVisit.MedicalType.ID=="42" || 
                    _patientInfo.PVisit.MedicalType.ID=="44" ||
                    _patientInfo.PVisit.MedicalType.ID=="45" )
                {
                    _invoiceType = "ZY03";
                }
            }
            }
        }

        #endregion

        #region IBalanceInvoicePrintmy ��Ա
        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.EBlanceType IsMidwayBalance
        {
            get
            {
                return MidBalanceFlag;
            }
            set
            {
                MidBalanceFlag = value;
            }
        }

        #endregion
    }
}