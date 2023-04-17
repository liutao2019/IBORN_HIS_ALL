using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Integrate.HealthRecord.Visit
{
    /// <summary>
    /// [��������: ��ü�������ҵ�������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-9-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class VisitSearches : IntegrateBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public VisitSearches()
        {
            
        }

        

        #region ����

        /// <summary>
        /// ��ü������������
        /// </summary>
        protected static FS.HISFC.BizLogic.HealthRecord.Visit.VisitSearches visitSearchesManager = new FS.HISFC.BizLogic.HealthRecord.Visit.VisitSearches();

        /// <summary>
        /// ����������
        /// </summary>
        protected static FS.HISFC.BizLogic.Manager.Constant constFunction = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region ����

        /// <summary>
        /// �����������в���һ���µļ�¼
        /// </summary>
        /// <param name="visitSearches">��ü�������ʵ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int InsertVisitSeaches(FS.HISFC.Models.HealthRecord.Visit.VisitSearches visitSearches)
        {
            this.SetDB(visitSearchesManager);

            int intReturn = visitSearchesManager.Insert(visitSearches);
            if (intReturn < 0)
            {
                this.Err = visitSearchesManager.Err;

                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// �޸���ü��������¼
        /// </summary>
        /// <param name="visitSearches">��ü�������ʵ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int UpdateVisitSearches(FS.HISFC.Models.HealthRecord.Visit.VisitSearches visitSearches)
        {
            this.SetDB(visitSearchesManager);

            int intReturn = visitSearchesManager.Update(visitSearches);
            if (intReturn < 0)
            {
                this.Err = visitSearchesManager.Err;

                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ����������ˮ��ɾ��һ����ü��������¼
        /// </summary>
        /// <param name="applyID">��ü���������ˮ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int DeleteVisitSearches(string applyID)
        {
            this.SetDB(visitSearchesManager);

            int intReturn = visitSearchesManager.Delete(applyID);
            if (intReturn < 0)
            {
                this.Err = visitSearchesManager.Err;

                return -1;
            }

            return intReturn;
        }

        /// <summary>
        /// ͨ��״̬������������¼
        /// </summary>
        /// <param name="searchesStat">����״̬</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList QueryBYDocCode(string docCode, string searchesStat)
        {
            this.SetDB(visitSearchesManager);

            ArrayList temp = visitSearchesManager.QueryByDocCode(docCode, searchesStat);
            if (temp == null)
            {
                this.Err = visitSearchesManager.Err;

                return null;
            }

            return temp;
        }

        /// <summary>
        /// ͨ��ִ��SQL��䣬����ѯ������Ϣ����ArrayList��
        /// </summary>
        /// <param name="strSQL">��Ҫִ�е�SQL���</param>
        /// <returns>���ض�ȡ�������顢���󷵻�null</returns>
        public ArrayList QueryByStat(string searches)
        {
            this.SetDB(visitSearchesManager);

            ArrayList temp = visitSearchesManager.QueryByStat(searches);
            if (temp == null)
            {
                this.Err = visitSearchesManager.Err;

                return null;
            }

            return temp;
        }

        /// <summary>
        /// ͨ��������ˮ�ż�����������¼
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        public ArrayList QueryByApplyId(string applyId)
        {
            this.SetDB(visitSearchesManager);

            ArrayList temp = visitSearchesManager.QueryByApplyId(applyId);
            if (temp == null)
            {
                this.Err = visitSearchesManager.Err;

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
        public DateTime GetSystemTime()
        {
            return visitSearchesManager.GetDateTimeFromSysDateTime();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;

            visitSearchesManager.SetTrans(trans);
            constFunction.SetTrans(trans);
        }

        #endregion
    }
}
