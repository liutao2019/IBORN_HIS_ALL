using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: ԭ�걾��������������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-01-11]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class GenerateBarCodeManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
       
        private string[] GetParam(SourceCode code)
        {
            string[] str = new string[] 
                        { 
                            code.BarCodeID.ToString(),
                            code.BarCode,
                            code.InPatientNo
                        };
            return str;
        }

        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        private int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.DisTypeManage.GetNextSequence");
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

        #region ���²���
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateBarCode(string sqlIndex, params string[] args)
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

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>����ʵ��</returns>
        private SourceCode SetSourceCode()
        {
            SourceCode code = new SourceCode();
            try
            {
                code.BarCodeID = Convert.ToInt32(this.Reader[0].ToString());
                code.BarCode = this.Reader[1].ToString();
                code.InPatientNo = this.Reader[2].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return code;
        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList GetSourceCode(string sqlIndex, params string[] parm)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            sql = string.Format(sql, parm);
            if (this.ExecQuery(sql) == -1)
                return null;
            ArrayList arrCode = new ArrayList();

            while (this.Reader.Read())
            {
                SourceCode c = SetSourceCode();
                arrCode.Add(c);
            }
            this.Reader.Close();
            return arrCode;
        }

        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="disType">��������Ĳ���ʵ��</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertBarCode(FS.HISFC.Models.Speciment.SourceCode code)
        {
            return this.UpdateBarCode("Speciment.BizLogic.BarCodeManage.SouceBarCode", this.GetParam(code));
        }
        #endregion
    }
}
