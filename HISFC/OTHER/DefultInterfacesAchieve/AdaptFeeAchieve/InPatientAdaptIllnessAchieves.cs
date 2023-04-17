using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FS.DefultInterfacesAchieve.AdaptFeeAchieve
{
    public class InPatientAdaptIllnessAchieves : HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient
    {
        #region IAdptIllnessInPatient ��Ա
        /// <summary>
        /// ��������(Ƕ����ѡ����Ŀ��)
        /// </summary>
        /// <param name="patientObj"></param>
        /// <param name="inpatientDetail"></param>
        /// <returns></returns>
        public int ProcessInpatientFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientObj, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList inpatientDetail)
        {
            string practicableSymptomText = "";
            int returnValue = this.QueryItem(patientObj, inpatientDetail, ref practicableSymptomText);
            switch (returnValue)
            {
                case 0: //û��ά��
                    {
                        //������

                        break;
                    }
                case 1: //��ά�� 
                    {
                        DialogResult d = System.Windows.Forms.MessageBox.Show("����Ŀ����Ӧ֢����ά����\n" + practicableSymptomText + "\n" + "�Ƿ�ѡ������Ӧ֢�շ�", "��ʾ", MessageBoxButtons.YesNoCancel);
                        if (d == DialogResult.Cancel)
                        {
                            return 1;
                        }
                        else if (d == DialogResult.Yes)
                        {


                            //�Ƿ���Ӧ֢��Ϊ1 ����inpatientDetail.Item.Memo
                            inpatientDetail.Item.Memo = "1";
                            return 1;
                        }
                        else
                        {
                            inpatientDetail.Item.Memo = "0";
                            //������  
                        }
                        
                        break;
                    }
                case -1: //����
                    {
                        break;
                    }

                default:
                    break;
            }
            return 1;
        }
        /// <summary>
        /// ��������(Ƕ���ڱ�����)
        /// </summary>
        /// <param name="patientObj">������Ϣ</param>
        /// <param name="alInpatientDetail">������Ϣ</param>
        /// <returns></returns>
        public int SaveInpatientFeeDetail(FS.HISFC.Models.RADT.PatientInfo patientObj, ref System.Collections.ArrayList alInpatientDetail)
        {
            Function.SISpecialLimit myManager = new FS.DefultInterfacesAchieve.Function.SISpecialLimit();
            
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alInpatientDetail)
            {
                
                if (feeItemList.Item.Memo == "1")
                {
                    //ɾ���м��
                    myManager.DeleteMedItemList(feeItemList);

                    //�����м��
                    int iReturn = myManager.InsertMedItemList(patientObj, feeItemList);

                    if (iReturn < 0)
                    {
                        
                        MessageBox.Show("������Ӧ֢����!" + myManager.Err);
                        return -1;
                    }
                    
                }
            }
            return 1;
        }

        #endregion

        #region ˽�з���
        
        /// <summary>
        /// �Ӷ��ձ��в���ά����Ӧ֢����Ŀ
        /// </summary>
        /// <param name="patientObj"></param>
        /// <param name="feeitemlist"></param>
        /// <param name="practicableSymptomText">��Ӧ֢�ı�</param>
        /// <returns> -1 ���� 0 û��ά�� 1 ��ά��</returns>
        private int QueryItem(FS.HISFC.Models.RADT.PatientInfo patientObj, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeitemlist, ref string practicableSymptomText)
        {

            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FS.HISFC.Models.SIInterface.Compare myCompare = new FS.HISFC.Models.SIInterface.Compare();

            if (patientObj != null || patientObj.ID != "")
            {
                myInterface.GetCompareSingleItem(patientObj.Pact.ID, feeitemlist.Item.ID, ref myCompare);
                if (myCompare.Ispracticablesymptom)
                {
                    practicableSymptomText = myCompare.Practicablesymptomdepiction;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            if (feeitemlist.Item.Memo != "1")
            {
                //��ά�����ж�����Ŀ

            }
            return 0;
        }
        #endregion

    }


}
