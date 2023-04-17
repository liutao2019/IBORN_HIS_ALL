using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Material
{
    /// <summary>
    /// IMatFee<br></br>
    /// <Font color='#FF1111'>[��������: �����շѽӿ�]</Font><br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2010-07-06]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public interface IMatFee
    {

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        void SetTrans(System.Data.IDbTransaction trans);

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string Err
        {
            get;
            set;
        }

        /// <summary>
        /// ������
        /// </summary>
        string ErrCode
        {
            get;
            set;
        }

        /// <summary>
        /// ���ݿ������
        /// </summary>
        int DBErrCode
        {
            get;
            set;
        }

        /// <summary>
        /// �����˷�����
        /// </summary>
        /// <param name="outputList"></param>
        /// <param name="isCancelApply"></param>
        /// <returns></returns>
        int ApplyMaterialFeeBack(List<FS.HISFC.Models.FeeStuff.Output> outputList, bool isCancelApply);

        /// <summary>
        /// ͨ��������Ŀ��Ų�ѯ������Ŀ��Ϣ
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        FS.HISFC.Models.FeeStuff.MaterialItem GetMetItem(string itemCode);

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="outNo">������ˮ��</param>
        /// <param name="stockNo">������</param>
        /// <returns>�����¼</returns>
        FS.HISFC.Models.FeeStuff.Output GetOutput(string outNo, string stockNo);

        /// <summary>
        /// �����շ�
        /// </summary>
        /// <param name="feeItemLists">�շ���Ŀ�б�</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        int MaterialFeeOutput(System.Collections.ArrayList feeItemLists);

        /// <summary>
        /// �����˷�ȷ��
        /// </summary>
        /// <param name="outputList">�շ���Ŀ�����б�</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        int MaterialFeeOutputBack(List<FS.HISFC.Models.FeeStuff.Output> outputList);

        /// <summary>
        /// �����˷�ȷ��
        /// </summary>
        /// <param name="returnApplyList">�շ���Ŀ�����б�</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        int MaterialFeeOutputBack(List<FS.HISFC.Models.Fee.ReturnApplyMet> returnApplyList);

        /// <summary>
        /// �����˷�
        /// </summary>
        /// <param name="backOutput">�շ���Ŀ</param>
        /// <returns>�ɹ���1��ʧ�ܣ�-1</returns>
        int MaterialFeeOutputBack(FS.HISFC.Models.FeeStuff.Output backOutput);

        /// <summary>
        /// �����˷�ȷ��
        /// </summary>
        /// <param name="feeitemLists"></param>
        /// <returns></returns>
        int MaterialFeeOutputBack(System.Collections.ArrayList feeitemLists);

        /// <summary>
        /// �����˿�
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <param name="sequenceNO"></param>
        /// <param name="backQty"></param>
        /// <param name="trans"></param>
        /// <param name="backOutList"></param>
        /// <returns></returns>
        int MaterialOutpubBack(string recipeNO, int sequenceNO, decimal backQty, System.Data.IDbTransaction trans, ref List<FS.HISFC.Models.FeeStuff.Output> backOutList);

        /// <summary>
        /// ���ʿۿ�
        /// </summary>
        /// <param name="feeItem"></param>
        /// <param name="trans"></param>
        /// <param name="isCompare"></param>
        /// <param name="outListCollect"></param>
        /// <returns></returns>
        int MaterialOutput(FS.HISFC.Models.Fee.FeeItemBase feeItem, System.Data.IDbTransaction trans, ref bool isCompare, ref List<FS.HISFC.Models.FeeStuff.Output> outListCollect);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="storeDetail">�����ϸ��Ϣ</param>
        /// <param name="outQty">������</param>
        /// <returns>1 �ɹ� -1 ʧ��</returns>
        int OutputByStore(FS.HISFC.Models.FeeStuff.StoreDetail storeDetail, decimal outQty);

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="outNo">������ˮ��</param>
        /// <param name="itemCode">���ʱ���</param>
        /// <returns>�����¼�б�</returns>
        List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(string outNo, string itemCode);

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="outNo">������ˮ��</param>
        /// <returns>�����¼�б�</returns>
        List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(string outNo);

        /// <summary>
        /// ��ȡ�����¼
        /// </summary>
        /// <param name="feeItemList">�շ���Ŀ</param>
        /// <returns>�����¼�б�</returns>
        List<FS.HISFC.Models.FeeStuff.Output> QueryOutput(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList);

        /// <summary>
        /// ���ݿ�沿�����Ŀ����ȡ��������Ϣ
        /// ������Ŀ��Ϣ,�������Ѷ��պͲ��շѵ�������Ŀ
        /// </summary>
        /// <param name="storeDeptCode">��沿��</param>
        /// <returns>�ɹ����ؿ���������� ʧ�ܷ���null</returns>
        List<FS.HISFC.Models.FeeStuff.MaterialItem> QueryStockHeadItemForFee(string storeDeptCode);

        /// <summary>
        /// ���ݿ����ұ����ȡ����δ����������Ŀ�����ϸ
        /// </summary>
        /// <param name="storeDeptCode"></param>
        /// <returns></returns>
        List<FS.HISFC.Models.FeeStuff.StoreDetail> QueryUnCompareMaterialStoreDetail(string storeDeptCode);

        /// <summary>
        /// ���ݿ����ұ����ȡ����δ����������Ŀ������
        /// </summary>
        /// <param name="storeDeptCode"></param>
        /// <returns></returns>
        List<FS.HISFC.Models.FeeStuff.StoreHead> QueryUnCompareMaterialStoreHead(string storeDeptCode);

        /// <summary>
        /// ������ʳ����¼ ������Ӧ�շѼ�¼�����š���������Ŀ��ˮ��
        /// </summary>
        /// <param name="outListCollect"></param>
        /// <param name="recipeNO"></param>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        int UpdateFeeRecipe(List<FS.HISFC.Models.FeeStuff.Output> outListCollect, string recipeNO, int sequenceNO);
    }
}
