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
    /// [��������: �걾�й���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-09-27]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-19' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class SpecBoxManage : FS.FrameWork.Management.Database
    {

        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">�걾�ж���</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecBox specBox)
        {
            string[] str = new string[]
						{
							specBox.BoxId.ToString(), 
							specBox.BoxBarCode,
                            specBox.BoxSpec.BoxSpecID.ToString(),
                            specBox.DesCapType.ToString(),
                            specBox.DesCapID.ToString(),
                            specBox.DesCapCol.ToString(),
                            specBox.DesCapRow.ToString(),
                            specBox.DesCapSubLayer.ToString(),
                            specBox.DiseaseType.DisTypeID.ToString(),
                            specBox.OrgOrBlood.ToString(),
                            specBox.SpecTypeID.ToString(),
                            specBox.IsOccupy.ToString(),
                            specBox.Capacity.ToString(),
                            specBox.OccupyCount.ToString(),
                            specBox.InIceBox.ToString(),
                            specBox.Comment,
                            specBox.SpecialUse
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
            sequence = this.GetSequence("Speciment.BizLogic.SpecBoxManage.GetNextSequence");
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
            this.Err = errorText + "[" + this.Err + "]"; // + "��TerminalConfirm.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion
        #endregion

        #region ���±걾�в���

        private SpecBox SetSpecBox()
        {
            SpecBox specBox = new SpecBox();
            try
            {
                specBox.BoxId = Convert.ToInt32(this.Reader["BOXID"].ToString());
                specBox.BoxBarCode = this.Reader["BOXBARCODE"].ToString();
                specBox.BoxSpec.BoxSpecID = Convert.ToInt32(this.Reader["BOXSPECID"].ToString());
                specBox.DesCapType = Convert.ToChar(this.Reader["DESCAPTYPE"].ToString());
                specBox.DesCapID = Convert.ToInt32(this.Reader["DESCAPID"].ToString());
                specBox.DesCapCol = Convert.ToInt32(this.Reader["DESCAPCOL"].ToString());
                specBox.DesCapRow = Convert.ToInt32(this.Reader["DESCAPROW"].ToString());
                specBox.DesCapSubLayer = Convert.ToInt32(this.Reader["DESCAPSUBLAYER"].ToString());
                specBox.DiseaseType.DisTypeID = Convert.ToInt32(this.Reader["DISEASETYPEID"].ToString());
                specBox.OrgOrBlood = Convert.ToInt32(this.Reader["BLOODORORGID"].ToString());
                specBox.SpecTypeID = Convert.ToInt32(this.Reader["SPECTYPEID"].ToString());
                specBox.IsOccupy = Convert.ToChar(this.Reader["ISOCCUPY"].ToString());
                specBox.Capacity = Convert.ToInt32(this.Reader["CAPACITY"].ToString());
                specBox.OccupyCount = Convert.ToInt32(this.Reader["OCCUPYCOUNT"].ToString());
                specBox.InIceBox = Convert.ToChar(this.Reader["INBOX"].ToString());

                if (!Reader.IsDBNull(15)) specBox.SpecialUse = this.Reader["SPECUSE"].ToString();
                else specBox.SpecialUse = "";

                if (!Reader.IsDBNull(16)) specBox.Comment = this.Reader["MARK"].ToString();
                else specBox.Comment = "";

            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return specBox;
                 
        }

        /// <summary>
        /// ���±걾��
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSpecBox(string sqlIndex, params string[] args)
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
        /// ��ȡ���������ı걾��List��
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetSpecBox(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return null;
            }
            if (this.ExecQuery(sql, args)==-1)
                return null;
            ArrayList arrSpecBox = new ArrayList();
            while (this.Reader.Read())
            {
                SpecBox boxTmp = SetSpecBox();
                arrSpecBox.Add(boxTmp); 
            }
            this.Reader.Close();
            return arrSpecBox;
        }
        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// �걾�в���
        /// </summary>
        /// <param name="specBox">��������ı걾��</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertSpecBox(FS.HISFC.Models.Speciment.SpecBox specBox)
        {
            return this.UpdateSpecBox("Speciment.BizLogic.SpecBoxManage.Insert", this.GetParam(specBox));
        }

        /// <summary>
        /// �ڱ걾�����ŵ������У����ұ������һ������
        /// </summary>
        /// <param name="capID">�걾��������ID</param>
        /// <returns></returns>
        public SpecBox GetLastCapBox(string sql,string capID)
        {
            string[] parms = new string[] { capID };
            ArrayList arrSpecBox = GetSpecBox(sql ,capID);
            SpecBox lastBox = new SpecBox();
            //���arrSpecBox.Count<0 ˵�����ӻ���ǿյģ���ô��ʼ����һ��λ��
            if (arrSpecBox.Count <= 0)
            {
                lastBox.DesCapCol = 0;
                lastBox.DesCapRow = 1;
                lastBox.DesCapSubLayer = 1;
                return lastBox;
 
            }           
            foreach (SpecBox s in arrSpecBox)
            {
                lastBox = s; 
            }
            return lastBox; 
        }

        /// <summary>
        /// ������ڱ�����
        /// </summary>
        /// <param name="capID"></param>
        /// <returns></returns>
        public SpecBox LayerGetLastCapBox(string capID)
        {
            return GetLastCapBox("Speciment.BizLogic.SpecBoxManage.BoxLocationLayer", capID);
        }

        /// <summary>
        /// ������ڶ������
        /// </summary>
        /// <param name="capID"></param>
        /// <returns></returns>
        public SpecBox ShelfGetLastCapBox(string capID)
        {
            return GetLastCapBox("Speciment.BizLogic.SpecBoxManage.BoxLocation", capID);
        }

        /// <summary>
        /// ���ݲ������ͺͱ걾�еĹ���ȡ�ù��ģ���û�ű걾�ĺ���
        /// </summary>
        /// <param name="disTypeId">��������</param>
        /// <param name="specId">���ID</param>
        /// <param name="saveType">������������</param>
        /// <param name="specClassId">�걾����ID</param>
        /// <param name="specTypeId">�걾����ID</param>
        /// <returns></returns>
        public SpecBox GetUsedNoOccupyBox(string disTypeId, string specId, string saveType, string specClassId, string specTypeId, string specUse)
        {
            string[] parms = new string[] { disTypeId, specId,saveType, specClassId, specTypeId };
            ArrayList arrTmp = GetSpecBox("Speciment.BizLogic.SpecBoxManage.GetUsedNoOccupyBox", parms);
            SpecBox usedBox = new SpecBox();
            if (arrTmp.Count <= 0)
                return null;
            foreach (SpecBox s in arrTmp)
            {
                if (s.SpecialUse == specUse)
                {
                    usedBox = s;
                    break;
                }
                else
                {
                    continue;
                }
                //break;
            }
            return usedBox;
        }

        /// <summary>
        /// ��ָ���ļ��ӻ��߱����л�ȡδʹ�õı걾��
        /// </summary>
        /// <param name="saveType"></param>
        /// <param name="capId"></param>
        /// <returns></returns>
        public SpecBox GetUsedNoOccupyBox(string saveType, string capId, string specUse)
        {
            string[] parms = new string[] { capId,saveType, specUse };
            ArrayList arrTmp = GetSpecBox("Speciment.BizLogic.SpecBoxManage.GetUsedNoOccupyBoxInCap", parms);
            if (arrTmp.Count > 0)
                return arrTmp[0] as SpecBox;
            else
                return null;
        }

        /// <summary>
        /// ���ݼ��ӺŻ��߱����Ż�ȡ�걾��List
        /// </summary>
        /// <param name="capId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetBoxByCap(string capId, char type)
        {
            string[] parms = new string[] { capId };
            ArrayList arrBox = new ArrayList();
            switch (type)
            {
                case 'J':
                    arrBox = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.SelectByShelfID", parms);
                    break;
                case 'B':
                    arrBox = GetSpecBox("Speciment.BizLogic.SpecBoxManage.SelectByLayerID", parms);
                    break;
                default:
                    break;
            }
            return arrBox;
        }

        /// <summary>
        /// ����Ŀ�������� ĳһ�еĺ��Ӽ���
        /// </summary>
        /// <param name="capId"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public ArrayList GetBoxByCapRow(string capId, string row)
        {
            string[] parms = new string[] { capId, row };
            ArrayList arrBox = new ArrayList();
            arrBox = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.LayerIDAndRow", parms);
            return arrBox;
        }

        /// <summary>
        /// ����Ŀ��������ĳһ�еĺ���
        /// </summary>
        /// <param name="capId"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public ArrayList GetBoxByCapCol(string capId, string col)
        {
            string[] parms = new string[] { capId, col };
            ArrayList arrBox = new ArrayList();
            arrBox = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.LayerIDAndCol", parms);
            return arrBox;
        }

        /// <summary>
        /// ���ҵ�ǰʹ�õı걾��
        /// </summary>
        /// <param name="disTypeId"></param>
        /// <param name="specTypeId"></param>
        /// <returns></returns>
        public ArrayList GetLastLocation(string disTypeId, string specTypeId)
        {
            string[] parms = new string[] { disTypeId, specTypeId };           
            ArrayList arrBox = new ArrayList();
            arrBox = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.SubSpecLocate", parms);
            return arrBox;
        }

        /// <summary>
        /// ���±걾�е�ռ������
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public int UpdateOccupyCount(string occupyCount,string boxId)
        {
            string[] parms = new string[] { occupyCount, boxId };
            return this.UpdateSpecBox("Speciment.BizLogic.SpecBoxManage.UpdateOccupyCount", parms);
        }

        /// <summary>
        /// ���±걾��������־λ
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public int UpdateOccupy(string boxId, string occupyFlag)
        {
            string[] parms = new string[] { occupyFlag,boxId };
            return this.UpdateSpecBox("Speciment.BizLogic.SpecBoxManage.UpdateOccupy", parms);
        }

        public int UpdateSotreFlag(string flag, string boxId)
        {
            string[] parms = new string[] { flag, boxId };
            return this.UpdateSpecBox("Speciment.BizLogic.SpecBoxManage.InBoxFlag", parms);
        }

        /// <summary>
        /// �걾�����ʱ���ݽ�������������ȡ�걾��List
        /// </summary>
        /// <param name="boxSpecID">���</param>
        /// <param name="orgTypeID">�걾����</param>
        /// <param name="specTypeID">�걾����</param>
        /// <param name="diseaseTypeID">����</param>
        /// <param name="boxCode">������</param>
        /// <param name="isOccupy">�Ƿ�����</param>
        /// <returns>�걾��List</returns>
        public ArrayList GetBoxForInStore(string boxSpecID, string orgTypeID, string specTypeID, string diseaseTypeID, string boxCode, string isOccupy)
        {
            string[] parms = new string[] { boxSpecID, orgTypeID, specTypeID, diseaseTypeID, boxCode, isOccupy };
            ArrayList arrBox = new ArrayList();
            arrBox = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.GetBoxForInStore", parms);
            return arrBox;
        }

        /// <summary>
        /// ����sql����ȡboxlist����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="ds">���ص�dataset����</param>
        /// <returns>Ӱ�������</returns>
        public int GetBoxBySql(string sql, ref DataSet ds)
        {      
            return  this.ExecQuery(sql, ref ds);            
        }

        public ArrayList GetBoxBySql(string sql)
        {
            if (this.ExecQuery(sql) == -1)
                return null;
            ArrayList arrSpecBox = new ArrayList();
            while (this.Reader.Read())
            {
                SpecBox boxTmp = SetSpecBox();
                arrSpecBox.Add(boxTmp);
            }
            this.Reader.Close();
            return arrSpecBox;
        }

        /// <summary>
        /// ���ݣ£���ɣ䡡��ѯ�걾��
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public SpecBox GetBoxById(string boxId)
        {
            string sql = "select * from SPEC_BOX WHERE BOXID = " + boxId;
            if (this.ExecQuery(sql) == -1)
                return null;
            SpecBox specBox = new SpecBox();
            while (Reader.Read())
            {
                specBox = SetSpecBox();
            }
            Reader.Close();
            return specBox;
        }

        public int UpdateSpecBox(SpecBox specBox)
        {
            return this.UpdateSpecBox("Speciment.BizLogic.SpecBoxManage.Update", GetParam(specBox));
 
        }

        /// <summary>
        /// �����������ȡ���ӵ���Ϣ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public SpecBox GetBoxByBarCode(string barCode)
        {
            string sql = "select * from SPEC_BOX WHERE BOXBARCODE = '" + barCode + "' and DESCAPID > 0";
            if (this.ExecQuery(sql) == -1)
                return null;
            SpecBox specBox = new SpecBox();
            while (Reader.Read())
            {
                specBox = SetSpecBox();
            }
            Reader.Close();
            return specBox;
        }

        /// <summary>
        /// ���ݱ걾����ʱ��� ��ѯ�걾���б�
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList GetBoxByIn(string startTime, string endTime)
        {
            ArrayList arr = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.SpecInTime", new string[] { startTime, endTime });
            return arr;
        }

        /// <summary>
        /// ���ݱ걾�����ʱ��β�ѯ�걾���б�
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList GetBoxByOut(string startTime, string endTime)
        {
            ArrayList arr = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.SpecOutTime", new string[] { startTime, endTime });
            return arr;
        }

        /// <summary>
        /// ��ָ����λ���ϣ��鿴�Ƿ���ڱ걾��
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="height"></param>
        /// <param name="capId"></param>
        /// <param name="capType"></param>
        /// <returns></returns>
        public SpecBox GetByPoint(string col, string row, string height, string capId, string capType)
        {
            ArrayList arr = this.GetSpecBox("Speciment.BizLogic.SpecBoxManage.GetBoxInPointLocation", new string[] { col, row, height, capId, capType });
            try
            {
                return arr[0] as SpecBox;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
