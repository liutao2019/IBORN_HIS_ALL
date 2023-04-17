using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.Models.Account
{
    
    [Serializable]
    public class EnumOperTypesService : FS.HISFC.Models.Base.EnumServiceBase
    {
        public EnumOperTypesService() 
        {
            this.Items[OperTypes.PrePay] = "Ԥ����";
            this.Items[OperTypes.NewAccount] = "�½��ʻ�";
            this.Items[OperTypes.StopAccount] = "ͣ�ʻ�";
            this.Items[OperTypes.AginAccount] = "�����ʻ�";
            this.Items[OperTypes.Pay] = "�ʻ�֧��";
            this.Items[OperTypes.CancelPay] = "�˷��뻧";
            this.Items[OperTypes.CancelAccount] = "ע���ʻ�";
            this.Items[OperTypes.EmpowerPay] = "��Ȩ֧��";
            this.Items[OperTypes.CancelPrePay] = "��Ԥ����";
            this.Items[OperTypes.EditPassWord] = "�޸�����";
            this.Items[OperTypes.BalanceVacancy] = "�������";
            this.Items[OperTypes.Empower] = "��Ȩ";
            this.Items[OperTypes.CancelEmpower] = "ȡ����Ȩ";
            this.Items[OperTypes.EmpowerCancelPay] = "��Ȩ�˷��뻧";
            this.Items[OperTypes.EditEmpowerInfo] = "�޸���Ȩ��Ϣ";
            this.Items[OperTypes.RevertEmpower] = "�ָ���Ȩ";

            // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
            this.Items[OperTypes.AccountTaken] = "ȡ��";
        }

        #region ����
        /// <summary>
        /// �������
        /// </summary>
        OperTypes operTypes;
        /// <summary>
        /// �洢ö��
        /// </summary>
        protected static Hashtable items = new Hashtable();
        #endregion

        #region ����
        /// <summary>
        /// ����ö��
        /// </summary>
        protected override Hashtable Items
        {
            get 
            { 
                return items; 
            }
        }
        /// <summary>
        /// ö����Ŀ
        /// </summary>
        protected override System.Enum EnumItem
        {
            get 
            {
                return operTypes; 
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// �õ�ö�ٵ�NeuObject����
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
        #endregion  

      
    }
    #region ��������ö��
    /// <summary>
    /// ��������0Ԥ����1���ʻ�2ͣ�ʻ�3�����ʻ�4֧��5�˷��뻧
    /// </summary>
    public enum OperTypes
    {
        /// <summary>
        /// Ԥ����
        /// </summary>
        PrePay = 0,
        /// <summary>
        /// �½��ʻ�
        /// </summary>
        NewAccount=1,
        /// <summary>
        /// ͣ�ʻ�
        /// </summary>
        StopAccount=2,
        /// <summary>
        /// �����ʻ�
        /// </summary>
        AginAccount=3,
        /// <summary>
        /// ֧��
        /// </summary>
        Pay=4,
        /// <summary>
        /// �˷��뻧
        /// </summary>
        CancelPay=5,
        /// <summary>
        /// ע���ʻ�
        /// </summary>
        CancelAccount=6,
        /// <summary>
        /// ��Ȩ֧��
        /// </summary>
        EmpowerPay=7,
        /// <summary>
        /// ��Ԥ����
        /// </summary>
        CancelPrePay=8,
        /// <summary>
        /// �޸�����
        /// </summary>
        EditPassWord=9,
        /// <summary>
        /// �������
        /// </summary>
        BalanceVacancy=10,
        /// <summary>
        /// ��Ȩ
        /// </summary>
        Empower=11,
        /// <summary>
        /// ȡ����Ȩ
        /// </summary>
        CancelEmpower=12,
        /// <summary>
        /// ��Ȩ�˷��뻧
        /// </summary>
        EmpowerCancelPay = 13,
        /// <summary>
        /// �޸���Ȩ��Ϣ
        /// </summary>
        EditEmpowerInfo=14,
        /// <summary>
        /// �ָ���Ȩ
        /// </summary>
        RevertEmpower = 15,
        /// <summary>
        /// �˻�ȡ��
        /// {4679504A-CEDA-44a8-8C67-DB7F847C6450}
        /// </summary>
        AccountTaken = 16
    };
    #endregion
}
