using System;
using System.Collections;
using System.Data;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// PactStatRelation ��ժҪ˵����
	/// </summary> 
    public class PactStatRelation : DataBase
    {
        public PactStatRelation()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        /// <summary>
        /// ��ȡ��ͬ��λ��ͳ�ƴ����ϵ��
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.PactStatRelation GetItem(string pactID, string ItemCode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Manager.Pactstatrelation.GetItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.Pactstatrelation.GetItem�ֶ�!";
                return null;
            }

            string strWhere = "";
            //ȡWHERE���
            if (this.GetSQL("Manager.Pactstatrelation.GetItem.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Manager.Pactstatrelation.GetItem.Where�ֶ�!";
                return null;
            }

            //��ʽ��SQL���
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, pactID, ItemCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.Pactstatrelation.GetItem.Where:" + ex.Message;
                return null;
            }

            //ȡ��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ
            ArrayList al = this.myGetItem(strSQL);
            if (al == null)
                return null;

            if (al.Count == 0)
                return new FS.HISFC.Models.Base.PactStatRelation();

            return al[0] as FS.HISFC.Models.Base.PactStatRelation;
        }


        /// <summary>
        /// ȡ��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ�б�
        /// </summary>
        /// <returns>��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ���飬������null</returns>
        public ArrayList GetItemList()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Manager.Pactstatrelation.GetItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.Pactstatrelation.GetItem�ֶ�!";
                return null;
            }

            //ȡ��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ����
            return this.myGetItem(strSQL);
        }


        /// <summary>
        /// ���ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ���в���һ����¼
        /// </summary>
        /// <param name="Item">������չ������</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertItem(FS.HISFC.Models.Base.PactStatRelation Item)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.GetSQL("Manager.Pactstatrelation.InsertItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.Pactstatrelation.InsertItem�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(Item);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.Pactstatrelation.InsertItem:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }



        /// <summary>
        /// ���ͬ��λ���ձ��в���һ����¼(��������ķ������������˶���)
        /// </summary>
        /// <param name="Item">������չ������</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertItem(FS.HISFC.Models.Base.PactCompare Item)
        {
            string strSQL = "";
            //ȡ���������SQL���
            if (this.GetSQL("Manager.PactCompare.InsertItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.PactCompare.InsertItem�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(Item);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.PactCompare.InsertItem:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ���º�ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ����һ����¼
        /// </summary>
        /// <param name="Item">������չ������</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateItem(FS.HISFC.Models.Base.PactStatRelation Item)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Manager.Pactstatrelation.UpdateItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.Pactstatrelation.UpdateItem�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(Item);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.Pactstatrelation.UpdateItem:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        //��������ķ�����ֻ�ǻ��˴���Ĳ���
        public int UpdateItem(FS.HISFC.Models.Base.PactCompare Item)
        {
            string strSQL = "";
            //ȡ���²�����SQL���
            if (this.GetSQL("Manager.PactCompare.UpdateItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.PactCompare.UpdateItem�ֶ�!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmItem(Item);     //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);            //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.PactCompare.UpdateItem:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }



        /// <summary>
        /// ɾ����ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ����һ����¼
        /// </summary>
        /// <param name="ID">��ˮ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int DeleteItem(string ID)
        {
            string strSQL = "";
            //ȡɾ��������SQL���
            if (this.GetSQL("Manager.Pactstatrelation.DeleteItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.Pactstatrelation.DeleteItem�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.Pactstatrelation.DeleteItem:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ɾ����ͬ��λ���ձ����һ����¼
        /// </summary>
        /// <param name="ID">��ͬ��λ����</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int DeleteCompareItem(string ID)
        {
            string strSQL = "";
            //ȡɾ��������SQL���
            if (this.GetSQL("Manager.PactCompare.DeleteItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.PactCompare.DeleteItem�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.PactCompare.DeleteItem:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }





        /// <summary>
        /// ������Ա���Ա䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="Item">��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣʵ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int SetItem(FS.HISFC.Models.Base.PactStatRelation Item)
        {
            int parm;
            //ִ�и��²���
            parm = UpdateItem(Item);

            //���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (parm == 0)
            {
                parm = InsertItem(Item);
            }
            return parm;
        }


        public int SetItem(FS.HISFC.Models.Base.PactCompare Item)
        {
            int parm;
            //ִ�и��²���
            parm = UpdateItem(Item);

            //���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (parm == 0)
            {
                parm = InsertItem(Item);
            }
            return parm;
        }


        /// <summary>
        /// ȡ��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ�б�������һ�����߶���
        /// ˽�з����������������е���
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ��Ϣ��������</returns>
        private ArrayList myGetItem(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.PactStatRelation Item; //��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣʵ��
            //ִ�в�ѯ���
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "��ú�ͬ��λ��ͳ�ƴ�����Ŀ��Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //ȡ��ѯ����еļ�¼
                    Item = new FS.HISFC.Models.Base.PactStatRelation();
                    Item.ID = this.Reader[0].ToString();  // ��Ŀ����
                    Item.Group.ID = this.Reader[1].ToString();  // ��Ŀ����
                    Item.Pact.ID = this.Reader[2].ToString();  // ��Ŀ����
                    Item.Pact.Name = this.Reader[3].ToString();  // ��Ŀ����
                    Item.StatClass.ID = this.Reader[4].ToString();  // ��Ŀ����
                    //Item.StatClass.ID  = this.Reader[4].ToString() ;  // ��Ŀ����
                    //Item.Name  = this.Reader[5].ToString() ;  // ��Ŀ���� del xf
                    Item.StatClass.Name = this.Reader[5].ToString();  // ��Ŀ����
                    Item.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);  // ��Ŀ����
                    Item.Quota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);  // ��Ŀ����
                    Item.DayQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);  // ��Ŀ����
                    Item.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);  // ��Ŀ����
                    al.Add(Item);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ú�ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ��Ϣʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            this.Reader.Close();

            this.ProgressBarValue = -1;
            return al;
        }


        /// <summary>
        /// ���update����insert��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ��Ĵ����������
        /// </summary>
        /// <param name="Item">��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] myGetParmItem(FS.HISFC.Models.Base.PactStatRelation Item)
        {
            string[] strParm ={   
								 Item.ID,
								 Item.Group.ID,
								 Item.Pact.ID,
								 Item.Pact.Name,
								 Item.StatClass.ID,
								 Item.StatClass.Name,
								 Item.BaseCost.ToString(),
								 Item.Quota.ToString(),
								 Item.DayQuota.ToString(),
								 Item.SortID.ToString(),
								 this.Operator.ID
							 };
            return strParm;
        }

        /// <summary>
        /// ���update����insert��ͬ��λ���ձ�Ĵ����������
        /// </summary>
        /// <param name="Item">��ͬ��λ������Ϣʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] myGetParmItem(FS.HISFC.Models.Base.PactCompare Item)
        {
            string[] strParm ={   
								 Item.PactCode,
								 Item.PactHead,
                                 Item.PactName,
								 Item.ParentPact,
								 Item.ParentName,
								 Item.PactFlag,
								 Item.PayKind.ID,
								 Item.ValldState,
								 Item.SortID.ToString(),
								 this.Operator.ID
							 };
            return strParm;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public string GetPactSequence()
        {
            return this.GetSequence("SELECT SEQ_COM_PACTSTATRELATION.NEXTVAL FROM DUAL");
        }
        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <returns></returns>
        public ArrayList GetFeeCodeState()
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Manager.Pactstatrelation.GetFeeCodeState", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.Pactstatrelation.GetFeeCodeState�ֶ�!";
                return null;
            }
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject Item; //��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣʵ��
            //ִ�в�ѯ���
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "���ͳ�ƴ�����Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //ȡ��ѯ����еļ�¼
                    Item = new FS.FrameWork.Models.NeuObject();
                    Item.ID = this.Reader[0].ToString();  // ͳ�Ʊ���
                    Item.Name = this.Reader[1].ToString();  // ͳ������
                    al.Add(Item);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "���ͳ�ƴ�����Ϣ��Ϣʱ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            this.Reader.Close();
            return al;
        }
        /// <summary>
        /// ��ú�ͬ��λ�µ��ײ����ƽ��
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="groupID">�ײͺ�</param>
        /// <param name="statRelationDataSet">��ͬ��λ�µ��ײ����ƽ���</param>
        /// <returns></returns>
        public int GetStatRelation(string pactCode, string groupID, ref DataSet statRelationDataSet)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Manager.PactStatRelation.GetStatRelation.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.PactStatRelation.GetStatRelation.Select�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, pactCode, groupID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.PactStatRelation.GetStatRelation.Select:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecQuery(strSQL, ref statRelationDataSet);
        }

        /// <summary>
        /// ��ú�ͬ��λ�µ��ײ����ƽ��(���ּӼ�����ͨ)
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="groupID"></param>
        /// <param name="isEmergency"></param>
        /// <returns></returns>
        public string GetStatRelation(string pactCode, string groupID, bool isEmergency)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Manager.PactStatRelation.GetStatRelation.Select.IsEmergency", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.PactStatRelation.GetStatRelation.Select�ֶ�!";
                return "-1";
            }
            try
            {
                strSQL = string.Format(strSQL, pactCode, groupID, FS.FrameWork.Function.NConvert.ToInt32(isEmergency));    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.PactStatRelation.GetStatRelation.Select:" + ex.Message;
                this.WriteErr();
                return "-1";
            }
            return this.ExecSqlReturnOne(strSQL,"200");
        }

        /// <summary>
        /// ��ú�ͬ��λ�µ������ײ�
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>��ͬ��λ�µ������ײͼ���</returns>
        public ArrayList GetRelationByPactCode(string pactCode)
        {
            string strSQL = "";
            //ȡSELECT���
            if (this.GetSQL("Manager.Pactstatrelation.GetItem", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Manager.PactStatRelation.GetItem�ֶ�!";
                return null;
            }

            string strWhere = "";
            //ȡWHERE���
            if (this.GetSQL("Manager.PactStatRelation.GetRelationByPactCode.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Manager.PactStatRelation.GetRelationByPactCode.Where�ֶ�!";
                return null;
            }
            //��ʽ��SQL���
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, pactCode);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Manager.PactStatRelation.GetRelationByPactCode.Where:" + ex.Message;
                return null;
            }

            //ȡ��ͬ��λ��ͳ�ƴ�����Ŀ��Ϣ
            return this.myGetItem(strSQL);
        }

    }
}
