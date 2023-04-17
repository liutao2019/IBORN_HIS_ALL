using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.HealthRecord
{
    public class Baby : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ��ѯĳ��סԺ���µ�Ӥ����Ϣ
        /// </summary>
        /// <param name="inpatientNo"> סԺ��</param>
        /// <returns> ���ط�����������Ϣ</returns>
        public ArrayList QueryBabyByInpatientNo(string inpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.CBaby.SelectInfo", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Baby info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.Baby();
                    info.InpatientNo = Reader[0].ToString();
                    info.HappenNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[1]);
                    info.SexCode = Reader[2].ToString();
                    info.BirthEnd = Reader[3].ToString();
                    if (Reader[4] != DBNull.Value)
                    {
                        info.Weight = Convert.ToSingle(Reader[4]);
                    }
                    else
                    {
                        info.Weight = 0;
                    }
                    info.BabyState = Reader[5].ToString();
                    info.Breath = Reader[6].ToString();
                    if (Reader[7] != DBNull.Value)
                    {
                        info.InfectNum = Convert.ToInt32(Reader[7].ToString());
                    }
                    info.Infect.ID = Reader[8].ToString();
                    info.Infect.Name = Reader[9].ToString();
                    if (Reader[10] != DBNull.Value)
                    {
                        info.SalvNum = Convert.ToInt32(Reader[10].ToString());
                    }
                    if (Reader[11] != DBNull.Value)
                    {
                        info.SuccNum = Convert.ToInt32(Reader[11].ToString());
                    }
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
        /// ��ѯĳ��סԺ���µ��������
        /// Creator: zhangjunyi@FS.com
        /// </summary>
        /// <param name="Inpatient"></param>
        /// <returns></returns>
        public int GetMaxHappenNum(string Inpatient)
        {
            //�������
            int HappenNum = 0;
            string strSql = "";
            if (this.Sql.GetSql("Case.CBaby.GetMaxHappenNum", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, Inpatient);
            string strReturn = this.ExecSqlReturnOne(strSql);
            //ȡ�������
            HappenNum = FS.FrameWork.Function.NConvert.ToInt32(strReturn);
            return HappenNum;
        }
        /// <summary>
        ///����ĳ����¼
        ///zhangjunyi@FS.com
        /// </summary>
        /// <param name="info"></param>
        /// <returns>ʧ�ܷ��� ��1 �ɹ����� 1</returns>
        public int Update(FS.HISFC.Models.HealthRecord.Baby info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.CBaby.Update", ref strSql) == -1) return -1;
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
        ///����ĳ����¼
        ///zhangjunyi@FS.com
        /// </summary>
        /// <param name="info"></param>
        /// <returns>ʧ�ܷ��� ��1 �ɹ����� 1</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Baby info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.CBaby.Insert", ref strSql) == -1) return -1;
            try
            {
                info.HappenNum = GetMaxHappenNum(info.InpatientNo);
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
        private object[] GetInfo(FS.HISFC.Models.HealthRecord.Baby info)
        {
            try
            {
                object[] s = new object[14];
                s[0] = info.InpatientNo;		//סԺ��ˮ��
                s[1] = info.HappenNum;//Ӥ�����  
                s[2] = info.SexCode;			//�Ա�   M ���� FŮ�� 
                s[3] = info.BirthEnd;	 //�ѳ���� 0 ��� 1 ���� 2  ��̥    
                s[4] = info.Weight;  //  ����  
                s[5] = info.BabyState; // ת��  0 ����  1 ת�� 2 ��Ժ   
                s[6] = info.Breath; //����  0 ��Ȼ 1 I����Ϣ 2 II����Ϣ 
                s[7] = info.InfectNum;// ��Ⱦ����  
                s[8] = info.Infect.ID; //��Ⱦ����
                s[9] = info.Infect.Name;//��Ⱦ����
                s[10] = info.SalvNum;// ���ȴ���  
                s[11] = info.SuccNum;// �ɹ�����   
                s[12] = info.BirthMod;//������ʽ 
                s[13] = info.OperInfo.ID;//��¼��
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
        /// <returns>�ɹ����� 1 ʧ�ܷ��� ��1 </returns>
        public int Delete(FS.HISFC.Models.HealthRecord.Baby info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.CBaby.Delete", ref strSql) == -1) return -1;
            try
            {
                //��ʽ���ַ���
                strSql = string.Format(strSql, info.InpatientNo, info.HappenNum);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #region �µ��Ӳ���ʹ��ģ��ʵ����ҳ���
        /// <summary>
        /// ��ѯĳ��סԺ���µ�Ӥ����Ϣ
        /// </summary>
        /// <param name="inpatientNo"> סԺ��</param>
        /// <returns> ���ط�����������Ϣ</returns>
        public ArrayList QueryBabyFromEmrViewByInpatientNo(string inpatientNo)
        {
            ArrayList List = null;
            string strSql = @"select 
INPATIENT_NO , -- סԺ��ˮ�� 
HAPPEN_NO,  --Ӥ����� 
SEX_CODE, --�Ա�
BIRTH_END , --�ѳ����
WEIGHT , --����
BABY_STATE , --ת��
BREATH , --����
INFECT_NUM ,-- ��Ⱦ����
INFECT_CODE, --��Ⱦ����
INFECT_NAME, --��Ⱦ����
SALV_TIMES , --���ȴ��� 
SUCC_TIMES, --�ɹ����� 
BIRTH_MODE, -- ������ʽ 
OPER_CODE , --��¼��  
OPER_DATE    --��¼ʱ��   
from view_met_cas_baby  t
where t.inpatient_no = '{0}'";
            
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Baby info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.Baby();
                    info.InpatientNo = Reader[0].ToString();
                    info.HappenNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[1]);
                    info.SexCode = Reader[2].ToString();
                    info.BirthEnd = Reader[3].ToString();
                    if (Reader[4] != DBNull.Value)
                    {
                        info.Weight = Convert.ToSingle(Reader[4]);
                    }
                    else
                    {
                        info.Weight = 0;
                    }
                    info.BabyState = Reader[5].ToString();
                    info.Breath = Reader[6].ToString();
                    if (Reader[7] != DBNull.Value)
                    {
                        info.InfectNum = Convert.ToInt32(Reader[7].ToString());
                    }
                    info.Infect.ID = Reader[8].ToString();
                    info.Infect.Name = Reader[9].ToString();
                    if (Reader[10] != DBNull.Value)
                    {
                        info.SalvNum = Convert.ToInt32(Reader[10].ToString());
                    }
                    if (Reader[11] != DBNull.Value)
                    {
                        info.SuccNum = Convert.ToInt32(Reader[11].ToString());
                    }
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
        #region ����
        /// <summary>
        /// Ӥ��ת��
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� ���� ZG ��ʾ", true)]
        public ArrayList GetBabyState()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "ת��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "��Ժ";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� ���� BREATHSTATE ��ʾ", true)]
        public ArrayList GetBreath()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "��Ȼ";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "I�ȸ�Ⱦ";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "II�ȸ�Ⱦ";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� ���� CHILDBEARINGRESULT ��ʾ", true)]
        public ArrayList GetBirthEnd()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "���";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "��̥";
            //list.Add(info);
            return list;

        }
        /// <summary>
        /// �Ա�
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� ö�ٱ�ʾ" ,true)]
        public ArrayList GetSex()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "Ů";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ��ѯĳ��סԺ���µ�Ӥ����Ϣ
        /// </summary>
        /// <param name="inpatientNo"> סԺ��</param>
        /// <returns> ���ط�����������Ϣ</returns>
        [Obsolete("���� ��QueryBabyByInpatientNo����",true)]
        public ArrayList SelectInfo(string inpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.CBaby.SelectInfo", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                //��ѯ
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Baby info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.Baby();
                    info.InpatientNo = Reader[0].ToString();
                    info.HappenNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[1]);
                    info.SexCode = Reader[2].ToString();
                    info.BirthEnd = Reader[3].ToString();
                    if (Reader[4] != DBNull.Value)
                    {
                        info.Weight = Convert.ToSingle(Reader[4]);
                    }
                    else
                    {
                        info.Weight = 0;
                    }
                    info.BabyState = Reader[5].ToString();
                    info.Breath = Reader[6].ToString();
                    if (Reader[7] != DBNull.Value)
                    {
                        info.InfectNum = Convert.ToInt32(Reader[7].ToString());
                    }
                    info.Infect.ID = Reader[8].ToString();
                    info.Infect.Name = Reader[9].ToString();
                    if (Reader[10] != DBNull.Value)
                    {
                        info.SalvNum = Convert.ToInt32(Reader[10].ToString());
                    }
                    if (Reader[11] != DBNull.Value)
                    {
                        info.SuccNum = Convert.ToInt32(Reader[11].ToString());
                    }
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

    }
}
