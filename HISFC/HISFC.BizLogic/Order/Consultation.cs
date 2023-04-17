using System;
using FS.HISFC.Models;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// ���������<br></br>
    /// <Font color='#FF1111'>[����סԺ���߻�����Ϣ]</Font><br></br>
    /// [�� �� ��: ]<br>wolf</br>
    /// [����ʱ��: ]<br>2004-11</br>
    /// <�޸ļ�¼ 
    ///		�޸���='����' 
    ///		�޸�ʱ��='2007-8-24' 
    ///		�޸�Ŀ��='�ܹ��������ݿ�Ŀ���ҽ��������'
    ///		�޸�����='�ڲ���͸��·����������һ������(�ܷ���ҽ��)'
    ///		/>
    /// </summary>
    public class Consultation : FS.FrameWork.Management.Database
    {
        public Consultation()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultation"></param>
        /// <returns></returns>
        [Obsolete("��InsertConsultation������", true)]
        public int Insert(FS.HISFC.Models.Order.Consultation consultation)
        {
            return this.InsertConsultation(consultation);
        }
        /// <summary>
        /// ������˱��
        /// </summary>
        /// <param name="consultationNO"></param>
        /// <returns></returns>
        [Obsolete("��UpdateConsultationAuditingFlag������", true)]
        public int Update(string consultationNO)
        {
            return this.UpdateConsultationAuditingFlag(consultationNO);
        }
        #endregion

        #region ��ɾ��
        /// <summary>
        /// ��������¼
        /// </summary>
        /// <param name="consultation"></param>
        /// <returns></returns>
        public int InsertConsultation(FS.HISFC.Models.Order.Consultation consultation)
        {
            #region "�ӿ�"
            //			--סԺ��ˮ��
            //            ,   --סԺ������
            //            ,   --סԺ���Ҵ���
            //            ,   --����վ����
            //            ,   --ҽ��ҽʦ����
            //            ,   --ҽ��ҽʦ����
            //            ,   --��д��������
            //            ,   --ԤԼ��������
            //            ,   --�������ͣ�0����/1ҽ��,2ҽԺ
            //            ,   --�Ӽ�����,1��/0��
            //            ,   --�������
            //            ,   --����ҽʦ
            //            ,   --����˵��
            //            ,   --������ʼ��
            //            ,   --����������
            //            ,   --ʵ�ʻ�����
            //            ,   --������
            //            ,   --ȷ��ҽ������
            //            ,   --����״̬,1����/2ȷ��
            //            ,   --�û�����
            //            ,   --ҽԺ����
            //            ,   --����˵��,1,2,3,��ע����λ
            #endregion
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Consultation.InsertItem.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            //try
            //{
            //    strSql = string.Format(strSql,consultation.InpatientNo ,consultation.PatientNo ,consultation.Dept.ID,
            //        consultation.NurseStation.ID,consultation.Doctor.ID,consultation.Doctor.Name,consultation.ApplyTime .ToString(),
            //        consultation.PreConsultationTime.ToString(),consultation.Type.GetHashCode().ToString(),
            //        consultation.IsEmergency.GetHashCode().ToString(),consultation.DeptConsultation.ID,
            //        consultation.DoctorConsultation.ID,consultation.Name,consultation.BeginTime.ToString(),
            //        consultation.EndTime.ToString(),consultation.ConsultationTime.ToString(),consultation.Result,
            //        consultation.DoctorConfirm.ID,consultation.State.ToString(),this.Operator.ID,
            //        consultation.HosConsultation.Name,//ҽԺ����
            //        consultation.EmergencyMemo,//����˵��
            //        consultation.User01,
            //        consultation.User02,
            //        consultation.User03,
            //        consultation.Memo,//���߱�ע
            //        consultation.BedNO);//���ߴ�λ
            //}
            //catch(Exception ex)
            //{
            //    this.Err=ex.Message;
            //    this.ErrCode=ex.Message;
            //    this.WriteErr();
            //    return -1;
            //}

            return this.ExecNoQuery(strSql,
                    consultation.InpatientNo,
                     consultation.PatientNo,
                    consultation.Dept.ID,
                    consultation.NurseStation.ID,
                    consultation.Doctor.ID,
                    consultation.Doctor.Name,
                    consultation.ApplyTime.ToString(),
                    consultation.PreConsultationTime.ToString(),
                    consultation.Type.GetHashCode().ToString(),
                    consultation.IsEmergency.GetHashCode().ToString(),
                    consultation.DeptConsultation.ID,
                    consultation.DoctorConsultation.ID,
                    consultation.Name,
                    consultation.BeginTime.ToString(),
                    consultation.EndTime.ToString(),
                    consultation.ConsultationTime.ToString(),
                    consultation.Result,
                    consultation.DoctorConfirm.ID,
                    consultation.State.ToString(),
                    this.Operator.ID,
                    consultation.HosConsultation.Name,//ҽԺ����
                    consultation.EmergencyMemo,//����˵��
                    consultation.User01,
                    consultation.User02,
                    consultation.User03,
                    consultation.Memo,//���߱�ע
                    consultation.BedNO,
                    consultation.IsCreateOrder.GetHashCode().ToString()//�ܷ���ҽ��
                    );
        }
        /// <summary>
        /// ���»����¼
        /// </summary>
        /// <param name="consultation"></param>
        /// <returns></returns>
        public int UpdateConsultation(FS.HISFC.Models.Order.Consultation consultation)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Consultation.UpdateItem.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            return this.ExecNoQuery(strSql, consultation.ID, consultation.InpatientNo, consultation.PatientNo, consultation.Dept.ID,
                    consultation.NurseStation.ID, consultation.Doctor.ID, consultation.Doctor.Name, consultation.ApplyTime.ToString(),
                    consultation.ConsultationTime.ToString(), consultation.Type.GetHashCode().ToString(),
                    consultation.IsEmergency.GetHashCode().ToString(), consultation.DeptConsultation.ID,
                    consultation.DoctorConsultation.ID, consultation.Name, consultation.BeginTime.ToString(),
                    consultation.EndTime.ToString(), consultation.ConsultationTime.ToString(), consultation.Result,
                    consultation.DoctorConfirm.ID, consultation.State.ToString(), this.Operator.ID,
                    consultation.HosConsultation.Name,//ҽԺ����
                    consultation.EmergencyMemo,//����˵��
                    consultation.User01,
                    consultation.User02,
                    consultation.User03,
                    consultation.Memo,//���߱�ע
                    consultation.BedNO,
                    consultation.IsCreateOrder.GetHashCode().ToString()//�ܷ���ҽ��
                    );
        }

        /// <summary>
        /// ������˱��
        /// </summary>
        /// <returns></returns>
        public int UpdateConsultationAuditingFlag(string consultationNO)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Consultation.UpdateAuditFlag", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, consultationNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ���»����¼�ͻ������
        /// </summary>
        /// <param name="consultation"></param>
        /// <returns></returns>
        public int UpdateConsulationRecord(FS.HISFC.Models.Order.Consultation consultation)
        {

            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Consultation.UpdateCnsl", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.Consultation.UpdateCnsl�ֶ�";
                return -1;
            }
            //try
            //{
            //    strSql = string.Format(strSql,consultation.ID,consultation.Record,consultation.Suggestion);
            //}
            //catch (Exception ex)
            //{
            //    this.Err = ex.Message;
            //    this.WriteErr();
            //    return -1;
            //}
            return this.ExecNoQuery(strSql, consultation.ID, consultation.Record, consultation.Suggestion);
        }
        /// <summary>
        /// ɾ�������¼
        /// </summary>
        /// <param name="ConsultationNo"></param>
        /// <returns></returns>
        public int DeleteConsulation(string ConsultationNo)
        {
            string strSql = "";
            #region "�ӿ�"
            //���룺0 �������� 1�÷����� 2 ����Ա 3 ����ʱ��
            //������0
            #endregion

            if (this.Sql.GetCommonSql("Order.Consultation.DeleteItem.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, ConsultationNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion

        #region ˽��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList myGetList(string sql)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(sql) == -1) return null;
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.Consultation obj = new FS.HISFC.Models.Order.Consultation();
                try
                {
                    obj.ID = this.Reader[0].ToString();
                    obj.InpatientNo = this.Reader[1].ToString();
                    obj.PatientNo = this.Reader[2].ToString();
                    obj.Dept.ID = this.Reader[3].ToString();
                    obj.NurseStation.ID = this.Reader[4].ToString();
                    obj.Doctor.ID = this.Reader[5].ToString();
                    obj.Doctor.Name = this.Reader[6].ToString();
                    obj.ApplyTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
                    obj.PreConsultationTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());
                    obj.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    obj.DeptConsultation.ID = this.Reader[10].ToString();
                    obj.DoctorConsultation.ID = this.Reader[11].ToString();
                    obj.Name = this.Reader[12].ToString();
                    obj.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[13].ToString());
                    obj.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
                    obj.ConsultationTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
                    obj.Result = this.Reader[16].ToString();
                    obj.DoctorConfirm.ID = this.Reader[17].ToString();
                    obj.State = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[18].ToString());
                    obj.Doctor.User01 = this.Reader[19].ToString();
                    obj.Doctor.User02 = this.Reader[20].ToString();
                    obj.HosConsultation.Name = this.Reader[21].ToString();//ҽԺ����
                    obj.EmergencyMemo = this.Reader[22].ToString();//����˵��
                    obj.User01 = this.Reader[23].ToString();
                    obj.User02 = this.Reader[24].ToString();
                    obj.User03 = this.Reader[25].ToString();
                    obj.Memo = this.Reader[26].ToString();//���߱�ע
                    obj.BedNO = this.Reader[27].ToString();//���ߴ�λ
                    obj.IsCreateOrder = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[28].ToString());//�ɷ���ҽ��
                }
                catch { this.WriteErr(); return null; }
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region ��ѯ
        /// <summary>
        /// ��û����б�
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryConsulation(string InpatientNo)
        {
            string strSql = "";
            //Order.Consultation.SelectItem.1
            //���룺0  InpatientNo
            //����:����
            if (this.Sql.GetCommonSql("Order.Consultation.Select.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            try
            {
                strSql = string.Format(strSql, InpatientNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return null;
            }

            return this.myGetList(strSql);
        }

        /// <summary>
        /// ���ݻ�����ˮ�Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="consulationNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Consultation GetConsultation(string consulationNo)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Consultation.GetSingleCnsl", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.Consultation.GetSingleCnsl�ֶΣ�";
                return null;
            }
            strSql = string.Format(strSql, consulationNo);
            ArrayList al = this.myGetList(strSql);
            if (al.Count > 0) return al[0] as FS.HISFC.Models.Order.Consultation;
            return null;
        }
        /// <summary>
        /// ���Ժ������б�
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="iState"></param>
        /// <returns></returns>
        public ArrayList QueryOutHosConsultaion(DateTime dt1, DateTime dt2, int iState)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            //Order.Consultation.SelectItem.1
            //���룺0  InpatientNo
            //����:����
            if (this.Sql.GetCommonSql("Order.Consultation.Select.2", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = string.Format(strSql, iState.ToString(), dt1.ToString(), dt2.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return null;
            }

            return this.myGetList(strSql);
        }
        /// <summary>
        /// ��ѯ���ﵥ
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="iState"></param>
        /// <returns></returns>
        public System.Data.DataSet QueryOuthosConsultation(DateTime dt1, DateTime dt2, int iState)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Consultation.Select.3", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                strSql = string.Format(strSql, iState.ToString(), dt1.ToString(), dt2.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(strSql, ref ds) == -1)
            {
                return null;
            }
            return ds;
        }
        #endregion
    }
}
