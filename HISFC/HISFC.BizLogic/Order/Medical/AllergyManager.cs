using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using FS.FrameWork.Function;
using FS.HISFC.Models.Order.Medical;

namespace FS.HISFC.BizLogic.Order.Medical
{
    /// <summary>
    /// [��������: ��������ҵ����]
    /// [�� �� ��: wangw]
    /// [����ʱ��: 2008-3-20]
    /// </summary>
    public class AllergyManager : FS.FrameWork.Management.Database
    {
        #region ���췽��

        public AllergyManager()
        {
        }

        #endregion

        /// <summary>
        /// ��ȡ����ʵ�����Ϣ
        /// </summary>
        /// <param name="allergyInfo">����ʵ��</param>
        /// <returns>�ɹ����ز�������/ʧ�ܷ���nullֵ</returns>
        protected string[] myGetParmAllergy(FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo)
        {
            try
            {
                string[] parm = {
                                            allergyInfo.PatientNO,//�����Ż���סԺ��
                                            NConvert.ToInt32(allergyInfo.PatientType).ToString(),//1���ﻼ��/2סԺ����
                                            allergyInfo.Allergen.ID,//ҩƷԺ�ڴ���
                                            allergyInfo.Allergen.Name,//����ҩ��
                                            allergyInfo.Symptom.ID,//1��Ƥ������ 2���ݿ� 3��ҩ��
                                            NConvert.ToInt32(allergyInfo.ValidState).ToString(),//1��Ч/0��Ч
                                            allergyInfo.Remark,//��ע
                                            allergyInfo.Oper.ID,//����Ա����
                                            allergyInfo.Oper.OperTime.ToString(),//����ʱ��(����)
                                            allergyInfo.CancelOper.ID,//������
                                            allergyInfo.CancelOper.OperTime.ToString(),//����ʱ��
                                            allergyInfo.Type.ToString(),//��������
                                            allergyInfo.ID,//����Ż���סԺ��ˮ��
                                            allergyInfo.HappenNo.ToString(),//�������
                                            //{D1B1616C-3863-40f6-AAD5-11D9161C6B14}
                                            allergyInfo.Allergen.Memo//����ҩ�����
                                       };
                return parm;
            }
            catch (System.Exception ex)
            {
                this.Err = "��ʵ���ȡ������Ϣʱ����! \n" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ����
        /// </summary>
        /// <param name="strSql">��ѯ����SQL���</param>
        /// <returns>���ز�ѯ����</returns>
        protected ArrayList myGetAllergy(string strSql)
        {
            ArrayList al = new ArrayList();
            AllergyInfo allergyInfo = null;
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "��ȡ���߹�����Ϣʧ��!" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    allergyInfo = new AllergyInfo();
                    allergyInfo.PatientNO = this.Reader[0].ToString();
                    allergyInfo.PatientType = (FS.HISFC.Models.Base.ServiceTypes)NConvert.ToInt32(this.Reader[1].ToString());
                    FS.FrameWork.Models.NeuObject allergenObj = new FS.FrameWork.Models.NeuObject();
                    allergenObj.ID = this.Reader[2].ToString();
                    allergenObj.Name = this.Reader[3].ToString();
                    //{D1B1616C-3863-40f6-AAD5-11D9161C6B14}
                    //allergenObj.Memo = this.Reader[14].ToString();
                    allergyInfo.Allergen = allergenObj;
                    FS.FrameWork.Models.NeuObject symptomObj = new FS.FrameWork.Models.NeuObject();
                    symptomObj.ID = this.Reader[4].ToString();
                    allergyInfo.Symptom = symptomObj;
                    allergyInfo.ValidState = NConvert.ToBoolean(this.Reader[5].ToString());
                    allergyInfo.Remark = this.Reader[6].ToString();
                    allergyInfo.Oper.ID = this.Reader[7].ToString();
                    allergyInfo.Oper.OperTime = NConvert.ToDateTime(this.Reader[8].ToString());
                    allergyInfo.CancelOper.ID = this.Reader[9].ToString();
                    allergyInfo.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[10].ToString());
                    allergyInfo.Type = (FS.HISFC.Models.Order.Medical.AllergyType)Enum.Parse(typeof(FS.HISFC.Models.Order.Medical.AllergyType), this.Reader[11].ToString());
                    allergyInfo.ID = this.Reader[12].ToString();
                    allergyInfo.HappenNo = NConvert.ToInt32(this.Reader[13].ToString());


                    al.Add(allergyInfo);
                }
                return al;
            }
            catch (System.Exception ex)
            {
                this.Err = "��ȡ������Ϣʱ ��READER�ڶ�ȡ��Ϣ����!" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="inpatientNo">����Ż���סԺ��ˮ��</param>
        /// <param name="patientKind">1���ﻼ��/2סԺ����</param>
        /// <returns>�ɹ��������HappenNo/ʧ�ܷ���-1</returns>
        public int GetMaxHappenNo(string inpatientNo, string patientKind)
        {
            string strSql = string.Empty;
            //ȡSQL���
            if (this.Sql.GetCommonSql("Order.Allergy.GetMaxHappenNo", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.Allergy.GetMaxHappenNo�ֶ�!";
                return -1;
            }

            strSql = string.Format(strSql, inpatientNo, patientKind);
            //ִ��sql���
            this.ExecQuery(strSql);
            int maxHappenNo = 1;

            try
            {
                while (this.Reader.Read())
                {
                    string temp = this.Reader[0].ToString();
                    if (temp != "")
                    {
                        maxHappenNo = NConvert.ToInt32(temp) + 1;
                        return maxHappenNo;
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.Err = "��ȡ�������Ŵ���!" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return maxHappenNo;
        }

        /// <summary>
        /// ����SQL��ȡ������Ϣ
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        private ArrayList MyQueryAllergyInfo(string whereIndex,params object[] parms)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Order.Allergy.QueryAllAllergyInfo", ref strSql) == -1)
            {
                this.Err = "����Sqlʧ�ܣ�[Order.Allergy.QueryAllAllergyInfo]";
                return null;
            }

            if (!string.IsNullOrEmpty(whereIndex))
            {
                string whereSql = "";
                if (this.Sql.GetCommonSql(whereIndex, ref whereSql) == -1)
                {
                    this.Err = "����Sqlʧ�ܣ�[" + whereIndex + "]";
                    return null;
                }

                whereSql = string.Format(whereSql, parms);

                strSql = strSql + "\r\n" + whereSql;
            }
            return this.myGetAllergy(strSql);
        }

        /// <summary>
        /// ��ȡ���й�����Ϣ
        /// </summary>
        /// <returns>������Ϣ����</returns>
        public ArrayList QueryAllergyInfo()
        {
            return MyQueryAllergyInfo("");
        }

        /// <summary>
        /// ��ѯ���߹�����Ϣ
        /// </summary>
        /// <param name="inPatientNo">��ˮ��</param>
        /// <returns>������Ϣ</returns>
        public ArrayList QueryAllergyInfo(string inPatientNo)
        {
            return MyQueryAllergyInfo("Order.Allergy.QueryAllergyByiPatientNo", inPatientNo);

            //string strSql = string.Empty;
            //string strWhere = string.Empty;
            ////ȡSELECT���
            //if (this.Sql.GetCommonSql("Order.Allergy.QueryAllAllergyInfo", ref strSql) == -1)
            //{
            //    this.Err = "����Order.Allergy.QueryAllAllergyInfo�ֶ�ʧ��";
            //    return null;
            //}
            ////ȡWHERE���
            //if (this.Sql.GetCommonSql("Order.Allergy.QueryAllergyByiPatientNo", ref strWhere) == -1)
            //{
            //    this.Err = "����Order.Allergy.QueryAllergyByiPatientNo�ֶ�ʧ��";
            //    return null;
            //}

            //strSql = strSql + strWhere;

            //strSql = string.Format(strSql, inPatientNo);

            //return this.myGetAllergy(strSql);
        }

        /// <summary>
        /// ��ѯ��Ч�Ļ��߹�����Ϣ
        /// </summary>
        /// <param name="patientNO"></param>
        /// <param name="patientType">1 ���� 2 סԺ</param>
        /// <returns></returns>
        public ArrayList QueryValidAllergyInfo(string patientNO, string patientType)
        {
            return this.MyQueryAllergyInfo("Order.Allergy.QueryValidAllergyByPatient", patientNO, patientType);
        }

        /// <summary>
        /// ��ѯ���߹�����Ϣ
        /// {D1B1616C-3863-40f6-AAD5-11D9161C6B14}
        /// </summary>
        /// <param name="patientNo">���￨��/סԺ��</param>
        /// <returns>������Ϣ</returns>
        public ArrayList QueryAllergyInfo(string patientNO, string patientType)
        {
            return this.MyQueryAllergyInfo("Order.Allergy.QueryAllergyByPatient", patientNO, patientType);
            //string strSql = string.Empty;
            //string strWhere = string.Empty;
            ////ȡSELECT���
            //if (this.Sql.GetCommonSql("Order.Allergy.QueryAllAllergyInfo", ref strSql) == -1)
            //{
            //    this.Err = "����Order.Allergy.QueryAllAllergyInfo�ֶ�ʧ��";
            //    return null;
            //}
            ////ȡWHERE���
            //if (this.Sql.GetCommonSql("Order.Allergy.QueryAllergyByPatient", ref strWhere) == -1)
            //{
            //    this.Err = "����Order.Allergy.QueryAllergyByPatient�ֶ�ʧ��";
            //    return null;
            //}

            //strSql = strSql +" " + strWhere;

            //strSql = string.Format(strSql, patientNO, patientType);

            //return this.myGetAllergy(strSql);
        }

        /// <summary>
        /// ��ѯ���߹�����Ϣ
        /// </summary>
        /// <param name="patient">סԺ��</param>
        /// <param name="inpatientNO">��ˮ��</param>
        /// <param name="happenNO">�������</param>
        /// <returns>������Ϣ</returns>
        public ArrayList GetAllergyInfo(string patient, string inpatientNO, int happenNO)
        {
            return MyQueryAllergyInfo("Order.Allergy.QueryAllergyInfoWhere", inpatientNO, happenNO);
            //string strSql = string.Empty;
            //string strWhere = string.Empty;
            ////ȡSELECT���
            //if (this.Sql.GetCommonSql("Order.Allergy.QueryAllAllergyInfo", ref strSql) == -1)
            //{
            //    this.Err = "����Order.Allergy.QueryAllAllergyInfo�ֶ�ʧ��";
            //    return null;
            //}
            ////ȡWHERE���
            //if (this.Sql.GetCommonSql("Order.Allergy.QueryAllergyInfoWhere", ref strWhere) == -1)
            //{
            //    this.Err = "����Order.Allergy.QueryAllergyByiPatientNo�ֶ�ʧ��";
            //    return null;
            //}

            //strSql = strSql + strWhere;

            //strSql = string.Format(strSql, patient, inpatientNO, happenNO);

            //return this.myGetAllergy(strSql);
        }

        /// <summary>
        /// ����һ��������Ϣ
        /// </summary>
        /// <param name="allergyInfo">����ʵ��</param>
        public int InsertAllergyInfo(FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Order.Allergy.InsertAllergyInfo", ref strSql) == -1)
            {
                this.Err = "����Order.Allergy.InsertAllergyInfo�ֶη�������!";
            }
            
            try
            {
                string[] parm = this.myGetParmAllergy(allergyInfo);
                strSql = string.Format(strSql, parm);
            }
            catch (System.Exception ex)
            {
                this.Err = "��ʽ��SQL���Order.Allergy.InsertAllergyInfo����!" + ex.Message;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����һ��������Ϣ
        /// </summary>
        /// <param name="allergyInfo">����ʵ��</param>
        public int UpdateAllergyInfo(FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Order.Allergy.UpdateAllergy", ref strSql) == -1)
            {
                this.Err = "����Order.Allergy.UpdateAllergy�ֶ�ʧ��" + this.Err;
            }

            try
            {
                string[] parm = this.myGetParmAllergy(allergyInfo);
                strSql = string.Format(strSql, parm);
            }
            catch (System.Exception ex)
            {
                this.Err = "��ʽ��SQL���Order.Allergy.UpdateAllergy��������!" + ex.Message;
            }

            return this.ExecNoQuery(strSql);
        }

        public int SetAllergyInfo(FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo)
        {
            int parm = this.UpdateAllergyInfo(allergyInfo);
            if (parm == 0)
            {
                parm = this.InsertAllergyInfo(allergyInfo);
            }
            return parm;
        }
    }
}
