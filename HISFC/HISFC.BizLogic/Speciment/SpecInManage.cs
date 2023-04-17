using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �걾������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-12-01]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-19' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class SpecInManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������

        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specIn">���걾ID</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecIn specIn)
        {
            //string sequence = "";

            //GetNextSequence(ref sequence);
            string[] str = new string[]
						{
							specIn.InId.ToString(),
                            specIn.InTime.ToString(),
                            specIn.OperId,
                            specIn.OperName,
                            specIn.SubSpecId.ToString(),
                            specIn.TypeId.ToString(),
                            specIn.SpecTypeId.ToString(),
                            specIn.Count.ToString(),
                            specIn.Unit,
                            specIn.RelateId.ToString(),                        
                            specIn.BoxId.ToString(),
                            specIn.Row.ToString(),
                            specIn.Col.ToString(),
                            specIn.Comment,
                            specIn.BoxBarCode,
                            specIn.Oper,
                            specIn.SubSpecBarCode
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
            sequence = this.GetSequence("Speciment.BizLogic.SpecInManage.GetNextSequence");
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

        #region ���±걾��֯���Ͳ���
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSpecIn(string sqlIndex, params string[] args)
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
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <returns>���ʵ��</returns>
        private SpecIn SetSubSpecIn()
        {
            SpecIn subSpecIn = new SpecIn();
            try
            {
                subSpecIn.InId = Convert.ToInt32(this.Reader["INID"].ToString());
                subSpecIn.InTime = Convert.ToDateTime(this.Reader["INDATE"].ToString());
                subSpecIn.OperId = this.Reader["OPERATORID"].ToString();
                subSpecIn.OperName = this.Reader["OPERATORNAME"].ToString();
                subSpecIn.SubSpecId = Convert.ToInt32(this.Reader["SUBSPECID"].ToString());
                subSpecIn.TypeId = Convert.ToInt32(this.Reader["TYPEID"].ToString());
                subSpecIn.SpecTypeId = Convert.ToInt32(this.Reader["SPECTYPEID"].ToString());
                subSpecIn.Count = Convert.ToDecimal(this.Reader["SPECCOUNT"].ToString());
                subSpecIn.RelateId = Convert.ToInt32(this.Reader["RELATEID"].ToString());
                subSpecIn.Unit = this.Reader["UNIT"].ToString();
                subSpecIn.Col = Convert.ToInt32(this.Reader["BOXCOL"].ToString());
                subSpecIn.Row = Convert.ToInt32(this.Reader["BOXROW"].ToString());
                subSpecIn.BoxId = Convert.ToInt32(this.Reader["BOXID"].ToString());
                subSpecIn.BoxBarCode = this.Reader["BOXBARCODE"].ToString();
                subSpecIn.Oper = this.Reader["OPER"].ToString();
                subSpecIn.Comment = this.Reader["MARK"].ToString();
                subSpecIn.SubSpecBarCode = this.Reader["SUBSPECBARCODE"].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return subSpecIn;
        }

        /// <summary>
        /// ����������ȡ�������������걾
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetSubSpecIn(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";
                return null;
            }
            if (this.ExecQuery(sql, args) == -1)
                return null;
            ArrayList arrSubSpecIn = new ArrayList();
            while (this.Reader.Read())
            {
                SpecIn inTmp = SetSubSpecIn();
                arrSubSpecIn.Add(inTmp);
            }
            Reader.Close();
            return arrSubSpecIn;
        }

        #endregion

        #endregion

        #region ��������

        /// <summary>
        /// ���걾����
        /// </summary>
        /// <param name="specIn">�����걾</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertSubSpecIn(FS.HISFC.Models.Speciment.SpecIn specIn)
        {
            return this.UpdateSpecIn("Speciment.BizLogic.SpecInManage.Insert", this.GetParam(specIn));

        }

        /// <summary>
        /// ֱ�Ӹ���sql�����¼�¼
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int UpdateSpecIn(string sql)
        {
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ����sql����ѯ����
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QuerySpecIn(string sql, ref DataSet ds)
        {
            return this.ExecQuery(sql, ref ds);
        }

        /// <summary>
        /// ��ѯһ���걾�е��е����걾�б�
        /// </summary>
        /// <param name="boxId">�걾��ID</param>
        /// <returns>�걾�б�</returns>
        public ArrayList GetInSpecInBox(string boxId)
        {
            return this.GetSubSpecIn("Speciment.BizLogic.SpecInManage.InOneBox", new string[] { boxId });
        }

        public int DeleteById(string inId)
        {
            return this.UpdateSpecIn("Speciment.BizLogic.SpecInManage.DeleteById", new string[] { inId });
        }

        #endregion
    }
}
