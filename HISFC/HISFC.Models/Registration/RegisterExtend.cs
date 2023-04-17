using System;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Account;
using System.Collections.Generic;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// Register<br></br>
    /// [��������: �Һ���չ��Ϣʵ��]<br></br>
    /// <summary>
    [Serializable]
    public class RegisterExtend : Patient
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisterExtend()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            // 
        }

        #region ����
        /// <summary>
        /// ԤԼ�Һ�����ID
        /// </summary>
        private string bookingTypeId = string.Empty;

        /// <summary>
        /// ԤԼ�Һ���������
        /// </summary>
        private string bookingTypeName = string.Empty;
        #endregion

        #region ����
        /// <summary>
        /// ԤԼ�Һ�����ID
        /// </summary>
        public string BookingTypeId
        {
            get
            {
                return bookingTypeId;
            }
            set
            {
                bookingTypeId = value;
            }
        }

        /// <summary>
        /// ԤԼ�Һ���������
        /// </summary>
        public string BookingTypeName
        {
            get
            {
                return bookingTypeName;
            }
            set
            {
                bookingTypeName = value;
            }
        }
        #endregion

        #region ����
        ///// <summary>
        /////  �Һŵĸ���
        ///// </summary>
        ///// <returns></returns>
        public new RegisterExtend Clone()
        {
            RegisterExtend regExtend = base.Clone() as RegisterExtend;
            return regExtend;
        }
        #endregion
    }
}
