using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �ƻ�ȡ�걾����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-21]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-19' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class SpecSourcePlanManage : FS.FrameWork.Management.Database
    {       
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specSource">�ƻ��걾����</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecSourcePlan specPlan)
        {
            //string sequence = "";
            //GetNextSequence(ref sequence);
            string[] str = new string[]
						{
							specPlan.PlanID.ToString(), 
							specPlan.SpecID.ToString(),
                            specPlan.SpecType.SpecTypeID.ToString(),
                            specPlan.Count.ToString(),
                            specPlan.Capacity.ToString(),
                            specPlan.Unit,
                            specPlan.ForSelfUse.ToString(),
                            specPlan.TumorType.ToString(),
                            specPlan.LimitUse.ToString(),
                            specPlan.TumorPos,
                            specPlan.SideFrom,
                            specPlan.TumorPor,
                            specPlan.BaoMoEntire,
                            specPlan.BreakPiece.ToString(),
                            specPlan.StoreTime.ToString(),
                            specPlan.StoreCount.ToString(),
                            specPlan.Comment,
                            specPlan.TransPos,
                            specPlan.Ext1,
                            specPlan.Ext2,
                            specPlan.Ext3
						};
            return str;
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

        #region ����SpecSourcePlan����

        private SpecSourcePlan SetSpecSource()
        {
            SpecSourcePlan specSourcePlan = new SpecSourcePlan();
            try
            {
                specSourcePlan.PlanID = Convert.ToInt32(this.Reader["SOTREID"].ToString());
                specSourcePlan.SpecID = Convert.ToInt32(this.Reader["SPECID"].ToString());
                if (!Reader.IsDBNull(2)) specSourcePlan.SpecType.SpecTypeID = Convert.ToInt32(this.Reader["SPECTYPEID"].ToString());//2
                else specSourcePlan.SpecType.SpecTypeID = 0;
                if (!Reader.IsDBNull(3)) specSourcePlan.Count = Convert.ToInt32(this.Reader["SUBCOUNT"].ToString());//3
                else specSourcePlan.Count = 0;
                if (!Reader.IsDBNull(4)) specSourcePlan.Capacity = Convert.ToDecimal(this.Reader["CAPACITY"].ToString());//4
                else specSourcePlan.Capacity = 0;
                if (!Reader.IsDBNull(5)) specSourcePlan.Unit = this.Reader["UNIT"].ToString();//5
                else specSourcePlan.Unit = "֧";
                if (!Reader.IsDBNull(6)) specSourcePlan.ForSelfUse = Convert.ToInt32(this.Reader["FORSELFUSE"].ToString());//6
                else specSourcePlan.ForSelfUse = 0;
                if (!Reader.IsDBNull(7)) specSourcePlan.TumorType = this.Reader["TUMORTYPE"].ToString();//7
                else specSourcePlan.TumorType = "";
                if (!Reader.IsDBNull(8)) specSourcePlan.LimitUse = this.Reader["LIMITUSE"].ToString();//8
                else specSourcePlan.LimitUse = "";
                if (!Reader.IsDBNull(9)) specSourcePlan.TumorPos = this.Reader["TUMORPOS"].ToString();//9
                else specSourcePlan.TumorPos = "";
                if (!Reader.IsDBNull(10)) specSourcePlan.SideFrom = this.Reader["SIDEFROM"].ToString();//10
                else specSourcePlan.SideFrom = "";
                if (!Reader.IsDBNull(11)) specSourcePlan.TumorPor = this.Reader["TUMORPOR"].ToString();//11
                else specSourcePlan.TumorPor = "";
                if (!Reader.IsDBNull(12)) specSourcePlan.BaoMoEntire = this.Reader["BAOMOENTIRE"].ToString();//12
                else specSourcePlan.BaoMoEntire ="";
                if (!Reader.IsDBNull(13)) specSourcePlan.BreakPiece = this.Reader["BREAKPIECE"].ToString();//13
                else specSourcePlan.BreakPiece = "";
                if (!Reader.IsDBNull(14)) specSourcePlan.StoreTime = Convert.ToDateTime(this.Reader["STORETIME"].ToString());//14
                else specSourcePlan.StoreTime = DateTime.MinValue;
                if (!Reader.IsDBNull(15)) specSourcePlan.StoreCount = Convert.ToInt32(this.Reader["SOTRECOUNT"].ToString());//15
                else specSourcePlan.StoreCount = 0;
                if (!Reader.IsDBNull(16)) specSourcePlan.Comment = this.Reader["MARK"].ToString();//16
                else specSourcePlan.Comment = "";
                if (!Reader.IsDBNull(17)) specSourcePlan.TransPos = this.Reader["TRANSPOS"].ToString();//16
                else specSourcePlan.TransPos = "";
                if (!Reader.IsDBNull(18)) specSourcePlan.Ext1 = this.Reader["EXT1"].ToString();//16
                else specSourcePlan.Ext1 = "";
                if (!Reader.IsDBNull(19)) specSourcePlan.Ext2 = this.Reader["EXT2"].ToString();//16
                else specSourcePlan.Ext2 = "";
                if (!Reader.IsDBNull(20)) specSourcePlan.Ext3 = this.Reader["EXT3"].ToString();//16
                else specSourcePlan.Ext3 = "";

            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return specSourcePlan;

        }

        /// <summary>
        /// ����SpecSourcePlan
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSpecSource(string sqlIndex, params string[] args)
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
        /// ����sql�������ҷ���������ԭ�걾
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns>ԭ�걾�б�</returns>
        private ArrayList GetSpecSourcePlan(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return null;
            }
            if (this.ExecQuery(sql, args) == -1)
                return null;
            ArrayList arrSpecSourcePlan = new ArrayList();
            while (this.Reader.Read())
            {
                SpecSourcePlan specTmpPlan = SetSpecSource();
                arrSpecSourcePlan.Add(specTmpPlan);
            }
            Reader.Close();
            return arrSpecSourcePlan;
        }
        #endregion

        #endregion

        #region ��������
        
        /// <summary>
        /// SpecSourcePlan����
        /// </summary>
        /// <param name="specBox">���������SpecSourcePlan</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertSourcePlan(FS.HISFC.Models.Speciment.SpecSourcePlan specSourcePlan)
        {
            return this.UpdateSpecSource("Speciment.BizLogic.SpecSourcePlanManage.Insert", this.GetParam(specSourcePlan));
        }

        public int UpdateSourcePlan(string sql)
        {
            return this.ExecNoQuery(sql);
        }

        public ArrayList GetSourcePlan(string sql)
        {
            if (this.ExecQuery(sql) == -1)
                return null;
            ArrayList arrSourcePlan = new ArrayList();
            while (this.Reader.Read())
            {
                SpecSourcePlan s = SetSpecSource();
                arrSourcePlan.Add(s);
            }
            this.Reader.Close();
            return arrSourcePlan;
        }

        /// <summary>
        /// ����Storeid
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public SpecSourcePlan GetPlanById(string storeId, string subSpecId )
        {
            ArrayList arr = new ArrayList();
            if (storeId != "")
            {
                arr = this.GetSpecSourcePlan("Speciment.BizLogic.SpecSourcePlanManage.QueryStoreById", new string[] { storeId });
            }
            if (subSpecId != "")
            {
                arr = this.GetSpecSourcePlan("Speciment.BizLogic.SpecSourcePlanManage.QueryStoreBySpecid", new string[] { subSpecId });
            }
            if (arr != null && arr.Count > 0)
            {
                return arr[0] as SpecSourcePlan;
            }
            else
            {
                return null;
            }
        }

       /// <summary>
       /// ���ݷ�װ����Ż�ȡ
       /// </summary>
       /// <param name="storeId"></param>
       /// <param name="subSpecId"></param>
       /// <returns></returns>
        public SpecSourcePlan GetPlanBySubBarCode(string subBarCode)
        {
            ArrayList arr = new ArrayList();

            arr = this.GetSpecSourcePlan("Speciment.BizLogic.SpecSourcePlanManage.QueryStoreBySubSpecBarCode", new string[] { subBarCode });

            try
            {
                return arr[0] as SpecSourcePlan;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// �ϲ�ArrayList�о�����ͬ���������õı걾
        /// </summary>
        /// <param name="arrSpecPlan"></param>
        /// <returns></returns>
        public Dictionary<SpecSourcePlan, List<StoreInfo>> ParseSourcePlan(ArrayList arrSpecPlan)
        {
            Dictionary<SpecSourcePlan, List<StoreInfo>> dicMergeResult = new Dictionary<SpecSourcePlan, List<StoreInfo>>();
            List<KeyValuePair<SpecSourcePlan, List<StoreInfo>>> listTmp = new List<KeyValuePair<SpecSourcePlan, List<StoreInfo>>>();
          
            int i = 0;
            foreach (SpecSourcePlan p in arrSpecPlan)
            {
                List<StoreInfo> tmpStore = new List<StoreInfo>();                
                StoreInfo store = new StoreInfo();
                store.Count = p.Count;
                store.LimitUse = p.LimitUse;
                store.SpecTypeId = p.SpecType.SpecTypeID;
                store.StoreId = p.PlanID;
                if (i == 0)
                {
                    tmpStore.Add(store);
                    KeyValuePair<SpecSourcePlan, List<StoreInfo>> tmpPair = new KeyValuePair<SpecSourcePlan, List<StoreInfo>>(p,tmpStore);
                    listTmp.Add(tmpPair);
                    i++;
                    continue;
                }
                for (int k = listTmp.Count - 1; k >= -1; k--)
                {
                    if (k == -1)
                    {
                        tmpStore.Add(store);
                        KeyValuePair<SpecSourcePlan, List<StoreInfo>> tmpPair = new KeyValuePair<SpecSourcePlan, List<StoreInfo>>(p, tmpStore);
                        listTmp.Add(tmpPair);
                        break;
                    }
                    KeyValuePair<SpecSourcePlan, List<StoreInfo>> item = listTmp[k];
                    if (p.ChkFromSamePro(item.Key))
                    {
                        if (item.Value.Contains(store))
                            break;
                        else
                        {
                            item.Value.Add(store);
                            break;
                        }
                    }
                    //if (!p.ChkFromSamePro(item.Key))
                    //{
                    //    tmpStore.Add(store);
                    //    KeyValuePair<SpecSourcePlan, List<StoreInfo>> tmpPair = new KeyValuePair<SpecSourcePlan, List<StoreInfo>>(p, tmpStore);
                    //    listTmp.Add(tmpPair);
                    //    break;
                    //}
                }
                i++;
            }
            foreach (KeyValuePair<SpecSourcePlan, List<StoreInfo>> tmp in listTmp)
            {
                dicMergeResult.Add(tmp.Key,tmp.Value);
            }
            return dicMergeResult; 
        }

        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public string GetNextSequence()
        {
            //
            // ִ��SQL���
            //
            return this.GetSequence("Speciment.BizLogic.SpecSourcePlanManage.GetNextSequence");
        }

        public int UpdateSpecPlan(SpecSourcePlan sp)
        {
            return this.UpdateSpecSource("Speciment.BizLogic.SpecSourcePlanManage.Update", GetParam(sp));
        }

        #endregion
    }
    public class StoreInfo
    {
        private int specTypeId = 0;
        private string specTypeName = "";
        private int count = 0;
        private string limitUse = "";
        private int storeId = 0;

        public int SpecTypeId
        {
            get
            {
                return specTypeId;
            }
            set
            {
                specTypeId = value;
            }
        }

        public string SpecTypeName
        {
            get
            {
                return specTypeName;
            }
            set
            {
                specTypeName = value;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        public string LimitUse
        {
            get
            {
                return limitUse;
            }
            set
            {
                limitUse = value;
            }
        }

        public int StoreId
        {
            get
            {
                return storeId;
            }
            set
            {
                storeId = value;
            }
        }
    }
    
}
