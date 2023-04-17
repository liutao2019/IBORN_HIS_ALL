using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Account
{
    
    /// <summary>
    /// FS.HISFC.Models.Account.AccountRecord<br></br>
    /// [��������: �����ʻ�������ʵ��]<br></br>
    /// [�� �� ��: ·־��]<br></br>
    /// [����ʱ��: 2007-05-22]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class AccountCardRecord : FS.FrameWork.Models.NeuObject
    {
        #region ����
        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        private string markNO = string.Empty;
        /// <summary>
        /// ��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
        /// </summary>
        private FS.FrameWork.Models.NeuObject markType = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ���￨��
        /// </summary>
        private string cardNO = string.Empty;
        /// <summary>
        /// ����������
        /// </summary>
        private EnumMarkOperateTypesService operateTypes = new EnumMarkOperateTypesService();
        /// <summary>
        /// ��������ʵ��
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ���ɱ���
        /// </summary>
        private decimal cardMoney = 0m;
        #endregion

        #region ����
        /// <summary>
        /// ��ݱ�ʶ����
        /// </summary>
        public string MarkNO
        {
            get
            {
                return markNO;
            }
            set
            {
                markNO = value;
            }
        }
        /// <summary>
        /// ��ݱ�ʶ����� 1�ſ� 2IC�� 3���Ͽ�
        /// </summary>
        public FS.FrameWork.Models.NeuObject MarkType
        {
            get
            {
                return markType;
            }
            set
            {
                markType = value;
            }
        }
        /// <summary>
        /// ���￨��
        /// </summary>
        public string CardNO
        {
            get
            {
                return cardNO;
            }
            set
            {
                cardNO = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public EnumMarkOperateTypesService OperateTypes
        {
            get
            {
                return operateTypes;
            }
            set
            {
                operateTypes = value;
            }
        }

        /// <summary>
        /// ��������ʵ��
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        /// <summary>
        /// ���ɱ���
        /// </summary>
        public decimal CardMoney
        {
            get
            {
                return cardMoney;
            }
            set
            {
                cardMoney = value;
            }
        }
        #endregion

        #region ����
        public new AccountCardRecord Clone()
        {
            AccountCardRecord obj = base.Clone() as AccountCardRecord;
            //obj.operateTypes = this.operateTypes.Clone() as EnumMarkOperateTypesService;
            //obj.markType = this.markType.Clone() as EnumMarkTypesService;
            obj.oper = this.oper.Clone();
            return obj;
        }
        #endregion
    }
}
