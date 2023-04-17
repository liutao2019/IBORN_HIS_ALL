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
    /// [��������: �걾������ϸ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-18' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class SpecApplyOutManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������

        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specOut">����걾ID</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecOut specOut)
        {
            //string sequence = "";

            //GetNextSequence(ref sequence);
            string[] str = new string[]
						{
							specOut.OutId.ToString(),
                            specOut.OutDate.ToString(),
                            specOut.OperId,
                            specOut.OperName,
                            specOut.SubSpecId.ToString(),
                            specOut.TypeId.ToString(),
                            specOut.SpecTypeId.ToString(),
                            specOut.Count.ToString(),
                            specOut.Unit,
                            specOut.RelateId.ToString(),
                            specOut.BoxBarCode,
                            specOut.BoxCol.ToString(),
                            specOut.BoxId.ToString(),
                            specOut.BoxRow.ToString(),
                            specOut.Comment,
                            specOut.Oper,
                            specOut.SubSpecBarCode,
                            specOut.SpecId.ToString(),
                            specOut.IsOut,
                            specOut.IsRetuanAble
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
            sequence = this.GetSequence("Speciment.BizLogic.SpecApplyOutManage.GetNextSequence");
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
        /// ���³�����Ϣ
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSpecOut(string sqlIndex, params string[] args)
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
        private SpecOut SetSubSpecOut()
        {
            SpecOut subSpecOut = new SpecOut();
            try
            {
                subSpecOut.OutId = Convert.ToInt32(this.Reader["APPLYID"].ToString());
                subSpecOut.OutDate = Convert.ToDateTime(this.Reader["APPLYDATE"].ToString());
                subSpecOut.OperId = this.Reader["OPERATORID"].ToString();
                subSpecOut.OperName = this.Reader["OPERATORNAME"].ToString();
                subSpecOut.SubSpecId = Convert.ToInt32(this.Reader["SUBSPECID"].ToString());
                subSpecOut.TypeId = Convert.ToInt32(this.Reader["TYPEID"].ToString());
                subSpecOut.SpecTypeId = Convert.ToInt32(this.Reader["SPECTYPEID"].ToString());
                subSpecOut.Count = Convert.ToDecimal(this.Reader["SPECCOUNT"].ToString());
                subSpecOut.RelateId = Convert.ToInt32(this.Reader["RELATEID"].ToString());
                subSpecOut.Unit = this.Reader["UNIT"].ToString();
                subSpecOut.BoxCol = Convert.ToInt32(this.Reader["BOXCOL"].ToString());
                subSpecOut.BoxRow = Convert.ToInt32(this.Reader["BOXROW"].ToString());
                subSpecOut.BoxId = Convert.ToInt32(this.Reader["BOXID"].ToString());
                subSpecOut.BoxBarCode = this.Reader["BOXBARCODE"].ToString();
                subSpecOut.Oper = this.Reader["OPER"].ToString();
                subSpecOut.Comment = this.Reader["MARK"].ToString();
                subSpecOut.SubSpecBarCode = this.Reader["SUBSPECBARCODE"].ToString();
                subSpecOut.SpecId = Convert.ToInt32(this.Reader["SPECID"].ToString());
                subSpecOut.IsOut = this.Reader["ISOUT"].ToString();
                subSpecOut.IsRetuanAble = this.Reader["ISRETURNABLE"].ToString();
                 
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return subSpecOut;
        }

        /// <summary>
        /// ����������ȡ���������ĳ���걾
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetSubSpecOut(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";
                return null;
            }
            if (this.ExecQuery(sql, args) == -1)
                return null;
            ArrayList arrSubSpecOut = new ArrayList();
            while (this.Reader.Read())
            {
                SpecOut outTmp = SetSubSpecOut();
                arrSubSpecOut.Add(outTmp);
            }
            return arrSubSpecOut;
        }

        #endregion

        #endregion

        #region ��������

        /// <summary>
        /// ����걾����
        /// </summary>
        /// <param name="SpecOut">�����걾</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertSubSpecApplyOut(FS.HISFC.Models.Speciment.SpecOut specOut)
        {
            return this.UpdateSpecOut("Speciment.BizLogic.SpecApplyOutManage.Insert", this.GetParam(specOut));

        }
        /// <summary>
        /// ֱ�Ӹ���sql�����¼�¼
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int UpdateSpecOut(string sql)
        {
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ����sql����ѯ����
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QuerySpecOut(string sql, ref DataSet ds)
        {
            return this.ExecQuery(sql, ref ds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specId"></param>
        /// <param name="applyNum"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Speciment.SpecOut GetBySpecId(string specId, string applyNum)
        {
            return this.GetSubSpecOut("Speciment.BizLogic.SpecApplyOutManage.GetApplyOutBySubspecIdAndRelateId", new string[] { specId, applyNum })[0] as SpecOut;
        }

        /// <summary>
        /// ���ݶ�Ӧ����ID����ɸѡ��������¼
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        public ArrayList GetSubSpecOut(string applyId)
        {
            return this.GetSubSpecOut("Speciment.BizLogic.SpecApplyOutManage.ByRELATEID", new string[] { applyId });
        }
        #endregion
    }

}
