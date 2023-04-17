using System;
using System.Collections;
using System.Text;
using FS.FrameWork.Models;
using FS.FrameWork.Function;
using FS.HISFC.Models.Registration;
using System.Data;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.BizProcess.Interface.FeeInterface;

namespace FS.HISFC.Components.OutpatientFee.Class
{
    public class Function
    {
        #region ��̬����

        /// <summary>
        /// ���Ʋ���������
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper controlerHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        public static FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// �Ƿ��˷�    {18B0895D-9F55-4d93-B374-69E96F296D0D}
        /// </summary>
        private static bool isQuitFee = false;

        /// <summary>
        /// �Ƿ��˷�    {18B0895D-9F55-4d93-B374-69E96F296D0D}
        /// </summary>
        public static bool IsQuitFee
        {
            get
            {
                return isQuitFee;
            }
            set
            {
                isQuitFee = value;
            }
        }

        #endregion

        #region �ַ�Ʊ

        /// <summary>
        /// ��Ʊ����, ����������
        /// </summary>
        /// <param name="cost">��ǰ���</param>
        /// <returns>�����ý��</returns>
        public static decimal DealCent(decimal cost)
        {
            return DealCent(cost, null);
        }
        /// <summary>
        /// ��Ʊ���� ��������
        /// </summary>
        /// <param name="cost">��ǰ���</param>
        /// <param name="t">���ݿ�����</param>
        /// <returns>�����ý��</returns>
        public static decimal DealCent(decimal cost, FS.FrameWork.Management.Transaction t)
        {
            FS.FrameWork.Management.ControlParam myCtrl = new FS.FrameWork.Management.ControlParam();
            if (t != null)
            {
                myCtrl.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }
            string conValue = "0";//myCtrl.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);
            if (controlerHelper == null || controlerHelper.ArrayObject == null || controlerHelper.ArrayObject.Count <= 0)
            {
                conValue = myCtrl.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = controlerHelper.GetObjectFromID(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);

                if (obj == null)
                {
                    conValue = myCtrl.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.Const.CENTRULE);
                }
                else
                {
                    conValue = ((FS.HISFC.Models.Base.ControlParam)obj).ControlerValue;
                }
            }
            if (conValue == null || conValue == "" || conValue == "-1")
            {
                conValue = "0";//Ĭ�ϲ�����
            }
            decimal dealedCost = 0;

            switch (conValue)
            {
                case "0": //������
                    dealedCost = cost;
                    break;
                case "1": //��������
                    dealedCost = FS.FrameWork.Public.String.FormatNumber(cost, 1);
                    break;
                case "2": //��ȡ��
                    dealedCost = cost * 10;
                    if (dealedCost != Decimal.Truncate(dealedCost))
                    {
                        dealedCost = Decimal.Truncate(dealedCost) + 1;
                    }
                    dealedCost = dealedCost / 10;
                    break;
                case "3": //��ȡ��
                    dealedCost = cost * 10;
                    dealedCost = Decimal.Truncate(dealedCost) / 10;
                    break;

            }
            return dealedCost;
        }

        #endregion

        #region �����

        /// <summary>
        /// �������Ϣ
        /// </summary>
        /// <param name="sender">�����farpointSheetView</param>
        /// <param name="column">��˳��</param>
        /// <param name="DrawColumn">����˳��</param>
        /// <param name="ChildViewLevel"></param>
        public static void DrawCombo(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "��"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //��ͷ
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "��";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "��";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "��";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "";
                            o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "��"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //��ͷ
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "��";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "��";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "��";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "";
                                c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }

        #endregion

        #region �γ����﷢Ʊ

        /// <summary>
        /// ���Ʋ�����
        /// </summary>
        protected static FS.FrameWork.Management.ControlParam controlManager = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// ƽ�ⷢƱ��ʾ����
        /// </summary>
        /// <param name="feeItemLists"></param>
        public static FS.HISFC.Models.Base.FT DecDifInvoiceData(ArrayList feeItemLists)
        {
            #region ƽ�ⷢƱ����

            FS.HISFC.Models.Base.FT FT = new FS.HISFC.Models.Base.FT();

            decimal tempOwnCost = 0m;
            decimal tempPayCost = 0m;
            decimal tempPubCost = 0m;

            Hashtable hsFeeDifRate = new Hashtable();
            ArrayList alDifRate = new ArrayList();

            foreach (FeeItemList f in feeItemLists)
            {
                if (hsFeeDifRate.Contains(f.NewItemRate))
                {
                    (hsFeeDifRate[f.NewItemRate] as ArrayList).Add(f.FT);
                }
                else
                {
                    alDifRate.Add(f.NewItemRate);

                    ArrayList al = new ArrayList();

                    al.Add(f.FT);

                    hsFeeDifRate.Add(f.NewItemRate, al);
                }
            }

            for (int i = 0; i < alDifRate.Count; i++)
            {
                decimal rate = (decimal)alDifRate[i];

                ArrayList alCost = hsFeeDifRate[rate] as ArrayList;

                decimal tmpTot = 0m;
                decimal tmpPay = 0m;
                decimal tmpPub = 0m;

                for (int j = 0; j < alCost.Count; j++)
                {
                    FS.HISFC.Models.Base.FT ft = alCost[j] as FS.HISFC.Models.Base.FT;

                    tempOwnCost += ft.OwnCost;

                    tmpTot += ft.PayCost + ft.PubCost;
                }

                tmpPay = FS.FrameWork.Public.String.FormatNumber(tmpTot * rate, 2);

                tmpPub = tmpTot - tmpPay;

                tempPayCost += tmpPay;

                tempPubCost += tmpPub;
            }

            FT.OwnCost = tempOwnCost;
            FT.PayCost = tempPayCost;
            FT.PubCost = tempPubCost;

            return FT;

            #endregion
        }

