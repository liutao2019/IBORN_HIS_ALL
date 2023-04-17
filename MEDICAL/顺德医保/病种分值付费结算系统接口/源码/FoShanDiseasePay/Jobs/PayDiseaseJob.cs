using System;
using System.Data;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FoShanDiseasePay.Jobs
{

    /*
        *ע�⣺
        *1���Һŵ����ѣ�
        *2�������Ŀ�ϴ�(�ֲ�) - ������û��
        *
        *3�����Ӳ��������ϴ� - ��ԺС�᡾�°���Ӳ����;ɰ���Ӳ�����
        *
        *4�������Ϣ�ϴ�/������Ϣ�ϴ�
        *5��HIS��ƽ̨�������� ���ο� ��ϸ�ϴ��Ķ��չ�ϵ��
        * 
        * */

    /// <summary>
    /// OutpatientJob ��ժҪ˵����
    /// ����ҵ����Ϣ
    /// </summary>
    public class PayDiseaseJob : BaseJob
    {
        public PayDiseaseJob()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //

            ArrayList alDept = this.dataBaseHelp.getConstDict("SporadicDept");
            fsDeptHelper.ArrayObject = alDept;
        }

        string SessionNO = string.Empty;

        //private DataBaseHelp dbHelp = new DataBaseHelp();

        Neusoft.FrameWork.Public.ObjectHelper LisHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper fsDeptHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        //Neusoft.FrameWork.Public.ObjectHelper diagHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        public override void Start()
        {
            //��ʼ��log�ļ���
            this.logFileName = @".\log\Detail" + this.endTime + ".log";
            try
            {
                DateTime dtBegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.startTime).AddDays(-180);
                DateTime dtEnd = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.endTime).AddDays(-1);
                StringBuilder sb = new StringBuilder();

                //ArrayList alDiagNoses  = dbHelp.QueryDiagNoseInfoToday(dtBegin.ToString("yyyy-MM-dd"), dtEnd.ToString("yyyy-MM-dd"));
                //diagHelper.ArrayObject = alDiagNoses;

                #region �����ϴ�
                WriteLog("******************************������Ϣ��ʼ�ϴ�**************************\n");

                if (false)
                {
                    ArrayList alOutpatient = dbHelp.QueryOutpatientInvoiceInfo4Pay(dtBegin.ToString("yyyy-MM-dd"), dtEnd.ToString("yyyy-MM-dd"));

                    WriteLog("��ѯ�����ﻼ�߷�Ʊ��Ϣ" + alOutpatient.Count + "����¼\n");

                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.Balance balance in alOutpatient)
                    {
                        if (string.IsNullOrEmpty(balance.Patient.IDCard))
                        {
                            WriteLog(balance.Patient.ID + "û���ϴ�@���֤����Ϊ�գ�");
                            continue;
                        }

                        if (string.IsNullOrEmpty(balance.Memo))
                        {
                            WriteLog(balance.Patient.ID + "û���ϴ�@�籣����Ϊ�գ�");
                            continue;
                        }

                        int result = 0;

                        if (balance.Invoice.ValidState == "0")
                        {
                            result = this.UploadCancelSIInfo(balance.ID, balance.Memo);
                        }
                        else
                        {
                            result = this.QueryUploadedInfo(balance.ID, balance.Memo);

                            if (result < 0) //��ʾû�в�ѯ�� �����ϴ�����
                            {
                                result = this.UploadSIInfo(balance.Patient.ID, balance.Patient.PID.CardNO, balance.ID, balance.Memo, true);

                                if (result <= 0)
                                {
                                    if (result == 0)
                                    {
                                        WriteLog(balance.Patient.ID + "���Ϊ�Զ�����ϲ��ϴ���");
                                        continue;
                                    }
                                    WriteLog(balance.Patient.ID + " " + balance.ID + "�ϴ�����@" + errMsg);
                                    continue;
                                }
                            }
                        }
                    }

                }

                WriteLog("******************************������Ϣ�ϴ����**************************\n");

                #endregion

                #region סԺ�ϴ�

                WriteLog("******************************סԺ��Ϣ��ʼ�ϴ�**************************\n");

                ArrayList alInpatient = dbHelp.QueryInpatientInvoiceInfo4PayNew(dtBegin.ToString("yyyy-MM-dd"), dtEnd.ToString("yyyy-MM-dd"));

                #region ����
                //ArrayList alInpatient = new ArrayList();
                //Neusoft.HISFC.Models.Fee.Inpatient.Balance balancec = new Neusoft.HISFC.Models.Fee.Inpatient.Balance();
                //balancec.ID = "101879100934";
                //balancec.Patient.ID = "ZY020000150194";
                //balancec.Patient.Name = "2";
                //balancec.FT.TotCost = 3457.07m;
                //balancec.FT.PubCost = 2544.96m;
                //balancec.FT.PayCost = 912.11m;
                //balancec.Patient.IDCard = "429001198211074990";
                //balancec.Memo = "70829473";
                //alInpatient.Add(balancec);
                #endregion

                if (alInpatient != null && alInpatient.Count > 0)
                {

                    WriteLog("��ѯ��סԺ���߷�Ʊ��Ϣ" + alInpatient.Count + "����¼\n");

                    foreach (Neusoft.HISFC.Models.Fee.Inpatient.Balance invoice in alInpatient)
                    {
                        if (string.IsNullOrEmpty(invoice.Patient.IDCard))
                        {
                            WriteLog(invoice.Patient.ID + "û���ϴ�@���֤����Ϊ�գ�");
                            continue;
                        }
                        int result = 0;

                        //��ѯסԺ���ߵ�����
                        Neusoft.HISFC.Models.RADT.PatientInfo inPatientInfo = dbHelp.QueryPatientInfoByInpatientNO(invoice.Patient.ID);
                        if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
                        {
                            WriteLog("δ�ҵ���" + invoice.Patient.ID + "����סԺ��Ϣ!");
                            continue;
                        }

                        //��ѯסԺ���ߵķ�Ʊ��Ϣ
                        ArrayList balanceList = dbHelp.QueryBalances(invoice.Invoice.ID);
                        if (balanceList == null || balanceList.Count <= 0)
                        {
                            WriteLog("��Ʊ�š�" + invoice.Invoice.ID + "����δ�ҵ���Ӧ�Ľ�����Ϣ!");
                            continue;
                        }
                        if (balanceList.Count > 1)
                        {
                            WriteLog("��Ʊ�š�" + invoice.Invoice.ID + "�����Ѿ��˷�!");
                            continue;
                        }

                        Neusoft.HISFC.Models.Fee.Inpatient.Balance balance = balanceList[0] as Neusoft.HISFC.Models.Fee.Inpatient.Balance;
                        if (balance == null || string.IsNullOrEmpty(balance.Invoice.ID))
                        {
                            WriteLog("��Ʊ�š�" + balance.Invoice.ID + "����δ�ҵ���Ӧ�Ľ�����Ϣ!");
                            continue;
                        }

                        //��Ʊ��Ӧ���籣������Ϣ
                        string siMemo = dbHelp.GetSiInfo(balance.Patient.ID, balance.Invoice.ID, "2");
                        string siReturnID = string.Empty;
                        if (!string.IsNullOrEmpty(siMemo))
                        {
                            string[] arr = siMemo.Split('|');
                            if (arr.Length > 0)
                            {
                                siReturnID = arr[0];
                            }
                        }
                        string wylsh = Manager.setObj.HospitalID + "-2-" + balance.Invoice.ID + "-" + siReturnID;  //����

                        //��ѯ�����Ϣ
                        ArrayList alDiag = dbHelp.QueryCaseDiagnoseForClinicByState(inPatientInfo.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                        string firstDiag = string.Empty;
                        string secondDiag = string.Empty;

                        #region ��ȡ���

                        if (alDiag != null && alDiag.Count > 0)
                        {
                            int i = 0;
                            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                            {
                                if (diag.IsValid && diag.PerssonType == Neusoft.HISFC.Models.Base.ServiceTypes.I)  //������� 0 ���ﻼ�� 1 סԺ����
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


                        //5.1 ���ﵥ����Ϣ�ϴ�(HJ001)
                        if (balance.CancelType != Neusoft.HISFC.Models.Base.CancelTypes.Valid)
                        {
                            result = this.UploadCancelSIInfo(wylsh, siReturnID);
                        }
                        else
                        {
                            result = this.QueryUploadedInfo(wylsh, siReturnID);
                            if (result < 0) //��ʾû�в�ѯ�� �����ϴ�����
                            {
                                result = this.UploadSIInfo(balance.Patient.ID, balance.Patient.PID.PatientNO, balance.Invoice.ID, siReturnID, false);
                                if (result <= 0)
                                {
                                    if (result == 0)
                                    {
                                        WriteLog(balance.Patient.ID + "���Ϊ�Զ�����ϲ��ϴ���");
                                        continue;
                                    }
                                    WriteLog(balance.Patient.ID + " " + balance.ID + "�ϴ�����@" + errMsg);
                                    continue;
                                }
                            }
                        }

                        //5.7 ������Ϣ�ϴ�(HB5001)
                        result = this.QueryCaseInfo(balance.Patient.PID.CardNO);
                        if (result < 0) //��ʾû�в�ѯ�� �����ϴ�����
                        {
                            result = this.UploadCaseInfo(balance.Patient.ID, balance.Patient.IDCard, balance.Memo, "", DateTime.Now, DateTime.Now);
                            if (result <= 0)
                            {
                                WriteLog(balance.Patient.ID + "�ϴ�������Ϣ����@" + errMsg);
                                continue;
                            }
                        }
                    }
                }

                WriteLog("******************************סԺ��Ϣ�ϴ����**************************\n");

                #endregion

            }
            catch (Exception ex)
            {
                this.WriteLog(ex.Message);
            }
            finally
            {

            }
        }

        #region ��ɽ��ֵ����ϵͳ

        //5.1	���ﵥ����Ϣ�ϴ�(HJ001)
        private int UploadSIInfo(string patientID, string patientNO, string invoiceNO, string balanceBusNo, bool isOutpatient)
        {
            string json = @"'wylsh':'{0}','sfdjh':'{1}','djjsrq':'{2}','ddjgbm':'{3}','ddjgmc':'{4}','fyfsjgbm':'{5}',
                            'fyfsjgmc':'{6}','zwjgmc':'{7}','gmsfhm':'{8}','cbrmc':'{9}','cbrxb':'{10}','cbrcsrq':'{11}',
                            'rylbbm':'{12}','jzfsbm':'{13}','sfydjy':'{14}','ryzdbm':'{15}','cyzdbm':'{16}','fzd1':'{17}',
                            'fzd2':'{18}','fzd3':'{19}','fzd4':'{20}','fzd5':'{21}','fzd6':'{22}','fzd7':'{23}','fzd8':'{24}',
                            'fzd9':'{25}','fzd10':'{26}','fzd11':'{27}','fzd12':'{28}','fzd13':'{29}','fzd14':'{30}','fzd15':'{31}',
                            'fzd16':'{32}','sftbmbdjbz':'{33}','mbtbdm':'{34}','ryrq':'{35}','cyrq':'{36}','jzrq':'{37}','sfhy':'{38}',
                            'sfbrq':'{39}','sg':'{40}','tz':'{41}','xy':'{42}','kfxt':'{43}','chxt':'{44}','tw':'{45}','xl':'{46}',
                            'sfzry':'{47}','ybzxdm':'{48}','ydrylb':'{49}','ydcbxzqydm':'{50}','dylx':'{51}','cblx':'{52}','zje':'{53}',
                            'ybnzje':'{54}','ybnzfje':'{55}','zfbl':'{56}','zhfbl':'{57}','xyf':'{58}','zchyf':'{59}','zcyf':'{60}',
                            'ybylfwf':'{61}','ybzlczf':'{62}','hlf':'{63}','blzdf':'{64}','syszdf':'{65}','yxxzdf':'{66}','lczdxmf':'{67}',
                            'sszlf':'{68}','mjf':'{69}','fsszlxmf':'{70}','lcwlzlf':'{71}','kff':'{72}','xf':'{73}','bdblzpf':'{74}',
                            'qdblzpf':'{75}','nxyzlzpf':'{76}','xbyzlzpf':'{77}','jcyclf':'{78}','zlyclf':'{79}','ssyclf':'{80}',
                            'mzzyh':'{81}','cjrbs':'{82}','yylb':'{83}','cjdjh':'{84}','jjlx':'{85}','ssdm':'{86}','ssqkfl':'{87}',
                            'cyzry':'{88}',detail: [{89}]";

            string innerJson = @"'sfdjh':'{0}','wylsh':'{1}','fwrq':'{2}','xmbm':'{3}','xmmc':'{4}','xmlb':'{5}',
                            'dj':'{6}','sl':'{7}','zj':'{8}','ysbm':'{9}','ysmc':'{10}','ksbm':'{11}',
                            'ksmc':'{12}','yf':'{13}','gytj':'{14}','yl':'{15}','pc':'{16}','yyts':'{17}',
                            'ybnje':'{18}','jylx':'{19}','ypgg':'{20}','cydybs':'{21}','zyh':'{22}',
                            'yzzdid':'{23}','yzmxid':'{24}'";

            if (isOutpatient)
            {
                #region ����

                #region ��ϸ��Ŀ����

                ArrayList comFeeItemLists = dbHelp.QueryFeeItemListsByInvoiceNO4Pay(invoiceNO);

                ArrayList tempFeeDetails = new ArrayList();
                tempFeeDetails = new ArrayList();
                Hashtable hsUpLoadFeeDetails = new Hashtable();
                Neusoft.HISFC.Models.Fee.FeeItemBase feeItemList = null;

                foreach (Neusoft.HISFC.Models.Fee.FeeItemBase f in comFeeItemLists)
                {
                    this.DealFeeItemList(f);

                    if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0") //2010-9-1
                    {
                        this.WriteLog("��Ŀ" + f.Item.Name + "û�ж�����");
                        tempFeeDetails = new ArrayList();
                        return -1;
                    }

                    if (hsUpLoadFeeDetails.ContainsKey(f.Item.UserCode))
                    {
                        feeItemList = hsUpLoadFeeDetails[f.Item.UserCode] as Neusoft.HISFC.Models.Fee.FeeItemBase;

                        feeItemList.Item.Qty += f.Item.Qty;//�����ۼ�
                        feeItemList.FT.TotCost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;//����ۼ�
                    }
                    else
                    {
                        f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        hsUpLoadFeeDetails.Add(f.Item.UserCode, f);
                        tempFeeDetails.Add(f);
                    }
                }

                hsUpLoadFeeDetails = null;
                comFeeItemLists = null;

                int days = 0;

                string type = string.Empty;

                StringBuilder sb = new StringBuilder();

                Neusoft.FrameWork.Models.NeuObject deptObj = null;

                int seqNo = 1;
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f in tempFeeDetails)
                {
                    if (f.Item.Qty == 0)//����Ϊ0�Ĳ��ϴ�
                        continue;

                    if (f.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive)
                    {
                        type = "1";
                    }
                    else
                    {
                        type = "2";
                    }

                    if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        f.Item.PriceUnit = "0";
                    }

                    deptObj = fsDeptHelper.GetObjectFromID(f.ConfirmOper.Dept.ID);

                    sb.Append(string.Format(innerJson,
                                  balanceBusNo,
                                  seqNo++,
                        //htDetail[f.Item.UserCode].ToString(),
                                  f.FeeOper.OperTime.ToString("yyyyMMdd"),
                                  f.Item.UserCode,
                                  f.Item.Name,
                                  f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug ? "1" : "0",
                                  f.Item.Price.ToString(),
                                  f.Item.Qty.ToString(),
                                  f.FT.TotCost.ToString(),
                                  f.ConfirmOper.ID,
                                  f.RecipeOper.Name,
                        //f.ConfirmOper.Dept.ID,
                                  deptObj.Memo,//���� 
                                  deptObj.Name, //����
                        //f.RecipeOper.Dept.Name,
                                  string.IsNullOrEmpty(f.Item.PriceUnit) ? "0" : f.Item.PriceUnit,
                                  string.IsNullOrEmpty(f.Order.Usage.Name) ? "900" : f.Order.Usage.Name,
                                  f.Item.Qty.ToString(),
                                  string.IsNullOrEmpty(f.Order.Frequency.ID) ? "62" : f.Order.Frequency.ID,
                                  days.ToString(),
                                  "0",
                                  type,
                                  string.IsNullOrEmpty(f.Item.Specs) ? "0" : f.Item.Specs,
                                  "0",
                                  f.Patient.PID.CardNO,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID));
                    sb.Append(",");
                }
                #endregion

                Neusoft.HISFC.Models.Registration.Register register = dbHelp.QuerySIOutpatient4Pay(patientID, invoiceNO);

                json = string.Format(json, Manager.setObj.HospitalID + "-" + register.InvoiceNO + "-" + balanceBusNo, balanceBusNo, register.InputOper.OperTime.ToString("yyyyMMdd"),
                    Manager.setObj.HospitalID, Manager.setObj.HospitalName, Manager.setObj.HospitalID, Manager.setObj.HospitalName,
                    "", register.SSN, register.Name, register.Sex.ID.ToString(), register.Birthday.ToString("yyyyMMdd"), register.Pact.ID, register.Pact.Name,
                    "", register.ClinicDiagnose, register.ClinicDiagnose,//������Ϊ12ʱ�����Բ��ô���Ҫ���
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", register.DoctorInfo.SeeDate.ToString("yyyyMMdd"), register.DoctorInfo.SeeDate.ToString("yyyyMMdd"),
                    register.DoctorInfo.SeeDate.ToString("yyyyMMdd"), "", "", "", "", "", "", "", "", "",
                    "", "440606", "", "", "", register.Pact.PayKind.ID, register.PayCost.ToString(), register.PubCost.ToString(), "0", register.OwnCost.ToString(), register.SIMainInfo.TotCost.ToString(),
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", register.PID.CardNO, "", register.DIST, register.InputOper.Memo, register.InputOper.Oper.Memo,
                    "", "0", "", sb.ToString().TrimEnd(','));

                #endregion
            }
            else
            {
                #region סԺ

                string wylsh = Manager.setObj.HospitalID + "-2-" + invoiceNO + "-" + balanceBusNo;  //����

                #region ��ϸ����

                //��ȡ��ҩƷ��ϸ
                ArrayList feeDetails = dbHelp.QueryFeeItemListsByInpatientNOAndInvoiceNO("FoShanSI.SIUploadJob.PayDisease.QueryFeeItemListsByInpatientNOAndInvoiceNO", patientID, invoiceNO);
                //��ȡҩƷ��ϸ
                ArrayList feeDetailsDrug = dbHelp.QueryMedItemListsByInpatientNOAndInvoiceNO("FoShanSI.SIUploadJob.PayDisease.QueryMedItemListsByInvoiceNO", patientID, invoiceNO);
                if (feeDetailsDrug != null)
                {
                    feeDetails.AddRange(feeDetailsDrug);
                }

                Hashtable hsUpLoadFeeDetails = new Hashtable();

                ArrayList tempFeeDetails = new ArrayList();

                ArrayList alFeeDetails = this.SplitFeeItemList(feeDetails);
                string keyCode = string.Empty;

                foreach (Neusoft.HISFC.Models.Fee.FeeItemBase f in alFeeDetails)
                {
                    this.DealFeeItemList(f);

                    if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0") //2010-9-1
                    {
                        this.WriteLog("��Ŀ" + f.Item.Name + "û�ж�����");
                        tempFeeDetails = new ArrayList();
                        break;
                    }

                    keyCode = f.Item.UserCode + f.FeeOper.OperTime.ToString("yyyyMMdd");
                    if (hsUpLoadFeeDetails.ContainsKey(keyCode))
                    {
                        Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = hsUpLoadFeeDetails[keyCode] as
                            Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                        feeItemList.Item.Qty += f.Item.Qty;//�����ۼ�
                        feeItemList.FT.TotCost += f.FT.TotCost;//����ۼ�
                    }
                    else
                    {
                        hsUpLoadFeeDetails.Add(keyCode, f);
                        tempFeeDetails.Add(f);
                    }
                }

                int days = 0;

                string type = string.Empty;

                StringBuilder sb = new StringBuilder();

                Neusoft.FrameWork.Models.NeuObject deptObj = null;

                int seqNo = 1;

                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in tempFeeDetails)
                {
                    if (f.Item.Qty == 0)//����Ϊ0�Ĳ��ϴ�
                        continue;

                    if (f.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive)
                    {
                        type = "1";
                    }
                    else
                    {
                        type = "2";
                    }

                    if (string.IsNullOrEmpty(f.RecipeOper.Dept.ID))
                    {
                        f.RecipeOper.Dept.ID = ((Neusoft.HISFC.Models.RADT.PatientInfo)f.Patient).PVisit.PatientLocation.Dept.ID;
                    }

                    deptObj = fsDeptHelper.GetObjectFromID(f.RecipeOper.Dept.ID);

                    if (deptObj == null)
                    {
                        deptObj = fsDeptHelper.GetObjectFromID(((Neusoft.HISFC.Models.RADT.PatientInfo)f.Patient).PVisit.PatientLocation.Dept.ID);
                    }

                    if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        f.Item.PriceUnit = "0";
                    }

                    sb.Append(string.Format(innerJson,
                                  balanceBusNo,
                        //htDetail[f.Item.UserCode].ToString(),
                                  seqNo++,
                                  f.FeeOper.OperTime.ToString("yyyyMMdd"),
                                  f.Item.UserCode,
                                  f.Item.Name,
                                  f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug ? "1" : "0",
                                  f.Item.Price.ToString(),
                                  f.Item.Qty.ToString(),
                                  f.FT.TotCost.ToString(),
                                  f.ConfirmOper.ID,
                                  f.RecipeOper.Name,
                        //f.RecipeOper.Dept.ID,
                        //f.MachineNO,
                                  deptObj.Memo,
                                  deptObj.Name,
                                  string.IsNullOrEmpty(f.Item.PriceUnit) ? "0" : f.Item.PriceUnit,
                                  string.IsNullOrEmpty(f.Order.Usage.ID) ? "900" : f.Order.Usage.ID,
                                  f.Item.Qty.ToString(),
                                  string.IsNullOrEmpty(f.Order.Frequency.ID) ? "62" : f.Order.Frequency.ID,
                                  days.ToString(),
                                  "0",
                                  "0",
                                  string.IsNullOrEmpty(f.Item.Specs) ? "0" : f.Item.Specs,
                                  f.ChargeOper.ID == "CD" ? "1" : "0",
                                  patientNO,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID));

                    sb.Append(",");
                }
                #endregion

                Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = dbHelp.QuerySIInpatient4Pay(patientID, invoiceNO);
                Neusoft.HISFC.Models.HealthRecord.Fee inpatientFee = dbHelp.QuerySIInpatientFee4Pay(patientID, invoiceNO);

                json = string.Format(json, wylsh, balanceBusNo, patientInfo.SIMainInfo.OperDate.ToString("yyyyMMdd"),
                    Manager.setObj.HospitalID, Manager.setObj.HospitalName, Manager.setObj.HospitalID, Manager.setObj.HospitalName, "",
                    patientInfo.SSN, patientInfo.Name, patientInfo.Sex.ID, patientInfo.Birthday.ToString("yyyyMMdd"), patientInfo.Pact.ID, patientInfo.PayKind.ID,
                    "", string.IsNullOrEmpty(patientInfo.ClinicDiagnose) ? "��" : patientInfo.ClinicDiagnose, string.IsNullOrEmpty(patientInfo.Disease.Name) ? "��" : patientInfo.Disease.Name, patientInfo.ExtendFlag, patientInfo.ExtendFlag1, patientInfo.ExtendFlag2,
                    patientInfo.User01, patientInfo.User02, patientInfo.User03, patientInfo.Pact.PayKind.User01, patientInfo.Pact.PayKind.User02, patientInfo.Pact.PayKind.User03,
                    patientInfo.PID.User01, patientInfo.PID.User02, patientInfo.PID.User03, patientInfo.Profession.User01, patientInfo.Profession.User02, patientInfo.Profession.User03,
                    patientInfo.SpellCode, "", "", patientInfo.PVisit.InTime.ToString("yyyyMMdd"), patientInfo.PVisit.OutTime.ToString("yyyyMMdd"),
                    patientInfo.PVisit.InTime.ToString("yyyyMMdd"), "", "", "", "", "", "", "", "", "",
                    "", "440606", "", "", "", patientInfo.PayKind.Name, patientInfo.SIMainInfo.TotCost.ToString(), patientInfo.SIMainInfo.PubCost.ToString(),
                    "0", patientInfo.SIMainInfo.OwnCost.ToString(), patientInfo.SIMainInfo.PayCost.ToString(), inpatientFee.User01, inpatientFee.User02, inpatientFee.User03, inpatientFee.DeptInfo.User01,
                    inpatientFee.DeptInfo.User02, inpatientFee.DeptInfo.User03, inpatientFee.FeeInfo.User01, inpatientFee.FeeInfo.User02, inpatientFee.FeeInfo.User03, inpatientFee.MainOutICD.User01,
                    inpatientFee.MainOutICD.User02, inpatientFee.MainOutICD.User03, inpatientFee.OperInfo.User01, inpatientFee.OperInfo.User02, inpatientFee.OperInfo.User03, inpatientFee.ID, inpatientFee.Name,
                    inpatientFee.Memo, inpatientFee.DeptInfo.ID, inpatientFee.DeptInfo.Name, inpatientFee.DeptInfo.Memo, inpatientFee.FeeInfo.ID, inpatientFee.FeeInfo.Name, patientInfo.PID.PatientNO, "",
                    patientInfo.ProCreateNO, patientInfo.AddressHome, patientInfo.AddressBusiness, "", "0", "", sb.ToString().TrimEnd(','));

                #endregion
            }

            string error = string.Empty;
            int result = base.UploadInfo("HJ001", json, ref error);
            return result;

        }

        //5.2	������Ϣ����(HJ002)
        private int UploadCancelSIInfo(string serialNO, string billNO)
        {
            string json = @"'wylsh': '{0}','sfdjh': '{1}'";

            string seqNO = Manager.setObj.HospitalID + "-" + serialNO + "-" + billNO;

            json = string.Format(json, seqNO, billNO);

            string error = string.Empty;

            int result = base.UploadInfo("HJ002", json, ref error);

            return result;
        }

        //5.3	������Ϣ��ѯ(HJ003)
        private int QueryUploadedInfo(string serialNO, string billNO)
        {
            string json = @"'wylsh': '{0}','sfdjh': '{1}'";

            string seqNO = Manager.setObj.HospitalID + "-" + serialNO + "-" + billNO;

            json = string.Format(json, seqNO, billNO);

            string error = string.Empty;

            int result = base.UploadInfo("HJ003", json, ref error);

            return result;
        }

        //5.7	������Ϣ�ϴ�(HB5001)
        private int UploadCaseInfo(string inpatientNO, string idenno, string balanceBusNO,string inTimes,DateTime dtBegin,DateTime dtEnd)
        {
            string json = @"'Medical': {
                                    'HospitalId':'{0}','AdmissionNo':'{1}','SciCardNo':'{2}','SciCardIdentified':'{3}',
		                            'OutBedNum':'{4}','AdmissionDate':'{5}','DischargeDate':'{6}','DoctorCode':'{7}',
		                            'DoctorName':'{8}','IsDrugAllergy':'{9}','AllergyDrugCode':'{10}','AllergyDrugName':'{11}',
		                            'IsPathologicalExamination':'{12}','PathologyCode':'{13}','IsHospitalInfected':'{14}',
		                            'HospitalInfectedCode':'{15}','BloodTypeS':'{16}','BloodTypeE':'{17}','LeaveHospitalType':'{18}',
		                            'ChiefComplaint':'{19}','MedicalHistory':'{20}','SurgeryHistory':'{21}','BloodTransHistory':'{22}',
		                            'Marriage':'{23}','Height':'{24}','Weight':'{25}','NewbornDate':'{26}','NewbornWeight':'{27}',
		                            'NewbornCurrentWeight':'{28}','BearPregnancy':'{29}','BearYie':'{30}','AdmissionDiseaseId':'{31}',
		                            'AdmissionDiseaseName':'{32}','DiagnosePosition1':'{33}','DischargeDiseaseId':'{34}',
		                            'DischargeDiseaseName':'{35}','DiagnosePosition2':'{36}','Tsblbs':'{37}',{38}
                                },
                                'ListCheck': [{39}],
                                'ListOperation': [{40}],
                                'LeaveHospital': {41}
                            ";

            string checkJson = @"'CheckId': '{0}','DepartmentCode': '{1}','DepartmentName': '{2}','ApplyProjectCode': '{3}',
                            'ApplyProjectName': '{4}','ApplyDoctor': '{5}','ApplyDoctorName': '{6}','ApplyDatetime': '{7}',
                            'ReportDatetime': '{8}','CheckPositionCode': '{9}','Abnormal': '{10}','CheckResult': '{11}'";

            string operationRecordlJson = @"'OperationRecordNo': '{0}','OperationNo': '{1}','OperationCode': '{2}','OperationName': '{3}','OperationLevel': '{4}',
                            'OperationIncisionClass': '{5}','OperationHealClass': '{6}','IsMajorIden': '{7}','IsIatrogenic': '{8}'";

            string operationDetailJson = @"'OperationRecordNo': '{0}','OperationDoctorCode': '{1}','OperationDoctorName': '{2}','FirstOperdoctorcode': '{3}',
                            'FirstOperdoctorname': '{4}','SecondOperdoctorcode': '{5}','SecondOperdoctorname': '{6}','AnesthesiologistCode': '{7}',
                            'AnesthesiologistName': '{8}','OperationDate': '{9}','OperationFinishDate': '{10}','AnaesthesiaType': '{11}',
                            'IsComplication': '{12}','ComplicationCode': '{13}','ComplicationName': '{14}','OperationRecord': '{15}',
                            'RecordDoctorCode': '{16}','RecordDoctorName': '{17}','ListOperationDetail': [{18}]";

            string leaveHos = @"'DischargeOutcome': '{0}','HospitalizationSituation': '{1}','DtProcess': '{2}','LeaveHospitalStatus': '{3}','LeaveDoctorAdvice': '{4}'";

            string error = string.Empty;

            StringBuilder sb = null;

            #region �����鲿�� ����checkInfo 

            ArrayList al = dbHelp.QueryPacsResults(inpatientNO, dtBegin, dtEnd);

            sb = new StringBuilder();
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo patient in al)
            {
                sb.Append(string.Format(checkJson,patient.ID,patient.PVisit.PatientLocation.Dept.ID,patient.PVisit.PatientLocation.Dept.Name,
                                    patient.Surety.ID,patient.Surety.Name,patient.DoctorReceiver.ID,patient.DoctorReceiver.Name,patient.PVisit.InTime.ToString("yyyy-MM-dd"),
                                    patient.PVisit.OutTime.ToString("yyyy-MM-dd"),patient.AreaCode,patient.Disease.Memo,patient.Disease.Name));
                sb.Append(',');
            }

            al = dbHelp.QueryLisResults(inpatientNO, inTimes);
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo patient in al)
            {
                sb.Append(string.Format(checkJson, patient.ID, patient.PVisit.PatientLocation.Dept.ID, patient.PVisit.PatientLocation.Dept.Name,
                                    patient.Surety.ID, patient.Surety.Name, patient.DoctorReceiver.ID, patient.DoctorReceiver.Name, patient.PVisit.InTime.ToString("yyyy-MM-dd"),
                                    patient.PVisit.OutTime.ToString("yyyy-MM-dd"), patient.AreaCode, patient.Disease.Memo, patient.Disease.Name));
            }


            string checkInfo = sb.ToString().TrimEnd(',');
            #endregion
                
            #region �������ִ��� ����sbRecord.toString();
            al = dbHelp.QueryOperationInfo4Pay(inpatientNO);

            //���Ƚ��з���
            Hashtable ht = new Hashtable();
            ArrayList alOper = null;
            foreach (Neusoft.HISFC.Models.HealthRecord.OperationDetail info in al)
            {
                sb = new StringBuilder();
                sb.Append(info.FirDoctInfo.ID).Append(info.SecDoctInfo.ID).Append(info.ThrDoctInfo.ID).Append(info.NarcDoctInfo.ID).Append(info.OperationDate.ToString("yyyyMMdd"));
                string key = sb.ToString();

                if (ht.ContainsKey(key))
                {
                    alOper = ht[key] as ArrayList;
                    alOper.Add(info);
                }
                else
                {
                    alOper = new ArrayList();
                    alOper.Add(info);
                    ht.Add(key, alOper);
                }
            }

            sb = null;
            Neusoft.HISFC.Models.HealthRecord.OperationDetail opTmp = null;

            int recordNum = 1;

            StringBuilder sbRecord = null;
            StringBuilder sbDetail = null;

            foreach (string key in ht.Keys)
            {
                sbRecord = new StringBuilder();
                al = ht[key] as ArrayList;

                opTmp = al[0] as Neusoft.HISFC.Models.HealthRecord.OperationDetail;

                sbDetail = new StringBuilder();

                foreach (Neusoft.HISFC.Models.HealthRecord.OperationDetail opDetail in al)
                { 
                    sbDetail.Append(string.Format(operationDetailJson,recordNum.ToString(),opDetail.HappenNO,opDetail.OperationInfo.ID,
                        opDetail.OperationInfo.Name,"�ȼ�",opDetail.NickKind,opDetail.CicaKind,opDetail.HappenNO =="1" ?"1":"0","")).Append(",");
                }

                sbRecord.Append(string.Format(operationRecordlJson, recordNum.ToString(), opTmp.FirDoctInfo.ID, opTmp.FirDoctInfo.Name, opTmp.SecDoctInfo.ID,
                            opTmp.SecDoctInfo.Name, opTmp.ThrDoctInfo.ID, opTmp.ThrDoctInfo.Name, opTmp.NarcDoctInfo.ID, opTmp.NarcDoctInfo.Name,
                            opTmp.OperDate.ToString(), opTmp.OperDate.ToString(), opTmp.CicaKind, "0", "", "", "", "", "", sbDetail.ToString().TrimEnd(','))).Append(",");

                recordNum++;
            }

            sbDetail = null;

            #endregion

            #region ��ԺС�� leaveHos
            Neusoft.FrameWork.Models.NeuObject obj = null;
            string doct = string.Empty;

            dbHelp.GetEmrOutResult(inpatientNO,out obj);

            leaveHos = string.Format(leaveHos, obj.ID, obj.Memo, obj.User01, obj.User02, obj.User03);

            #endregion

            #region ���������Ϣ

            ArrayList alDiag = dbHelp.QueryDiagNoseInfo(inpatientNO,"2");

            sb = new StringBuilder();
            obj = null;
            for (int i = 1; i < 17; i++)
            {
                if (i <= al.Count)
                {
                    obj = al[i - 1] as Neusoft.FrameWork.Models.NeuObject;
                    sb.Append("'DiagnosisCode").Append(i.ToString()).Append("':'").Append(obj.ID).Append("','DiagnosisName").Append(i.ToString()).Append("':'").Append(obj.Name).Append("',");
                }
                else
                {
                    sb.Append("'DiagnosisCode").Append(i.ToString()).Append("':'','DiagnosisName").Append(i.ToString()).Append("':'',");
                }
            }

            string diagInfo = sb.ToString().TrimEnd(',');
            sb = null;
            obj = null;

            #endregion 

            #region ���������Ϣ
            Neusoft.HISFC.Models.HealthRecord.Base metCase = dbHelp.QueryMetCase4Pay(inpatientNO);

            if (metCase == null)
            {
                this.WriteLog("û�в�ѯ�����ߵĲ�����Ϣ");
                return 1;
            }
            #endregion

            json = string.Format(json, "44061104", metCase.PatientInfo.PID.PatientNO, metCase.PatientInfo.IDCard,"", metCase.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(5),
                metCase.PatientInfo.PVisit.InTime.ToString("yyyy-MM-dd"), metCase.PatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd"), metCase.PatientInfo.DoctorReceiver.ID, metCase.PatientInfo.DoctorReceiver.Name, 
                metCase.AnaphyFlag, metCase.FirstAnaphyPharmacy.ID, metCase.FirstAnaphyPharmacy.Name, metCase.YnFirst, metCase.PathNum,"0","", metCase.ReactionBlood, 
                metCase.RhBlood, metCase.Out_Type,"","","","", metCase.PatientInfo.MaritalStatus.ID, metCase.PatientInfo.Height, metCase.PatientInfo.Weight,"","","","","",
                "��Ժ���1", "��Ժ���2", "��Ժ���3", "��Ժ���1", "��Ժ���2", "��Ժ���3", "", diagInfo, checkInfo, sbRecord.ToString(), leaveHos); 

            int result = base.UploadInfo("HB5001", json, ref error);
            return result;
        }

        //5.8	������Ϣ��ѯ(HB5002)
        private int QueryCaseInfo(string patientNO)
        {
            string json = @"'AdmissionNo':'{0}','HospitalId':'{1}'";

            string error = string.Empty;

            json = string.Format(json, patientNO, Manager.setObj.HospitalID).Replace("'", "\"");

            int result = base.UploadInfo("HB5002", json, ref error);

            return result;
        }

        //5.9	������Ϣ����(HB5003)
        private int CancelCaseInfo(string patientNO)
        {
            string json = @"AdmissionNo:'{0}',HospitalId:'{1}'";

            string error = string.Empty;

            json = string.Format(json, patientNO, Manager.setObj.HospitalID).Replace("'", "\"");

            int result = base.UploadInfo("HB5003", json, ref error);

            return result;
        }

        #endregion

        #region  ��ϸ����

        /// <summary>
        /// ���������ϸ
        /// </summary>
        /// <param name="f"></param>
        private void DealFeeItemList(Neusoft.HISFC.Models.Fee.FeeItemBase f)
        {
            bool isReplace = false;
            if (f.Item.Name.Contains("����") && f.Item.Qty != 0)
            {
                //diagObj = conMgr.GetConstant("DiagSICompare", fitem.Item.ID);

                Neusoft.FrameWork.Models.NeuObject diagObj = dbHelp.QueryItemInfo("FoShanSI.SIUploadJob.QueryDictionaryInfo", f.Item.ID);

                if (diagObj != null && !string.IsNullOrEmpty(diagObj.ID))
                {
                    f.Item.ID = diagObj.ID;
                    isReplace = true;
                }
            }

            string sqlIndex = string.Empty;
            if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                sqlIndex = "FoShanSI.SIUploadJob.QueryPharmacyInfo";
            }
            else if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                sqlIndex = "FoShanSI.SIUploadJob.QueryUndrugInfo";
            }

            Neusoft.FrameWork.Models.NeuObject obj = dbHelp.QueryItemInfo(sqlIndex, f.Item.ID);

            f.Item.UserCode = obj.Memo;
            f.Memo = "0";
            f.Item.Memo = obj.User01; //����
            f.Item.MinFee.ID = obj.User02; //�������
            f.Item.MinFee.Name = obj.User03; //��������

            if (isReplace)
            {
                f.Item.Name = obj.Name;
                f.Item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(obj.User01);
                f.FT.OwnCost = f.Item.Price * f.Item.Qty;
                f.FT.PubCost = 0;
                f.FT.PayCost = 0;
            }

            if (!string.IsNullOrEmpty(f.Compare.CenterItem.ID.Trim()) && f.Compare.CenterItem.ID.Trim().Length > 1) //2010-9-1
            {
                if (f.Compare.CenterItem.ID.Trim().Contains(f.Item.UserCode.Trim()))
                {
                    f.Memo = "1";
                    f.Item.UserCode = f.Compare.CenterItem.ID.Trim();
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public ArrayList SplitFeeItemList(ArrayList feeItemLists)
        {
            ArrayList al = dbHelp.GetList("FoShanSI.SIUploadJob.QueryConst", "SIPack");

            if (al == null || al.Count == 0)
                return feeItemLists;

            Hashtable htPackage = new Hashtable();

            foreach (Neusoft.HISFC.Models.Base.Const con in al)
            {
                htPackage.Add(con.ID, con);
            }

            ArrayList unPackList = new ArrayList();
            ArrayList packList = new ArrayList();

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in feeItemLists)
            {
                if (string.IsNullOrEmpty(f.UndrugComb.ID))
                {
                    unPackList.Add(f);
                }
                else
                {
                    if (htPackage.ContainsKey(f.UndrugComb.ID))
                    {
                        packList.Add(f);
                    }
                    else
                    {
                        unPackList.Add(f);
                    }
                }
            }

            ArrayList alPackList = SetGroupItems(packList);

            unPackList.AddRange(alPackList);

            return unPackList;
        }

        /// <summary>
        /// ���ض��ײ���Ŀ���д���
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public ArrayList SetGroupItems(ArrayList feeItemLists)
        {
            if (feeItemLists.Count == 0)
                return feeItemLists;

            Hashtable htFeeItemList = new Hashtable();
            string key = string.Empty;

            Hashtable htPackAge = new Hashtable();

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList in feeItemLists)
            {
                if (!string.IsNullOrEmpty(itemList.UndrugComb.ID))
                {
                    if (!htPackAge.Contains(itemList.UndrugComb.ID))
                    {
                        htPackAge.Add(itemList.UndrugComb.ID, itemList);
                    };

                    key = itemList.UndrugComb.ID + "|" + itemList.Item.ID;
                    if (htFeeItemList.ContainsKey(key))
                    {
                        Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = htFeeItemList[key] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                        item.Item.Qty += itemList.Item.Qty;//�����ۼ�
                        item.FT.TotCost += itemList.FT.TotCost;//����ۼ�
                    }
                    else
                    {
                        htFeeItemList.Add(key, itemList);
                    }
                }
            }

            ArrayList alAfter = new ArrayList();

            foreach (string skey in htFeeItemList.Keys)
            {
                string[] keys = skey.Split('|');

                string packageCode = keys[0];

                if (htPackAge.ContainsKey(packageCode))
                {
                    htPackAge.Remove(packageCode);
                }
                else
                {
                    continue;
                }

                List<Neusoft.HISFC.Models.Fee.Item.UndrugComb> alUndrugs = new List<Neusoft.HISFC.Models.Fee.Item.UndrugComb>();

                alUndrugs = dbHelp.QueryUnDrugztDetail("FoShanSI.SIUploadJob.QueryUnDrugztDetail", packageCode);

                decimal packQty = 0;

                foreach (Neusoft.HISFC.Models.Fee.Item.UndrugComb undrug in alUndrugs)
                {
                    if (undrug.ValidState == "��Ч")
                    {
                        continue;
                    }

                    decimal packageQty = undrug.Qty;

                    string s = packageCode + "|" + undrug.ID;
                    Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = htFeeItemList[s] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                    packQty = feeItem.Item.Qty / packageQty;

                    break;
                }

                if (packQty == 0)
                {
                    continue;
                }

                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList packFeeItemList = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();

                Neusoft.HISFC.Models.Fee.Item.Undrug item = dbHelp.GetValidItemByUndrugCode("FoShanSI.SIUploadJob.QueryValidUndrugInfo", packageCode);

                packFeeItemList.Item = item;

                packFeeItemList.Item.Qty = packQty;

                packFeeItemList.FT.TotCost = item.Price * packQty;

                alAfter.Add(packFeeItemList);
            }

            return alAfter;
        }

        #endregion

        
    }
}
