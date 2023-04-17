using System;
using System.Collections.Generic;
using System.Collections;

namespace Neusoft.HISFC.Integrate.PhysicalExamination
{
    class Group : IntegrateBase
    {
        #region ����
        //������׹�����
        protected static Neusoft.HISFC.Management.PhysicalExamination.Group mgrGroup = new Neusoft.HISFC.Management.PhysicalExamination.Group();
        #endregion

        #region ���к���
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            mgrGroup.SetTrans(trans);
        }
         #endregion
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllGroups()
        {
            this.SetDB(mgrGroup);
            return mgrGroup.QueryAllGroups();
        }

        /// <summary>
        /// ��������ID��ȡ������Ϣ
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Object.PhysicalExamination.Group GetGroupByGroupID(string groupID)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.GetGroupByGroupID(groupID);
        }

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGroup(Neusoft.HISFC.Object.PhysicalExamination.Group info)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.InsertGroup(info);
        }

        /// <summary>
        /// �޸�һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroup(Neusoft.HISFC.Object.PhysicalExamination.Group info)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.UpdateGroup(info);
        }

        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public int DeleteGroup(Neusoft.HISFC.Object.PhysicalExamination.Group info)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.DeleteGroup(info);
        }

        /// <summary>
        /// ɾ������������ϸ
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int DelGroupDetails(string groupID)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.DelGroupDetails(groupID);
        }

        /// <summary>
        /// �����һ�ȡ������Ч����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryValidGroupList(string deptID)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.QueryValidGroupList(deptID);
        }

        /// <summary>
        /// �����һ�ȡ������Ч����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllGroupListByDeptID(string deptID)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.QueryAllGroupListByDeptID(deptID);
        }

        #endregion 
    }
}
