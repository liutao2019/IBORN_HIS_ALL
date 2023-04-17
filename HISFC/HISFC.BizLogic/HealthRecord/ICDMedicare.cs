using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;
using FS.HISFC.Models.HealthRecord.EnumServer;

namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// ������<br>ICDMedicare</br>
    /// <Font color='#FF1111'>[��������: ҽ��ICDҵ���]</Font><br></br>
    /// [�� �� ��: ]<br>������</br>
    /// [����ʱ��: ]<br>2007-08-14</br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public class ICDMedicare : FS.FrameWork.Management.Database
    {
        	/// <summary>
	/// ���캯��
	/// </summary>
        public ICDMedicare()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        #region ˽��
        #endregion

        #region ����
        #endregion

        #region ����
        #endregion

        #endregion

        #region ����
        #endregion

        #region ����

        #region ��Դ�ͷ�
        #endregion

        #region ��¡
        #endregion

        #region ˽��
        /// <summary>
        /// ��Reader  �ж�ȡ����
        /// </summary>
        /// <returns>����δ����Ĵ��󷵻� null �м�¼list.Count >1  û�м�¼ list.Count =0</returns>
        private ArrayList ICDReaderInfo()
        {
            //���� ��̬���飬 �����洢��������Ϣ
            ArrayList list = new ArrayList();
            try
            {
                FS.HISFC.Models.HealthRecord.ICDMedicare icdM = null;
                while (this.Reader.Read())
                {
                    icdM = new FS.HISFC.Models.HealthRecord.ICDMedicare();

                    icdM.SeqID = Reader[0].ToString();//���к�
                    icdM.ID = Reader[1].ToString();//ҽ����ϴ���
                    icdM.Name = Reader[2].ToString();//ҽ���������
                    icdM.SpellCode = Reader[3].ToString();//ҽ�����ƴ��
                    icdM.IcdType = Reader[4].ToString();//1 ICD10��2 ��ҽ����3 ʡҽ��
                    //��ICD�����б�
                    list.Add(icdM);
                }
                this.Reader.Close(); //�ر�reade

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!this.Reader.IsClosed) // �ж��Ƿ�ر���Reader
                {
                    this.Reader.Close();//û�йر����ȹر�
                }

                return null; //���ִ��󷵻�null 
            }
            return list;
        }
        /// <summary>
        /// ��ȡ��ѯ���
        /// </summary>
        /// <param name="dType">��ѯ��� 0 ȫ����1 ICD10��2 ��ҽ����3 ʡҽ��</param>
        /// <returns>sql
        ///          ������null</returns>
        private String GetSQL(String dType)
        {
            String strSQL = "";
            switch (dType)
            {
                case "0"://ȫ��
                    if (this.Sql.GetSql("Case.ICDDML.Query.ICDAll.Base", ref strSQL) == -1)
                    {
                        this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICDAll.Base";
                        return null;
                    }
                    break;
                case "1"://ICD10
                    if (this.Sql.GetSql("Case.ICDDML.Query.ICD10ForMedicare.Base", ref strSQL) == -1)
                    {
                        this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICDAll.Base";
                        return null;
                    }
                    break;
                default://ICDMEDICARE 2 ��ҽ����3 ʡҽ��
                    if (this.Sql.GetSql("Case.ICDDML.Query.ICDMedicare.Base", ref strSQL) == -1)
                    {
                        this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICDMedicare.Base";
                        return null;
                    }
                    try
                    {
                        //��ʽ��SQL���
                        strSQL = string.Format(strSQL, dType);
                    }
                    catch (Exception ex)
                    {
                        this.Err = "SQL��丳ֵ����!" + ex.Message;
                    }
                    break;
            }
            return strSQL;
        }

        //{32F5AA9B-FAB2-4390-A82C-D34266406FA3}
        /// <summary>
        /// ��ȡ�ϱ���ѯ��䣬��Ⱦ���Ƿ����ڼ��ұ��ഫȾ��
        /// </summary>
        /// <param name="dType">��ѯ��� 0 icd10�����ѯ��1 ICD10����ģ����ѯ</param>
        /// <returns>sql
        ///          ������null</returns>
        private String GetICDSQL(String dType)
        {
            String strSQL = "";
            switch (dType)
            {
                case "0"://icd10�����ѯ
                    if (this.Sql.GetSql("Case.ICDDML.Query.ICD10ForMedicare.ICDIsUpload", ref strSQL) == -1)
                    {
                        this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD10ForMedicare.ICDIsUpload";
                        return null;
                    }
                    break;
                case "1"://ICD10����ģ����ѯ
                    if (this.Sql.GetSql("Case.ICDDML.Query.ICD10ForMedicare.ICDNameIsUpload", ref strSQL) == -1)
                    {
                        this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD10ForMedicare.ICDNameIsUpload";
                        return null;
                    }
                    break;
            }
            return strSQL;
        }
        #endregion

        #region ����
        #endregion

        #region ����

        /// <summary>
        /// ��ѯҽ��ICD��Ϣ
        /// </summary>
        /// <param name="dType">��ѯ��� 0 ȫ����1 ICD10��2 ��ҽ����3 ʡҽ��</param>
        /// <returns>arrayList.Count >= 1 ��ȷ��÷���������ICD����
        ///          arrayList.Count == 0 û�з���������ICD���� 
        ///          ������null</returns>
        public ArrayList Query(String dType)
        {
            String strQuerySQL = "";
            //�������飬�����ѯ������Ϣ
            ArrayList arryList = new ArrayList();
            try
            {
                //��ȡSQL
                strQuerySQL = this.GetSQL(dType);
                //ִ�в�ѯ����
                this.ExecQuery(strQuerySQL);
                //��ȡ����
                arryList = ICDReaderInfo();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!Reader.IsClosed) // ���û�йر�reader
                {
                    this.Reader.Close(); //�ر�reader
                }

                return null; // ���ִ��󷵻�null
            }

            return arryList;
        }

        //{32F5AA9B-FAB2-4390-A82C-D34266406FA3}
        public string isICDUpload(String type, String icdName)
        {
            //������0����Ҫ��ʾ��Ⱦ���ϱ���������1���ǹ��Ҵ�Ⱦ�����Ƶķ�Χ��
            string sql = GetICDSQL(type);
            string sql2 = string.Format(sql, icdName);

            
            //int isUpload = this.ExecQuery(sql2);

            //{ff204e15-7044-4176-a7a1-fd52b06129b6}
            string isUpload = this.ExecSqlReturnOne(sql2);
            //int isUpload1 = this.ExecNoQuery(sql2);
            //int isUpload2 = this.ExecQuery(sql2);
            //int isUpload3 = this.ExecQueryByIndex(sql2);

            return isUpload;
        }


        #endregion

        #endregion
    }
}
