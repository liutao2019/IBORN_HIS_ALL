using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizProcess.Integrate.PhysicalExamination
{

    public class ExamiManager : IntegrateBase
    {
        #region ����
        //��쵥λ������
        protected FS.HISFC.BizLogic.PhysicalExamination.Company mgrCompany = new FS.HISFC.BizLogic.PhysicalExamination.Company();
        //������׹�����
        protected FS.HISFC.BizLogic.PhysicalExamination.Group mgrGroup = new FS.HISFC.BizLogic.PhysicalExamination.Group();
        //������׹�����
        protected FS.HISFC.BizLogic.PhysicalExamination.GroupDetail mgrGroupDetail = new FS.HISFC.BizLogic.PhysicalExamination.GroupDetail();
        //������׹�����
        protected FS.HISFC.BizLogic.PhysicalExamination.Register mgrReg = new FS.HISFC.BizLogic.PhysicalExamination.Register();
        #endregion

        #region ��������
         /// <summary>
        /// ����
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            mgrGroup.SetTrans(trans);
            mgrCompany.SetTrans(trans);
            mgrGroupDetail.SetTrans(trans);
            mgrReg.SetTrans(trans);
        }
         
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
        /// <param name="groupID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.PhysicalExamination.Group GetGroupByGroupID(string groupID)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.GetGroupByGroupID(groupID);
        }

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGroup(FS.HISFC.Models.PhysicalExamination.Group info)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.InsertGroup(info);
        }

        /// <summary>
        /// �޸�һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroup(FS.HISFC.Models.PhysicalExamination.Group info)
        {
            this.SetDB(mgrGroup);
            return mgrGroup.UpdateGroup(info);
        }

        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroup(FS.HISFC.Models.PhysicalExamination.Group info)
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

        #region ���������ϸ��
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
        public int InsertGroupTail(FS.HISFC.Models.PhysicalExamination.GroupDetail info)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.InsertGroupTail(info);
        }

        /// <summary>
        /// �޸�һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroupTail(FS.HISFC.Models.PhysicalExamination.GroupDetail info)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.UpdateGroupTail(info);
        }

        /// <summary>
        /// ɾ��һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroupTail(FS.HISFC.Models.PhysicalExamination.GroupDetail info)
        {
            this.SetDB(mgrGroupDetail);
            return mgrGroupDetail.DeleteGroupTail(info);
        }
        #endregion 

        #region ��쵥λά��
        #region ��ѯ���е���쵥λ��Ϣ ���ض�̬����
        /// <summary>
        /// ��ѯ���е���쵥λ��Ϣ ���ض�̬����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryCompany()
        {
            this.SetDB(mgrCompany);
            return mgrCompany.QueryCompany();
        }
        #endregion

        #region ��ѯĳ��ID����쵥λ��Ϣ
        /// <summary>
        /// ��ѯĳ��ID����쵥λ��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.Company GetCompanyByID(string ID)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.GetCompanyByID(ID);
        }
        #endregion

        #region ���ӻ�ɾ��һ������
        /// <summary>
        /// ���ӻ�ɾ��һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int AddOrUpdate(FS.HISFC.Models.Pharmacy.Company company)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.AddOrUpdate(company);
        }
        #endregion

        #region  ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int DeleteInfo(FS.HISFC.Models.Pharmacy.Company company)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.DeleteInfo(company);
        }
        #endregion
        #region �Ƿ��Ѿ�����
        /// <summary>
        /// �Ƿ�
        /// </summary>
        /// <param name="comCode"></param>
        /// <returns>-1 ���� ��1 û���ù� 2 �ù�</returns>
        public int IsExistCompany(string comCode)
        {
            this.SetDB(mgrCompany);
            return mgrCompany.IsExistCompany(comCode);
        }
        #endregion
        #endregion 

        #region ��������Ϣ
        #region ��ѯһ��ʱ���������Ա��Ϣ ���� ��̬����
        /// <summary>
        /// ��ѯһ��ʱ���������Ա��Ϣ ���� ��̬����
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string beginTime, string endTime)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryPatient(beginTime, endTime);
        }
        #endregion

        #region ���ݿ��Ż�ȡ���˻�����Ϣ  ע�ⲻ�ǵǼ���Ϣ
        /// <summary>
        /// ���ݿ��Ż�ȡ���˻�����Ϣ  ע�ⲻ�ǵǼ���Ϣ 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string cardNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryPatient(cardNo);
        }
        #endregion

        #region ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet
        /// <summary>
        /// ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet 
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryPatient(string beginTime, string endTime, ref System.Data.DataSet ds)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryPatient(beginTime, endTime, ref ds);
        }
        #endregion

        #region  ���ӻ��޸�һ������
        /// <summary>
        /// ���ӻ��޸�һ������
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int AddOrUpdate(FS.HISFC.Models.PhysicalExamination.Register register)
        {
            this.SetDB(mgrReg);
            return mgrReg.AddOrUpdate(register);
        }
        #endregion

        #region  ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="register">���Ǽ�ʵ��</param>
        /// <returns></returns>
        protected int DeleteInfo(FS.HISFC.Models.PhysicalExamination.Register register)
        {
            this.SetDB(mgrReg);
            return mgrReg.DeleteInfo(register);
        }
        #endregion
        #endregion 

        #region ��Ա��Ϣ�Ǽ�

        #region ��ѯһ��ʱ���ڵļ���Ǽ���Ϣ
        public ArrayList QueryCompanyRegister(string beginDate, string endDate)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryCompanyRegister(beginDate, endDate);
        }
        #endregion 
        #region ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet
        /// <summary>
        /// ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet 
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetRegisterPatient(string beginTime, string endTime, ref System.Data.DataSet ds)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetRegisterPatient(beginTime, endTime, ref ds);
        }
        #endregion

        #region �������ѯ�����Ա��Ϣ
        /// <summary>
        /// �������ѯ�����Ա��Ϣ
        /// </summary>
        /// <param name="ID">���� �򽡿�������,��쵥λ,���� </param>
        /// <param name="ds"></param>
        /// <param name="type">����</param>
        /// <returns></returns>
        public int GetRegisterPatient(string beginTime, string endTime, string ID, ref System.Data.DataSet ds,FS.HISFC.BizLogic.PhysicalExamination.Enum.ExamType type)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetRegisterPatient(beginTime, endTime, ID, ref ds, type);
        }
        #endregion

        #region �������Ż�ȡ���Ǽ���Ϣ
        /// <summary>
        /// �������Ż�ȡ���Ǽ���Ϣ
        /// </summary>
        /// <param name="clinicNO">����</param>
        /// <returns></returns>
        public FS.HISFC.Models.PhysicalExamination.Register GetRegisterByClinicNO(string clinicNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetRegisterByClinicNO(clinicNO);
        }
        #endregion

        #region ���ݿ��Ų�ѯ
        /// <summary>
        /// ���ݿ��Ų�ѯ�Ǽ���Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByCardNO(string cardNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryRegisterByCardNO(cardNo);
        }
        /// <summary>
        /// ���ݿ��Ż�ȡ�������ͼ������Ǽ���Ա��Ϣ ������
        /// </summary>
        /// <param name="cardNo">����</param>
        /// <returns></returns>
        public ArrayList QueryCollectivityRegisterByCardNO(string cardNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryCollectivityRegisterByCardNO(cardNo);
        }
        #endregion

        #region ���ݽ��������Ų�ѯ�Ǽ���Ϣ
        /// <summary>
        /// ���ݽ��������Ų�ѯ�Ǽ���Ϣ
        /// </summary>
        /// <param name="archivesNO"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByArchivesNO(string archivesNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryRegisterByArchivesNO(archivesNO);
        }
        #endregion

        #region ���ӻ����ĳ������
        /// <summary>
        /// ���ӻ����ĳ������
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int AddOrUpdateRegister(FS.HISFC.Models.PhysicalExamination.Register register)
        {
            this.SetDB(mgrReg);
            return mgrReg.AddOrUpdateRegister(register);
        }
        #endregion

        #region ɾ��һ������
        /// <summary>
        /// ���������ˮ�� ɾ��һ������
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int DeleteInfoRegister(string clinicNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.DeleteInfoRegister(clinicNo);
        }

        #endregion

        #region ��ȡ �������
        /// <summary>
        /// ��ȡ��������¼
        /// </summary>
        /// <param name="compCode"></param>
        /// <returns></returns>
        public ArrayList QueryCollectivity(string compCode)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryCollectivity(compCode);
        }
        #endregion

        #region ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// <summary>
        /// ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// </summary>
        /// <param name="collectivityCode"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByCollectivityCode(string collectivityCode)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryRegisterByCollectivityCode(collectivityCode);
        }
        #endregion

        #region ���ݼ������Ż�ȡ����쵥λ�����μ�������
        /// <summary>
        /// ���ݼ������Ż�ȡ����쵥λ�����μ�������
        /// </summary>
        /// <param name="collectivityCode"></param>
        /// <returns></returns>
        public ArrayList QueryCompanyByCollectivityCode(string collectivityCode)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryCompanyByCollectivityCode(collectivityCode);
        }
        #endregion

        #endregion

        #region ������Ϣ
        #region  ���������ϸ��ȷ���ˣ�ȷ���¼�
        /// <summary>
        /// ���������ϸ��ȷ���ˣ�ȷ���¼�
        /// ��Ҫ��ֵ obj.NoBackQty, obj.SequenceNO, obj.IsConfirm, obj.ConformOper.ID
        /// </summary>
        /// <param name="MoOrder">ҽ����ˮ��</param>
        /// <param name="ConfirmFlag">ȷ�ϱ�־ 0 δ�շ�,1�շ� 2 ִ��</param>
        /// <param name="NoBackQty">��������</param>
        /// <returns></returns>
        public int UpdateConfirmInfo(string MoOrder, string ConfirmFlag, int NoBackQty)
        {
            this.SetDB(mgrReg);
            return mgrReg.UpdateConfirmInfo(MoOrder, ConfirmFlag, NoBackQty);
        }
        #endregion

        #region ���»�ɾ�� ��������ϸ ���QtyΪ�� ��ɾ��
        /// <summary>
        /// ���»�ɾ�� ��������ϸ ��� 
        /// </summary>
        /// <param name="seqenceNO"></param>
        /// <param name="Qty"></param>
        /// <param name="BackQty"></param>
        /// <returns></returns>
        public int UpdateOrDeleteItemListBySequenceNO(string seqenceNO, int Qty, int BackQty)
        {
            this.SetDB(mgrReg);
            if (Qty == 0)
            {
                return mgrReg.DeleteItemListBySeqenceNO(seqenceNO);
            }
            else
            {
                return mgrReg.UpdateNobackNum(seqenceNO, Qty, BackQty);
            }
        }
        #endregion 
        #region �����շѱ�־
        /// <summary>
        /// �����շѱ�־
        /// </summary>
        /// <param name="feeFlag">  0 δ�շѣ�1�����շѣ�2���� </param>
        /// <param name="MoOrder">ҽ����ˮ��</param>
        /// <returns></returns>
        public int UpdateItemListFeeFlagByMoOrder(string feeFlag, string MoOrder)
        {
            this.SetDB(mgrReg);
            return mgrReg.UpdateItemListFeeFlagByMoOrder(feeFlag, MoOrder);
        }
         /// <summary>
        /// �����շѱ�־
        /// </summary>
        /// <param name="feeFlag">  0 δ�շѣ�1�����շѣ�2���� </param>
        /// <param name="RecipeSeq">�շ���Ϻ�</param>
        /// <returns></returns>
        public int UpdateItemListFeeFlagByRecipeSeq(string feeFlag, string RecipeSeq)
        {
            this.SetDB(mgrReg);
            return mgrReg.UpdateItemListFeeFlagByRecipeSeq(feeFlag, RecipeSeq);
        }
        #endregion
        #region  ��ȡ�����ϸ
        /// <summary>
        /// ��ȡ�����ϸ
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryItemListByClinicNO(string clinicNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryItemListByClinicNO(clinicNO);
        }
        /// <summary>
        /// ������ˮ�Ż�ȡ�����Ŀ��ϸ
        /// </summary>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.PhysicalExamination.ItemList GetItemListBySequence(string sequenceNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetItemListBySequence(sequenceNO);
        }
        #endregion

        #region ɾ��ĳһ�������ϸ
        /// <summary>
        /// ĳһ�������ϸ
        /// </summary>
        /// <param name="seqenceNO"></param>
        /// <returns></returns>
        public int DeleteItemListBySeqenceNO(string seqenceNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.DeleteItemListBySeqenceNO(seqenceNO);
        }
        #endregion
       
        #endregion 
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum EnumServiceEditTypes
    {
        Add, //����
        Modify,//�޸�
        Delete,//ɾ��
        Disuse //����
    }
}
