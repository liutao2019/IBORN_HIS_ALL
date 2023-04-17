using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Account;

namespace FS.HISFC.BizProcess.Interface.Account
{
    /// <summary>
    /// ��ӡ���ʻ���ӡƾ֤
    /// </summary>
    public interface IPrintCreateAccount
    {
        /// <summary>
        /// Ϊ��ӡUC��ֵ
        /// </summary>
        /// <param name="account">�ʻ�ʵ��</param>
        void SetValue(HISFC.Models.Account.Account account);
        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();

    }

    /// <summary>
    /// ��ӡԤ�����վ�
    /// </summary>
    public interface IPrintPrePayRecipe
    {
        /// <summary>
        /// Ϊ��ӡUC��ֵ
        /// </summary>
        /// <param name="account">�ʻ�ʵ��</param>
        void SetValue(HISFC.Models.Account.PrePay prepay);
        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();
    }

    /// <summary>
    /// ��ӡ���ʻ����
    /// </summary>
    public interface IPrintCancelVacancy
    {
        /// <summary>
        /// Ϊ��ӡUC��ֵ
        /// </summary>
        /// <param name="account">�ʻ�ʵ��</param>
        void SetValue(HISFC.Models.Account.AccountRecord accountRecord);
        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();
    }

    /// <summary>
    /// �ʻ�����ƾ֤��ӡ
    /// </summary>
    public interface IPrintOperRecipe
    {
        /// <summary>
        /// Ϊ��ӡUC��ֵ
        /// </summary>
        /// <param name="accountRecord"></param>
        void SetValue(HISFC.Models.Account.AccountRecord accountRecord);
        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();
    }

    /// <summary>
    /// �߱����߱�ǩ��ӡ
    /// </summary>
    public interface IPrintLable
    {
        /// <summary>
        /// ��ӡ�߱����߱�ǩ
        /// </summary>
        /// <param name="accountCard"></param>
        void PrintLable(AccountCard accountCard);
    }

    /// <summary>
    /// [��������: �����ʻ�Ԥ�����ӡ]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2006-6-22]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IAccountPrint
    {
        /// <summary>
        /// ���ô�ӡ����
        /// </summary>
        /// <param name="account">�ʻ�ʵ��</param>
        /// <returns></returns>
        int PrintSetValue(FS.HISFC.Models.Account.Account account);
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        int Print();
    }

    public interface IPassWord
    {
        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        bool ValidPassWord
        {
            get;
        }
        /// <summary>
        /// ���￨��
        /// </summary>
        FS.HISFC.Models.RADT.Patient Patient
        {
            get;
            set;
        }
        /// <summary>
        /// �Ƿ���֤����
        /// </summary>
        bool IsOK
        {
            get;
        }
    }
    
}
