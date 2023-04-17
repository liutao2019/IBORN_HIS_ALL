using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// ������<br>DiagnoseMediccare</br>
    /// <Font color='#FF1111'>ҽ�����ҵ����</Font><br></br>
    /// [�� �� ��: ]<br>������</br>
    /// [����ʱ��: ]<br>2007-08-13</br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public class DiagnoseMedicare : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public DiagnoseMedicare()
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

        #region ˽��
        /// <summary>
        /// ��ʵ���л�ȡ�����γ�����
        /// </summary>
        /// <param name="Diag"></param>
        /// <returns></returns>
        private string[] myGetItemParm(FS.HISFC.Models.HealthRecord.Diagnose Diag)
        {
            string[] strParm = new string[13];
            String isMain = "";
            if (Diag.DiagInfo.IsMain)
            {
                isMain = "1";
            }
            else
            {
                isMain = "0";
            }
            strParm[0] = Diag.DiagInfo.Patient.ID; // סԺ��ˮ��
            strParm[1] = Diag.DiagInfo.HappenNo.ToString();// �������
            strParm[2] = Diag.DiagInfo.Patient.Card.ID; //���￨��
            strParm[3] = Diag.DiagInfo.DiagType.ID; // ������
            strParm[4] = Diag.DiagInfo.ICD10.ID;// ���ICD��
            strParm[5] = Diag.DiagInfo.ICD10.Name;// �������
            strParm[6] = Diag.DiagInfo.ICD10.SpellCode;// ���ƴ��
            strParm[7] = isMain;// �Ƿ������1��0��
            strParm[8] = Diag.Pvisit.PatientLocation.Dept.ID;// ���߿���
            strParm[9] = this.Operator.ID;// ����Ա
            strParm[10] = this.GetSysDateTime().ToString();// ����ʱ��
            strParm[11] = Diag.DiagInfo.DiagDate.ToString();// ���ʱ��
            strParm[12] = Diag.DiagInfo.Doctor.ID;// ���ҽ��

            return strParm;
        }
        private ArrayList myQuery(string strSQL)
        {
            ArrayList arrl = new ArrayList();
            FS.HISFC.Models.HealthRecord.Diagnose diags;
            this.ExecQuery(strSQL);

            try
            {
                while (this.Reader.Read())
                {
                    diags = new FS.HISFC.Models.HealthRecord.Diagnose();

                    diags.DiagInfo.Patient.ID = this.Reader[0].ToString();//סԺ��ˮ��
                    diags.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());//�������
                    diags.DiagInfo.Patient.Card.ID = this.Reader[2].ToString();//���￨��
                    diags.DiagInfo.DiagType.ID = this.Reader[3].ToString();//������
                    diags.DiagInfo.ICD10.ID = this.Reader[4].ToString();//���ICD��
                    diags.DiagInfo.ICD10.Name = this.Reader[5].ToString();//�������
                    diags.DiagInfo.ICD10.SpellCode = this.Reader[6].ToString();//���ƴ��
                    diags.DiagInfo.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());//�Ƿ������
                    diags.Pvisit.PatientLocation.Dept.ID = this.Reader[8].ToString();//���߿���
                    diags.ID = this.Reader[9].ToString();//����Ա
                    diags.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());//����ʱ��
                    diags.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());//���ʱ��
                    diags.DiagInfo.Doctor.ID = this.Reader[12].ToString();//���ҽ��
                    diags.DiagInfo.User03 = this.Reader[13].ToString();//��ϴ������ 'ҽ��';'ICD10'
                    diags.OperType = this.Reader[14].ToString();//��� 1 ҽ��վ¼�����  2 ������¼�����
                    arrl.Add(diags);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ�����������Ϣ����![MET_COM_DIAGNOSE_MEDICARE]" + ex.Message;
                this.WriteErr();
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            this.Reader.Close();

            return arrl;
        }
        #endregion

        #region ����
        #endregion

        #region ����
        /// <summary>
        /// ����һ��ҽ�������Ϣ
        /// </summary>
        /// <param name="Diag"></param>
        /// <returns></returns>
        public int InsertDiagnoseMedicare(FS.HISFC.Models.HealthRecord.Diagnose Diag)
        {
            String strSQL = "";
            if (this.Sql.GetSql("CASE.DiagnoseMedicare.Insert", ref strSQL) == -1) return -1;
            string[] strParm = myGetItemParm(Diag);
            return this.ExecNoQuery(strSQL, strParm);
        }
        /// <summary>
        /// ��ѯҽ�������Ϣ
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <param name="HappenNo">������� ��ѯ���п�����%</param>
        /// <returns>�����Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Diagnose</returns>
        public ArrayList QueryDiagnoseMedicare(string InpatientNO,string HappenNo)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.DiagnoseMedicare.Select", ref strSQL) == -1) return null;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, HappenNo);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.DiagnoseMedicare.Select";
                return null;
            }
            return this.myQuery(strSQL);

        }
        /// <summary>
        /// ��ѯ����(met_cas_diagnose)��ҽ��(met_com_diagnose_medicare)�������Ϣ
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <returns>�����Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Diagnose</returns>
        public ArrayList QueryDiagnoseBoth(string InpatientNO)
        {
            String strSQL = "";
            if (this.Sql.GetSql("CASE.DiagnoseBoth.Select", ref strSQL) == -1) return null;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.DiagnoseBoth.Select";
                return null;
            }
            return this.myQuery(strSQL);

        }
        /// <summary>
        /// ��ȡ��Ժ�����
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns>���������</returns>
        public ArrayList GetOutMainDiagnose(string inpatientNo)
        {
            ArrayList myArr = null;//��Ÿ�סԺ��ˮ�����������Ϣ
            ArrayList mainDiagNose = new ArrayList();//��ų�Ժ�������Ϣ
            myArr = this.QueryDiagnoseBoth(inpatientNo);
            FS.HISFC.Models.HealthRecord.Diagnose tempDiagNose = null;
            #region ��ѯ��Ժ����ϣ���������
            for (int i = 0;i < myArr.Count; i++)
            {
                tempDiagNose = (FS.HISFC.Models.HealthRecord.Diagnose)myArr[i];

                #region �ж��Ƿ��Ժ�����

                if (tempDiagNose.DiagInfo.User03 == "ҽ��")//�Ƿ�ҽ�����
                {
                    if (tempDiagNose.DiagInfo.IsMain)//�Ƿ������
                    {
                        if (tempDiagNose.DiagInfo.DiagType.ID == "1")//��������Ƿ�������ϣ���Ժ��ϣ�
                        {
                            mainDiagNose.Add(tempDiagNose);
                        }
                    }
                }
                #endregion
            }
            #endregion
            return mainDiagNose;
        }
        /// <summary>
        /// �޸�ҽ�����
        /// </summary>
        /// <param name="dg">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>�ɹ�0 ʧ��-1</returns>
        public int UpdateDiagnoseMedicare(FS.HISFC.Models.HealthRecord.Diagnose dg)
        {
            String strSQL = "";
            if (this.Sql.GetSql("CASE.DiagnoseMedicare.Update.1", ref strSQL) == -1) return -1;
            string[] strParm = this.myGetItemParm(dg);
            return this.ExecNoQuery(strSQL, strParm);
        }
        /// <summary>
        /// ɾ��ҽ����ϱ�����¼
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <param name="happenNO">HAPPEN_NO</param>
        /// <returns>ʧ�� -1</returns>
        public int DeleteDiagnoseMedicare(String InpatientNO, int happenNO)
        {
            String strSQL = "";
            if (this.Sql.GetSql("CASE.DiagnoseMedicare.Delete.1", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, happenNO.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�CASE.DiagnoseMedicare.Delete.1";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #endregion

        #region �¼�
        #endregion

        #region �ӿ�ʵ��
        #endregion

    }
}
