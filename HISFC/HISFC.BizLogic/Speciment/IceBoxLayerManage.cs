using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: ��������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class IceBoxLayerManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">�����в����</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.IceBoxLayer layer)
        {           
            layer.Comment = "";
            string[] str = new string[]
						{
							layer.LayerId.ToString(), 
							layer.IceBox.IceBoxId.ToString(),
                            layer.IsOccupy.ToString(),
                            layer.LayerNum.ToString(),
                            layer.Height.ToString(),
                            layer.Row.ToString(),
                            layer.Col.ToString(),
                            layer.SpecID.ToString(),
                            layer.SaveType.ToString(),
                            layer.DiseaseType.DisTypeID.ToString(),
                            layer.SpecTypeID.ToString(),
                            layer.BloodOrOrgId,
                            layer.Capacity.ToString(),
                            layer.OccupyCount.ToString(),
                            layer.Comment
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
            sequence = this.GetSequence("Speciment.BizLogic.IceBoxLayerManage.GetNextSequence");
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

        #region ���±�������
        /// <summary>
        /// ���¼��ӹ��
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateIceBoxLayer(string sqlIndex, params string[] args)
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
        /// ��ȡ�걾��֯������Ϣ
        /// </summary>
        /// <returns>��֯����ʵ��</returns>
        private IceBoxLayer SetIceBoxLayer()
        {
            IceBoxLayer layer = new IceBoxLayer();
            try
            {
                layer.LayerId = Convert.ToInt32(this.Reader[0].ToString());
                layer.IceBox.IceBoxId = Convert.ToInt32(this.Reader[1].ToString());
                layer.IsOccupy = Convert.ToInt16(this.Reader[2].ToString());
                layer.LayerNum = Convert.ToInt16(this.Reader[3].ToString());
                layer.Height = Convert.ToInt32(this.Reader[4].ToString());
                layer.Row = Convert.ToInt32(this.Reader[5].ToString());
                layer.Col = Convert.ToInt32(this.Reader[6].ToString());
                layer.SpecID = Convert.ToInt32(this.Reader[7].ToString());
                layer.SaveType = Convert.ToChar(this.Reader[8].ToString());
                layer.DiseaseType.DisTypeID = Convert.ToInt32(this.Reader[9].ToString());
                if (!Reader.IsDBNull(10)) layer.SpecTypeID = Convert.ToInt32(this.Reader[10].ToString());
                if (!Reader.IsDBNull(11)) layer.BloodOrOrgId = Reader[11].ToString();
                if (!Reader.IsDBNull(12)) layer.Capacity = Convert.ToInt32(Reader[12].ToString());
                if (!Reader.IsDBNull(13)) layer.OccupyCount = Convert.ToInt32(Reader[13].ToString());
                if (!Reader.IsDBNull(14)) layer.Comment = Reader[14].ToString();

                // orgType.OrgName = this.Reader[1].ToString();
                //orgType.IsShowOnApp = Convert.ToInt16(this.Reader[2].ToString());
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return layer;
        }
        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="specBox">��������ı����</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertIceBoxLayer(FS.HISFC.Models.Speciment.IceBoxLayer layer)
        {
            return this.UpdateIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.Insert", this.GetParam(layer));

        }

        /// <summary>
        /// ���ݱ������Ͳ�õ�IceBoxLayerID
        /// </summary>
        /// <param name="iceBoxID">�����ID</param>
        /// <param name="layNum">��</param>
        /// <returns>LayerID</returns>
        public IceBoxLayer GetLayerIDByIceBox(string iceBoxID, string layNum)
        {
            string[] parm = new string[] { iceBoxID, layNum };
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.IceBoxLayerManage.SelectIDByIceBox", ref sql) == -1)
                return  null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            IceBoxLayer layer = new IceBoxLayer();
            while (this.Reader.Read())
            {
                layer = SetIceBoxLayer();
            }
            this.Reader.Close();
            return layer;
        }

        /// <summary>
        ///  ����ID,�ͱ걾�й��ID
        /// </summary>
        /// <param name="disTypeId">����Id</param>
        /// <param name="specId">�걾�й��Id</param>
        /// <param name="specClassId">�걾����ID</param>
        /// <param name="specTypeId">�걾����ID</param>
        /// <returns>�����</returns>
        private IceBoxLayer GetLayer(string selectSQL, string disTypeId, string specId, string specClassId, string specTypeId)
        {
            string[] parm = new string[] { disTypeId, specId, specClassId, specTypeId };
            string sql = "";
            if (this.Sql.GetSql(selectSQL, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            IceBoxLayer layer = new IceBoxLayer();
            while (this.Reader.Read())
            {
                layer = SetIceBoxLayer();
                break;
            }
            this.Reader.Close();
            return layer;
        }

        /// <summary>
        /// ����sql��������IceBoxLayer
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList GetIceBoxLayer(string sqlIndex, string[] parm)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            IceBoxLayer layer;
            ArrayList arrLayer = new ArrayList();
            while (this.Reader.Read())
            {
                layer = new IceBoxLayer();
                layer = SetIceBoxLayer();
                arrLayer.Add(layer);
            }
            this.Reader.Close();
            return arrLayer;
        }

        /// <summary>
        /// ����sql��������IceBoxLayer
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList GetIceBoxLayerAndAtt(string sqlIndex, string[] parm, string whereSub)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            sql += whereSub;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            IceBoxLayer layer;
            ArrayList arrLayer = new ArrayList();
            while (this.Reader.Read())
            {
                layer = new IceBoxLayer();
                layer = SetIceBoxLayer();
                arrLayer.Add(layer);
            }
            this.Reader.Close();
            return arrLayer;
        }

        /// <summary>
        ///  ����ID,�ͱ걾�й��ID, ����걾����ڱ�����ִ�д˺���
        /// </summary>
        /// <param name="disTypeId">����Id</param>
        /// <param name="specId">�걾�й��Id</param>
        /// <param name="specClassId">�걾����ID</param>
        /// <param name="specTypeId">�걾����ID</param>
        /// <returns>�����</returns>
        public IceBoxLayer LayerGetLayerById(string disTypeId, string specId, string specClassId, string specTypeId)
        {
            return GetLayer("Speciment.BizLogic.IceBoxLayerManage.GetLayerById", disTypeId, specId, specClassId, specTypeId);
        }

        /// <summary>
        ///  ����ID,�ͱ걾�й��ID, ����걾����ڶ����ִ�д˺���
        /// </summary>
        /// <param name="disTypeId">����Id</param>
        /// <param name="specId">����ܹ��Id</param>
        /// <param name="specClassId">�걾����ID</param>
        /// <param name="specTypeId">�걾����ID</param>
        /// <returns>�����</returns>
        public IceBoxLayer ShelfGetLayerById(string disTypeId, string specId, string specClassId, string specTypeId)
        {
            return GetLayer("Speciment.BizLogic.IceBoxLayerManage.GetLayerByShelfId", disTypeId, specId, specClassId, specTypeId);
        }

        /// <summary>
        /// ���±�����ռ�����
        /// </summary>
        /// <param name="layerId"></param>
        /// <returns></returns>
        public int UpdateOccupy(string occupyFlag, string layerId)
        {
            string[] parms = new string[] { occupyFlag ,layerId };
            return this.UpdateIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.UpdateOccupy", parms);
        }

        /// <summary>
        /// ���±���ռ�õ�λ����
        /// </summary>
        /// <param name="occupyCount">ռ�õ�λ����</param>
        /// <param name="layerId">�����ID</param>
        /// <returns></returns>
        public int UpdateOccupyCount(string occupyCount, string layerId)
        {
            string[] parms = new string[] { occupyCount, layerId };
            return this.UpdateIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.UpdateOccupyCount", parms);
        }

        /// <summary>
        /// ��ȡһ���������м���
        /// </summary>
        /// <param name="iceBoxId"></param>
        /// <returns></returns>
        public ArrayList GetIceBoxLayers(string iceBoxId)
        {
            string[] parm = new string[] { iceBoxId };
            return GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.SelectByIceBoxName", parm);
        }

        /// <summary>
        /// ��ȡ������ÿһ��Ĺ��
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public Dictionary<IceBoxLayer, List<int>> GetLayerSpec(ArrayList arr)
        {
            int i = 1;
            List<int> layerNum;
            List<KeyValuePair<IceBoxLayer, List<int>>> listLayerSpec = new List<KeyValuePair<IceBoxLayer, List<int>>>();
            Dictionary<IceBoxLayer, List<int>> dicLayerSpec = new Dictionary<IceBoxLayer, List<int>>();

            //����ÿһ�㣬�����������һ��������ż���LayerNum�У�����������ò�һ����ÿһ�����ü���listLayerSpec
            foreach (IceBoxLayer layer in arr)
            {
                layerNum = new List<int>();
                if (i == 1)
                {
                    layerNum.Add(layer.LayerNum);
                    KeyValuePair<IceBoxLayer, List<int>> tmp = new KeyValuePair<IceBoxLayer, List<int>>(layer, layerNum);
                    listLayerSpec.Add(tmp);
                }
                //Сbug���������и�������һ��������ɲ�һ���Ĺ��SourcePlanManageҲһ��
                for (int k = listLayerSpec.Count - 1; k >= -1; k--)
                {
                    if (k == -1)
                    {
                        layerNum.Add(layer.LayerNum);
                        KeyValuePair<IceBoxLayer, List<int>> tmp = new KeyValuePair<IceBoxLayer, List<int>>(layer, layerNum);
                        listLayerSpec.Add(tmp);
                        break;
                    }
                    KeyValuePair<IceBoxLayer, List<int>> item = listLayerSpec[k];
                    //if (!layer.CheckSameSpec(item.Key))
                    //{
                    //    layerNum.Add(layer.LayerNum);
                    //    KeyValuePair<IceBoxLayer, List<int>> tmp = new KeyValuePair<IceBoxLayer, List<int>>(layer, layerNum);
                    //    listLayerSpec.Add(tmp);
                    //    break;
                    //}
                    if (layer.CheckSameSpec(item.Key))
                    {
                        if (item.Value.Contains(layer.LayerNum))
                            break;
                        else
                        {
                            item.Value.Add(layer.LayerNum);
                            break;
                        }
                    }
                }
                i++;
            }
            foreach (KeyValuePair<IceBoxLayer, List<int>> kp in listLayerSpec)
            {
                dicLayerSpec.Add(kp.Key, kp.Value);
            }
            return dicLayerSpec;
        }

        /// <summary>
        /// ����LayerID��ȡLayer
        /// </summary>
        /// <param name="layerId"></param>
        /// <returns></returns>
        public IceBoxLayer GetLayerById(string layerId)
        {
            ArrayList arrLayer = GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.SelectIDByLayerID", new string[] { layerId });
            return arrLayer[0] as IceBoxLayer;
            //FS.HISFC.BizLogic.Speciment.IceBoxLayer.SelectIDByLayerID
        }

        public int UpdateLayer(IceBoxLayer layer)
        {
            return this.UpdateIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.Update", this.GetParam(layer));
        }

        /// <summary>
        /// ��ȡһ�����������еı����
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public ArrayList GetLayerInOneBox(string boxId)
        {
            return GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.SelectInOneIceBox", new string[] { boxId });
        } 

        /// <summary>
        /// ���ݱ걾��Id��ȡ�����
        /// </summary>
        /// <param name="boxId">�걾��Id</param>
        /// <returns></returns>
        public IceBoxLayer GetLayerBySpecBox(string boxId)
        {
            try
            {
                return this.GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.GetIceBoxLayerBySpecBoxId", new string[] { boxId })[0] as IceBoxLayer;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݼ��ӻ�ȡ���ڵı����
        /// </summary>
        /// <param name="shelfId">����Id</param>
        /// <returns></returns>
        public IceBoxLayer GetLayerByShelf(string shelfId)
        {
            try
            {
                return this.GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.GetIceBoxLayerByShelfId", new string[] { shelfId })[0] as IceBoxLayer;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݲ������ͺͱ걾���ͻ�ȡ����� (OR����)
        /// </summary>
        /// <param name="disId">��������</param>
        /// <param name="specTypeId">�걾����</param>
        /// <returns></returns>
        public ArrayList GetLayerByDisOrSpecType(string disId, string specTypeId)
        {
            try
            {
                return this.GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.GetIceBoxLayerByDisAndSpecType", new string[] { disId, specTypeId });
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݲ������ͺͱ걾���ͻ�ȡ����� (And����)
        /// </summary>
        /// <param name="disId">��������</param>
        /// <param name="specTypeId">�걾����</param>
        /// <returns></returns>
        public ArrayList GetIceBoxByDisAndSpecType(string disId, string specTypeId)
        {
            try
            {
                return this.GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.GetIceBoxLayerByDisAndSpecType1", new string[] { disId, specTypeId });
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
        public ArrayList GetIceBoxBySubStoreTimeA(string disId, string specTypeId,string start ,string end)
        {
            try
            {
                return this.GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.GetIceBoxLayerByDisAndSpecTypeAndStoreTime1", new string[] { disId, specTypeId, start, end });
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// ���ݴ洢ʱ��+�������ͣ��걾���Ͳ�ѯ OR ����
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
                return this.GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.GetIceBoxLayerByDisAndSpecTypeAndStoreTime", new string[] { disId, specTypeId, start, end });
            }
            catch
            {
                return null;
            }
        } 

        /// <summary>
        /// ���ݱ걾�洢ʱ���ѯ�����
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList GetIceBoxByStoreTime(string start, string end)
        {
            try
            {
                return this.GetIceBoxLayer("Speciment.BizLogic.IceBoxLayerManage.GetIceBoxLayerByStoreTime", new string[] { start, end });
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
