using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.SOC.HISFC.BizProcess.DCP
{
    /// <summary>
    /// [��������: Ȩ�޹���]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2009-05]<br></br>
    /// <˵��>
    ///     1��HIS4.5 ϵͳ�ع� ��ԭHISFC.Manager��Ų����
    /// </˵��>
    /// </summary>
    public class Permission : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ������Ա���룬����Ȩ�ޱ���ȡ��Աӵ��Ȩ�޵Ĳ���
        /// </summary>
        /// <param name="userCode">����Ա����</param>
        /// <param name="class2Code">����Ȩ����</param>
        /// <returns>�ɹ����ؾ���Ȩ�޵Ŀ��Ҽ��� ʧ�ܷ���null</returns>        
        public List<FS.FrameWork.Models.NeuObject> QueryUserPriv(string userCode, string class2Code)
        {
            //ȡSQL���
            string sqlstring = PrepareSQL("Manager.UserPowerDetailManager.LoadPrivDept", userCode, class2Code);
            if (sqlstring == null)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            //ȡ����
            List<FS.FrameWork.Models.NeuObject> al = new List<NeuObject>();
            try
            {
                FS.FrameWork.Models.NeuObject info;
                this.ExecQuery(sqlstring);
                while (this.Reader.Read())
                {
                    info = new NeuObject();
                    info.ID = this.Reader[0].ToString();  //���ұ���
                    info.Name = this.Reader[1].ToString();  //��������
                    info.User01 = this.Reader[2].ToString();  //����Ȩ�ޱ���
                    info.User02 = this.Reader[3].ToString();  //����Ȩ������
                    info.User03 = this.Reader[4].ToString();  //����Ȩ�������ǣ�1�жϴ���Ȩ��ʱ��ֻҪ����Ȩ�޾�������룬����Ҫ�û�ѡ�����
                    info.Memo = this.Reader[5].ToString();  //��������
                    al.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private string PrepareSQL(string sqlName, params string[] values)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql(sqlName, ref  strSql) == -1)
            {
                this.Err = "�Ҳ���sql���:" + sqlName;
                return null;
            }
            try
            {
                if (values != null)
                    strSql = string.Format(strSql, values);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                strSql = null;
            }
            return strSql;
        }
    }
}
