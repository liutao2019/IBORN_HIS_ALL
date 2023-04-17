using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: ����ܹ���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-15]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-18' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class ShelfManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">����ܶ���</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.Shelf shelf)
        {
            string[] str = new string[]
						{
							shelf.ShelfID.ToString(),
                            shelf.IceBoxLayer.Row.ToString(),
                            shelf.IceBoxLayer.Col.ToString(),
                            shelf.IceBoxLayer.Height.ToString(),
                            shelf.IceBoxLayer.LayerId.ToString(),
                            shelf.SpecBarCode.ToString(),
                            shelf.ShelfSpec.ShelfSpecID.ToString(),
                            shelf.Capacity.ToString(),
                            shelf.OccupyCount.ToString(),
                            shelf.IsOccupy.ToString(),
                            shelf.DisTypeId.ToString(),
                            shelf.SpecTypeId.ToString(),
                            shelf.TumorType,
                            shelf.Comment
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
            sequence = this.GetSequence("Speciment.BizLogic.ShelfManage.GetNextSequence");
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
        /// ���¼��ӿ�λ��Ϣ
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateShelf(string sqlIndex, params string[] args)
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

        private Shelf SetShelf()
        {
            Shelf shelf = new Shelf();
            try
            {
                //�ڲ�ĵڼ���
                shelf.ShelfID = Convert.ToInt32(this.Reader["SHELFID"].ToString());                
                //shelf.IceBoxLayer.Col = Convert.ToInt32(this.Reader[""].ToString());
                shelf.IceBoxLayer.LayerId = Convert.ToInt32(this.Reader["ICEBOXLAYERID"].ToString());
                shelf.SpecBarCode = this.Reader["SHELFBARCODE"].ToString();
                shelf.ShelfSpec.ShelfSpecID = Convert.ToInt32(this.Reader["SHELFSPECID"].ToString());
                if (Reader["CAPACITY"] != null)
                {
                    shelf.Capacity = Convert.ToInt32(this.Reader["CAPACITY"].ToString());
                }
                else
                {
                    shelf.Capacity = 0;
                }
                if (Reader["OCCUPYCOUNT"] != null)
                {
                    shelf.OccupyCount = Convert.ToInt32(this.Reader["OCCUPYCOUNT"].ToString());
                }
                else
                {
                    shelf.OccupyCount = 0;
                }
                if (Reader["ISOCCUPY"] != null)
                {
                    shelf.IsOccupy = Convert.ToChar(this.Reader["ISOCCUPY"].ToString());
                }
                else
                    shelf.IsOccupy = '0';
                shelf.Comment = this.Reader["MARK"].ToString();
                shelf.DisTypeId = Convert.ToInt32(this.Reader["DISTYPEID"].ToString());
                shelf.SpecTypeId = Convert.ToInt32(this.Reader["SPECTYPEID"].ToString());
                shelf.TumorType = this.Reader["TUMORTYPE"].ToString();
                shelf.IceBoxLayer.Col = Convert.ToInt32(this.Reader["SHELFCOL"].ToString());
                shelf.IceBoxLayer.Row = Convert.ToInt32(this.Reader["SHELFROW"].ToString()) ;
                shelf.IceBoxLayer.Height = Convert.ToInt32(this.Reader["SPECLAYER"].ToString());

            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return shelf;
        }

        /// <summary>
        /// �����������ҳ�����������ShelfList
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList SelectShelf(string sqlIndex, string[] parm)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            Shelf shelf;
            ArrayList arrList = new ArrayList();
            while (this.Reader.Read())
            {
                shelf = new Shelf();
                shelf = SetShelf();
                arrList.Add(shelf);
            }
            this.Reader.Close();
            return arrList;
        }

        #endregion

        #region ��������

        /// <summary>
        /// ����ܲ���
        /// </summary>
        /// <param name="specBox">��������ļ�</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertShelf(FS.HISFC.Models.Speciment.Shelf shelf)
        {
            return this.UpdateShelf("Speciment.BizLogic.ShelfManage.Insert", this.GetParam(shelf));

        }

        /// <summary>
        ///  ����ID,�ͱ걾�й��ID
        /// </summary>
        /// <param name="disTypeId">����Id</param>
        /// <param name="specId">�걾�й��Id</param>
        /// <param name="specClassId">�걾����ID</param>
        /// <param name="specTypeId">�걾����ID</param>
        /// <param name="shelfId">�걾�ܵ�ID��=shelfId</param>
        /// <returns>�䶳��</returns>
        
        public Shelf GetShelf(string disTypeId, string specId, string specTypeId, string shelfId)
        {
            string[] parm = new string[] { disTypeId, specId, specTypeId, shelfId };
            ArrayList arrShelf = this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfBySpecBox", parm);
            if (arrShelf.Count >= 1)
            {
                return arrShelf[0] as Shelf;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���Ұ��ղ��֣���񣬱걾���Ͳ��Ҽ��� ���Ҽ������ڲ��Id����layerId�У������ƿ�
        /// </summary>
        /// <param name="disTypeId"></param>
        /// <param name="specId"></param>
        /// <param name="specTypeId"></param>
        /// <param name="layerId"></param>
        /// <returns></returns>
        public ArrayList GetShelfNotInOneLayer(string disTypeId, string specId, string specTypeId, string layerId)
        {
            string[] parm = new string[] { disTypeId, specId, specTypeId, layerId };
            return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfNotInLayer", parm);
                      
        }

        /// <summary>
        /// ���Ұ��ղ��֣���񣬱걾���Ͳ��Ҽ��� ���Ҽ������ڱ����Id����boxId�У������ƿ�
        /// </summary>
        /// <param name="disTypeId"></param>
        /// <param name="specId"></param>
        /// <param name="specTypeId"></param>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public ArrayList GetShelfNotInOneIceBox(string disTypeId, string specId, string specTypeId, string boxId)
        {
            string[] parm = new string[] { disTypeId, specId, specTypeId, boxId };
            return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfNotInIceBox", parm);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disTypeId"></param>
        /// <param name="specId"></param>
        /// <param name="specTypeId"></param>
        /// <param name="shelfId"></param>
        /// <returns></returns>
        public ArrayList GetOtherShelf(string disTypeId, string specId, string specTypeId, string shelfId)
        {
            string[] parm = new string[] { disTypeId, specId, specTypeId, shelfId };
            return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfBySpecBox", parm);
        }

        /// <summary>
        /// ����barCode���߼���Id��ȡ������Ϣ
        /// </summary>
        /// <param name="shelfId"></param>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public Shelf GetShelfByShelfId(string shelfId, string barCode)
        {
            ArrayList arrShelf = new ArrayList();
            if (shelfId != "")
            {
                string[] parms = new string[] { shelfId };
                arrShelf = this.SelectShelf("Speciment.BizLogic.ShelfManage.SelectByShelfID", parms);              
            }
            if (barCode != "")
            {
                arrShelf = this.SelectShelf("Speciment.BizLogic.ShelfManage.SelectByShelfBarCode", new string[] { barCode });                
            }
            return arrShelf[0] as Shelf;
        }

        /// <summary>
        /// ����ռ�õ�����
        /// </summary>
        /// <param name="count"></param>
        /// <param name="shelfId"></param>
        /// <returns></returns>
        public int UpdateOccupyCount(string count, string shelfId)
        {
            string[] parms = new string[] { count,shelfId };
            return this.UpdateShelf("Speciment.BizLogic.ShelfManage.OccupyCount", parms);
        }

        /// <summary>
        /// ���¼����Ƿ�����
        /// </summary>
        /// <param name="shelfId"></param>
        /// <returns></returns>
        public int UpdateIsFull(string occupyFlag,string shelfId)
        {
            string[] parms = new string[] { occupyFlag,shelfId };
            return this.UpdateShelf("Speciment.BizLogic.ShelfManage.IsOccupy", parms);
        }

        /// <summary>
        /// ��ȡ�����ļ���
        /// </summary>
        /// <param name="layerID"></param>
        /// <returns></returns>
        public ArrayList GetShelfByLayerID(string layerID)
        {
            string[] parms = new string[] { layerID };
            ArrayList arrShelf = this.SelectShelf("Speciment.BizLogic.ShelfManage.SelectByLayerID", parms);
            return arrShelf; 
        }

        /// <summary>
        /// ���¼���ʵ��
        /// </summary>
        /// <param name="shelf"></param>
        /// <returns></returns>
        public int UpdateShelf(Shelf shelf)
        {
            return this.UpdateShelf("Speciment.BizLogic.ShelfManage.Update", GetParam(shelf));
        }

        /// <summary>
        /// ɨ�����㣬�����Ӷ�λ
        /// </summary>
        /// <param name="arrShelf">ĳһ���е����м���</param>
        /// <returns></returns>
        public Shelf ScanLayer(ArrayList arrShelf)
        {
            Shelf current = new Shelf();
            Shelf next = new Shelf();

            //���û�м���
            if (arrShelf.Count == 0)
            {
                return new Shelf();
            }
            if (arrShelf.Count >= 1)
            {
                current = arrShelf[0] as Shelf;
                //�����һ��û�м���
                if (current.IceBoxLayer.Col > 1)
                {
                    return new Shelf();
                }
            }
            //���ֻ��һ�����ӣ����ص�ǰ��
            if (arrShelf.Count == 1)
            {
                return current;
            }
            for (int i = 0; i < arrShelf.Count - 1; i++)
            {
                current = arrShelf[i] as Shelf;
                next = arrShelf[i + 1] as Shelf;
                
                //������������ǰ��ŵ�
                if (next.IceBoxLayer.Col - current.IceBoxLayer.Col == 1)
                {
                    continue;
                }
                if (next.IceBoxLayer.Col - current.IceBoxLayer.Col >= 2)
                {
                    return current;
                }
            }
            //����������,���ֶ��ǰ��ŵ�,�������һ��
            return next; 
        }

        /// <summary>
        /// ��ȡ�걾�����ڵļ���
        /// </summary>
        /// <param name="boxId">�걾��ID</param>
        /// <returns></returns>
        public Shelf GetShelfByBoxId(string boxId)
        {
            try
            {
                return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfBySpecBoxId", new string[] { boxId })[0] as Shelf;
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
        public ArrayList GetShelfByDisOrSpecType(string disId, string specTypeId)
        {
            try
            {
                return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfByDisAndSpecType", new string[] { disId, specTypeId });
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
        public ArrayList GetShelfByDisAndSpecType(string disId, string specTypeId)
        {
            try
            {
                return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfByDisAndSpecType1", new string[] { disId, specTypeId });
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
        public ArrayList GetShelfBySubStoreTimeA(string disId, string specTypeId, string start, string end)
        {
            try
            {
                return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfByDisAndSpecTypeAndStoreTime1", new string[] { disId, specTypeId, start, end });
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
        public ArrayList GetShelfBySubStoreTimeO(string disId, string specTypeId, string start, string end)
        {
            try
            {
                return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfByDisAndSpecTypeAndStoreTime", new string[] { disId, specTypeId, start, end });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݴ洢ʱ���ѯ�걾�����б�
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList GetShelfBySubStoreTime(string start, string end)
        {
            try
            {
                return this.SelectShelf("Speciment.BizLogic.ShelfManage.GetShelfByStoreTime", new string[] { start, end });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ���ӵ������ı걾��
        /// </summary>
        /// <param name="shelfId"></param>
        /// <returns></returns>
        public string GetMaxOrMinSubInShelf(string shelfId,string minOrMax)
        {
            string sql = @"
                          select case length(oper(subbarcode)) when 9 then  substr(oper(subbarcode),2,6) else substr(oper(subbarcode),1,6) end
                          from SPEC_SUBSPEC
                          where boxid in
                          (
                          select boxid 
                          from SPEC_BOX
                          where DESCAPID = {0} and DESCAPTYPE = 'J'

                          )
                          and subbarcode <>''";
            sql = sql.Replace("oper", minOrMax);
            sql = string.Format(sql, shelfId);
            return this.ExecSqlReturnOne(sql);
        }
 

        #endregion
    }
}
