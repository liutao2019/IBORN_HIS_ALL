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
    /// [��������: ��װ�걾����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-11-18]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-19' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class SubSpecManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������

        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="subSpec">��װ�걾</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SubSpec subSpec)
        {
            //string sequence = "";
            
            //GetNextSequence(ref sequence);
            string[] str = new string[]
						{
							subSpec.SubSpecId.ToString(), 
                            subSpec.SubBarCode,
                            subSpec.SpecId.ToString(),//ԭ�걾ID
                            subSpec.StoreTime.ToString(),
                            subSpec.SpecCount.ToString(),//
                            subSpec.SpecCap.ToString(),
                            subSpec.IsCancer.ToString(),
                            subSpec.SpecTypeId.ToString(),
                            subSpec.LastReturnTime.ToString(),
                            subSpec.DateReturnTime.ToString(),
                            subSpec.Status,
                            subSpec.IsReturnable.ToString(),
                            subSpec.BoxId.ToString(),
                            subSpec.BoxStartRow.ToString(),
                            subSpec.BoxStartCol.ToString(),
                            subSpec.BoxEndRow.ToString(),
                            subSpec.BoxEndCol.ToString(),
                            subSpec.InStore,
                            subSpec.Comment,
                            subSpec.StoreID.ToString()
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
            sequence = this.GetSequence("Speciment.BizLogic.SubSpecManage.GetNextSequence");
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
        /// ���·�װ�걾��Ϣ
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSubSpec(string sqlIndex, params string[] args)
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
        /// ��ȡ�걾��Ϣ
        /// </summary>
        /// <returns>��֯����ʵ��</returns>
        private SubSpec SetSubSpec()
        {
            SubSpec subSpec = new SubSpec();
            try
            {
                subSpec.SubSpecId = Convert.ToInt32(this.Reader["SUBSPECID"].ToString());
                subSpec.BoxId = Convert.ToInt32(this.Reader["BOXID"]);
                subSpec.SubBarCode = this.Reader["SUBBARCODE"].ToString();
                subSpec.SpecId = Convert.ToInt32(this.Reader["SPECID"].ToString());
                subSpec.StoreTime = Convert.ToDateTime(this.Reader["STORETIME"].ToString());
                subSpec.SpecCount = Convert.ToInt32(this.Reader["SPECCOUNT"].ToString());
                subSpec.SpecCap = Convert.ToDecimal(this.Reader["SPECCAP"].ToString());
                subSpec.IsCancer = Convert.ToChar(this.Reader["ISCANCER"].ToString());
                subSpec.SpecTypeId = Convert.ToInt32(this.Reader["SPECTYPEID"].ToString());
                subSpec.LastReturnTime = Convert.ToDateTime(this.Reader["LASTRETURNTIME"].ToString());
                subSpec.DateReturnTime = Convert.ToDateTime(this.Reader["DATERETURNTIME"].ToString());
                subSpec.Status = this.Reader["STATUS"].ToString();
                subSpec.IsReturnable = Convert.ToChar(this.Reader["ISRETURNABLE"].ToString());        
                subSpec.BoxStartRow =  Convert.ToInt32 (this.Reader["BOXSTARTROW"].ToString());
                subSpec.BoxStartCol =  Convert.ToInt32 (this.Reader["BOXSTARTCOL"].ToString());
                subSpec.BoxEndRow =  Convert.ToInt32 (this.Reader["BOXENDROW"].ToString());
                subSpec.BoxEndCol =  Convert.ToInt32 (this.Reader["BOXENDCOL"].ToString());
                subSpec.InStore = this.Reader["INSTORE"].ToString();
                subSpec.Comment = this.Reader["MARK"].ToString();
                subSpec.StoreID = Convert.ToInt32(this.Reader["STOREID"].ToString());
             }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return subSpec;
        }

        /// <summary>
        /// ����������ȡ���������ķ�װ�걾�б�
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetSubSpec(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return null;
            }
            if (this.ExecQuery(sql, args) == -1)
                return null;
            ArrayList arrSubSpec = new ArrayList();
            while (this.Reader.Read())
            {
                SubSpec subTmp = SetSubSpec();
                arrSubSpec.Add(subTmp);
            }
            this.Reader.Close();
            return arrSubSpec;
        }

        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// ��װ�걾����
        /// </summary>
        /// <param name="specBox">�����걾</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertSubSpec(FS.HISFC.Models.Speciment.SubSpec subSpec)
        {
            return this.UpdateSubSpec("Speciment.BizLogic.SubSpecManage.Insert", this.GetParam(subSpec));

        }

        public int UpdateSubSpec(SubSpec subSpec)
        {
            return this.UpdateSubSpec("Speciment.BizLogic.SubSpecManage.Update", this.GetParam(subSpec));
        }

        /// <summary>
        /// ������ָ�����������ı걾
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public SubSpec GetLastSpecInBox(string boxId)
        {
            string[] parms = new string[] { boxId };
            ArrayList arrSubSpec= new ArrayList();
            SubSpec subSpec = new SubSpec();
            arrSubSpec = this.GetSubSpec("Speciment.BizLogic.SubSpecManage.SubLocate", parms);
            //���arrSubSpec.Count<0 ˵�������ǿյģ���ô��ʼ����һ��λ��
            if (arrSubSpec.Count <= 0)
            {
                subSpec.BoxStartCol = 0;
                subSpec.BoxStartRow = 1;
                subSpec.BoxEndCol = 0;
                subSpec.BoxEndRow = 1;
            }
            foreach (SubSpec s in arrSubSpec)
            {
                if (s == null)
                {
                    continue;
                }
                subSpec = s;
                break;
            }
            return subSpec; 
        }

        /// <summary>
        /// ���¹񷵻����һ��λ��
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public SubSpec GetLastSpecForTmp(string boxId)
        {

            string[] parms = new string[] { boxId };
            ArrayList arrSubSpec = new ArrayList();
            SubSpec subSpec = new SubSpec();
            arrSubSpec = this.GetSubSpec("Speciment.BizLogic.SubSpecManage.SubLocateTmp", parms);
            //���arrSubSpec.Count<0 ˵�������ǿյģ���ô��ʼ����һ��λ��
            if (arrSubSpec.Count <= 0)
            {
                subSpec.BoxStartCol = 1;
                subSpec.BoxStartRow = 0;
                subSpec.BoxEndCol = 1;
                subSpec.BoxEndRow = 0;
            }
            foreach (SubSpec s in arrSubSpec)
            {
                subSpec = s;
                break;
            }
            return subSpec; 
        }

        /// <summary>
        /// ���ݱ걾�е�ID,�������ҿ�λ
        /// </summary>
        /// <param name="boxId">�걾��ID</param>
        /// <param name="boxSpec">�걾�й��</param>
        /// <returns></returns>
        public SubSpec ScanSpecBox(string boxId, BoxSpec boxSpec)
        {
            ArrayList arrSubSpecInBox = new ArrayList();
            arrSubSpecInBox = this.GetSubSpec("Speciment.BizLogic.SubSpecManage.InOneBox", new string[] { boxId });
            SubSpec current = new SubSpec();
            SubSpec next = new SubSpec();            

            //����걾�� ���ޱ걾
            if (arrSubSpecInBox.Count == 0)
            {
                SubSpec firstSubSpec = new SubSpec();
                firstSubSpec.BoxStartRow = 1;
                firstSubSpec.BoxEndRow = 1;
                return firstSubSpec;
            }           
            //�����һ�е�һ��û�б걾
            if (arrSubSpecInBox.Count >= 1)
            {
                current = arrSubSpecInBox[0] as SubSpec;
                if (current.BoxStartCol > 1 || (current.BoxStartCol == 1 && current.BoxStartRow > 1))
                {
                    SubSpec firstSubSpec = new SubSpec();
                    firstSubSpec.BoxStartRow = 1;
                    firstSubSpec.BoxEndRow = 1;
                    return firstSubSpec;
                }               
            }
            //���ֻ��1����¼
            if (arrSubSpecInBox.Count == 1)
            {
                return current;
            }
            for (int i = 0; i < arrSubSpecInBox.Count-1; i++)
            {
                current = arrSubSpecInBox[i] as SubSpec;
                next = arrSubSpecInBox[i + 1] as SubSpec;
                int curCol = current.BoxEndCol;
                int curRow = current.BoxEndRow;
                int nextCol = next.BoxEndCol;
                int nextRow = next.BoxEndRow;
                //��������걾����ͬһ�У������⣲���걾�Ƿ����������ǽ��������ص�ǰ�ı걾
                if (curRow == nextRow)
                {
                    if ((nextCol - curCol) == 1)
                    {
                        continue;
                    }
                    if ((nextCol - curCol >= 2))
                    {
                        return current;
                    }
                }
                else
                {
                    //��������걾�����ǰ��ŵ�
                    if ((nextRow - curRow) == 1)
                    {
                        //�����������������һ�У��������ǵ�һ�У�����Ϊ2���걾ʱ���ŵ�
                        if (curCol == boxSpec.Col && nextCol == 1)
                        {
                            continue;
                        }
                        else
                        {
                            return current;
                        }
                    }
                    else
                    {
                        return current;
                    }
                }
            }
            //���������,���ֶ��ǰ��ŵ�,�������һ���걾
            return next;
 
        }

        /// <summary>
        /// ���ݱ걾�е�ID���ҿ�λ��
        /// </summary>
        /// <param name="boxId">�걾��ID</param>
        /// <param name="vpCount">���ؿ�λ��</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int ScanSpecBox(string boxId, out int vpCount)
        {
            vpCount = 0;
            string strSQL = "";
            //ȡSELECT���
            try
            {
                if (this.Sql.GetSql("Speciment.BizLogic.SubSpecManage.VPCount", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�Speciment.BizLogic.SubSpecManage.VPCount�ֶ�!";
                    return -1;
                }
                strSQL = string.Format(strSQL, boxId);
                //ȡ��λ������
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "ִ��ȡ�걾�п�λ������SQL���ʱ����" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        vpCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());  //�걾�п�λ������
                    }
                    catch (Exception ex)
                    {
                        this.Err = "ȡ�걾�п�λ������ʱ����" + ex.Message;
                        return -1;
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "ִ��Sql��� ��ȡ�걾�п�λ��������������" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int UpdateSubSpec(string sql)
        {
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���ݱ걾�Ż����������ȡ�걾��Ϣ
        /// </summary>
        /// <param name="subSpecId"></param>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public SubSpec GetSubSpecById(string subSpecId,string barCode)
        {
            SubSpec subSpec = new SubSpec();
            ArrayList arrSubSpec = new ArrayList();
            if (subSpecId != "")
            {
                arrSubSpec = this.GetSubSpec("Speciment.BizLogic.SubSpecManage.ById", new string[] { subSpecId });
                foreach (SubSpec sub in arrSubSpec)
                {
                    subSpec = sub;
                    break;
                } 
            }
            if (barCode != "")
            {
                arrSubSpec = this.GetSubSpec("Speciment.BizLogic.SubSpecManage.ByBarCode", new string[] { barCode });
                foreach (SubSpec sub in arrSubSpec)
                {
                    subSpec = sub;
                    break;
                } 
            }
            return subSpec;
        }

        /// <summary>
        /// ����ͬһ�����������еı걾
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public ArrayList GetSubSpecInOneBox(string boxId)
        {
            return this.GetSubSpec("Speciment.BizLogic.SubSpecManage.InOneBox", new string[] { boxId });
        }

        /// <summary>
        /// �鿴���� ��������Ƿ���ڱ걾
        /// </summary>
        /// <param name="layerId"></param>
        /// <param name="cap"></param>
        /// <returns></returns>
        public ArrayList GetSubSpecInLayerOrShelf(string capId, string cap)
        {
            //������зű걾��
            if (cap == "B")
            {
                return this.GetSubSpec("Speciment.BizLogic.SubSpecManage.GetInLayer", new string[] { capId });
            }
            //������зż���
            if (cap == "J")
            {
                return this.GetSubSpec("Speciment.BizLogic.SubSpecManage.GetInShelf", new string[] { capId });
            }
            //�����ļ���
            if (cap == "IJ")
            {
                return this.GetSubSpec("Speciment.BizLogic.SubSpecManage.GetInShelf1", new string[] { capId });
            }
            return null;
        }

        /// <summary>
        /// ����ԭ�걾Id��ѯ��װ�걾�б�
        /// </summary>
        /// <param name="specId"></param>
        /// <returns></returns>
        public ArrayList GetSubSpecBySpecId(string specId)
        {
            return this.GetSubSpec("Speciment.BizLogic.SubSpecManage.GetBySpecID", new string[] { specId });
        }

        /// <summary>
        /// ����StoreId��ѯ��װ��ȡ��װ�걾
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public ArrayList GetSubSpecByStoreId(string storeId)
        {
            return this.GetSubSpec("Speciment.BizLogic.SubSpecManage.GetByStoreID", new string[] { storeId });
        }

        /// <summary>
        /// ��ָ����λ�ò�ѯ�걾
        /// </summary>
        /// <param name="boxId">�걾��ID</param>
        /// <param name="row">��</param>
        /// <param name="col">��</param>
        /// <returns></returns>
        public SubSpec GetSubSpecByLocate(string boxId, string row, string col)
        {
            ArrayList arr = this.GetSubSpec("Speciment.BizLogic.SubSpecManage.Locate", new string[] { boxId, row, col });
            if (arr == null || arr.Count <= 0)
            {
                return null;
            }
            return arr[0] as SubSpec;
        }

        /// <summary>
        /// ��ָ����λ�ò�ѯ�걾
        /// </summary>
        /// <param name="boxId">�걾��ID</param>
        /// <param name="endCol">ֹ��</param>
        /// <param name="endRow">ֹ��</param>
        /// <param name="startCol">����</param>
        /// <param name="startRow">����</param>
        /// <returns></returns>
        public SubSpec GetSubSpecByLocate(string boxId, string endCol, string endRow, string startCol, string startRow)
        {
            ArrayList arr = this.GetSubSpec("Speciment.BizLogic.SubSpecManage.LocateDetail", new string[] { boxId, endCol, endRow, startCol, startRow });
            if (arr == null || arr.Count <= 0)
            {
                return null;
            }
            return arr[0] as SubSpec;
        }

        /// <summary>
        /// ����sql��ȡ���������ķ�װ�걾�б�
        /// </summary>
        /// <param name="sql">sql���</param> 
        /// <returns></returns>
        public ArrayList GetSubSpec(string sql)
        {
            if (this.ExecQuery(sql, new string[] { }) == -1)
                return null;
            ArrayList arrSubSpec = new ArrayList();
            while (this.Reader.Read())
            {
                SubSpec subTmp = SetSubSpec();
                arrSubSpec.Add(subTmp);
            }
            this.Reader.Close();
            return arrSubSpec;
        }

        /// <summary>
        /// ��ȡ�걾��ר������
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetUseAlonePro(string storeId)
        {
            string sql = @"select ss.SUBBARCODE,ss.USER from SPEC_SUBSPEC ss where ss.STOREID = {0}";
            sql = string.Format(sql, storeId);
            DataSet ds = new DataSet();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                this.ExecQuery(sql, ref ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr[0] != null && dr[0].ToString() != "")
                    {
                        if (dr[1] != null && dr[1].ToString() != "")
                        {
                            dic.Add(dr[0].ToString(), dr[1].ToString());
                        }
                        else
                        {
                            dic.Add(dr[0].ToString(), "");
                        }
                    }
                }
            }
            catch
            { }
            finally
            {
                //this.Reader.Close();
            }
            return dic;
        }

        /// <summary>
        /// ���±걾ר������
        /// </summary>
        /// <param name="useAlonePro"></param>
        /// <returns></returns>
        public int UpdateUseAlonePro(Dictionary<string, string> useAlonePro)
        {
            string sql = @" update SPEC_SUBSPEC set USEALONE = '{0}',USER='{1}' where SUBBARCODE= '{2}'";

            try
            {
                foreach (KeyValuePair<string, string> vp in useAlonePro)
                {
                    if (vp.Value == "")
                    {
                        sql = string.Format(sql, "0", "", vp.Key);
                    }
                    else
                    {
                        sql = string.Format(sql, "1", vp.Value, vp.Key);
                    }

                    if (this.ExecNoQuery(sql) < 0)
                    {
                        return -1;
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 1;

        }
        #endregion
    }
}
