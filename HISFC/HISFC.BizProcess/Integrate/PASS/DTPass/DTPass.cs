using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FS.HISFC.BizProcess.Integrate.Pass
{
    /// <summary>
    /// ��ͨDTPass ��ժҪ˵����
    /// </summary>
    public class DTPass : FS.HISFC.BizProcess.Interface.Order.IReasonableMedicine
    {
        public DTPass()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }��

        #region ע��

        /*
         * �����������Ŵ��������ҩ�ᵼ�¿��� <> �� &
         * 
         * ע:
         * 1.	<general_name>ͨ����</general_name>��<medicine_name>��Ʒ��</medicine_name>�������У�������ЩҽԺ���С�<>�����ţ����硰<medicine_name><��>�����Ƭ</medicine_name>������<>��������XML�еı����ַ�����������ӿڵĹ����У�����ѡ�<>���滻�ɡ�()������VB�п��������滻��
            test = "<��>�����Ƭ"
            test = Replace(Replace(test, "<", "("), ">", ")")     ���滻Ϊ  (��)�����Ƭ
         * 2.	<general_name>ͨ����</general_name>��<medicine_name>��Ʒ��</medicine_name>�������У�������ЩҽԺ���С��򡱷��ţ����硰<medicine_name>�����˫��Ƭ</medicine_name>�������򡱷����Ǵ�ͨ�����еķָ�������������ӿڵĹ����У�����ѡ���ȥ������VB�п��������滻��
            test = "�����˫��Ƭ"
            test = Replace(test, "��", "")       ���滻Ϊ����˫��Ƭ
         * 3.	ͬ��2���е���������&������Ҳ��Ҫ�ڽӿ��н����滻��

         * */

        #endregion

        #region ��ͨ�ӿ�

        /// <summary>
        /// ���ݽӿڲ�������ҪHIS�������
        /// </summary>
        /// <param name="a">���ܲ��������Ƶ��õĹ���</param>
        /// <param name="b">�̶�Ϊ��0������Ϊ������չʹ�ã���ʱ��������</param>
        /// <param name="XML">�ַ�����ʽ�����յ�XML��ʽ���ڵ�ķ�ʽ��������������ں�����ϸ˵��</param>
        /// <returns>��0������1������2�����ֱ����û�����⡱����һ�����⡱�͡��������⡱</returns>
        [System.Runtime.InteropServices.DllImport("dtywzxUI.dll", EntryPoint = "dtywzxUI")]
        public static extern int dtywzxUI(int a, int b, string XML);

        #endregion

        /// <summary>
        /// �����xml
        /// </summary>
        private string strXML = "";

        /// <summary>
        /// ȡҽ���Լ����ߵ���Ϣ
        /// </summary>
        /// <param name="myOrder"></param>
        /// <param name="dataTime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetBaseInfo(FS.HISFC.Models.Order.Order myOrder, string dataTime, string type)
        {
            string strBase = "";
            strBase = "<doctor_information job_number='" + myOrder.Oper.ID + "' date='" + dataTime + "'/>";//���� ��������
            strBase = strBase + "<doctor_name>" + myOrder.ReciptDoctor.Name + "</doctor_name>";//ҽ���� myOrder.Oper.Name
            strBase = strBase + "<doctor_type>" + myOrder.ReciptDoctor.User01 + "</doctor_type>";//ҽ��������� (ѡ��)
            strBase = strBase + "<department_code>" + myOrder.ReciptDept.ID + "</department_code>";//���Ҵ���  myOrder.Oper.Dept.ID
            strBase = strBase + "<department_name>" + myOrder.ReciptDept.Name + "</department_name>";//��������  myOrder.Oper.Dept.Name
            if (type == "mz")
            {
                strBase = strBase + "<case_id>" + myOrder.Patient.PID.CardNO + "</case_id>";//��������
            }
            else
            {
                strBase = strBase + "<case_id>" + myOrder.Patient.PID.PatientNO + "</case_id>";//��������
            }
            strBase = strBase + "<inhos_code>" + myOrder.Patient.ID + "</inhos_code>";//��������/סԺ��
            strBase = strBase + "<bed_no>" + ((myOrder.Patient.PVisit.PatientLocation.Bed.ID.Length > 4) ?
                myOrder.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) : myOrder.Patient.PVisit.PatientLocation.Bed.ID) + "</bed_no>";//����
            //��Ϊ������ҩ����Ὣ���������뵱ǰʱ����бȶ�,����ش˼�ȥ1��,�ú�����ҩ�������ʾ��������С�ڵ�ǰʱ�䡣
            //strBase = strBase + "<patient_information weight='" + "" + "' height='" + "" + "' birth='" + myOrder.Patient.Birthday.ToString("yyyy-MM-dd") + "'>";//���� ��� ����������(ǰ2ѡ��;

            if (type == "zy")
            {
                strBase = strBase + "<patient_information weight='" + "" + "' height='" + "" + "' birth='" + this.myPatient.Birthday.ToString("yyyy-MM-dd") + "'>";//���� ��� ����������(ǰ2ѡ��;
                strBase = strBase + "<patient_name>" + myPatient.Name + "</patient_name>";//������
                strBase = strBase + "<patient_sex>" + myPatient.Sex.Name + "</patient_sex>";//�����Ա�
            }
            else
            {
                strBase = strBase + "<patient_information weight='" + "" + "' height='" + "" + "' birth='" + this.myPatient.Birthday.ToString("yyyy-MM-dd") + "'>";//���� ��� ����������(ǰ2ѡ��;
                strBase = strBase + "<patient_name>" + this.myPatient.Name + "</patient_name>";//������
                strBase = strBase + "<patient_sex>" + this.myPatient.Sex.Name + "</patient_sex>";//�����Ա�
            }

            strBase = strBase + "<physiological_statms>" + "" + "</physiological_statms>";//�������(ѡ��)  
 
            strBase = strBase + "<boacterioscopy_effect>" + "" + "</boacterioscopy_effect>";//������(ѡ��)
            strBase = strBase + "<bloodpressure>" + "" + "</bloodpressure>";//Ѫѹ(ѡ��)
            strBase = strBase + "<liver_clean>" + "" + "</liver_clean>";//��������� (ѡ��)
            if (type == "zy")
            {
                strBase = strBase + "<pregnant>" + "" + "</pregnant>";//�и�����ʱ��
                strBase = strBase + "<pdw>" + "" + "</pdw> ";//����ʱ�������λ
            }

            strBase = strBase + "<allergic_history>";
            strBase = strBase + "<case>";
            //�����ж�
            strBase = strBase + "<case_code>" + "" + "</case_code>";//����Դ����
            strBase = strBase + "<case_name>" + "" + "</case_name>";//����Դ����
            strBase = strBase + "</case><case>";
            strBase = strBase + "<case_code>" + "" + "</case_code>";
            strBase = strBase + "<case_name>" + "" + "</case_name>";
            strBase = strBase + "</case><case>";
            strBase = strBase + "<case_code>" + "" + "</case_code>";
            strBase = strBase + "<case_name>" + "" + "</case_name></case></allergic_history>";


            strBase = strBase + "<diagnoses>";
            if (type == "mz")
            {
                //���
                //for (int i = 0; i < 3; i++)
                //{
                //    if (myOrder.Patient.Diagnoses != null && myOrder.Patient.Diagnoses.Count > i)
                //    {
                //        strBase = strBase + "<diagnose>" + ((myOrder.Patient.Diagnoses[i] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.IsMain ?
                //            (myOrder.Patient.Diagnoses[i] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name :
                //            (myOrder.Patient.Diagnoses[i] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.ID) + "</diagnose>";
                //    }
                //    else
                //    {
                //        strBase = strBase + "<diagnose>" + "" + "</diagnose>";
                //    }
                //}

                if (diagnoseArrayList.Count > 0)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diagnose in diagnoseArrayList)
                    {
                        strBase = strBase + "<diagnose type ='0' name = '" + diagnose.DiagInfo.ICD10.Name + "'>" + diagnose.DiagInfo.ICD10.ID + "</diagnose>";
                    
                    }
                }
                else 
                {
                     strBase = strBase + "<diagnose type = '0'  name = ''>" + "" + "</diagnose>";
                }
                //���������
                strBase = strBase + "<diagnose>" + "" + "</diagnose>";//���������1����������   ����:��/�����ܲ�ȫ������ҩ���/�����ڸ�Ů��ҩ���/�����ڸ�Ů��ҩ���
                strBase = strBase + "<diagnose>" + "" + "</diagnose>";
                strBase = strBase + "<diagnose>" + "" + "</diagnose>";
            }
            else
            {
                //���
                //for (int i = 0; i < 3; i++)
                //{
                //    if (myOrder.Patient.Diagnoses != null && myOrder.Patient.Diagnoses.Count > i)
                //    {
                //        strBase = strBase + "<diagnose type ='0' name = '" + (myOrder.Patient.Diagnoses[i] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.ID +
                //            "'>" + (myOrder.Patient.Diagnoses[i] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name + "</diagnose>";
                //    }
                //    else
                //    {
                //        strBase = strBase + "<diagnose type = '0'  name = ''>" + "" + "</diagnose>";
                //    }
                //}

                if (diagnoseArrayList.Count > 0)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diagnose in diagnoseArrayList)
                    {
                        strBase = strBase + "<diagnose type ='0' name = '" + diagnose.DiagInfo.ICD10.Name + "'>" + diagnose.DiagInfo.ICD10.ID + "</diagnose>";

                    }
                }
                else
                {
                    strBase = strBase + "<diagnose type = '0'  name = ''>" + "" + "</diagnose>";
                }

                //���������
                strBase = strBase + "<diagnose type ='1' name =''>" + "" + "</diagnose>";//���������1����������   ����:��/�����ܲ�ȫ������ҩ���/�����ڸ�Ů��ҩ���/�����ڸ�Ů��ҩ���
                strBase = strBase + "<diagnose type ='1' name =''>" + "" + "</diagnose>";
                strBase = strBase + "<diagnose type ='1' name =''>" + "" + "</diagnose>";
            }
            strBase = strBase + "</diagnoses></patient_information>";

            return strBase;
        }

        /// <summary>
        /// ���ݴ�����ҽ������ȡסԺXML
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="cfInfo"></param>
        /// <param name="baseInfo"></param>
        /// <param name="strXml"></param>
        /// <returns></returns>
        private int GetXML(ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes type,
            string orderInfo, string baseInfo, string dataTime, ref string strXml)
        {
            try
            {
                if (type == FS.HISFC.Models.Base.ServiceTypes.C)
                {
                    foreach (FS.HISFC.Models.Order.OutPatient.Order myOrder in alOrder)
                    {
                        if (myOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            orderInfo = orderInfo + GetOutOrderInfo(myOrder);
                        }
                    }
                    if (orderInfo == "")
                    {
                        return 0;
                    }
                    baseInfo = GetBaseInfo((alOrder[0] as FS.HISFC.Models.Order.Order), dataTime, "mz");
                    orderInfo = "<prescriptions>" + orderInfo + "</prescriptions>";
                    strXml = "<safe>" + baseInfo + orderInfo + "</safe>";
                }
                else
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order myOrder in alOrder)
                    {
                        if (myOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            orderInfo = orderInfo + GetInpatientOrderInfo(myOrder);
                        }
                    }
                    if (orderInfo == "")
                    {
                        return 0;
                    }
                    baseInfo = GetBaseInfo((alOrder[0] as FS.HISFC.Models.Order.Order), dataTime, "zy");
                    orderInfo = "<prescriptions>" + orderInfo + "</prescriptions>";
                    strXml = "<safe>" + baseInfo + orderInfo + "</safe>";
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return -1;
            }
            return 1;
        }

        #region ����

        /// <summary>
        /// ȡ����ҽ����Ϣ
        /// </summary>
        /// <param name="myOrder"></param>
        /// <returns></returns>
        private string GetOutOrderInfo(FS.HISFC.Models.Order.OutPatient.Order myOrder)
        {
            string strCfinfo = "";
            strCfinfo = strCfinfo + "<prescription id='" + myOrder.ReciptNO + "' type='" + "mz" + "' current='1'>"; //������
            strCfinfo = strCfinfo + "<medicine suspension='false' judge='true'>";
            strCfinfo = strCfinfo + "<group_number>" + myOrder.Combo.ID + "</group_number>";//���
            strCfinfo = strCfinfo + "<general_name>" + myOrder.Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</general_name>";//ͨ����
            //{9DB64486-4398-4944-85FC-48F63A21CD7E}
            strCfinfo = strCfinfo + "<license_number>" + myOrder.Item.ID + "</license_number>";//ҽԺҩƷ���� //FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(myOrder.Item.ID).TrimStart('0')
            strCfinfo = strCfinfo + "<medicine_name>" + myOrder.Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</medicine_name>";//��Ʒ��
            strCfinfo = strCfinfo + "<single_dose coef='1'>" + myOrder.DoseOnce.ToString() + "</single_dose>";//������
            strCfinfo = strCfinfo + "<times>" + myOrder.Frequency.ID + "</times>";//Ƶ�δ���
            strCfinfo = strCfinfo + "<days>" + "" + "</days>";//����
            strCfinfo = strCfinfo + "<unit>" + ((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).DoseUnit + "</unit>";//��λ��mg,g�ȣ�
            strCfinfo = strCfinfo + "<administer_drugs>" + myOrder.Usage.ID + "</administer_drugs></medicine></prescription>";//��ҩ;��
            return strCfinfo;
        }

        /// <summary>
        /// ���﹦�ܵ���
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="type">������ҩ����ö��</param>
        /// <param name="str">(��ҽ������ -- ������)(���Ϸ���/��xml --��ʱ��,)</param>
        /// <returns></returns>
        public int RationalOutPatientDrug(ArrayList alOrder, RationalType type, string str)
        {
            try
            {
                string strXml = "";
                string cfInfo = "";
                string baseInfo = "";
                int returnValue;
                switch (type)
                {
                    case RationalType.Analysis:
                        #region ����
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            returnValue = GetXML(alOrder, this.stationType, cfInfo, baseInfo, str, ref strXml);
                            if (returnValue == 1)
                            {
                                returnValue = dtywzxUI((int)type, 0, strXml);
                            }
                            return returnValue;
                        }
                        else
                        {
                            return 0;
                        }

                        #endregion
                        break;

                    case RationalType.Close:

                        #region ˢ��
                        dtywzxUI(3, 0, "");
                        #endregion

                        #region �˳�
                        dtywzxUI((int)type, 0, "");
                        #endregion

                        break;
                    case RationalType.DelOrder:

                        #region ���Ϸ�������
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            strXml = "<prescription id='" + (alOrder[0] as FS.HISFC.Models.Order.Order).ReciptNO + "'date='" + str + "'/>";
                            dtywzxUI((int)type, 0, strXml);
                        }
                        else
                        {
                            return 0;
                        }
                        #endregion
                        break;

                    case RationalType.Init:

                        #region ��ʼ��
                        dtywzxUI((int)type, 0, "");
                        #endregion
                        break;

                    case RationalType.ReShowTips:

                        #region ����ʾ
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            foreach (FS.HISFC.Models.Order.OutPatient.Order myOrder in alOrder)
                            {
                                if (myOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    strXml = "<safe><general_name>" + ((FS.HISFC.Models.Order.Order)myOrder).Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</general_name><license_number>"
                        + ((FS.HISFC.Models.Order.Order)myOrder).Item.ID + "</license_number></safe>";
                                    dtywzxUI((int)type, 0, strXml);
                                }
                            }
                        }
                        else
                        {
                            return 0;
                        }
                        #endregion
                        break;

                    case RationalType.Refresh:

                        #region ˢ��
                        dtywzxUI((int)type, 0, "");
                        #endregion
                        break;

                    case RationalType.SaveAnalysis:

                        #region ��������
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            returnValue = GetXML(alOrder, this.stationType, cfInfo, baseInfo, str, ref strXml);
                            if (returnValue == 1)
                            {
                                if (dtywzxUI((int)type, 0, strXml) != 0)
                                {
                                    return -1;
                                }
                                strXML = strXml;
                            }
                            else
                            {
                                return returnValue;
                            }
                        }
                        else
                        {
                            return 0;
                        }

                        #endregion
                        break;

                    case RationalType.SaveXML:

                        #region ��XML
                        if (string.IsNullOrEmpty(strXML))
                        {
                            if (alOrder.Count > 0 && alOrder != null)
                            {
                                if (GetXML(alOrder, stationType, cfInfo, baseInfo, str, ref strXML) == -1)
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }

                        dtywzxUI((int)type, 0, strXML);
                        #endregion
                        break;

                    case RationalType.SendDoctID:

                        #region ��ҽ������
                        dtywzxUI((int)type, 0, str);
                        #endregion
                        break;

                    case RationalType.ShowTips:

                        #region ��ʾ
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            foreach (FS.HISFC.Models.Order.OutPatient.Order myOrder in alOrder)
                            {
                                if (myOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    strXml = "<safe><general_name>" + ((FS.HISFC.Models.Order.Order)myOrder).Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</general_name><license_number>"
                        + ((FS.HISFC.Models.Order.Order)myOrder).Item.ID + "</license_number></safe>";
                                    dtywzxUI((int)type, 0, strXml);
                                }
                            }
                        }
                        else
                        {
                            return 0;
                        }
                        #endregion
                        break;

                    default:
                        return 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return -1;
            }
            return 1;
        }

        #endregion

        #region סԺ

        /// <summary>
        /// ȡסԺҽ����Ϣ
        /// </summary>
        /// <param name="myOrder"></param>
        /// <returns></returns>
        private string GetInpatientOrderInfo(FS.HISFC.Models.Order.Inpatient.Order myOrder)
        {
            string strCfinfo = "";
            //strCfinfo = strCfinfo + "<prescription  id='" + myOrder.ID + "' type='" + ((myOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG) ? "L" : "T") + "'> ";//ҽ�����ͣ�L�������ڣ�T������ʱ��
            strCfinfo = strCfinfo + "<prescription  id='" + myOrder.ID + "' type='" + ((myOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG) ? "L" : "L") + "'> ";//ҽ�����ͣ�L�������ڣ�T������ʱ��
            strCfinfo = strCfinfo + "<medicine suspension='false'    judge='true'>";
            strCfinfo = strCfinfo + "<group_number>" + myOrder.Combo.ID + "</group_number>";//���
            strCfinfo = strCfinfo + "<general_name>" + myOrder.Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</general_name>";//ͨ����
            //{9DB64486-4398-4944-85FC-48F63A21CD7E}
            strCfinfo = strCfinfo + "<license_number>" + myOrder.Item.ID + "</license_number>";//ҽԺҩƷ���� //FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(myOrder.Item.ID).TrimStart('0')
            strCfinfo = strCfinfo + "<medicine_name>" + myOrder.Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</medicine_name>";//��Ʒ��
            strCfinfo = strCfinfo + "<single_dose coef='1'>" + myOrder.DoseOnce.ToString() + "</single_dose>";//������
            strCfinfo = strCfinfo + "<frequency>" + myOrder.Frequency.ID + "</frequency>";//Ƶ�δ���
            strCfinfo = strCfinfo + "<times>" + myOrder.InjectCount.ToString() + "</times> ";//����    --?Ժע����
            strCfinfo = strCfinfo + "<unit>" + ((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).DoseUnit + "</unit>";//��λ��mg,g�ȣ�
            strCfinfo = strCfinfo + "<administer_drugs>" + myOrder.Usage.ID + "</administer_drugs>";//��ҩ;��
            strCfinfo = strCfinfo + "<begin_time>" + myOrder.BeginTime.ToString("yyyy-MM-dd hh:mm:ss") + "</begin_time> ";//��ҩ��ʼʱ��(YYYY-MM-DD HH:mm:SS)
            if (myOrder.EndTime == DateTime.MinValue)
            {
                strCfinfo = strCfinfo + "<end_time>" + "</end_time>";
            }
            else
            {
                strCfinfo = strCfinfo + "<end_time>" + myOrder.EndTime.ToString("yyyy-MM-dd hh:mm:ss") + "</end_time>";//��ҩ����ʱ��(YYYY-MM-DD HH:mm:SS)
            }
            strCfinfo = strCfinfo + "<prescription_time>" + myOrder.MOTime.ToString("yyyy-MM-dd hh:mm:ss") + "</prescription_time> ";//ҽ��ʱ��(YYYY-MM-DD HH:mm:SS)
            strCfinfo = strCfinfo + "</medicine>";
            strCfinfo = strCfinfo + "</prescription>";
            return strCfinfo;
        }

        /// <summary>
        /// סԺ���ܵ���
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        /// <param name="str">��ʱ���ҽ������</param>
        /// <returns></returns>
        public int RationalInpatientDrug(ArrayList alOrder, RationalType type, string str)
        {
            try
            {
                string strXml = "";
                string cfInfo = "";
                string baseInfo = "";
                int returnValue;
                switch (type)
                {
                    //{09B34A6E-F6E7-4416-9BE3-FA7A9CBB48E3} ������ҩסԺ���ֽӿ��޸�
                    case RationalType.InpatientAnalysis:
                        #region ����  ҽ���������(סԺ)
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            returnValue = GetXML(alOrder, stationType, cfInfo, baseInfo, str, ref strXml);
                            if (returnValue == 1)
                            {
                                returnValue = dtywzxUI((int)type, 1, strXml);
                            }
                            return returnValue;
                        }
                        else
                        {
                            return 0;
                        }

                        #endregion
                        break;
                    case RationalType.Close:
                        #region �˳�
                        dtywzxUI((int)type, 0, "");
                        #endregion
                        break;
                    case RationalType.DelOrder:
                        #region ���Ϸ�������
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            strXml = "<prescription id='" + (alOrder[0] as FS.HISFC.Models.Order.Order).ReciptNO + "'date='" + str + "'/>";
                            dtywzxUI((int)type, 0, strXml);
                        }
                        else
                        {
                            return 0;
                        }
                        #endregion
                        break;
                    case RationalType.Init:
                        #region ����
                        dtywzxUI((int)type, 0, "");
                        #endregion
                        break;
                    case RationalType.ReShowTips:
                        #region ����ʾ  ������ʾҪ����ʾ(סԺ)
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            foreach (FS.HISFC.Models.Order.Order myOrder in alOrder)
                            {
                                if (myOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    strXml = "<safe><general_name>" + ((FS.HISFC.Models.Order.Order)myOrder).Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</general_name><license_number>"
                        + ((FS.HISFC.Models.Order.Order)myOrder).Item.ID + "</license_number></safe>";
                                    dtywzxUI((int)type, 0, strXml);
                                }
                            }
                        }
                        else
                        {
                            return 0;
                        }
                        #endregion
                        break;
                    case RationalType.Refresh:
                        #region ˢ��  ��ҽ����ʼ(סԺ)
                        dtywzxUI((int)type, 0, "");
                        #endregion
                        break;
                    case RationalType.InpatientSaveAnalysis:
                        #region ��������  ҽ���������������������(סԺ)
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            returnValue = GetXML(alOrder, stationType, cfInfo, baseInfo, str, ref strXml);
                            if (returnValue == 1)
                            {
                                if (dtywzxUI((int)type, 1, strXml) != 0)
                                {
                                    return -1;
                                }
                                strXML = strXml;
                            }
                            else
                            {
                                return returnValue;
                            }
                        }
                        else
                        {
                            return 0;
                        }

                        #endregion
                        break;
                    case RationalType.SaveXML:
                        #region ��XML
                        if (string.IsNullOrEmpty(strXML))
                        {
                            if (alOrder.Count > 0 && alOrder != null)
                            {
                                if (GetXML(alOrder, stationType, cfInfo, baseInfo, str, ref strXML) == -1)
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }

                        dtywzxUI((int)type, 0, strXML);

                        #endregion
                        break;
                    case RationalType.SendDoctID:
                        #region ��ҽ������
                        dtywzxUI((int)type, 0, str);
                        #endregion
                        break;
                    case RationalType.ShowTips:
                        #region ��ʾ
                        if (alOrder.Count > 0 && alOrder != null)
                        {
                            foreach (FS.HISFC.Models.Order.Order myOrder in alOrder)
                            {
                                if (myOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    strXml = "<safe><general_name>" + ((FS.HISFC.Models.Order.Order)myOrder).Item.Name.Replace('<', '(').Replace('>', ')').Replace("��", "").Replace("&", "") + "</general_name><license_number>"
                        + ((FS.HISFC.Models.Order.Order)myOrder).Item.ID + "</license_number></safe>";
                                    dtywzxUI((int)type, 0, strXml);
                                }
                            }
                        }
                        else
                        {
                            return 0;
                        }
                        #endregion
                        break;
                    default:
                        return 0;
                        break;
                }
            }
            catch
            { }
            return 1;
        }

        #endregion

        #region IReasonableMedicine ��Ա

        /// <summary>
        /// ������ҩ�����Ƿ����
        /// </summary>
        bool passEnabled = false;

        /// <summary>
        /// ������ҩ�����Ƿ����
        /// </summary>
        public bool PassEnabled
        {
            get
            {
                return passEnabled;
            }
            set
            {
                passEnabled = value;
            }
        }

        /// <summary>
        /// ����վ��� C ���� I סԺ
        /// </summary>
        FS.HISFC.Models.Base.ServiceTypes stationType = FS.HISFC.Models.Base.ServiceTypes.C;

        /// <summary>
        /// ����վ��� C ���� I סԺ
        /// </summary>
        public FS.HISFC.Models.Base.ServiceTypes StationType
        {
            get
            {
                return stationType;
            }
            set
            {
                stationType = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string err = "";

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        /// <summary>
        /// ҩƷͳһ���
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alOrder"></param>
        /// <param name="isSave"></param>
        /// <returns>0 ��Ϊѡ��ͨ����1 ���ͨ���� -1 ���ʧ��</returns>
        public int PassDrugCheck(ArrayList alOrder, bool isSave)
        {
            FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

            //��0������1������2�����ֱ����û�����⡱����һ�����⡱�͡��������⡱
            int returnValue = 0;

            if (this.stationType == FS.HISFC.Models.Base.ServiceTypes.C)
            {
                returnValue = RationalOutPatientDrug(alOrder, RationalType.Analysis, outOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                returnValue = RationalInpatientDrug(alOrder, RationalType.InpatientAnalysis, outOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (returnValue == -1)
            {
                return -1;
            }

            if (isSave)
            {
                bool isSaveInfo = true;
                if (returnValue == 2)
                {
                    //MessageBox.Show("������ҩ�������ܴ�������,�������ʾ���濼�ǣ�\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //return 0;

                    //DialogResult dr = MessageBox.Show("������ҩ�������ܴ�����������,�Ƿ����?", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    //if (dr != DialogResult.Yes)
                    //{
                    //    return 0;
                    //}


                    if (MessageBox.Show("������ҩ�������ܴ�������,�������ʾ���濼�ǣ�\r\n�Ƿ�������棿", "����", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        isSaveInfo = false;
                        return 0;
                    }
                }
                if (isSaveInfo)
                {
                    #region ��������
                    //����
                    if (this.stationType == FS.HISFC.Models.Base.ServiceTypes.C)
                    {
                        if (RationalOutPatientDrug(alOrder, RationalType.SaveAnalysis,
                            outOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss")) == -1)
                        {
                            err = "������ҩ�����������!\r\n" + err;
                            return -1;
                        }
                    }
                    else
                    {
                        if (RationalInpatientDrug(alOrder, RationalType.InpatientSaveAnalysis,
                          outOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss")) == -1)
                        {
                            err = "������ҩ�����������!\r\n" + err;
                            return -1;
                        }
                    }
                    #endregion

                    #region ����XML����
                    if (this.stationType == FS.HISFC.Models.Base.ServiceTypes.C)
                    {
                        if (RationalOutPatientDrug(alOrder, RationalType.SaveXML, outOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss")) == -1)
                        {
                            err = "������ҩ����XML����!\r\n" + err;
                            return -1;
                        }
                    }
                    else
                    {

                        if (RationalInpatientDrug(alOrder, RationalType.SaveXML, outOrderMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss")) == -1)
                        {
                            err = "������ҩ����XML����!\r\n" + err;
                            return -1;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                if (returnValue == 2)
                {
                    MessageBox.Show("������ҩ�������ܴ�������,�������ʾ���濼�ǣ�\r\n", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return 1;
        }

        /// <summary>
        /// ������ҩϵͳ��ʼ��
        /// </summary>
        /// <param name="logEmpl"></param>
        /// <param name="logDept"></param>
        /// <param name="workStationType"></param>
        /// <returns></returns>
        public int PassInit(FS.FrameWork.Models.NeuObject logEmpl, FS.FrameWork.Models.NeuObject logDept, string workStationType)
        {
            try
            {
                this.passEnabled = true;
                return RationalOutPatientDrug(null, RationalType.Init, "");
            }
            catch (Exception ex)
            {
                err = "������ҩϵͳ��ʼ��ʧ��!\r\n" + ex.Message;
                this.passEnabled = false;
                return -1;
            }
        }

        FS.HISFC.Models.RADT.Patient myPatient = new FS.HISFC.Models.RADT.Patient();

        /// <summary>
        /// ���ô��뻼�߻�����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int PassSetPatientInfo(FS.HISFC.Models.RADT.Patient patient, FS.FrameWork.Models.NeuObject recipeDoct)
        {
            this.myPatient = patient;
            return 1;
        }

        ArrayList diagnoseArrayList;

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="diagnoseList"></param>
        /// <returns></returns>
        public int PassSetDiagnoses(ArrayList diagnoseList)
        {
            diagnoseArrayList = diagnoseList;
            return 1;
        }


        /// <summary>
        /// ������ҩ���ܳ�ʼ��ˢ��
        /// </summary>
        /// <returns></returns>
        public int PassRefresh()
        {
            if (this.passEnabled)
            {
                return RationalOutPatientDrug(null, RationalType.Refresh, "");
            }
            return 1;
        }

        /// <summary>
        /// ������ҩ���ܹر�
        /// </summary>
        /// <returns></returns>
        public int PassClose()
        {
            if (this.passEnabled)
            {
                return RationalOutPatientDrug(null, RationalType.Close, "");
            }
            return 1;
        }

        /// <summary>
        /// ��ʾ����ҩƷҪ����ʾ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="LeftTop"></param>
        /// <param name="RightButton"></param>
        /// <param name="isFirst">�Ƿ��״���ʾ</param>
        /// <returns></returns>
        public int PassShowSingleDrugInfo(FS.HISFC.Models.Order.Order order, System.Drawing.Point LeftTop, System.Drawing.Point RightButton, bool isFirst)
        {
            ArrayList orderList = new ArrayList();
            orderList.Add(order);

            return RationalInpatientDrug(orderList, RationalType.ShowTips, "");
        }

        /// <summary>
        /// ��ʾҪ����ʾ
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public int PassShowFloatWindow(bool isShow)
        {
            return 1;
        }

        /// <summary>
        /// ��ʾ������Ϣ��Ŀǰ�����ڲ�ѯ�˵���
        /// </summary>
        /// <param name="order"></param>
        /// <param name="queryType"></param>
        /// <param name="alShowMenu"></param>
        /// <returns></returns>
        public int PassShowOtherInfo(FS.HISFC.Models.Order.Order order, FS.FrameWork.Models.NeuObject queryType, ref ArrayList alShowMenu)
        {
            return 1;
        }

        /// <summary>
        /// ��ȡҩƷ������Ϣ
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int PassShowWarnDrug(FS.HISFC.Models.Order.Order order)
        {
            return 1;
        }

        #endregion
    }

    /// <summary>
    /// ������ҩ����ö��
    /// </summary>
    public enum RationalType
    {

        /// <summary>
        /// ��ʼ��
        /// </summary>
        Init = 0,

        /// <summary>
        /// �˳�
        /// </summary>
        Close = 1,

        /// <summary>
        /// ˢ��
        /// </summary>
        Refresh = 3,

        /// <summary>
        /// ����
        /// </summary>
        Analysis = 4,

        /// <summary>
        /// ��ʾ
        /// </summary>
        ShowTips = 12,

        /// <summary>
        /// ��������
        /// </summary>
        SaveAnalysis = 13,

        /// <summary>
        /// ���Ϸ�������
        /// </summary>
        DelOrder = 512,

        /// <summary>
        /// ����ҽ������
        /// </summary>
        SendDoctID = 768,

        /// <summary>
        /// ����ʾ
        /// </summary>
        ReShowTips = 4108,

        /// <summary>
        /// ��XML
        /// </summary>
        SaveXML = 4109,

        /// <summary>
        /// סԺ�������
        /// </summary>
        InpatientAnalysis = 28676,

        /// <summary>
        /// סԺ�����������
        /// </summary>
        InpatientSaveAnalysis = 28685,
    }
}
