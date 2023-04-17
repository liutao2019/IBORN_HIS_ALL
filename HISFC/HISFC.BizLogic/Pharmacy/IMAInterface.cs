using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.Pharmacy
{
    public interface IMAOutManager
    {
        string ErrStr
        {
            get;
            set;
        }

        /// <summary>
        /// ��ȡ��ǰ�����
        /// </summary>
        /// <param name="outputStore">������ʵ��</param>
        /// <param name="storageNum">���ؿ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int GetStorageNum(FS.HISFC.Models.IMA.IMAStoreBase outputStore, out decimal storageNum);

        /// <summary>
        /// ��ȡ�����ϸ��¼
        /// </summary>
        /// <param name="outputStore">������ʵ��</param>
        /// <returns>�ɹ����ؿ���¼���� ʧ�ܷ���null</returns>
        List<FS.HISFC.Models.IMA.IMAStoreBase> QueryStorageList(FS.HISFC.Models.IMA.IMAStoreBase outputStore);

        /// <summary>
        /// ��ȡ�³��ⵥ��ˮ��
        /// </summary>
        /// <returns>�ɹ������³��ⵥ��ˮ�� ʧ�ܷ���null</returns>
        string GetNewOutputNO();

        /// <summary>
        /// ���ݱ����γ�������Ϣ�Գ�����Ϣ���и�ֵ
        /// </summary>
        /// <param name="storeInfo">�����γ�������Ϣ</param>
        /// <param name="outputStore">������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int FillOutputInfo(FS.HISFC.Models.IMA.IMAStoreBase storeInfo, ref FS.HISFC.Models.IMA.IMAStoreBase outputStore);

        /// <summary>
        /// ��������¼
        /// </summary>
        /// <param name="outputStore">������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int SetOutput(FS.HISFC.Models.IMA.IMAStoreBase outputStore);

        /// <summary>
        /// �������˿�����
        /// </summary>
        /// <param name="outputReturnQty">�����˿���Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int UpdateOutputReturnQty(FS.HISFC.Models.IMA.IMAStoreBase outputReturnQty);

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="storeInfo">�����γ�������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int SetStorage(FS.HISFC.Models.IMA.IMAStoreBase storeInfo);

        /// <summary>
        /// ���ݳ�����Ϣ ��������¼
        /// </summary>
        /// <param name="outputStore">������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int SetInput(FS.HISFC.Models.IMA.IMAStoreBase outputStore);

        /// <summary>
        /// ���ݳ�����ˮ�Ż�ȡ�����¼
        /// </summary>
        /// <param name="outputNO">������ˮ��</param>
        /// <returns>�ɹ����س����¼���� ʧ�ܷ���null</returns>
        List<FS.HISFC.Models.IMA.IMAStoreBase> QueryOutputList(string outputNO);

        /// <summary>
        /// ���ݳ����¼��Ϣ�Գ����˿��¼���и�ֵ
        /// 
        /// output.Item.PriceCollection.RetailPrice = info.Item.PriceCollection.RetailPrice;	//���ۼ� ����ԭ����۸��˿�
        /// </summary>
        /// <param name="outputStore">�����¼��Ϣ</param>
        /// <param name="outputReturnStore">�����˿���Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int FillOutputReturnInfo(FS.HISFC.Models.IMA.IMAStoreBase outputStore, ref FS.HISFC.Models.IMA.IMAStoreBase outputReturnStore);

        /// <summary>
        /// �����˿�ʱ �Լ۸����仯ʱ ���µ��ۼ�¼
        /// </summary>
        /// <param name="privOutputStore">ԭ�����¼</param>
        /// <param name="outputReturnStore">�����˿��¼</param>
        /// <param name="sysTime">��ǰ����ʱ��</param>
        /// <param name="serialNo">�˿�˳���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int OutputAdjust(FS.HISFC.Models.IMA.IMAStoreBase privOutputStore, FS.HISFC.Models.IMA.IMAStoreBase outputReturnStore, DateTime sysTime, int serialNo);
    }

    public interface IMAInManager
    {
        string ErrStr
        {
            get;
            set;
        }

        int SetInput(FS.HISFC.Models.IMA.IMAStoreBase inputStore);

        /// <summary>
        /// ���/�˿����
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        int InputAdjust(FS.HISFC.Models.IMA.IMAStoreBase inputStore);

        /// <summary>
        /// ���������Ϣ���¿��
        /// </summary>
        /// <param name="inputStore"></param>
        /// <param name="storageState"></param>
        /// <returns></returns>
        int SetStorage(FS.HISFC.Models.IMA.IMAStoreBase inputStore, string storageState);

        /// <summary>
        /// ����׼ ��������¼��Ϣ
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        int UpdateApproveInfo(FS.HISFC.Models.IMA.IMAStoreBase inputStore);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputStore"></param>
        /// <param name="storageState"></param>
        /// <returns></returns>
        int UpdateStorageState(FS.HISFC.Models.IMA.IMAStoreBase inputStore, string storageState, bool isUpdateStorage);

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        int UpdateApplyInfo(FS.HISFC.Models.IMA.IMAStoreBase inputStore);

        /// <summary>
        /// ���³�����Ϣ
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        int UpdateOutputInfo(FS.HISFC.Models.IMA.IMAStoreBase inputStore);

        /// <summary>
        /// ���¸���ĿȫԺ�����Ϣ
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        int UpdateItemInfoForStorage(FS.HISFC.Models.IMA.IMAStoreBase inputStore);

        /// <summary>
        /// ���¸���Ŀ������Ϣ
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        int UpdateItemInfoForBase(FS.HISFC.Models.IMA.IMAStoreBase inputStore);

        /// <summary>
        /// ���¸���Ŀ������Ϣ
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        int UpdateItemInfoForOutput(FS.HISFC.Models.IMA.IMAStoreBase inputStore);
    }
}
