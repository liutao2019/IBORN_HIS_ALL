using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace Neusoft.HISFC.Integrate.PhysicalExamination
{
    class GroupDetail : IntegrateBase
    {
        #region ����
        //������׹�����
        protected static Neusoft.HISFC.Management.PhysicalExamination.GroupDetail mgrGroupDetail = new Neusoft.HISFC.Management.PhysicalExamination.GroupDetail();
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
            mgrGroupDetail.SetTrans(trans);
        }
        #endregion

        /// <summary>
        /// ���ݿ��ұ����ȡ������Ŀ��Ϣ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryGroupTailByDeptID(string deptCode)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.QueryGroupTailByDeptID(deptCode);
        }

        /// <summary>
        /// �õ��µ�ID
        /// </summary>
        /// <returns></returns>
        public string GetGroupID()
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.GetGroupID();
        }

        public ArrayList QueryGroupTailByGroupID(string groupID)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.QueryGroupTailByGroupID(groupID);
        }

        /// <summary>
        /// ����һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGroupTail(Neusoft.HISFC.Object.PhysicalExamination.GroupDetail info)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.InsertGroupTail(info);
        }

        /// <summary>
        /// �޸�һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroupTail(Neusoft.HISFC.Object.PhysicalExamination.GroupDetail info)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.UpdateGroupTail(info);
        }

        /// <summary>
        /// ɾ��һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroupTail(Neusoft.HISFC.Object.PhysicalExamination.GroupDetail info)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.DeleteGroupTail(info);
        }

        #endregion
    }
}