        /// <summary>
        /// ��������Ʊ��Ϣ
        /// </summary>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <param name="register">�Һ���Ϣ</param>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <param name="realInvoiceNO">ʵ�ʷ�Ʊ��</param>
        /// <param name="invoiceFlag">��Ʊ���</param>
        /// <param name="splitFlag">�ַ�Ʊ��־</param>
        /// <returns></returns>
        private static Balance MakeInvoiceInfo(ArrayList feeItemLists, Register register, string invoiceNO,
            string realInvoiceNO, string invoiceFlag, string splitFlag)
        {
            Balance invoice = new Balance();
            decimal totCost = 0;//�ܽ��
            decimal ownCost = 0;//�Էѽ��
            decimal pubCost = 0;//���ʽ��
            decimal payCost = 0;//�Ը����
            //{37245321-1E0D-4a29-BF2A-54FED778C602}
            decimal rebateCost = 0;//�Żݼ۸� by niuxy

            if (invoiceFlag == "1")//�Էѷ�Ʊ
            {

                foreach (FeeItemList f in feeItemLists)
                {
                    ownCost += f.FT.OwnCost;
                    //payCost += f.FT.PayCost;---------del by xf 20120710 �Էѷ�Ʊֻ��OwnCost
                    //pubCost += f.FT.PubCost;---------del by xf 20120710 �Էѷ�Ʊֻ��OwnCost
                    //{37245321-1E0D-4a29-BF2A-54FED778C602}
                    //������� by niuxy
                    rebateCost += f.FT.RebateCost;
                }
                totCost = ownCost + payCost + pubCost;
            }
            if (splitFlag == "0")//��ҽ
            {
                if (invoiceFlag == "2")//���ʷ�Ʊ,��ʱ����д,�Ժ�Ҫ���ǹ��ѵ��㷨
                {
                    foreach (FeeItemList f in feeItemLists)
                    {
                        payCost += f.FT.PayCost;
                        pubCost += f.FT.PubCost;
                        //������� by niuxy
                        //{37245321-1E0D-4a29-BF2A-54FED778C602}
                        rebateCost += f.FT.RebateCost;
                    }
                    totCost = payCost + pubCost;
                }
                if (invoiceFlag == "3")//���ⷢƱ,��ʱ����д,�Ժ�Ҫ����.
                {
                    foreach (FeeItemList f in feeItemLists)
                    {
                        payCost += f.FT.PayCost;
                        pubCost += f.FT.PubCost;
                        //������� by niuxy{37245321-1E0D-4a29-BF2A-54FED778C602}
                        rebateCost += f.FT.RebateCost;
                    }
                    totCost = payCost + pubCost;
                }
            }
            if (splitFlag == "1")//��ɽ
            {
                if (invoiceFlag == "2")//���ʷ�Ʊ,��ʱ����д,�Ժ�Ҫ���ǹ��ѵ��㷨
                {
                    foreach (FeeItemList f in feeItemLists)
                    {
                        ownCost += f.FT.OwnCost;
                        payCost += f.FT.PayCost;
                        pubCost += f.FT.PubCost;
                        //������� by niuxy{37245321-1E0D-4a29-BF2A-54FED778C602}
                        rebateCost += f.FT.RebateCost;
                    }
                    totCost = ownCost + payCost + pubCost;
                }
                if (invoiceFlag == "3")//���ⷢƱ,��ʱ����д,�Ժ�Ҫ����.
                {
                    foreach (FeeItemList f in feeItemLists)
                    {
                        ownCost += f.FT.OwnCost;
                        payCost += f.FT.PayCost;
                        pubCost += f.FT.PubCost;
                        //������� by niuxy{37245321-1E0D-4a29-BF2A-54FED778C602}
                        rebateCost += f.FT.RebateCost;
                    }
                    totCost = ownCost + payCost + pubCost;
                }
            }
            if (invoiceFlag == "4")//ҽ����Ʊ,��ʱ��ôд,��Ϊ��֪����ôд.
            {
                foreach (FeeItemList f in feeItemLists)
                {
                    ownCost += f.FT.OwnCost;
                    payCost += f.FT.PayCost;
                    pubCost += f.FT.PubCost;
                    //������� by niuxy{37245321-1E0D-4a29-BF2A-54FED778C602}
                    rebateCost += f.FT.RebateCost;
                }
                totCost = ownCost + payCost + pubCost;
            }
            if (invoiceFlag == "5")//���з�Ʊ,����ǹ��ѻ�������Ҫд�㷨.
            {
                foreach (FeeItemList f in feeItemLists)
                {
                    payCost += f.FT.PayCost;
                    ownCost += f.FT.OwnCost;
                    pubCost += f.FT.PubCost;
                    //������� by niuxy{37245321-1E0D-4a29-BF2A-54FED778C602}
                    rebateCost += f.FT.RebateCost;
                }
                totCost = ownCost + payCost + pubCost;
            }
            #region ����ϸ��Ʊ�Ÿ�ֵ 
            if (isQuitFee)
            {
                foreach (FeeItemList f in feeItemLists)
                {
                    f.Invoice.ID = invoiceNO;
                }
            }
            #endregion

            invoice.Invoice.ID = invoiceNO;
            invoice.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            invoice.Patient = register.Clone();

            invoice.FT.OwnCost = ownCost;
            invoice.FT.PayCost = payCost;
            invoice.FT.PubCost = pubCost;
            invoice.FT.TotCost = totCost;
            invoice.FT.RebateCost = rebateCost;
            invoice.User01 = rebateCost.ToString();
            string tempExamineFlag = null;
            if (register.ChkKind.Length > 0)
            {
                tempExamineFlag = register.ChkKind;
            }
            else
            {
                tempExamineFlag = "0";
            }
            invoice.ExamineFlag = tempExamineFlag;
            invoice.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
            invoice.CanceledInvoiceNO = "";
            invoice.IsDayBalanced = false;
            invoice.Memo = invoiceFlag;
            invoice.PrintTime = outpatientManager.GetDateTimeFromSysDateTime();
            invoice.PrintedInvoiceNO = realInvoiceNO;

            //FS.HISFC.Models.Base.FT tempFT = DecDifInvoiceData(feeItemLists);

            //invoice.FT.OwnCost = tempFT.OwnCost;
            //invoice.FT.PayCost = tempFT.PayCost;
            //invoice.FT.PubCost = tempFT.PubCost;

            return invoice;
        }

