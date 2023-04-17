using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord
{
    public class Tumour : FS.FrameWork.Management.Database
    {
        #region  ��������
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Tumour GetTumour(string inpatientNo)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.GetTumour", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Tumour info = new FS.HISFC.Models.HealthRecord.Tumour();
                while (this.Reader.Read())
                {
                    info.InpatientNo = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();//סԺ��ˮ�� 
                    info.Rmodeid = this.Reader[1] == DBNull.Value ? string.Empty : this.Reader[1].ToString();//���Ʒ�ʽ
                    info.Rprocessid = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//���Ƴ�ʽ
                    info.Rdeviceid = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//����װ��
                    info.Cmodeid = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();//���Ʒ�ʽ
                    info.Cmethod = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();//���Ʒ���
                    info.Gy1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6] == DBNull.Value ? "0" : Reader[6].ToString());		//ԭ����gy����
                    info.Time1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7] == DBNull.Value ? "0" : Reader[7].ToString());		//ԭ�������
                    info.Day1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8] == DBNull.Value ? "0" : Reader[8].ToString());		//ԭ��������
                    info.BeginDate1 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9] == DBNull.Value ? "0001-01-01" : Reader[9].ToString());//ԭ���ʼʱ��
                    info.EndDate1 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10] == DBNull.Value ? "0001-01-01" : Reader[10].ToString());  //ԭ�������ʱ��
                    info.Gy2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11] == DBNull.Value ? "0" : Reader[11].ToString());		//�����ܰͽ�gy����
                    info.Time2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12] == DBNull.Value ? "0" : Reader[12].ToString());		//�����ܰͽ����
                    info.Day2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13] == DBNull.Value ? "0" : Reader[13].ToString());		//�����ܰͽ�����
                    info.BeginDate2 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14]==DBNull.Value?"0001-01-01": Reader[14].ToString());//�����ܰͽῪʼʱ��
                    info.EndDate2 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15] == DBNull.Value ? "0001-01-01" : Reader[15].ToString());  //�����ܰͽ����ʱ��
                    info.Gy3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16] == DBNull.Value ? "0" : Reader[16].ToString());		//ת����gy����
                    info.Time3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17] == DBNull.Value ? "0" : Reader[17].ToString());		//�����ܰͽ����
                    info.Day3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18] == DBNull.Value ? "0" : Reader[18].ToString());		//�����ܰͽ�����
                    info.BeginDate3 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[19] == DBNull.Value ? "0001-01-01" : Reader[19].ToString());//�����ܰͽῪʼʱ��
                    info.EndDate3 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20] == DBNull.Value ? "0001-01-01" : Reader[20].ToString());  //�����ܰͽ����ʱ��
                    info.OperInfo.ID =this.Reader[21]==DBNull.Value?string.Empty:this.Reader[21].ToString();								 //����Ա 
                    info.OperInfo.OperTime =FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22]==DBNull.Value?"0001-01-01": Reader[22].ToString());//����ʱ�� 
                    info.Position = this.Reader[23] == DBNull.Value ? string.Empty : this.Reader[23].ToString();//ת����λ��
                    info.Tumour_Type = this.Reader[24] == DBNull.Value ? string.Empty : this.Reader[24].ToString();//������������ P���� C�ٴ�
                    info.Tumour_T = this.Reader[25] == DBNull.Value ? string.Empty : this.Reader[25].ToString();//ԭ������ Tumor T
                    info.Tumour_N = this.Reader[26] == DBNull.Value ? string.Empty : this.Reader[26].ToString();//�ܰ�ת�� Node N
                    info.Tumour_M = this.Reader[27] == DBNull.Value ? string.Empty : this.Reader[27].ToString();//Զ��ת�� Metastasis  M
                    info.Tumour_Stage = this.Reader[28] == DBNull.Value ? string.Empty : this.Reader[28].ToString();//����
          
                }
                return info;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateTumour(FS.HISFC.Models.HealthRecord.Tumour info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.UpdateTumour", ref strSql) == -1) return -1;
            try
            {
                object[] mm = GetTumourInfo(info);
                if (mm == null)
                {
                    this.Err = "ҵ����ʵ���л�ȡ�ַ��������";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        private string[] GetTumourInfo(FS.HISFC.Models.HealthRecord.Tumour info)
        {
            string[] ss = new string[28];
            ss[0] = info.InpatientNo;	//סԺ��ˮ�� 
            ss[1] = info.Rmodeid;		//���Ʒ�ʽ
            ss[2] = info.Rprocessid;	//���Ƴ�ʽ
            ss[3] = info.Rdeviceid;		//����װ��
            ss[4] = info.Cmodeid;		//���Ʒ���
            ss[5] = info.Cmethod;		//���Ʒ���
            ss[6] = info.Gy1.ToString();			//ԭ����gy����
            ss[7] = info.Time1.ToString();			//ԭ�������
            ss[8] = info.Day1.ToString();			//ԭ��������
            ss[9] = info.BeginDate1.ToString();	//ԭ���ʼʱ��
            ss[10] = info.EndDate1.ToString();		//ԭ�������ʱ��
            ss[11] = info.Gy2.ToString();			//�����ܰͽ�gy����
            ss[12] = info.Time2.ToString();		//�����ܰͽ����
            ss[13] = info.Day2.ToString();			//�����ܰͽ�����
            ss[14] = info.BeginDate2.ToString();	//�����ܰͽῪʼʱ��
            ss[15] = info.EndDate2.ToString();		//�����ܰͽ����ʱ��
            ss[16] = info.Gy3.ToString();			//ת����gy����
            ss[17] = info.Time3.ToString();		//�����ܰͽ����
            ss[18] = info.Day3.ToString();		//�����ܰͽ�����
            ss[19] = info.BeginDate3.ToString();	//�����ܰͽῪʼʱ��
            ss[20] = info.EndDate3.ToString();		//�����ܰͽ����ʱ��
            ss[21] = this.Operator.ID;	//����Ա 
            ss[22] = info.Position;//ת����λ��
            ss[23] = info.Tumour_Type;//������������
            ss[24] = info.Tumour_T;//ԭ������ Tumor T
            ss[25] = info.Tumour_N;//�ܰ�ת�� Node N
            ss[26] = info.Tumour_M;//Զ��ת�� Metastasis  M
            ss[27] = info.Tumour_Stage;//����     
            return ss;
        }
        /// <summary>
        /// ��������ϸ���в���һ������
        /// </summary>
        /// <param name="info"></param>
        /// <returns>�����쳣���أ�1 �ɹ�����1 ����ʧ�ܷ��� 0</returns>
        public int InsertTumour(FS.HISFC.Models.HealthRecord.Tumour info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.InsertTumour", ref strSql) == -1) return -1;
            try
            {
                //��ȡ����ֵ
                object[] mm = GetTumourInfo(info);
                if (mm == null)
                {
                    this.Err = "ҵ����ʵ���л�ȡ�ַ��������";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��������ϸ����ɾ��һ������
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns>�����쳣���أ�1 �ɹ�����1 ����ʧ�ܷ��� 0</returns>
        public int DeleteTumour(string InpatientNo)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.DeleteTumour", ref strSql) == -1) return -1;
            try
            {
                //��ȡ����ֵ
                strSql = string.Format(strSql, InpatientNo);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion

        #region ������ϸ��
        /// <summary>
        /// ��ȡ������ϸ���е�����
        /// </summary>
        /// <param name="inpatienNo">סԺ��ˮ��</param>
        /// <returns>������null</returns>
        public ArrayList QueryTumourDetail(string inpatienNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.GetTumourDetail", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.TumourDetail info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.TumourDetail();
                    info.InpatientNO = Reader[0].ToString();
                    info.HappenNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                    info.CureDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[2].ToString());
                    info.DrugInfo.ID = Reader[3].ToString();
                    info.DrugInfo.Name = Reader[4].ToString();
                    info.Qty = FS.FrameWork.Function.NConvert.ToInt32(Reader[5].ToString());
                    info.Unit = Reader[6].ToString();
                    info.Period = Reader[7].ToString();
                    info.Result = Reader[8].ToString();
                    info.OperInfo.ID = Reader[9].ToString();
                    info.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }
        /// <summary>
        /// ����������ϸ���е�����
        /// </summary>
        /// <param name="info"></param>
        /// <returns>�����쳣���أ�1 �ɹ�����1����ʧ�ܷ��� 0 </returns>
        public int UpdateTumourDetail(FS.HISFC.Models.HealthRecord.TumourDetail info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.UpdateTumourDetail", ref strSql) == -1) return -1;
            try
            {
                info.OperInfo.ID = this.Operator.ID;
                object[] mm = GetInfo(info);
                if (mm == null)
                {
                    this.Err = "ҵ����ʵ���л�ȡ�ַ��������";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��������ϸ���в���һ������
        /// </summary>
        /// <param name="info"></param>
        /// <returns>�����쳣���أ�1 �ɹ�����1 ����ʧ�ܷ��� 0</returns>
        public int InsertTumourDetail(FS.HISFC.Models.HealthRecord.TumourDetail info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.InsertTumourDetail", ref strSql) == -1) return -1;
            try
            {
                info.OperInfo.ID = this.Operator.ID;
                //��ȡ����ֵ
                info.HappenNO = FS.FrameWork.Function.NConvert.ToInt32(this.GetSequence("Case.Tumour.GetSequence"));
                object[] mm = GetInfo(info);
                if (mm == null)
                {
                    this.Err = "ҵ����ʵ���л�ȡ�ַ��������";
                    return -1;
                }
                strSql = string.Format(strSql, mm);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        private object[] GetInfo(FS.HISFC.Models.HealthRecord.TumourDetail info)
        {
            try
            {
                object[] s = new object[11];
                s[0] = info.InpatientNO;		//סԺ��ˮ��
                s[1] = info.HappenNO; //�������      
                s[2] = info.CureDate.ToString(); //��������
                s[3] = info.DrugInfo.ID;//ҩ�����       
                s[4] = info.DrugInfo.Name;//ҩ������         
                s[5] = info.Qty.ToString();//����   
                s[6] = info.Unit;//��λ 
                s[7] = info.Period;//�Ƴ�
                s[8] = info.Result;// ��Ч		
                s[9] = info.OperInfo.ID;//  ����Ա����
                s[10] = info.OperInfo.OperTime.ToString();
                return s;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteTumourDetail(FS.HISFC.Models.HealthRecord.TumourDetail info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.DeleteTumourDetail", ref strSql) == -1) return -1;
            try
            {
                //��ʽ���ַ���
                strSql = string.Format(strSql, info.InpatientNO, info.HappenNO);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion
        #region �µ��Ӳ�����ȡ����
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Tumour GetTumourEmrView(string inpatientNo)
        {
            string strSql = @"
SELECT inpatient_no,   --סԺ��ˮ��
       rmodeid,   --���Ʒ�ʽ
       rprocessid,   --���Ƴ�ʽ
       rdeviceid,   --����װ��
       cmodeid,   --���Ʒ�ʽ
       cmethod,   --���Ʒ���
       gy1,   --ԭ����gy����
       time1,   --ԭ�������
       day1,   --ԭ��������
       begin_date1,   --ԭ���ʼʱ��
       end_date1,   --ԭ�������ʱ��
       gy2,   --�����ܰͽ�gy����
       time2,   --�����ܰͽ����
       day2,   --�����ܰͽ�����
       begin_date2,   --�����ܰͽῪʼʱ��
       end_date2,   --�����ܰͽ����ʱ��
       gy3,   --ת����gy����
       time3,   --ת�������
       day3,   --ת��������
       begin_date3,   --ת���ʼʱ��
       end_date3,   --ת�������ʱ��
       oper_code,   --����Ա����
       oper_date,    --����ʱ��
       POSITION,--ת����λ��
       tumour_type,--������������
       tumour_t,--ԭ������ Tumor T
       tumour_n,--�ܰ�ת�� Node N
       tumour_m,--Զ��ת�� Metastasis  M
       tumour_stage--����
  FROM view_met_cas_tumour   --�����������Ƽ�¼��
 WHERE inpatient_no= '{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Tumour info = new FS.HISFC.Models.HealthRecord.Tumour();
                while (this.Reader.Read())
                {
                    info.InpatientNo = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();//סԺ��ˮ�� 
                    info.Rmodeid = this.Reader[1] == DBNull.Value ? string.Empty : this.Reader[1].ToString();//���Ʒ�ʽ
                    info.Rprocessid = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//���Ƴ�ʽ
                    info.Rdeviceid = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//����װ��
                    info.Cmodeid = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();//���Ʒ�ʽ
                    info.Cmethod = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();//���Ʒ���
                    info.Gy1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6] == DBNull.Value ? "0" : Reader[6].ToString());		//ԭ����gy����
                    info.Time1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7] == DBNull.Value ? "0" : Reader[7].ToString());		//ԭ�������
                    info.Day1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8] == DBNull.Value ? "0" : Reader[8].ToString());		//ԭ��������
                    info.BeginDate1 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9] == DBNull.Value ? "0001-01-01" : Reader[9].ToString());//ԭ���ʼʱ��
                    info.EndDate1 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10] == DBNull.Value ? "0001-01-01" : Reader[10].ToString());  //ԭ�������ʱ��
                    info.Gy2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11] == DBNull.Value ? "0" : Reader[11].ToString());		//�����ܰͽ�gy����
                    info.Time2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12] == DBNull.Value ? "0" : Reader[12].ToString());		//�����ܰͽ����
                    info.Day2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13] == DBNull.Value ? "0" : Reader[13].ToString());		//�����ܰͽ�����
                    info.BeginDate2 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14] == DBNull.Value ? "0001-01-01" : Reader[14].ToString());//�����ܰͽῪʼʱ��
                    info.EndDate2 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15] == DBNull.Value ? "0001-01-01" : Reader[15].ToString());  //�����ܰͽ����ʱ��
                    info.Gy3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16] == DBNull.Value ? "0" : Reader[16].ToString());		//ת����gy����
                    info.Time3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17] == DBNull.Value ? "0" : Reader[17].ToString());		//�����ܰͽ����
                    info.Day3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18] == DBNull.Value ? "0" : Reader[18].ToString());		//�����ܰͽ�����
                    info.BeginDate3 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[19] == DBNull.Value ? "0001-01-01" : Reader[19].ToString());//�����ܰͽῪʼʱ��
                    info.EndDate3 = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20] == DBNull.Value ? "0001-01-01" : Reader[20].ToString());  //�����ܰͽ����ʱ��
                    info.OperInfo.ID = this.Reader[21] == DBNull.Value ? string.Empty : this.Reader[21].ToString();								 //����Ա 
                    info.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22] == DBNull.Value ? "0001-01-01" : Reader[22].ToString());//����ʱ�� 
                    info.Position = this.Reader[23] == DBNull.Value ? string.Empty : this.Reader[23].ToString();//ת����λ��
                    info.Tumour_Type = this.Reader[24] == DBNull.Value ? string.Empty : this.Reader[24].ToString();//������������ P���� C�ٴ�
                    info.Tumour_T = this.Reader[25] == DBNull.Value ? string.Empty : this.Reader[25].ToString();//ԭ������ Tumor T
                    info.Tumour_N = this.Reader[26] == DBNull.Value ? string.Empty : this.Reader[26].ToString();//�ܰ�ת�� Node N
                    info.Tumour_M = this.Reader[27] == DBNull.Value ? string.Empty : this.Reader[27].ToString();//Զ��ת�� Metastasis  M
                    info.Tumour_Stage = this.Reader[28] == DBNull.Value ? string.Empty : this.Reader[28].ToString();//����

                }
                return info;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ��ȡ������ϸ���е�����
        /// </summary>
        /// <param name="inpatienNo">סԺ��ˮ��</param>
        /// <returns>������null</returns>
        public ArrayList QueryTumourDetailEmrView(string inpatienNo)
        {
            ArrayList List = null;
            string strSql = @"
SELECT 
       inpatient_no,   --סԺ��ˮ��
       happen_no,   --�������
       cure_date,   --��������
       drug_code,   --ҩ�����
       drug_name,   --ҩ������
       qty,   --����
       unit,   --��λ
       period,   --�Ƴ�
       result,   --��Ч
       oper_code,   --����Ա����
       oper_date    --����ʱ��
  FROM view_met_cas_tumourdetail   --��������������ҩ��ϸ��
 WHERE  inpatient_no = '{0}' 
 order by happen_no ";
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.TumourDetail info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.TumourDetail();
                    info.InpatientNO = Reader[0].ToString();
                    info.HappenNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                    info.CureDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[2].ToString());
                    info.DrugInfo.ID = Reader[3].ToString();
                    info.DrugInfo.Name = Reader[4].ToString();
                    info.Qty = FS.FrameWork.Function.NConvert.ToInt32(Reader[5].ToString());
                    info.Unit = Reader[6].ToString();
                    info.Period = Reader[7].ToString();
                    info.Result = Reader[8].ToString();
                    info.OperInfo.ID = Reader[9].ToString();
                    info.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }
        #endregion 
        #region  ����
        /// <summary>
        /// ��ȡ������ϸ���е�����
        /// </summary>
        /// <param name="inpatienNo">סԺ��ˮ��</param>
        /// <returns>������null</returns>
        [Obsolete("����,�� QueryTumourDetail ����", true)]
        public ArrayList GetTumourDetail(string inpatienNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.Tumour.GetTumourDetail", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.TumourDetail info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.TumourDetail();
                    info.InpatientNO = Reader[0].ToString();
                    info.HappenNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                    info.CureDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[2].ToString());
                    info.DrugInfo.ID = Reader[3].ToString();
                    info.DrugInfo.Name = Reader[4].ToString();
                    info.Qty = FS.FrameWork.Function.NConvert.ToInt32(Reader[5].ToString());
                    info.Unit = Reader[6].ToString();
                    info.Period = Reader[7].ToString();
                    info.Result = Reader[8].ToString();
                    info.OperInfo.ID = Reader[9].ToString();
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }
        /// <summary>
        /// �Ƴ��б�
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ����PERIODOFTREATMENT����", true)]
        public ArrayList GetPeriodList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "�Ƴ�I";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "�Ƴ�II";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "�Ƴ�III";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ��ȡ����б�
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ����RADIATERESULT ����", true)]
        public ArrayList GetResultList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "CR";
            //info.Name = "��ʧ";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "PR";
            //info.Name = "��Ч";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "MR";
            //info.Name = "��ת";
            //list.Add(info);

            //info.ID = "S";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "P";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "NA";
            //info.Name = "δ��";

            //list.Add(info);
            return list;
        }
        /// <summary>
        /// ���Ʒ�ʽ 
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ���� RADIATETYPE ����", true)]
        public ArrayList GetRmodeidList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "������";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "��Ϣ��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "������";
            //list.Add(info);
            return list;
        }
        /// <summary>
        /// ���Ƴ�ʽ 
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ����RADIATEPERIOD ����", true)]
        public ArrayList GetRprocessidList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "���";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "�ֶ�";
            //list.Add(info);
            return list;
        }
        /// <summary>
        /// ����װ��
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ���� RADIATEDEVICE ����", true)]
        public ArrayList GetRdeviceidList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "ֱ��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "X��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "4";
            //info.Name = "��װ";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ���� CHEMOTHERAPY ����", true)]
        public ArrayList GetCmodeidList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "������";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "��Ϣ��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "�¸�����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "4";
            //info.Name = "������";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "5";
            //info.Name = "��ҩ����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "6";
            //info.Name = "����";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ���Ʒ���
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ���� CHEMOTHERAPYWAY ����", true)]
        public ArrayList GetCmethodList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "ȫ��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "�뻯";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "A���";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "4";
            //info.Name = "��ǻע";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "5";
            //info.Name = "��ǻע";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "6";
            //info.Name = "����";
            //list.Add(info);

            return list;
        }
        #endregion 
    }
}
