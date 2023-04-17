using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �û��������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-12-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class UserApplyManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="userApply">�û���������</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.UserApply userApply)
        {          
            string[] str = new string[]
						{
							userApply.UserAppId.ToString(), 
							userApply.UserId,
                            userApply.ApplyId.ToString(),
                            userApply.Schedule.ToString(),
                            userApply.CurDate.ToString(),
                            userApply.OperId.ToString(),
                            userApply.OperName.ToString(),
                            userApply.ScheduleId.ToString()
						};
            return str;
        }

        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.UserApplyManage.GetNextSequence");
            //
            // �������NULL�����ȡʧ��
            //
            if (sequence == null)
            {
                this.SetError("", "��ȡSequenceʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }

        #region ���ô�����Ϣ
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="errorCode">������뷢������</param>
        /// <param name="errorText">������Ϣ</param>
        private void SetError(string errorCode, string errorText)
        {
            this.ErrCode = errorCode;
            this.Err = errorText + "[" + this.Err + "]"; // + "��ShelfSpecManage.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion
        #endregion

        #region���û������¼
        /// <summary>
        /// �����û������¼
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateUserApply(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }

        #endregion

        #endregion

        #region�����з���
        /// <summary>
        /// �����û������¼
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        public int UpdateUserApply(string sql)
        {
            try
            {
                return this.ExecNoQuery(sql);
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public int InsertUserApply(UserApply userApply)
        {
            return this.UpdateUserApply("Speciment.BizLogic.UserApplyManage.Insert", GetParam(userApply));
        }

        /// <summary>
        /// ��������ID�ͽ���������Ҽ�¼
        /// </summary>
        /// <param name="appID">����ID</param>
        /// <param name="schedule">�������</param>
        /// <returns></returns>
        public int QueryUserApply(string appID, string schedule)
        {
            return this.UpdateUserApply("Speciment.BizLogic.UserApplyManage.Query", new string[] { appID, schedule });
        }
        #endregion 

    }
}
