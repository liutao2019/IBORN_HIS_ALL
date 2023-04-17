using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.HealthRecord;
using FS.FrameWork.Function;
using System.Data;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord.Case
{
    /// <summary>
    /// Visit<br></br>
    /// [��������: ���������ά��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-08-21]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class CaseCabinet : FS.FrameWork.Management.Database
    {
        #region ���ݿ��������

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="caseCabinet">�������¼��</param>
        /// <returns>�����쳣���أ�1 �ɹ�����1 ����ʧ�ܷ��� 0</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Case.CaseCabinet caseCabinet)
        {

            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseCabinet.Insert", ref strSql) == -1) return -1;
            try
            {
                //����
                strSql = string.Format(strSql, GetInfo(caseCabinet));
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }      

        }

        private string[] GetInfo(FS.HISFC.Models.HealthRecord.Case.CaseCabinet caseCabinet)
        {
            string[] str = new string[7];
            str[0] = caseCabinet.Store.ID; //��������� 
            str[1] = caseCabinet.ID;       //���������
            str[2] = caseCabinet.GridCount.ToString(); //����
            if (caseCabinet.IsValid)
            {
                str[3] = "1";
            }
            else
            {
                str[3] = "0";
            }
            str[4] = caseCabinet.Memo;  //��ע
            str[5] = caseCabinet.OperEnv.ID; //���һ�β���Ա����
            str[6] = caseCabinet.OperEnv.OperTime.ToString(); //���һ�β���ʱ��
           
            return str;
        }

        /// <summary>
        /// ���²������¼
        /// </summary>
        /// <param name="caseCabinet">�������¼��</param>
        /// <returns>Ӱ�������-�ɹ�;-1-����ʧ��,0-�쳣</returns>
        public int Update(FS.HISFC.Models.HealthRecord.Case.CaseCabinet caseCabinet)
        {
            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseCabinet.Update", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = GetInfo(caseCabinet);
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                 return -1;
            }

            //��ִ��SQL��䷵��
            return this.ExecNoQuery(strSql);

        }
        /// <summary>
       
        #endregion

        #region ��ѯ

        /// <summary>
        ///�����ݲ���������ѯ���������Ϣ
        /// </summary>
        /// <param name="cabinetCode,cabinetCode">���������</param>
        /// <returns>��Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Case.CaseCabinet</returns>

        public ArrayList Query(string storeCode, string cabinetCode)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseCabinet.Select", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, storeCode, cabinetCode);
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Case.CaseCabinet cabinet = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    cabinet = new FS.HISFC.Models.HealthRecord.Case.CaseCabinet();
                    cabinet.Store.ID = this.Reader[0].ToString();        //���������
                    cabinet.ID = this.Reader[1].ToString();        //���������
                    cabinet.GridCount = FS.FrameWork.Function.NConvert.ToInt32(Reader[2].ToString()); // ���������
                    //Cabinet.IsValid = this.Reader[3].ToString().Equals("0") ? false : true;   //�Ƿ���Ч��1���ǡ�0����
                    cabinet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[3].ToString());//�Ƿ���Ч��1���ǡ�0����
                    cabinet.Memo = this.Reader[4].ToString();      //��ע
                    cabinet.OperEnv.ID = this.Reader[5].ToString();//���һ�β���Ա����                  
                    cabinet.OperEnv.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());  //���һ�β���ʱ��
                    List.Add(cabinet);
                    cabinet = null;
                }
                return List;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
   
        }

        /// <summary>
        ///�����ݲ���������ѯ���������Ϣ
        /// </summary>
        /// <param name="cabinetCode,cabinetCode">���������</param>
        /// <returns>��Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Case.CaseCabinet</returns>
        public ArrayList QueryAll()
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseCabinet.SelectAll", ref strSql) == -1)
                return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql);
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Case.CaseCabinet cabinet = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    cabinet = new FS.HISFC.Models.HealthRecord.Case.CaseCabinet();
                    cabinet.Store.ID = this.Reader[0].ToString();        //���������
                    cabinet.ID = this.Reader[1].ToString();        //���������
                    cabinet.GridCount = FS.FrameWork.Function.NConvert.ToInt32(Reader[2].ToString()); // ���������
                    cabinet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[3].ToString());//�Ƿ���Ч��1���ǡ�0����
                    cabinet.Memo = this.Reader[4].ToString();      //��ע
                    cabinet.OperEnv.ID = this.Reader[5].ToString();//���һ�β���Ա����
                    cabinet.OperEnv.Name = this.Reader[6].ToString();
                    cabinet.OperEnv.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());  //���һ�β���ʱ��
                    List.Add(cabinet);
                }
                return List;
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
