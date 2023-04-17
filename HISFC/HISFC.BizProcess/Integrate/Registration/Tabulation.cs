using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizProcess.Integrate.Registration
{
    public class Tabulation : IntegrateBase
    {
        /// <summary>
        /// �Ű������
        /// </summary>
        protected FS.HISFC.BizLogic.Operation.Tabulation tabulationManager = new FS.HISFC.BizLogic.Operation.Tabulation();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            tabulationManager.SetTrans(trans);
        }

        #region  ����

        #region ����ɾ����
        /// <summary>
        /// ����������_goa_med_worktype
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.WorkType type)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Insert(type);
        }
        /// <summary>
        /// ����Ƴ��ó������_goa_med_depttype,�������ʵ��,ʵ��ֻҪ��ֵid,OperID
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int Insert(string deptID, FS.HISFC.Models.Registration.WorkType type)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Insert(deptID, type);
        }
        /// <summary>
        /// �����Ű�_goa_med_tabulation
        /// </summary>
        /// <param name="tabular"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.Tabulation tabular)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Insert(tabular);
        }
        /// <summary>
        /// ɾ���Ƴ��ó������
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Delete(string deptID, string ID)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Delete(deptID, ID);
        }
        /// <summary>
        /// ɾ���������
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Delete(string ID)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Delete(ID);
        }
        /// <summary>
        /// �����Ű����ɾ���Ű��¼
        /// </summary>
        /// <param name="arrangeID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public int DeleteTabular(string arrangeID, string deptID)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.DeleteTabular(arrangeID, deptID);
        }
        #endregion

        #region ��ѯ
        /// <summary>
        /// ��������Ų�ѯ�Ű���Ϣ
        /// </summary>
        /// <param name="arrangeID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList Query(string arrangeID, string deptID)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Query(arrangeID, deptID);
        }
        /// <summary>
        /// ���������ڡ����Ҳ�ѯ�Ű���Ϣ
        /// </summary>
        /// <param name="workDate"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public ArrayList Query(DateTime workDate, FS.FrameWork.Models.NeuObject dept)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Query(workDate, dept);
        }
        /// <summary>
        /// ��ʱ��Ρ����Ҳ�ѯ�Ű����
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList Query(DateTime beginDate, string deptID)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Query(beginDate, deptID);
        }

        /// <summary>
        /// ��ѯ�Ƴ��ó������
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList Query(string deptID)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Query(deptID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="deptcode"></param>
        /// <param name="classcode"></param>
        /// <returns></returns>
        public ArrayList QueryTabular(DateTime begin, DateTime end, string deptcode, string classcode)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.QueryTabular(begin, end, deptcode, classcode);
        }


        /// <summary>
        /// ��ѯȫ������Ч����Ч�������
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ArrayList Query(FS.HISFC.BizLogic.Operation.QueryState state)
        {
            this.SetDB(tabulationManager);
            return tabulationManager.Query(state);
        } 

        #endregion
        #endregion 
    }
}
