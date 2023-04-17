using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-12]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class IceBoxManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">����ʵ�����</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.IceBox iceBox, string oper)
        {
            string sequence = "";
            switch (oper)
            {
                case "Insert":
                    GetNextSequence(ref sequence);
                    break;
                case "Update":
                    sequence = iceBox.IceBoxId.ToString();
                    break;
                default:
                    break;
            }

            string[] str = new string[]
						{
							sequence, 							
                            iceBox.LayerNum.ToString(),
                            iceBox.IsOccupy.ToString(),
                            iceBox.IceBoxTypeId.ToString(),
                            iceBox.IceBoxName,
                            iceBox.SpecTypeId,
                            iceBox.OrgOrBlood,
                            iceBox.UseStaus,
                            iceBox.Comment
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
            sequence = this.GetSequence("Speciment.BizLogic.IceBoxManage.GetNextSequence");
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
        /// ��ȡ����ID��Ϣ
        /// </summary>
        /// <returns>�����ID</returns>
        private IceBox SetIcebox()
        {
            IceBox iceBox = new IceBox();
            try
            {
                //iceBox.IceBoxId = Convert.ToInt32(this.Reader[0].ToString());
                iceBox.IceBoxId = Convert.ToInt32(this.Reader[0].ToString());
                //iceBox.LayerNum = Convert.ToInt32(this.Reader[1].ToString());
                //iceBox.IsOccupy = Convert.ToInt16(this.Reader[2].ToString());
                //iceBox.IceBoxTypeId = this.Reader[3].ToString();
                iceBox.IceBoxName = this.Reader[4].ToString();

            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return iceBox;
        }

        /// <summary>
        /// ��ȡIceBox������ʵ����Ϣ
        /// </summary>
        /// <returns>����ʵ��</returns>
        private IceBox SetAllIceBox()
        {
            IceBox iceBox = new IceBox();
            try
            {
                iceBox.IceBoxId = Convert.ToInt32(this.Reader[0].ToString());
                iceBox.LayerNum = Convert.ToInt32(this.Reader[1].ToString());
                iceBox.IsOccupy = Convert.ToInt16(this.Reader[2].ToString());
                iceBox.IceBoxTypeId = this.Reader[3].ToString();
                iceBox.IceBoxName = this.Reader[4].ToString();
                iceBox.SpecTypeId = this.Reader[5].ToString();
                iceBox.OrgOrBlood = this.Reader[6].ToString();
                iceBox.UseStaus = this.Reader[7].ToString();
                if (!Reader.IsDBNull(8)) iceBox.Comment = this.Reader[8].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return iceBox;
        }

        /// <summary>
        /// ����sql����ȡ��Icebox
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList SelectIceBox(string sqlIndex, string[] parm)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            IceBox iceBox;
            ArrayList arrIceBox = new ArrayList();
            while (this.Reader.Read())
            {
                iceBox = SetAllIceBox();
                arrIceBox.Add(iceBox);
            }
            this.Reader.Close();
            return arrIceBox;
        }

        /// <summary>
        /// �쿴ָ���ı����Ƿ��б걾
        /// </summary>
        /// <param name="iceBoxId"></param>
        /// <returns></returns>
        public int CheckIceBoxHaveSpecBox(string iceBoxId)
        {
            string[] parm = new string[] { iceBoxId };
            IceBox ib = new IceBox();
            ArrayList arr = new ArrayList();

            arr = SelectIceBox("Speciment.BizLogic.IceBoxManage.IceBoxHaveSpecBox1", parm);
            if (arr.Count > 0 && arr != null)
                return 1;

            arr = new ArrayList();
            arr = SelectIceBox("Speciment.BizLogic.IceBoxManage.IceBoxHaveSpecBox2", parm);
            if (arr.Count > 0 && arr != null)
                return 1;

            return -1;

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

        #region ���±������
        /// <summary>
        /// ���±���
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateIceBox(string sqlIndex, params string[] args)
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

        #region ��������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="specBox">��������ı���</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertIceBox(FS.HISFC.Models.Speciment.IceBox iceBox)
        {
            try
            {
                return this.UpdateIceBox("Speciment.BizLogic.IceBoxManage.Insert", this.GetParam(iceBox, "Insert"));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���±���
        /// </summary>
        /// <param name="iceBox"></param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int UpdateIceBox(IceBox iceBox)
        {
            try
            {
                return this.UpdateIceBox("Speciment.BizLogic.IceBoxManage.Update", this.GetParam(iceBox, "Update"));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }

        }

        /// <summary>
        ///�������ֻ�ȡ�����ID��
        /// </summary>
        /// <param name="iceBoxName"></param>
        /// <returns></returns>
        public IceBox GetIceBoxByName(string iceBoxName)
        {
            string[] parm = new string[] { iceBoxName };
            ArrayList arr = SelectIceBox("Speciment.BizLogic.IceBoxManage.SelectByName", parm);
            if (arr.Count > 0)
                return arr[0] as IceBox;
            else
                return null;
        }
        /// <summary>
        /// ���ݲ��ID�Ų������ڵı���
        /// </summary>
        /// <param name="layerID">��ID</param>
        /// <returns></returns>
        public IceBox GetIceBoxByLayerID(string layerID)
        {
            string[] parm = new string[] { layerID };
            ArrayList arr = SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByLayer", parm);
            if (arr.Count > 0)
                return arr[0] as IceBox;
            return null;
        }

        /// <summary>
        /// ��ѯ�õ����б������Ϣ
        /// </summary>
        /// <returns>����List</returns>
        public ArrayList GetAllIceBox()
        {
            return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.SelectAllIceBox", new string[] { });
            //string sql = "";
            //if (this.Sql.GetSql("Speciment.BizLogic.IceBoxManage.SelectAllIceBox", ref sql) == -1)
            //    return null;

            //if (this.ExecQuery(sql) == -1)
            //    return null;
            //ArrayList arrIceBox = new ArrayList();
            //while (this.Reader.Read())
            //{
            //    arrIceBox.Add(SetAllIceBox());
            //}
            //this.Reader.Close();
            //return arrIceBox;
        }

        /// <summary>
        /// ���ݱ���Ż�ȡ����
        /// </summary>
        /// <param name="iceBoxId"></param>
        /// <returns></returns>
        public IceBox GetIceBoxById(string iceBoxId)
        {
            string[] parm = new string[] { iceBoxId };
            ArrayList arr = SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxId", parm);
            if (arr.Count > 0)
            {
                return arr[0] as IceBox;
            }
            return null;
        }

        /// <summary>
        /// ���ݱ���������ȡ
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public ArrayList GetIceBoxByType(string typeId)
        {
            string[] parm = new string[] { typeId };
            return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByType", new string[] { typeId });
        }

        /// <summary>
        /// ���Һ������ڵı���
        /// </summary>
        /// <param name="boxId">�걾��Id</param>
        /// <returns></returns>
        public IceBox GetIceBoxBySpecBoxId(string boxId)
        {
            try
            {
                return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxBySpecBoxId", new string[] { boxId })[0] as IceBox;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���Һ������ڵļ���
        /// </summary>
        /// <param name="shelfId">����Id</param>
        /// <returns></returns>
        public IceBox GetIceBoxByShelf(string shelfId)
        {
            try
            {
                return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByShelfId", new string[] { shelfId })[0] as IceBox;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݲ������ͺͱ걾���ͻ�ȡ���� (OR����)
        /// </summary>
        /// <param name="disId">��������</param>
        /// <param name="specTypeId">�걾����</param>
        /// <returns></returns>
        public ArrayList GetIceBoxByDisOrSpecType(string disId, string specTypeId)
        {
            try
            {
                return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByDisAndSpecType", new string[] { disId, specTypeId });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݲ������ͺͱ걾���ͻ�ȡ���� (And����)
        /// </summary>
        /// <param name="disId">��������</param>
        /// <param name="specTypeId">�걾����</param>
        /// <returns></returns>
        public ArrayList GetIceBoxByDisAndSpecType(string disId, string specTypeId)
        {
            try
            {
                return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByDisAndSpecType1", new string[] { disId, specTypeId });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݴ洢ʱ��+�������ͣ��걾���Ͳ�ѯ And ����
        /// </summary>
        /// <param name="disId"></param>
        /// <param name="specTypeId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList GetIceBoxBySubStoreTimeA(string disId, string specTypeId, string start, string end)
        {
            try
            {
                return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByDisAndSpecTypeAndStoreTime1", new string[] { disId, specTypeId, start, end });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݴ洢ʱ��+�������ͣ��걾���Ͳ�ѯ Or ����
        /// </summary>
        /// <param name="disId"></param>
        /// <param name="specTypeId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList GetIceBoxBySubStoreTimeO(string disId, string specTypeId, string start, string end)
        {
            try
            {
                return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByDisAndSpecTypeAndStoreTime", new string[] { disId, specTypeId, start, end });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݲ���ʱ���ѯ����
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList GetIceBoxBySubStoreTime(string start, string end)
        {
            try
            {
                return this.SelectIceBox("Speciment.BizLogic.IceBoxManage.GetIceBoxByStoreTime1", new string[] { "", "", start, end });
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
