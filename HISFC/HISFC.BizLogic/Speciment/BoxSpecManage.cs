using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FS.HISFC.Models.Speciment;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �걾�й�����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class BoxSpecManage : FS.FrameWork.Management.Database
    {        
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">�걾�й�����</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.BoxSpec spec)
        {
            string[] str = new string[]
						{
							spec.BoxSpecID.ToString(), 
							spec.BoxSpecName,
                            spec.Row.ToString(),
                            spec.Col.ToString(),
                            spec.Comment
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
            sequence = this.GetSequence("Speciment.BizLogic.BoxSpecManage.GetNextSequence");
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

        #region ���¼��ӹ�����
        /// <summary>
        /// ���±걾�й��
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateBoxSpec(string sqlIndex, params string[] args)
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
        /// ��ȡ�걾���ʵ����Ϣ
        /// </summary>
        /// <returns>�걾���ʵ��</returns>
        private BoxSpec SetSpecBox()
        {
            BoxSpec spec = new BoxSpec();
            try
            {
                spec.BoxSpecID = Convert.ToInt32(this.Reader[0].ToString());
                spec.Row = Convert.ToInt32(this.Reader["SPECROW"].ToString());
                spec.Col = Convert.ToInt32(this.Reader["SPECCOL"].ToString());
                spec.BoxSpecName = this.Reader["SPECNAME"].ToString();
                spec.Comment = this.Reader["MARK"].ToString();
                spec.Capacity = Convert.ToInt32(this.Reader["CAPACITY"].ToString());
                spec.OccupyCount = Convert.ToInt32(this.Reader["OCCUPYCOUNT"].ToString());
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return spec; 
        }

        /// <summary>
        /// ����������ѯ�걾�еĹ��
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        private ArrayList SelectSpec(string sqlIndex, string[] parms)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql,parms) == -1)
                return null;     
            ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                BoxSpec spec = SetSpecBox();
                specList.Add(spec); 
            }
            this.Reader.Close();
            return specList;
        }

        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// �걾�й�����
        /// </summary>
        /// <param name="specBox">��������ı걾�й��</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertBoxSpec(FS.HISFC.Models.Speciment.BoxSpec spec)
        {
            // return this.UpdateSingleTable("Fee.OutPatient.DayBalance.Insert", this.GetDayBalanceParams(dayBalance));
            return this.UpdateBoxSpec("Speciment.BizLogic.BoxSpecManage.Insert", this.GetParam(spec));
        
        }

        /// <summary>
        /// �������еı걾���
        /// </summary>
        /// <returns>�걾���List</returns>
        public Dictionary<int,string> GetAllBoxSpec()
        {
            ArrayList arrAllSpec = new ArrayList();
            arrAllSpec = this.SelectSpec("Speciment.BizLogic.BoxSpecManage.SelectAll", new string[] { });
            Dictionary<int, string> dicSpec = new Dictionary<int, string>();
            foreach (BoxSpec spec in arrAllSpec)
            {
                dicSpec.Add(spec.BoxSpecID, spec.BoxSpecName + " " + spec.Row.ToString() + "��" + spec.Col.ToString());
            }
            return dicSpec;
            
        }

        /// <summary>
        /// ���ݱ걾��ID��ȡ�걾�й��
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public BoxSpec GetSpecByBoxId(string boxId)
        {
            ArrayList arrSpec = new ArrayList();
            BoxSpec boxSpec = new BoxSpec();
            arrSpec = this.SelectSpec("Speciment.BizLogic.BoxSpecManage.SelectbySpecId", new string[] { boxId });
            foreach (BoxSpec spec in arrSpec)
            {
                boxSpec = spec;
                break;
            }
            return boxSpec;
        }

        /// <summary>
        /// ����ID��ȡ���ӹ��
        /// </summary>
        /// <param name="specID">�걾�еĹ��ID</param>
        /// <returns></returns>
        public BoxSpec GetSpecById(string specID)
        {
            ArrayList arrSpec = new ArrayList();
            BoxSpec boxSpec = new BoxSpec();
            arrSpec = this.SelectSpec("Speciment.BizLogic.BoxSpecManage.SelectbyId", new string[] { specID });
            foreach (BoxSpec spec in arrSpec)
            {
                boxSpec = spec;
                break;
            }
            return boxSpec;
        }

        #endregion
    }
}
