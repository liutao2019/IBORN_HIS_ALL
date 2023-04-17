using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br> PayParameters</br>
    /// <br>[��������: ���ʲ���ʵ��]</br>
    /// <br>[�� �� ��: �εº�]</br>
    /// <br>[����ʱ��: 2008-07-03]</br>
    ///     <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class PayParameters : Neusoft.FrameWork.Models.NeuObject
    {

       #region �ֶ�
        /// <summary>
        /// �ؼ�������
        /// </summary>
        private string keyType=string.Empty;       

        /// <summary>
        /// �ؼ������
        /// </summary>
        private string keyCode=string.Empty;

        /// <summary>
        /// ���������
        /// </summary>
        private string payItemCode = string.Empty;

        /// <summary>
        /// ������ֵ
        /// </summary>
        private decimal payItemValue = 0.0m;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new OperEnvironment();

��������#endregion

       #region ����
        /// <summary>
        /// �ؼ�������
        /// </summary>
        public string KeyType
        {
            get 
            { 
                return keyType; 
            }
            set 
            { 
                keyType = value; 
            }
        }

        /// <summary>
        /// �ؼ������
        /// </summary>
        public string KeyCode
        {
            get 
            { 
                return keyCode; 
            }
            set 
            { 
                keyCode = value; 
            }
        }

        /// <summary>
        /// ���������
        /// </summary>
        public string PayItemCode
        {
            get 
            { 
                return payItemCode; 
            }
            set 
            { 
                payItemCode = value;
            }
        }
        
        /// <summary>
        /// ������ֵ
        /// </summary>
        public decimal PayItemValue
        {
            get 
            { 
                return payItemValue; 
            }
            set 
            { 
                payItemValue = value; 
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
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
        #endregion

       #region ��¡����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new PayParameters Clone()
        {
            PayParameters payParameters = base.Clone() as PayParameters;
            payParameters.Oper = this.Oper.Clone();

            return payParameters;

        }
        #endregion
    }
}
