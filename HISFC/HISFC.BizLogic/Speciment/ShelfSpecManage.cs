using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �걾�ܹ�����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-18' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class ShelfSpecManage : FS.FrameWork.Management.Database
    {

        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">���ӹ�����</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.ShelfSpec shelfSpec)
        {
            string[] str = new string[]
						{
							shelfSpec.ShelfSpecID.ToString(), 
							shelfSpec.Row.ToString(),
                            shelfSpec.Col.ToString(),
                            shelfSpec.Height.ToString(),
                            shelfSpec.ShelfSpecName,
                            shelfSpec.BoxSpec.BoxSpecID.ToString(),
                            shelfSpec.Comment
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
            sequence = this.GetSequence("Speciment.BizLogic.ShelfSpecManage.GetNextSequence");
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

        /// <summary>
        /// ��ȡ���ӹ��ʵ����Ϣ
        /// </summary>
        /// <returns>���ӹ��ʵ��</returns>
        private ShelfSpec SetShelf()
        {
            ShelfSpec spec = new ShelfSpec();
            try
            {
                spec.ShelfSpecID = Convert.ToInt32(this.Reader[0].ToString());
                spec.Row = Convert.ToInt32(this.Reader[1].ToString());
                spec.Col = Convert.ToInt32(this.Reader[2].ToString());
                spec.Height = Convert.ToInt32(this.Reader[3].ToString());
                spec.ShelfSpecName = this.Reader[4].ToString();
                if (null == this.Reader[5].ToString())
                {
                    spec.BoxSpec.BoxSpecID = 0;
                }
                else
                {
                    spec.BoxSpec.BoxSpecID = Convert.ToInt32(this.Reader[5].ToString());
                }
                //if (null == this.Reader["MARK"]) spec.Comment = "";
                //else
                //    spec.Comment = this.Reader["MARK"].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return spec; 
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
        /// ���¼��ӹ��
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateShelfSpec(string sqlIndex, params string[] args)
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

        /// <summary>
        /// ����ID��ȡ���ӵĹ��
        /// </summary>
        /// <param name="id">���Id���߼���Id</param>
        /// <returns></returns>
        private ShelfSpec GetShelfByID(string sqlIndex, string id)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            sql = string.Format(sql, id);
            if (this.ExecQuery(sql) == -1)
                return null;
            ShelfSpec spec = new ShelfSpec();
            while (this.Reader.Read())
            {
                spec = SetShelf();
            }
            this.Reader.Close();
            return spec;
        }

        #endregion

        #region ��������
        /// <summary>
        /// ���ӹ�����
        /// </summary>
        /// <param name="specBox">��������ļ��ӹ��</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertShelfSpec(FS.HISFC.Models.Speciment.ShelfSpec shelfSpec)
        {
            return this.UpdateShelfSpec("Speciment.BizLogic.ShelfSpecManage.Insert", this.GetParam(shelfSpec));
        }

        /// <summary>
        /// �������еļ��ӹ��
        /// </summary>
        /// <returns>�걾����List</returns>
        public Dictionary<int, string> GetAllShelfSpec()
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.ShelfSpecManage.SelectAll", ref sql) == -1)
                return null;
            if (this.ExecQuery(sql) == -1)
                return null;
            Dictionary<int, string> dicSpec = new Dictionary<int, string>();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                ShelfSpec spec = SetShelf();
                dicSpec.Add(spec.ShelfSpecID, spec.ShelfSpecName + "  ���" + spec.Row.ToString() + "��" + spec.Height.ToString());
                //specList.Add(spec); 
            }
            this.Reader.Close();
            return dicSpec;

        }

        /// <summary>
        /// ����ID��ȡ���ӵĹ��
        /// </summary>
        /// <param name="specID"></param>
        /// <returns></returns>
        public ShelfSpec GetShelfByID(string specID)
        {
            return this.GetShelfByID("Speciment.BizLogic.ShelfSpecManage.SelectByID", specID);
        }

        /// <summary>
        /// ���ݹ��Id��ȡ���ӹ��
        /// </summary>
        /// <param name="shelfId"></param>
        /// <returns></returns>
        public ShelfSpec GetShelfByShelf(string shelfId)
        {
            return this.GetShelfByID("Speciment.BizLogic.ShelfSpecManage.ByShelfID", shelfId);
        }
        #endregion
    
    }
}
