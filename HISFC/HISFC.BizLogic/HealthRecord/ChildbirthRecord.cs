using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord
{
    public class ChildbirthRecord : FS.FrameWork.Management.Database
    {

        #region ����
        /// <summary>
        /// ����סԺ�Ų�ѯ���߷����¼
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.ChildbirthRecord> QueryChildbirthRecord(string inpatientNO)
        {
            List<FS.HISFC.Models.HealthRecord.ChildbirthRecord> record = null;

            string strSql = "";

            if (this.Sql.GetSql("Case.ChildbirthRecord.Select", ref strSql) == -1)
            {
                return null;
            }

            try
            {
                strSql = string.Format(strSql, inpatientNO);

                this.ExecQuery(strSql);

                FS.HISFC.Models.HealthRecord.ChildbirthRecord info = null;

                record = new List<FS.HISFC.Models.HealthRecord.ChildbirthRecord>();

                while (this.Reader.Read())
                {
                       //is_normal,   --�Ƿ���������
                       //is_dystocia,   --�Ƿ��Ѳ�
                       //familyplanning,   --�ƻ�������ʽ
                       //is_perineumbreak,   --�Ƿ��������
                       //womenkind,   --��������
                       //isbreak,   --�Ƿ�����
                       //breaklevel,   --���ѳ̶�
                       //babysex,   --Ӥ���Ա�
                       //babyweight   --Ӥ������

                    info = new FS.HISFC.Models.HealthRecord.ChildbirthRecord();
                    info.Patient.ID = inpatientNO;
                    //�Ƿ���������
                    if (Reader[0] != DBNull.Value)
                    {
                        info.IsNormalChildbirth = FS.FrameWork.Function.NConvert.ToBoolean(Reader[0].ToString());
                    }
                    else
                    {
                        info.IsNormalChildbirth = false;
                    }
                    //�Ƿ��Ѳ�
                    if (Reader[1] != DBNull.Value)
                    {
                        info.IsDystocia = FS.FrameWork.Function.NConvert.ToBoolean(Reader[1].ToString());
                    }
                    else
                    {
                        info.IsDystocia = false;
                    }
                    //�ƻ�������ʽ
                    info.FamilyPlanning.ID = Reader[2].ToString();
                    //�Ƿ��������
                    if (Reader[3] != DBNull.Value)
                    {
                        info.IsPerineumBreak = FS.FrameWork.Function.NConvert.ToBoolean(Reader[3].ToString());
                    }
                    else
                    {
                        info.IsPerineumBreak = false;
                    }
                    //��������
                    info.WomenKind.ID = Reader[4].ToString();

                    //�Ƿ�����
                    if (Reader[5] != DBNull.Value)
                    {
                        info.IsBreak = FS.FrameWork.Function.NConvert.ToBoolean(Reader[5].ToString());
                    }
                    else
                    {
                        info.IsBreak = false;
                    }
                    //���ѳ̶�

                    info.BreakLevel.ID = Reader[6].ToString();
         
                    //Ӥ���Ա�
                    switch (Reader[7].ToString())
                    {
                        case "U":
                            info.BabySex = FS.HISFC.Models.Base.EnumSex.U;
                            break;
                        case "M":
                            info.BabySex = FS.HISFC.Models.Base.EnumSex.M;
                            break;
                        case "F":
                            info.BabySex = FS.HISFC.Models.Base.EnumSex.F;
                            break;
                        case "O":
                            info.BabySex = FS.HISFC.Models.Base.EnumSex.O;
                            break;
                        case "A":
                            info.BabySex = FS.HISFC.Models.Base.EnumSex.A;
                            break;
                    }
                    //Ӥ������
                    if (Reader[8] != DBNull.Value)
                    {
                        info.BabyWeight = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
                    }
                    else
                    {
                        info.BabyWeight = 0;
                    }
                    record.Add(info);
                    info = null;
                   
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                record = null;
            }
            return record;
        }

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="obj">�����¼ʵ��</param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.HealthRecord.ChildbirthRecord obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.ChildbirthRecord.Insert", ref strSql) == -1)
                return -1;
            try
            {
                obj.HappenNO = this.GetMaxHappenNO(obj.Patient.ID);
                obj.Oper.ID = this.Operator.ID;
                object[] mm = GetInfo(obj);
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
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="obj">�����¼ʵ��</param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.HealthRecord.ChildbirthRecord obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.ChildbirthRecord.Delete", ref strSql) == -1)
                return -1;
            try
            {
                //��ʽ���ַ���
                strSql = string.Format(strSql, obj.Patient.ID, obj.HappenNO);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ������ȫ����¼
        /// </summary>
        /// <param name="obj">������ˮ��</param>
        /// <returns></returns>
        public int DeleteAllByInpatientNO(string inpatientNO)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.ChildbirthRecord.Delete.All", ref strSql) == -1)
                return -1;
            try
            {
                //��ʽ���ַ���
                strSql = string.Format(strSql, inpatientNO);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="obj">�����¼ʵ��</param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.HealthRecord.ChildbirthRecord obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.ChildbirthRecord.Update", ref strSql) == -1)
                return -1;
            try
            {
                obj.Oper.ID = this.Operator.ID;
                object[] mm = GetInfo(obj);
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
        /// ��ȡ�������
        /// </summary>
        /// <param name="Inpatient"></param>
        /// <returns></returns>
        private int GetMaxHappenNO(string InpatientNO)
        {
            //�������
            int HappenNum = 0;
            string strSql = "";
            if (this.Sql.GetSql("Case.ChildbirthRecord.GetMaxHappenNum", ref strSql) == -1)
                return -1;
            strSql = string.Format(strSql, InpatientNO);
            string strReturn = this.ExecSqlReturnOne(strSql);
            HappenNum = FS.FrameWork.Function.NConvert.ToInt32(strReturn);
            return HappenNum;
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private object[] GetInfo(FS.HISFC.Models.HealthRecord.ChildbirthRecord info)
        {
            try
            {
                object[] s = new object[12];
                s[0] = info.Patient.ID;		//סԺ��ˮ��
                s[1] = info.HappenNO;//���  
                s[2] = FS.FrameWork.Function.NConvert.ToInt32(info.IsNormalChildbirth);	//�Ƿ���������
                s[3] = FS.FrameWork.Function.NConvert.ToInt32(info.IsDystocia);	 //�Ƿ��Ѳ�
                s[4] = info.FamilyPlanning.ID;  // �ƻ�������ʽ
                s[5] = FS.FrameWork.Function.NConvert.ToInt32(info.IsPerineumBreak); // �Ƿ��������
                s[6] = info.WomenKind.ID; //��������
                s[7] = FS.FrameWork.Function.NConvert.ToInt32(info.IsBreak);// �Ƿ����� 
                s[8] = info.BreakLevel.ID; //���ѳ̶�
                s[9] = info.BabySex.ToString();//С���Ա�
                s[10] = info.BabyWeight;//С������  
                s[11] = info.Oper.ID;
                return s;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
