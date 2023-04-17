using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
namespace FS.HISFC.BizLogic.HealthRecord
{
    public class Fee : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ����סԺ��ˮ�� �ӷ�����ϸ���� ��ѯ������Ϣ
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryFeeInfoState(string InpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.GetFeeInfoState", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Patient info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Patient();
                    info.DIST = this.Reader[0].ToString(); //ͳ�ƴ������
                    info.AreaCode = this.Reader[1].ToString(); //ͳ������ 
                    info.IDCard = this.Reader[2].ToString(); //ͳ�Ʒ���
                    List.Add(info);
                    info = null;
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
        ///�Ӳ�����Ϣ�����ѯ��Ϣ 
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryCaseFeeState(string InpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.GetCaseFeeState", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Patient info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Patient();
                    info.DIST = this.Reader[2].ToString(); //ͳ�ƴ������
                    info.AreaCode = this.Reader[3].ToString(); //ͳ������ 
                    info.IDCard = this.Reader[4].ToString(); //ͳ�Ʒ���
                    List.Add(info);
                    info = null;
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
        /// ����һ������
        /// </summary>
        /// <param name="info"> ��������˻���ʵ�����洢������Ϣ</param>
        /// <returns></returns>
        public int InsertFeeInfo(FS.HISFC.Models.RADT.Patient info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.InsertFeeInfo", ref strSql) == -1) return -1;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, GetInfo(info));
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ����һ������ �������ʧ�� ��ִ�в������
        /// </summary>
        /// <param name="info">��������˻���ʵ�����洢������Ϣ</param>
        /// <returns></returns>
        public int UpdateFeeInfo(FS.HISFC.Models.RADT.Patient info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.UpdateFeeInfo", ref strSql) == -1) return -1;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, GetInfo(info));
                int i = this.ExecNoQuery(strSql);
                if (i < 1) //����ʧ�� 
                {
                    return InsertFeeInfo(info); //ִ�в������
                }
                else
                {
                    return i;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ��ȡʵ���������
        /// </summary>
        /// <param name="info">��������˻���ʵ�����洢������Ϣ</param>
        /// <returns></returns>
        private string[] GetInfo(FS.HISFC.Models.RADT.Patient info)
        {
            string[] str = new string[8];
            str[0] = info.ID; //סԺ��ˮ�� 
            str[2] = info.DIST; //ͳ�ƴ���
            str[3] = info.AreaCode; //ͳ������
            str[4] = info.IDCard; //ͳ�Ʒ���
            str[5] = info.User01; //��Ժ����
            str[7] = this.Operator.ID; //����Ա
            return str;
        }
        #region drgs����ҳ����ͳ��

        public int QueryFeeForDrgsByInpatientNO(string inpatientNO, ref DataSet ds)
        {
            try
            {
                string strSql = string.Empty;
                //if (this.Sql.GetSql("Case.BaseDML.GetFeeForDrgsByInpatientNO", ref strSql) == -1) return -1;
                if (this.Sql.GetSql("Case.BaseDML.GetFeeForDrgsByInpatientNO1", ref strSql) == -1) return -1;
                try
                {
                    //��ѯ
                    strSql = string.Format(strSql, inpatientNO);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    return -1;
                }
                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }


        }
        #endregion
        #region ����
        /// <summary>
        ///�Ӳ�����Ϣ�����ѯ��Ϣ 
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        [Obsolete("���� ,��QueryCaseFeeState ����")]
        public ArrayList GetCaseFeeState(string InpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.GetCaseFeeState", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Patient info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Patient();
                    info.DIST = this.Reader[4].ToString(); //ͳ�ƴ������
                    info.AreaCode = this.Reader[5].ToString(); //ͳ������ 
                    info.IDCard = this.Reader[6].ToString(); //ͳ�Ʒ���
                    List.Add(info);
                    info = null;
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
        /// ����סԺ��ˮ�� �ӷ�����ϸ���� ��ѯ������Ϣ
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        [Obsolete("����,�� QueryFeeInfoState ����")]
        public ArrayList GetFeeInfoState(string InpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.BaseDML.GetFeeInfoState", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.RADT.Patient info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Patient();
                    info.DIST = this.Reader[0].ToString(); //ͳ�ƴ������
                    info.AreaCode = this.Reader[1].ToString(); //ͳ������ 
                    info.IDCard = this.Reader[2].ToString(); //ͳ�Ʒ���
                    List.Add(info);
                    info = null;
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
