using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.Operation
{
    public  class OpsDiagnose : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ���»��������Ϣ
        /// </summary>
        /// <param name="Diagnose"></param>
        /// <returns></returns> 
        public int UpdatePatientDiagnose(FS.HISFC.Models.HealthRecord.DiagnoseBase Diagnose)
        {
            #region "�ӿ�˵��"
            //�ӿ����� RADT.Diagnose.UpdatePatientDiagnose.1
            // 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
            // 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
            // 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.UpdatePatientDiagnose.1", ref strSql) == -1) return -1;

            try
            {
                string[] s = new string[15];
                try
                {
                    s[0] = Diagnose.Patient.ID.ToString();// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[1] = Diagnose.HappenNo.ToString();//  --�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[2] = Diagnose.Patient.PID.CardNO;// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[3] = Diagnose.DiagType.ID.ToString();//  --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[4] = Diagnose.ID.ToString();// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[5] = Diagnose.Name;//.Replace("'","''");//--�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[6] = Diagnose.DiagDate.ToString();//  --���ʱ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[7] = Diagnose.Doctor.ID.ToString();//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[8] = Diagnose.Doctor.Name;//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[9] = (System.Convert.ToInt16(Diagnose.IsValid)).ToString();//    --�Ƿ���Ч
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[10] = Diagnose.Dept.ID.ToString();//  --��Ͽ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[11] = (System.Convert.ToInt16(Diagnose.IsMain)).ToString();//  --�Ƿ������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }

                try
                {
                    s[12] = Diagnose.Memo;//    --��ע
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[13] = this.Operator.ID.ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[14] = this.GetSysDateTime().ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                //				strSql=string.Format(strSql,s);
                return this.ExecNoQuery(strSql, s);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }
        #region ��������Ϸ������
        /// <summary>
        /// ��������Ϸ������
        /// </summary>
        /// <returns> ���������� ����ʱ����-1</returns> 
        public int GetNewDignoseNo()
        {
            int lNewNo = -1;
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.GetNewDiagnoseNo.1", ref strSql) == -1) return -1;
            if (strSql == null) return -1;
            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    lNewNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr(); ;
                return -1;
            }
            this.Reader.Close();
            return lNewNo;
        }
        #endregion
        #region �Ǽǻ��������Ϣ
        /// <summary>
        /// �Ǽ��µĻ������
        /// </summary>
        /// <param name="Diagnose"></param>
        /// <returns></returns> 
        public int CreatePatientDiagnose(FS.HISFC.Models.HealthRecord.DiagnoseBase Diagnose)
        {
            #region "�ӿ�˵��"
            //�ӿ����� RADT.Diagnose.CreatePatientDiagnose.1
            // 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
            // 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
            // 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.CreatePatientDiagnose.1", ref strSql) == -1) return -1;
            string[] s = new string[16];
            s[0] = Diagnose.Patient.ID.ToString();// --����סԺ��ˮ�� 
            s[1] = Diagnose.HappenNo.ToString();//  --������� 
            s[2] = Diagnose.Patient.PID.CardNO;// --���￨�� 
            s[3] = Diagnose.DiagType.ID.ToString();//  --������ 
            s[4] = Diagnose.ID.ToString();// --��ϱ��� 
            s[5] = Diagnose.Name;//.Replace("'","''") ;//--������� 
            s[6] = Diagnose.DiagDate.ToString();//  --���ʱ�� 
            s[7] = Diagnose.Doctor.ID.ToString();//    --���ҽ�� 
            s[8] = Diagnose.Doctor.Name;//    --���ҽ�� 
            s[9] = (System.Convert.ToInt16(Diagnose.IsValid)).ToString();//    --�Ƿ���Ч 
            s[10] = Diagnose.Dept.ID.ToString();//  --��Ͽ��� 
            s[11] = (System.Convert.ToInt16(Diagnose.IsMain)).ToString();//  --�Ƿ������ 
            s[12] = Diagnose.Memo;//    --��ע 
            s[13] = this.Operator.ID.ToString();//    --������ 
            s[14] = this.GetSysDateTime().ToString();//    --������ 
            s[15] = Diagnose.OperationNo;//������� 
            return this.ExecNoQuery(strSql, s);
        }
        #endregion
        /// <summary>
        /// ��ѯ�����������
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <returns></returns> 
        public ArrayList QueryOpsDiagnose(string InPatientNo)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.1
            //���룺סԺ��ˮ��
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.1", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.1�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo);
            return this.myPatientQuery(sql1);
        }
        /// <summary>
        /// ��ѯ���߸��������
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <param name="DiagType"></param>
        /// <returns></returns> 
        public ArrayList QueryOpsDiagnose(string InPatientNo, string DiagType)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.2
            //���룺סԺ��ˮ��
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.3", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.3�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo, DiagType);
            return this.myPatientQuery(sql1);
        }
        /// ��ѯ���������Ϣ��select��䣨��where������ 
        private string PatientQuerySelect()
        {
            #region �ӿ�˵��
            //RADT.Diagnose.DiagnoseQuery.select.1
            //���룺0
            //������sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetSql("RADT.Diagnose.DiagnoseQuery.select.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.DiagnoseQuery.select.1�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }
        //˽�к�������ѯ���߻�����Ϣ 
        private ArrayList myPatientQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.HealthRecord.DiagnoseBase Diagnose;
            this.ProgressBarText = "���ڲ�ѯ�������...";
            this.ProgressBarValue = 0;

            this.ExecQuery(SQLPatient);
            try
            {
                while (this.Reader.Read())
                {
                    Diagnose = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
                    Diagnose.Patient.ID = this.Reader[0].ToString();// סԺ��ˮ��

                    Diagnose.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());//  �������

                    Diagnose.Patient.PID.CardNO = this.Reader[2].ToString();//������

                    Diagnose.DiagType.ID = this.Reader[3].ToString();//������
                    //FS.HISFC.Models.HealthRecord.DiagnoseType diagnosetype = new FS.HISFC.Models.HealthRecord.DiagnoseType();
                    //diagnosetype.ID = Diagnose.DiagType.ID;
                    //Diagnose.DiagType.Name = diagnosetype.Name;//���������� zjy

                    Diagnose.ID = this.Reader[4].ToString();		//��ϴ���
                    Diagnose.ICD10.ID = this.Reader[4].ToString();
                    Diagnose.Name = this.Reader[5].ToString();		//�������
                    Diagnose.ICD10.Name = this.Reader[5].ToString();

                    Diagnose.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());

                    Diagnose.Doctor.ID = this.Reader[7].ToString();

                    Diagnose.Doctor.Name = this.Reader[8].ToString();

                    Diagnose.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9]);

                    Diagnose.Dept.ID = this.Reader[10].ToString();

                    Diagnose.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11]);

                    Diagnose.Memo = this.Reader[12].ToString();

                    Diagnose.User01 = this.Reader[13].ToString();
                    Diagnose.User02 = this.Reader[14].ToString();

                    //�������
                    Diagnose.OperationNo = this.Reader[15].ToString();

                    al.Add(Diagnose);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��û��������Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();

            this.ProgressBarValue = -1;
            return al;
        }

        public int DeleteDiagnoseByOperationNO(string operationNO)
        {
              string sql = "";
              if (this.Sql.GetSql("RADT.Diagnose.Diagnose.Delete.By.OperationNO", ref sql) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.Diagnose.Delete.By.OperationNO�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }

              sql = string.Format(sql, operationNO);
              return this.ExecNoQuery(sql);

            
        }
    }
}
