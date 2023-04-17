using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizProcess.Integrate.HealthRecord.Visit
{
    /// <summary>
    /// [��������: ���ҵ�������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-8-22]<br></br>
    ///  <�޸ļ�¼
    ///		�޸���=���'
    ///     ��������,���������ϸ,�������������¼
    ///     {E9F858A6-BDBC-4052-BA57-68755055FB80}
    ///     '
    ///		�޸�ʱ��='2009-09-21'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Visit : IntegrateBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public Visit()
        {
            
        }
     

        #region ����

        /// <summary>
        /// �������¼������
        /// </summary>
        protected static FS.HISFC.BizLogic.HealthRecord.Visit.Visit visitManager = new FS.HISFC.BizLogic.HealthRecord.Visit.Visit();

        /// <summary>
        /// �����ϸ������
        /// </summary>
        protected static FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord visitRecordManager = new FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord();

        /// <summary>
        /// ����������
        /// </summary>
        protected static FS.HISFC.BizLogic.Manager.Constant constFunction = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ��ϵ��ʽ������
        /// </summary>
        protected static FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay linkWayManager = new FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay();

        #endregion    

        #region �������¼

        /// <summary>
        /// ����һ���������¼
        /// </summary>
        /// <param name="visit">�������¼ʵ����</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int InsertVisit(FS.HISFC.Models.HealthRecord.Visit.Visit visit)
        {
            this.SetDB(visitManager);

            int intReturn = visitManager.Insert(visit);
            if (intReturn < 0)
            {
                this.Err = visitManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// �����������¼
        /// </summary>
        /// <param name="visit">�������¼ʵ����</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int UpdateVisit(FS.HISFC.Models.HealthRecord.Visit.Visit visit)
        {
            this.SetDB(visitManager);

            int intReturn = visitManager.Update(visit);
            if (intReturn < 0)
            {
                this.Err = visitManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ��ĳ�����ߵ����״̬��Ϊֹͣ���
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>Ӱ���������-1��ʧ��</returns>
        public int UpdateVisitStat(string cardNo)
        {
            this.SetDB(visitManager);

            int intReturn = visitManager.UpdateStat(cardNo);
            if (intReturn != 1)
            {
                this.Err = visitManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ���ݲ����Ų�ѯ�������¼
        /// </summary>
        /// <param name="visit">�������¼ʵ����</param>
        /// <param name="cardNo">������</param>
        /// <returns>1 ��ѯ�������0 û�в�ѯ�������-1 ʧ��</returns>   
        public int GetVisitInfo(ref FS.HISFC.Models.HealthRecord.Visit.Visit visit, string cardNo)
        {
            this.SetDB(visitManager);

            int intReturn = visitManager.Select(ref visit, cardNo);
            if (intReturn < 0)
            {
                this.Err = visitManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ���벡�����жϸû����Ƿ��Ѿ�ֹͣ��� ����ҽ��վ��ʾҽ��¼�������Ϣʹ�ã�
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>-1 ʧ�ܡ�0 ֹͣ��á�1 �������</returns>
        public int IsVisitStop(string cardNo)
        {
            this.SetDB(visitManager);

            int intReturn = visitManager.IsVisitStop(cardNo);
            if (intReturn == -1)
            {
                this.Err = visitManager.Err;

                return -1;
            }

            return intReturn;
        }
        #endregion

        #region ��ϵ��ʽ

        /// <summary>
        /// ����һ����ϵ��ʽ��¼
        /// </summary>
        /// <param name="linkway">��ϵ��ʽʵ����</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int InsertLinkWay(FS.HISFC.Models.HealthRecord.Visit.LinkWay linkway)
        {
            this.SetDB(linkWayManager);

            int intReturn = linkWayManager.Insert(linkway);
            if (intReturn < 0)
            {
                this.Err = linkWayManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ɾ��һ����ϵ��ʽ��¼
        /// </summary>
        /// <param name="linkWayID">��ϵ��ʽ��ˮ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int DeleteLinkWayByLinkID(string linkWayID)
        {
            this.SetDB(linkWayManager);

            int intReturn = linkWayManager.DeleteByLinkWayID(linkWayID);
            if (intReturn < 0)
            {
                this.Err = linkWayManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ɾ��ĳ�����ߵ�������ϵ��ʽ
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int DeleteLinkWayByCardNo(string cardNo)
        {
            this.SetDB(linkWayManager);

            int intReturn = linkWayManager.DeleteByCardNo(cardNo);
            if (intReturn < 0)
            {
                this.Err = linkWayManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ���ݵ绰���뻼�ߵ���ϵ��ʽ
        /// </summary>
        /// <param name="Phone">�绰����</param>
        /// <returns>�������顢ʧ�ܷ���null</returns>
        public ArrayList GetLinkWayByPhone(string phone)
        {
            this.SetDB(linkWayManager);

            ArrayList temp = new ArrayList();
            temp = linkWayManager.SelectByPhone(phone);
            if (temp == null)
            {
                this.Err = linkWayManager.Err;
                return null;
            }

            return temp;
        }

        /// <summary>
        /// ����������ѯ��ϵ��ʽ
        /// </summary>
        /// <param name="Phone">����</param>
        /// <returns>�������顢ʧ�ܷ���null</returns>
        public ArrayList GetLinkWayByName(string name)
        {
            this.SetDB(linkWayManager);

            ArrayList temp = new ArrayList();
            temp = linkWayManager.SelectByName(name);
            if (temp == null)
            {
                this.Err = linkWayManager.Err;
                return null;
            }

            return temp;
        }

        /// <summary>
        /// ���ݵ�ַ��ѯ��ϵ��ʽ
        /// </summary>
        /// <param name="Phone">��ַ</param>
        /// <returns>�������顢ʧ�ܷ���null</returns>
        public ArrayList GetLinkWayByAddress(string address)
        {
            this.SetDB(linkWayManager);

            ArrayList temp = new ArrayList();
            temp = linkWayManager.SelectByAddress(address);
            if (temp == null)
            {
                this.Err = linkWayManager.Err;
                return null;
            }

            return temp;
        }

        /// <summary>
        /// ���ݲ����Ŷ�ȡ���ߵ���ϵ��ʽ
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>�������顢ʧ�ܷ���null</returns>
        public ArrayList GetLinkWayByCard(string cardNo)
        {
            this.SetDB(linkWayManager);

            ArrayList temp = new ArrayList();
            temp = linkWayManager.SelectByCardNo(cardNo);
            if (temp == null)
            {
                this.Err = linkWayManager.Err;
                return null;
            }

            return temp;
        }

        #endregion

        #region �����ϸ

        /// <summary>
        /// ������ü�¼��ϸ
        /// </summary>
        /// <param name="visitRecord">�����ϸ��¼</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int InsertVisitRecordInfo(FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord, string sequ)
        {
            this.SetDB(visitRecordManager);

            int intReturn = visitRecordManager.Insert(visitRecord, sequ);
            if (intReturn < 0)
            {
                this.Err = visitRecordManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ��ȡ�����ϸ��ˮ��
        /// </summary>
        /// <returns>��ˮ��</returns>
        public string GetVisitRecordSequ()
        {
            this.SetDB(visitRecordManager);

            string strReturn = visitRecordManager.GetVisitRecordSequ();
            if (strReturn == null)
            {
                this.Err = visitRecordManager.Err;

                return null;
            }

            return strReturn;
        }

        /// <summary>
        /// ������ü�¼��ϸ
        /// </summary>
        /// <param name="visitRecord">�����ϸ��¼</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int UpdateVisitRecord(FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord)
        {
            this.SetDB(visitRecordManager);

            int intReturn = visitRecordManager.Update(visitRecord);
            if (intReturn < 0)
            {
                this.Err = visitRecordManager.Err;
                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ��ѯ�����ϸ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="type">�Ƿ��н��</param>
        /// <param name="cardNo">������</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList GetVisitRecordInfo(DateTime beginTime, DateTime endTime, char type, string cardNo)
        {
            this.SetDB(visitRecordManager);

            ArrayList temp = new ArrayList();
            temp = visitRecordManager.Select(beginTime, endTime, type, cardNo);
            if (temp == null)
            {
                this.Err = visitRecordManager.Err;
                return null;
            }

            return temp;
        }

        /// <summary>
        /// ������ü�¼��ϸIDɾ�������е�֢״���ּ�¼
        /// </summary>
        /// <param name="recordID">��ü�¼��ϸID</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int DeleteSymptom(int recordID)
        {
            this.SetDB(visitRecordManager);

            int intReturn = visitRecordManager.DeleteSymptom(recordID);
            if (intReturn < 0)
            {
                this.Err = visitRecordManager.Err;

                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// �������ϸ֢״���ֱ��в���һ���¼�¼,����ü�¼��ϸ��ID���ڳ���ʵ���SortID��
        /// </summary>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int InsertSymptom(FS.HISFC.Models.Base.Const symptom)
        {
            this.SetDB(visitRecordManager);

            int intReturn = visitRecordManager.InsertSymptom(symptom);
            if (intReturn <= 0)
            {
                this.Err = visitRecordManager.Err;

                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ������ü�¼��ϸID��ȡ�����е�֢״����
        /// </summary>
        /// <param name="recordID">��ü�¼��ϸID</param>
        /// <returns>���ز�ѯ�����顢ʧ�ܷ���null</returns>
        public ArrayList GetSymptom(int recordID)
        {
            this.SetDB(visitRecordManager);

            ArrayList temp = visitRecordManager.SelectSymptom(recordID);
            if (temp == null)
            {
                this.Err = visitRecordManager.Err;

                return null;
            }

            return temp;
        }

        #endregion

        #region ��������

        /// <summary>
        /// ��ȡϵͳ��ǰʱ��
        /// </summary>
        /// <returns>ϵͳ��ǰʱ��</returns>
        public DateTime GetCurrentDateTime()
        {
            return visitManager.GetDateTimeFromSysDateTime();
        }

        /// <summary>
        /// ���ֵ���л�ȡ���е���÷�ʽ��ϵ�����ȵȣ����ڳ�ʼ��
        /// </summary>
        /// <param name="constType">��������</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList GetVisitConst(string constType)
        {
            this.SetDB(constFunction);

            ArrayList temp = new ArrayList();
            temp = constFunction.GetList(constType);
            if (temp == null)
            {
                this.Err = constFunction.Err;
                return null;
            }

            return temp;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;

            visitManager.SetTrans(trans);
            constFunction.SetTrans(trans);
            visitRecordManager.SetTrans(trans);
            linkWayManager.SetTrans(trans);
        }

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        /// <summary>
        /// ��������,���������ϸ,�������������¼
        /// </summary>
        /// <param name="visitRecord">�����ϸʵ��</param>
        /// <param name="sequ">��ü�¼ΨһID</param>
        /// <param name="visit">�������¼ʵ��</param>
        /// <returns>�ɹ����� 0;ʧ�ܷ��� -1;</returns>
        public int InsertAndUpdateVisit(FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord
            , string sequ, FS.HISFC.Models.HealthRecord.Visit.Visit visit)
        {
            if (InsertVisitRecordInfo(visitRecord, sequ) == -1)
            {
                return -1;
            }
            if (UpdateVisit(visit) == -1)
            {
                return -1;
            }
            return 0;
        }

        
        #endregion

       

        #endregion
    }
}