        /// <summary>
        /// ��շ�Ʊͳ�ƴ���ķ���,����һ�ŷ�Ʊʹ��.
        /// </summary>
        /// <param name="table"></param>
        private static void ResetInvoiceTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                row["TOT_COST"] = 0;
                row["OWN_COST"] = 0;
                row["PAY_COST"] = 0;
                row["PUB_COST"] = 0;
            }
        }

        /// <summary>
        /// ���ɷ�Ʊ��ϸ
        /// </summary>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <param name="register">�Һ���Ϣ</param>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <param name="dtInvoice">��Ʊ����</param>
        /// <param name="invoiceFlag">��Ʊ��־</param>
        /// <param name="splitType">�ַ�Ʊ��־</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ�: ��Ʊ��ϸ���� ʧ�� null</returns>
        private static ArrayList MakeInvoiceDetail(ArrayList feeItemLists, Register register, string invoiceNO, DataTable dtInvoice, string invoiceFlag, string splitType, ref string errText)
        {
            ArrayList balanceLists = new ArrayList();

            foreach (FeeItemList f in feeItemLists)
            {
                DataRow rowFind = dtInvoice.Rows.Find(new object[] { f.Item.MinFee.ID });
                if (rowFind == null)
                {
                    errText = "��С����Ϊ" + f.Item.MinFee.ID + "����С����û����MZ01�ķ�Ʊ������ά��";
                    return null;
                }
                if (invoiceFlag == "1")//�Էѷ�Ʊ
                {
                    rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.OwnCost;
                    rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + f.FT.OwnCost;
                    //rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + f.FT.PayCost;-----del by xf 20120710
                    //rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + f.FT.PubCost;-----del by xf 20120710
                    rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + 0;//-----add by xf 20120710
                    rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + 0;//-----add by xf 20120710

                }
                if (splitType == "0")//��ҽ
                {
                    if (invoiceFlag == "2" || invoiceFlag == "3")//���ʷ�Ʊ,���ⷢƱ
                    {
                        rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.PayCost + f.FT.PubCost;
                        rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + 0;
                        rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + f.FT.PayCost;
                        rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + f.FT.PubCost;
                    }
                }
                if (splitType == "1")//��ɽ
                {
                    if (invoiceFlag == "2" || invoiceFlag == "3")//���ʷ�Ʊ,���ⷢƱ
                    {
                        rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.PayCost + f.FT.PubCost + f.FT.OwnCost;
                        rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + f.FT.OwnCost;
                        rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + f.FT.PayCost;
                        rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + f.FT.PubCost;
                    }
                }
                if (invoiceFlag == "4")//ҽ����Ʊ
                {
                    rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.PayCost + f.FT.PubCost + f.FT.OwnCost;
                    rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + f.FT.OwnCost;
                    rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + f.FT.PayCost;
                    rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + f.FT.PubCost;
                }

                if (invoiceFlag == "5")
                {
                    rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.PayCost + f.FT.PubCost + f.FT.OwnCost;
                    rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + f.FT.OwnCost;
                    rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + f.FT.PayCost;
                    rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + f.FT.PubCost;
                }

                //���ñ��¼
                f.FeeCodeStat.ID = rowFind["FEE_STAT_CATE"].ToString();
                f.FeeCodeStat.Name = rowFind["FEE_STAT_NAME"].ToString();
                f.FeeCodeStat.SortID = NConvert.ToInt32(rowFind["SEQ"].ToString());

            }

            BalanceList detail = null;//��Ʊ��ϸʵ��

            for (int i = 1; i < 100; i++)
            {
                //�ҵ���ͬ��ӡ���,��ͬһͳ�����ķ��ü���
                DataRow[] rowFind = dtInvoice.Select("SEQ = " + i.ToString(), "SEQ ASC");
                //���û���ҵ�˵���Ѿ��ҹ������Ĵ�ӡ���,���з����Ѿ��ۼ����.
                if (rowFind.Length == 0)
                {

                }
                else
                {
                    detail = new BalanceList();
                    detail.BalanceBase.Invoice.ID = invoiceNO;
                    detail.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    detail.InvoiceSquence = i;
                    detail.FeeCodeStat.ID = rowFind[0]["FEE_STAT_CATE"].ToString();
                    detail.FeeCodeStat.Name = rowFind[0]["FEE_STAT_NAME"].ToString();
                    ///liuq
                    ///2007-8-20�޸ģ������ӡ��ŵ�ʵ�塣
                    ///----------------------------------------------------
                    detail.FeeCodeStat.SortID = NConvert.ToInt32(rowFind[0]["SEQ"].ToString());
                    ///---------------------------------------------------
                    detail.BalanceBase.IsDayBalanced = false;
                    detail.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    detail.Memo = invoiceFlag;
                    foreach (DataRow row in rowFind)
                    {
                        detail.BalanceBase.FT.PubCost += NConvert.ToDecimal(row["PUB_COST"].ToString());
                        detail.BalanceBase.FT.OwnCost += NConvert.ToDecimal(row["OWN_COST"].ToString());
                        detail.BalanceBase.FT.PayCost += NConvert.ToDecimal(row["PAY_COST"].ToString());
                    }
                    detail.BalanceBase.FT.TotCost = detail.BalanceBase.FT.PubCost + detail.BalanceBase.FT.OwnCost + detail.BalanceBase.FT.PayCost;

                    //�������Ϊ0 ˵����ͳ�����(��ӡ���)��û�з���
                    //��������������ã���ʱ���Σ�С��0Ҳ���Դ�ӡ�ڷ�Ʊ��{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
                    if (detail.BalanceBase.FT.TotCost == 0)
                    {
                        continue;
                    }
                    detail.BalanceBase.RecipeOper.Dept.ID = register.SeeDoct.Dept.ID;
                    detail.BalanceBase.RecipeOper.Dept.Name = register.SeeDoct.Dept.Name;
                    if (string.IsNullOrEmpty(detail.BalanceBase.RecipeOper.Dept.ID))
                    {
                        detail.BalanceBase.RecipeOper.Dept.ID = register.DoctorInfo.Templet.Dept.ID;
                        detail.BalanceBase.RecipeOper.Dept.Name = register.DoctorInfo.Templet.Dept.Name;
                    }

                    balanceLists.Add(detail);
                }
            }

            return balanceLists;
        }

        /// <summary>
        /// ������ϸ�γ����﷢Ʊ�ͷ�Ʊ��ϸ
        /// </summary>
        /// <param name="feeIntegrate">����ҵ���</param>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <param name="register">�Һ���Ϣ</param>
        /// <param name="invoiceNO">��Ʊ�ŵ���</param>
        /// <param name="realInvoiceNO">��Ʊ�Ŵ�ӡ��</param>
        /// <param name="dtInvoice">��Ʊͳ�ƴ���MZ01</param>
        /// <param name="invoiceFlag">��Ʊ��� 1 �Է� 2 ���� 3 ���� 4 ҽ��</param>
        /// <param name="balance">��Ʊ����ʵ��</param>
        /// <param name="balanceLists">��Ʊ��ϸ����</param>
        /// <param name="splitType">�ַ�Ʊ���</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private static int MakeInvoiceAndDetail(FS.HISFC.BizProcess.Integrate.Fee feeIntegrate, ArrayList feeItemLists, Register register, ref string invoiceNO, ref string realInvoiceNO, DataTable dtInvoice,
            string invoiceFlag, ref Balance balance, ref ArrayList balanceLists, string splitType, ref string errText, int countI)
        {
            //{1C257F28-1A60-437d-9E61-A6A3D99357EE}
            string invoiceType = feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, "0");
            FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
            employee.ID = FS.FrameWork.Management.Connection.Operator.ID;
            if (invoiceType == "2")
            {
                //invoiceNO = (NConvert.ToInt32(invoiceNO) + 1).ToString().PadLeft(12, '0');

                #region {3E09A62D-504B-4490-80A1-256F021B1ABD}

                //{18B0895D-9F55-4d93-B374-69E96F296D0D}  ����ȡ��Ʊ������Bug����
                //if (!isQuitFee)
                //{
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i <= countI; i++)
                {
                    int iReturnValue = feeIntegrate.GetInvoiceNO(employee, "C", ref invoiceNO, ref realInvoiceNO, ref errText);
                    //realInvoiceNO = invoiceNO;
                    if (iReturnValue == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //MessageBox.Show(errText);

                        return -1;
                    }
                }


                FS.FrameWork.Management.PublicTrans.RollBack();

                //}


                #endregion

            }
            //�γ�����Ʊ
            balance = MakeInvoiceInfo(feeItemLists, register, invoiceNO, realInvoiceNO, invoiceFlag, splitType);

            if (balance.FT.TotCost < 0)
            {
                return -2;
            }
            //��շ�Ʊͳ�ƴ���ķ��úϼ�.
            ResetInvoiceTable(dtInvoice);
            //�γɷ�Ʊ��ϸ
            ArrayList tempBalanceLists = MakeInvoiceDetail(feeItemLists, register, invoiceNO, dtInvoice, invoiceFlag, splitType, ref errText);
            if (tempBalanceLists == null)
            {
                return -1;
            }
            //���Էѷ�Ʊ���뷢Ʊ��ϸ����.
            balanceLists.Add(tempBalanceLists);

            //��ϸ���¸�ֵ��Ʊ 
            if (isQuitFee)
            {
                foreach (FeeItemList f in feeItemLists)
                {
                    f.Invoice = balance.Invoice;
                }
            }

            try
            {
                switch (invoiceType)
                {

                    case "1":
                    case "3"://��ɽ
                        int len = invoiceNO.Length;
                        string orgInvoice = invoiceNO.Substring(0, len - 4);
                        string addInvoice = invoiceNO.Substring(len - 4, 4);
                        invoiceNO = orgInvoice + (NConvert.ToInt32(addInvoice) + 1).ToString().PadLeft(4, '0');
                        realInvoiceNO = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1); 
                        break;
                    default:
                        invoiceNO = FS.FrameWork.Public.String.AddNumber(invoiceNO, 1);
                        realInvoiceNO = invoiceNO;
                        break;
                }
            }
            catch (Exception e)
            {
                errText = e.Message;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ������ϸ���ɷ�Ʊ�ͷ�Ʊ��ϸ��Ϣ
        /// </summary>
        /// <param name="feeIntegrate">����ҵ���</param>
        /// <param name="register">���߹ҺŻ�����Ϣ</param>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <param name="invoiceBeginNO">��ʼ��Ʊ��</param>
        /// <param name="realInvoiceBeginNO">��ʼ��Ʊ��ӡ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <param name="t">����</param>
        /// <returns>��Ʊ����ͷ�Ʊ��ϸ����[0]��Ʊ���� [1]��Ʊ��ϸ��</returns>
        public static ArrayList MakeInvoice(FS.HISFC.BizProcess.Integrate.Fee feeIntegrate, Register register, ArrayList feeItemLists,
            string invoiceBeginNO, string realInvoiceBeginNO, ref string errText, System.Data.IDbTransaction t)
        {
            const string OWN_INVOICE = "1";//�Էѷ�Ʊ
            const string PUB_INVOICE = "2";//���ʷ�Ʊ
            const string SP_INVOICE = "3";//���ⷢƱ
            const string YB_INVOICE = "4";//ҽ����Ʊ
            const string MAIN_INVOICE = "5";//���з�����Ϣ�γɵķ�Ʊ

            int returnValue = 0;//����ֵ
            DataSet dsInvoice = new DataSet();//��Ʊ����
            ArrayList balancesAndBalanceListsAndFeeListsAll = new ArrayList();//���з�Ʊ�ͷ�Ʊ��ϸ��Ϣ
            ArrayList balances = new ArrayList();  //��Ʊ������
            ArrayList balanceLists = new ArrayList();//��Ʊ��ϸ����
            //��Ʊ������ϸ
            ArrayList feeLists = new ArrayList();
            if (t != null)
            {
                feeIntegrate.SetTrans(t);
                controlManager.SetTrans(t);
            }
            //{DF484D55-5A9E-4afd-9B82-21EF6DA6E400}
            //������﷢Ʊ����
            //returnValue = feeIntegrate.GetInvoiceClass("MZ01", ref dsInvoice);
            //������﷢Ʊ����
            #region liuq 2007-8-28 �޸�Ϊ�ӽӿ�ȡ��Ʊ����
            string invoicePrintDll = null;
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

            FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint iInvoicePrint = null;

            // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
            // 2011-08-04
            bool blnNewPrintStyle = false;

            if (string.IsNullOrEmpty(invoicePrintDll))
            {
                blnNewPrintStyle = true;
                //errText = "û��ά����Ʊ��ӡ����!��ά��";
                //return null;
            }

            if (!blnNewPrintStyle)
            {
                #region ��Ʊ��ӡ�ɷ�ʽ
                invoicePrintDll = System.Windows.Forms.Application.StartupPath + invoicePrintDll;

                object obj = null;
                System.Reflection.Assembly a = System.Reflection.Assembly.LoadFrom(invoicePrintDll);
                try
                {
                    System.Type[] types = a.GetTypes();


                    foreach (System.Type type in types)
                    {
                        if (type.GetInterface("IInvoicePrint") != null)
                        {
                            try
                            {
                                obj = System.Activator.CreateInstance(type);
                                iInvoicePrint = obj as FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint;

                                break;
                            }
                            catch (Exception ex)
                            {
                                errText = ex.Message;

                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errText = ex.Message;
                    return null;
                }
                #endregion
            }
            else
            {
                #region �·�ʽ
                iInvoicePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Fee), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)) as IInvoicePrint;
                if (iInvoicePrint == null)
                {
                    errText = "��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�";
                    return null;
                }
                #endregion
            }
            iInvoicePrint.Register = register;

            #endregion
            returnValue = feeIntegrate.GetInvoiceClass(iInvoicePrint.InvoiceType, ref dsInvoice);
            if (dsInvoice.Tables[0].PrimaryKey.Length == 0)
            {
                dsInvoice.Tables[0].PrimaryKey = new DataColumn[] { dsInvoice.Tables[0].Columns["FEE_CODE"] };
            }
            foreach (FeeItemList f in feeItemLists)
            {
                DataRow rowFind = dsInvoice.Tables[0].Rows.Find(new object[] { f.Item.MinFee.ID });
                //�ҵ���Ӧ��Ӧ�ķ�Ʊ��Ŀ
                f.Item.MinFee.User01 = rowFind["FEE_STAT_CATE"].ToString();
                f.Invoice.Type.ID = rowFind["FEE_STAT_CATE"].ToString();

            }

            string splitType = feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.AUTO_INVOICE_TYPE, "0");
            #region liuq 2007-8-23
            if (register.Pact.PayKind.ID != "03")//�Էѻ���ҽ������
            {
                if (feeItemLists.Count > 0)
                {
                    string tempInvoiceNO = invoiceBeginNO;//���巢Ʊ�ķ�Ʊ�Ų���Ҫ�ۼ�,��Ҫ��Ϊ��

                    ArrayList feeItemListSplit = feeIntegrate.SplitInvoice(register, ref feeItemLists);
                    if (feeItemListSplit == null)
                    {
                        //{20DC3F9B-39E8-46dd-8941-C5AC75FD652B}
                        errText = feeIntegrate.Err;
                        return null;
                    }
                    //{1C257F28-1A60-437d-9E61-A6A3D99357EE}
                    int i = 0;
                    foreach (ArrayList list in feeItemListSplit)
                    {
                        Balance invoice = new Balance();//��Ʊʵ��
                        ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

                        if (t != null)
                        {
                            outpatientManager.SetTrans(t);
                        }
                        //��÷�Ʊ����
                        string invoiceCombNO = outpatientManager.GetInvoiceCombNO();

                        returnValue = MakeInvoiceAndDetail(
                            feeIntegrate, list, register, ref tempInvoiceNO, ref realInvoiceBeginNO,
                            dsInvoice.Tables[0], OWN_INVOICE, ref invoice,
                            ref tempBalanceLists, splitType, ref errText, i/*{1C257F28-1A60-437d-9E61-A6A3D99357EE}*/);
                        if (returnValue == -1)
                        {
                            return null;
                        }
                        if (returnValue != -2)
                        {
                            #region liu.xq20071008 ��Ʊ��seq
                            //�Է��ص� invoice, list, tempBalanceLists ��ֵҲ����.
                            invoice.CombNO = invoiceCombNO;
                            foreach (FeeItemList f in list)
                            {
                                f.InvoiceCombNO = invoiceCombNO;
                                f.Invoice.ID = invoice.Invoice.ID;
                            }
                            foreach (ArrayList tempBalanceList in tempBalanceLists)
                            {
                                foreach (BalanceList detail in tempBalanceList)
                                {
                                    ((Balance)detail.BalanceBase).CombNO = invoiceCombNO;
                                }
                            }
                            #endregion

                            //���㷢Ʊ������ʾ����.
                            //// GetSpDisplayFee(feeDetails, ref alTempInvoiceDetails, OWN_INVOICE);
                            //��Ʊ����Ӧ�ķ�����ϸ
                            feeLists.Add(list);
                            balances.Add(invoice);
                            balanceLists.Add(tempBalanceLists);
                        }
                        i++;//{1C257F28-1A60-437d-9E61-A6A3D99357EE}
                    }
                    //{47258E4D-E881-416b-B3EC-622E4C7337D8}�������ϴ�ҽ���󹫷��ֶ���ֵʱ��һ�·�������ʹ��Ʊ��ʾ��ȷ
                    if (register.Pact.PayKind.ID != "01" && register.SIMainInfo.PubCost > 0)
                    {
                        if (balances != null && balances.Count != 0)
                        {
                            decimal rate = 0;
                            decimal totPubCost = 0;
                            decimal totPayCost = 0;
                            Balance balance = new Balance();
                            int intCount = 0;
                            for (intCount = 0; intCount < balances.Count - 1; intCount++)
                            {
                                balance = balances[intCount] as Balance;
                                rate = balance.FT.TotCost / register.SIMainInfo.TotCost;
                                balance.FT.PubCost = register.SIMainInfo.PubCost * rate;
                                balance.FT.PayCost = register.SIMainInfo.PayCost * rate;
                                balance.FT.PayCost = FS.FrameWork.Public.String.FormatNumber(balance.FT.PayCost, 2);
                                balance.FT.PubCost = FS.FrameWork.Public.String.FormatNumber(balance.FT.PubCost, 2);
                                balance.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(balance.FT.TotCost, 2);
                                balance.FT.OwnCost = balance.FT.TotCost - balance.FT.PayCost - balance.FT.PayCost;
                                totPayCost += balance.FT.PayCost;
                                totPubCost += balance.FT.PubCost;
                            }
                            balance = balances[intCount] as Balance;
                            balance.FT.PubCost = register.SIMainInfo.PubCost - totPubCost;
                            balance.FT.PayCost = register.SIMainInfo.PayCost - totPayCost;
                            balance.FT.OwnCost = register.SIMainInfo.TotCost - balance.FT.PubCost - balance.FT.PayCost;
                        }
                    }
                }
            }
            #endregion

            #region liuq 2007-8-23 �ɴ���
            //if (register.Pact.PayKind.ID == "01")//�Էѻ���
            //{
            //    if (feeItemLists.Count > 0)
            //    {
            //        string tempInvoiceNO = invoiceBeginNO;//���巢Ʊ�ķ�Ʊ�Ų���Ҫ�ۼ�,��Ҫ��Ϊ��
            //        Balance invoice = new Balance();//��Ʊʵ��
            //        ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

            //        returnValue = MakeInvoiceAndDetail(feeIntegrate, feeItemLists, register, ref tempInvoiceNO, ref realInvoiceBeginNO, dsInvoice.Tables[0], OWN_INVOICE, ref invoice,
            //            ref tempBalanceLists, splitType, ref errText);
            //        if (returnValue == -1)
            //        {
            //            return null;
            //        }
            //        if (returnValue != -2)
            //        {
            //            //���㷢Ʊ������ʾ����.
            //            //// GetSpDisplayFee(feeDetails, ref alTempInvoiceDetails, OWN_INVOICE);
            //            balances.Add(invoice);
            //            balanceLists.Add(tempBalanceLists);
            //        }
            //    }
            //} 
            #endregion

            //#region ҽ������
            //if (register.Pact.PayKind.ID == "02")//ҽ������
            //{
            //    ArrayList ownFeeItemLists = new ArrayList();//�Է�
            //    ArrayList pubFeeItemLists = new ArrayList();//ҽ��

            //    //����ϸ����
            //    foreach (FeeItemList f in feeItemLists)
            //    {
            //        pubFeeItemLists.Add(f);
            //    }
            //    if (splitType == "0")//��ҽ
            //    {
            //        //�γ����з��õķ�Ʊ
            //        if (feeItemLists.Count > 0)
            //        {
            //            string tempInvoiceNO = invoiceBeginNO;//���巢Ʊ�ķ�Ʊ�Ų���Ҫ�ۼ�,��Ҫ��Ϊ��
            //            string tempRealInvoiceBeginNO = realInvoiceBeginNO;
            //            Balance invoice = new Balance();//��Ʊʵ��
            //            ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

            //            returnValue = MakeInvoiceAndDetail(feeIntegrate,feeItemLists, register, ref tempInvoiceNO, ref tempRealInvoiceBeginNO, dsInvoice.Tables[0], MAIN_INVOICE, ref invoice,
            //                ref tempBalanceLists, splitType, ref errText);
            //            if (returnValue == -1)
            //            {
            //                return null;
            //            }
            //            if (returnValue != -2)
            //            {
            //                ////GetSpDisplayFee(feeDetails, ref alTempInvoiceDetails, MAIN_INVOICE);
            //                balances.Add(invoice);
            //                balanceLists.Add(tempBalanceLists);
            //            }
            //        }
            //    }

            //    //�γ��Էѷ�Ʊ
            //    if (ownFeeItemLists.Count > 0)
            //    {
            //        Balance invoice = new Balance();//��Ʊʵ��
            //        ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

            //        returnValue = MakeInvoiceAndDetail(feeIntegrate, ownFeeItemLists, register, ref invoiceBeginNO, ref realInvoiceBeginNO, dsInvoice.Tables[0], OWN_INVOICE, ref invoice,
            //            ref tempBalanceLists, splitType, ref errText);
            //        if (returnValue == -1)
            //        {
            //            return null;
            //        }
            //        if (returnValue != -2)
            //        {
            //            ////GetSpDisplayFee(alOwnFee, ref alTempInvoiceDetails, OWN_INVOICE);
            //            balances.Add(invoice);
            //            balanceLists.Add(tempBalanceLists);
            //        }
            //    }
            //    //�γɼ��ʷ�Ʊ
            //    if (pubFeeItemLists.Count > 0)
            //    {
            //        Balance invoice = new Balance();//��Ʊʵ��
            //        ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

            //        returnValue = MakeInvoiceAndDetail(feeIntegrate, pubFeeItemLists, register, ref invoiceBeginNO, ref realInvoiceBeginNO, dsInvoice.Tables[0], YB_INVOICE, ref invoice,
            //            ref tempBalanceLists, splitType, ref errText);
            //        if (returnValue == -1)
            //        {
            //            return null;
            //        }
            //        if (returnValue != -2)
            //        {
            //            ////GetSpDisplayFee(alPubFee, ref alTempInvoiceDetails, PUB_INVOICE);
            //            balances.Add(invoice);
            //            balanceLists.Add(tempBalanceLists);
            //        }
            //    }

            //}
            //#endregion
            #region ���ѻ���
            if (register.Pact.PayKind.ID == "03")//���ѻ���,��ӡ3�ŷ�Ʊ,��Ϊ�Է�,����,������,����3�ŷ�Ʊ
            {
                ArrayList ownFeeItemLists = new ArrayList();//�Է�
                ArrayList pubFeeItemLists = new ArrayList();//����
                ArrayList spFeeItemLists = new ArrayList();//����
                if (splitType == "0")//��ҽ
                {
                    //����ϸ����
                    foreach (FeeItemList f in feeItemLists)
                    {
                        if (f.ItemRateFlag == "1" || f.FT.OwnCost > 0)
                        {
                            ownFeeItemLists.Add(f);
                        }
                        if (f.ItemRateFlag == "2")
                        {
                            pubFeeItemLists.Add(f);
                        }
                        if (f.ItemRateFlag == "3")
                        {
                            spFeeItemLists.Add(f);
                        }
                    }
                }
                if (splitType == "1")//��ɽ
                {
                    foreach (FeeItemList f in feeItemLists)
                    {
                        pubFeeItemLists.Add(f);
                    }
                }
                if (splitType == "0")
                {
                    //�γ����з��õķ�Ʊ
                    if (feeItemLists.Count > 0)
                    {
                        string tempInvoiceNO = invoiceBeginNO;//���巢Ʊ�ķ�Ʊ�Ų���Ҫ�ۼ�,��Ҫ��Ϊ��
                        string tempRealInvoiceBeginNO = realInvoiceBeginNO;
                        Balance invoice = new Balance();//��Ʊʵ��
                        ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

                        returnValue = MakeInvoiceAndDetail(feeIntegrate, feeItemLists, register, ref tempInvoiceNO, ref tempRealInvoiceBeginNO, dsInvoice.Tables[0], MAIN_INVOICE, ref invoice,
                            ref tempBalanceLists, splitType, ref errText,0);
                        if (returnValue == -1)
                        {
                            return null;
                        }
                        if (returnValue != -2)
                        {
                            #region ��ϸ���¸�ֵ��Ʊ
                            foreach (FeeItemList f in feeItemLists)
                            {
                                f.Invoice = invoice.Invoice;
                            }
                            #endregion

                            ////GetSpDisplayFee(feeDetails, ref alTempInvoiceDetails, MAIN_INVOICE);
                            balances.Add(invoice);
                            balanceLists.Add(tempBalanceLists);
                            feeLists.Add(feeItemLists);
                        }
                    }
                }

                //�γ��Էѷ�Ʊ
                if (ownFeeItemLists.Count > 0)
                {
                    Balance invoice = new Balance();//��Ʊʵ��
                    ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

                    returnValue = MakeInvoiceAndDetail(feeIntegrate, ownFeeItemLists, register, ref invoiceBeginNO, ref realInvoiceBeginNO, dsInvoice.Tables[0], OWN_INVOICE, ref invoice,
                        ref tempBalanceLists, splitType, ref errText,0);
                    if (returnValue == -1)
                    {
                        return null;
                    }
                    if (returnValue != -2)
                    {
                        ////GetSpDisplayFee(alOwnFee, ref alTempInvoiceDetails, OWN_INVOICE);
                        balances.Add(invoice);
                        balanceLists.Add(tempBalanceLists);
                        feeLists.Add(ownFeeItemLists);
                    }
                }
                //�γɼ��ʷ�Ʊ
                if (pubFeeItemLists.Count > 0)
                {
                    Balance invoice = new Balance();//��Ʊʵ��
                    ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

                    returnValue = MakeInvoiceAndDetail(feeIntegrate, pubFeeItemLists, register, ref invoiceBeginNO, ref realInvoiceBeginNO, dsInvoice.Tables[0], PUB_INVOICE, ref invoice,
                        ref tempBalanceLists, splitType, ref errText,0);
                    if (returnValue == -1)
                    {
                        return null;
                    }
                    if (returnValue != -2)
                    {
                        ////GetSpDisplayFee(alPubFee, ref alTempInvoiceDetails, PUB_INVOICE);
                        balances.Add(invoice);
                        balanceLists.Add(tempBalanceLists);
                        feeLists.Add(pubFeeItemLists);
                    }
                }
                //�γ����ⷢƱ
                if (spFeeItemLists.Count > 0)
                {
                    Balance invoice = new Balance();//��Ʊʵ��
                    ArrayList tempBalanceLists = new ArrayList();//��Ʊ��ϸʵ�弯��

                    returnValue = MakeInvoiceAndDetail(feeIntegrate, spFeeItemLists, register, ref invoiceBeginNO, ref realInvoiceBeginNO, dsInvoice.Tables[0], SP_INVOICE, ref invoice,
                        ref tempBalanceLists, splitType, ref errText,0);
                    if (returnValue == -1)
                    {
                        return null;
                    }
                    if (returnValue != -2)
                    {
                        ////GetSpDisplayFee(alSpFee, ref alTempInvoiceDetails, SP_INVOICE);
                        balances.Add(invoice);
                        balanceLists.Add(tempBalanceLists);
                        feeLists.Add(spFeeItemLists);
                    }
                }
            }
            #endregion

            balancesAndBalanceListsAndFeeListsAll.Add(balances);
            balancesAndBalanceListsAndFeeListsAll.Add(balanceLists);
            balancesAndBalanceListsAndFeeListsAll.Add(feeLists);

            return balancesAndBalanceListsAndFeeListsAll;
        }

        /// <summary>
        /// ������ϸ���ɷ�Ʊ�ͷ�Ʊ��ϸ��Ϣ
        /// </summary>
        /// <param name="feeIntegrate">����ҵ���</param>
        /// <param name="register">���߹ҺŻ�����Ϣ</param>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <param name="invoiceBeginNO">��ʼ��Ʊ��</param>
        /// <param name="realInvoiceBeginNO">��ʼ��Ʊ��ӡ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>��Ʊ����ͷ�Ʊ��ϸ����[0]��Ʊ���� [1]��Ʊ��ϸ��</returns>
        public static ArrayList MakeInvoice(FS.HISFC.BizProcess.Integrate.Fee feeIntegrate, Register register, ArrayList feeItemLists,
            string invoiceBeginNO, string realInvoiceBeginNO, ref string errText)
        {
            //ʹ�÷ַ�Ʊ�ӿڣ����δ���ã���ֱ��ʹ��Ĭ�Ϻ���
            ISplitInvoice ISplitInvoiceImplement = InterfaceManager.GetISplitInvoice();
            if (ISplitInvoiceImplement == null)
            {
                return MakeInvoice(feeIntegrate, register, feeItemLists, invoiceBeginNO, realInvoiceBeginNO, ref errText, null);
            }
            else
            {
                ISplitInvoiceImplement.SetTrans(null);
                return ISplitInvoiceImplement.SplitInvoice(register, ref feeItemLists);
            }
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="pactId">��ͬ��λ����</param>
        /// <param name="f">������ϸ</param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.PactItemRate PactRate(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, ref string errMsg)
        {
            FS.HISFC.Models.Base.PactItemRate pRate = new FS.HISFC.Models.Base.PactItemRate();
            //FS.HISFC.BizProcess.Fee.PactUnitItemRate pactItemRate = new FS.HISFC.BizProcess.Fee.PactUnitItemRate();
            //FS.HISFC.BizProcess.Fee.PactUnitInfo pactInfoManager = new FS.HISFC.BizProcess.Fee.PactUnitInfo();
            //if (r.Pact.PayKind.ID == "01")
            //{

            //    pRate = pactItemRate.GetOnepPactUnitItemRateByItem(r.Pact.ID, f.Item.ID);
            //    if (pRate == null)
            //    {
            //        pRate = pactItemRate.GetOnePaceUnitItemRateByFeeCode(r.Pact.ID, f.Item.MinFee.ID);
            //        if (pRate == null)
            //        {
            //            FS.HISFC.Models.Base.PactInfo p = pactInfoManager.GetPactUnitInfoByPactCode(r.Pact.ID);
            //            if (p == null)
            //            {
            //                errMsg = pactInfoManager.Err;

            //                return null;
            //            }

            //            pRate = new FS.HISFC.Models.Base.PactItemRate();

            //            pRate.Rate = p.Rate;
            //        }
            //    }
            //}
            //else
            //{
            pRate.Rate.RebateRate = 0;
            //}
            return pRate;
        }

        #endregion


        
    }
    #region ��־��¼
    /// <summary>
    /// ��־��¼
    /// </summary>
    public class LogManager
    {
        public static void Write(string logInfo)
        {
            //return;
            //���Ŀ¼�Ƿ����
            if (System.IO.Directory.Exists("./Log/OutPatienFee/CardTicket") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/OutPatienFee/CardTicket");
            }
            //����һ�ܵ���־
            System.IO.File.Delete("./Log/OutPatienFee/CardTicket/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/OutPatienFee/CardTicket/" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
    #endregion
}
